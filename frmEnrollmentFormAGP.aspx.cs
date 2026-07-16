using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmEnrollmentFormAGP : System.Web.UI.Page
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
            return;
        }
        if (!Validation())
            return;
        SaveData();

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
        string G = "";
        string B = "";
        foreach (ListItem item in CBL_bookformat.Items)
        {
            if (item.Selected)
            {

                G += "" + item.Value + "" + ",";

            }
        }
        if (G.Length > 0) { G = G.Substring(0, G.LastIndexOf(",")); }


        foreach (ListItem item in CBL_bookformatNew.Items)
        {
            if (item.Selected)
            {

                B += "" + item.Value + "" + ",";

            }
        }
        if (B.Length > 0) { B = B.Substring(0, B.LastIndexOf(",")); }


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
        if (Session["UnquieId"].ToString().Length > 6)
        {


            int result = Update_Enrolment_AGP(Convert.ToString(ddlMapping.SelectedValue), Convert.ToString(txtUiniqCOde.Text), Convert.ToString(ddlNew.SelectedValue), "G", "B", Convert.ToString(txtSamgra.Text), Convert.ToString(txtSurveyVillage.Text), Convert.ToString(ddlEduationStatus.SelectedValue), Convert.ToString(ddlScat.SelectedValue), Convert.ToString(dllClass.SelectedValue), Convert.ToString(strSerial), Convert.ToString(ChildName), Convert.ToString(FathersName), Convert.ToString(Gender), Convert.ToString(DateAdminision), Convert.ToString(DoAv), Convert.ToString(ChildDOB), Convert.ToString(Age), Convert.ToString(AsDob.ToString("yyyy-MM-dd")), Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd")), Convert.ToString(Session["username"].ToString()), Convert.ToString(txtHHNo.Text.Trim()), Convert.ToString(Session["UnquieId"].ToString()));


            if (result > 0)
            {
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

                txt_pbnameNew.Text = "";
                txt_pbname.Text = "";
                GroupA.Visible = false;
                GroupB.Visible = false;
                ddlNew.SelectedIndex = 0;
                LblDtdt.Visible = false;
                txtUiniqCOde.Visible = false;
                ddlMapping.SelectedIndex = 0;
                dllClass.SelectedIndex = 0;
                txtUiniqCOde.Text = "";
                ddlScat.SelectedIndex = 0;
                txtBirth.Text = "";
                txtDobDate.Text = "";
                Session["UnquieId"] = "";
                //  Response.Write("<script>window.close();</" + "script>");
                LoadData();
                //Response.End();
                // Response.Redirect("~/frmEnrollmentForm6.aspx?ID=1");
                //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ClosePage", "window.close();", true);

                MpexdrDistrict.Show();
            }
        }
        else
        {
            if (ddlSchool.SelectedValue == "99")
            {

                string Othersname = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtSchooName.Text.Trim());

                string UCOde = objComman.Generate_RandomString(8);
                int InsertScool = SaveDataSchool(ddlVillage.SelectedValue, Convert.ToString(UCOde), "0", Othersname, "5", DateTime.Now.ToString("yyyy-MM-dd"), Session["username"].ToString(), "0", txtDiseCode.Text, ddlschoolLevel.SelectedValue, txtDiseCode.Text, ddlManagement.SelectedValue);

                // int InsertScool = mstSchool_Add(Convert.ToString(ddlVillage.SelectedValue), Convert.ToString(UCOde), "0", Convert.ToString(Othersname), Convert.ToString(Othersname), Convert.ToString(Othersname), "5", Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd")), Convert.ToString(Session["username"].ToString()), "0", "0", "0", "0", Convert.ToString(txtDiseCode.Text));
                FillSchool();
                ddlSchool.SelectedValue = UCOde;
                IDschool.Visible = false;
                IDDise.Visible = false;
                Div13.Visible = false;
                Div14.Visible = false;
                txtSchooName.Text = "";
                txtDiseCode.Text = "";
            }


            Int32 ssNo = 0;
            string strQry = " select isnull(Max(MaxID),0)+1 Serial from [tblEnrolment_AGP]  where villagecode= '" + ddlVillage.SelectedValue + "' ";
            DataTable dt = objMain.LoadData(strQry);
            if (dt.Rows.Count > 0)
            {
                ssNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
            }

            string UNICOde = objMain.Generate_RandomString(8);

            string D2D = "";
            if (Convert.ToString(Session["D2D"]).Length > 3)
            {
                D2D = Convert.ToString(Session["D2D"]);
            }



            int InsertTSEnroll = Insert_Enrolment_AGP(Convert.ToString(UNICOde), "0", Convert.ToString(ddlVillage.SelectedValue), Convert.ToString(txtSrno.Text), Convert.ToString(ddlScat.SelectedValue), Convert.ToString(dllClass.SelectedValue), Convert.ToString(Session["mYear"].ToString()), Convert.ToString(ChildName), Convert.ToString(FathersName), Convert.ToString(Gender), Convert.ToString(ddlSchool.SelectedValue), Convert.ToDateTime(Adminision).ToString("yyyy-MM-dd"), Convert.ToString(DoAv), Convert.ToString(ChildDOB), Convert.ToString(Age), Convert.ToString(ddlEduationStatus.SelectedValue), Convert.ToString(ddlEduationStatus.SelectedValue), "1", Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd")), Convert.ToString(Session["username"].ToString()), Convert.ToString(txtHHNo.Text), "1", Convert.ToString(txtSurveyVillage.Text), Convert.ToString(txtSamgra.Text), Convert.ToString(ddlMapping.SelectedValue), Convert.ToString(txtUiniqCOde.Text), Convert.ToString(ssNo), Convert.ToString(G), Convert.ToString(B), Convert.ToString(ddlNew.SelectedValue), D2D);




            if (InsertTSEnroll > 0)
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
                txt_pbnameNew.Text = "";
                txt_pbname.Text = "";
                GroupA.Visible = false;
                GroupB.Visible = false;
                ddlNew.SelectedIndex = 0;
                LblDtdt.Visible = false;
                txtUiniqCOde.Visible = false;
                ddlMapping.SelectedIndex = 0;
                dllClass.SelectedIndex = 0;
                txtUiniqCOde.Text = "";
                ddlScat.SelectedIndex = 0;
                txtBirth.Text = "";
                txtDobDate.Text = "";
                //this.Close();
                LoadData();
                MpexdrDistrict.Show();
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

        }
        return Icount;
    }

    public int Update_Enrolment_AGP(string mappingType, string D2dChildCode, string EnrolmentAttempt, string SubjectA, string SubjectB, string SamgraID, string VillagenameOther, string EnrollCategory, string Category, string Class, string Serial, string ChildName, string FatherName, string Gender, string EnrolmentDate, string DOBAvailable, string DOB, string AgeAson, string AsOnDate, string ModifyDate, string ModifyBy, string HouseNo, string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@mappingType", mappingType),
                    new SqlParameter("@campNo", D2dChildCode),
                    new SqlParameter("@EnrolmentAttempt", EnrolmentAttempt),
                    new SqlParameter("@SubjectA", SubjectA),
                    new SqlParameter("@SubjectB", SubjectB),
                    new SqlParameter("@SamgraID", SamgraID),
                    new SqlParameter("@VillagenameOther", VillagenameOther),
                    new SqlParameter("@EnrollCategory", EnrollCategory),
                    new SqlParameter("@Category", Category),
                    new SqlParameter("@Class", Class),
                    new SqlParameter("@Serial", Serial),
                    new SqlParameter("@ChildName", ChildName),
                    new SqlParameter("@FatherName", FatherName),
                    new SqlParameter("@Gender", Gender),
                    new SqlParameter("@EnrolmentDate", EnrolmentDate),
                    new SqlParameter("@DOBAvailable", DOBAvailable),
                    new SqlParameter("@DOB", DOB),
                    new SqlParameter("@AgeAson", AgeAson),
                    new SqlParameter("@AsOnDate", AsOnDate),
                    new SqlParameter("@ModifyDate", ModifyDate),
                    new SqlParameter("@ModifyBy", ModifyBy),
                    new SqlParameter("@HouseNo", HouseNo),
                    new SqlParameter("@UniqueChildCode", UniqueChildCode)


    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp_lEnrolment_AGP_Update", cmdParameters);

    }
    public int mstSchool_Add(string VillageCode, string SchoolCode, string SchoolCodeID, string Name, string Name1, string Name2, string Status, string Createdate, string CreateBy, string sysFlag, string DISECode, string DISECode1, string DISECode2, string Govt_DiseCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
                    new SqlParameter("@VillageCode", VillageCode),
                    new SqlParameter("@SchoolCode", SchoolCode),
                    new SqlParameter("@SchoolCodeID", SchoolCodeID),
                    new SqlParameter("@Name", Name),
                    new SqlParameter("@Name1", Name1),
                    new SqlParameter("@Name2", Name2),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@Createdate", Createdate),
                    new SqlParameter("@CreateBy", CreateBy),
                    new SqlParameter("@sysFlag", sysFlag),
                    new SqlParameter("@DISECode", DISECode),
                    new SqlParameter("@DISECode1", DISECode1),
                    new SqlParameter("@DISECode2", DISECode2),
                    new SqlParameter("@Govt_DiseCode", Govt_DiseCode)
   };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp_Enroll_mstSchool_Add", cmdParameters);

    }

    public int Insert_Enrolment_AGP(string UniqueChildCode, string ChildCode, string VillageCode, string Serial, string Category, string Class, string Session, string ChildName, string FatherName, string Gender, string SchoolCode, string EnrolmentDate, string DOBAvailable, string DOB, string AgeAson, string Type, string EnrollCategory, string Status, string Createdate, string CreateBy, string HouseNo, string DeleteFlag, string VillagenameOther, string SamgraID, string mappingType, string D2dChildCode, string MaxID, string SubjectA, string SubjectB, string EnrolmentAttempt, string D2D)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
         {
                    new SqlParameter("@UniqueChildCode", UniqueChildCode),
                    new SqlParameter("@ChildCode", ChildCode),
                    new SqlParameter("@VillageCode", VillageCode),
                    new SqlParameter("@Serial", Serial),
                    new SqlParameter("@Category", Category),
                    new SqlParameter("@Class", Class),
                    new SqlParameter("@Session", Session),
                    new SqlParameter("@ChildName", ChildName),
                    new SqlParameter("@FatherName", FatherName),
                    new SqlParameter("@Gender", Gender),
                    new SqlParameter("@SchoolCode", SchoolCode),
                    new SqlParameter("@EnrolmentDate", EnrolmentDate),
                    new SqlParameter("@DOBAvailable", DOBAvailable),
                    new SqlParameter("@DOB", DOB),
                    new SqlParameter("@AgeAson", AgeAson),
                    new SqlParameter("@Type", Type),
                    new SqlParameter("@EnrollCategory", EnrollCategory),
                    new SqlParameter("@Status", Status),
                    new SqlParameter("@Createdate", Createdate),
                    new SqlParameter("@CreateBy", CreateBy),
                    new SqlParameter("@HouseNo", HouseNo),
                    new SqlParameter("@DeleteFlag", DeleteFlag),
                    new SqlParameter("@VillagenameOther", VillagenameOther),
                    new SqlParameter("@SamgraID", SamgraID),
                    new SqlParameter("@mappingType", mappingType),
                    new SqlParameter("@D2dChildCode", D2dChildCode),
                    new SqlParameter("@MaxID", MaxID),
                    new SqlParameter("@SubjectA", SubjectA),
                    new SqlParameter("@SubjectB", SubjectB),
                    new SqlParameter("@EnrolmentAttempt", EnrolmentAttempt),
                              new SqlParameter("@D2DCode", D2D)
     };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp_Insert_Enrolment_AGP", cmdParameters);


    }


    public bool CheckAllphanumeric(string txtHhno)
    {


        System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("^(?=.*[0-9]+.*)");
        return !objAlphaNumericPattern.IsMatch(txtHhno);
    }
    public bool CheckChu(string txtHhno)
    {


        System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("^(?=.*[a-zA-Z]+.*)");
        //("^[a-zA-Z,.-\\-_]*$");
        return !objAlphaNumericPattern.IsMatch(txtHhno);
    }
    private Boolean Validation()
    {
        try
        {



            if (Session["UnquieId"].ToString().Length > 6)
            { }
            else
            {
                string strQry = " Select [Serial] FROM tblEnrolment_AGP where DeleteFlag=1 and [Serial]='" + txtSrno.Text.ToString() + "' and  SchoolCode ='" + ddlSchool.SelectedValue + "'";
                DataTable dt = objMain.LoadData(strQry);

                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This  Serial No already exists in Database')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }

            }
            if (Convert.ToInt32(ddlMapping.SelectedIndex) == 1 || Convert.ToInt32(ddlMapping.SelectedIndex) == 2)
            {
                if (txtUiniqCOde.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select D2D Unique ID')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
            if ((dllClass.SelectedItem.Text == "10" || dllClass.SelectedItem.Text == "12") && Convert.ToInt32(ddlEduationStatus.SelectedValue) == 6)
            {

                if (ddlNew.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Enrolment Attempt')</script>", false);
                    MpexdrDistrict.Show();
                    return false;

                }
                if (dllClass.SelectedItem.Text == "10")
                {
                    string commOther = "";

                    string commOtherCC = "";
                    foreach (ListItem item in CBL_bookformat.Items)
                    {
                        if (item.Selected)
                        {

                            commOther += "" + item.Value + "" + ",";


                        }
                    }
                    foreach (ListItem item in CBL_bookformatNew.Items)
                    {
                        if (item.Selected)
                        {

                            commOtherCC += "" + item.Value + "" + ",";


                        }
                    }

                    int icountA = 0;
                    foreach (ListItem item in CBL_bookformat.Items)
                    {
                        if (item.Selected)
                        {

                            icountA = icountA + 1;


                        }
                    }

                    int icountB = 0;
                    foreach (ListItem item in CBL_bookformatNew.Items)
                    {
                        if (item.Selected)
                        {

                            icountB = icountB + 1;


                        }
                    }
                    if (Convert.ToInt32(ddlNew.SelectedValue) == 1)
                    {
                        //if (commOther.Length > 0)
                        //{
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select at least 1 Subject from Group A Subjects')</script>", false);
                        //    MpexdrDistrict.Show();
                        //    return false;
                        //}
                        //if (icountB >= 2)
                        //{
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select at least 2 Subject from Group B Subjects')</script>", false);
                        //    MpexdrDistrict.Show();
                        //    return false;
                        //}

                        if (icountB + icountA < 4)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select minimum 4 subjects from Group A and Group B Subjects')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                        if (icountB + icountA > 7)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 7 subjects can be selected from subject group A + subject group B')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                    }
                    if (Convert.ToInt32(ddlNew.SelectedValue) == 2)
                    {
                        if (commOther.Length > 0 || commOtherCC.Length > 0)
                        {
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Minimum 1 Subject selection will be mandatory either in group A subject or in group B subject.')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }

                        if (icountB + icountA > 7)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 7 subjects can be selected from subject group A + subject group B')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                    }

                }
                if (dllClass.SelectedItem.Text == "12")
                {
                    string commOther = "";

                    string commOtherCC = "";
                    if (ddlNew.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Enrolment Attempt')</script>", false);
                        MpexdrDistrict.Show();
                        return false;

                    }
                    foreach (ListItem item in CBL_bookformat.Items)
                    {
                        if (item.Selected)
                        {

                            commOther += "" + item.Value + "" + ",";


                        }
                    }
                    foreach (ListItem item in CBL_bookformatNew.Items)
                    {
                        if (item.Selected)
                        {

                            commOtherCC += "" + item.Value + "" + ",";


                        }
                    }

                    int icountA = 0;
                    foreach (ListItem item in CBL_bookformat.Items)
                    {
                        if (item.Selected)
                        {

                            icountA = icountA + 1;


                        }
                    }

                    int icountB = 0;
                    foreach (ListItem item in CBL_bookformatNew.Items)
                    {
                        if (item.Selected)
                        {

                            icountB = icountB + 1;


                        }
                    }
                    if (Convert.ToInt32(ddlNew.SelectedValue) == 1)
                    {
                        //if (commOther.Length > 0)
                        //{
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select at least 1 Subject from Group A Subjects')</script>", false);
                        //    MpexdrDistrict.Show();
                        //    return false;
                        //}
                        //if (icountB >= 2)
                        //{
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select at least 2 Subject from Group B Subjects')</script>", false);
                        //    MpexdrDistrict.Show();
                        //    return false;
                        //}

                        if (icountB + icountA < 4)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select minimum 4 subjects from Group A and Group B Subjects')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                        if (icountB + icountA > 7)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 7 subjects can be selected from subject group A + subject group B')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }
                    }
                    if (Convert.ToInt32(ddlNew.SelectedValue) == 2)
                    {
                        if (commOther.Length > 0 || commOtherCC.Length > 0)
                        {
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Minimum 1 Subject selection will be mandatory either in group A subject or in group B subject.')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }

                        if (icountB + icountA > 7)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Maximum 7 subjects can be selected from subject group A + subject group B')</script>", false);
                            MpexdrDistrict.Show();
                            return false;
                        }

                    }
                }
            }
            bool Alf = CheckAllphanumeric(txtSrno.Text);
            if (Alf == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter at least one number in SR')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            if (txtHHNo.Text != "")
            {
                bool Alf1 = CheckAllphanumeric(txtHHNo.Text);
                if (Alf1 == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter at least one number in HH No')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
            //bool Alf1 = CheckChu(txtSrno.Text);
            //if (Alf1 == true)
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter atleast one character ')</script>", false);
            //    MpexdrDistrict.Show();
            //    return false;
            //}
            if (ddlSchool.SelectedValue == "99")
            {
                if (txtSchooName.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter school Name')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
                if (txtDiseCode.Text == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter GovtDisecode')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
            if (txtSrno.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Serial No')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }

            if (txtChildName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Child name')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            else if (ddlMapping.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Mapping')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            else if (ddlGender.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Gender')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            else if (txtFatherName.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Father name')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }

            else if (dllClass.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Class')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }

            else if (ddlScat.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select SocialCategory')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            else if (ddlEduationStatus.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Enrollment Category')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            else if (Convert.ToInt32(ddlEduationStatus.SelectedValue) == 6)
            {
                if (dllClass.SelectedItem.Text == "10" || dllClass.SelectedItem.Text == "12")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Only 10th and 12 class enrolment is allowed in Open School Enrolment Category')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }

            if (ddlState.SelectedValue == "23")
            {
                if (txtSamgra.Text.Trim() == "")
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Samagra ID')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
                if (txtSamgra.Text.Trim().Length < 8)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Samagra ID should be 8 or 9 digits')</script>", false);
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

            if (txtDobDate.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter DOB')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }
            DateTime AdmissionDate = Convert.ToDateTime(txtBirth.Text);
            Int32 fDate = ((AdmissionDate.Year) * 10000 + (AdmissionDate.Month) * 100 + (AdmissionDate.Day));



            DateTime DOB;
            DateTime AsDob;
            Int32 Age = 0;

            string DateSarveyDate = txtBirth.Text;
            string[] b = DateSarveyDate.Split('/');

            string DateB = txtDobDate.Text;
            string[] a = txtDobDate.Text.Split('/');
            string BithDate = a[2] + '-' + a[1] + '-' + a[0];



            Age = Convert.ToInt32(b[2]) - Convert.ToInt32(a[2]);
            DOB = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

            Int32 iyear = Convert.ToInt32(a[2]) + Age;
            string dyear = iyear.ToString();
            if (Convert.ToDateTime(txtBirth.Text.ToString()) <= Convert.ToDateTime(txtDobDate.Text))
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Date of admission is subsequent to DOB')</script>", false);
                MpexdrDistrict.Show();
                return false;
            }

            if (Convert.ToInt32(ddlMapping.SelectedValue) == 1)
            {
            }
            else
            {
                if (Convert.ToInt32(Session["mYear"].ToString()) > Convert.ToInt32(AdmissionDate.Year))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure enrollment date should be in current year')</script>", false);
                    MpexdrDistrict.Show();
                    //dDOB.Style.BackColor = Color.Red;
                    return false;
                }
            }


            string strQr1y = " Select mstClassValdation.[Operator], mstClassValdation.[Class], mstLookup.SeqNo AS SeqNoCode FROM mstClassValdation, mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " and  [Age]=" + Age + " ";
            DataTable dtNew = objMain.LoadData(strQr1y);


            if (Convert.ToInt32(dllClass.SelectedValue) <= 5)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select valid class')</script>", false);

                MpexdrDistrict.Show();
                //dDOB.Style.BackColor = Color.Red;
                return false;

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

            if (Session["Schoolid"].ToString() == "99" || Session["SchoolCodeID"].ToString() == "0")
            {
            }
            else
            {
                if (Session["SchoolLevel"].ToString() == "5")
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

                    string strQr1yC = " Select  mstLookup.SeqNo AS SeqNoCode FROM mstLookup where LookupFlag ='ECL' and LookupCode=" + dllClass.SelectedValue + " ";
                    DataTable dtNewC = objMain.LoadData(strQr1yC);
                    Int32 MainClass = Convert.ToInt32(dtNewC.Rows[0]["SeqNoCode"].ToString());
                    if (Session["SchoolLevel"].ToString() == "5")
                    {
                        string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                        DataTable dtNew1 = objMain.LoadData(strQr1y1);
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
                        string strQr1y1 = " Select MaxClass FROM mstClassValdation where  SchoolType=" + Session["SchoolLevel"].ToString() + " ";
                        DataTable dtNew1 = objMain.LoadData(strQr1y1);
                        if (dtNew1.Rows.Count > 0)
                        {
                            Int32 MaxClass = Convert.ToInt32(dtNew1.Rows[0]["MaxClass"].ToString());
                            if (MainClass > MaxClass)
                            {
                                if (Session["SchoolLevel"].ToString() == "1")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Class 1 to 5')</script>", false);
                                    MpexdrDistrict.Show();
                                    return false;
                                }
                                else if (Session["SchoolLevel"].ToString() == "2")
                                {
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please  select Class 1 to 8')</script>", false);
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
            }

            if (Convert.ToInt32(ddlMapping.SelectedValue) != 1)
            {
                if (Age < 5)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 5 and 24 years')</script>", false);
                    MpexdrDistrict.Show();
                    return false;

                }
                if (Age > 24)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure Age is between 5 and 24 years')</script>", false);
                    MpexdrDistrict.Show();
                    return false;
                }
            }
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
        conditions = "LookupFlag ='ECG' and Active=1";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEduationStatus, "Description", "LookupCode", "Select");



    }
    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Enrollment AGP'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

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
            ImageButton2.Visible = true;

            string strQry;
            strQry = "Select * from mstModuleLocking  where [FromName]='Enrolment AGP Entry' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "'";


            DataTable dtModel = objMain.LoadData(strQry);
            if (dtModel.Rows.Count > 0)
            {


                DateTime date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                DateTime date2 = DateTime.Now.Date;





                if (date1 < date2)
                {


                    ImageButton2.Visible = false;
                    gvnroll.Columns[0].Visible = false;
                    gvnroll.Columns[1].Visible = false;

                }

            }

        }


    }
    public void LockingEdit()
    {
        if (ddlYear.SelectedIndex > 0)
        {


            string strQry;

            strQry = "Select * from mstModuleLocking  where [FromName]='Enrolment AGP Edit/Delete' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');

            DateTime date1;
            DateTime date2;
            DataTable dtModel = objMain.LoadData(strQry);
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
            // objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            // ddlState.SelectedIndex = 1;
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
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
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
    public void FillSchool()
    {
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'  union Select  SchoolCode, Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  union Select top 1 '99' as SchoolCode,'Other School' Name from mstSchool  ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");




        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


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

        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlockSearch, "BlockName", "BlockCode", "--Select--");


    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");

        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlphan, "PanchayatName", "PanchayatCode", "Select");


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

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlvillageSearch, "VillageName", "VillageCode", "Select");

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
            DateTime date1;
            DateTime date2;
            date2 = Convert.ToDateTime(lblCreatedate.Text);
            date1 = Convert.ToDateTime(Session["EDITLOCK"]);

            //if (date1 < date2)
            //{
            //    LnkBtnBlock_OnClick.Enabled = false;
            //}
            //else
            //{
            //    LnkBtnBlock_OnClick.Enabled = true;
            //}




            //LnkBtnBlock_OnClick.Enabled = true;
            //lbtn.Enabled = true;

            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            {

                if (date1 < date2)
                {
                    lbtn.Enabled = false;
                    LnkBtnBlock_OnClick.Enabled = false;
                }
                else
                {
                    LnkBtnBlock_OnClick.Enabled = true;
                    lbtn.Enabled = true;
                }
            }

            else
            {
                lbtn.Enabled = false;
                LnkBtnBlock_OnClick.Enabled = false;
            }


            //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
            //{
            //    LnkBtnBlock_OnClick.Enabled = true;
            //    lbtn.Enabled = true;
            //}
            //else
            //{
            //    LnkBtnBlock_OnClick.Enabled = false;
            //    lbtn.Enabled = false;
            //}



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
        //  strQry = " Select UniqueChildCode,Serial as ID,StrConv(ChildName,3) as [Child Name] from tblEnrolment_AGP  where VillageCode='" + CBVillage.SelectedValue + "' order by ChildName ";
        //}
        //else
        //{
        //    strQry = " Select UniqueCode,ChildCode as ID,ChildName1 as [Child Name] from tblDTD  where tblEnrolment_AGP='" + CBVillage.SelectedValue + "' order by ChildName1 ";

        //}
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School')</script>", false);


            this.ddlSchool.Focus();
            return;
        }
        if (Convert.ToString(Session["StateCode"]) == "23")
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
            conditions = conditions + "and tblEnrolment_AGP.SchoolCode='" + ddlSchool.SelectedValue.ToString() + "'";
        }


        //strQry += "  SELECT mst2District.DistrictName ,mst3Block.BlockName ,mstPanchayat.PanchayatName ,mst5Village.VillageName ,tblEnrolment_AGP.SchoolCode, tblEnrolment_AGP.[UniqueChildCode], D2DCode as Uniqueid,case Gender when 1 then 'Male' else 'Female' end as Gender , tblEnrolment_AGP.Serial as  Serial,convert(varchar, tblEnrolment_AGP.[EnrolmentDate],103) as EnrolmentDate, aged.Description as Class, tblEnrolment_AGP.AsOnDate,  tblEnrolment_AGP.[HouseNo] as HHNo1, tblEnrolment_AGP.[Category], [ChildName] AS ChildName, [FatherName] AS FathersName, tblEnrolment_AGP.[Gender], tblEnrolment_AGP.[DOBAvailable], convert(varchar, tblEnrolment_AGP.[DOB],103) as DOB, tblEnrolment_AGP.[AgeAson] as Age, ES.Description AS SocialCategory, mstSchool.Name as School,  EC.Description as EnrolmentCategory, mst5Village.PanchayatCode, mst5Village.BlockCode, mst5Village.DistrictCode, ES1.Description as EduationStatus ,tblEnrolment_AGP.SysFlag,tblEnrolment_AGP.Status ";
        //strQry += "    FROM tblEnrolment_AGP ";
        //strQry += "    INNER JOIN mst5Village ON mst5Village.VillageCode = tblEnrolment_AGP.VillageCode ";
        //strQry += "      LEFT JOIN mst3Block ON mst5Village.BlockCode = mst3Block.BlockCode";
        //strQry += " LEFT JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode";
        //strQry += " LEFT JOIN mst2District ON mst5Village.DistrictCode = mst2District.DistrictCode ";

        //strQry += "  LEFT JOIN mstSchool ON tblEnrolment_AGP.SchoolCode = mstSchool.SchoolCode  LEFT JOIN mstLookup aged on aged.LookupCode=Class and aged.LookupFlag='ECL'";

        //strQry += "	LEFT JOIN mstLookup ES on ES.LookupCode=Category and ES.LookupFlag='CAT'	  LEFT JOIN mstLookup EC on EC.LookupCode=EnrollCategory and EC.LookupFlag='EC'	      LEFT JOIN mstLookup ES1 on ES1.LookupCode=TYPE and ES1.LookupFlag='ES' ";
        //strQry += "	 where " + conditions + "  and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "' and DeleteFlag=1 order by D2DCode  ";

        //DataTable dt1 = objMain.LoadData(strQry);

        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  1),
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadOnlineEnrollment2020AGP]", parm1);



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
        if (ddlState.SelectedValue == "23")
        {
            Div9.Visible = true;
        }
        else
        {
            Div9.Visible = false;
        }
        Session["D2D"] = "";
        FillD2dData();
        MpexdrDistrict.Show();

        // Session["UnquieId"] = UniqueChildCode;
        //string url = "frmAddEnrollmentFrom6.aspx";

        //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=650,left=500,top=500,scrollbars=1,resizable=yes');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


    }
    protected void ddlMapping_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlMapping.SelectedValue) == 1 || Convert.ToInt32(ddlMapping.SelectedValue) == 2)
        {
            LblDtdt.Visible = true;
            txtUiniqCOde.Visible = true;
            LinkButton1.Visible = true;
        }
        else
        {
            LblDtdt.Visible = false;
            txtUiniqCOde.Visible = false;
            txtUiniqCOde.Text = "";
            LinkButton1.Visible = false;
        }
        MpexdrDistrict.Show();

    }
    protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((dllClass.SelectedItem.Text == "10" || dllClass.SelectedItem.Text == "12") && Convert.ToInt32(ddlEduationStatus.SelectedValue) == 6)
        {
            if (dllClass.SelectedItem.Text == "10")
            {
                Load10();
            }
            if (dllClass.SelectedItem.Text == "12")
            {
                Load12();
            }
            txt_pbnameNew.Text = "";
            txt_pbname.Text = "";
            GroupA.Visible = true;
            GroupB.Visible = true;
            Div12.Visible = true;
        }
        else
        {
            txt_pbnameNew.Text = "";
            txt_pbname.Text = "";
            GroupA.Visible = false;
            GroupB.Visible = false;
            Div12.Visible = false;
        }
        MpexdrDistrict.Show();
    }

    public void Load10()
    {
        string strQry = " select *  from [mstLookup]   where LookupFlag='OP' ";


        DataTable dtRole = objMain.LoadData(strQry);
        CBL_bookformat.DataSource = dtRole;
        CBL_bookformat.DataTextField = "Description";
        CBL_bookformat.DataValueField = "LookupCode";
        CBL_bookformat.DataBind();

        string strQry1 = " select *  from [mstLookup]   where LookupFlag='OPN' ";


        DataTable dtRole1 = objMain.LoadData(strQry1);
        CBL_bookformatNew.DataSource = dtRole1;
        CBL_bookformatNew.DataTextField = "Description";
        CBL_bookformatNew.DataValueField = "LookupCode";
        CBL_bookformatNew.DataBind();
    }
    public void Load12()
    {
        string strQry = " select *  from [mstLookup]   where LookupFlag='CP' ";


        DataTable dtRole = objMain.LoadData(strQry);
        CBL_bookformat.DataSource = dtRole;
        CBL_bookformat.DataTextField = "Description";
        CBL_bookformat.DataValueField = "LookupCode";
        CBL_bookformat.DataBind();

        string strQry1 = " select *  from [mstLookup]   where LookupFlag='CPN' ";


        DataTable dtRole1 = objMain.LoadData(strQry1);
        CBL_bookformatNew.DataSource = dtRole1;
        CBL_bookformatNew.DataTextField = "Description";
        CBL_bookformatNew.DataValueField = "LookupCode";
        CBL_bookformatNew.DataBind();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
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
            if (txtDiseCode.Text.Trim().Length < 11)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Govt DiseCode should be 11 digits')</script>", false);

                return;
            }
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
            string strQry1 = "select ManagementType,WorkingStatus,SchoolLevel,SchoolCodeID from mstSchool where Govt_DiseCode='" + txtDiseCode.Text.Trim() + "'   ";


            DataTable dtDid = objMain.LoadData(strQry1);
            if (dtDid.Rows.Count > 0)
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
        txt_pbnameNew.Text = "";
        txt_pbname.Text = "";
        GroupA.Visible = false;
        GroupB.Visible = false;
        Session["UnquieId"] = "";
        LblDtdt.Visible = false;
        txtUiniqCOde.Visible = false;
        ddlMapping.SelectedIndex = 0;
        txtUiniqCOde.Text = "";
        txtChildName.Text = "";
        txtFatherName.Text = "";
        txtHHNo.Text = "";
        txtSrno.Text = "";
        txtSamgra.Text = "";
        txtSurveyVillage.Text = "";

        ddlEduationStatus.SelectedIndex = 0;
        LblDtdt.Visible = false;
        txtUiniqCOde.Visible = false;
        ddlMapping.Enabled = true;
        ddlMapping.SelectedIndex = 0;
        dllClass.SelectedIndex = 0;
        txtUiniqCOde.Text = "";
        ddlScat.SelectedIndex = 0;
        txtBirth.Text = "";
        txtDobDate.Text = "";
        Session["D2D"] = "";
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
        MpexdrDistrict.Show();
    }

    public void FillD2dData()
    {
        string strQry = " Select [UniqueChildCode],isnull(EnrolmentAttempt,0) as EnrolmentAttempt, mappingType,SubjectA,SubjectB,D2dChildCode,VillagenameOther,SamgraID,ChildCode,mstSchool.name,tblEnrolment_AGP.[VillageCode],EnrolmentDate as SurvayDate,Class,AsOnDate,[Serial],[HouseNo],[Category],[ChildName] as ChildName,[FatherName] as FathersName,[Gender],[DOBAvailable],[DOB],[AgeAson],Type as EduationStatus,tblEnrolment_AGP.[SchoolCode],[EnrollCategory], mst5Village.PanchayatCode,mst5Village.BlockCode,mst5Village.DistrictCode,tblEnrolment_AGP.Status FROM (mst5Village INNER JOIN tblEnrolment_AGP ON mst5Village.VillageCode = tblEnrolment_AGP.VillageCode) left JOIN mstSchool ON tblEnrolment_AGP.SchoolCode = mstSchool.SchoolCode where UniqueChildCode='" + Session["UnquieId"].ToString() + "' ";
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
            ddlNew.SelectedValue = dt.Rows[0]["EnrolmentAttempt"].ToString();
            ddlMapping.SelectedValue = dt.Rows[0]["mappingType"].ToString();
            ddlMapping_SelectedIndexChanged(ddlMapping, null);
            ddlMapping.Enabled = false;
            if (dt.Rows[0]["mappingType"].ToString() == "1" || dt.Rows[0]["mappingType"].ToString() == "2")
            {
                LinkButton1.Visible = false;
            }
            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString();

            txtSrno.Text = dt.Rows[0]["Serial"].ToString();
            txtChildName.Text = dt.Rows[0]["ChildName"].ToString();
            txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();

            txtSamgra.Text = dt.Rows[0]["SamgraID"].ToString();
            txtSurveyVillage.Text = dt.Rows[0]["VillagenameOther"].ToString();
            //villagecode = dt.Rows[0]["VillageCode"].ToString();




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
            ddlEduationStatus.SelectedValue = Convert.ToInt32(dt.Rows[0]["EnrollCategory"].ToString()).ToString();
            //ddlEnroll.SelectedValue = dt.Rows[0]["EduationStatus"].ToString();


            lblSchool.Text = dt.Rows[0]["name"].ToString();
            txtUiniqCOde.Text = dt.Rows[0]["D2dChildCode"].ToString();
            txtHHNo.Text = dt.Rows[0]["HouseNo"].ToString();
            dllClass.SelectedValue = dt.Rows[0]["Class"].ToString();

            ddlClass_SelectedIndexChanged(ddlDistrict, null);
            string cmeeting = dt.Rows[0]["SubjectA"].ToString();
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

            }



            string cmeeting1 = dt.Rows[0]["SubjectB"].ToString();
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

            }
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
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblStatus = (gvr.FindControl("lblStatus") as Label).Text;
        string lblD2dChildCode = (gvr.FindControl("lblD2dChildCode") as Label).Text;

        string strQry = "";


        int res1 = DeleteEnrollMentData(UniqueChildCode, lblStatus);

        if (res1 > 0)
        {
            LoadData();
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }


    }
    public int DeleteEnrollMentData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag),
            new SqlParameter("@UserName",  Session["username"].ToString() )
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteEnrollMentDataModifyAGP", cmdParameters);
    }
    public void ddlMappingNew_SelectedIndexChanged(object sender, EventArgs e)
    {
        LinkButton1.Visible = false;
        Session["D2D"] = "";
        GridView2.DataSource = null;
        GridView2.DataBind();
        MpexdrDistrict2.Show();
        MpexdrDistrict.Show();
        ddlBlockSearch.SelectedValue = ddlBlock.SelectedValue;
        ddlphan.SelectedValue = ddlPanchayat.SelectedValue;

        //ddlvillageSearch.SelectedValue = ddlVillage.SelectedValue;

    }
    public void FillCBClusterserach()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlockSearch.SelectedValue + "'";

        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlphan, "PanchayatName", "PanchayatCode", "Select");


    }
    public void FillCVillageSer()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");

        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlockSearch.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlockSearch.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlphan.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);


        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlvillageSearch, "VillageName", "VillageCode", "Select");

    }
    protected void ddlBlockSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBClusterserach();
        ddlvillageSearch.Items.Clear();
        MpexdrDistrict2.Show();
        MpexdrDistrict.Show();
    }
    protected void ddlphan_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillageSer();
        MpexdrDistrict2.Show();
        MpexdrDistrict.Show();
    }

    protected void btnSerachNew_Click(object sender, EventArgs e)
    {
        LoadDataNew();
        MpexdrDistrict2.Show();
        MpexdrDistrict.Show();
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        int indcount1 = 0, indD2d = 0;
        foreach (GridViewRow Itemst in GridView2.Rows)
        {
            if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
            {
                indcount1++;
            }

        }


        if (indcount1 == 1)
        {
            foreach (GridViewRow Itemst in GridView2.Rows)
            {
                if (((CheckBox)Itemst.FindControl("ChkD2d")).Checked)
                {
                    Label lblD2dUniqueCode = (Label)Itemst.FindControl("lblD2dUniqueCode");
                    Label lblNewUniqueffId = (Label)Itemst.FindControl("lblNewUniqueffId");
                    txtUiniqCOde.Text = lblNewUniqueffId.Text;

                    Session["D2D"] = lblD2dUniqueCode.Text;
                }
            }

            MpexdrDistrict.Show();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select single matching entry')</script>", false);
            MpexdrDistrict2.Show();
            MpexdrDistrict.Show();
            return;
        }
    }
    public void LoadDataNew()
    {
        string strQry = "";
        //if (Program.UserLevel == 1)
        //{
        //  strQry = " Select UniqueChildCode,Serial as ID,StrConv(ChildName,3) as [Child Name] from tblEnrolment_AGP  where VillageCode='" + CBVillage.SelectedValue + "' order by ChildName ";
        //}
        //else
        //{
        //    strQry = " Select UniqueCode,ChildCode as ID,ChildName1 as [Child Name] from tblDTD  where tblEnrolment_AGP='" + CBVillage.SelectedValue + "' order by ChildName1 ";

        //}

        conditions = "";
        conditions = " and mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";

        if (ddlDistrict.SelectedIndex > 0)
        {
            conditions = conditions + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlockSearch.SelectedIndex > 0)
        {

            conditions = conditions + "and mst5Village.BlockCode='" + ddlBlockSearch.SelectedValue.ToString() + "'";

        }



        if (ddlphan.SelectedIndex > 1)
        {
            conditions = conditions + "and mst5Village.PanchayatCode='" + ddlphan.SelectedValue.ToString() + "'";
        }
        if (ddlvillageSearch.SelectedIndex > 0)
        {

            conditions = conditions + "and mst5Village.VillageCode='" + ddlvillageSearch.SelectedValue.ToString() + "'";
        }

        int Flag = 0;
        if (Convert.ToInt32(ddlMapping.SelectedIndex) == 1)
        {
            Flag = 1;
        }
        if (Convert.ToInt32(ddlMapping.SelectedIndex) == 2)
        {
            Flag = 2;
        }

        SqlParameter[] parm1 = new SqlParameter[]
            {

               new SqlParameter("@Con",  conditions),
                 new SqlParameter("@Flag",  1),
            };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadOnlineD2d2020AGP]", parm1);



        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
    }
}