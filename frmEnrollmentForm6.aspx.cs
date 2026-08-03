using PMS.Crypto.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmEnrollmentForm6 : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    SqlInjection sqlInjection = new SqlInjection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                UserLevelFilter();
                FillClass();
                FillSocialCat();
                FillENrollment();
                FillEduStauts();
                ViewState["1"] = "ss";
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }

    public int SaveDataSchool(string VillageCode, string SchoolCode, string SchoolCodeID, string Name, string Status, string Createdate, string CreateBy, string sysFlag, string DISECode, string SchoolLevel, string Govt_DiseCode, string Mangment)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@SchoolCodeID", SchoolCodeID),
            new SqlParameter("@Name", Name),
            new SqlParameter("@Status", Status),
            new SqlParameter("@Createdate", Createdate),
            new SqlParameter("@CreateBy", CreateBy),
            new SqlParameter("@sysFlag", sysFlag),
            new SqlParameter("@DISECode", DISECode),
            new SqlParameter("@SchoolLevel", SchoolLevel),
            new SqlParameter("@Govt_DiseCode", Govt_DiseCode),
            new SqlParameter("@Mangment", Mangment),

            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSchoolData", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public void SaveData()
    {
        string strUnique = "0";
        string HHNo = txtHHNo.Text.Trim();
        string ChildName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtChildName.Text.Trim());
        string FathersName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFatherName.Text.Trim());
        string strSerial = txtSrno.Text.Trim();

        string dllClasss = dllClass.SelectedValue;
        string Scat = ddlScat.SelectedValue.ToString();
        Int32 Gender = Convert.ToInt32(ddlGender.SelectedValue);

        Int32 DoAv = 1;

        //if (cmbGender == "2")
        //{
        //    Gender = 2;
        //}
        //else
        //{
        //    Gender = 1;
        //}

        DateTime DOB;
        DateTime AsDob;
        Int32 Age = 0;



        Int32 ymyear = Convert.ToInt32(Session["mYear"].ToString());
        string Adminision = txtBirth.Text;

        string[] b = Adminision.Split('/');
        string DateAdminision = b[2] + '-' + b[1] + '-' + b[0];

        DateTime DOBStudent = Convert.ToDateTime(txtDobDate.Text);
        DateTime dtason = DOBStudent;
        Age = ymyear - dtason.Year;

        DOB = DOBStudent;

        string[] c = txtDobDate.Text.Split('/');
        string ChildDOB = c[2] + '-' + c[1] + '-' + c[0];

        string DOB1 = DOBStudent.ToString();
        string[] words = DOB1.Split('/');
        Int32 iyear = Convert.ToInt32(dtason.Year) + Age;
        string lYear = iyear.ToString();
        AsDob = Convert.ToDateTime(DateTime.Today); ;
        // AsDob = words[2] + '-' + words[1] + '-' + iyear.ToString();
        string StudentTSInsertQuery = "";
        string concatstr = "";
        string concatCvalstr = "";
        string concatPvalstr = "";
        if (Session["UnquieId"].ToString().Length > 6)
        {
            string unquieKey = Session["UnquieId"].ToString();

            string encChildName = CryptoService.Encrypt(ChildName);
            string encFathersName = CryptoService.Encrypt(FathersName);
            string encMotherName = CryptoService.Encrypt(txtmotherName.Text);
            string encChildDOB = CryptoService.Encrypt(ChildDOB);

            string encSamgra = string.Empty;

            if (!string.IsNullOrEmpty(txtSamgra.Text))
            {
                encSamgra = CryptoService.Encrypt(txtSamgra.Text);
            }

            string[] Str = { ddlFC.SelectedValue, encChildName, encFathersName, encMotherName, dllClass.SelectedValue, txtSrno.Text, Convert.ToDateTime(txtBirth.Text).ToString("yyyy-MM-dd"), encChildDOB, ddlScat.SelectedValue, ddlGender.SelectedValue, txtSamgra.Text, txtHHNo.Text, ddlRemarks.SelectedValue, ddlEduationStatus.SelectedValue };

            DataTable dt = ALLQueryinPage("", "", "", "", Session["UnquieId"].ToString(), "11");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Rows[0][i].ToString() != Str[i])
                    {
                        concatstr = concatstr + "," + "'" + dt.Columns[i].ColumnName + "'";
                        concatCvalstr = concatCvalstr + "," + Str[i];
                        concatPvalstr = concatPvalstr + "," + dt.Rows[0][i].ToString();
                    }
                }
            }

            int Iocunt = SaveDataEnrolment(Session["UnquieId"].ToString(), ddlTbName.SelectedValue, ddlFC.SelectedValue, encMotherName, "", ddlVillage.SelectedValue, txtSrno.Text, ddlScat.SelectedValue, dllClass.SelectedValue, ddlYear.SelectedValue, encChildName, encFathersName, encChildName, encFathersName, Gender.ToString(), ddlSchool.SelectedValue, Convert.ToDateTime(Adminision).ToString("yyyy-MM-dd"), DoAv.ToString(), encChildDOB, Age.ToString(), AsDob.ToString("yyyy-MM-dd"), "1", DateTime.Now.ToString("yyyy-MM-dd"), Session["username"].ToString(), txtHHNo.Text, "2", txtSurveyVillage.Text, encSamgra, "11", "1", DateTime.Now.ToString("yyyy-MM-dd"), "0", DateTime.Now.ToString("yyyy-MM-dd"), ddlRemarks.SelectedValue, "U");

            int Iocunt1 = UpdateEnrolment(Session["UnquieId"].ToString(), Session["username"].ToString());


            if (Iocunt > 0)
            {

                if (concatstr != "")
                {
                    int Cunt = SaveDataEnrolmentHistory(Session["UnquieId"].ToString(), concatstr.Substring(1), concatPvalstr.Substring(1), concatCvalstr.Substring(1), Session["UserID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"));
                }

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

                txtChildName.Text = "";
                txtFatherName.Text = "";
                txtHHNo.Text = "";
                txtSrno.Text = "";
                txtSamgra.Text = "";
                txtSurveyVillage.Text = "";
                txtHHNo.Focus();
                txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlEduationStatus.SelectedIndex = 0;

                dllClass.SelectedIndex = 0;

                ddlScat.SelectedIndex = 0;
                txtBirth.Text = "";
                txtDobDate.Text = "";
                //this.Close();
                LoadData();
                MpexdrDistrict.Show();
                Session["UnquieId"] = "";
                //Response.End();
                // Response.Redirect("~/frmEnrollmentForm6.aspx?ID=1");
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ClosePage", "window.close();", true);


            }
        }
        else
        {

            if (ddlSchool.SelectedValue == "99")
            {

                string Othersname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSchooName.Text.Trim());

                string UCOde1 = objComman.Generate_RandomString(8);
                DataTable dtDid2 = ALLQueryinPage(txtDiseCode.Text.Trim(), ddlVillage.SelectedValue, "", "", "", "10");
                if (dtDid2.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode already exists')</script>", false);
                    MpexdrDistrict.Show();
                    return;
                }
                int iSchoolCount = SaveDataSchool(ddlVillage.SelectedValue, UCOde1, "0", Othersname, "5", DateTime.Now.ToString("yyyy-MM-dd"), Session["username"].ToString(), "0", txtDiseCode.Text, ddlschoolLevel.SelectedValue, txtDiseCode.Text, ddlManagement.SelectedValue);
                FillSchool();
                ddlSchool.SelectedValue = UCOde1;
                IDschool.Visible = false;
                IDDise.Visible = false;
                Div13.Visible = false;
                Div14.Visible = false;
                txtSchooName.Text = "";
                txtDiseCode.Text = "";
            }


            string strQry2 = " Select [Serial] FROM tblEnrolment where [villagecode]='" + ddlVillage.SelectedValue.ToString() + "' and  SchoolCode ='" + ddlSchool.SelectedValue + "' and Serial='" + txtSrno.Text + "'  and DeleteFlag<>2";
            DataTable dtSer = objMain.LoadData(strQry2);
            if (dtSer.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);
                MpexdrDistrict.Show();
                return;
            }



            //string VillageCodwwwe = "  VillageCode in(select VillageCode from mst5Village where EGVillageCode in(select EGVillageCode from mst5Village where VillageCode='" +ddlVillage.SelectedValue + "')) ";

            Int32 ssNo = 0;
            //string strQry = " select isnull(Max(Serial),0)+1 Serial from [tblDTD]  where " + VillageCodwwwe + " ";
            //DataTable dt = objMain.LoadData(strQry);
            DataTable dt = ALLQueryinPage(ddlVillage.SelectedValue, "", "", "", "", "5");
            if (dt.Rows.Count > 0)
            {
                ssNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
            }

            string UNICOde = objMain.Generate_RandomString(8);
            string UCOde = objComman.Generate_RandomString(8);

            string encChildName = CryptoService.Encrypt(ChildName);
            string encFathersName = CryptoService.Encrypt(FathersName);
            string encMotherName = CryptoService.Encrypt(txtmotherName.Text);
            string encChildDOB = CryptoService.Encrypt(ChildDOB);

            string encSamgra = string.Empty;

            if (!string.IsNullOrEmpty(txtSamgra.Text))
            {
                encSamgra = CryptoService.Encrypt(txtSamgra.Text);
            }


            int Iocunt = SaveDataEnrolment(UNICOde, ddlTbName.SelectedValue, ddlFC.SelectedValue, encMotherName, "", ddlVillage.SelectedValue, txtSrno.Text, ddlScat.SelectedValue, dllClass.SelectedValue, ddlYear.SelectedValue, encChildName, encFathersName, encChildName, encFathersName, Gender.ToString(), ddlSchool.SelectedValue, Convert.ToDateTime(Adminision).ToString("yyyy-MM-dd"), DoAv.ToString(), encChildDOB, Age.ToString(), AsDob.ToString("yyyy-MM-dd"), "1", DateTime.Now.ToString("yyyy-MM-dd"), Session["username"].ToString(), txtHHNo.Text, "2", txtSurveyVillage.Text, encSamgra, "11", "1", DateTime.Now.ToString("yyyy-MM-dd"), "0", DateTime.Now.ToString("yyyy-MM-dd"), ddlRemarks.SelectedValue, "I");



            int Iocunt1 = objMain.SaveDataD2d(UCOde, UNICOde, ddlVillage.SelectedValue, ssNo.ToString(), ddlScat.SelectedValue, encChildName, encFathersName, Gender.ToString(), DoAv.ToString(), encChildDOB, Age.ToString(), ddlSchool.SelectedValue, ddlEnroll.SelectedValue, txtHHNo.Text.Trim(), dllClass.SelectedValue, "3", "4", AsDob.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), Session["username"].ToString(), AsDob.ToString("yyyy-MM-dd"), UNICOde, "3", "1");




            if (Iocunt > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                //if (UnquieId.Length > 6)
                //{
                //}
                //else
                //{
                //    Program.EnrollDate = DTPicker_Sur.Value;
                //    Program.Esc = Convert.ToInt32(cmbCategory.SelectedValue);
                //    Program.Escatory = Convert.ToInt32(cmbEnrollCat.SelectedValue);
                //    Program.Gender = Convert.ToInt32(cmbGender.SelectedIndex);

                //}
                txtChildName.Text = "";
                txtFatherName.Text = "";
                txtHHNo.Text = "";
                txtSrno.Text = "";
                txtSamgra.Text = "";
                txtSurveyVillage.Text = "";
                txtHHNo.Focus();
                txtBirth.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlEduationStatus.SelectedIndex = 0;

                dllClass.SelectedIndex = 0;

                ddlScat.SelectedIndex = 0;
                txtBirth.Text = "";
                txtDobDate.Text = "";
                //this.Close();
                LoadData();
                MpexdrDistrict.Show();
                //this.Close();
            }
        }

    }

    public int UpdateEnrolment(string UniqueChildCode, string CreateBy)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueChildCode", UniqueChildCode),

            new SqlParameter("@CreateBy", CreateBy),





            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateENrolmentTracker", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }

    public int SaveDataEnrolment(string UniqueChildCode, string EnTBcode, string TBFC, string MotherName, string ChildCode, string VillageCode, string Serial, string Category, string Class, string Session, string ChildName, string FatherName, string ChildNameH, string FatherNameH, string Gender, string SchoolCode, string EnrolmentDateTime, string DOBAvailable, string DOB, string AgeAson, string AsOnDateTime, string Status, string CreateDateTime, string CreateBy, string HouseNo, string DeleteFlag, string VillagenameOther, string SamgraID, string IsDoBoFlag, string IsComplete, string ActivityDateTime, string EnrolmentMatching, string SysDateTime, string remark, string Flag)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueChildCode", UniqueChildCode),
            new SqlParameter("@EnTBcode", EnTBcode),
            new SqlParameter("@TBFC", TBFC),
            new SqlParameter("@MotherName", MotherName),
            new SqlParameter("@ChildCode", ChildCode),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Serial", Serial),
            new SqlParameter("@Category", Category),
            new SqlParameter("@Class", Class),
            new SqlParameter("@Session", Session),
                  new SqlParameter("@ChildName", ChildName),
            new SqlParameter("@FatherName", FatherName),
            new SqlParameter("@ChildNameH", ChildNameH),
            new SqlParameter("@FatherNameH", FatherNameH),
            new SqlParameter("@Gender", Gender),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@EnrolmentDate", EnrolmentDateTime),
            new SqlParameter("@DOBAvailable", DOBAvailable),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@AgeAson", AgeAson),
            new SqlParameter("@AsOnDate", AsOnDateTime),
            new SqlParameter("@Status", Status),
            new SqlParameter("@CreateDate", CreateDateTime),
            new SqlParameter("@CreateBy", CreateBy),
            new SqlParameter("@HouseNo", HouseNo),
            new SqlParameter("@DeleteFlag", DeleteFlag),
            new SqlParameter("@VillagenameOther", VillagenameOther),
            new SqlParameter("@SamgraID", SamgraID),
                new SqlParameter("@IsDoBoFlag", IsDoBoFlag),

                new SqlParameter("@IsComplete", IsComplete),
                new SqlParameter("@ActivityDate", ActivityDateTime),
                  new SqlParameter("@EnrolmentMatching", EnrolmentMatching),
                 new SqlParameter("@SysDate", SysDateTime),
 new SqlParameter("@remark", remark),
 new SqlParameter("@Flag",  Flag),
 new SqlParameter("@Cat",  ddlEduationStatus.SelectedValue),




            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateENrolmentDatanew", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    public bool AddUpdate(string query)
    {
        bool result;
        using (SqlCommand sqlCommand = new SqlCommand())
        {
            SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
            try
            {
                new DataTable();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = query;

                sqlCommand.CommandTimeout = 0;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
                result = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        return result;
    }
    public void FillClassWorking()
    {

        conditions = "LookupFlag ='ECL'  and lookupcode not in(1,2,4)";

        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", dllClass, "Description", "LookupCode", "Select");



    }
    protected void Add_Click(object sender, EventArgs e)
    {
        if (ddlState.SelectedValue == "6")
        {
            txtSrno.MaxLength = 12;
        }
        else
        {
            txtSrno.MaxLength = 9;
        }
        if (ddlSchool.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select School')</script>", false);

            return;
        }

        if (ddlSchool.SelectedValue == "99")
        {
            if (txtSchooName.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter School Name')</script>", false);

                return;
            }
            if (txtDiseCode.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Govt Disecode')</script>", false);

                return;
            }
            if (ddlState.SelectedValue == "9A" || ddlState.SelectedValue == "9B" || ddlState.SelectedValue == "9D" || ddlState.SelectedValue == "9C" || ddlState.SelectedValue == "8" || ddlState.SelectedValue == "6")
            {
                if (txtDiseCode.Text.Trim().Length < 10)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be 10 digits')</script>", false);

                    return;
                }
            }


            else
            {
                if (txtDiseCode.Text.Trim().Length < 11)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be 11 digits')</script>", false);

                    return;
                }
            }

            if (ddlState.SelectedValue == "9A" || ddlState.SelectedValue == "9B" || ddlState.SelectedValue == "9C" || ddlState.SelectedValue == "9D")
            {
                String firstCharacters = txtDiseCode.Text.Trim().Substring(0, 1);
                if (firstCharacters == "9")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be Start 9')</script>", false);
                    return;
                }
            }
            if (ddlState.SelectedValue == "6")
            {
                String firstCharacters = txtDiseCode.Text.Trim().Substring(0, 1);
                if (firstCharacters == "6")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be Start 6')</script>", false);
                    return;
                }
            }
            if (ddlState.SelectedValue == "8")
            {
                String firstCharacters = txtDiseCode.Text.Trim().Substring(0, 1);
                if (firstCharacters == "8")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be Start 8')</script>", false);
                    return;
                }
            }
            if (ddlState.SelectedValue == "23")
            {
                String firstCharacters = txtDiseCode.Text.Trim().Substring(0, 2);
                if (firstCharacters == "23")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be Start 23')</script>", false);
                    return;
                }
            }
            if (ddlState.SelectedValue == "10")
            {
                String firstCharacters = txtDiseCode.Text.Trim().Substring(0, 2);
                if (firstCharacters == "10")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be Start 10')</script>", false);
                    return;
                }
            }

            string str = txtDiseCode.Text.Trim();
            int n = str.Length;
            for (int i = 0; i <= str.Length - 6; i++)
            {
                // Take 5 characters starting at i
                string substring = str.Substring(i, 6);

                // Check if all 5 characters in that substring are the same
                if (substring.All(c => c == substring[0]))
                {


                    // Remove the last character (the one that caused the repetition)
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter correct DISE Code')</script>", false);
                    return;

                }
            }
            //for (int i =1; i < n; i++)
            //{
            //    string searchItem = i.ToString();
            //    int count = new Regex(searchItem, RegexOptions.Compiled | RegexOptions.IgnoreCase).Matches(str).Count;
            //    if (ddlState.SelectedValue == "10")
            //    {
            //        if (count >8)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter correct DISE Code')</script>", false);
            //            return;
            //        }
            //    }
            //    else
            //    {


            //        if (count > 5)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter correct DISE Code')</script>", false);
            //            return;
            //        }
            //    }
            //}




            if (ddlschoolLevel.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select School Level')</script>", false);

                return;
            }
            if (ddlManagement.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select School Management')</script>", false);

                return;
            }
        }

        if (ddlSchool.SelectedValue == "99")
        {
            //string strQry1 = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool inner join mst5Village on mst5Village.VillageCode=mstSchool.VillageCode where Govt_DiseCode='" + txtDiseCode.Text.Trim() + "'  and   mstSchool.VillageCode ='" + ddlVillage.SelectedValue + "'  and Status=5 ";


            //DataTable dtDid = objMain.LoadData(strQry1);
            DataTable dtDid = ALLQueryinPage(txtDiseCode.Text.Trim(), ddlVillage.SelectedValue, "", "", "", "7");
            if (dtDid.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode already exists')</script>", false);

                return;
            }

            //string strQry2 = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where Govt_DiseCode='" + txtDiseCode.Text.Trim() + "' and   mstSchool.VillageCode ='" + ddlVillage.SelectedValue + "'   and Status=5  ";


            //DataTable dtDid2 = objMain.LoadData(strQry2);

            DataTable dtDid2 = ALLQueryinPage(txtDiseCode.Text.Trim(), ddlVillage.SelectedValue, "", "", "", "10");
            if (dtDid2.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode already exists')</script>", false);

                return;
            }
        }
        if (ddlState.SelectedValue == "23")
        {
            Div9.Visible = true;
        }
        else
        {
            Div9.Visible = false;
        }

        Session["UnquieId"] = "";

        txtChildName.Text = "";
        txtFatherName.Text = "";
        txtHHNo.Text = "";
        txtSrno.Text = "";
        txtSamgra.Text = "";
        txtSurveyVillage.Text = "";

        ddlEduationStatus.SelectedIndex = 0;

        dllClass.SelectedIndex = 0;

        ddlScat.SelectedIndex = 0;
        ddlRemarks.SelectedIndex = 0;
        txtBirth.Text = "";
        txtDobDate.Text = "";

        txtHHNo.Focus();
        string strQry = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where SchoolCode='" + ddlSchool.SelectedValue + "'   ";


        DataTable dtMangment = objMain.LoadData(strQry);

        if (dtMangment.Rows.Count > 0)
        {
            Session["ManagementType"] = dtMangment.Rows[0]["ManagementType"].ToString();
            Session["SchoolLevel"] = dtMangment.Rows[0]["SchoolLevel"].ToString();
            Session["WorkingStatus"] = dtMangment.Rows[0]["WorkingStatus"].ToString();
            Session["SchoolCodeID"] = dtMangment.Rows[0]["SchoolCodeID"].ToString();

        }
        else
        {
            Session["ManagementType"] = ddlManagement.SelectedValue;
            Session["SchoolLevel"] = ddlschoolLevel.SelectedValue;
            Session["SchoolCodeID"] = "0";
        }
        Session["mYear"] = ddlYear.SelectedValue;
        Session["Schoolid"] = ddlSchool.SelectedValue;
        if (ddlSchool.SelectedValue == "99")
        { }
        else
        {
            if (Convert.ToInt32(dtMangment.Rows[0]["SchoolLevel"]) == 12)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Entry will not be allowed in Madarsha without FLN school type')</script>", false);

                return;

            }
        }
        MpexdrDistrict.Show();

        if (Convert.ToString(Session["ManagementType"]) == "2" && (Convert.ToString(Session["SchoolLevel"]) == "1" || Convert.ToString(Session["SchoolLevel"]) == "2" || Convert.ToString(Session["SchoolLevel"]) == "3"))
        {
            FillClassWorking();
        }
        else
        {
            FillClass();
        }
    }


    public bool CheckAllphanumeric(string txtHhno)
    {


        System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("^(?=.*[0-9]+.*)");
        return !objAlphaNumericPattern.IsMatch(txtHhno);
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
    private Boolean Validation()
    {
        try
        {



            if (Session["UnquieId"].ToString().Length > 6)
            {
                string strQry2 = " Select [Serial] FROM tblEnrolment where [Serial]='" + txtSrno.Text.Trim().ToString() + "' and  SchoolCode ='" + ddlSchool.SelectedValue + "' and UniqueChildCode<>'" + Session["UnquieId"].ToString() + "'  and DeleteFlag<>2";
                DataTable dtSer = objMain.LoadData(strQry2);

                if (dtSer.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);

                    return false;
                }
            }
            else
            {

                string strQrychk = " Select [EntryAllowed] FROM mstEnrollmentEntryvalidation where [SchoolLevel]='" + Convert.ToString(Session["SchoolLevel"]) + "' and  [Mangment] ='" + Convert.ToString(Session["ManagementType"]) + "'";
                DataTable dtCheck = objMain.LoadData(strQrychk);
                if (Convert.ToInt32(dtCheck.Rows[0]["EntryAllowed"]) > 0)
                {
                    string FstrQry2 = " select sum(Icount) Icount from (Select count(*) as Icount FROM tblEnrolment where   SchoolCode ='" + ddlSchool.SelectedValue + "' and IsComplete=1  and DeleteFlag<>2 union Select count(*) as Icount FROM tblEnrolment where   SchoolCode ='" + ddlSchool.SelectedValue + "' and IsComplete=1 and IsDoBoFlag=11 and DeleteFlag=2) as ff";
                    DataTable dtFSer = objMain.LoadData(FstrQry2);
                    if (dtFSer.Rows.Count > 0)
                    {
                        int TotalEn = Convert.ToInt32(dtFSer.Rows[0]["Icount"]);
                        int TotalVal = Convert.ToInt32(dtCheck.Rows[0]["EntryAllowed"]);
                        if (TotalEn > TotalVal)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enrolment cannot entry more then " + TotalVal + "' )</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                    }

                }

                //string strQry = " Select [DISECode] FROM mstschool where [DISECode]='" + txtDiseCode.Text.ToString() + "' and  Createdate ='2020-06-01'";
                DataTable dt = ALLQueryinPage(txtDiseCode.Text.ToString(), "", "", "", "", "1");

                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Dise Code No already exists in Database')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
                string strQry2 = " Select [Serial] FROM tblEnrolment where [Serial]='" + txtSrno.Text.Trim().ToString() + "' and  SchoolCode ='" + ddlSchool.SelectedValue + "' and UniqueChildCode<>'" + Session["UnquieId"].ToString() + "'  and DeleteFlag<>2";
                DataTable dtSer = objMain.LoadData(strQry2);

                if (dtSer.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);

                    return false;
                }
            }


            if (txtChildName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Child name')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }

            if (txtFatherName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Father name')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (txtmotherName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Mother name')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlFC.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select TB/FC')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }

            if (Convert.ToInt32(ddlFC.SelectedValue) == 2 || Convert.ToInt32(ddlFC.SelectedValue) == 3)
            {
                if (ddlTbName.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select TB Name')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
            if (dllClass.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Class')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (txtSrno.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Serial No')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            bool Alf = CheckAllphanumeric(txtSrno.Text);
            if (Alf == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter at least one number in SR')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlScat.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select SocialCategory')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlGender.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Gender')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlState.SelectedValue == "23")
            {
                if (txtSamgra.Text.Trim() == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter sangram ID')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
            if (ddlState.SelectedValue == "23")
            {
                if (txtSamgra.Text.Trim().Length < 9)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('sangram ID should be  9 digits')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }

                string strQry4 = " Select [Serial] FROM tblEnrolment where [SamgraID]='" + txtSamgra.Text.Trim().ToString() + "' and  session ='2024' and UniqueChildCode<>'" + Session["UnquieId"].ToString() + "'";
                DataTable dtSerS = objMain.LoadData(strQry4);
                if (dtSerS.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' This sangram ID already exists in Database ')</script>", false);
                    MpexdrDistrict.Show();
                    return false;

                }

            }


            if (txtBirth.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Admission Date')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (txtHHNo.Text != "")
            {
                bool Alf1 = CheckAllphanumeric(txtSrno.Text);
                if (Alf1 == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter at least one number in HH No')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
            if (txtDobDate.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter DOB')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlEduationStatus.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Enrollment Category')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (ddlEduationStatus.SelectedIndex >= 0)
            {
                if (Convert.ToInt32(ddlEduationStatus.SelectedValue) == 1)
                {
                    if (Convert.ToString(Session["SchoolLevel"]) == "1" || Convert.ToString(Session["SchoolLevel"]) == "2" || Convert.ToString(Session["SchoolLevel"]) == "3" || Convert.ToString(Session["SchoolLevel"]) == "4" || Convert.ToString(Session["SchoolLevel"]) == "7")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Enrollment Category.This category can be selected for school type PS/UPS/Secondary/Sr Secondary.')</script>", false);
                        MpexdrDistrict.Show();
                        return false;
                    }
                }

                if (Convert.ToInt32(ddlEduationStatus.SelectedValue) == 2)
                {
                    if (Convert.ToString(Session["SchoolLevel"]) == "10" || Convert.ToString(Session["SchoolLevel"]) == "2" || Convert.ToString(Session["SchoolLevel"]) == "3")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Enrollment Category.This category can be selected for school type KGBV/UPS/Secondary .')</script>", false);
                        MpexdrDistrict.Show();
                        return false;
                    }
                }
                if (Convert.ToInt32(ddlEduationStatus.SelectedValue) == 3 || Convert.ToInt32(ddlEduationStatus.SelectedValue) == 4)
                {
                    if (Convert.ToString(Session["SchoolLevel"]) == "1" || Convert.ToString(Session["SchoolLevel"]) == "2")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Enrollment Category.This category can be selected for school type PS/UPS .')</script>", false);
                        MpexdrDistrict.Show();
                        return false;
                    }
                }
                if (Convert.ToInt32(ddlEduationStatus.SelectedValue) == 6)
                {
                    if (Convert.ToString(Session["SchoolLevel"]) == "6")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Enrollment Category.This category can be selected for school type Madrasa.')</script>", false);
                        MpexdrDistrict.Show();
                        return false;
                    }
                }
            }

            DateTime AdmissionDate = Convert.ToDateTime(txtBirth.Text);
            Int32 fDate = ((AdmissionDate.Year) * 10000 + (AdmissionDate.Month) * 100 + (AdmissionDate.Day));

            Int32 cFyear = Convert.ToInt32(ddlYear.SelectedValue);

            Int32 cYear = ((cFyear) * 10000 + (04) * 100 + (01));

            DateTime DOB;
            DateTime AsDob;
            Int32 Age = 0;
            DateTime DobDateQ1 = Convert.ToDateTime(txtDobDate.Text);

            string DateSarveyDate = txtBirth.Text;
            string[] b = DateSarveyDate.Split('/');

            string DateB = txtDobDate.Text;
            string[] a = txtDobDate.Text.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];


            string strQry = "select dbo.udfDateDiffinYrMonDay('" + DobDateQ1.ToString("yyyy-MM-dd") + "','" + AdmissionDate.ToString("yyyy-MM-dd") + "') as age ";
            DataTable dtDate = objMain.LoadData(strQry);
            if (dtDate.Rows.Count > 0)
            {
                Age = Convert.ToInt32(dtDate.Rows[0]["age"]);
            }

            //  Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
            DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) + Age;
            string dyear = iyear.ToString();
            DateTime kk = Convert.ToDateTime("2022-04-01");
            if (Convert.ToDateTime(txtBirth.Text.ToString()) > Convert.ToDateTime(kk))
            {
                if (txtmotherName.Text.Trim() == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select mother Name')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }

            if (Convert.ToDateTime(txtBirth.Text.ToString()) <= Convert.ToDateTime(txtDobDate.Text))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of admission is subsequent to DOB')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            string strQry5 = "select * from mstLookup where LookupFlag='AV' and Description='" + ddlDistrict.SelectedValue + "' ";
            DataTable dtAge = objMain.LoadData(strQry5);
            int FAge = 0;
            int ToAge = 0;
            if (dtAge.Rows.Count > 0)
            {
                FAge = Convert.ToInt32(dtAge.Rows[0]["LookupCode"]);

                ToAge = Convert.ToInt32(dtAge.Rows[0]["SeqNo"]);
            }
            if (Age < FAge)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between " + FAge + " and  " + ToAge + "  years')</script>", false);
                MpexdrDistrict.Show();
                return false;

            }
            if (Age > ToAge)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between  " + FAge + " and  " + ToAge + "  years')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }


            //if ( Convert.ToString(ddlState.SelectedValue) == "9" || Convert.ToString(ddlState.SelectedValue) == "23")
            //{
            //    if (Age < 4)
            //    {

            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 4 and 14 years')</script>", false);
            //        MpexdrDistrict.Show();
            //        return false;

            //    }
            //    if (Age > 14)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 4 and 14 years')</script>", false);
            //        MpexdrDistrict.Show();
            //        return false;
            //    }
            //}

            //if (Convert.ToString(ddlState.SelectedValue) == "8")
            //{
            //    if (Age < 4)
            //    {

            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 4 and 18 years')</script>", false);
            //        MpexdrDistrict.Show();
            //        return false;

            //    }
            //    if (Age > 18)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 4 and 18 years')</script>", false);
            //        MpexdrDistrict.Show();
            //        return false;
            //    }
            //}

            //if (Convert.ToString(ddlState.SelectedValue) == "10")
            //{
            //    if (Age < 7)
            //    {

            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 7 and 14 years')</script>", false);
            //        MpexdrDistrict.Show();
            //        return false;

            //    }
            //    if (Age > 18)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 7 and 14 years')</script>", false);
            //        MpexdrDistrict.Show();
            //        return false;
            //    }
            //}

            //if (Convert.ToInt32(ddlYear.SelectedValue) > Convert.ToInt32(AdmissionDate.Year))
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure enrollment date should be in current year')</script>", false);
            //    MpexdrDistrict.Show();
            //    //dDOB.Style.BackColor = Color.Red;
            //    return false;
            //}



            //string strQr1y = " Select mstClassValdation.[Operator], mstClassValdation.[Class], mstLookup.SeqNo AS SeqNoCode FROM mstClassValdation, mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " and  [Age]=" + Age + " ";
            DataTable dtNew = ALLQueryinPage(dllClass.SelectedValue, Age.ToString(), "", "", "", "2");
            //if (Session["Schoolid"].ToString() == "99" || Session["SchoolCodeID"].ToString() == "0")
            //{
            //}
            //else
            //{

            if (Convert.ToInt32(dllClass.SelectedValue) <= 5)
            {

                if (Convert.ToInt32(dllClass.SelectedValue) == 3 || Convert.ToInt32(dllClass.SelectedValue) == 5)
                {
                    //if (Age > 2)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 1 and 2 years')</script>", false);
                    //    MpexdrDistrict.Show();
                    //    return false;
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid class')</script>", false);

                    MpexdrDistrict.Show();
                    //dDOB.Style.BackColor = Color.Red;
                    return false;
                }

            }
            else
            {
                if (dtNew.Rows.Count > 0)
                {
                    Int32 Iclass = Convert.ToInt32(dtNew.Rows[0]["Class"].ToString());
                    Int32 SeqNoCode = Convert.ToInt32(dtNew.Rows[0]["SeqNoCode"].ToString());
                    string Op = dtNew.Rows[0]["Operator"].ToString();
                    if (Convert.ToInt32(Iclass) < SeqNoCode)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can  select max " + Iclass + " class')</script>", false);
                        MpexdrDistrict.Show();
                        return false;
                    }
                }
            }

            if (Session["SchoolLevel"].ToString() == "10")
            {
                if (Convert.ToInt32(ddlGender.SelectedValue) == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('You can add only female child in KGBV School')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }


            if (Convert.ToInt32(dllClass.SelectedValue) <= 5)
            {
            }
            else
            {

                //string strQr1yC = " Select  mstLookup.SeqNo AS SeqNoCode FROM mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " ";
                //DataTable dtNewC = objMain.LoadData(strQr1yC);
                DataTable dtNewC = ALLQueryinPage(dllClass.SelectedValue, "", "", "", "", "3");
                Int32 MainClass = Convert.ToInt32(dtNewC.Rows[0]["SeqNoCode"].ToString());
                if (Session["SchoolLevel"].ToString() == "10")
                {
                    //string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                    //DataTable dtNew1 = objMain.LoadData(strQr1y1);
                    DataTable dtNew1 = ALLQueryinPage(Session["SchoolLevel"].ToString(), "", "", "", "", "4");
                    if (MainClass < 6)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);
                        MpexdrDistrict.Show();
                        return false;
                    }
                    else
                    {
                        Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                        if (MainClass > MaxClass)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 6 to 12 School')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                    }
                }
                else
                {
                    //string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                    //DataTable dtNew1 = objMain.LoadData(strQr1y1);
                    DataTable dtNew1 = ALLQueryinPage(Session["SchoolLevel"].ToString(), "", "", "", "", "4");
                    if (dtNew1.Rows.Count > 0)
                    {
                        Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                        if (MainClass > MaxClass)
                        {
                            if (Session["SchoolLevel"].ToString() == "1")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 5')</script>", false);
                                MpexdrDistrict.Show();
                                return false;
                            }
                            else if (Session["SchoolLevel"].ToString() == "2" || Session["SchoolLevel"].ToString() == "7")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 8')</script>", false);
                                MpexdrDistrict.Show();
                                return false;
                            }
                            else if (Session["SchoolLevel"].ToString() == "3")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 10')</script>", false);

                                MpexdrDistrict.Show();
                                return false;
                            }
                            else if (Session["SchoolLevel"].ToString() == "7")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 8')</script>", false);

                                MpexdrDistrict.Show();
                                return false;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Class 1 to 12 ')</script>", false);
                                MpexdrDistrict.Show();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid School')</script>", false);
                        MpexdrDistrict.Show();

                        //dDOB.Style.BackColor = Color.Red;
                        return false;
                    }
                }
            }
            //}
            DateTime date1 = Convert.ToDateTime(txtDobDate.Text);
            DateTime date2 = Convert.ToDateTime(txtBirth.Text);
            // int daysDiff = ((TimeSpan)(date2 - date1)).Days;
            TimeSpan timeSpan = date2 - date1;

            decimal finalResult = 0;
            finalResult = Convert.ToDecimal(timeSpan.TotalDays / 365);
            if (finalResult < 3)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of Birth and Date of Enrollment difference of 3 years')</script>", false);
                MpexdrDistrict.Show();
                //dDOB.Style.BackColor = Color.Red;
                return false;
            }


            return true;

        }
        catch
        {
            // MessageBox.Show(ex.Message, "EG", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }
    public bool InterventionSql_Injection(string RVal)
    {
        SqlInjection objAudit = new SqlInjection();
        bool injection = false;


        injection = objAudit.CheckInputBool(RVal);

        return injection;

    }
    protected void btSave_Click(object sender, EventArgs e)
    {
        string RVal = SetTextBoxFocusSelect(this.Page);

        if (!InterventionSql_Injection(RVal))
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Spurious input detected. Data rejected')</script>", false);
            MpexdrDistrict.Show();
            return;
        }
        if (!Validation())
            return;
        SaveData();

    }
    public void FillClass()
    {

        conditions = "LookupFlag ='ECL'  and lookupcode not in(1,2,3,4,5)";

        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", dllClass, "Description", "LookupCode", "Select");



    }
    public void FillSocialCat()
    {
        conditions = "";
        conditions = "LookupFlag ='CAT' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlScat, "Description", "LookupCode", "Select");



    }

    public void FillENrollment()
    {
        conditions = "";
        conditions = "LookupFlag ='ES' and Active=1 and LookupCode in(1,2,3) ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEnroll, "Description", "LookupCode", "Select");



    }

    public void FillEduStauts()
    {
        conditions = "";
        conditions = "LookupFlag ='EC' and Active=1 and LookupCode not in(5,99)";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEduationStatus, "Description", "LookupCode", "Select");



    }
    public void UserLevelFilter()
    {

        string strQry = "";
        //string Cond = "Module='Enroll'";
        //strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        //DataTable dtRole = objMain.LoadData(strQry);


        DataTable dtRole = ALLQueryinPage("Enroll", Session["user_level"].ToString(), "", "", "", "8");



        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            edit_status = Convert.ToBoolean(dtRole.Rows[0]["edit_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
            ViewState["edit_status"] = edit_status;
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


        }
        else
        {


        }
        if (vVerify == true)
        {



        }
        if (vVerify == true || vADD == true)
        {


        }
        else
        {

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
        LockingEdit();
    }

    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {
            gvnroll.Columns[0].Visible = true;
            gvnroll.Columns[1].Visible = true;


            string strQry;
            // strQry = "Select * from mstModuleLocking  where [FromName]='Enroll' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";


            DataTable dtModel = ALLQueryinPage("Enroll", ddlDistrict.SelectedValue, ddlYear.SelectedItem.Text, "", "", "9");


            if (dtModel.Rows.Count > 0)
            {


                DateTime date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                DateTime date2 = DateTime.Now.Date;





                if (date1 < date2)
                {


                    ImageButton2.Visible = false;
                    gvnroll.Columns[0].Visible = false;
                    gvnroll.Columns[1].Visible = false;
                    gvnroll.Columns[2].Visible = false;

                }
                else
                {
                    ImageButton2.Visible = true;
                }

            }

        }


    }
    public void LockingEdit()
    {
        if (ddlYear.SelectedIndex > 0)
        {


            string strQry;

            //    strQry = "Select * from mstModuleLocking  where [FromName]='EnrollmentEditDelete' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');

            DateTime date1;
            DateTime date2;
            // DataTable dtModel = objMain.LoadData(strQry);
            DataTable dtModel = ALLQueryinPage("EnrollmentEditDelete", ddlDistrict.SelectedValue, ddlYear.SelectedItem.Text, "", "", "9");

            if (dtModel.Rows.Count > 0)
            {
                date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                Session["EDITLOCK"] = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());


                Int32 Ik = Convert.ToInt32(dtModel.Rows[0]["LockMonth"].ToString());
                if (DateTime.Today.Month == 1 || DateTime.Today.Month == 3)
                {
                    date1 = new DateTime(Convert.ToInt32(Year1[1]), DateTime.Today.Month, 30, 0, 0, 0);
                    date2 = new DateTime(Convert.ToInt32(Year1[1]), Ik, 30, 0, 0, 0);
                }
                if (DateTime.Today.Month == 2)
                {
                    date1 = new DateTime(Convert.ToInt32(Year1[1]), DateTime.Today.Month, 28, 0, 0, 0);
                    date2 = new DateTime(Convert.ToInt32(Year1[1]), Ik, 29, 0, 0, 0);
                }
                else
                {
                    date1 = new DateTime(Convert.ToInt32(Year1[0]), DateTime.Today.Month, 30, 0, 0, 0);
                    date2 = new DateTime(Convert.ToInt32(Year1[0]), Ik, 30, 0, 0, 0);
                }

                decimal result = DateTime.Compare(date1, date2);
                if (Math.Abs(result) > 0)
                {
                    ViewState["EDITDelete"] = false;
                }
                else
                {
                    ViewState["EDITDelete"] = true;
                }
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
        //DateTime GivenDate = DateTime.Now;
        //int GivenYear = GivenDate.Year;
        //int m = GivenDate.Month;

        //DataTable dt = null;
        ////ddlYear.Items.Add("--Select--","0");
        //int y = GivenDate.Year;


        //DateTime GivenDate1 = DateTime.Now;
        //int GivenYear1 = GivenDate1.Year;
        //DataTable dtYear = CreateDataTable();
        //DataRow dr;
        //if (ddlYear.SelectedIndex < 0)
        //{

        //    string mYear1 = GivenYear1.ToString();
        //    for (int j = 0; j < 1; j++)
        //    {
        //        if (m > 3)
        //        {
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
        //            dr["ID"] = y;
        //            dtYear.Rows.Add(dr);
        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
        //            dr["ID"] = y - 1;
        //            dtYear.Rows.Add(dr);
        //            //get last  two digits (eg: 10 from 2010);

        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }
        //        else
        //        {
        //            dr = dtYear.NewRow();
        //            dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
        //            //y = y - 1;
        //            dr["ID"] = y - 1;

        //            dtYear.Rows.Add(dr);


        //            dr = dtYear.NewRow();
        //            dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
        //            dr["ID"] = y - 2;
        //            dtYear.Rows.Add(dr);
        //        }

        //    }

        //}
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





    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
        txtSchooName.Text = "";
        txtDiseCode.Text = "";
        IDschool.Visible = false;
        IDDise.Visible = false;
        Div13.Visible = false;
        Div14.Visible = false;
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        Locking();
        LockingEdit();
        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        Locking();
        LockingEdit();
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }

    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool();

        string strQry = "      select TBCode,TBname from mstTeamBalika mst  with(nolock) left join mst5Village V on V.VillageCode=mst.VillageCode   	or  V.refVillage16=mst.VillageCode	or V.refVillage17=mst.VillageCode	or  V.refVillage18=mst.VillageCode or  V.refVillage19=mst.VillageCode or  V.refVillage20=mst.VillageCode or  V.refVillage21=mst.VillageCode  or  V.refVillage22=mst.VillageCode  or  V.refVillage23=mst.VillageCode or  v.refVillage24=mst.VillageCode or  v.refVillage25=mst.VillageCode where WorkingStatus=1 and V.VillageCode='" + ddlVillage.SelectedValue + "'  ";
        DataTable dtVillageActivtiy = objMain.LoadData(strQry);
        objComman.BindDLLDatatable("mstSchool", dtVillageActivtiy, "TBCode,TBname", conditions, "TBname", "asc", ddlTbName, "TBname", "TBCode", "Select");
    }
    protected void ddlFC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlFC.SelectedValue) == 1)
        {
            pnlTb.Visible = false;
        }
        else
        {
            pnlTb.Visible = true;

        }
        MpexdrDistrict.Show();
    }
    public void FillSchool()
    {
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'  union Select  SchoolCode, Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  union Select top 1 '99' as SchoolCode,'Other School' Name from mstSchool  ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");




        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


    }
    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSchool.SelectedValue == "99")
        {
            IDschool.Visible = true;
            IDDise.Visible = true;

            Div13.Visible = true;
            Div14.Visible = true;

        }
        else
        {
            txtSchooName.Text = "";
            txtDiseCode.Text = "";
            IDschool.Visible = false;
            IDDise.Visible = false;
            Div13.Visible = false;
            Div14.Visible = false;
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

    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblUniqueChildCode = (Label)e.Row.FindControl("lblUniqueChildCode");

            ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            Label lblCreatedate = (Label)e.Row.FindControl("lblCreatedate");

            LinkButton LnkBtnBlock_OnClick = (LinkButton)e.Row.FindControl("lbtn");

            LinkButton ldddbtn = (LinkButton)e.Row.FindControl("ldddbtn");


            ldddbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Unmatach? ')");

            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            if (lblStatus.Text == "2")
            {
                ldddbtn.Visible = true;
            }
            else
            {
                ldddbtn.Visible = false;
            }


            DateTime date1;
            DateTime date2;
            date1 = Convert.ToDateTime(lblCreatedate.Text);
            date2 = Convert.ToDateTime(Session["EDITLOCK"]);

            //LnkBtnBlock_OnClick.Enabled = true;
            //lbtn.Enabled = true;
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136" || Session["user_level"].ToString() == "145")
            {
                LnkBtnBlock_OnClick.Enabled = true;
                lbtn.Enabled = true;
            }
            else
            {
                LnkBtnBlock_OnClick.Enabled = false;
                lbtn.Enabled = false;
            }

            //LnkBtnBlock_OnClick.Enabled = true;
            //lbtn.Enabled = true;
            // Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            //string schoolcode = lblUniqueChildCode.Text;
            //Session["UnquieId"] = lblUniqueChildCode.Text;
            //Session["StateCode"] = ddlState.SelectedValue;
            //Session["DistCode"] = ddlDistrict.SelectedValue;
            //Session["BlockCode"] = ddlBlock.SelectedValue;
            //Session["PhanyCode"] = ddlPanchayat.SelectedValue;
            //Session["VillCode"] = ddlVillage.SelectedValue;
            //Session["Schoolid"] = ddlSchool.SelectedValue;
            //if (ddlSchool.SelectedIndex > 0)
            //{
            //    Session["SchoolName"] = ddlSchool.SelectedItem.Text;
            //}
            //if (ddlPanchayat.SelectedIndex > 0)
            //{
            //    Session["PhanyName"] = ddlPanchayat.SelectedItem.Text;
            //}
            //if (ddlVillage.SelectedIndex > 0)
            //{
            //    Session["Villageame"] = ddlVillage.SelectedItem.Text;
            //}
            //Session["mYear"] = ddlYear.SelectedValue;
            //Session["EnStatus"] = lblStatus.Text;
            //   string   strURL = "frmD2dEnrollment.aspx?CommandArgument=" & CType(e.Row.FindControl("lblpksdMatItemID"), Label).Text & "," & CType(e.Row.FindControl("lblAOrder"), Label).Text & "," & CType(e.Row.FindControl("lblArticleNo"), Label).Text
            //string strURL = "frmAddEnrollmentFrom6From6.aspx";
            //   lbtn.Attributes.Add("onclick", "window.open('" + strURL + "', 'name', 'width=1000,height=500,left=700,top=400,scrollbars=1,resizable=yes');");

            //  Puppop();



        }


    }

    public void Puppop()
    {
        string url = "frmAddEnrollmentFrom6.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=500,left=700,top=400,scrollbars=1,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }
    public void LoadData()
    {
        string strQry = "";
        //if (Program.UserLevel == 1)
        //{
        //  strQry = " Select UniqueChildCode,Serial as ID,StrConv(ChildName,3) as [Child Name] from tblEnrolment  where VillageCode='" + CBVillage.SelectedValue + "' order by ChildName ";
        //}
        //else
        //{
        //    strQry = " Select UniqueCode,ChildCode as ID,ChildName1 as [Child Name] from tblDTD  where tblEnrolment='" + CBVillage.SelectedValue + "' order by ChildName1 ";

        //}
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School')</script>", false);


            this.ddlSchool.Focus();
            return;
        }
        if (ddlState.SelectedValue == "23")
        {
            Div9.Visible = true;
        }
        else
        {
            Div9.Visible = false;
        }
        conditions = "";
        conditions = " mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedIndex > 0)
        {

            conditions = conditions + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";

        }



        if (ddlPanchayat.SelectedIndex > 1)
        {
            conditions = conditions + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }


        conditions = conditions + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";


        if (ddlSchool.SelectedValue != null && ddlSchool.SelectedIndex > 0)
        {
            conditions = conditions + "and tblEnrolment.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "'";
        }


        //strQry += "  SELECT mst2District.DistrictName ,mst3Block.BlockName ,mstPanchayat.PanchayatName ,mst5Village.VillageName ,tblEnrolment.SchoolCode, tblEnrolment.[UniqueChildCode], D2DCode as Uniqueid,case Gender when 1 then 'Male' else 'Female' end as Gender , tblEnrolment.Serial as  Serial,convert(varchar, tblEnrolment.[EnrolmentDate],103) as EnrolmentDate, aged.Description as Class, tblEnrolment.AsOnDate,  tblEnrolment.[HouseNo] as HHNo1, tblEnrolment.[Category], [ChildName] AS ChildName, [FatherName] AS FathersName, tblEnrolment.[Gender], tblEnrolment.[DOBAvailable], convert(varchar, tblEnrolment.[DOB],103) as DOB, tblEnrolment.[AgeAson] as Age, ES.Description AS SocialCategory, mstSchool.Name as School,  EC.Description as EnrolmentCategory, mst5Village.PanchayatCode, mst5Village.BlockCode, mst5Village.DistrictCode, ES1.Description as EduationStatus ,tblEnrolment.SysFlag,tblEnrolment.Status ";
        //strQry += "    FROM tblEnrolment ";
        //strQry += "    INNER JOIN mst5Village ON mst5Village.VillageCode = tblEnrolment.VillageCode ";
        //strQry += "      LEFT JOIN mst3Block ON mst5Village.BlockCode = mst3Block.BlockCode";
        //strQry += " LEFT JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode";
        //strQry += " LEFT JOIN mst2District ON mst5Village.DistrictCode = mst2District.DistrictCode ";

        //strQry += "  LEFT JOIN mstSchool ON tblEnrolment.SchoolCode = mstSchool.SchoolCode  LEFT JOIN mstLookup aged on aged.LookupCode=Class and aged.LookupFlag='ECL'";

        //strQry += "	LEFT JOIN mstLookup ES on ES.LookupCode=Category and ES.LookupFlag='CAT'	  LEFT JOIN mstLookup EC on EC.LookupCode=EnrollCategory and EC.LookupFlag='EC'	      LEFT JOIN mstLookup ES1 on ES1.LookupCode=TYPE and ES1.LookupFlag='ES' ";
        //strQry += "	 where " + conditions + "  and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "' and DeleteFlag=1 order by D2DCode  ";

        //DataTable dt1 = objMain.LoadData(strQry);

        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag", 5),
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadOnlineEnrollment2020]", parm1);

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                string encryptionKey = row["UniqueChildCode"].ToString();

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


        if (dt.Rows.Count > 0)
        {
            gvnroll.DataSource = dt;
            gvnroll.DataBind();
        }
        else
        {
            gvnroll.DataSource = null;
            gvnroll.DataBind();
        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (ddlState.SelectedValue == "6")
        {
            txtSrno.MaxLength = 12;
        }
        else
        {
            txtSrno.MaxLength = 9;
        }
        LoadData();
    }
    protected void btnMain_Click(object sender, EventArgs e)
    {
        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);


            this.ddlVillage.Focus();
            return;
        }
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School')</script>", false);


            this.ddlSchool.Focus();
            return;
        }
        Session["StateCode"] = ddlState.SelectedValue;
        Session["DistCode"] = ddlDistrict.SelectedValue;
        Session["BlockCode"] = ddlBlock.SelectedValue;
        Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        Session["VillCode"] = ddlVillage.SelectedValue;
        Session["Schoolid"] = ddlSchool.SelectedValue;
        Session["SchoolName"] = ddlSchool.SelectedItem.Text;
        Session["PhanyName"] = ddlPanchayat.SelectedItem.Text;
        Session["Villageame"] = ddlVillage.SelectedItem.Text;
        Session["mYear"] = ddlYear.SelectedValue;
        Session["FYear"] = ddlYear.SelectedItem.Text;
        string strQry = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where SchoolCode='" + ddlSchool.SelectedValue + "'   ";


        DataTable dtMangment = objMain.LoadData(strQry);

        if (dtMangment.Rows.Count > 0)
        {
            Session["ManagementType"] = dtMangment.Rows[0]["ManagementType"].ToString();
            Session["SchoolLevel"] = dtMangment.Rows[0]["SchoolLevel"].ToString();
            Session["WorkingStatus"] = dtMangment.Rows[0]["WorkingStatus"].ToString();
            Session["SchoolCodeID"] = dtMangment.Rows[0]["SchoolCodeID"].ToString();

        }

        string url = "frmD2dEnrollment.aspx";

        string s = "window.open('" + url + "', 'popup_window', 'width=1300,height=500,left=700,top=400,scrollbars=1,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }

    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        Label lblStatus = (Label)gvr.FindControl("lblStatus");
        Label lblSchool = (Label)gvr.FindControl("lblSchool");
        Label lblSchoolID = (Label)gvr.FindControl("lblSchoolID");


        Session["UnquieId"] = UniqueChildCode;
        Session["StateCode"] = ddlState.SelectedValue;
        Session["DistCode"] = ddlDistrict.SelectedValue;
        Session["BlockCode"] = ddlBlock.SelectedValue;
        Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        Session["VillCode"] = ddlVillage.SelectedValue;
        Session["Schoolid"] = lblSchoolID.Text;

        Session["SchoolName"] = lblSchool.Text;

        if (ddlPanchayat.SelectedIndex > 0)
        {
            Session["PhanyName"] = ddlPanchayat.SelectedItem.Text;
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            Session["Villageame"] = ddlVillage.SelectedItem.Text;
        }
        Session["mYear"] = ddlYear.SelectedValue;
        Session["EnStatus"] = lblStatus.Text;

        string strQry = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where SchoolCode='" + ddlSchool.SelectedValue + "'   ";


        DataTable dtMangment = objMain.LoadData(strQry);

        if (dtMangment.Rows.Count > 0)
        {
            Session["ManagementType"] = dtMangment.Rows[0]["ManagementType"].ToString();
            Session["SchoolLevel"] = dtMangment.Rows[0]["SchoolLevel"].ToString();
            Session["WorkingStatus"] = dtMangment.Rows[0]["WorkingStatus"].ToString();
            Session["SchoolCodeID"] = dtMangment.Rows[0]["SchoolCodeID"].ToString();

        }

        if (Convert.ToString(Session["ManagementType"]) == "2" && (Convert.ToString(Session["SchoolLevel"]) == "1" || Convert.ToString(Session["SchoolLevel"]) == "2" || Convert.ToString(Session["SchoolLevel"]) == "3"))
        {
            FillClassWorking();
        }
        else
        {
            FillClass();
        }
        FillD2dData();
        MpexdrDistrict.Show();

        // Session["UnquieId"] = UniqueChildCode;
        //string url = "frmAddEnrollmentFrom6.aspx";

        //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=650,left=500,top=500,scrollbars=1,resizable=yes');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


    }
    public void FillD2dData()
    {
        string strQry = " Select [UniqueChildCode],EnTBcode,isnull(ECategory,0) as ECategory,isnull(remark,0)remark,	TBFC,MotherName,VillagenameOther,SamgraID,ChildCode,mstSchool.name,tblEnrolment.[VillageCode],EnrolmentDate as SurvayDate,Class,AsOnDate,[Serial],[HouseNo],[Category],[ChildName] as ChildName,[FatherName] as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],Type as EduationStatus,tblEnrolment.[SchoolCode],[EnrollCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,tblEnrolment.Status FROM (mst5Village INNER JOIN tblEnrolment ON mst5Village.VillageCode = tblEnrolment.VillageCode) left JOIN mstSchool ON tblEnrolment.SchoolCode = mstSchool.SchoolCode where UniqueChildCode='" + Session["UnquieId"].ToString() + "' ";
        DataTable dt = objMain.LoadData(strQry);


        if (dt.Rows.Count > 0)
        {

            //if (dt.Rows[0]["Status"].ToString() == "2")
            //{
            //    btnd2dSave.Enabled = false;
            //    btnD2Delete.Enabled = false;
            //}
            //else
            //{
            //    btnd2dSave.Enabled = true;
            //    btnD2Delete.Enabled = true;
            //}
            ddlFC.SelectedValue = dt.Rows[0]["TBFC"].ToString();
            if (dt.Rows[0]["EnTBcode"].ToString() == "2" || dt.Rows[0]["EnTBcode"].ToString() == "1")
            {
                pnlTb.Visible = true;
                ddlTbName.SelectedValue = dt.Rows[0]["EnTBcode"].ToString();
            }
            else
            {
                pnlTb.Visible = false;
            }

            ddlRemarks.SelectedValue = dt.Rows[0]["remark"].ToString();
            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
            txtmotherName.Text = dt.Rows[0]["MotherName"].ToString();
            txtSrno.Text = dt.Rows[0]["Serial"].ToString();
            txtChildName.Text = dt.Rows[0]["ChildName"].ToString();
            txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();

            txtSamgra.Text = dt.Rows[0]["SamgraID"].ToString();
            txtSurveyVillage.Text = dt.Rows[0]["VillagenameOther"].ToString();
            //villagecode = dt.Rows[0]["VillageCode"].ToString();

            ddlEduationStatus.SelectedValue = dt.Rows[0]["ECategory"].ToString();


            //DTPicker_DOB.Format = DateTimePickerFormat.Custom;
            //DTPicker_DOB.CustomFormat = "dd/MM/yyyy ";

            DateTime DOB = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());

            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtDobDate.Text = "";
            }
            else
            {
                txtDobDate.Text = DOB.ToString("dd/MM/yyyy");
            }




            DateTime SurvayDate = Convert.ToDateTime(dt.Rows[0]["SurvayDate"].ToString());
            if (dt.Rows[0]["DOB"].ToString() == "01/01/1900 00:00:00")
            {
                txtBirth.Text = "";
            }
            else
            {
                txtBirth.Text = SurvayDate.ToString("dd/MM/yyyy");
            }




            ddlScat.SelectedValue = dt.Rows[0]["Category"].ToString();
            //ddlEduationStatus.SelectedValue = Convert.ToInt32(dt.Rows[0]["EnrollCategory"].ToString()).ToString();
            //ddlEnroll.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();


            lblSchool.Text = dt.Rows[0]["name"].ToString();
            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();
            dllClass.SelectedValue = dt.Rows[0]["Class"].ToString();
            //if (dt.Rows[0]["ReasonDO_NE"].ToString() == "0")
            //{
            //    txtReason.Text = "";

            //}
            //else
            //{
            //    txtReason.Text = dt.Rows[0]["ReasonDO_NE"].ToString();
            //}


            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();

        }
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblStatus = (gvr.FindControl("lblStatus") as Label).Text;
        string lblD2dChildCode = (gvr.FindControl("lblD2dChildCode") as Label).Text;

        string strQry = "";
        //strQry = "  SELECT EnrollCode from tblDTD where EnrollCode ='" + UniqueChildCode + "' ";
        //DataTable dt = objMain.LoadData(strQry);
        //if (dt.Rows.Count > 0)
        //{
        //    bt.Attributes.Add("onclick", "javascript:return " + "confirm(' Enrollment link in D2D Please confirm if you want to Deleted?  ')");
        //}
        //if (dt.Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('You can not  Deleted because Enrollment link in D2D');", true);

        //   // bt.Attributes.Add("onclick", "javascript:return " + "confirm(' Enrollment link in D2D Please confirm if you want to Deleted?  ')");

        //    //int res1 = objMain.DeleteEnrollMentData(UniqueChildCode, "D");

        //    //if (res1 > 0)
        //    //{
        //    //    LoadData();
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        //    //}
        //}
        //else
        //{
        if (lblStatus == "2")
        {

            strQry = "  SELECT D2dChildCode from tblEnrolment with(nolock) where D2dChildCode ='" + lblD2dChildCode + "' and DeleteFlag<>2 ";
            DataTable dted = objMain.LoadData(strQry);
            if (dted.Rows.Count > 1)
            {
                strQry = "  SELECT EnrollCode from tblDTD with(nolock) where EnrollCode ='" + UniqueChildCode + "' ";
                DataTable dt = objMain.LoadData(strQry);
                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please delete other Duplicate Record ');", true);

                }
                else
                {
                    int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

                    if (res1 > 0)
                    {
                        LoadData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                    }

                }
            }
            else
            {
                int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

                if (res1 > 0)
                {
                    LoadData();
                    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                }

            }
        }
        else
        {

            int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

            if (res1 > 0)
            {
                LoadData();
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

            }
        }
        //}

    }


    protected void btn_Un_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblStatus = (gvr.FindControl("lblStatus") as Label).Text;
        string lblD2dChildCode = (gvr.FindControl("lblD2dChildCode") as Label).Text;

        string strQry = "";
        int res1 = UnmatcgEnrollMentData(UniqueChildCode);



        if (res1 > 0)
        {
            LoadData();
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }

        //}

    }

    public DataTable ALLQueryinPage(string Filter, string Filter1, string Filter2, string Filter3, string Filter4, string Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Filter ", Filter),
              new SqlParameter("@Filter1 ", Filter1),
                new SqlParameter("@Filter2 ", Filter2),
                  new SqlParameter("@Filter3 ", Filter3),
                  new SqlParameter("@Filter4 ", Filter4),
                  new SqlParameter("@Flag ", Flag),

        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadAlltypeofPageQuery", cmdParameters);
        return dt;

    }
    public int UnmatcgEnrollMentData(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Uni ", UniqueChildCode),

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "insertUpdateUnmatch", cmdParameters);
    }


    public int DeleteEnrollMentData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag),
            new SqlParameter("@UserName",  Session["username"].ToString() )
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteEnrollMentDataModifyForm6", cmdParameters);
    }


    public int SaveDataEnrolmentHistory(string UniqueChildCode, string lookupCode, string PVal, string CVal, string UpdatedBy, string UpdatedOn)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@UniqueChildCode", UniqueChildCode),
                new SqlParameter("@lookupCode", lookupCode),
                new SqlParameter("@PVal", PVal),
                new SqlParameter("@CVal", CVal),
                new SqlParameter("@UpdatedBy", UpdatedBy),
                new SqlParameter("@UpdatedOn", UpdatedOn),
            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateENrolmentHistory", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }


}