using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmNewSchoolActivity : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CalendarExtender1.StartDate = DateTime.Today;
            //CalendarExtender1.EndDate = DateTime.Today.AddMonths(1);
            // UserData();


            ModalPopupExtender.Hide();

            LoadEnrolled();
            dropdownbind();
            if (Session["StateCode"].ToString() == "9A" || Session["StateCode"].ToString() == "9B" || Session["StateCode"].ToString() == "9C")
            {
                DV.Visible = true;
                Slides.Visible = false;
            }
            else
            {
                Slides.Visible = true;
                DV.Visible = false;

            }
            CalendarExtenderTourdate.StartDate = Convert.ToDateTime(Session["FromDate"].ToString());
            if (Convert.ToString(Session["user_level"]) == "")
            {
                Response.Redirect("login.aspx");
            }

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

            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

            if (Request.QueryString["ID"] != null)
            {

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
                    con = "ActivityDate =('" + aToDate + "') and UserEntry=2  and ApproveStatus='FC'  and mst5village.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = LoadAllActivtiyDatewise(con, 1);

                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    con = "ActivityDate =('" + aToDate + "') and UserEntry=3  and ApproveStatus='B' and mst5village.ClusterCode='" + Session["Cluseter"].ToString() + "' ";
                    dtMain = LoadAllActivtiyDatewise(con, 1);
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
                        ddlSchool.SelectedValue = dtMain.Rows[0]["SchoolCode"].ToString();

                        btnSerach_Click(btnSerach, null);
                    }
                }

                //DataTable dt = objMain.GetActivityUserWiseMaxDate(ddlUser.SelectedValue);
                //if (dt.Rows.Count > 0)
                //{
                //    CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dtMain.Rows[0]["Villagecode"].ToString());
                //}
                pnlMain.Enabled = false;
                //btnSerach_Click(btnSerach, null);
            }
            ViewState["GUID_School"] = "";



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

    protected void txtchdate(object sender, EventArgs e)
    {
        btnSerach_Click(btnSerach, null);
    }
    #region anuj  code
    public void dropdownbind()
    {
        objComman.BindDLL("mstLookup", "LookupCode, description", "Lookupflag= 'G'", "description", "asc", ddlgender, "description", "LookupCode", "--Select--");
        ddlgender.SelectedIndex = 2;
        objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='CL' and description in  ('7','8','9')", "LookupCode", "asc", ddlClass, "Description", "LookupCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag = 'CAT'", "LookupCode", "asc", ddlCategory, "Description", "LookupCode", "Select");
        objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", "flag = 50 and  language= 0 and TopicDiscussID in  (228,229,230,231,232,233,240,241,242,243)", "TopicDIscussID", "asc", ddlsession, "TopicDiscussName", "TopicDIscussID", "Select");

        ddlgender.SelectedValue = "2";

        if (Convert.ToString(ddldobavail.SelectedValue) != "")
        {
            if (ddldobavail.SelectedValue == "1")
            {
                txtDOB.Visible = true;
                txtage.Visible = false;
                lblage.Visible = false;
                lblDOB.Visible = true;
            }
            else if (ddldobavail.SelectedValue == "2")
            {
                txtDOB.Visible = false;
                txtage.Visible = true;
                lblage.Visible = true;
                lblDOB.Visible = false;
            }
        }

    }
    protected void dddl_DO(object sender, EventArgs e)
    {
        if (Convert.ToString(ddldobavail.SelectedValue) != "")
        {
            if (ddldobavail.SelectedValue == "1")
            {
                txtDOB.Visible = true;
                txtage.Visible = false;
                lblage.Visible = false;
                lblDOB.Visible = true;
            }
            else if (ddldobavail.SelectedValue == "2")
            {
                txtDOB.Visible = false;
                txtage.Visible = true;
                lblage.Visible = true;
                lblDOB.Visible = false;
            }
        }
        ModalAddclass.Show();
    }
    protected void LnkBtnBlock_ffOnClick(object sender, EventArgs e)
    {
        string session2 = "";
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        if (Convert.ToString(hdnsession2.Value) != "")
        {
            session2 = Convert.ToString(hdnsession2.Value);
        }
        string UniqueCode = (gvr.FindControl("lblUniqueChildRCode") as Label).Text;
        string whr = " where a.UniqueChildRCode = '" + UniqueCode + "' ";

        string datacheck = " where a.UniqueChildRCode ='" + UniqueCode + "'";

        DataTable dt = new DataTable();
        dt = DatabindReg(datacheck, 1);
        txtage.Text = "";
        txtDOB.Text = "";
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["Registrationdate"]) != "")
            {
                DateTime Registrationdate = Convert.ToDateTime(dt.Rows[0]["Registrationdate"].ToString());
                txtRegistration.Text = Registrationdate.ToString("dd/MM/yyy");
                //txtRegistration.Text = Convert.ToString(dt.Rows[0]["Registrationdate"]);
                hdnUniqueChildRCode.Value = Convert.ToString(dt.Rows[0]["UniqueChildRCode"]);
            }
            if (Convert.ToString(dt.Rows[0]["DOBAvailable"]) != "")
            {
                ddldobavail.SelectedValue = Convert.ToString(dt.Rows[0]["DOBAvailable"]);


                if (ddldobavail.SelectedValue == "1")
                {
                    txtDOB.Visible = true;
                    txtage.Visible = false;
                    lblage.Visible = false;
                    lblDOB.Visible = true;
                }
                else if (ddldobavail.SelectedValue == "2")
                {
                    txtDOB.Visible = false;
                    txtage.Visible = true;
                    lblage.Visible = true;
                    lblDOB.Visible = false;
                }
            }
            if (Convert.ToString(dt.Rows[0]["DOB"]) != "")
            {
                DateTime DateDrop = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString());
                txtDOB.Text = DateDrop.ToString("dd/MM/yyy");
                //txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]);
            }
            if (Convert.ToString(dt.Rows[0]["Age"]) != "")
            {
                txtage.Text = Convert.ToString(dt.Rows[0]["Age"]);
            }


            txtGirlChildName.Text = Convert.ToString(dt.Rows[0]["ChildName"]);

            ddlCategory.SelectedValue = Convert.ToString(dt.Rows[0]["Category"]);

            txtFathername.Text = Convert.ToString(dt.Rows[0]["FatherName"]);


            ddlClass.SelectedValue = Convert.ToString(dt.Rows[0]["LookupCode"]);

            txtParentMobileNumber.Text = Convert.ToString(dt.Rows[0]["MobileNo"]);

            txtSRNumber.Text = Convert.ToString(dt.Rows[0]["SRnumber"]);

            ddlgender.SelectedValue = Convert.ToString(dt.Rows[0]["Gender"]);

            ModalAddclass.Show();
        }

        // string strPopup = "<script language='javascript' ID='script1'>"

        //// Passing intId to popup window.
        //+ "window.open('FrmAddChild.aspx?databind=" + HttpUtility.UrlEncode("'" + UniqueCode + "','" + session2 + "'")

        //+ "','new window', ' width=730,height=300,left=300,top=200,dependant=no, scrollbars=1,location=0, alwaysRaised=no, menubar=no, resizeable=no,  toolbar=no, status=no, center=yes')"

        //+ "</script>";

        // ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

    }

    public void cleardatapop()
    {
        txtRegistration.Text = Convert.ToDateTime(txtDate.Text).ToString("dd/MM/yyyy");
        txtRegistration.Enabled = false;
        ddldobavail.SelectedValue = "0";
        txtDOB.Text = "";
        txtage.Text = "";
        txtGirlChildName.Text = "";
        ddlCategory.SelectedValue = "0";
        txtFathername.Text = "";
        ddlClass.SelectedValue = "0";
        txtParentMobileNumber.Text = "";
        txtSRNumber.Text = "";
        ddlgender.SelectedValue = "2";
        ddldobavail.SelectedValue = "2";
        if (ddldobavail.SelectedValue == "1")
        {
            txtDOB.Visible = true;
            txtage.Visible = false;
            lblage.Visible = false;
            lblDOB.Visible = true;
        }
        else if (ddldobavail.SelectedValue == "2")
        {
            txtDOB.Visible = false;
            txtage.Visible = true;
            lblage.Visible = true;
            lblDOB.Visible = false;
        }
    }

    public bool CheckAllphanumeric(string txtHhno)
    {


        System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("^(?=.*[0-9]+.*)");
        return !objAlphaNumericPattern.IsMatch(txtHhno);
    }

    protected void onclick_savedata(object sender, EventArgs e)
    {
        string UniqueChildRCode = "", SRnumber = "", SchoolCode = "", CreatedBy = "", MobileNo = "", FatherName = "", ChildName = "", VillageCode = "", flag = "", session2 = "";
        int Class = 0, Category = 0, Age = 0, DOBAvailable = 0, Gender = 0, ID = 0;
        DateTime? DOB = null; DateTime? Registrationdate = null;
        if (Convert.ToString(hdnUniqueChildRCode.Value) != "")
        {
            UniqueChildRCode = Convert.ToString(hdnUniqueChildRCode.Value);
            flag = "U";
            if (Convert.ToString(ViewState["session2"]) != "")
            {
                session2 = Convert.ToString(ViewState["session2"]);
            }
        }
        else
        {
            UniqueChildRCode = objMain.Generate_RandomString(8);
            flag = "I";
        }
        if (txtRegistration.Text != "")
        {
            Registrationdate = Convert.ToDateTime(txtRegistration.Text);
            if (Convert.ToString(session2) != "")
            {
                if (Convert.ToDateTime(txtRegistration.Text) > Convert.ToDateTime(session2))
                {

                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Registration date should be greater  then Session2 date')</script>", false);
                    return;
                }

            }



            if (Convert.ToInt32(ddldobavail.SelectedValue) == 1)
            {
                DateTime DOB1;
                DateTime AsDob;
                Int32 Age1 = 0;
                string DateSarveyDate = txtDOB.Text;
                string[] b = DateSarveyDate.Split('/');

                string DateB = txtDate.Text;
                string[] a = DateB.Split('/');
                string BithDate = a[2] + '-' + a[1] + '-' + a[0];



                Age1 = Convert.ToInt32(a[2]) - Convert.ToInt32(b[2]);
                DOB1 = Convert.ToDateTime(a[2] + '-' + a[1] + '-' + a[0]);

                Int32 iyear = Convert.ToInt32(a[2]) + Age1;
                string dyear = iyear.ToString();
                //  AsDob = Convert.ToDateTime(dyear + '-' + a[1] + '-' + a[0]);

                if (Age1 < 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Fill Age between 9 to 18')</script>", false);


                    this.txtDOB.Focus();

                    ModalAddclass.Show();
                    return;

                }
                if (Age1 > 18)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Fill Age between 9 to 18')</script>", false);

                    ModalAddclass.Show();
                    this.txtDOB.Focus();
                    return;
                }
            }

        }

        if (ddldobavail.SelectedValue != "")
        {
            DOBAvailable = Convert.ToInt32(ddldobavail.SelectedValue);
        }
        if (ddldobavail.SelectedValue == "1")
        {
            DOB = Convert.ToDateTime(txtDOB.Text);
        }
        else if (ddldobavail.SelectedValue != "")
        {
            Age = Convert.ToInt32(txtage.Text);
        }
        if (txtGirlChildName.Text != "")
        {
            ChildName = Convert.ToString(txtGirlChildName.Text);
        }
        if (ddlCategory.SelectedValue != "")
        {
            Category = Convert.ToInt32(ddlCategory.SelectedValue);
        }
        if (txtFathername.Text != "")
        {
            FatherName = Convert.ToString(txtFathername.Text);
        }
        if (ddlClass.SelectedValue != "")
        {
            Class = Convert.ToInt32(ddlClass.SelectedValue);
        }
        if (txtParentMobileNumber.Text != "")
        {
            MobileNo = Convert.ToString(txtParentMobileNumber.Text);
        }
        SRnumber = Convert.ToString(txtSRNumber.Text);
        bool Alf = CheckAllphanumeric(txtSRNumber.Text);
        if (Alf == true)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter atleast one numbe')</script>", false);

            ModalAddclass.Show();
            return;
        }
        if (txtSRNumber.Text != "" && flag == "I")
        {
            SRnumber = Convert.ToString(txtSRNumber.Text);
            DataTable dtcheck = new DataTable();
            DataTable dtcheck2 = new DataTable();

            string condition = " where villagecode= '" + ddlVilage.SelectedValue + "' and Schoolcode= '" + ddlSchool.SelectedValue + "' and SrNumber= '" + SRnumber + "'";
            string whr1 = " where villagecode= '" + ddlVilage.SelectedValue + "' and Schoolcode= '" + ddlSchool.SelectedValue + "' ";

            dtcheck = GetSRNumberData(condition);
            if (dtcheck.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate SRNumber .Please enter New SRNumber')</script>", false);

                ModalAddclass.Show();
                return;
            }

            dtcheck2 = GetSRNumberData(whr1);
            if (dtcheck2.Rows.Count > 12)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Number of child already added')</script>", false);


                ModalAddclass.Show();
                return;
            }
        }
        if (ddlgender.SelectedValue != "")
        {
            Gender = Convert.ToInt32(ddlgender.SelectedValue);
        }
        if (Convert.ToString(Session["username"]) != "" && Convert.ToString(Session["username"]) != null)
        {
            CreatedBy = Convert.ToString(Session["username"]);
        }

        ID = InsertRetentionChildata(UniqueChildRCode, ddlVilage.SelectedValue, ddlSchool.SelectedValue, Registrationdate, DOBAvailable, DOB, Age, ChildName, Category, FatherName, Class, MobileNo, SRnumber, Gender, CreatedBy, flag);
        if (ID > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Successfull')</script>", false);
            Session["asdsa"] = "1";
            DataTable dt = new DataTable();
            string con = "";
            if (ddlVilage.SelectedIndex > 0)
            {
                con = " where a.villagecode= '" + ddlVilage.SelectedValue + "'";
            }
            if (ddlSchool.SelectedIndex > 0)
            {
                con = con + " and a.Schoolcode= '" + ddlSchool.SelectedValue + "'";
            }
            con = con + " and CONVERT(date,a. Registrationdate) <= CONVERT(date,'" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "')";
            dt = DatabindReg(con, 1);
            GvReg.DataSource = dt;
            GvReg.DataBind();
            if (hdnsession2.Value.Length > 2)
            {
                DateTime currentdate = Convert.ToDateTime(txtDate.Text);
                string Cdate = currentdate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

                if (Convert.ToDateTime(Cdate) > Convert.ToDateTime(hdnsession2.Value))
                {

                    if (GvReg.Rows.Count > 6)
                    {
                        pnlLife.Enabled = true;
                    }
                }

            }

            //      Response.Redirect("./frmNewSchoolActivity.aspx?ID=5",true);
            //if (Convert.ToString(ddldobavail.SelectedValue) != "")
            //{
            //    if (ddldobavail.SelectedValue == "1")
            //    {
            //        txtage.Style.Add("display", "none");
            //        lblage.Style.Add("display", "none");


            //        txtDOB.Style.Add("display", "block");
            //        lblDOB.Style.Add("display", "block");
            //    }
            //    else if (ddldobavail.SelectedValue == "2")
            //    {
            //        txtage.Style.Add("display", "block");
            //        lblage.Style.Add("display", "block");


            //        txtDOB.Style.Add("display", "none");
            //        lblDOB.Style.Add("display", "none");

            //    }

            //    ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>CloseWin(true)</script>", false);
            //}


        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Unsuccessfull')</script>", false);

        }

    }
    public int InsertRetentionChildata(string UniqueChildRCode, string VillageCode, string SchoolCode, DateTime? Registrationdate, int DOBAvailable, DateTime? DOB, int Age, string ChildName, int Category, string FatherName, int Class, string MobileNo, string SRnumber, int Gender, string CreatedBy, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@UniqueChildRCode",UniqueChildRCode),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@schoolcode", SchoolCode),
            new SqlParameter("@Registrationdate", Registrationdate),
            new SqlParameter("@DOBAvailable", DOBAvailable),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@Age", Age),
            new SqlParameter("@ChildName", ChildName),
            new SqlParameter("@Category", Category),
            new SqlParameter("@FatherName", FatherName),
            new SqlParameter("@Class", Class),
            new SqlParameter("@MobileNo", MobileNo),
            new SqlParameter("@SRnumber", SRnumber),
            new SqlParameter("@Gender", Gender),
            new SqlParameter("@CreatedBy", CreatedBy),
              new SqlParameter("@Flag", flag),

        };

        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_InsertRetentionChildata", cmdParameters);

        return result;
    }


    protected void onclick_btnaddclass(object sender, EventArgs e)
    {
        cleardatapop();
        DataTable dtchk = new DataTable();
        string con = " ", session2 = "";
        if (ddlVilage.SelectedIndex > 0)
        {
            con = " where villagecode= '" + ddlVilage.SelectedValue + "'";
        }
        if (ddlSchool.SelectedIndex > 0)
        {
            con = con + " and Schoolcode= '" + ddlSchool.SelectedValue + "'";
        }
        if (Convert.ToString(hdnsession2.Value) != "")
        {
            session2 = Convert.ToString(hdnsession2.Value);
        }
        dtchk = GetSRNumberData(con);
        if (hdnsession2.Value.Length > 2)
        {
            DateTime currentdate = Convert.ToDateTime(txtDate.Text);
            string Cdate = currentdate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

            if (Convert.ToDateTime(Cdate) > Convert.ToDateTime(hdnsession2.Value))
            {

                if (GvReg.Rows.Count > 6)
                {
                    pnlLife.Enabled = true;
                }
            }

        }

        if (dtchk.Rows.Count > 12)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('only  13 girls can be added')</script>", false);
            return;
        }
        else
        {
            hdnUniqueChildRCode.Value = "";
            //Session["V"] = ddlVilage.SelectedValue;
            //Session["S"] = ddlSchool.SelectedValue;
            //Session["D"] = txtDate.Text;
            //string strPopup = "<script language='javascript' ID='script1'>"


            //+ "window.open('FrmAddChild.aspx?data=" + HttpUtility.UrlEncode("" + ddlVilage.SelectedValue + "," + ddlSchool.SelectedValue + "," + session2 + "")

            //+ "','new window', ' width=730,height=300,left=300,top=200,dependant=no, scrollbars=1,location=0, alwaysRaised=no, menubar=no, resizeable=no,  toolbar=no, status=no, center=yes')"

            //+ "</script>";

            //ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

            ModalAddclass.Show();
        }

    }
    protected void gv_regOnDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView GV_Retention = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int dtcount = 0;
            DropDownList ddlAttendance = (DropDownList)e.Row.FindControl("ddlAttendance");
            LinkButton LinkButton = (LinkButton)e.Row.FindControl("lbtn1");
            if (Convert.ToString(Session["Reg_Count"]) != "")
            {
                dtcount = Convert.ToInt32(Session["Reg_Count"]);
            }

            if (dtcount > 6)
            {
                ddlAttendance.Enabled = true;
                ddlsession.Enabled = true;
            }
            else
            {
                ddlAttendance.Enabled = false;
                ddlsession.Enabled = false;
            }
            if (ddlAttendance.SelectedValue == "0")
            {
                LinkButton.Visible = false;
            }

        }
    }
    protected void ddlsession_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (Convert.ToInt32(ddlsession.SelectedValue)== 230 || Convert.ToInt32(ddlsession.SelectedValue) == 233 || Convert.ToInt32(ddlsession.SelectedValue) == 242 || Convert.ToInt32(ddlsession.SelectedValue) == 245 || Convert.ToInt32(ddlsession.SelectedValue) == 248)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please select other Session');", true);
        //    ddlsession.SelectedIndex = 0;
        //    return;

        //}


        string cond = " where 1=1 and";
        string cond1 = " where 1=1 and";
        if (ddlsession.SelectedIndex > 0)
        {
            DataTable dtsessionchcek = new DataTable();
            if (ddlSchool.SelectedValue != "0")
            {
                cond = cond + "  Schoolcode = '" + ddlSchool.SelectedValue + "' ";
                cond1 = cond1 + "  Schoolcode = '" + ddlSchool.SelectedValue + "' ";
            }

            if (ddlVilage.SelectedValue != "0")
            {
                cond = cond + " and Villagecode = '" + ddlVilage.SelectedValue + "' ";
                cond1 = cond1 + " and Villagecode = '" + ddlVilage.SelectedValue + "' ";
            }
            if (ddlsession.SelectedValue == "228")
            {
                cond = cond + " and Session = " + (Convert.ToInt32(ddlsession.SelectedValue)) + " ";

            }
            if (ddlsession.SelectedValue != "228")
            {
                cond = cond + " and Session = " + (Convert.ToInt32(ddlsession.SelectedValue)) + " ";

            }
            if (ddlsession.SelectedValue != "228")
            {
                cond = cond + " and Session = " + (Convert.ToInt32(ddlsession.SelectedValue)) + " ";
            }
            if (ddlsession.SelectedValue != "228")
            {
                if (ddlsession.SelectedValue == "240")
                {
                    cond1 = cond1 + " and Session = " + (Convert.ToInt32(ddlsession.SelectedValue) - 7) + " ";
                }
                else
                {

                    cond1 = cond1 + " and Session = " + (Convert.ToInt32(ddlsession.SelectedValue) - 1) + " ";
                }
            }


            dtsessionchcek = Getatt_SessionData(cond);
            DataTable dtsessionchcek1 = Getatt_SessionData(cond1);

            DataTable dtrecordcheck = new DataTable();
            string whr1 = " where villagecode= '" + ddlVilage.SelectedValue + "' and Schoolcode= '" + ddlSchool.SelectedValue + "' and convert(varchar(50),AttDate,103) = convert(varchar(50),'" + txtDate.Text + "',103)  ";

            dtrecordcheck = Get_Attendance_Data(whr1);
            if (dtrecordcheck.Rows.Count > 0)
            {
                string s1 = "", s2 = "", SName = "";
                s1 = Convert.ToString(dtrecordcheck.Rows[0]["session"]);
                s2 = Convert.ToString(ddlsession.SelectedValue);

                SName = Convert.ToString(dtrecordcheck.Rows[0]["TopicDiscussName"]);
                if (s1 != s2)
                {
                    int kkk = Convert.ToInt32(s1);
                    ddlsession.SelectedValue = kkk.ToString();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Today " + SName + " Attendance already upload.')</script>", false);


                    return;
                }
            }
            if (dtsessionchcek1.Rows.Count > 0)
            {
            }
            else
            {
                if (ddlsession.SelectedValue == "228")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please select  Previous session');", true);
                    ddlsession.SelectedIndex = 0;
                }
            }

            if (dtsessionchcek.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('" + ddlsession.SelectedItem.Text + "   Attendance already upload Previous Date');", true);
                ddlsession.SelectedIndex = 0;

            }
            else
            {
                if (ddlsession.SelectedValue == "228")
                {
                    if (dtsessionchcek.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Session 1   Attendance already upload Previous Date');", true);
                        ddlsession.SelectedIndex = 0;
                    }

                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please select  Previous session');", true);
                    //ddlsession.SelectedIndex = 0;
                }


            }
        }
    }



    public DataTable Get_Attendance_Data(string condition)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@Condition", condition)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_Attendance_Data]", cmdParameters);
    }

    public DataTable GetSRNumberData(string condition)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@Condition", condition)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_SRNumberData]", cmdParameters);
    }

    public DataTable Getatt_SessionData(string condition)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@Condition", condition)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_Att_sessionData]", cmdParameters);
    }

    public DataTable DatabindReg(string WhereQuery, int flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@WhereQuery", WhereQuery),
new SqlParameter("@Flag", flag)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Sp_DataReg]", cmdParameters);
    }


    public DataTable DatabindRegNew(string WhereQuery, int flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
        new SqlParameter("@SchoolCOde", ddlSchool.SelectedValue),
        new SqlParameter("@Villagecode", ddlVilage.SelectedValue),
        new SqlParameter("@Flag", "1"),
        new SqlParameter("@Date", Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd"))
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Sp_DataRegNew]", cmdParameters);
    }

    public DataTable DatabindRegNew2021(string WhereQuery, int flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
        new SqlParameter("@SchoolCOde", ddlSchool.SelectedValue),
        new SqlParameter("@Villagecode", ddlVilage.SelectedValue),
        new SqlParameter("@Flag", "2"),
        new SqlParameter("@Date", Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd"))
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Sp_DataRegNew]", cmdParameters);
    }

    public void GVregdatabind()
    {
        DataTable dt = new DataTable();
        string con = "";
        if (ddlVilage.SelectedIndex > 0)
        {
            con = " where a.villagecode= '" + ddlVilage.SelectedValue + "'";
        }
        if (ddlSchool.SelectedIndex > 0)
        {
            con = con + " and a.Schoolcode= '" + ddlSchool.SelectedValue + "'";
        }
        con = con + " and CONVERT(date,a.Registrationdate) <= CONVERT(date,'" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "')";

        dt = DatabindReg(con, 1);

        DataTable dtLiff = DatabindRegNew(con, 1);
        if (dtLiff.Rows.Count > 0)
        {
            for (int i = 0; i < dtLiff.Rows.Count; i++)
            {
                if (dtLiff.Rows[i]["TBCode"].ToString().Length > 0)
                {
                    divLiff_Click(ddlliffTb, null);
                    ddlliffTb.SelectedValue = dtLiff.Rows[i]["TBCode"].ToString();
                }
            }

        }
        if (dt.Rows.Count > 0)
        {

            Session["GridViewData_Reg"] = dt;
            Session["Reg_Count"] = dt.Rows.Count;
            bool dataf = false;
            int dd = 0;
            GvReg.DataSource = dt;
            GvReg.DataBind();
            for (int i = 0; i < GvReg.Rows.Count; i++)
            {
                DropDownList Attendance = (DropDownList)GvReg.Rows[i].FindControl("ddlAttendance");
                Label presetndata = (Label)GvReg.Rows[i].FindControl("lblPresent");
                Label lblUniqueChildCode = (Label)GvReg.Rows[i].FindControl("lblUniqueChildRCode");
                LinkButton LinkButton = (LinkButton)GvReg.Rows[i].FindControl("lbtn1");
                if (dtLiff.Rows.Count > 0)
                {
                    DataRow[] dr = dtLiff.Select("UniqueChildRCode='" + lblUniqueChildCode.Text + "'");
                    if (dr.Length > 0)
                    {
                        if (Convert.ToInt32(dr[0]["Present"]) == 1)
                        {
                            Attendance.SelectedValue = "1";
                            LinkButton.Visible = true;
                        }
                        if (Convert.ToInt32(dr[0]["Present"]) == 2)
                        {
                            Attendance.SelectedValue = "2";
                            LinkButton.Visible = false;
                        }

                        dataf = true;
                        dd = Convert.ToInt32(dr[0]["session"]);
                        chkSession1.Checked = true;
                    }
                    else
                    {
                        Attendance.SelectedValue = "0";
                    }

                }

                else
                {
                    Attendance.SelectedValue = "0";
                }
            }
            if (dataf == true)
            {
                ddlsession.SelectedValue = Convert.ToString(dd);
            }
            else
            {
                ddlsession.SelectedIndex = -1;
            }
        }
        else
        {
            Session["GridViewData_Reg"] = null;
            GvReg.DataSource = null;
            GvReg.DataBind();
        }
    }

    protected void chkSession2_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chkSession2.Checked == true)
        {

            Imgaddclass.Enabled = true;
        }
    }
    //protected void GvReg_Onrowcommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {


    //        int index = 0;
    //        GridViewRow row;
    //        GridView grid = sender as GridView;

    //        if (e.CommandName == "Edit1")
    //        {

    //            index = Convert.ToInt32(e.CommandArgument);
    //            row = grid.Rows[index];


    //            string UniqueChildRCode = grid.DataKeys[index]["UniqueChildRCode"].ToString();
    //            childatabind(UniqueChildRCode);
    //            ModalAddclass.Show();
    //        }

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }
    //}

    protected void GvReg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GvReg.PageIndex = e.NewPageIndex;
        if (Session["GridViewData_Reg"] != null)
        {
            DataTable dt = Session["GridViewData_Reg"] as DataTable;
            GvReg.DataSource = dt;
            GvReg.DataBind();
        }


    }



    #endregion
    //public void childatabind(string UniqueChildRCode)
    //{
    //    string whr = " where UniqueChildRCode = '" + UniqueChildRCode + "' ";

    //    DataTable dt = new DataTable();
    //    dt = objMain.DatabindReg(whr, 1);

    //    if (dt.Rows.Count > 0)
    //    {
    //        if (Convert.ToString(dt.Rows[0]["Registrationdate"]) != "")
    //        {
    //            txtRegistration.Text = Convert.ToString(dt.Rows[0]["Registrationdate"]);
    //            hdnUniqueChildRCode.Value = Convert.ToString(dt.Rows[0]["Registrationdate"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["DOBAvailable"]) != "")
    //        {
    //            ddldobavail.SelectedValue = Convert.ToString(dt.Rows[0]["DOBAvailable"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["DOB"]) != "")
    //        {
    //            txtDOB.Text = Convert.ToString(dt.Rows[0]["DOB"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["Age"]) != "")
    //        {
    //            txtage.Text = Convert.ToString(dt.Rows[0]["Age"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["ChildName"]) != "")
    //        {
    //            txtGirlChildName.Text = Convert.ToString(dt.Rows[0]["ChildName"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["Category"]) != "")
    //        {
    //            ddlCategory.SelectedValue = Convert.ToString(dt.Rows[0]["Category"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["FatherName"]) != "")
    //        {
    //            txtFathername.Text = Convert.ToString(dt.Rows[0]["FatherName"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["Class"]) != "")
    //        {
    //            ddlClass.SelectedValue = Convert.ToString(dt.Rows[0]["Class"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["MobileNo"]) != "")
    //        {
    //            txtParentMobileNumber.Text = Convert.ToString(dt.Rows[0]["MobileNo"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["SRnumber"]) != "")
    //        {
    //            txtSRNumber.Text = Convert.ToString(dt.Rows[0]["SRnumber"]);
    //        }
    //        if (Convert.ToString(dt.Rows[0]["Gender"]) != "")
    //        {
    //            ddlgender.SelectedValue = Convert.ToString(dt.Rows[0]["Gender"]);
    //        }
    //    }
    //}



    //protected void ddldob_selectedindexchanged(object sender, EventArgs e)
    //{
    //    if (ddldobavail.SelectedValue == "1")
    //    {
    //        txtDOB.Enabled = true;
    //        txtage.Visible = false;
    //        txtDOB.Visible = true;
    //        lblDOB.Visible = true;
    //        lblage.Visible = false;
    //    }
    //    else if (ddldobavail.SelectedValue == "2")
    //    {
    //        lblage.Visible = true;
    //        txtage.Visible = true;
    //        txtDOB.Visible = false;
    //        lblDOB.Visible = false;
    //        txtDOB.Enabled = false;
    //    }
    //    ModalAddclass.Show();
    //}


    protected void Group1_CheckedChanged(Object sender, EventArgs e)
    {
        //if (rblPossiblie.Checked)
        //{
        //    pnlBalTest.Visible = true;
        //    pnlBalTest1.Visible = false;
        //}

        //if (rblIMPossiblie.Checked)
        //{
        //    pnlBalTest1.Visible = true;
        //    pnlBalTest.Visible = false;
        //}
    }
    protected void GroupLiff_CheckedChanged(Object sender, EventArgs e)
    {
        int Icount = 0;
        if (chkGame1.Checked)
        {
            Icount += 1;
        }
        if (chkGame2.Checked)
        {
            Icount += 1;
        }
        if (chkGame3.Checked)
        {
            Icount += 1;
        }
        if (chkGame4.Checked)
        {
            Icount += 1;
        }
        if (chkGame5.Checked)
        {
            Icount += 1;
        }
        if (Icount > 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please select only one game');", true);
            chkGame5.Checked = false;
            chkGame4.Checked = false;
            chkGame3.Checked = false;
            chkGame2.Checked = false;
            chkGame1.Checked = false;
        }
    }

    protected void GKPDelete_OnClick(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;


        SqlParameter[] parm = new SqlParameter[]
            {

              new SqlParameter("@uniquid",UniqueChildCode)

            };

        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteGKp", parm);


        if (result > 0)
        {
            LoadData();
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }


    }

    protected void btnD2dSerach_Click(object sender, EventArgs e)
    {
        if (this.ddlSearch.SelectedIndex > 0)
        {
            DataTable dataTable = this.Session["D2dBind"] as DataTable;
            if (Convert.ToInt32(this.ddlSearch.SelectedValue) == 1)
            {
                string str = "UniqueIdNew";
                DataTable dataTable2 = dataTable.Copy();
                string rowFilter = str + " like '%" + this.txtSearch.Text.Trim() + "%'   ";
                dataTable2.DefaultView.RowFilter = rowFilter;
                dataTable2.DefaultView.Sort = "UniqueIdNew asc";
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


    public void btnEdit_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        dropdownbind();
    }
    public void LoadData(string ClusterName)
    {

        string fromDate = txtDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string strQry = "";
        string UserName = "";

        string strQry2 = "";
        strQry2 += " select distinct UserID from tblActivityUpdate_School  with(nolock)  ";
        strQry2 += " inner join mst5village on mst5village.villagecode=tblActivityUpdate_School.villagecode  ";
        strQry2 += " where ActivityDate =('" + afromDate + "')  and  ";
        strQry2 += " mst5village.ClusterCode   = '" + Session["Cluseter"].ToString() + "'";

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
        strQry = "Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and VillageCode   = '" + Session["Cluseter"].ToString() + "'   ";

        strQry += "union  ";
        strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and UserName in(" + UserName + ")";

        DataTable dtUser = objMain.LoadData(strQry);
        objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        //strQry += " Select  distinct UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName]  from MstUser  where UserLevel=24 and UserName in(  ";
        //strQry += " select UserID from Tbl_GKP  ";
        //strQry += " inner join mst5village on mst5village.villagecode=Tbl_GKP.villagecode  ";
        //strQry += " where ActivityDate =('" + afromDate + "') )   ";
        ////strQry += " and mst5village.ClusterCode   = '" + Session["Cluseter"].ToString() + "' )    ";


        //DataTable dtUser3 = objMain.LoadData(strQry);
        //objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser3, conditions, "", "", ddlUser, "UserName", "UserId", "Select");


        objComman.BindDLL("mstSubject", "SubjectID, SubjectName", conditions, "SubjectID", "asc", ddlSubject, "SubjectName", "SubjectID", "Select");
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //  btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");


        Response.Redirect("~/FrmActivityDatewiseSearch.aspx?ID=" + Session["CluseterName"].ToString() + "," + Session["FromData"].ToString() + "," + Session["Todate"].ToString() + "");


    }
    protected void btnSmc_Click(object sender, EventArgs e)
    {
        chkSMC.Checked = false;
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;
        txtOtherSIPFC.Text = "";
        txtsmcmeetinFC.Text = "";
        foreach (ListItem item in CBL_bookformat.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_bookformat1.Items) { item.Selected = false; }
        chkNewSmc.Checked = false;
        rblSmcNew.Checked = false;
        rblSmcNew1.Checked = false;
        rdPSMCPY.Checked = false;
        rdPSMCPN.Checked = false;
        rdRegisterY.Checked = false;
        rdRegisterN.Checked = false;
        txtTotalMember.Text = "";
        txtTotalFmember.Text = "";
        txt_pbname.Text = "";
        txt_pbname1.Text = "";
        trGssId.Visible = false;
        tre1.Visible = false;
        rdTeamY.Checked = false;
        rdTeamN.Checked = false;
        ddlrec.SelectedIndex = 0;
        ddlDatemeeting.SelectedIndex = 0;
        ddlWrite.SelectedIndex = 0;
        ddlF5.SelectedIndex = 0;
        txtmembers.Text = "";
        lblCom22.Text = "";
        lblTottal.Text = "";
        lblFemale.Text = "";
        lblmale.Text = "";
        Session["dtmc"] = null;
        gvSmc.DataSource = null;
        gvSmc.DataBind();
    }

    protected void btnCLT_Click(object sender, EventArgs e)
    {


        //chkHindiA.Checked = false;
        //chkEnglishA.Checked = false;
        //chkMathA.Checked = false;


        //chkHindiB.Checked = false;
        //chkEnglishB.Checked = false;
        //chkMathB.Checked = false;

        //chkHindiC.Checked = false;
        //chkEnglishC.Checked = false;
        //chkMathC.Checked = false;

        //chkHindiD.Checked = false;
        //chkEnglishD.Checked = false;
        //chkMathD.Checked = false;


        //chkHindiE.Checked = false;
        //chkEnglishE.Checked = false;
        //chkMathE.Checked = false;
        if (rblCompletePre.Checked == true)
        {
            SqlParameter[] parm1 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),


               new SqlParameter("@ActivityDate",Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")),
                new SqlParameter("@Flag","2"),

                 };
            DataTable dtActivtyPreTest = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckBalSabaChat]", parm1);


            if (dtActivtyPreTest.Rows.Count > 0)
            {
                return;
            }
        }

        SqlParameter[] parm11 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),


               new SqlParameter("@ActivityDate",Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")),
                new SqlParameter("@Flag","6"),

                 };
        DataTable dtActivtyGk = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckBalSabaChat]", parm11);


        if (dtActivtyGk.Rows.Count > 0)
        {
            Int32 icount = Convert.ToInt32(dtActivtyGk.Rows[0]["SACupdate"].ToString());
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please delete GKP Frist')</script>", false);


                return;
            }
        }


        if (rblCompleteMid.Checked == true)
        {
            SqlParameter[] parm1 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),


               new SqlParameter("@ActivityDate",Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")),
                new SqlParameter("@Flag","3"),

                 };
            DataTable dtActivtyPreTest = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckBalSabaChat]", parm1);


            if (dtActivtyPreTest.Rows.Count > 0)
            {
                return;
            }
        }

        chkClT.Checked = false;
        rblCLTTB.Checked = false;
        rblCLTFC.Checked = false;

        rblTestTBPre.Checked = false;
        rblTestTBMid.Checked = false;
        rblTestTBPost.Checked = false;

        rblTestpreFC.Checked = false;
        rblTestMidFC.Checked = false;
        rblTestPostFC.Checked = false;

        rblPartialPre.Checked = false;
        rblPartialMid.Checked = false;
        rblPartialPost.Checked = false;



        rblCompletePre.Checked = false;
        rblCompleteMid.Checked = false;
        rblCompletePost.Checked = false;


    }
    protected void btnContactSchool(object sender, EventArgs e)
    {
        rblConTB.Checked = false;
        rblConFC.Checked = false;
        rbloption1.Checked = false;
        rbloption2.Checked = false;
        foreach (ListItem item in chkSchoolCOntact.Items) { item.Selected = false; }
    }
    protected void btnBalSab_Click(object sender, EventArgs e)
    {

        SqlParameter[] parm5 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")),
              new SqlParameter("@Flag",  "5"),

                 };


        DataTable dtSACKidd = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckBalSabaChat]", parm5);
        if (dtSACKidd.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please delete LiffSkill Frist')</script>", false);

            return;
        }
        rblBalsabaTB.Checked = false;
        rblBalsabaFC.Checked = false;
        chkBalSabhaFor.Checked = false;
        chkOrientation.Checked = false;
        chkChat.Checked = false;
        chkBalsabha.Checked = false;
        rblIMPossiblie.Checked = false;
        rblPossiblie.Checked = false;
        chkKit.Checked = false;
        chkSession1.Checked = false;
        chkSession2.Checked = false;
    }

    protected void btnLife(object sender, EventArgs e)
    {



        chklife.Checked = false;
        rblLifeTB.Checked = false;
        rblLifeFC.Checked = false;
        chkGame1.Checked = false;
        chkGame2.Checked = false;
        chkGame3.Checked = false;
        chkGame4.Checked = false;
        chkGame5.Checked = false;
        ddlsession.SelectedIndex = 0;
        GvReg.DataSource = null;
        GvReg.DataBind();

    }

    protected void btnSacUpdate_Click(object sender, EventArgs e)
    {
        chkSACUpdate.Checked = false;
        rblSacTB.Checked = false;
        rblSacFB.Checked = false;
        txtSMCMeeting.Text = "";

        txtHealth.Text = "";

        txtAdgirls.Text = "";

        txtAdBoy.Text = "";

        txtleftGirl.Text = "";

        txtleftBoy.Text = "";

        txtGirlNot.Text = "";

        txtGirlNot.Text = "";
        txtBoyNot.Text = "";
        txtPreSMCMeeting.Text = "";
        txtSMCMeeting.Text = "";
        txtSepSMCMeeting.Text = "";
        txtDescSMCMeeting.Text = "";
        txtMarSMCMeeting.Text = "";
        txtPrvHealth.Text = "";
        txtHealth.Text = "";
        txtSepHealth.Text = "";
        txtDescHealth.Text = "";
        txtMarHealth.Text = "";
        txtPreAdgirls.Text = "";
        txtAdgirls.Text = "";
        txtsepAdgirls.Text = "";
        txtDescAdgirls.Text = "";
        txtMarAdgirls.Text = "";
        txtPrvAdBoy.Text = "";
        txtAdBoy.Text = "";
        txtSepAdBoy.Text = "";
        txtDescAdBoy.Text = "";
        txtMarAdBoy.Text = "";
        txtPrvleftGirl.Text = "";
        txtleftGirl.Text = "";
        txtSepleftGirl.Text = "";
        txtDescleftGirl.Text = "";
        txtMarleftGirl.Text = "";
        txtPrevleftBoy.Text = "";
        txtleftBoy.Text = "";
        txtSepleftBoy.Text = "";
        txtdescleftBoy.Text = "";
        txtMarleftBoy.Text = "";
        txtPrvGirlNot.Text = "";
        txtGirlNot.Text = "";
        txtSepGirlNot.Text = "";
        txtDescGirlNot.Text = "";
        txtMarGirlNot.Text = "";
        txtprvBoyNot.Text = "";
        txtBoyNot.Text = "";
        txtSepBoyNot.Text = "";
        txtDecBoyNot.Text = "";
        txtMarBoyNot.Text = "";

    }
    protected void btninfrastructure_Click(object sender, EventArgs e)
    {
        lbldriking.Text = "0";
        lblToilet.Text = "0";
        lblElectricity.Text = "0";
        lblCltKit.Text = "0";
        lblbook.Text = "0";
        lblKitchen.Text = "0";
        lblBoundaryWall.Text = "0";
        lblSlides.Text = "0";
        lblPlay.Text = "0";
        txtClassRoom.Text = "";
        txtMaleTeacher.Text = "";
        txtFemaleTeacher.Text = "";
        txtToilet.BackColor = Color.White;
        txtdrinking.BackColor = Color.White;

        txtElectricity.BackColor = Color.White;
        txtbook.BackColor = Color.White;
        txtPlay.BackColor = Color.White;
        txtSlides.BackColor = Color.White;
        txtBoundaryWall.BackColor = Color.White;
        txtKitchen.BackColor = Color.White;
        txtCltKit.BackColor = Color.White;

        chkPhysical.Checked = false;
        rblPhysicalTB.Checked = false;
        rblPhysicalFC.Checked = false;

        txtBoysToilet.BackColor = Color.White;
        TextTapWater.BackColor = Color.White;

        TxtTiling.BackColor = Color.White;
        txtHandicapped.BackColor = Color.White;
        txtMultipleHandwashing.BackColor = Color.White;
        txtTilingclassroom.BackColor = Color.White;
        txtBlackboards.BackColor = Color.White;
        txtProperpainting.BackColor = Color.White;

        txtDisabledaccessible.BackColor = Color.White;
        txtAppropriateelectrical.BackColor = Color.White;
        txtBoysUrinal.BackColor = Color.White;
        txtGirlsUrinal.BackColor = Color.White;
        txtFurniture.BackColor = Color.White;
        txtWaterStorage.BackColor = Color.White;

        lblBoysToilet.Text = "0";
        lblWaterSupply.Text = "0";
        lblTilingToilet.Text = "0";
        lblHandicappedAccessibleToilet.Text = "0";
        lblMultipleHandwashingUnit.Text = "0";
        lblTilingClassroomFloor.Text = "0";
        lblBlackboards.Text = "0";
        lblProperPainting.Text = "0";
        lblDisabledAccessibleRamp.Text = "0";
        lblAppropriateElectricalWiring.Text = "0";
        lblBoysUrinal.Text = "0";
        lblGirlsUrinal.Text = "0";
        lblFurniture.Text = "0";
        lblTapWaterFacility.Text = "0";
    }


    protected void btnAnnual_Click(object sender, EventArgs e)
    {
        chkAnnual.Checked = false;
        chkSIPAnnaul.Checked = false;
        chkRetention.Checked = false;

        chkSIPTB.Checked = false;
        chkRenTB.Checked = false;
        chkSIPFC.Checked = false;

        chkRenFC.Checked = false;
        chkSipPartial.Checked = false;
        chkRenPartial.Checked = false;


        chkSipComplete.Checked = false;
        chkComplete.Checked = false;

    }
    private Boolean Validation()
    {
        try
        {
            #region Main
            if (ddlUser.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
                return false;
            }
            if (ddlVilage.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Village')</script>", false);
                return false;
            }
            if (txtDate.Text == "")
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
                return false;
            }
            if (ddlSchool.SelectedIndex <= 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);
                return false;
            }
            if (txtOther.Text != "")
            {
                if (rblothertb.Checked == true || rblotherfc.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Other TB or FC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
            }
            #endregion

            #region SMC
            string commmeeting = "";
            string commmeeting1 = "";
            foreach (ListItem item in CBL_bookformat.Items)
            {
                if (item.Selected)
                {

                    commmeeting += "" + item.Value + "" + ",";


                }
            }
            string ggg = "";
            foreach (ListItem item in CBL_bookformat1.Items)
            {
                if (item.Selected)
                {

                    commmeeting1 += "" + item.Value + "" + ",";

                    if (item.Text == "others (specify)")
                    {
                        ggg = item.Text;
                        TxtSmcOther.Enabled = true;
                    }

                }
            }
            if (chkSMC.Checked == true || commmeeting.Length > 0 || commmeeting1.Length > 0 || rblSMCTB.Checked == true || rblSMCFC.Checked == true || rdRegisterY.Checked == true || rdRegisterN.Checked == true || txtTotalMember.Text != "" || txtTotalFmember.Text != "")
            {
                if (ddlMeetingPrepare.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select who prepared the meeting agenda')</script>", false);
                    return false;
                }

                if (chkSMC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC ')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC TB or FC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (rblSMCTB.Checked == true)
                {
                    if (ddlGssTbname.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB Name')</script>", false);
                        return false;
                    }
                }

                if (rdPSMCPY.Checked == false && rdPSMCPN.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Presence of SMC President')</script>", false);
                    this.rdPSMCPY.Focus();
                    return false;
                }
                if (rdRegisterY.Checked == false && rdRegisterN.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Is SMC register available in the school?')</script>", false);
                    this.rdRegisterY.Focus();
                    return false;
                }
                if (rdTeamY.Checked == false && rdTeamN.Checked == false && rblSMCFC.Checked == true)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Is Team Baika available in meeting')</script>", false);
                    return false;

                }
                if (rdTeamY.Checked == true && rblSMCTB.Checked == false)
                {
                    if (ddlMMTb.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB Name')</script>", false);
                        return false;
                    }
                }
                if (commmeeting.Length > 0)
                {
                    commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(","));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select   Objective off Educate Girls Meeting')</script>", false);
                    this.CBL_bookformat.Focus();
                    return false;
                }

                if (commmeeting1.Length > 0)
                {
                    commmeeting1 = commmeeting1.Substring(0, commmeeting1.LastIndexOf(","));
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Main Discussion point')</script>", false);
                    this.CBL_bookformat1.Focus();
                    return false;
                }
                if (ggg.Length > 1)
                {

                    if (TxtSmcOther.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select   Other Main Discussion point ')</script>", false);
                        this.TxtSmcOther.Focus();
                        TxtSmcOther.Enabled = true;
                        return false;
                    }
                }

                if (txtOtherSIPFC.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  Critical SIP prepared')</script>", false);
                    this.txtOtherSIPFC.Focus();
                    return false;
                }
                if (txtsmcmeetinFC.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter   Other Critical SIP prepared')</script>", false);
                    this.txtsmcmeetinFC.Focus();
                    return false;
                }




                if (rdTeamY.Checked == true && rblSMCTB.Checked == false)
                {
                    if (ddlMMTb.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB')</script>", false);
                        return false;
                    }
                }
                if (ddlrec.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select kind of SMC register')</script>", false);
                    return false;
                }
                if (ddlDatemeeting.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select entered in the meeting register')</script>", false);
                    return false;
                }
                if (ddlWrite.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select entered in the Form-05')</script>", false);
                    return false;
                }
                if (ddlF5.SelectedIndex <= 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select entered in the Form-05')</script>", false);
                    return false;
                }
                if (txtmembers.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  physically  Members')</script>", false);
                    this.txtOtherSIPFC.Focus();
                    return false;
                }

                if (Convert.ToInt32(txtmembers.Text) >= 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter physically Members Greater then Zero')</script>", false);
                    this.txtOtherSIPFC.Focus();
                    return false;
                }
                //if (txtTotalMember.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total SMC Members')</script>", false);
                //    this.txtOtherSIPFC.Focus();
                //    return false;
                //}
                //if (txtTotalFmember.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Female Members')</script>", false);
                //    this.txtsmcmeetinFC.Focus();
                //    return false;
                //}


                //Int32 SIP=Convert.ToInt32(

                if (gvSmc.Rows.Count > 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please add attendence')</script>", false);
                    this.txtTotalFmember.Focus();
                    return false;
                }
                int Ipre = 0;
                if (gvSmc.Rows.Count > 0)
                {
                    if (Convert.ToInt32(txtmembers.Text) <= gvSmc.Rows.Count)
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Minimum " + txtmembers.Text + " Members can be Entered')</script>", false);
                        this.txtOtherSIPFC.Focus();
                        return false;
                    }
                    for (int i = 0; i < gvSmc.Rows.Count; i++)
                    {
                        CheckBox Attendance = (CheckBox)gvSmc.Rows[i].FindControl("ddlAttendanceSmc");



                        if (Attendance.Checked == true)
                        {
                            Ipre = Ipre + 1;
                        }
                    }
                }

                if (Convert.ToInt32(txtmembers.Text) <= Ipre)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Minimum " + txtmembers.Text + "  Present Attendation')</script>", false);
                    this.txtOtherSIPFC.Focus();
                    return false;
                }

                if (Convert.ToString(Session["SchoolLevel"]) == "1" && Convert.ToInt32(Ipre) > 18)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Entry allowed less then or equal to 18 for PS ')</script>", false);
                    this.txtTotalMember.Focus();
                    return false;
                }


                if ((Session["SchoolLevel"].ToString() == "2" || Session["SchoolLevel"].ToString() == "5") && Convert.ToInt32(Ipre) > 16)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Entry allowed less then or equal to 16 for PS and KGBV')</script>", false);
                    this.txtTotalMember.Focus();
                    return false;
                }

                //if (Convert.ToInt32(txtTotalMember.Text) >= 6)
                //{
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Entry allowed greater then 6')</script>", false);
                //    this.txtTotalMember.Focus();
                //    return false;
                //}
                //if (Convert.ToInt32(txtTotalMember.Text) > Convert.ToInt32(txtTotalFmember.Text))
                //{
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Total Female member always less then Total Members')</script>", false);
                //    this.txtTotalFmember.Focus();
                //    return false;

                //}


            }
            #endregion

            #region SMC Orientation

            //if (chkSMC.Checked == true)
            //{
            //    if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
            //    {

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Orientation TB or FC')</script>", false);
            //        this.chkSMC.Focus();
            //        return false;
            //    }
            //    if (txtTotalMember.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total Trained Member')</script>", false);
            //        this.txtTotalMember.Focus();
            //        return false;
            //    }
            //    if (txtTotalFmember.Text == "")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter Total Trained Female Member')</script>", false);
            //        this.txtTotalFmember.Focus();
            //        return false;
            //    }
            //    if (Convert.ToInt32(txtTotalMember.Text) < 6 || Convert.ToInt32(txtTotalMember.Text) > 16)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please ensure that Total Trained  Member number should be greater than 6 and less then 16')</script>", false);
            //        this.txtTotalFmember.Focus();
            //    }

            //    //Int32 TotoSip = Convert.ToInt32(txtOtherSIPFC.Text) + Convert.ToInt32(txtsmcmeetinFC.Text);
            //    //if (TotoSip <= 0)
            //    //{
            //    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  SIP prepared or completed Value')</script>", false);
            //    //    this.txtTotalFmember.Focus();
            //    //    return false;
            //    //}
            //    Int32 Toto = Convert.ToInt32(txtTotalMember.Text) + Convert.ToInt32(txtTotalFmember.Text);
            //    if (Toto <= 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter  Trained Female or Male Member Value')</script>", false);
            //        this.txtTotalFmember.Focus();
            //        return false;
            //    }
            //}


            //if (txtTotalMember.Text != "" || txtTotalFmember.Text != "")
            //{
            //    if (rblSMCTB.Checked == true || rblSMCFC.Checked == true)
            //    {

            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Orientation TB or FC')</script>", false);
            //        this.chkSMC.Focus();
            //        return false;
            //    }
            //    if (chkSMC.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC Orientation')</script>", false);
            //        this.chkSMC.Focus();
            //        return false;
            //    }
            //}


            #endregion

            #region Balsabha
            Int32 BalsabaTB = 0;
            Int32 BalsabFC = 0;
            if (rblBalsabaTB.Checked == true)
            {
                BalsabaTB = 1;
            }
            if (rblBalsabaFC.Checked == true)
            {
                BalsabFC = 1;
            }
            Int32 BalSabha_Formation = 0;


            //if (chkBalSabhaFor.Checked == true)
            //{
            //    BalSabha_Formation = 1;
            //}
            //if (chkOrientation.Enabled == true)
            //{
            //    if (chkOrientation.Checked == true)
            //    {
            //        BalSabha_Formation = 1;
            //    }
            //}
            //if (chkChat.Enabled == true)
            //{
            //    if (chkChat.Checked == true)
            //    {
            //        BalSabha_Formation = 1;
            //    }
            //}
            //if (chkKit.Checked == true)
            //{
            //    BalSabha_Formation = 1;
            //}

            //if (rblPossiblie.Enabled == true)
            //{
            //    if (rblPossiblie.Checked == true)
            //    {
            //        if (BalSabha_Formation == 1)
            //        {
            //            //if (chkBalsabha.Checked == false)
            //            //{

            //            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Balsabha')</script>", false);
            //            //    this.chkSMC.Focus();
            //            //    return false;
            //            //}
            //            if (BalsabaTB == 0 && BalsabFC == 0)
            //            {
            //                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Balsabha')</script>", false);



            //                this.chkSMC.Focus();
            //                return false;
            //            }
            //        }
            //        if (chkBalsabha.Checked == true)
            //        {
            //            if (BalSabha_Formation == 0)
            //            {
            //                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Any one Balsabha')</script>", false);



            //                this.chkSMC.Focus();
            //                return false;
            //            }
            //            if (BalsabaTB == 0 && BalsabFC == 0)
            //            {
            //                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Balsabha')</script>", false);



            //                this.chkSMC.Focus();
            //                return false;
            //            }
            //        }

            //        //if (BalSabha_Formation == 0)
            //        //{
            //        //    if (chkBalsabha.Checked == true)
            //        //    {

            //        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Any One Balsabha')</script>", false);
            //        //        this.chkSMC.Focus();
            //        //        return false;
            //        //    }

            //        //}
            //    }
            //}


            //if (rblIMPossiblie.Enabled == true)
            //{
            //    if (rblIMPossiblie.Checked == true)
            //    {
            //        if (BalsabaTB == 0 && BalsabFC == 0)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Balsabha')</script>", false);



            //            this.chkSMC.Focus();
            //            return false;
            //        }
            //        //if (chkBalsabha.Checked == false)
            //        //{

            //        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Balsabha')</script>", false);
            //        //    this.chkSMC.Focus();
            //        //    return false;
            //        //}
            //        if (ddlreasons.SelectedIndex <= 0)
            //        {

            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Reason')</script>", false);
            //            this.chkSMC.Focus();
            //            return false;
            //        }
            //    }
            //}
            //Int32 LifeTB = 0;
            //Int32 LifeFC = 0;
            //if (rblLifeTB.Checked == true)
            //{
            //    LifeTB = 1;
            //}
            //if (rblLifeFC.Checked == true)
            //{
            //    LifeFC = 1;
            //}
            //#endregion

            //#region Game

            //Int32 Game_TB = 0;
            //Int32 Game_FC = 0;
            //if (rblLifeTB.Checked == true)
            //{
            //    Game_TB = 1;
            //}
            //if (rblLifeFC.Checked == true)
            //{
            //    Game_FC = 2;
            //}

            //int Game = 0;
            //if (chkGame1.Checked == true)
            //{
            //    Game = 1;
            //}
            //if (chkGame2.Checked == true)
            //{
            //    Game = 1;
            //}
            //if (chkGame3.Checked == true)
            //{
            //    Game = 1;
            //}
            //if (chkGame4.Checked == true)
            //{
            //    Game = 1;
            //}

            //if (chkGame5.Checked == true)
            //{
            //    Game = 1;
            //}

            ////if (Game == 1)
            ////{
            ////if (chklife.Checked == false)
            ////{

            ////    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Life Skill')</script>", false);
            ////    this.chkSMC.Focus();
            ////    return false;
            ////}
            ////if (LifeTB == 0 && LifeFC == 0)
            ////{
            ////    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Life Skill')</script>", false);



            ////    this.chkSMC.Focus();
            ////    return false;
            ////}


            ////if (chklife.Checked == true)
            ////{
            ////    //if (Game == 0)
            ////    //{

            ////    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Any One Life Skill')</script>", false);
            ////    //    this.chkSMC.Focus();
            ////    //    return false;
            ////    //}
            ////    //if (LifeTB == 0 && LifeFC == 0)
            ////    //{
            ////    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Life Skill')</script>", false);



            ////    //    this.chkSMC.Focus();
            ////    //    return false;
            ////    //}
            ////}

            #endregion

            #region SAC Update
            Int32 SACTB = 0;
            Int32 SACFC = 0;
            if (rblSacTB.Checked == true)
            {
                SACTB = 1;
            }
            if (rblSacFB.Checked == true)
            {
                SACFC = 1;
            }

            int SAC_No_Of_Attended = 0;



            string Dateof = txtDate.Text;
            string[] b = Dateof.Split('/');

            string FcDate = b[2] + '-' + b[1] + '-' + b[0];

            int month = 0;
            if (txtDate.Text != "")
            {
                month = Convert.ToInt32(b[1]);
            }

            if (month == 7)
            {
                if (chkSACUpdate.Checked == true)
                {
                    if (txtSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter SMCMeeting')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }


                    if (txtSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtSMCMeeting.Text);
                    }



                    if (txtHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Regular Health Checkup')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtHealth.Text);
                    }


                    if (txtAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of girls')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtAdgirls.Text);
                    }
                    if (txtAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of boys')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSMCMeeting.Text);
                    }

                    if (txtleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Girls left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtleftGirl.Text);
                    }
                    if (txtleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Boys left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtleftBoy.Text);
                    }

                    if (txtGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Girls- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtGirlNot.Text);
                    }
                    if (txtBoyNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Boys- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtBoyNot.Text);
                    }
                }

                if (chkSACUpdate.Checked == false)
                {

                    if (txtSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {



                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtSMCMeeting.Text);
                    }



                    if (txtHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtHealth.Text);
                    }


                    if (txtAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtAdgirls.Text);
                    }
                    if (txtAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {


                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSMCMeeting.Text);
                    }

                    if (txtleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtleftGirl.Text);
                    }
                    if (txtleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtleftBoy.Text);
                    }

                    if (txtGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtGirlNot.Text);
                    }
                    if (txtBoyNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtBoyNot.Text);
                    }
                }
            }

            if (month == 10 || month == 11)
            {
                if (chkSACUpdate.Checked == true)
                {
                    if (txtSepSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter SMCMeeting')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }


                    if (txtSepSMCMeeting.Text.Trim() == "" && txtSepSMCMeeting.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtSepSMCMeeting.Text);
                    }



                    if (txtSepHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Regular Health Checkup')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepHealth.Text);
                    }


                    if (txtsepAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of girls')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtsepAdgirls.Text);
                    }
                    if (txtSepleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of boys')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepleftBoy.Text);
                    }

                    if (txtSepleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Girls left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepleftGirl.Text);
                    }
                    if (txtSepleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Boys left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepleftBoy.Text);
                    }

                    if (txtSepGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Girls- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepGirlNot.Text);
                    }
                    if (txtSepBoyNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Boys- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepBoyNot.Text);
                    }
                }

                if (chkSACUpdate.Checked == false)
                {

                    if (txtSepSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {



                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtSepSMCMeeting.Text);
                    }



                    if (txtSepHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepHealth.Text);
                    }


                    if (txtsepAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtsepAdgirls.Text);
                    }
                    if (txtSepAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {


                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepAdBoy.Text);
                    }

                    if (txtSepleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepleftGirl.Text);
                    }
                    if (txtSepleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepleftBoy.Text);
                    }

                    if (txtSepGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepGirlNot.Text);
                    }
                    if (txtSepBoyNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtSepBoyNot.Text);
                    }
                }
            }


            if (month == 1)
            {
                if (chkSACUpdate.Checked == true)
                {
                    if (txtDescSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter SMCMeeting')</script>", false);

                        this.txtDescSMCMeeting.Focus();
                        return false;
                    }


                    if (txtDescSMCMeeting.Text.Trim() == "" && txtDescSMCMeeting.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtDescSMCMeeting.Text);
                    }



                    if (txtDescHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Regular Health Checkup')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescHealth.Text);
                    }


                    if (txtDescAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of girls')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescAdgirls.Text);
                    }
                    if (txtDescAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of boys')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescAdgirls.Text);
                    }

                    if (txtDescleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Girls left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescleftGirl.Text);
                    }
                    if (txtdescleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Boys left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtdescleftBoy.Text);
                    }

                    if (txtDescGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Girls- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescGirlNot.Text);
                    }
                    if (txtdescleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Boys- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtdescleftBoy.Text);
                    }
                }

                if (chkSACUpdate.Checked == false)
                {

                    if (txtDescSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {



                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtDescSMCMeeting.Text);
                    }



                    if (txtDescHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescHealth.Text);
                    }


                    if (txtDescAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescAdgirls.Text);
                    }
                    if (txtDescAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {


                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescAdBoy.Text);
                    }

                    if (txtDescleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescleftGirl.Text);
                    }
                    if (txtdescleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtdescleftBoy.Text);
                    }

                    if (txtDescGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtDescGirlNot.Text);
                    }
                    if (txtdescleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtdescleftBoy.Text);
                    }
                }
            }


            if (month == 3)
            {
                if (chkSACUpdate.Checked == true)
                {
                    if (txtMarSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter SMCMeeting')</script>", false);

                        this.txtMarSMCMeeting.Focus();
                        return false;
                    }


                    if (txtMarSMCMeeting.Text.Trim() == "" && txtMarSMCMeeting.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtMarSMCMeeting.Text);
                    }



                    if (txtMarHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Regular Health Checkup')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarHealth.Text);
                    }


                    if (txtMarAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of girls')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarAdgirls.Text);
                    }
                    if (txtMarAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Admission of boys')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarAdgirls.Text);
                    }

                    if (txtMarleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Girls left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarleftGirl.Text);
                    }
                    if (txtMarleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('# Boys left school')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarleftBoy.Text);
                    }

                    if (txtMarGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Girls- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarGirlNot.Text);
                    }
                    if (txtMarleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Boys- Who needs to be Regularized')</script>", false);

                        this.txtSMCMeeting.Focus();
                        return false;
                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarleftBoy.Text);
                    }
                }

                if (chkSACUpdate.Checked == false)
                {

                    if (txtMarSMCMeeting.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {



                    }
                    else
                    {
                        SAC_No_Of_Attended = Convert.ToInt32(txtMarSMCMeeting.Text);
                    }



                    if (txtMarHealth.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarHealth.Text);
                    }


                    if (txtMarAdgirls.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarAdgirls.Text);
                    }
                    if (txtMarAdBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {


                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarAdBoy.Text);
                    }

                    if (txtMarleftGirl.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarleftGirl.Text);
                    }
                    if (txtMarleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarleftBoy.Text);
                    }

                    if (txtMarGirlNot.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }

                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarGirlNot.Text);
                    }
                    if (txtMarleftBoy.Text.Trim() == "" && pnlSACUpdate.Enabled == true)
                    {

                    }
                    else
                    {
                        SAC_No_Of_Attended += Convert.ToInt32(txtMarleftBoy.Text);
                    }
                }
            }

            if (SAC_No_Of_Attended > 0 && pnlSACUpdate.Enabled == true)
            {
                if (chkSACUpdate.Checked == false)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SAC')</script>", false);
                    this.chkSMC.Focus();
                    return false;
                }
                if (SACTB == 0 && SACFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB SAC')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            if (chkSACUpdate.Checked == true)
            {

                if (SACTB == 0 && SACFC == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB SAC')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }

            #endregion

            #region

            //if (rblPartialPre.Checked == true || rblCompletePre.Checked == true || rblTestTBPre.Checked == true || rblTestpreFC.Checked == true)
            //{
            //    if (rblTestTBPre.Checked == false && rblTestpreFC.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Baseline-Test')</script>", false);



            //        this.rblTestTBPre.Focus();
            //        return false;
            //    }
            //    if (rblPartialPre.Checked == false && rblCompletePre.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete Baseline-Test')</script>", false);



            //        this.rblPartialPre.Focus();
            //        return false;
            //    }
            //}
            //if (rblPartialMid.Checked == true || rblCompleteMid.Checked == true || rblTestTBMid.Checked == true || rblTestMidFC.Checked == true)
            //{
            //    if (rblTestTBMid.Checked == false && rblTestMidFC.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Midline-Test')</script>", false);



            //        this.rblTestTBMid.Focus();
            //        return false;
            //    }
            //    if (rblPartialMid.Checked == false && rblCompleteMid.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete Midline-Test')</script>", false);



            //        this.rblPartialMid.Focus();
            //        return false;
            //    }
            //}

            //if (rblPartialPost.Checked == true || rblCompletePost.Checked == true || rblTestTBPost.Checked == true || rblTestPostFC.Checked == true)
            //{
            //    if (rblTestTBPost.Checked == false && rblTestPostFC.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Endline-Test')</script>", false);



            //        this.rblTestPostFC.Focus();
            //        return false;
            //    }
            //    if (rblPartialPost.Checked == false && rblCompletePost.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete Endline-Test')</script>", false);



            //        this.rblCompletePost.Focus();
            //        return false;
            //    }
            //}

            //if (chkRetention.Checked == true)
            //{
            //    if (chkRenTB.Checked == false && chkRenFC.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB Retention')</script>", false);



            //        this.chkSMC.Focus();
            //        return false;
            //    }
            //    if (chkRenPartial.Checked == false && chkComplete.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete Retention')</script>", false);



            //        this.chkSMC.Focus();
            //        return false;
            //    }
            //}
            //if (chkRetention.Checked == false)
            //{
            //    if (chkRenTB.Checked == true || chkRenFC.Checked == true || chkRenPartial.Checked == true || chkComplete.Checked == true)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Retention')</script>", false);



            //        this.chkSMC.Focus();
            //        return false;
            //    }

            //}
            #endregion

            #region
            if (chkSIPAnnaul.Checked == true)
            {
                if (chkSIPTB.Checked == false && chkSIPFC.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select FC OR TB SIPAnnaul')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
                if (chkSipPartial.Checked == false && chkSipComplete.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Partial OR Complete SIPAnnaul')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }
            }
            if (chkSIPAnnaul.Checked == false)
            {
                if (chkSIPTB.Checked == true || chkSIPFC.Checked == true || chkSipPartial.Checked == true || chkSipComplete.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  SIPAnnaul')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }

            }

            //if (chkSession1.Checked == true || chkSession2.Checked == true)
            //{
            //    if (rblBalsabaTB.Checked == false && rblBalsabaFC.Checked == false)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select  Balsabha T.B. or F.C.')</script>", false);

            //        return false;
            //    }
            //    if (rblBalsabaTB.Checked==true)
            //    {
            //        if (ddlBalSabaTB.SelectedIndex <= 0)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB Name Balsabha')</script>", false);



            //            this.chkSMC.Focus();
            //            return false;
            //        }
            //    }

            //}

            #endregion

            #region SPF
            int Infrastructure = 0;
            int Classrooms = 0;
            int DrinkingWater = 0;
            int GirlsToilet = 0;
            int Electricity = 0;
            int Playground = 0;
            int Slide = 0;
            int BoundaryWall = 0;
            int Kitchen = 0;
            int Teachers_Male = 0;
            int Teachers_Female = 0;
            int CLT_Kit = 0;
            int bookAvl = 0;


            int BoysToilet = 0;
            int WaterSupply = 0;
            int TilingToilet = 0;
            int HandicappedAccessibleToilet = 0;
            int MultipleHandwashingUnit = 0;
            int TilingClassroomFloor = 0;
            int Blackboards = 0;
            int ProperPainting = 0;
            int DisabledAccessibleRamp = 0;
            int AppropriateElectricalWiring = 0;
            int BoysUrinal = 0;
            int GirlsUrinal = 0;
            int Furniture = 0;
            int TapWaterFacility = 0;
            int Infrastructure_FC = 0; int Infrastructure_TB = 0;

            if (txtClassRoom.Text != "")
            {
                Classrooms = Convert.ToInt32(txtClassRoom.Text);
            }
            if (lbldriking.Text == "1")
            {
                DrinkingWater = 1;
            }
            else if (lbldriking.Text == "2")
            {

                DrinkingWater = 2;
            }
            else if (lbldriking.Text == "3")
            {

                DrinkingWater = 3;
            }
            else if (lbldriking.Text == "4")
            {

                DrinkingWater = 4;
            }

            if (lblToilet.Text == "1")
            {
                GirlsToilet = 1;
            }
            else if (lblToilet.Text == "2")
            {

                GirlsToilet = 2;
            }
            else if (lblToilet.Text == "3")
            {

                GirlsToilet = 3;
            }
            else if (lblToilet.Text == "4")
            {

                GirlsToilet = 4;
            }

            if (lblElectricity.Text == "1")
            {
                Electricity = 1;
            }
            else if (lblElectricity.Text == "2")
            {

                Electricity = 2;
            }
            else if (lblElectricity.Text == "3")
            {

                Electricity = 3;
            }
            else if (lblElectricity.Text == "4")
            {

                Electricity = 4;
            }

            if (lblPlay.Text == "1")
            {
                Playground = 1;
            }
            else if (lblPlay.Text == "2")
            {

                Playground = 2;
            }
            else if (lblPlay.Text == "3")
            {

                Playground = 3;
            }
            else if (lblPlay.Text == "4")
            {

                Playground = 4;
            }


            if (lblPlay.Text == "1")
            {
                Slide = 1;
            }
            else if (lblSlides.Text == "2")
            {

                Slide = 2;
            }
            else if (lblSlides.Text == "3")
            {

                Slide = 3;
            }
            else if (lblSlides.Text == "4")
            {

                Slide = 4;
            }

            if (lblBoundaryWall.Text == "1")
            {
                BoundaryWall = 1;
            }
            else if (lblBoundaryWall.Text == "2")
            {

                BoundaryWall = 2;
            }
            else if (lblBoundaryWall.Text == "3")
            {

                BoundaryWall = 3;
            }
            else if (lblBoundaryWall.Text == "4")
            {

                BoundaryWall = 4;
            }


            if (lblSlides.Text == "1")
            {
                Slide = 1;
            }
            else if (lblSlides.Text == "2")
            {

                Slide = 2;
            }
            else if (lblSlides.Text == "3")
            {

                Slide = 3;
            }
            else if (lblSlides.Text == "4")
            {

                Slide = 4;
            }

            if (lblKitchen.Text == "1")
            {
                Kitchen = 1;
            }
            else if (lblKitchen.Text == "2")
            {

                Kitchen = 2;
            }
            else if (lblKitchen.Text == "3")
            {

                Kitchen = 3;
            }
            else if (lblKitchen.Text == "4")
            {

                Kitchen = 4;
            }
            if (lblCltKit.Text == "1")
            {
                CLT_Kit = 1;
            }
            else if (lblCltKit.Text == "2")
            {

                CLT_Kit = 2;
            }
            else if (lblCltKit.Text == "3")
            {

                CLT_Kit = 3;
            }
            else if (lblCltKit.Text == "4")
            {

                CLT_Kit = 4;
            }

            if (lblbook.Text == "1")
            {
                bookAvl = 1;
            }
            else if (lblbook.Text == "2")
            {

                bookAvl = 2;
            }
            else if (lblbook.Text == "3")
            {

                bookAvl = 3;
            }
            else if (lblbook.Text == "4")
            {

                bookAvl = 4;
            }
            if (lblBoysToilet.Text == "1")
            {
                BoysToilet = 1;
            }
            else if (lblBoysToilet.Text == "2")
            {

                BoysToilet = 2;
            }
            else if (lblBoysToilet.Text == "3")
            {

                BoysToilet = 3;
            }
            else if (lblBoysToilet.Text == "4")
            {

                BoysToilet = 4;
            }

            if (lblWaterSupply.Text == "1")
            {
                WaterSupply = 1;
            }
            else if (lblWaterSupply.Text == "2")
            {

                WaterSupply = 2;
            }
            else if (lblWaterSupply.Text == "3")
            {

                WaterSupply = 3;
            }
            else if (lblWaterSupply.Text == "4")
            {

                WaterSupply = 4;
            }

            if (lblTilingToilet.Text == "1")
            {
                TilingToilet = 1;
            }
            else if (lblTilingToilet.Text == "2")
            {

                TilingToilet = 2;
            }
            else if (lblTilingToilet.Text == "3")
            {

                TilingToilet = 3;
            }
            else if (lblTilingToilet.Text == "4")
            {

                TilingToilet = 4;
            }


            if (lblHandicappedAccessibleToilet.Text == "1")
            {
                HandicappedAccessibleToilet = 1;
            }
            else if (lblHandicappedAccessibleToilet.Text == "2")
            {

                HandicappedAccessibleToilet = 2;
            }
            else if (lblHandicappedAccessibleToilet.Text == "3")
            {

                HandicappedAccessibleToilet = 3;
            }
            else if (lblHandicappedAccessibleToilet.Text == "4")
            {

                HandicappedAccessibleToilet = 4;
            }

            if (lblMultipleHandwashingUnit.Text == "1")
            {
                MultipleHandwashingUnit = 1;
            }
            else if (lblMultipleHandwashingUnit.Text == "2")
            {
                MultipleHandwashingUnit = 2;
            }
            else if (lblMultipleHandwashingUnit.Text == "3")
            {
                MultipleHandwashingUnit = 3;
            }
            else if (lblMultipleHandwashingUnit.Text == "4")
            {
                MultipleHandwashingUnit = 4;
            }

            if (lblTilingClassroomFloor.Text == "1")
            {
                TilingClassroomFloor = 1;
            }
            else if (lblTilingClassroomFloor.Text == "2")
            {
                TilingClassroomFloor = 2;
            }
            else if (lblTilingClassroomFloor.Text == "3")
            {
                TilingClassroomFloor = 3;
            }
            else if (lblTilingClassroomFloor.Text == "4")
            {
                TilingClassroomFloor = 4;
            }


            if (lblBlackboards.Text == "1")
            {
                Blackboards = 1;
            }
            else if (lblBlackboards.Text == "2")
            {
                Blackboards = 2;
            }
            else if (lblBlackboards.Text == "3")
            {
                Blackboards = 3;
            }
            else if (lblBlackboards.Text == "4")
            {
                Blackboards = 4;
            }


            if (lblProperPainting.Text == "1")
            {
                ProperPainting = 1;
            }
            else if (lblProperPainting.Text == "2")
            {
                ProperPainting = 2;
            }
            else if (lblProperPainting.Text == "3")
            {
                ProperPainting = 3;
            }
            else if (lblProperPainting.Text == "4")
            {
                ProperPainting = 4;
            }



            if (lblDisabledAccessibleRamp.Text == "1")
            {
                DisabledAccessibleRamp = 1;
            }
            else if (lblDisabledAccessibleRamp.Text == "2")
            {
                DisabledAccessibleRamp = 2;
            }
            else if (lblDisabledAccessibleRamp.Text == "3")
            {
                DisabledAccessibleRamp = 3;
            }
            else if (lblDisabledAccessibleRamp.Text == "4")
            {
                DisabledAccessibleRamp = 4;
            }

            if (lblAppropriateElectricalWiring.Text == "1")
            {
                AppropriateElectricalWiring = 1;
            }
            else if (lblAppropriateElectricalWiring.Text == "2")
            {
                AppropriateElectricalWiring = 2;
            }
            else if (lblAppropriateElectricalWiring.Text == "3")
            {
                AppropriateElectricalWiring = 3;
            }
            else if (lblAppropriateElectricalWiring.Text == "4")
            {
                AppropriateElectricalWiring = 4;
            }

            if (lblBoysUrinal.Text == "1")
            {
                BoysUrinal = 1;
            }
            else if (lblBoysUrinal.Text == "2")
            {
                BoysUrinal = 2;
            }
            else if (lblBoysUrinal.Text == "3")
            {
                BoysUrinal = 3;
            }
            else if (lblBoysUrinal.Text == "4")
            {
                BoysUrinal = 4;
            }

            if (lblGirlsUrinal.Text == "1")
            {
                GirlsUrinal = 1;
            }
            else if (lblGirlsUrinal.Text == "2")
            {
                GirlsUrinal = 2;
            }
            else if (lblGirlsUrinal.Text == "3")
            {
                GirlsUrinal = 3;
            }
            else if (lblGirlsUrinal.Text == "4")
            {
                GirlsUrinal = 4;
            }

            if (lblFurniture.Text == "1")
            {
                Furniture = 1;
            }
            else if (lblFurniture.Text == "2")
            {
                Furniture = 2;
            }
            else if (lblFurniture.Text == "3")
            {
                Furniture = 3;
            }
            else if (lblFurniture.Text == "4")
            {
                Furniture = 4;
            }

            if (lblTapWaterFacility.Text == "1")
            {
                TapWaterFacility = 1;
            }
            else if (lblTapWaterFacility.Text == "2")
            {
                TapWaterFacility = 2;
            }
            else if (lblTapWaterFacility.Text == "3")
            {
                TapWaterFacility = 3;
            }
            else if (lblTapWaterFacility.Text == "4")
            {
                TapWaterFacility = 4;
            }


            if (chkPhysical.Checked == true)
            {
                if (Session["StateCode"].ToString() == "9A" || Session["StateCode"].ToString() == "9B" || Session["StateCode"].ToString() == "9C")

                {

                    if (Classrooms > 0 && DrinkingWater > 0 && GirlsToilet > 0 && Electricity > 0 && Playground > 0 && BoundaryWall > 0 && Kitchen > 0 && CLT_Kit > 0 && bookAvl > 0 && BoysToilet > 0 && WaterSupply > 0 && TilingToilet > 0 && HandicappedAccessibleToilet > 0 && MultipleHandwashingUnit > 0 && TilingClassroomFloor > 0 && Blackboards > 0 && ProperPainting > 0 && DisabledAccessibleRamp > 0 && AppropriateElectricalWiring > 0 && BoysUrinal > 0 && GirlsUrinal > 0 && Furniture > 0 && TapWaterFacility > 0)
                    {
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select All Colour')</script>", false);
                        return false;
                    }
                }
                else if (Classrooms > 0 && DrinkingWater > 0 && GirlsToilet > 0 && Electricity > 0 && Playground > 0 && Slide > 0 && BoundaryWall > 0 && Kitchen > 0 && CLT_Kit > 0 && bookAvl > 0)
                {


                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select All Colour')</script>", false);
                    this.txtdrinking.Focus();
                    return false;
                }
                if (rblPhysicalTB.Checked == true || rblPhysicalFC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Infrastructure TB or FC')</script>", false);



                    this.chkSMC.Focus();
                    return false;
                }

                if (txtClassRoom.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Class')</script>", false);



                    this.txtClassRoom.Focus();
                    return false;
                }
                if (txtSMCPre.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter SMC President Name')</script>", false);



                    this.txtSMCPre.Focus();
                    return false;
                }

                Int32 Teac = 0;
                if (txtFemaleTeacher.Text.Trim() != "")
                {
                    Teac = Convert.ToInt32(txtFemaleTeacher.Text);
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Female Teacher')</script>", false);



                    //this.txtFemaleTeacher.Focus();
                    //return false;
                }
                if (txtMaleTeacher.Text != "")
                {
                    Teac += Convert.ToInt32(txtMaleTeacher.Text);

                }
                if (Teac > 0)
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Male or Female Teacher')</script>", false);



                    this.txtFemaleTeacher.Focus();
                    return false;
                }
            }
            if (Infrastructure > 0 || Classrooms > 0 || DrinkingWater > 0 || GirlsToilet > 0 || Electricity > 0 || Playground > 0 || Slide > 0 || BoundaryWall > 0 || Kitchen > 0 || Teachers_Male > 0 || Teachers_Female > 0 || CLT_Kit > 0 || bookAvl > 0)
            {
                if (rblPhysicalTB.Checked == true || rblPhysicalFC.Checked == true)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Infrastructure TB or FC')</script>", false);



                    this.rblPhysicalTB.Focus();
                    return false;
                }
                if (chkPhysical.Checked == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select  Infrastructure ')</script>", false);



                    this.chkPhysical.Focus();
                    return false;
                }
            }
            #endregion


            #region SchoolConact
            string SchoolConact = "";


            foreach (ListItem item in chkSchoolCOntact.Items)
            {
                if (item.Selected)
                {

                    SchoolConact += "" + item.Value + "" + ",";


                }
            }
            if (SchoolConact.Length > 0)
            {
                SchoolConact = SchoolConact.Substring(0, SchoolConact.LastIndexOf(","));
            }
            //if (rblConTB.Checked == false && rblConFC.Checked == false)
            //{
            //    if (SchoolConact.Length>0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School Conact TB or FC')</script>", false);
            //        this.rblConTB.Focus();
            //        return false;
            //    }
            //}
            // if (rblConTB.Checked == true || rblConFC.Checked == true )
            //{

            //    if (SchoolConact.Length > 0)
            //    {

            //        if (rbloption1.Checked == false && rbloption2.Checked == false)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School Opation ')</script>", false);
            //            this.rblConTB.Focus();
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School Conact ')</script>", false);
            //        this.rblConTB.Focus();
            //        return false;
            //    }







            //}


            #endregion
            //if (GvReg.Rows.Count > 0)
            //{

            //    for (int i = 0; i < GvReg.Rows.Count; i++)
            //    {
            //        DropDownList Attendance = (DropDownList)GvReg.Rows[i].FindControl("ddlAttendance");

            //        int Attendancedata = Convert.ToInt32(Attendance.SelectedValue);
            //        if (Attendancedata > 0)
            //        {
            //            if (ddlsession.SelectedIndex <= 0)
            //            {
            //                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Session')</script>", false);
            //                return false;
            //            }
            //        }

            //    }
            //}

            if (GvReg.Rows.Count > 6)
            {

                if (ddlsession.SelectedIndex > 0)
                {
                    if (rblLifeFC.Checked == false && rblLifeTB.Checked == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select T.B or F.C. for Life Skill Education.')</script>", false);
                        return false;

                    }
                    if (chklife.Checked == false)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select   Life Skill')</script>", false);
                        this.chkSMC.Focus();
                        return false;

                    }

                    if (rblLifeTB.Checked == true)
                    {
                        if (ddlliffTb.SelectedIndex <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TB Name LiffSkill')</script>", false);



                            this.chkSMC.Focus();
                            return false;
                        }
                    }

                    for (int i = 0; i < GvReg.Rows.Count; i++)
                    {
                        DropDownList Attendance = (DropDownList)GvReg.Rows[i].FindControl("ddlAttendance");

                        int Attendancedata = Convert.ToInt32(Attendance.SelectedValue);
                        if (Attendancedata <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('please select all grils attendacne')</script>", false);
                            return false;
                        }

                    }
                }


            }



            return true;

        }
        catch
        {

            return false;
        }
    }
    public void Save()
    {

        if (this.ddlRemark.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);
            this.chkSMC.Focus();
            return;

        }

        #region Main
        Int32 TBHolding = 0;
        if (chkHolding.Checked == true)
        {
            TBHolding = 1;
        }



        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        string UNICOde = "";
        if (ViewState["GUID_School"].ToString().Length > 5)
        {
            UNICOde = ViewState["GUID_School"].ToString();
        }
        else
        {
            UNICOde = objMain.Generate_RandomString(15);
        }
        #endregion

        #region SMC
        Int32 SMC = 0;
        Int32 SMCTB = 0;
        Int32 SMCFC = 0;
        Int32 OtherSIPprepared = 0;
        Int32 OtherSIPcompleted = 0;
        string commmeeting = "";
        string commmeeting1 = "";

        string TBCode = "";
        Int32 ISTeamBalik = 0;

        Int32 SMCregisterismeeting = 0;
        Int32 SMCmeetingregister = 0;
        Int32 SMCWrite = 0;
        Int32 SMCF5 = 0;
        Int32 SMCIsMember = 0;
        if (rblSMCTB.Checked == true)
        {
            if (ddlGssTbname.SelectedIndex > 0)
            {

                TBCode = ddlGssTbname.SelectedValue;
            }
        }
        if (rdTeamY.Checked == true)
        {
            if (ddlMMTb.SelectedIndex > 0)
            {

                TBCode = ddlMMTb.SelectedValue;
            }
        }
        if (rdTeamY.Checked == true)
        {
            ISTeamBalik = 1;
        }
        if (rdTeamN.Checked == true)
        {
            ISTeamBalik = 2;
        }
        if (ddlrec.SelectedIndex > 0)
        {

            SMCregisterismeeting = Convert.ToInt32(ddlrec.SelectedValue);
        }
        if (ddlDatemeeting.SelectedIndex > 0)
        {
            SMCmeetingregister = Convert.ToInt32(ddlDatemeeting.SelectedValue);

        }
        if (ddlWrite.SelectedIndex > 0)
        {

            SMCWrite = Convert.ToInt32(ddlWrite.SelectedValue);
        }
        if (ddlF5.SelectedIndex > 0)
        {
            SMCF5 = Convert.ToInt32(ddlF5.SelectedValue);
        }
        if (txtmembers.Text != "")
        {
            SMCIsMember = Convert.ToInt32(txtmembers.Text);
        }

        if (chkSMC.Checked == true)
        {
            SMC = 1;
        }
        if (rblSMCTB.Checked == true)
        {
            SMCTB = 1;
        }
        if (rblSMCFC.Checked == true)
        {
            SMCFC = 1;
        }
        if (txtOtherSIPFC.Text != "")
        {
            OtherSIPprepared = Convert.ToInt32(txtOtherSIPFC.Text);
        }
        if (txtsmcmeetinFC.Text != "")
        {
            OtherSIPcompleted = Convert.ToInt32(txtsmcmeetinFC.Text);

        }

        foreach (ListItem item in CBL_bookformat.Items)
        {
            if (item.Selected)
            {

                commmeeting += "" + item.Value + "" + ",";


            }
        }
        if (commmeeting.Length > 0)
        {
            commmeeting = commmeeting.Substring(0, commmeeting.LastIndexOf(","));
        }

        foreach (ListItem item in CBL_bookformat1.Items)
        {
            if (item.Selected)
            {

                commmeeting1 += "" + item.Value + "" + ",";


            }
        }
        if (commmeeting1.Length > 0)
        {
            commmeeting1 = commmeeting1.Substring(0, commmeeting1.LastIndexOf(","));
        }
        #endregion

        #region SMC Orientation
        Int32 SMCOrient = 0;
        Int32 SMCOrientTB = 0;
        Int32 SMCOrientFC = 0;
        Int32 TotalMember = 0;
        Int32 TotalFemaSmcFemal = 0;
        Int32 SMCDirector = 0;
        Int32 SMCRegister = 0;
        if (rdPSMCPY.Checked == true && chkSMC.Checked == true)
        {
            SMCDirector = 1;
        }
        if (rdPSMCPN.Checked == true && chkSMC.Checked == true)
        {
            SMCDirector = 2;
        }
        if (rdRegisterY.Checked == true && chkSMC.Checked == true)
        {
            SMCRegister = 1;
        }
        if (rdRegisterN.Checked == true && chkSMC.Checked == true)
        {
            SMCRegister = 2;
        }
        if (chkSMC.Checked == true)
        {
            SMCOrient = 1;
        }
        if (rblSMCTB.Checked == true)
        {
            SMCOrientTB = 1;
        }
        if (rblSMCFC.Checked == true)
        {
            SMCOrientFC = 1;
        }
        //if (txtTotalMember.Text != "")
        //{
        //    TotalMember = Convert.ToInt32(txtTotalMember.Text);
        //}
        //if (txtTotalFmember.Text != "")
        //{
        //    TotalFemaSmcFemal = Convert.ToInt32(txtTotalFmember.Text);
        //}





        #endregion

        #region Subject
        DataTable dtSubject;
        dtSubject = CreateDataDate();
        DataRow dr;
        Int32 CLT_TB = 0;
        Int32 CLT_FC = 0;
        Int32 CLT = 0;
        string CLTHindi = "";
        #region Hindi
        if (chkHindiA.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 1;

            dr["CLTGroup"] = "A";
            dtSubject.Rows.Add(dr);
            CLTHindi += "A" + ",";
        }
        if (chkHindiB.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 1;

            dr["CLTGroup"] = "B";
            dtSubject.Rows.Add(dr);

            CLTHindi += "B" + ",";
        }
        if (chkHindiC.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 1;

            dr["CLTGroup"] = "C";
            dtSubject.Rows.Add(dr);

            CLTHindi += "C" + ",";
        }
        if (chkHindiD.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 1;

            dr["CLTGroup"] = "D";


            dtSubject.Rows.Add(dr);

            CLTHindi += "D" + ",";
        }

        if (chkHindiE.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 1;

            dr["CLTGroup"] = "E";

            CLTHindi += "E" + ",";
            dtSubject.Rows.Add(dr);
        }

        if (CLTHindi.Length > 0)
        {
            CLTHindi = CLTHindi.Substring(0, CLTHindi.LastIndexOf(","));

        }
        #endregion

        string CltEnglish = "";

        #region English
        if (chkEnglishA.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 2;

            dr["CLTGroup"] = "A";
            dtSubject.Rows.Add(dr);

            CltEnglish += "A" + ",";
        }
        if (chkEnglishB.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 2;

            dr["CLTGroup"] = "B";
            dtSubject.Rows.Add(dr);
            CltEnglish += "B" + ",";
        }
        if (chkEnglishC.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 2;

            dr["CLTGroup"] = "C";

            dtSubject.Rows.Add(dr);
            CltEnglish += "C" + ",";
        }
        if (chkEnglishD.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 2;

            dr["CLTGroup"] = "D";
            dtSubject.Rows.Add(dr);
            CltEnglish += "D" + ",";
        }

        if (chkEnglishE.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 2;

            dr["CLTGroup"] = "E";
            dtSubject.Rows.Add(dr);
            CltEnglish += "E" + ",";
        }

        if (CltEnglish.Length > 0)
        {
            CltEnglish = CltEnglish.Substring(0, CltEnglish.LastIndexOf(","));

        }
        #endregion
        string CltMath = "";
        #region Math
        if (chkMathA.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 3;

            dr["CLTGroup"] = "A";
            dtSubject.Rows.Add(dr);
            CltMath += "A" + ",";
        }
        if (chkMathB.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 3;

            dr["CLTGroup"] = "B";
            dtSubject.Rows.Add(dr);
            CltMath += "B" + ",";
        }
        if (chkMathC.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 3;

            dr["CLTGroup"] = "C";
            dtSubject.Rows.Add(dr);
            CltMath += "C" + ",";
        }
        if (chkMathD.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 3;

            dr["CLTGroup"] = "D";
            dtSubject.Rows.Add(dr);
            CltMath += "D" + ",";
        }

        if (chkMathE.Checked == true)
        {
            dr = dtSubject.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["Subject"] = 3;

            dr["CLTGroup"] = "E";
            dtSubject.Rows.Add(dr);
            CltMath += "E" + ",";
        }

        if (CltMath.Length > 0)
        {
            CltMath = CltMath.Substring(0, CltMath.LastIndexOf(","));

        }
        #endregion

        if (dtSubject.Rows.Count > 0)
        {
            if (rblCLTTB.Checked == true)
            {
                CLT_TB = 1;
            }
            if (rblCLTFC.Checked == true)
            {
                CLT_FC = 1;
            }
            if (chkClT.Checked == true)
            {
                CLT = 1;
            }
        }

        #endregion

        #region Test


        Int32 CLT_Pretest_FC = 0;
        Int32 CLT_Pretest_TB = 0;
        Int32 CTL_Midtest_FC = 0;
        Int32 CTL_Midtest_TB = 0;
        Int32 CLT_Posttest_FC = 0;
        Int32 CLT_Posttest_TB = 0;

        Int32 CLT_Pretest = 0;
        Int32 CLT_Midtest = 0;
        Int32 CLT_Posttes = 0;
        string Clt_Pre_PC = "";
        string Clt_Mid_PC = "";
        string Clt_Post_PC = "";

        if (rblPartialPre.Checked == true || rblCompletePre.Checked == true)
        {
            if (rblTestTBPre.Checked == true)
            {
                CLT_Pretest_TB = 1;
            }
            if (rblTestpreFC.Checked == true)
            {
                CLT_Pretest_FC = 1;
            }
            if (rblPartialPre.Checked == true)
            {
                Clt_Pre_PC = "P";
                CLT_Pretest = 1;
            }
            if (rblCompletePre.Checked == true)
            {
                Clt_Pre_PC = "C";
                CLT_Pretest = 1;
            }
        }
        if (rblPartialMid.Checked == true || rblCompleteMid.Checked == true)
        {
            if (rblTestTBMid.Checked == true)
            {
                CTL_Midtest_TB = 1;
            }
            if (rblTestMidFC.Checked == true)
            {
                CTL_Midtest_FC = 1;
            }
            if (rblPartialMid.Checked == true)
            {
                Clt_Mid_PC = "P";
                CLT_Midtest = 1;
            }
            if (rblCompleteMid.Checked == true)
            {
                Clt_Mid_PC = "C";
                CLT_Midtest = 1;
            }
        }

        if (rblPartialPost.Checked == true || rblCompletePost.Checked == true)
        {
            if (rblTestTBPost.Checked == true)
            {
                CLT_Posttest_TB = 1;
            }
            if (rblTestPostFC.Checked == true)
            {
                CLT_Posttest_FC = 1;
            }
            if (rblPartialPost.Checked == true)
            {
                Clt_Post_PC = "P";
                CLT_Posttes = 1;
            }
            if (rblCompletePost.Checked == true)
            {
                Clt_Post_PC = "C";
                CLT_Posttes = 1;
            }
        }
        #endregion

        #region Balsabha
        Int32 BalsabaTB = 0;
        Int32 BalsabFC = 0;

        Int32 BalSabha_Formation = 0;
        Int32 BalSabha_Orientation = 0;
        Int32 BalSabha_Chart = 0;
        Int32 BalSabha_Kit = 0;
        Int32 Bal = 0;
        Int32 BalType = 0;
        Int32 BalResone = 0;
        if (chkSession1.Checked == true && chkSession2.Checked == true)
        {

            BalSabha_Formation = 1;
            Bal = 1;

        }
        if (chkSession1.Checked == true)
        {

            BalSabha_Orientation = 1;

        }
        if (chkSession2.Checked == true)
        {
            BalSabha_Chart = 1;
        }
        if (chkKit.Enabled == true)
        {
            if (chkKit.Checked == true)
            {
                BalSabha_Kit = 1;
            }
        }
        if (rblBalsabaTB.Checked == true)
        {
            BalsabaTB = 1;
        }
        if (rblBalsabaFC.Checked == true)
        {
            BalsabFC = 1;
        }
        //if (BalSabha_Chart == 1 || BalSabha_Kit == 1 || BalSabha_Orientation == 1 )
        //{
        //    Bal = 1;

        //}


        if (rblPossiblie.Checked == true)
        {
            BalType = 1;
        }

        if (rblIMPossiblie.Checked == true)
        {
            BalType = 2;
        }

        if (rblIMPossiblie.Enabled == true)
        {
            if (rblIMPossiblie.Checked == true)
            {
                BalSabha_Formation = 0;
                BalSabha_Orientation = 0;
                BalSabha_Chart = 0;
                BalSabha_Kit = 0;
                Bal = 1;
                BalResone = Convert.ToInt32(ddlreasons.SelectedValue);
            }
        }

        Int32 LifeTB = 0;
        Int32 LifeFC = 0;
        if (rblLifeTB.Checked == true)
        {
            LifeTB = 1;
        }
        if (rblLifeFC.Checked == true)
        {
            LifeFC = 1;
        }
        #endregion

        #region Game

        Int32 Game_TB = 0;
        Int32 Game_FC = 0;
        Int32 Game = 0;
        string GameEntry = "";

        DataTable dtGame = CreateDataGame();
        if (chkGame1.Checked == true)
        {
            dr = dtGame.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["GameNo"] = 1;


            dtGame.Rows.Add(dr);
            GameEntry += 1 + ",";
        }
        if (chkGame2.Checked == true)
        {
            dr = dtGame.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["GameNo"] = 2;


            dtGame.Rows.Add(dr);
            GameEntry += 2 + ",";
        }
        if (chkGame3.Checked == true)
        {
            dr = dtGame.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["GameNo"] = 3;


            dtGame.Rows.Add(dr);
            GameEntry += 3 + ",";
        }
        if (chkGame4.Checked == true)
        {
            dr = dtGame.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["GameNo"] = 4;


            dtGame.Rows.Add(dr);
            GameEntry += 4 + ",";
        }

        if (chkGame5.Checked == true)
        {
            dr = dtGame.NewRow();
            dr["GUID_School"] = UNICOde;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd");
            dr["GameNo"] = 5;


            dtGame.Rows.Add(dr);
            GameEntry += 5 + ",";
        }

        if (GameEntry.Length > 0)
        {
            GameEntry = GameEntry.Substring(0, GameEntry.LastIndexOf(","));

        }
        if (dtGame.Rows.Count > 0)
        {
            if (rblLifeTB.Checked == true)
            {
                Game_TB = 1;
            }
            if (rblLifeFC.Checked == true)
            {
                Game_FC = 2;
            }
            Game = 1;
        }

        if (rblLifeTB.Checked == true)
        {
            Game_TB = 1;
            Game = 1;
        }
        if (rblLifeFC.Checked == true)
        {
            Game = 1;
            Game_FC = 2;
        }
        #endregion

        #region SAC Update
        Int32 SACTB = 0;
        Int32 SACFC = 0;
        Int32 SAC = 0;

        int SAC_No_Of_Attended = 0;
        int SAC_Periodic_Checkup = 0;
        int SAC_Listing_Name_Of_Girls = 0;
        int SAC_Listing_Name_Of_Boys = 0;
        int SAC_Girls_Left = 0;
        int SAC_Boys_Left = 0;
        int SAC_Girls_Not_Joined_School = 0;
        int SAC_Boys_Not_Joined_School = 0;


        int month = 0;
        if (txtDate.Text != "")
        {
            month = Convert.ToInt32(b[1]);
        }
        if (month == 7)
        {
            if (txtSMCMeeting.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_No_Of_Attended = Convert.ToInt32(txtSMCMeeting.Text);
            }


            if (txtHealth.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Periodic_Checkup = Convert.ToInt32(txtHealth.Text);
            }


            if (txtAdgirls.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Girls = Convert.ToInt32(txtAdgirls.Text);
            }

            if (txtAdBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Boys = Convert.ToInt32(txtAdBoy.Text);
            }


            if (txtleftGirl.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Left = Convert.ToInt32(txtleftGirl.Text);
            }

            if (txtleftBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Left = Convert.ToInt32(txtleftBoy.Text);
            }


            if (txtGirlNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Not_Joined_School = Convert.ToInt32(txtGirlNot.Text);
            }


            if (txtBoyNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Not_Joined_School = Convert.ToInt32(txtBoyNot.Text);
            }
        }

        if (month == 10 || month == 11)
        {
            if (txtSepSMCMeeting.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_No_Of_Attended = Convert.ToInt32(txtSepSMCMeeting.Text);
            }


            if (txtSepHealth.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Periodic_Checkup = Convert.ToInt32(txtSepHealth.Text);
            }


            if (txtsepAdgirls.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Girls = Convert.ToInt32(txtsepAdgirls.Text);
            }

            if (txtSepAdBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Boys = Convert.ToInt32(txtSepAdBoy.Text);
            }


            if (txtSepleftGirl.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Left = Convert.ToInt32(txtSepleftGirl.Text);
            }

            if (txtSepleftBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Left = Convert.ToInt32(txtSepleftBoy.Text);
            }


            if (txtSepGirlNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Not_Joined_School = Convert.ToInt32(txtSepGirlNot.Text);
            }


            if (txtSepBoyNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Not_Joined_School = Convert.ToInt32(txtSepBoyNot.Text);
            }
        }

        if (month == 1)
        {
            if (txtDescSMCMeeting.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_No_Of_Attended = Convert.ToInt32(txtDescSMCMeeting.Text);
            }


            if (txtDescHealth.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Periodic_Checkup = Convert.ToInt32(txtDescHealth.Text);
            }


            if (txtDescAdgirls.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Girls = Convert.ToInt32(txtDescAdgirls.Text);
            }

            if (txtDescAdBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Boys = Convert.ToInt32(txtDescAdBoy.Text);
            }


            if (txtDescleftGirl.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Left = Convert.ToInt32(txtDescleftGirl.Text);
            }

            if (txtdescleftBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Left = Convert.ToInt32(txtdescleftBoy.Text);
            }


            if (txtDescGirlNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Not_Joined_School = Convert.ToInt32(txtDescGirlNot.Text);
            }


            if (txtDecBoyNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Not_Joined_School = Convert.ToInt32(txtDecBoyNot.Text);
            }
        }

        if (month == 3)
        {
            if (txtMarSMCMeeting.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_No_Of_Attended = Convert.ToInt32(txtMarSMCMeeting.Text);
            }


            if (txtMarHealth.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Periodic_Checkup = Convert.ToInt32(txtMarHealth.Text);
            }


            if (txtMarAdgirls.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Girls = Convert.ToInt32(txtMarAdgirls.Text);
            }

            if (txtMarAdBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Listing_Name_Of_Boys = Convert.ToInt32(txtMarAdBoy.Text);
            }


            if (txtMarleftGirl.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Left = Convert.ToInt32(txtMarleftGirl.Text);
            }

            if (txtMarleftBoy.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Left = Convert.ToInt32(txtMarleftBoy.Text);
            }


            if (txtMarGirlNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Girls_Not_Joined_School = Convert.ToInt32(txtMarGirlNot.Text);
            }


            if (txtMarBoyNot.Text.Trim() != "" && pnlSACUpdate.Enabled == true)
            {
                SAC_Boys_Not_Joined_School = Convert.ToInt32(txtMarBoyNot.Text);
            }
        }
        if (rblSacTB.Checked == true)
        {
            SACTB = 1;
        }
        if (rblSacFB.Checked == true)
        {
            SACFC = 1;
        }
        if (chkSACUpdate.Checked == true)
        {
            SAC = 1;
        }

        #endregion

        #region SPF
        int Infrastructure = 0;
        int Classrooms = 0;
        int DrinkingWater = 0;
        int GirlsToilet = 0;
        int Electricity = 0;
        int Playground = 0;
        int Slide = 0;
        int BoundaryWall = 0;
        int Kitchen = 0;
        int Teachers_Male = 0;
        int Teachers_Female = 0;
        int CLT_Kit = 0;
        int bookAvl = 0;


        int BoysToilet = 0;
        int WaterSupply = 0;
        int TilingToilet = 0;
        int HandicappedAccessibleToilet = 0;
        int MultipleHandwashingUnit = 0;
        int TilingClassroomFloor = 0;
        int Blackboards = 0;
        int ProperPainting = 0;
        int DisabledAccessibleRamp = 0;
        int AppropriateElectricalWiring = 0;
        int BoysUrinal = 0;
        int GirlsUrinal = 0;
        int Furniture = 0;
        int TapWaterFacility = 0;
        int Infrastructure_FC = 0; int Infrastructure_TB = 0;

        if (txtClassRoom.Text != "")
        {
            Classrooms = Convert.ToInt32(txtClassRoom.Text);
        }
        if (lbldriking.Text == "1")
        {
            DrinkingWater = 1;
        }
        else if (lbldriking.Text == "2")
        {

            DrinkingWater = 2;
        }
        else if (lbldriking.Text == "3")
        {

            DrinkingWater = 3;
        }
        else if (lbldriking.Text == "4")
        {

            DrinkingWater = 4;
        }

        if (lblToilet.Text == "1")
        {
            GirlsToilet = 1;
        }
        else if (lblToilet.Text == "2")
        {

            GirlsToilet = 2;
        }
        else if (lblToilet.Text == "3")
        {

            GirlsToilet = 3;
        }
        else if (lblToilet.Text == "4")
        {

            GirlsToilet = 4;
        }

        if (lblElectricity.Text == "1")
        {
            Electricity = 1;
        }
        else if (lblElectricity.Text == "2")
        {

            Electricity = 2;
        }
        else if (lblElectricity.Text == "3")
        {

            Electricity = 3;
        }
        else if (lblElectricity.Text == "4")
        {

            Electricity = 4;
        }

        if (lblPlay.Text == "1")
        {
            Playground = 1;
        }
        else if (lblPlay.Text == "2")
        {

            Playground = 2;
        }
        else if (lblPlay.Text == "3")
        {

            Playground = 3;
        }
        else if (lblPlay.Text == "4")
        {

            Playground = 4;
        }

        if (Convert.ToString(Session["StateCode"]) == "9")
        {

        }
        else
        {
            if (lblSlides.Text == "1")
            {
                Slide = 1;
            }
            else if (lblSlides.Text == "2")
            {

                Slide = 2;
            }
            else if (lblSlides.Text == "3")
            {

                Slide = 3;
            }
            else if (lblSlides.Text == "4")
            {

                Slide = 4;
            }
        }
        if (lblBoundaryWall.Text == "1")
        {
            BoundaryWall = 1;
        }
        else if (lblBoundaryWall.Text == "2")
        {

            BoundaryWall = 2;
        }
        else if (lblBoundaryWall.Text == "3")
        {

            BoundaryWall = 3;
        }
        else if (lblBoundaryWall.Text == "4")
        {

            BoundaryWall = 4;
        }




        if (lblKitchen.Text == "1")
        {
            Kitchen = 1;
        }
        else if (lblKitchen.Text == "2")
        {

            Kitchen = 2;
        }
        else if (lblKitchen.Text == "3")
        {

            Kitchen = 3;
        }
        else if (lblKitchen.Text == "4")
        {

            Kitchen = 4;
        }
        if (lblCltKit.Text == "1")
        {
            CLT_Kit = 1;
        }
        else if (lblCltKit.Text == "2")
        {

            CLT_Kit = 2;
        }
        else if (lblCltKit.Text == "3")
        {

            CLT_Kit = 3;
        }
        else if (lblCltKit.Text == "4")
        {

            CLT_Kit = 4;
        }

        if (lblbook.Text == "1")
        {
            bookAvl = 1;
        }
        else if (lblbook.Text == "2")
        {

            bookAvl = 2;
        }
        else if (lblbook.Text == "3")
        {

            bookAvl = 3;
        }
        else if (lblbook.Text == "4")
        {

            bookAvl = 4;
        }


        if (lblBoysToilet.Text == "1")
        {
            BoysToilet = 1;
        }
        else if (lblBoysToilet.Text == "2")
        {

            BoysToilet = 2;
        }
        else if (lblBoysToilet.Text == "3")
        {

            BoysToilet = 3;
        }
        else if (lblBoysToilet.Text == "4")
        {

            BoysToilet = 4;
        }

        if (lblWaterSupply.Text == "1")
        {
            WaterSupply = 1;
        }
        else if (lblWaterSupply.Text == "2")
        {

            WaterSupply = 2;
        }
        else if (lblWaterSupply.Text == "3")
        {

            WaterSupply = 3;
        }
        else if (lblWaterSupply.Text == "4")
        {

            WaterSupply = 4;
        }

        if (lblTilingToilet.Text == "1")
        {
            TilingToilet = 1;
        }
        else if (lblTilingToilet.Text == "2")
        {

            TilingToilet = 2;
        }
        else if (lblTilingToilet.Text == "3")
        {

            TilingToilet = 3;
        }
        else if (lblTilingToilet.Text == "4")
        {

            TilingToilet = 4;
        }


        if (lblHandicappedAccessibleToilet.Text == "1")
        {
            HandicappedAccessibleToilet = 1;
        }
        else if (lblHandicappedAccessibleToilet.Text == "2")
        {

            HandicappedAccessibleToilet = 2;
        }
        else if (lblHandicappedAccessibleToilet.Text == "3")
        {

            HandicappedAccessibleToilet = 3;
        }
        else if (lblHandicappedAccessibleToilet.Text == "4")
        {

            HandicappedAccessibleToilet = 4;
        }

        if (lblMultipleHandwashingUnit.Text == "1")
        {
            MultipleHandwashingUnit = 1;
        }
        else if (lblMultipleHandwashingUnit.Text == "2")
        {
            MultipleHandwashingUnit = 2;
        }
        else if (lblMultipleHandwashingUnit.Text == "3")
        {
            MultipleHandwashingUnit = 3;
        }
        else if (lblMultipleHandwashingUnit.Text == "4")
        {
            MultipleHandwashingUnit = 4;
        }

        if (lblTilingClassroomFloor.Text == "1")
        {
            TilingClassroomFloor = 1;
        }
        else if (lblTilingClassroomFloor.Text == "2")
        {
            TilingClassroomFloor = 2;
        }
        else if (lblTilingClassroomFloor.Text == "3")
        {
            TilingClassroomFloor = 3;
        }
        else if (lblTilingClassroomFloor.Text == "4")
        {
            TilingClassroomFloor = 4;
        }


        if (lblBlackboards.Text == "1")
        {
            Blackboards = 1;
        }
        else if (lblBlackboards.Text == "2")
        {
            Blackboards = 2;
        }
        else if (lblBlackboards.Text == "3")
        {
            Blackboards = 3;
        }
        else if (lblBlackboards.Text == "4")
        {
            Blackboards = 4;
        }


        if (lblProperPainting.Text == "1")
        {
            ProperPainting = 1;
        }
        else if (lblProperPainting.Text == "2")
        {
            ProperPainting = 2;
        }
        else if (lblProperPainting.Text == "3")
        {
            ProperPainting = 3;
        }
        else if (lblProperPainting.Text == "4")
        {
            ProperPainting = 4;
        }



        if (lblDisabledAccessibleRamp.Text == "1")
        {
            DisabledAccessibleRamp = 1;
        }
        else if (lblDisabledAccessibleRamp.Text == "2")
        {
            DisabledAccessibleRamp = 2;
        }
        else if (lblDisabledAccessibleRamp.Text == "3")
        {
            DisabledAccessibleRamp = 3;
        }
        else if (lblDisabledAccessibleRamp.Text == "4")
        {
            DisabledAccessibleRamp = 4;
        }

        if (lblAppropriateElectricalWiring.Text == "1")
        {
            AppropriateElectricalWiring = 1;
        }
        else if (lblAppropriateElectricalWiring.Text == "2")
        {
            AppropriateElectricalWiring = 2;
        }
        else if (lblAppropriateElectricalWiring.Text == "3")
        {
            AppropriateElectricalWiring = 3;
        }
        else if (lblAppropriateElectricalWiring.Text == "4")
        {
            AppropriateElectricalWiring = 4;
        }

        if (lblBoysUrinal.Text == "1")
        {
            BoysUrinal = 1;
        }
        else if (lblBoysUrinal.Text == "2")
        {
            BoysUrinal = 2;
        }
        else if (lblBoysUrinal.Text == "3")
        {
            BoysUrinal = 3;
        }
        else if (lblBoysUrinal.Text == "4")
        {
            BoysUrinal = 4;
        }

        if (lblGirlsUrinal.Text == "1")
        {
            GirlsUrinal = 1;
        }
        else if (lblGirlsUrinal.Text == "2")
        {
            GirlsUrinal = 2;
        }
        else if (lblGirlsUrinal.Text == "3")
        {
            GirlsUrinal = 3;
        }
        else if (lblGirlsUrinal.Text == "4")
        {
            GirlsUrinal = 4;
        }

        if (lblFurniture.Text == "1")
        {
            Furniture = 1;
        }
        else if (lblFurniture.Text == "2")
        {
            Furniture = 2;
        }
        else if (lblFurniture.Text == "3")
        {
            Furniture = 3;
        }
        else if (lblFurniture.Text == "4")
        {
            Furniture = 4;
        }

        if (lblTapWaterFacility.Text == "1")
        {
            TapWaterFacility = 1;
        }
        else if (lblTapWaterFacility.Text == "2")
        {
            TapWaterFacility = 2;
        }
        else if (lblTapWaterFacility.Text == "3")
        {
            TapWaterFacility = 3;
        }
        else if (lblTapWaterFacility.Text == "4")
        {
            TapWaterFacility = 4;
        }

        if (txtFemaleTeacher.Text != "")
        {
            Teachers_Female = Convert.ToInt32(txtFemaleTeacher.Text);
        }
        if (txtMaleTeacher.Text != "")
        {
            Teachers_Male = Convert.ToInt32(txtMaleTeacher.Text);
        }
        if (rblPhysicalTB.Checked == true)
        {
            Infrastructure_TB = 1;
        }
        if (rblPhysicalFC.Checked == true)
        {
            Infrastructure_FC = 1;
        }
        if (chkPhysical.Checked == true)
        {
            Infrastructure = 1;
        }

        #endregion

        #region Annaul
        int SIP_Annual_FC = 0; int SIP_Annual_TB = 0; int Retention_Annual_FC = 0; int Retention_Annual_TB = 0; int AnnualData = 0; int SIP_Annual = 0; int Retention_Annual = 0;
        int Other_FC = 0;
        string Retention_PC = "", SIP_PC = "", Other_TB = "";
        if (chkAnnual.Checked == true)
        {
            AnnualData = 1;
        }

        if (chkSIPAnnaul.Checked == true)
        {
            SIP_Annual = 1;
        }
        if (chkRetention.Checked == true)
        {
            Retention_Annual = 1;
        }
        if (chkSIPTB.Checked == true)
        {
            SIP_Annual_TB = 1;
        }
        if (chkRenTB.Checked == true)
        {
            Retention_Annual_TB = 1;
        }

        if (chkSIPFC.Checked == true)
        {
            SIP_Annual_FC = 1;
        }
        if (chkRenFC.Checked == true)
        {
            Retention_Annual_FC = 1;
        }



        if (chkSipPartial.Checked == true)
        {
            SIP_PC = "P";
        }

        if (chkSipComplete.Checked == true)
        {
            SIP_PC = "C";
        }
        if (chkComplete.Checked == true)
        {
            Retention_PC = "C";
        }
        if (chkRenPartial.Checked == true)
        {
            Retention_PC = "P";
        }
        Other_TB = txtOther.Text;
        #endregion

        #region Session
        // DateTime? Session1 = null, Session2 = null;

        //if (chkSession1.Checked == true && hdnsession1.Value == "")
        //{
        //    Session1 = Convert.ToDateTime(txtDate.Text);
        //}
        //else
        //{

        //    Session1 = Convert.ToDateTime(hdnsession1.Value);
        //}
        //if (chkSession1.Checked == true && chkSession2.Checked == true && hdnsession2.Value == "")
        //{
        //    Session2 = Convert.ToDateTime(txtDate.Text);
        //    BalSabha_Formation = 1;
        //}
        //else if (Convert.ToString(hdnsession2.Value) != "")
        //{

        //    Session2 = Convert.ToDateTime(hdnsession2.Value);
        //}
        #endregion

        #region Attendance
        string CreatedBy = "", UniqueCode = "", UniqueChildRCode = "", VillageCode = "", UniqueCodeNew = "", flag = "", Schoolcode = "";
        int result = 0, sessiondata = 0;
        int TotalSC = 0; int TotalFSC = 0;
        if (chkSMC.Checked == true)
        {
            if (gvSmc.Rows.Count > 0)
            {

                for (int i = 0; i < gvSmc.Rows.Count; i++)
                {

                    CheckBox Attendance = (CheckBox)gvSmc.Rows[i].FindControl("ddlAttendanceSmc");
                    int Ps = 0;
                    Label lblCUniqueChildCode = (Label)gvSmc.Rows[i].FindControl("lblCUniqueChildCode");
                    Label lblUniqueMemberCode = (Label)gvSmc.Rows[i].FindControl("lblUniqueMemberCode");
                    Label lblGender = (Label)gvSmc.Rows[i].FindControl("lblGender");

                    Label lblName = (Label)gvSmc.Rows[i].FindControl("lblName");
                    Label lblSession = (Label)gvSmc.Rows[i].FindControl("lblSession");
                    Label lblIsPrevEntry = (Label)gvSmc.Rows[i].FindControl("lblIsPrevEntry");
                    if (Attendance.Checked == true)
                    {
                        Ps = 1;
                        TotalSC = TotalSC + 1;
                    }
                    if (lblGender.Text == "2")
                    {
                        TotalFSC = TotalFSC + 1;
                    }

                    string strMainIDNo = objMain.Generate_RandomString(8);
                    if (lblCUniqueChildCode.Text == "")
                    {
                        lblCUniqueChildCode.Text = strMainIDNo;
                    }

                    SqlParameter[] parm = new SqlParameter[]
                        {



                    new SqlParameter("@UniqueCode", lblCUniqueChildCode.Text),
                      new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                        new SqlParameter("@SchoolCode", ddlSchool.SelectedValue),
                     new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate)),

                    new SqlParameter("@Name", lblName.Text),
                    new SqlParameter("@Gender", lblGender.Text),
                    new SqlParameter("@Mobile", lblSession.Text),
                    new SqlParameter("@PrevEntry", lblIsPrevEntry.Text),
                    new SqlParameter("@CreateBy", Session["UserName"].ToString()),

                      new SqlParameter("@Flag", Ps),
                         new SqlParameter("@UniqueMemberCode", lblUniqueMemberCode.Text),

                          };
                    int result44 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSMC2025", parm);


                }


                TotalMember = Convert.ToInt32(TotalSC);
                TotalFemaSmcFemal = Convert.ToInt32(TotalFSC);





            }


        }

        #endregion

        #region Contact Save
        string SchoolConact = "";
        int SchoolConactTBFC = 0;
        int SchoolConactOption = 0;
        SchoolConactTBFC = 2;
        if (rblConTB.Checked == true)
        {
            SchoolConactOption = 1;
        }




        foreach (ListItem item in chkSchoolCOntact.Items)
        {
            if (item.Selected)
            {

                SchoolConact += "" + item.Value + "" + ",";


            }
        }
        if (SchoolConact.Length > 0)
        {
            SchoolConact = SchoolConact.Substring(0, SchoolConact.LastIndexOf(","));
        }
        #endregion
        #region FinalSave
        Int32 MainResult = 0;
        if (ViewState["GUID_School"].ToString().Length > 5)
        {
            string userid = "";
            if (Convert.ToString(Session["user_level"]) == "")
            {
                Response.Redirect("Login.aspx");
            }

            if (Session["user_level"].ToString() == "19")
            {
                userid = "2";

                MainResult = InsertUpdateActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, userid, "U", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString(), BalType.ToString(), BalResone.ToString(), SMCDirector, SMCRegister, commmeeting1, TxtSmcOther.Text, SchoolConactTBFC, SchoolConactOption, SchoolConact, TBCode, ISTeamBalik, SMCregisterismeeting, SMCmeetingregister, SMCWrite, SMCF5, SMCIsMember, BoysToilet, WaterSupply, TilingToilet, HandicappedAccessibleToilet, MultipleHandwashingUnit, TilingClassroomFloor, Blackboards, ProperPainting, DisabledAccessibleRamp, AppropriateElectricalWiring, BoysUrinal, GirlsUrinal, Furniture, TapWaterFacility);

                //MainResult = objMain.ActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMC_TB.ToString(), SMC_FC.ToString(), totalMemberTrain.ToString(), MemberTrain.ToString(), SMCMeeting.ToString(), OtherSP.ToString(), commmeeting.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), "U", Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), userid, CLTHindi, CltEnglish, CltMath, GameEntry, Convert.ToInt32(Session["user_level"].ToString()), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, Infrastructure_FC, Infrastructure_TB, Other_TB, Other_FC, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Infrastructure, Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), SIP_PC, Retention_PC);
                userid = "3";
                MainResult = InsertUpdateActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, userid, "U", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString(), BalType.ToString(), BalResone.ToString(), SMCDirector, SMCRegister, commmeeting1, TxtSmcOther.Text, SchoolConactTBFC, SchoolConactOption, SchoolConact, TBCode, ISTeamBalik, SMCregisterismeeting, SMCmeetingregister, SMCWrite, SMCF5, SMCIsMember, BoysToilet, WaterSupply, TilingToilet, HandicappedAccessibleToilet, MultipleHandwashingUnit, TilingClassroomFloor, Blackboards, ProperPainting, DisabledAccessibleRamp, AppropriateElectricalWiring, BoysUrinal, GirlsUrinal, Furniture, TapWaterFacility);


            }
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {

                MainResult = InsertUpdateActivitySchool(ViewState["GUID_School"].ToString(), ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "3", "U", "B", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString(), BalType.ToString(), BalResone.ToString(), SMCDirector, SMCRegister, commmeeting1, TxtSmcOther.Text, SchoolConactTBFC, SchoolConactOption, SchoolConact, TBCode, ISTeamBalik, SMCregisterismeeting, SMCmeetingregister, SMCWrite, SMCF5, SMCIsMember, BoysToilet, WaterSupply, TilingToilet, HandicappedAccessibleToilet, MultipleHandwashingUnit, TilingClassroomFloor, Blackboards, ProperPainting, DisabledAccessibleRamp, AppropriateElectricalWiring, BoysUrinal, GirlsUrinal, Furniture, TapWaterFacility);

            }
        }
        else
        {


            if (Session["user_level"].ToString() == "19")
            {
                MainResult = InsertUpdateActivitySchool(UNICOde, ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "2", "I", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString(), BalType.ToString(), BalResone.ToString(), SMCDirector, SMCRegister, commmeeting1, TxtSmcOther.Text, SchoolConactTBFC, SchoolConactOption, SchoolConact, TBCode, ISTeamBalik, SMCregisterismeeting, SMCmeetingregister, SMCWrite, SMCF5, SMCIsMember, BoysToilet, WaterSupply, TilingToilet, HandicappedAccessibleToilet, MultipleHandwashingUnit, TilingClassroomFloor, Blackboards, ProperPainting, DisabledAccessibleRamp, AppropriateElectricalWiring, BoysUrinal, GirlsUrinal, Furniture, TapWaterFacility);
                MainResult = InsertUpdateActivitySchool(UNICOde, ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "3", "I", "FC", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString(), BalType.ToString(), BalResone.ToString(), SMCDirector, SMCRegister, commmeeting1, TxtSmcOther.Text, SchoolConactTBFC, SchoolConactOption, SchoolConact, TBCode, ISTeamBalik, SMCregisterismeeting, SMCmeetingregister, SMCWrite, SMCF5, SMCIsMember, BoysToilet, WaterSupply, TilingToilet, HandicappedAccessibleToilet, MultipleHandwashingUnit, TilingClassroomFloor, Blackboards, ProperPainting, DisabledAccessibleRamp, AppropriateElectricalWiring, BoysUrinal, GirlsUrinal, Furniture, TapWaterFacility);
            }

            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {

                MainResult = InsertUpdateActivitySchool(UNICOde, ddlVilage.SelectedValue, ddlUser.SelectedValue.ToString(), ddlSchool.SelectedValue.ToString(), Convert.ToDateTime(FcDate), TBHolding.ToString(), SMC.ToString(), SMCTB.ToString(), SMCFC.ToString(), OtherSIPprepared.ToString(), OtherSIPcompleted.ToString(), commmeeting, SMCOrient.ToString(), SMCOrientTB.ToString(), SMCOrientFC.ToString(), TotalMember.ToString(), TotalFemaSmcFemal.ToString(), CLT.ToString(), CLT_TB.ToString(), CLT_FC.ToString(), CLTHindi, CltEnglish, CltMath, CLT_Pretest_FC.ToString(), CLT_Pretest_TB.ToString(), CTL_Midtest_FC.ToString(), CTL_Midtest_TB.ToString(), CLT_Posttest_FC.ToString(), CLT_Posttest_TB.ToString(), Clt_Pre_PC.ToString(), Clt_Mid_PC.ToString(), Clt_Post_PC.ToString(), Bal.ToString(), BalsabaTB.ToString(), BalsabFC.ToString(), BalSabha_Formation.ToString(), BalSabha_Orientation.ToString(), BalSabha_Chart.ToString(), BalSabha_Kit.ToString(), Game.ToString(), Game_TB.ToString(), Game_FC.ToString(), SACTB.ToString(), SACFC.ToString(), SAC.ToString(), SAC_Periodic_Checkup.ToString(), SAC_Listing_Name_Of_Girls.ToString(), SAC_Listing_Name_Of_Boys.ToString(), SAC_Girls_Left.ToString(), SAC_Boys_Left.ToString(), SAC_Girls_Not_Joined_School.ToString(), SAC_Boys_Not_Joined_School.ToString(), SAC_No_Of_Attended.ToString(), Classrooms, DrinkingWater, GirlsToilet, Electricity, Playground, Slide, BoundaryWall, Kitchen, Teachers_Male, Teachers_Female, CLT_Kit, bookAvl, Infrastructure, Infrastructure_FC, Infrastructure_TB, SIP_Annual_FC, SIP_Annual_TB, Retention_Annual_FC, Retention_Annual_TB, AnnualData, SIP_Annual, Retention_Annual, SIP_PC, Retention_PC, txtOther.Text, GameEntry, "3", "I", "B", CLT_Pretest, CLT_Midtest, CLT_Posttes, ddlRemark.SelectedValue, Session["username"].ToString(), BalType.ToString(), BalResone.ToString(), SMCDirector, SMCRegister, commmeeting1, TxtSmcOther.Text, SchoolConactTBFC, SchoolConactOption, SchoolConact, TBCode, ISTeamBalik, SMCregisterismeeting, SMCmeetingregister, SMCWrite, SMCF5, SMCIsMember, BoysToilet, WaterSupply, TilingToilet, HandicappedAccessibleToilet, MultipleHandwashingUnit, TilingClassroomFloor, Blackboards, ProperPainting, DisabledAccessibleRamp, AppropriateElectricalWiring, BoysUrinal, GirlsUrinal, Furniture, TapWaterFacility);

            }
        }


        if (MainResult > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            ViewState["GUID_School"] = UNICOde;
        }



        #endregion
    }

    public int InsertUpdateActivitySchool(string GUID_School, string VillageCode, string UserID, string SchoolCode, DateTime ActivityDate, string TB_Handholding, string SMC, string SMC_TB, string SMC_FC, string SMC_Mtg, string SMC_OtherSIP, string SMC_OtherDiscussions, string SMCOr, string SMCOr_TB, string SMCOr_FC, string SMC_TotTrained, string SMC_FemaleTrained, string CLT, string CLTTB, string CLTFC, string CLTHindi, string CLTEnglish, string CLTMath, string CLT_Pretest_FC, string CLT_Pretest_TB, string CTL_Midtest_FC, string CTL_Midtest_TB, string CLT_Posttest_FC, string CLT_Posttest_TB, string Clt_Pre_PC, string Clt_Mid_PC, string Clt_Post_PC, string BalSabha, string BalSabha_TB, string BalSabha_FC, string BalSabha_Formation, string BalSabha_Orientation, string BalSabha_Chart, string BalSabha_Kit, string Lifeskill_Games, string Lifeskill_Games_TB, string Lifeskill_Games_FC, string SACUpdate_TB, string SACUpdate_FC, string SACUpdate, string SAC_Periodic_Checkup, string SAC_Listing_Name_Of_Girls, string SAC_Listing_Name_Of_Boys, string SAC_Girls_Left, string SAC_Boys_Left, string SAC_Girs_Not_Joined_School, string SAC_Boys_Not_Joined_School, string SAC_No_Of_Attended, int Classrooms, int DrinkingWater, int GirlsToilet, int Electricity, int Playground, int Slide, int BoundaryWall, int Kitchen, int Teachers_Male, int Teachers_Female, int CLT_Kit, int bookAvl, int Infrastructure, int Infrastructure_FC, int Infrastructure_TB, int SIP_Annual_FC, int SIP_Annual_TB, int Retention_Annual_FC, int Retention_Annual_TB, int AnnualData, int SIP_Annual, int Retention_Annual, string SIP_PC, string Retention_PC, string Others_Description, string LifeSkillGameEntry, string UserEntry, string Flag, string ApproveBy, int CLT_Pretest, int CLT_Midtest, int CLT_Posttes, string Remark, string CreateBy, string BalsabaType, string Balsabareason, int SMCDirector, int SMCRegister, string SMC_Purpose, string SMC_OtherDiscussions_Oth, int ContactFCTB, int ContactOption, string SchoolContactOption, string TBCode, int ISTeamBalik, int SMCregisterismeeting, int SMCmeetingregister, int SMCWrite, int SMCF5, int SMCIsMember, int BoysToilet, int WaterSupply, int TilingToilet, int HandicappedAccessibleToilet, int MultipleHandwashingUnit, int TilingClassroomFloor, int Blackboards, int ProperPainting, int DisabledAccessibleRamp, int AppropriateElectricalWiring, int BoysUrinal, int GirlsUrinal, int Furniture, int TapWaterFacility)
    {
        string BalSabaTBCode = "";
        if (rblBalsabaTB.Checked == true)
        {
            BalSabaTBCode = ddlBalSabaTB.SelectedValue;
        }
        int FCcoth = 0;
        int tbcoth = 0;
        if (rblothertb.Checked == true)
        {
            tbcoth = 1;
        }
        if (rblotherfc.Checked == true)
        {
            FCcoth = 1;
        }

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@GUID_School", GUID_School),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@ActivityDate", ActivityDate),
            new SqlParameter("@TB_Handholding", TB_Handholding),
            new SqlParameter("@SMC", SMC),
            new SqlParameter("@SMC_TB", SMC_TB),
            new SqlParameter("@SMC_FC", SMC_FC),
            new SqlParameter("@SMC_OtherSIPPrepaired", SMC_Mtg),
            new SqlParameter("@SMC_OtherSIPComp", SMC_OtherSIP),
            new SqlParameter("@SMC_OtherDiscussions", SMC_OtherDiscussions),
            new SqlParameter("@SMCOr", SMCOr),
            new SqlParameter("@SMCOr_TB", SMCOr_TB),
            new SqlParameter("@SMCOr_FC", SMCOr_FC),
            new SqlParameter("@SMC_TotTrained", SMC_TotTrained),
            new SqlParameter("@SMC_FemaleTrained", SMC_FemaleTrained),
            new SqlParameter("@CLT", CLT),
            new SqlParameter("@CLTTB", CLTTB),
            new SqlParameter("@CLTFC", CLTFC),
            new SqlParameter("@CLTHindi", CLTHindi),
            new SqlParameter("@CLTEnglish", CLTEnglish),
            new SqlParameter("@CLTMath", CLTMath),
            new SqlParameter("@CLT_Pretest_FC", CLT_Pretest_FC),
            new SqlParameter("@CLT_Pretest_TB", CLT_Pretest_TB),
            new SqlParameter("@CTL_Midtest_FC", CTL_Midtest_FC),
            new SqlParameter("@CTL_Midtest_TB", CTL_Midtest_TB),
            new SqlParameter("@CLT_Posttest_FC", CLT_Posttest_FC),
            new SqlParameter("@CLT_Posttest_TB", CLT_Posttest_TB),
            new SqlParameter("@Clt_Pre_PC", Clt_Pre_PC),
            new SqlParameter("@Clt_Mid_PC", Clt_Mid_PC),
            new SqlParameter("@Clt_Post_PC", Clt_Post_PC),
            new SqlParameter("@BalSabha", BalSabha),
            new SqlParameter("@BalSabha_TB", BalSabha_TB),
            new SqlParameter("@BalSabha_FC", BalSabha_FC),
            new SqlParameter("@BalSabha_Formation", BalSabha_Formation),
            new SqlParameter("@BalSabha_Orientation", BalSabha_Orientation),
            new SqlParameter("@BalSabha_Chart", BalSabha_Chart),
            new SqlParameter("@BalSabha_Kit", BalSabha_Kit),
            new SqlParameter("@Lifeskill_Games", Lifeskill_Games),
            new SqlParameter("@Lifeskill_Games_TB", Lifeskill_Games_TB),
            new SqlParameter("@Lifeskill_Games_FC", Lifeskill_Games_FC),
            new SqlParameter("@SACUpdate_TB", SACUpdate_TB),
            new SqlParameter("@SACUpdate_FC", SACUpdate_FC),
            new SqlParameter("@SACUpdate", SACUpdate),
            new SqlParameter("@SAC_Periodic_Checkup", SAC_Periodic_Checkup),
            new SqlParameter("@SAC_Listing_Name_Of_Girls", SAC_Listing_Name_Of_Girls),
            new SqlParameter("@SAC_Listing_Name_Of_Boys", SAC_Listing_Name_Of_Boys),
            new SqlParameter("@SAC_Girls_Left", SAC_Girls_Left),
            new SqlParameter("@SAC_Boys_Left", SAC_Boys_Left),
            new SqlParameter("@SAC_Girs_Not_Joined_School", SAC_Girs_Not_Joined_School),
            new SqlParameter("@SAC_Boys_Not_Joined_School", SAC_Boys_Not_Joined_School),
            new SqlParameter("@SAC_No_Of_Attended", SAC_No_Of_Attended),
            new SqlParameter("@Classrooms", Classrooms),
            new SqlParameter("@DrinkingWater", DrinkingWater),
            new SqlParameter("@GirlsToilet", GirlsToilet),
            new SqlParameter("@Electricity", Electricity),
            new SqlParameter("@Playground", Playground),
            new SqlParameter("@Slide", Slide),
            new SqlParameter("@BoundaryWall", BoundaryWall),
            new SqlParameter("@Kitchen", Kitchen),
            new SqlParameter("@Teachers_Male", Teachers_Male),
            new SqlParameter("@Teachers_Female", Teachers_Female),
            new SqlParameter("@CLT_Kit", CLT_Kit),
            new SqlParameter("@bookAvl", bookAvl),
            new SqlParameter("@Infrastructure_TB", Infrastructure_TB),
            new SqlParameter("@Infrastructure", Infrastructure),
            new SqlParameter("@Infrastructure_FC", Infrastructure_FC),
            new SqlParameter("@SIP_Annual_FC", SIP_Annual_FC),
            new SqlParameter("@SIP_Annual_TB", SIP_Annual_TB),
            new SqlParameter("@Retention_Annual_FC", Retention_Annual_FC),
            new SqlParameter("@Retention_Annual_TB", Retention_Annual_TB),
            new SqlParameter("@AnnualData", AnnualData),
            new SqlParameter("@SIP_Annual", SIP_Annual),
            new SqlParameter("@Retention_Annual", Retention_Annual),
            new SqlParameter("@SIP_PC", SIP_PC),
            new SqlParameter("@Retention_PC", Retention_PC),
            new SqlParameter("@LifeSkillGameEntry", LifeSkillGameEntry),
            new SqlParameter("@Others_Description", Others_Description),
            new SqlParameter("@UserEntry", UserEntry),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@ApproveBy", ApproveBy),
            new SqlParameter("@CLT_Pretest", CLT_Pretest),
            new SqlParameter("@CLT_Midtest", CLT_Midtest),
            new SqlParameter("@CLT_Posttes", CLT_Posttes),
            new SqlParameter("@Remark", Remark),
            new SqlParameter("@CreateBy", CreateBy),

            new SqlParameter("@Balsabareason", Balsabareason),
            new SqlParameter("@BalsabaType", BalsabaType),
            new SqlParameter("@SMCDirector",SMCDirector),
            new SqlParameter("@SMCRegister",SMCRegister),
            new SqlParameter("@SMC_Purpose",SMC_Purpose),
            new SqlParameter("@SMC_OtherDiscussions_Oth",SMC_OtherDiscussions_Oth),
              new SqlParameter("@ContactFCTB",ContactFCTB),
                new SqlParameter("@ContactOption",ContactOption),
                  new SqlParameter("@SchoolContactOption",SchoolContactOption),
                     new SqlParameter("@SchoolMerge",ddlMarge.SelectedValue),
                                    new SqlParameter("@SMCPresident",txtSMCPre.Text),
                       new SqlParameter("@TBCode",TBCode),
                          new SqlParameter("@ISTeamBalik",ISTeamBalik),
                             new SqlParameter("@SMCregisterismeeting",SMCregisterismeeting),
                                new SqlParameter("@SMCmeetingregister",SMCmeetingregister),
                                   new SqlParameter("@SMCWrite",SMCWrite),
                                  new SqlParameter("@SMCF5",SMCF5),
                                     new SqlParameter("@SMCIsMember",SMCIsMember),
                                      new SqlParameter("@BalSabaTBCode",BalSabaTBCode),

                                           new SqlParameter("@Other_TB",tbcoth),
                                            new SqlParameter("@Other_FC",FCcoth),

                                     new SqlParameter("@BoysToilet",BoysToilet),
                                     new SqlParameter("@WaterSupply",WaterSupply),
                                     new SqlParameter("@TilingToilet",TilingToilet),
                                     new SqlParameter("@HandicappedAccessibleToilet",HandicappedAccessibleToilet),
                                     new SqlParameter("@MultipleHandwashingUnit",MultipleHandwashingUnit),
                                     new SqlParameter("@TilingClassroomFloor",TilingClassroomFloor),
                                     new SqlParameter("@Blackboards",Blackboards),
                                     new SqlParameter("@ProperPainting",ProperPainting),
                                     new SqlParameter("@DisabledAccessibleRamp",DisabledAccessibleRamp),
                                     new SqlParameter("@AppropriateElectricalWiring",AppropriateElectricalWiring ),
                                     new SqlParameter("@BoysUrinal",BoysUrinal ),
                                     new SqlParameter("@GirlsUrinal",GirlsUrinal  ),
                                      new SqlParameter("@Furniture",Furniture  ),
                                       new SqlParameter("@TapWaterFacility ",TapWaterFacility  ),
                                          new SqlParameter("@AgendaPrepared ",ddlMeetingPrepare.SelectedValue  ),
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertUpdateActivity_School2025]", cmdParameters);
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
        if (!Validation())
            return;
        Save();
        LoadDataschool();
    }

    public int InsertUpdateAttendace(string UniqueCode, string UniqueChildRCode, string VillageCode, int childattendace, int sessiondata, string Schoolcode, string flag, string CreatedBy)
    {
        string LiffSkillTBcode = "";
        int TBFC = 0;
        if (rblLifeTB.Checked == true)
        {
            LiffSkillTBcode = ddlliffTb.SelectedValue;
        }

        if (rblLifeTB.Checked == true)
        {
            TBFC = 1;
        }
        if (rblLifeFC.Checked == true)
        {
            TBFC = 2;
        }
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Flag",flag),
            new SqlParameter("@UniqueCode",UniqueCode),
            new SqlParameter("@UniqueChildRCode", UniqueChildRCode),
            new SqlParameter("@villagecode", VillageCode),
            new SqlParameter("@childattendace", childattendace),
            new SqlParameter("@att_date",Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")),
              new SqlParameter("@sessiondata", sessiondata),
              new SqlParameter("@Schoolcode", Schoolcode),
              new SqlParameter("@CreatedBy", CreatedBy),
                new SqlParameter("@LiffSkillTBcode", LiffSkillTBcode),
                  new SqlParameter("@TBFC", TBFC),
        };


        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_InsertupdateAttendanceNew", cmdParameters);

        return result;
    }

    public DataTable CreateDataDate()
    {

        DataTable dtSubject = new DataTable();
        dtSubject.Columns.Add(new DataColumn("GUID_School", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("VillageCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("SchoolCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("ActivityDate", System.Type.GetType("System.DateTime")));
        dtSubject.Columns.Add(new DataColumn("Subject", System.Type.GetType("System.Int32")));
        dtSubject.Columns.Add(new DataColumn("CLTGroup", System.Type.GetType("System.String")));
        ViewState["dtSubject"] = dtSubject;
        return dtSubject;
    }
    public DataTable CreateDataGame()
    {

        DataTable dtGame = new DataTable();


        dtGame.Columns.Add(new DataColumn("GUID_School", System.Type.GetType("System.String")));
        dtGame.Columns.Add(new DataColumn("VillageCode", System.Type.GetType("System.String")));
        dtGame.Columns.Add(new DataColumn("SchoolCode", System.Type.GetType("System.String")));
        dtGame.Columns.Add(new DataColumn("ActivityDate", System.Type.GetType("System.DateTime")));

        dtGame.Columns.Add(new DataColumn("GameNo", System.Type.GetType("System.Int32")));
        ViewState["dtGame"] = dtGame;
        return dtGame;
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
    public void LoadData()
    {
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
        if (txtDate.Text == "")
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date')</script>", false);
            return;
        }
        if (ddlSchool.SelectedIndex <= 0)
        {
            ModalPopupExtender.Hide();
            this.ModalPopupExtender1.Hide();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select School')</script>", false);
            return;
        }


        ClearData();
        rblBalsabaFC.Checked = false;
        rblBalsabaTB.Checked = false;
        chkKit.Checked = false;
        chkChat.Checked = false;
        chkOrientation.Checked = false;
        chkBalSabhaFor.Checked = false;
        LoadDataschool();
        LoadDataschoolPre();
        if (this.ddlRemark.SelectedIndex > 0)
        {
            pnlMain.Enabled = true;
            gvGkp.Enabled = true;



        }
    }
    public DataTable BalsabaActivitySchool(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),

        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "BalsabaActivitySchool", cmdParameters);
        return dt;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ViewState["GUID_School"].ToString().Length > 5)
        {
            int res1 = 0;
            DataTable dt = BalsabaActivitySchool(ViewState["GUID_School"].ToString());
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please delete Balsabha Child')</script>", false);

            }
            else
            {
                if (ddlRemark.SelectedIndex > 0)
                {
                    res1 = objMain.DeleteD2dDataAcctivtiyAchool(ViewState["GUID_School"].ToString());
                    if (res1 > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);
                    }

                    if (chkSMC.Checked == true)
                    {


                    }
                    if (res1 > 0)
                    {
                        btnSerach_Click(btnSerach, null);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Remark')</script>", false);

                }
            }
        }
    }
    protected void lnl_click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(txt1.Text) != 0)
        {

            if (Convert.ToInt32(txt1.Text) == 4)
            {
                //txtdrinking.BackColor = Color.Green;
                txtdrinking.BackColor = Color.Blue;

                lbldriking.Text = "4";

                //  txt1.Text = "4";
            }
            if (Convert.ToInt32(txt1.Text) == 1)
            {
                txtdrinking.BackColor = Color.Green;
                lbldriking.Text = "1";
                //  txt1.Text = "1";
            }
            if (Convert.ToInt32(txt1.Text) == 2)
            {
                txtdrinking.BackColor = Color.Orange;
                lbldriking.Text = "2";
                // txt1.Text = "2";
            }
            if (Convert.ToInt32(txt1.Text) == 3)
            {
                txtdrinking.BackColor = Color.Red;
                lbldriking.Text = "3";
                //  txt1.Text = "3";

            }
        }


        if (Convert.ToInt32(txt2.Text) != 0)
        {
            if (Convert.ToInt32(txt2.Text) == 4)
            {
                txtToilet.BackColor = Color.Blue;
                //txtToilet.BackColor = Color.Green;
                lblToilet.Text = "4";

                //   txt2.Text = "4";

            }
            if (Convert.ToInt32(txt2.Text) == 1)
            {
                txtToilet.BackColor = Color.Green;
                lblToilet.Text = "1";
                // txt2.Text = "1";
            }
            if (Convert.ToInt32(txt2.Text) == 2)
            {
                txtToilet.BackColor = Color.Orange;
                lblToilet.Text = "2";
                // txt2.Text = "2";
            }
            if (Convert.ToInt32(txt2.Text) == 3)
            {
                txtToilet.BackColor = Color.Red;
                lblToilet.Text = "3";
                //   txt2.Text = "3";
            }
        }

        if (Convert.ToInt32(txt3.Text) != 0)
        {
            if (Convert.ToInt32(txt3.Text) == 4)
            {
                txtElectricity.BackColor = Color.Blue;
                lblElectricity.Text = "4";
                //  txt3.Text = "4";
            }
            if (Convert.ToInt32(txt3.Text) == 1)
            {
                txtElectricity.BackColor = Color.Green;
                lblElectricity.Text = "1";
                //  txt3.Text = "1";
            }
            if (Convert.ToInt32(txt3.Text) == 2)
            {
                txtElectricity.BackColor = Color.Orange;
                lblElectricity.Text = "2";

                //  txt3.Text = "2";
            }
            if (Convert.ToInt32(txt3.Text) == 3)
            {
                txtElectricity.BackColor = Color.Red;
                lblElectricity.Text = "3";
                //  txt3.Text = "3";
            }
        }


        if (Convert.ToInt32(txt4.Text) != 0)
        {
            if (Convert.ToInt32(txt4.Text) == 4)
            {
                txtPlay.BackColor = Color.Blue;
                lblPlay.Text = "4";
                //   txt4.Text = "4";
            }
            if (Convert.ToInt32(txt4.Text) == 1)
            {
                txtPlay.BackColor = Color.Green;
                lblPlay.Text = "1";
                //   txt4.Text = "1";
            }
            if (Convert.ToInt32(txt4.Text) == 2)
            {
                txtPlay.BackColor = Color.Orange;
                lblPlay.Text = "2";
                //  txt4.Text = "2";
            }
            if (Convert.ToInt32(txt4.Text) == 3)
            {
                txtPlay.BackColor = Color.Red;
                lblPlay.Text = "3";
                //  txt4.Text = "3";
            }
        }


        if (Convert.ToInt32(txt5.Text) != 0)
        {
            if (Convert.ToInt32(txt5.Text) == 4)
            {
                txtSlides.BackColor = Color.Blue;
                lblSlides.Text = "4";
                //   txt5.Text = "4";
            }
            if (Convert.ToInt32(txt5.Text) == 1)
            {
                txtSlides.BackColor = Color.Green;
                lblSlides.Text = "1";
                //   txt5.Text = "1";
            }
            if (Convert.ToInt32(txt5.Text) == 2)
            {
                txtSlides.BackColor = Color.Orange;
                lblSlides.Text = "2";
                //  txt5.Text = "2";
            }
            if (Convert.ToInt32(txt5.Text) == 3)
            {
                txtSlides.BackColor = Color.Red;
                lblSlides.Text = "3";
                //  txt5.Text = "3";
            }
        }

        if (Convert.ToInt32(txt6.Text) != 0)
        {
            if (Convert.ToInt32(txt6.Text) == 4)
            {
                txtBoundaryWall.BackColor = Color.Blue;
                lblBoundaryWall.Text = "4";
                //  txt6.Text = "4";

            }
            if (Convert.ToInt32(txt6.Text) == 1)
            {
                txtBoundaryWall.BackColor = Color.Green;
                lblBoundaryWall.Text = "1";
                //    txt6.Text = "1";
            }
            if (Convert.ToInt32(txt6.Text) == 2)
            {
                txtBoundaryWall.BackColor = Color.Orange;
                lblBoundaryWall.Text = "2";
                //  txt6.Text = "2";
            }
            if (Convert.ToInt32(txt6.Text) == 3)
            {
                txtBoundaryWall.BackColor = Color.Red;
                lblBoundaryWall.Text = "3";
                //  txt6.Text = "3";
            }
        }



        if (Convert.ToInt32(txt7.Text) != 0)
        {
            if (Convert.ToInt32(txt7.Text) == 4)
            {
                txtKitchen.BackColor = Color.Blue;

                lblKitchen.Text = "4";
                //   txt7.Text = "4";
            }
            if (Convert.ToInt32(txt7.Text) == 1)
            {
                txtKitchen.BackColor = Color.Green;
                lblKitchen.Text = "1";
                //   txt7.Text = "1";
            }
            if (Convert.ToInt32(txt7.Text) == 2)
            {
                txtKitchen.BackColor = Color.Orange;
                lblKitchen.Text = "2";
                // txt7.Text = "2";
            }
            if (Convert.ToInt32(txt7.Text) == 3)
            {
                txtKitchen.BackColor = Color.Red;
                lblKitchen.Text = "3";
                //   txt7.Text = "3";
            }
        }

        if (Convert.ToInt32(txt8.Text) != 0)
        {
            if (Convert.ToInt32(txt8.Text) == 4)
            {
                txtCltKit.BackColor = Color.Blue;

                lblCltKit.Text = "4";
                //  txt8.Text = "4";
            }
            if (Convert.ToInt32(txt8.Text) == 1)
            {
                txtCltKit.BackColor = Color.Green;
                lblCltKit.Text = "1";
                //  txt8.Text = "1";
            }
            if (Convert.ToInt32(txt8.Text) == 2)
            {
                txtCltKit.BackColor = Color.Orange;
                lblCltKit.Text = "2";
                //  txt8.Text = "2";
            }
            if (Convert.ToInt32(txt8.Text) == 3)
            {
                txtCltKit.BackColor = Color.Red;
                lblCltKit.Text = "3";
                //  txt8.Text = "3";
            }
        }

        if (Convert.ToInt32(txt9.Text) != 0)
        {
            if (Convert.ToInt32(txt9.Text) == 4)
            {
                txtbook.BackColor = Color.Blue;

                lblbook.Text = "4";
                //  txt9.Text = "4";
            }
            if (Convert.ToInt32(txt9.Text) == 1)
            {
                txtbook.BackColor = Color.Green;
                lblbook.Text = "1";
                // txt9.Text = "1";
            }
            if (Convert.ToInt32(txt9.Text) == 2)
            {
                txtbook.BackColor = Color.Orange;
                lblbook.Text = "2";
                //  txt9.Text = "2";
            }
            if (Convert.ToInt32(txt9.Text) == 3)
            {
                txtbook.BackColor = Color.Red;
                lblbook.Text = "3";
                // txt9.Text = "3";
            }
        }

        if (Convert.ToInt32(txt10.Text) != 0)
        {
            if (Convert.ToInt32(txt10.Text) == 4)
            {
                txtBoysToilet.BackColor = Color.Blue;
                lblBoysToilet.Text = "4";
            }
            if (Convert.ToInt32(txt10.Text) == 1)
            {
                txtBoysToilet.BackColor = Color.Green;
                lblBoysToilet.Text = "1";
            }
            if (Convert.ToInt32(txt10.Text) == 2)
            {
                txtBoysToilet.BackColor = Color.Orange;
                lblBoysToilet.Text = "2";
            }
            if (Convert.ToInt32(txt10.Text) == 3)
            {
                txtBoysToilet.BackColor = Color.Red;
                lblBoysToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(txt11.Text) != 0)
        {
            if (Convert.ToInt32(txt11.Text) == 4)
            {
                TextTapWater.BackColor = Color.Blue;
                lblWaterSupply.Text = "4";
            }
            if (Convert.ToInt32(txt11.Text) == 1)
            {
                TextTapWater.BackColor = Color.Green;
                lblWaterSupply.Text = "1";
            }
            if (Convert.ToInt32(txt11.Text) == 2)
            {
                TextTapWater.BackColor = Color.Orange;
                lblWaterSupply.Text = "2";
            }
            if (Convert.ToInt32(txt11.Text) == 3)
            {
                TextTapWater.BackColor = Color.Red;
                lblWaterSupply.Text = "3";
            }
        }

        if (Convert.ToInt32(txt12.Text) != 0)
        {
            if (Convert.ToInt32(txt12.Text) == 4)
            {
                TxtTiling.BackColor = Color.Blue;
                lblTilingToilet.Text = "4";
            }
            if (Convert.ToInt32(txt12.Text) == 1)
            {
                TxtTiling.BackColor = Color.Green;
                lblTilingToilet.Text = "1";
            }
            if (Convert.ToInt32(txt12.Text) == 2)
            {
                TxtTiling.BackColor = Color.Orange;
                lblTilingToilet.Text = "2";
            }
            if (Convert.ToInt32(txt12.Text) == 3)
            {
                TxtTiling.BackColor = Color.Red;
                lblTilingToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(txt13.Text) != 0)
        {
            if (Convert.ToInt32(txt13.Text) == 4)
            {
                txtHandicapped.BackColor = Color.Blue;
                lblHandicappedAccessibleToilet.Text = "4";
            }
            if (Convert.ToInt32(txt13.Text) == 1)
            {
                txtHandicapped.BackColor = Color.Green;
                lblHandicappedAccessibleToilet.Text = "1";
            }
            if (Convert.ToInt32(txt13.Text) == 2)
            {
                txtHandicapped.BackColor = Color.Orange;
                lblHandicappedAccessibleToilet.Text = "2";
            }
            if (Convert.ToInt32(txt13.Text) == 3)
            {
                txtHandicapped.BackColor = Color.Red;
                lblHandicappedAccessibleToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(txt14.Text) != 0)
        {
            if (Convert.ToInt32(txt14.Text) == 4)
            {
                txtMultipleHandwashing.BackColor = Color.Blue;
                lblMultipleHandwashingUnit.Text = "4";
            }
            if (Convert.ToInt32(txt14.Text) == 1)
            {
                txtMultipleHandwashing.BackColor = Color.Green;
                lblMultipleHandwashingUnit.Text = "1";
            }
            if (Convert.ToInt32(txt14.Text) == 2)
            {
                txtMultipleHandwashing.BackColor = Color.Orange;
                lblMultipleHandwashingUnit.Text = "2";
            }
            if (Convert.ToInt32(txt14.Text) == 3)
            {
                txtMultipleHandwashing.BackColor = Color.Red;
                lblMultipleHandwashingUnit.Text = "3";
            }
        }

        if (Convert.ToInt32(txt15.Text) != 0)
        {
            if (Convert.ToInt32(txt15.Text) == 4)
            {
                txtTilingclassroom.BackColor = Color.Blue;
                lblTilingClassroomFloor.Text = "4";
            }
            if (Convert.ToInt32(txt15.Text) == 1)
            {
                txtTilingclassroom.BackColor = Color.Green;
                lblTilingClassroomFloor.Text = "1";
            }
            if (Convert.ToInt32(txt15.Text) == 2)
            {
                txtTilingclassroom.BackColor = Color.Orange;
                lblTilingClassroomFloor.Text = "2";
            }
            if (Convert.ToInt32(txt15.Text) == 3)
            {
                txtTilingclassroom.BackColor = Color.Red;
                lblTilingClassroomFloor.Text = "3";
            }
        }


        if (Convert.ToInt32(txt16.Text) != 0)
        {
            if (Convert.ToInt32(txt16.Text) == 4)
            {
                txtBlackboards.BackColor = Color.Blue;
                lblBlackboards.Text = "4";
            }
            if (Convert.ToInt32(txt16.Text) == 1)
            {
                txtBlackboards.BackColor = Color.Green;
                lblBlackboards.Text = "1";
            }
            if (Convert.ToInt32(txt16.Text) == 2)
            {
                txtBlackboards.BackColor = Color.Orange;
                lblBlackboards.Text = "2";
            }
            if (Convert.ToInt32(txt16.Text) == 3)
            {
                txtBlackboards.BackColor = Color.Red;
                lblBlackboards.Text = "3";
            }
        }

        if (Convert.ToInt32(txt17.Text) != 0)
        {
            if (Convert.ToInt32(txt17.Text) == 4)
            {
                txtProperpainting.BackColor = Color.Blue;
                lblProperPainting.Text = "4";
            }
            if (Convert.ToInt32(txt17.Text) == 1)
            {
                txtProperpainting.BackColor = Color.Green;
                lblProperPainting.Text = "1";
            }
            if (Convert.ToInt32(txt17.Text) == 2)
            {
                txtProperpainting.BackColor = Color.Orange;
                lblProperPainting.Text = "2";
            }
            if (Convert.ToInt32(txt17.Text) == 3)
            {
                txtProperpainting.BackColor = Color.Red;
                lblProperPainting.Text = "3";
            }
        }


        if (Convert.ToInt32(txt18.Text) != 0)
        {
            if (Convert.ToInt32(txt18.Text) == 4)
            {
                txtDisabledaccessible.BackColor = Color.Blue;
                lblDisabledAccessibleRamp.Text = "4";
            }
            if (Convert.ToInt32(txt18.Text) == 1)
            {
                txtDisabledaccessible.BackColor = Color.Green;
                lblDisabledAccessibleRamp.Text = "1";
            }
            if (Convert.ToInt32(txt18.Text) == 2)
            {
                txtDisabledaccessible.BackColor = Color.Orange;
                lblDisabledAccessibleRamp.Text = "2";
            }
            if (Convert.ToInt32(txt18.Text) == 3)
            {
                txtDisabledaccessible.BackColor = Color.Red;
                lblDisabledAccessibleRamp.Text = "3";
            }
        }

        if (Convert.ToInt32(txt19.Text) != 0)
        {
            if (Convert.ToInt32(txt19.Text) == 4)
            {
                txtAppropriateelectrical.BackColor = Color.Blue;
                lblAppropriateElectricalWiring.Text = "4";
            }
            if (Convert.ToInt32(txt19.Text) == 1)
            {
                txtAppropriateelectrical.BackColor = Color.Green;
                lblAppropriateElectricalWiring.Text = "1";
            }
            if (Convert.ToInt32(txt19.Text) == 2)
            {
                txtAppropriateelectrical.BackColor = Color.Orange;
                lblAppropriateElectricalWiring.Text = "2";
            }
            if (Convert.ToInt32(txt19.Text) == 3)
            {
                txtAppropriateelectrical.BackColor = Color.Red;
                lblAppropriateElectricalWiring.Text = "3";
            }
        }


        if (Convert.ToInt32(txt20.Text) != 0)
        {
            if (Convert.ToInt32(txt20.Text) == 4)
            {
                txtBoysUrinal.BackColor = Color.Blue;
                lblBoysUrinal.Text = "4";
            }
            if (Convert.ToInt32(txt20.Text) == 1)
            {
                txtBoysUrinal.BackColor = Color.Green;
                lblBoysUrinal.Text = "1";
            }
            if (Convert.ToInt32(txt20.Text) == 2)
            {
                txtBoysUrinal.BackColor = Color.Orange;
                lblBoysUrinal.Text = "2";
            }
            if (Convert.ToInt32(txt20.Text) == 3)
            {
                txtBoysUrinal.BackColor = Color.Red;
                lblBoysUrinal.Text = "3";
            }
        }

        if (Convert.ToInt32(txt21.Text) != 0)
        {
            if (Convert.ToInt32(txt21.Text) == 4)
            {
                txtGirlsUrinal.BackColor = Color.Blue;
                lblGirlsUrinal.Text = "4";
            }
            if (Convert.ToInt32(txt21.Text) == 1)
            {
                txtGirlsUrinal.BackColor = Color.Green;
                lblGirlsUrinal.Text = "1";
            }
            if (Convert.ToInt32(txt21.Text) == 2)
            {
                txtGirlsUrinal.BackColor = Color.Orange;
                lblGirlsUrinal.Text = "2";
            }
            if (Convert.ToInt32(txt21.Text) == 3)
            {
                txtGirlsUrinal.BackColor = Color.Red;
                lblGirlsUrinal.Text = "3";
            }
        }

        if (Convert.ToInt32(txt22.Text) != 0)
        {
            if (Convert.ToInt32(txt22.Text) == 4)
            {
                txtFurniture.BackColor = Color.Blue;
                lblFurniture.Text = "4";
            }
            if (Convert.ToInt32(txt22.Text) == 1)
            {
                txtFurniture.BackColor = Color.Green;
                lblFurniture.Text = "1";
            }
            if (Convert.ToInt32(txt22.Text) == 2)
            {
                txtFurniture.BackColor = Color.Orange;
                lblFurniture.Text = "2";
            }
            if (Convert.ToInt32(txt22.Text) == 3)
            {
                txtFurniture.BackColor = Color.Red;
                lblFurniture.Text = "3";
            }
        }

        if (Convert.ToInt32(txt23.Text) != 0)
        {
            if (Convert.ToInt32(txt23.Text) == 4)
            {
                txtWaterStorage.BackColor = Color.Blue;
                lblTapWaterFacility.Text = "4";
            }
            if (Convert.ToInt32(txt23.Text) == 1)
            {
                txtWaterStorage.BackColor = Color.Green;
                lblTapWaterFacility.Text = "1";
            }
            if (Convert.ToInt32(txt23.Text) == 2)
            {
                txtWaterStorage.BackColor = Color.Orange;
                lblTapWaterFacility.Text = "2";
            }
            if (Convert.ToInt32(txt23.Text) == 3)
            {
                txtWaterStorage.BackColor = Color.Red;
                lblTapWaterFacility.Text = "3";
            }
        }

        txtMaleTeacher.Text = txtMaleTeacher1.Text;
        txtFemaleTeacher.Text = txtFemaleTeacher1.Text;
        txtClassRoom.Text = txtClassRoom1.Text;
    }

    public void LoadColour()
    {
        if (Convert.ToInt32(lbldriking.Text) != 0)
        {

            if (Convert.ToInt32(lbldriking.Text) == 4)
            {
                //txtdrinking.BackColor = Color.Green;
                txtdrinking.BackColor = Color.Blue;

                lbldriking.Text = "4";

                //  lbldriking.Text = "4";
            }
            if (Convert.ToInt32(lbldriking.Text) == 1)
            {
                txtdrinking.BackColor = Color.Green;
                lbldriking.Text = "1";
                //  lbldriking.Text = "1";
            }
            if (Convert.ToInt32(lbldriking.Text) == 2)
            {
                txtdrinking.BackColor = Color.Orange;
                lbldriking.Text = "2";
                // lbldriking.Text = "2";
            }
            if (Convert.ToInt32(lbldriking.Text) == 3)
            {
                txtdrinking.BackColor = Color.Red;
                lbldriking.Text = "3";
                //  lbldriking.Text = "3";

            }
        }


        if (Convert.ToInt32(lblToilet.Text) != 0)
        {
            if (Convert.ToInt32(lblToilet.Text) == 4)
            {
                txtToilet.BackColor = Color.Blue;
                //txtToilet.BackColor = Color.Green;
                lblToilet.Text = "4";

                //   lblToilet.Text = "4";

            }
            if (Convert.ToInt32(lblToilet.Text) == 1)
            {
                txtToilet.BackColor = Color.Green;
                lblToilet.Text = "1";
                // lblToilet.Text = "1";
            }
            if (Convert.ToInt32(lblToilet.Text) == 2)
            {
                txtToilet.BackColor = Color.Orange;
                lblToilet.Text = "2";
                // lblToilet.Text = "2";
            }
            if (Convert.ToInt32(lblToilet.Text) == 3)
            {
                txtToilet.BackColor = Color.Red;
                lblToilet.Text = "3";
                //   lblToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(lblElectricity.Text) != 0)
        {
            if (Convert.ToInt32(lblElectricity.Text) == 4)
            {
                txtElectricity.BackColor = Color.Blue;
                lblElectricity.Text = "4";
                //  lblElectricity.Text = "4";
            }
            if (Convert.ToInt32(lblElectricity.Text) == 1)
            {
                txtElectricity.BackColor = Color.Green;
                lblElectricity.Text = "1";
                //  lblElectricity.Text = "1";
            }
            if (Convert.ToInt32(lblElectricity.Text) == 2)
            {
                txtElectricity.BackColor = Color.Orange;
                lblElectricity.Text = "2";

                //  lblElectricity.Text = "2";
            }
            if (Convert.ToInt32(lblElectricity.Text) == 3)
            {
                txtElectricity.BackColor = Color.Red;
                lblElectricity.Text = "3";
                //  lblElectricity.Text = "3";
            }
        }


        if (Convert.ToInt32(lblPlay.Text) != 0)
        {
            if (Convert.ToInt32(lblPlay.Text) == 4)
            {
                txtPlay.BackColor = Color.Blue;
                lblPlay.Text = "4";
                //   lblPlay.Text = "4";
            }
            if (Convert.ToInt32(lblPlay.Text) == 1)
            {
                txtPlay.BackColor = Color.Green;
                lblPlay.Text = "1";
                //   lblPlay.Text = "1";
            }
            if (Convert.ToInt32(lblPlay.Text) == 2)
            {
                txtPlay.BackColor = Color.Orange;
                lblPlay.Text = "2";
                //  lblPlay.Text = "2";
            }
            if (Convert.ToInt32(lblPlay.Text) == 3)
            {
                txtPlay.BackColor = Color.Red;
                lblPlay.Text = "3";
                //  lblPlay.Text = "3";
            }
        }


        if (Convert.ToInt32(lblSlides.Text) != 0)
        {
            if (Convert.ToInt32(lblSlides.Text) == 4)
            {
                txtSlides.BackColor = Color.Blue;
                lblSlides.Text = "4";
                //   lblSlides.Text = "4";
            }
            if (Convert.ToInt32(lblSlides.Text) == 1)
            {
                txtSlides.BackColor = Color.Green;
                lblSlides.Text = "1";
                //   lblSlides.Text = "1";
            }
            if (Convert.ToInt32(lblSlides.Text) == 2)
            {
                txtSlides.BackColor = Color.Orange;
                lblSlides.Text = "2";
                //  lblSlides.Text = "2";
            }
            if (Convert.ToInt32(lblSlides.Text) == 3)
            {
                txtSlides.BackColor = Color.Red;
                lblSlides.Text = "3";
                //  lblSlides.Text = "3";
            }
        }

        if (Convert.ToInt32(lblBoundaryWall.Text) != 0)
        {
            if (Convert.ToInt32(lblBoundaryWall.Text) == 4)
            {
                txtBoundaryWall.BackColor = Color.Blue;
                lblBoundaryWall.Text = "4";
                //  lblBoundaryWall.Text = "4";

            }
            if (Convert.ToInt32(lblBoundaryWall.Text) == 1)
            {
                txtBoundaryWall.BackColor = Color.Green;
                lblBoundaryWall.Text = "1";
                //    lblBoundaryWall.Text = "1";
            }
            if (Convert.ToInt32(lblBoundaryWall.Text) == 2)
            {
                txtBoundaryWall.BackColor = Color.Orange;
                lblBoundaryWall.Text = "2";
                //  lblBoundaryWall.Text = "2";
            }
            if (Convert.ToInt32(lblBoundaryWall.Text) == 3)
            {
                txtBoundaryWall.BackColor = Color.Red;
                lblBoundaryWall.Text = "3";
                //  lblBoundaryWall.Text = "3";
            }
        }



        if (Convert.ToInt32(lblKitchen.Text) != 0)
        {
            if (Convert.ToInt32(lblKitchen.Text) == 4)
            {
                txtKitchen.BackColor = Color.Blue;

                lblKitchen.Text = "4";
                //   lblKitchen.Text = "4";
            }
            if (Convert.ToInt32(lblKitchen.Text) == 1)
            {
                txtKitchen.BackColor = Color.Green;
                lblKitchen.Text = "1";
                //   lblKitchen.Text = "1";
            }
            if (Convert.ToInt32(lblKitchen.Text) == 2)
            {
                txtKitchen.BackColor = Color.Orange;
                lblKitchen.Text = "2";
                // lblKitchen.Text = "2";
            }
            if (Convert.ToInt32(lblKitchen.Text) == 3)
            {
                txtKitchen.BackColor = Color.Red;
                lblKitchen.Text = "3";
                //   lblKitchen.Text = "3";
            }
        }

        if (Convert.ToInt32(lblCltKit.Text) != 0)
        {
            if (Convert.ToInt32(lblCltKit.Text) == 4)
            {
                txtCltKit.BackColor = Color.Blue;

                lblCltKit.Text = "4";
                //  lblCltKit.Text = "4";
            }
            if (Convert.ToInt32(lblCltKit.Text) == 1)
            {
                txtCltKit.BackColor = Color.Green;
                lblCltKit.Text = "1";
                //  lblCltKit.Text = "1";
            }
            if (Convert.ToInt32(lblCltKit.Text) == 2)
            {
                txtCltKit.BackColor = Color.Orange;
                lblCltKit.Text = "2";
                //  lblCltKit.Text = "2";
            }
            if (Convert.ToInt32(lblCltKit.Text) == 3)
            {
                txtCltKit.BackColor = Color.Red;
                lblCltKit.Text = "3";
                //  lblCltKit.Text = "3";
            }
        }

        if (Convert.ToInt32(lblbook.Text) != 0)
        {
            if (Convert.ToInt32(lblbook.Text) == 4)
            {
                txtbook.BackColor = Color.Blue;

                lblbook.Text = "4";
                //  lblbook.Text = "4";
            }
            if (Convert.ToInt32(lblbook.Text) == 1)
            {
                txtbook.BackColor = Color.Green;
                lblbook.Text = "1";
                // lblbook.Text = "1";
            }
            if (Convert.ToInt32(lblbook.Text) == 2)
            {
                txtbook.BackColor = Color.Orange;
                lblbook.Text = "2";
                //  lblbook.Text = "2";
            }
            if (Convert.ToInt32(lblbook.Text) == 3)
            {
                txtbook.BackColor = Color.Red;
                lblbook.Text = "3";
                // lblbook.Text = "3";
            }
        }

        if (Convert.ToInt32(lblBoysToilet.Text) != 0)
        {
            if (Convert.ToInt32(lblBoysToilet.Text) == 4)
            {
                txtBoysToilet.BackColor = Color.Blue;
                lblBoysToilet.Text = "4";
            }
            if (Convert.ToInt32(lblBoysToilet.Text) == 1)
            {
                txtBoysToilet.BackColor = Color.Green;
                lblBoysToilet.Text = "1";
            }
            if (Convert.ToInt32(lblBoysToilet.Text) == 2)
            {
                txtBoysToilet.BackColor = Color.Orange;
                lblBoysToilet.Text = "2";
            }
            if (Convert.ToInt32(lblBoysToilet.Text) == 3)
            {
                txtBoysToilet.BackColor = Color.Red;
                lblBoysToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(lblWaterSupply.Text) != 0)
        {
            if (Convert.ToInt32(lblWaterSupply.Text) == 4)
            {
                TextTapWater.BackColor = Color.Blue;
                lblWaterSupply.Text = "4";
            }
            if (Convert.ToInt32(lblWaterSupply.Text) == 1)
            {
                TextTapWater.BackColor = Color.Green;
                lblWaterSupply.Text = "1";
            }
            if (Convert.ToInt32(lblWaterSupply.Text) == 2)
            {
                TextTapWater.BackColor = Color.Orange;
                lblWaterSupply.Text = "2";
            }
            if (Convert.ToInt32(lblWaterSupply.Text) == 3)
            {
                TextTapWater.BackColor = Color.Red;
                lblWaterSupply.Text = "3";
            }
        }

        if (Convert.ToInt32(lblTilingToilet.Text) != 0)
        {
            if (Convert.ToInt32(lblTilingToilet.Text) == 4)
            {
                TxtTiling.BackColor = Color.Blue;
                lblTilingToilet.Text = "4";
            }
            if (Convert.ToInt32(lblTilingToilet.Text) == 1)
            {
                TxtTiling.BackColor = Color.Green;
                lblTilingToilet.Text = "1";
            }
            if (Convert.ToInt32(lblTilingToilet.Text) == 2)
            {
                TxtTiling.BackColor = Color.Orange;
                lblTilingToilet.Text = "2";
            }
            if (Convert.ToInt32(lblTilingToilet.Text) == 3)
            {
                TxtTiling.BackColor = Color.Red;
                lblTilingToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(lblHandicappedAccessibleToilet.Text) != 0)
        {
            if (Convert.ToInt32(lblHandicappedAccessibleToilet.Text) == 4)
            {
                txtHandicapped.BackColor = Color.Blue;
                lblHandicappedAccessibleToilet.Text = "4";
            }
            if (Convert.ToInt32(lblHandicappedAccessibleToilet.Text) == 1)
            {
                txtHandicapped.BackColor = Color.Green;
                lblHandicappedAccessibleToilet.Text = "1";
            }
            if (Convert.ToInt32(lblHandicappedAccessibleToilet.Text) == 2)
            {
                txtHandicapped.BackColor = Color.Orange;
                lblHandicappedAccessibleToilet.Text = "2";
            }
            if (Convert.ToInt32(lblHandicappedAccessibleToilet.Text) == 3)
            {
                txtHandicapped.BackColor = Color.Red;
                lblHandicappedAccessibleToilet.Text = "3";
            }
        }

        if (Convert.ToInt32(lblMultipleHandwashingUnit.Text) != 0)
        {
            if (Convert.ToInt32(lblMultipleHandwashingUnit.Text) == 4)
            {
                txtMultipleHandwashing.BackColor = Color.Blue;
                lblMultipleHandwashingUnit.Text = "4";
            }
            if (Convert.ToInt32(lblMultipleHandwashingUnit.Text) == 1)
            {
                txtMultipleHandwashing.BackColor = Color.Green;
                lblMultipleHandwashingUnit.Text = "1";
            }
            if (Convert.ToInt32(lblMultipleHandwashingUnit.Text) == 2)
            {
                txtMultipleHandwashing.BackColor = Color.Orange;
                lblMultipleHandwashingUnit.Text = "2";
            }
            if (Convert.ToInt32(lblMultipleHandwashingUnit.Text) == 3)
            {
                txtMultipleHandwashing.BackColor = Color.Red;
                lblMultipleHandwashingUnit.Text = "3";
            }
        }

        if (Convert.ToInt32(lblTilingClassroomFloor.Text) != 0)
        {
            if (Convert.ToInt32(lblTilingClassroomFloor.Text) == 4)
            {
                txtTilingclassroom.BackColor = Color.Blue;
                lblTilingClassroomFloor.Text = "4";
            }
            if (Convert.ToInt32(lblTilingClassroomFloor.Text) == 1)
            {
                txtTilingclassroom.BackColor = Color.Green;
                lblTilingClassroomFloor.Text = "1";
            }
            if (Convert.ToInt32(lblTilingClassroomFloor.Text) == 2)
            {
                txtTilingclassroom.BackColor = Color.Orange;
                lblTilingClassroomFloor.Text = "2";
            }
            if (Convert.ToInt32(lblTilingClassroomFloor.Text) == 3)
            {
                txtTilingclassroom.BackColor = Color.Red;
                lblTilingClassroomFloor.Text = "3";
            }
        }


        if (Convert.ToInt32(lblBlackboards.Text) != 0)
        {
            if (Convert.ToInt32(lblBlackboards.Text) == 4)
            {
                txtBlackboards.BackColor = Color.Blue;
                lblBlackboards.Text = "4";
            }
            if (Convert.ToInt32(lblBlackboards.Text) == 1)
            {
                txtBlackboards.BackColor = Color.Green;
                lblBlackboards.Text = "1";
            }
            if (Convert.ToInt32(lblBlackboards.Text) == 2)
            {
                txtBlackboards.BackColor = Color.Orange;
                lblBlackboards.Text = "2";
            }
            if (Convert.ToInt32(lblBlackboards.Text) == 3)
            {
                txtBlackboards.BackColor = Color.Red;
                lblBlackboards.Text = "3";
            }
        }

        if (Convert.ToInt32(lblProperPainting.Text) != 0)
        {
            if (Convert.ToInt32(lblProperPainting.Text) == 4)
            {
                txtProperpainting.BackColor = Color.Blue;
                lblProperPainting.Text = "4";
            }
            if (Convert.ToInt32(lblProperPainting.Text) == 1)
            {
                txtProperpainting.BackColor = Color.Green;
                lblProperPainting.Text = "1";
            }
            if (Convert.ToInt32(lblProperPainting.Text) == 2)
            {
                txtProperpainting.BackColor = Color.Orange;
                lblProperPainting.Text = "2";
            }
            if (Convert.ToInt32(lblProperPainting.Text) == 3)
            {
                txtProperpainting.BackColor = Color.Red;
                lblProperPainting.Text = "3";
            }
        }


        if (Convert.ToInt32(lblDisabledAccessibleRamp.Text) != 0)
        {
            if (Convert.ToInt32(lblDisabledAccessibleRamp.Text) == 4)
            {
                txtDisabledaccessible.BackColor = Color.Blue;
                lblDisabledAccessibleRamp.Text = "4";
            }
            if (Convert.ToInt32(lblDisabledAccessibleRamp.Text) == 1)
            {
                txtDisabledaccessible.BackColor = Color.Green;
                lblDisabledAccessibleRamp.Text = "1";
            }
            if (Convert.ToInt32(lblDisabledAccessibleRamp.Text) == 2)
            {
                txtDisabledaccessible.BackColor = Color.Orange;
                lblDisabledAccessibleRamp.Text = "2";
            }
            if (Convert.ToInt32(lblDisabledAccessibleRamp.Text) == 3)
            {
                txtDisabledaccessible.BackColor = Color.Red;
                lblDisabledAccessibleRamp.Text = "3";
            }
        }

        if (Convert.ToInt32(lblAppropriateElectricalWiring.Text) != 0)
        {
            if (Convert.ToInt32(lblAppropriateElectricalWiring.Text) == 4)
            {
                txtAppropriateelectrical.BackColor = Color.Blue;
                lblAppropriateElectricalWiring.Text = "4";
            }
            if (Convert.ToInt32(lblAppropriateElectricalWiring.Text) == 1)
            {
                txtAppropriateelectrical.BackColor = Color.Green;
                lblAppropriateElectricalWiring.Text = "1";
            }
            if (Convert.ToInt32(lblAppropriateElectricalWiring.Text) == 2)
            {
                txtAppropriateelectrical.BackColor = Color.Orange;
                lblAppropriateElectricalWiring.Text = "2";
            }
            if (Convert.ToInt32(lblAppropriateElectricalWiring.Text) == 3)
            {
                txtAppropriateelectrical.BackColor = Color.Red;
                lblAppropriateElectricalWiring.Text = "3";
            }
        }


        if (Convert.ToInt32(lblBoysUrinal.Text) != 0)
        {
            if (Convert.ToInt32(lblBoysUrinal.Text) == 4)
            {
                txtBoysUrinal.BackColor = Color.Blue;
                lblBoysUrinal.Text = "4";
            }
            if (Convert.ToInt32(lblBoysUrinal.Text) == 1)
            {
                txtBoysUrinal.BackColor = Color.Green;
                lblBoysUrinal.Text = "1";
            }
            if (Convert.ToInt32(lblBoysUrinal.Text) == 2)
            {
                txtBoysUrinal.BackColor = Color.Orange;
                lblBoysUrinal.Text = "2";
            }
            if (Convert.ToInt32(lblBoysUrinal.Text) == 3)
            {
                txtBoysUrinal.BackColor = Color.Red;
                lblBoysUrinal.Text = "3";
            }
        }

        if (Convert.ToInt32(lblGirlsUrinal.Text) != 0)
        {
            if (Convert.ToInt32(lblGirlsUrinal.Text) == 4)
            {
                txtGirlsUrinal.BackColor = Color.Blue;
                lblGirlsUrinal.Text = "4";
            }
            if (Convert.ToInt32(lblGirlsUrinal.Text) == 1)
            {
                txtGirlsUrinal.BackColor = Color.Green;
                lblGirlsUrinal.Text = "1";
            }
            if (Convert.ToInt32(lblGirlsUrinal.Text) == 2)
            {
                txtGirlsUrinal.BackColor = Color.Orange;
                lblGirlsUrinal.Text = "2";
            }
            if (Convert.ToInt32(lblGirlsUrinal.Text) == 3)
            {
                txtGirlsUrinal.BackColor = Color.Red;
                lblGirlsUrinal.Text = "3";
            }
        }

        if (Convert.ToInt32(lblFurniture.Text) != 0)
        {
            if (Convert.ToInt32(lblFurniture.Text) == 4)
            {
                txtFurniture.BackColor = Color.Blue;
                lblFurniture.Text = "4";
            }
            if (Convert.ToInt32(lblFurniture.Text) == 1)
            {
                txtFurniture.BackColor = Color.Green;
                lblFurniture.Text = "1";
            }
            if (Convert.ToInt32(lblFurniture.Text) == 2)
            {
                txtFurniture.BackColor = Color.Orange;
                lblFurniture.Text = "2";
            }
            if (Convert.ToInt32(lblFurniture.Text) == 3)
            {
                txtFurniture.BackColor = Color.Red;
                lblFurniture.Text = "3";
            }
        }

        if (Convert.ToInt32(lblTapWaterFacility.Text) != 0)
        {
            if (Convert.ToInt32(lblTapWaterFacility.Text) == 4)
            {
                txtWaterStorage.BackColor = Color.Blue;
                lblTapWaterFacility.Text = "4";
            }
            if (Convert.ToInt32(lblTapWaterFacility.Text) == 1)
            {
                txtWaterStorage.BackColor = Color.Green;
                lblTapWaterFacility.Text = "1";
            }
            if (Convert.ToInt32(lblTapWaterFacility.Text) == 2)
            {
                txtWaterStorage.BackColor = Color.Orange;
                lblTapWaterFacility.Text = "2";
            }
            if (Convert.ToInt32(lblTapWaterFacility.Text) == 3)
            {
                txtWaterStorage.BackColor = Color.Red;
                lblTapWaterFacility.Text = "3";
            }
        }

    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        string query = "   select isnull(SchoolLevel,0) as SchoolLevel,WorkingStatus ,ManagementType,isnull(LSG,0) as LSG from mstSchool   where SchoolCode='" + this.ddlSchool.SelectedValue + "' ";


        DataTable dataTable2 = this.objMain.LoadData(query);
        if (dataTable2.Rows.Count > 0)
        {
            Session["SchoolLevel"] = dataTable2.Rows[0]["SchoolLevel"].ToString();
            Session["LSG"] = dataTable2.Rows[0]["SchoolLevel"].ToString();

        }
        LoadData();

        LoadSMC();

    }


    protected void rblTbr_Click(object sender, EventArgs e)
    {
        string Dateof = txtDate.Text;

        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        string query = "   select *from tblSMCAttendanceChild   where SchoolCode='" + this.ddlSchool.SelectedValue + "' and DeleteFlag=1 and tblSMCAttendanceChild.ActivityDate =('" + FcDate + "')  ";
        DataTable dataTable2 = this.objMain.LoadData(query);
        if (dataTable2.Rows.Count > 0)
        {

        }
        else
        {
            string conq = " tblSMCAttendanceNew.Schoolcode='" + ddlSchool.SelectedValue + "'  ";
            DataTable dtGKP = LoadSMCDeatilsnew(conq, "6");
            if (dtGKP.Rows.Count > 0)
            {
                Session["dtmc"] = dtGKP;
                gvSmc.DataSource = dtGKP;
                gvSmc.DataBind();
                int GCount = 0;
                int MCount = 0;
                DataRow[] dr = dtGKP.Select("Gender='2'");

                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        GCount = GCount + 1;
                    }
                }
                DataRow[] dr1 = dtGKP.Select("Gender='1'");
                if (dr1.Length > 0)
                {
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        MCount = MCount + 1;
                    }
                }

                string kk = dtGKP.Rows.Count.ToString();
                txtTotalMember.Text = kk;
                txtTotalFmember.Text = GCount.ToString();
                lblTottal.Text = kk;
                lblFemale.Text = GCount.ToString();
                lblmale.Text = MCount.ToString();

            }
        }
    }
    public DataTable LoadSMCDeatilsnew(string WhereQuery, string Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Schoolcode", ddlSchool.SelectedValue)    ,
            new SqlParameter("@con", WhereQuery)    ,
                new SqlParameter("@Flag", Flag)    ,

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSMCDeatilsNew2025Pre]", cmdParameters);
    }
    public void LoadSMC()
    {
        if (txtDate.Text.Length > 0)
        {
            string Dateof = txtDate.Text;

            string[] b = Dateof.Split('/');

            string FcDate = b[2] + '-' + b[1] + '-' + b[0];
            string conq = "tblSMCAttendanceChild.ActivityDate =('" + FcDate + "')    and tblSMCAttendanceChild.Schoolcode='" + ddlSchool.SelectedValue + "'  ";
            DataTable dtGKP = LoadSMCDeatils(conq, "6");
            if (dtGKP.Rows.Count > 0)
            {
                Session["dtmc"] = dtGKP;
                gvSmc.DataSource = dtGKP;
                gvSmc.DataBind();
                int GCount = 0;
                int MCount = 0;
                DataRow[] dr = dtGKP.Select("Gender='2'  and Present=1");

                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        GCount = GCount + 1;
                    }
                }
                DataRow[] dr1 = dtGKP.Select("Gender='1'  and Present=1");
                if (dr1.Length > 0)
                {
                    for (int i = 0; i < dr1.Length; i++)
                    {
                        MCount = MCount + 1;
                    }
                }
                int total = GCount + MCount;

                string kk = dtGKP.Rows.Count.ToString();
                txtTotalMember.Text = total.ToString();
                txtTotalFmember.Text = GCount.ToString();
                lblTottal.Text = total.ToString();
                lblFemale.Text = GCount.ToString();
                lblmale.Text = MCount.ToString();

            }
        }
    }
    public void LoadDataschoolPre()
    {
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

        string strQry = "";

        string userid = "";
        if (Session["user_level"].ToString() == "19")
        {
            userid = "2";
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            userid = "3";
        }
        string query = "   select isnull(SchoolLevel,0) as SchoolLevel,WorkingStatus ,ManagementType from mstSchool   where SchoolCode='" + this.ddlSchool.SelectedValue + "' ";

        DataTable dataTable2 = this.objMain.LoadData(query);
        ViewState["SchoolLevel"] = dataTable2.Rows[0]["SchoolLevel"].ToString();
        //if (dataTable2.Rows[0]["WorkingStatus"].ToString() == "1")
        //{
        //    //pnlSmc.Enabled = true;
        //    //pnlClt.Enabled = true;
        //    //pnlBalshaba.Enabled = true;
        //    //pnlSACUpdate.Enabled = true;
        //    //pnlinfrastructure.Enabled = true;
        //    //pnlAnnual.Enabled = true;

        //}
        //else
        //{
        //    pnlSmc.Enabled = false;
        //    pnlClt.Enabled = false;
        //    pnlBalshaba.Enabled = false;
        //    pnlSACUpdate.Enabled = false;
        //    pnlinfrastructure.Enabled = false;
        //    pnlAnnual.Enabled = false;
        //    return;
        //}

        SqlParameter[] parm = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@UserEntry",userid),

                 };
        //  LoadSchoolActivityPreviousData
        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityPreviousData]", parm);


        SqlParameter[] parm44 = new SqlParameter[]
            {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@UserEntry",userid),

                };
        //  LoadSchoolActivityPreviousData
        DataTable dtStartVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityStartData]", parm44);
        if (dtUserVillage.Rows.Count > 0)
        {
            #region Priveous Colours

            if (Convert.ToString(dtUserVillage.Rows[0]["SMCPresident"]) != "")
            {
                txtSMCPre.Text = dtUserVillage.Rows[0]["SMCPresident"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Classrooms"].ToString()) != 0)
            {
                txtClassRoom1.Text = dtUserVillage.Rows[0]["Classrooms"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Teachers_Female"].ToString()) != 0)
            {
                txtFemaleTeacher1.Text = dtUserVillage.Rows[0]["Teachers_Female"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Teachers_Male"].ToString()) != 0)
            {
                txtMaleTeacher1.Text = dtUserVillage.Rows[0]["Teachers_Male"].ToString();
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) != 0)
            {

                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 4)
                {
                    //txtdrinking.BackColor = Color.Green;
                    txtdrinking1.BackColor = Color.Blue;

                    txt1.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 1)
                {
                    txtdrinking1.BackColor = Color.Green;
                    txt1.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 2)
                {
                    txtdrinking1.BackColor = Color.Orange;
                    txt1.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 3)
                {
                    txtdrinking1.BackColor = Color.Red;
                    txt1.Text = "3";

                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 4)
                {
                    txtToilet1.BackColor = Color.Blue;
                    txt2.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 1)
                {
                    txtToilet1.BackColor = Color.Green;
                    txt2.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 2)
                {
                    txtToilet1.BackColor = Color.Orange;
                    txt2.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 3)
                {
                    txtToilet1.BackColor = Color.Red;
                    txt2.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 4)
                {
                    txtElectricity1.BackColor = Color.Blue;
                    txt3.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 1)
                {
                    txtElectricity1.BackColor = Color.Green;
                    txt3.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 2)
                {
                    txtElectricity1.BackColor = Color.Orange;
                    txt3.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 3)
                {
                    txtElectricity1.BackColor = Color.Red;
                    txt3.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 4)
                {
                    txtPlay1.BackColor = Color.Blue;
                    txt4.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 1)
                {
                    txtPlay1.BackColor = Color.Green;
                    txt4.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 2)
                {
                    txtPlay1.BackColor = Color.Orange;
                    txt4.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 3)
                {
                    txtPlay1.BackColor = Color.Red;
                    txt4.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 4)
                {
                    txtSlides1.BackColor = Color.Blue;
                    txt5.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 1)
                {
                    txtSlides1.BackColor = Color.Green;
                    txt5.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 2)
                {
                    txtSlides1.BackColor = Color.Orange;
                    txt5.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 3)
                {
                    txtSlides1.BackColor = Color.Red;

                    txt5.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 4)
                {
                    txtBoundaryWall1.BackColor = Color.Blue;
                    txt6.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 1)
                {
                    txtBoundaryWall1.BackColor = Color.Green;
                    txt6.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 2)
                {
                    txtBoundaryWall1.BackColor = Color.Orange;
                    txt6.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 3)
                {
                    txtBoundaryWall1.BackColor = Color.Red;
                    txt6.Text = "3";
                }
            }



            if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 4)
                {
                    txtKitchen1.BackColor = Color.Blue;
                    txt7.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 1)
                {
                    txtKitchen1.BackColor = Color.Green;
                    txt7.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 2)
                {
                    txtKitchen1.BackColor = Color.Orange;
                    txt7.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 3)
                {
                    txtKitchen1.BackColor = Color.Red;

                    txt7.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 4)
                {
                    txtCltKit1.BackColor = Color.Blue;
                    txt8.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 1)
                {
                    txtCltKit1.BackColor = Color.Green;
                    txt8.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 2)
                {
                    txtCltKit1.BackColor = Color.Orange;
                    txt8.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 3)
                {
                    txtCltKit1.BackColor = Color.Red;
                    txt8.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 4)
                {
                    txtbook1.BackColor = Color.Blue;
                    txt9.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 1)
                {
                    txtbook1.BackColor = Color.Green;
                    txt9.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 2)
                {
                    txtbook1.BackColor = Color.Orange;
                    txt9.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 3)
                {
                    txtbook1.BackColor = Color.Red;
                    txt9.Text = "3";
                }
            }

            #region new fields
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 4)
                {
                    txtBoysToilet1.BackColor = Color.Blue;
                    txt10.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 1)
                {
                    txtBoysToilet1.BackColor = Color.Green;
                    txt10.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 2)
                {
                    txtBoysToilet1.BackColor = Color.Orange;
                    txt10.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 3)
                {
                    txtBoysToilet1.BackColor = Color.Red;
                    txt10.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 4)
                {
                    TextTapWater1.BackColor = Color.Blue;
                    txt11.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 1)
                {
                    TextTapWater1.BackColor = Color.Green;
                    txt11.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 2)
                {
                    TextTapWater1.BackColor = Color.Orange;
                    txt11.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 3)
                {
                    TextTapWater1.BackColor = Color.Red;
                    txt11.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 4)
                {
                    TxtTiling1.BackColor = Color.Blue;
                    txt12.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 1)
                {
                    TxtTiling1.BackColor = Color.Green;
                    txt12.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 2)
                {
                    TxtTiling1.BackColor = Color.Orange;
                    txt12.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 3)
                {
                    TxtTiling1.BackColor = Color.Red;
                    txt12.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 4)
                {
                    txtHandicapped1.BackColor = Color.Blue;
                    txt13.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 1)
                {
                    txtHandicapped1.BackColor = Color.Green;
                    txt13.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 2)
                {
                    txtHandicapped1.BackColor = Color.Orange;
                    txt13.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 3)
                {
                    txtHandicapped1.BackColor = Color.Red;
                    txt13.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 4)
                {
                    txtMultipleHandwashing1.BackColor = Color.Blue;
                    txt14.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 1)
                {
                    txtMultipleHandwashing1.BackColor = Color.Green;
                    txt14.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 2)
                {
                    txtMultipleHandwashing1.BackColor = Color.Orange;
                    txt14.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 3)
                {
                    txtMultipleHandwashing1.BackColor = Color.Red;
                    txt14.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 4)
                {
                    txtTilingclassroom1.BackColor = Color.Blue;
                    txt15.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 1)
                {
                    txtTilingclassroom1.BackColor = Color.Green;
                    txt15.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 2)
                {
                    txtTilingclassroom1.BackColor = Color.Orange;
                    txt15.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 3)
                {
                    txtTilingclassroom1.BackColor = Color.Red;
                    txt15.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 4)
                {
                    txtBlackboards1.BackColor = Color.Blue;
                    txt16.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 1)
                {
                    txtBlackboards1.BackColor = Color.Green;
                    txt16.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 2)
                {
                    txtBlackboards1.BackColor = Color.Orange;
                    txt16.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 3)
                {
                    txtBlackboards1.BackColor = Color.Red;
                    txt16.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 4)
                {
                    txtProperpainting1.BackColor = Color.Blue;
                    txt17.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 1)
                {
                    txtProperpainting1.BackColor = Color.Green;
                    txt17.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 2)
                {
                    txtProperpainting1.BackColor = Color.Orange;
                    txt17.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 3)
                {
                    txtProperpainting1.BackColor = Color.Red;
                    txt17.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 4)
                {
                    txtDisabledaccessible1.BackColor = Color.Blue;
                    txt18.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 1)
                {
                    txtDisabledaccessible1.BackColor = Color.Green;
                    txt18.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 2)
                {
                    txtDisabledaccessible1.BackColor = Color.Orange;
                    txt18.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 3)
                {
                    txtDisabledaccessible1.BackColor = Color.Red;
                    txt18.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 4)
                {
                    txtAppropriateelectrical1.BackColor = Color.Blue;
                    txt19.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 1)
                {
                    txtAppropriateelectrical1.BackColor = Color.Green;
                    txt19.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 2)
                {
                    txtAppropriateelectrical1.BackColor = Color.Orange;
                    txt19.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 3)
                {
                    txtAppropriateelectrical1.BackColor = Color.Red;
                    txt19.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 4)
                {
                    txtBoysUrinal1.BackColor = Color.Blue;
                    txt20.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 1)
                {
                    txtBoysUrinal1.BackColor = Color.Green;
                    txt20.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 2)
                {
                    txtBoysUrinal1.BackColor = Color.Orange;
                    txt20.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 3)
                {
                    txtBoysUrinal1.BackColor = Color.Red;
                    txt20.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 4)
                {
                    txtGirlsUrinal1.BackColor = Color.Blue;
                    txt21.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 1)
                {
                    txtGirlsUrinal1.BackColor = Color.Green;
                    txt21.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 2)
                {
                    txtGirlsUrinal1.BackColor = Color.Orange;
                    txt21.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 3)
                {
                    txtGirlsUrinal1.BackColor = Color.Red;
                    txt21.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 4)
                {
                    txtFurniture1.BackColor = Color.Blue;
                    txt22.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 1)
                {
                    txtFurniture1.BackColor = Color.Green;
                    txt22.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 2)
                {
                    txtFurniture1.BackColor = Color.Orange;
                    txt22.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 3)
                {
                    txtFurniture1.BackColor = Color.Red;
                    txt22.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 4)
                {
                    txtWaterStorage1.BackColor = Color.Blue;
                    txt23.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 1)
                {
                    txtWaterStorage1.BackColor = Color.Green;
                    txt23.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 2)
                {
                    txtWaterStorage1.BackColor = Color.Orange;
                    txt23.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 3)
                {
                    txtWaterStorage1.BackColor = Color.Red;
                    txt23.Text = "3";
                }
            }

            #endregion


            #endregion
        }

        if (dtStartVillage.Rows.Count > 0)
        {
            #region Priveous Colours
            if (Convert.ToInt32(dtStartVillage.Rows[0]["Classrooms"].ToString()) != 0)
            {
                txtClassRoom2.Text = dtStartVillage.Rows[0]["Classrooms"].ToString();
            }
            if (Convert.ToInt32(dtStartVillage.Rows[0]["Teachers_Female"].ToString()) != 0)
            {
                txtFemaleTeacher2.Text = dtStartVillage.Rows[0]["Teachers_Female"].ToString();
            }
            if (Convert.ToInt32(dtStartVillage.Rows[0]["Teachers_Male"].ToString()) != 0)
            {
                txtMaleTeacher2.Text = dtStartVillage.Rows[0]["Teachers_Male"].ToString();
            }


            if (Convert.ToInt32(dtStartVillage.Rows[0]["DrinkingWater"].ToString()) != 0)
            {

                if (Convert.ToInt32(dtStartVillage.Rows[0]["DrinkingWater"].ToString()) == 4)
                {
                    //txtdrinking.BackColor = Color.Green;
                    txtdrinking2.BackColor = Color.Blue;


                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["DrinkingWater"].ToString()) == 1)
                {
                    txtdrinking2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["DrinkingWater"].ToString()) == 2)
                {
                    txtdrinking2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["DrinkingWater"].ToString()) == 3)
                {
                    txtdrinking2.BackColor = Color.Red;

                }
            }

            if (Convert.ToInt32(dtStartVillage.Rows[0]["GirlsToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["GirlsToilet"].ToString()) == 4)
                {
                    txtToilet2.BackColor = Color.Blue;


                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["GirlsToilet"].ToString()) == 1)
                {
                    txtToilet2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["GirlsToilet"].ToString()) == 2)
                {
                    txtToilet2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["GirlsToilet"].ToString()) == 3)
                {
                    txtToilet2.BackColor = Color.Red;

                }
            }

            if (Convert.ToInt32(dtStartVillage.Rows[0]["Electricity"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Electricity"].ToString()) == 4)
                {
                    txtElectricity2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Electricity"].ToString()) == 1)
                {
                    txtElectricity2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Electricity"].ToString()) == 2)
                {
                    txtElectricity2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Electricity"].ToString()) == 3)
                {
                    txtElectricity2.BackColor = Color.Red;

                }
            }


            if (Convert.ToInt32(dtStartVillage.Rows[0]["Playground"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Playground"].ToString()) == 4)
                {
                    txtPlay2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Playground"].ToString()) == 1)
                {
                    txtPlay2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Playground"].ToString()) == 2)
                {
                    txtPlay2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Playground"].ToString()) == 3)
                {
                    txtPlay2.BackColor = Color.Red;

                }
            }


            if (Convert.ToInt32(dtStartVillage.Rows[0]["Slide"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Slide"].ToString()) == 4)
                {
                    txtSlides2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Slide"].ToString()) == 1)
                {
                    txtSlides2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Slide"].ToString()) == 2)
                {
                    txtSlides2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Slide"].ToString()) == 3)
                {
                    txtSlides2.BackColor = Color.Red;


                }
            }

            if (Convert.ToInt32(dtStartVillage.Rows[0]["BoundaryWall"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["BoundaryWall"].ToString()) == 4)
                {
                    txtBoundaryWall2.BackColor = Color.Blue;


                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["BoundaryWall"].ToString()) == 1)
                {
                    txtBoundaryWall2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["BoundaryWall"].ToString()) == 2)
                {
                    txtBoundaryWall2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["BoundaryWall"].ToString()) == 3)
                {
                    txtBoundaryWall2.BackColor = Color.Red;

                }
            }



            if (Convert.ToInt32(dtStartVillage.Rows[0]["Kitchen"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Kitchen"].ToString()) == 4)
                {
                    txtKitchen2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Kitchen"].ToString()) == 1)
                {
                    txtKitchen2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Kitchen"].ToString()) == 2)
                {
                    txtKitchen2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Kitchen"].ToString()) == 3)
                {
                    txtKitchen2.BackColor = Color.Red;


                }
            }

            if (Convert.ToInt32(dtStartVillage.Rows[0]["CLT_Kit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["CLT_Kit"].ToString()) == 4)
                {
                    txtCltKit2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["CLT_Kit"].ToString()) == 1)
                {
                    txtCltKit2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["CLT_Kit"].ToString()) == 2)
                {
                    txtCltKit2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["CLT_Kit"].ToString()) == 3)
                {
                    txtCltKit2.BackColor = Color.Red;

                }
            }

            if (Convert.ToInt32(dtStartVillage.Rows[0]["Books"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Books"].ToString()) == 4)
                {
                    txtbook2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Books"].ToString()) == 1)
                {
                    txtbook2.BackColor = Color.Green;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Books"].ToString()) == 2)
                {
                    txtbook2.BackColor = Color.Orange;

                }
                if (Convert.ToInt32(dtStartVillage.Rows[0]["Books"].ToString()) == 3)
                {
                    txtbook2.BackColor = Color.Red;

                }
            }

            #region new fields
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 4)
                {
                    txtBoysToilet2.BackColor = Color.Blue;

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 1)
                {
                    txtBoysToilet2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 2)
                {
                    txtBoysToilet2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 3)
                {
                    txtBoysToilet2.BackColor = Color.Red;
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 4)
                {
                    TextTapWater2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 1)
                {
                    TextTapWater2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 2)
                {
                    TextTapWater2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 3)
                {
                    TextTapWater2.BackColor = Color.Red;
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 4)
                {
                    TxtTiling2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 1)
                {
                    TxtTiling2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 2)
                {
                    TxtTiling2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 3)
                {
                    TxtTiling2.BackColor = Color.Red;
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 4)
                {
                    txtHandicapped2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 1)
                {
                    txtHandicapped2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 2)
                {
                    txtHandicapped2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 3)
                {
                    txtHandicapped2.BackColor = Color.Red;
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 4)
                {
                    txtMultipleHandwashing2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 1)
                {
                    txtMultipleHandwashing2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 2)
                {
                    txtMultipleHandwashing2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 3)
                {
                    txtMultipleHandwashing2.BackColor = Color.Red;
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 4)
                {
                    txtTilingclassroom2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 1)
                {
                    txtTilingclassroom2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 2)
                {
                    txtTilingclassroom2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 3)
                {
                    txtTilingclassroom2.BackColor = Color.Red;
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 4)
                {
                    txtBlackboards2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 1)
                {
                    txtBlackboards2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 2)
                {
                    txtBlackboards2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 3)
                {
                    txtBlackboards2.BackColor = Color.Red;
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 4)
                {
                    txtProperpainting2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 1)
                {
                    txtProperpainting2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 2)
                {
                    txtProperpainting2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 3)
                {
                    txtProperpainting2.BackColor = Color.Red;
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 4)
                {
                    txtDisabledaccessible2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 1)
                {
                    txtDisabledaccessible2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 2)
                {
                    txtDisabledaccessible2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 3)
                {
                    txtDisabledaccessible2.BackColor = Color.Red;
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 4)
                {
                    txtAppropriateelectrical2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 1)
                {
                    txtAppropriateelectrical2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 2)
                {
                    txtAppropriateelectrical2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 3)
                {
                    txtAppropriateelectrical2.BackColor = Color.Red;
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 4)
                {
                    txtBoysUrinal2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 1)
                {
                    txtBoysUrinal2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 2)
                {
                    txtBoysUrinal2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 3)
                {
                    txtBoysUrinal2.BackColor = Color.Red;
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 4)
                {
                    txtGirlsUrinal2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 1)
                {
                    txtGirlsUrinal2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 2)
                {
                    txtGirlsUrinal2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 3)
                {
                    txtGirlsUrinal2.BackColor = Color.Red;
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 4)
                {
                    txtFurniture2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 1)
                {
                    txtFurniture2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 2)
                {
                    txtFurniture2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 3)
                {
                    txtFurniture2.BackColor = Color.Red;
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 4)
                {
                    txtWaterStorage2.BackColor = Color.Blue;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 1)
                {
                    txtWaterStorage2.BackColor = Color.Green;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 2)
                {
                    txtWaterStorage2.BackColor = Color.Orange;
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 3)
                {
                    txtWaterStorage2.BackColor = Color.Red;
                }
            }

            #endregion

            #endregion
        }
        SqlParameter[] parm1 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",b[2] ),
               new SqlParameter("@ActivityDateNew",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),

                 };
        DataTable dtActivtyPreTest = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityPreTestCheck]", parm1);


        if (dtActivtyPreTest.Rows.Count > 0)
        {

            rblCompletePost.Enabled = true;
            rblCompleteMid.Enabled = true;
            rblPartialPost.Enabled = true;
            rblPartialMid.Enabled = true;
            rblTestPostFC.Enabled = true;
            rblTestMidFC.Enabled = true;
            rblTestTBPost.Enabled = true;
            rblTestTBMid.Enabled = true;
            rblCompletePre.Enabled = false;
            rblPartialPre.Enabled = false;
            rblTestpreFC.Enabled = false;
            rblTestTBPre.Enabled = false;
            ImageButton11.Enabled = true;

        }
        else
        {
            rblTestMidFC.Enabled = false;
            rblTestTBPost.Enabled = false;
            rblPartialPost.Enabled = false;
            rblPartialMid.Enabled = false;
            rblCompletePost.Enabled = false;
            rblCompleteMid.Enabled = false;
            ImageButton11.Enabled = false;

            rblCompletePre.Enabled = true;
            rblPartialPre.Enabled = true;
            rblTestpreFC.Enabled = true;
            rblTestTBPre.Enabled = true;
        }

        SqlParameter[] parm3 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",b[2] ),
               new SqlParameter("@ActivityDateNew",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),

                 };
        DataTable dtActivtyPreTest1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivityPreTestCheckMid]", parm3);

        if (dtActivtyPreTest1.Rows.Count > 0)
        {
            rblTestTBPre.Enabled = false;
            rblTestTBMid.Enabled = false;

            rblTestpreFC.Enabled = false;
            rblTestMidFC.Enabled = false;
            rblPartialPre.Enabled = false;
            rblPartialMid.Enabled = false;
            rblCompletePre.Enabled = false;
            rblCompleteMid.Enabled = false;

            rblCompletePost.Enabled = true;
            rblPartialPost.Enabled = true;
            rblTestPostFC.Enabled = true;
            rblTestTBPost.Enabled = true;
        }

        this.pnlSmc.Enabled = false;
        this.pnlClt.Enabled = false;
        this.pnlBalshaba.Enabled = false;
        this.pnlSACUpdate.Enabled = false;
        this.pnlinfrastructure.Enabled = false;
        pnlSchoolContact.Enabled = false;
        this.pnlAnnual.Enabled = false;
        rblIMPossiblie.Enabled = false;
        rblPossiblie.Enabled = false;
        chkOrientation.Enabled = false;
        chkChat.Enabled = false;
        chkKit.Enabled = false;
        pnlBalshaba.Enabled = false;
        rblBalsabaTB.Enabled = false;
        rblBalsabaTB.Enabled = false;
        chkBalsabha.Enabled = false;
        ddlreasons.Enabled = false;
        if (ddlMarge.SelectedIndex > 0)
        {

            if (dataTable2.Rows[0]["SchoolLevel"].ToString() == "1" && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                pnlBalshaba.Enabled = false;
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                //Anuj     //this.pnlBalshaba.Enabled = false;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

            }
            else if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "3" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4") && Convert.ToInt32(ddlMarge.SelectedValue) == 1 && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                pnlBalshaba.Enabled = false;
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                //Anuj     //this.pnlBalshaba.Enabled = false;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

            }

            else if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "3" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4") && Convert.ToInt32(ddlMarge.SelectedValue) == 2 && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                this.pnlBalshaba.Enabled = true;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;


                rblIMPossiblie.Enabled = true;
                rblPossiblie.Enabled = true;
                chkOrientation.Enabled = true;
                chkChat.Enabled = true;
                chkKit.Enabled = true;
                pnlBalshaba.Enabled = true;
                rblBalsabaTB.Enabled = true;
                rblBalsabaTB.Enabled = true;
                chkBalsabha.Enabled = true;
                ddlreasons.Enabled = true;
            }
            else if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "2") && Convert.ToInt32(ddlMarge.SelectedValue) == 1 && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                this.pnlBalshaba.Enabled = true;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

                rblIMPossiblie.Enabled = true;
                rblPossiblie.Enabled = true;
                chkOrientation.Enabled = true;
                chkChat.Enabled = true;
                chkKit.Enabled = true;
                pnlBalshaba.Enabled = true;
                rblBalsabaTB.Enabled = true;
                rblBalsabaTB.Enabled = true;
                chkBalsabha.Enabled = true;
                ddlreasons.Enabled = true;
            }
            else if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "2" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "3" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "10" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "11") && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                this.pnlBalshaba.Enabled = true;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

                rblIMPossiblie.Enabled = true;
                rblPossiblie.Enabled = true;
                chkOrientation.Enabled = true;
                chkChat.Enabled = true;
                chkKit.Enabled = true;
                pnlBalshaba.Enabled = true;
                rblBalsabaTB.Enabled = true;
                rblBalsabaTB.Enabled = true;
                chkBalsabha.Enabled = true;
                ddlreasons.Enabled = true;
            }

            //if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "2" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "10" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "11") && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1" )
            //{
            //    pnlBalshaba.Enabled = true;
            //    rblBalsabaTB.Enabled = true;
            //    rblBalsabaTB.Enabled = true;
            //    chkBalsabha.Enabled = true;
            //}
            //else
            //{
            //    pnlBalshaba.Enabled = false;
            //    rblBalsabaTB.Enabled = false;
            //    rblBalsabaTB.Enabled = false;
            //    chkBalsabha.Enabled = false;
            //}
        }
        else
        {
            //if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "3" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4") && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            //{
            //    this.pnlSmc.Enabled = false;
            //    this.pnlClt.Enabled = false;
            //    pnlBalshaba.Enabled = false;
            //    //Anuj     //this.pnlBalshaba.Enabled = false;
            //    this.pnlSACUpdate.Enabled = false;
            //    this.pnlinfrastructure.Enabled = false;
            //    pnlSchoolContact.Enabled = false;
            //    this.pnlAnnual.Enabled = true;
            //}
            if (dataTable2.Rows[0]["SchoolLevel"].ToString() == "1" && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                pnlBalshaba.Enabled = false;
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                //Anuj     //this.pnlBalshaba.Enabled = false;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

            }
            else if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "2" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "10" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "11") && dataTable2.Rows[0]["WorkingStatus"].ToString() != "2" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                this.pnlBalshaba.Enabled = true;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

                rblIMPossiblie.Enabled = true;
                rblPossiblie.Enabled = true;
                chkOrientation.Enabled = true;
                chkChat.Enabled = true;
                chkKit.Enabled = true;
                pnlBalshaba.Enabled = true;
                rblBalsabaTB.Enabled = true;
                rblBalsabaTB.Enabled = true;
                chkBalsabha.Enabled = true;
                ddlreasons.Enabled = true;
            }
            else if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "2" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "10" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "11") && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1")
            {
                this.pnlSmc.Enabled = true;
                this.pnlClt.Enabled = true;
                this.pnlBalshaba.Enabled = true;
                this.pnlSACUpdate.Enabled = true;
                this.pnlinfrastructure.Enabled = true;
                pnlSchoolContact.Enabled = true;

                rblIMPossiblie.Enabled = true;
                rblPossiblie.Enabled = true;
                chkOrientation.Enabled = true;
                chkChat.Enabled = true;
                chkKit.Enabled = true;
                pnlBalshaba.Enabled = true;
                rblBalsabaTB.Enabled = true;
                rblBalsabaTB.Enabled = true;
                chkBalsabha.Enabled = true;
                ddlreasons.Enabled = true;
            }
            //if ((dataTable2.Rows[0]["SchoolLevel"].ToString() == "2" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "4" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "5" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "10" || dataTable2.Rows[0]["SchoolLevel"].ToString() == "11") && dataTable2.Rows[0]["WorkingStatus"].ToString() == "1" && dataTable2.Rows[0]["ManagementType"].ToString() == "1" && Session["BAlVal"].ToString() == "1")
            //{
            //    pnlBalshaba.Enabled = true;
            //    rblBalsabaTB.Enabled = true;
            //    rblBalsabaTB.Enabled = true;
            //    chkBalsabha.Enabled = true;
            //}
            //else
            //{
            //    pnlBalshaba.Enabled = false;
            //    rblBalsabaTB.Enabled = false;
            //    rblBalsabaTB.Enabled = false;
            //    chkBalsabha.Enabled = false;
            //}
        }

        int month = 0;

        if (txtDate.Text != "")
        {
            month = Convert.ToInt32(b[1]);
        }


        SqlParameter[] parm5 = new SqlParameter[]
          {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                 new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),


              };
        DataSet dtSACUpdateAll = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckSACUpdateBackData]", parm5);
        if (dtSACUpdateAll.Tables[0].Rows.Count > 0)
        {
            #region SAC

            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Periodic_Checkup"].ToString()) != 0)
            //{
            txtPrvHealth.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Periodic_Checkup"].ToString();

            //}
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_No_Of_Attended"].ToString()) != 0)
            //{
            txtPreSMCMeeting.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_No_Of_Attended"].ToString();

            // }
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Listing_Name_Of_Girls"].ToString()) != 0)
            //{
            txtPreAdgirls.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();
            //}
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Listing_Name_Of_Boys"].ToString()) != 0)
            //{
            txtPrvAdBoy.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();
            //}
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Girls_Left"].ToString()) != 0)
            //{
            txtPrvleftGirl.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Girls_Left"].ToString();
            // }
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Boys_Left"].ToString()) != 0)
            //{
            txtPrevleftBoy.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Boys_Left"].ToString();
            //}
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            //{
            txtPrvGirlNot.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Girls_Not_Joined_School"].ToString();
            // }
            //if (Convert.ToInt32(dtSACUpdateAll.Tables[0].Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            //{
            txtprvBoyNot.Text = dtSACUpdateAll.Tables[0].Rows[0]["SAC_Boys_Not_Joined_School"].ToString();
            //}

            //divSafe.Style(
            //  divSafe.Attributes.Add.Style("background-color: #090;");
            // divSafe.Attributes.Add('style','color:green');






            #endregion
        }




        if (month == 7 || month == 10 || month == 11 || month == 1 || month == 3)
        {
            SqlParameter[] parm4 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",b[1]),


                 };
            DataTable dtSACUpdate = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckSACUpdate]", parm4);
            if (dtSACUpdate.Rows.Count > 0)
            {
                if (ViewState["GUID_School"].ToString() == "")
                {
                    this.pnlSACUpdate.Enabled = false;
                }
                else
                {
                    this.pnlSACUpdate.Enabled = true;
                }
            }
            else
            {
                this.pnlSACUpdate.Enabled = true;
            }

            txtSMCMeeting.Enabled = false;
            txtSepSMCMeeting.Enabled = false;
            txtDescSMCMeeting.Enabled = false;
            txtMarSMCMeeting.Enabled = false;
            txtHealth.Enabled = false;
            txtSepHealth.Enabled = false;
            txtDescHealth.Enabled = false;
            txtMarHealth.Enabled = false;

            txtAdgirls.Enabled = false;
            txtsepAdgirls.Enabled = false;
            txtDescAdgirls.Enabled = false;
            txtMarAdgirls.Enabled = false;

            txtAdBoy.Enabled = false;
            txtSepAdBoy.Enabled = false;
            txtDescAdBoy.Enabled = false;
            txtMarAdBoy.Enabled = false;

            txtleftGirl.Enabled = false;
            txtSepleftGirl.Enabled = false;
            txtDescleftGirl.Enabled = false;
            txtMarleftGirl.Enabled = false;
            txtleftBoy.Enabled = false;
            txtSepleftBoy.Enabled = false;
            txtdescleftBoy.Enabled = false;
            txtMarleftBoy.Enabled = false;
            txtGirlNot.Enabled = false;
            txtSepGirlNot.Enabled = false;
            txtDescGirlNot.Enabled = false;
            txtMarGirlNot.Enabled = false;

            txtBoyNot.Enabled = false;
            txtSepBoyNot.Enabled = false;
            txtDecBoyNot.Enabled = false;
            txtMarBoyNot.Enabled = false;
            if (month == 7)
            {
                txtSMCMeeting.Enabled = true;
                txtAdgirls.Enabled = true;
                txtHealth.Enabled = true;
                txtAdBoy.Enabled = true;
                txtleftGirl.Enabled = true;
                txtleftBoy.Enabled = true;
                txtGirlNot.Enabled = true;
                txtBoyNot.Enabled = true;
            }
            if (month == 10 || month == 11)
            {
                txtSepSMCMeeting.Enabled = true;
                txtsepAdgirls.Enabled = true;
                txtSepHealth.Enabled = true;
                txtSepAdBoy.Enabled = true;
                txtSepleftGirl.Enabled = true;
                txtSepleftBoy.Enabled = true;
                txtSepGirlNot.Enabled = true;
                txtSepBoyNot.Enabled = true;
            }

            if (month == 1)
            {
                txtDescSMCMeeting.Enabled = true;
                txtDescAdgirls.Enabled = true;
                txtDescHealth.Enabled = true;
                txtDescAdBoy.Enabled = true;
                txtDescleftGirl.Enabled = true;
                txtdescleftBoy.Enabled = true;
                txtDescGirlNot.Enabled = true;
                txtDecBoyNot.Enabled = true;
            }
            if (month == 3)
            {
                txtMarSMCMeeting.Enabled = true;
                txtMarAdgirls.Enabled = true;
                txtMarHealth.Enabled = true;
                txtMarAdBoy.Enabled = true;
                txtMarleftGirl.Enabled = true;
                txtMarleftBoy.Enabled = true;
                txtMarGirlNot.Enabled = true;
                txtMarBoyNot.Enabled = true;
            }

        }
        else
        {
            this.pnlSACUpdate.Enabled = false;
        }

        if (dtSACUpdateAll.Tables[1].Rows.Count > 0)
        {
            #region SAC
            if (Convert.ToDateTime(FcDate) != Convert.ToDateTime(dtSACUpdateAll.Tables[1].Rows[0]["ActivityDate"]))
            {

                txtHealth.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Periodic_Checkup"].ToString();


                txtSMCMeeting.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_No_Of_Attended"].ToString();


                txtAdgirls.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtAdBoy.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtleftGirl.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Girls_Left"].ToString();

                txtleftBoy.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Boys_Left"].ToString();

                txtGirlNot.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtBoyNot.Text = dtSACUpdateAll.Tables[1].Rows[0]["SAC_Boys_Not_Joined_School"].ToString();
                if (month == 7)
                {
                    pnlSACUpdate.Enabled = false;
                }
            }

            #endregion
        }



        if (dtSACUpdateAll.Tables[2].Rows.Count > 0)
        {
            #region SAC

            if (Convert.ToDateTime(FcDate) != Convert.ToDateTime(dtSACUpdateAll.Tables[2].Rows[0]["ActivityDate"]))
            {

                txtSepHealth.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Periodic_Checkup"].ToString();

                txtSepSMCMeeting.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_No_Of_Attended"].ToString();

                txtsepAdgirls.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtSepAdBoy.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtSepleftGirl.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Girls_Left"].ToString();

                txtSepleftBoy.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Boys_Left"].ToString();

                txtSepGirlNot.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtSepBoyNot.Text = dtSACUpdateAll.Tables[2].Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

                if (month == 10 || month == 11)
                {
                    pnlSACUpdate.Enabled = false;
                }
            }
            #endregion
        }

        if (dtSACUpdateAll.Tables[3].Rows.Count > 0)
        {
            #region SAC
            if (Convert.ToDateTime(FcDate) != Convert.ToDateTime(dtSACUpdateAll.Tables[3].Rows[0]["ActivityDate"]))
            {


                txtDescHealth.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Periodic_Checkup"].ToString();


                txtDescSMCMeeting.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_No_Of_Attended"].ToString();


                txtDescAdgirls.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtDescAdBoy.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtDescleftGirl.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Girls_Left"].ToString();

                txtdescleftBoy.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Boys_Left"].ToString();

                txtDescGirlNot.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtDecBoyNot.Text = dtSACUpdateAll.Tables[3].Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

                if (month == 1)
                {
                    pnlSACUpdate.Enabled = false;
                }
            }


            #endregion
        }

        if (dtSACUpdateAll.Tables[4].Rows.Count > 0)
        {
            #region SAC
            if (Convert.ToDateTime(FcDate) != Convert.ToDateTime(dtSACUpdateAll.Tables[4].Rows[0]["ActivityDate"]))
            {


                txtMarHealth.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Periodic_Checkup"].ToString();


                txtMarSMCMeeting.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_No_Of_Attended"].ToString();


                txtMarAdgirls.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtMarAdBoy.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtMarleftGirl.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Girls_Left"].ToString();

                txtMarleftBoy.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Boys_Left"].ToString();

                txtMarGirlNot.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtMarBoyNot.Text = dtSACUpdateAll.Tables[4].Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

                if (month == 3)
                {
                    pnlSACUpdate.Enabled = false;
                }
            }

            #endregion
        }
        SqlParameter[] parm7 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
              new SqlParameter("@Flag",  "4"),

                 };


        DataTable dtSACKidd = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckBalSabaChat]", parm7);
        //if (dtSACKidd.Rows.Count > 0)
        //{
        //    pnlLife.Enabled = true;
        //}
        //else
        //{
        //    pnlLife.Enabled = false;
        //}
        if (Session["user_level"].ToString() == "19")
        {
            userid = "FC";
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            userid = "B";
        }
        SqlParameter[] parm6 = new SqlParameter[]
             {

               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),

                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
              new SqlParameter("@Flag",  "1"),

                   new SqlParameter("@UserApprove",  userid),

                 };
        DataSet dtSACKiddNew = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptCheckBalSabaChatNew]", parm6);
        DataTable dtB = dtSACKiddNew.Tables[0];
        DataTable dtC = dtSACKiddNew.Tables[1];
        DataTable dtD = dtSACKiddNew.Tables[2];
        DataTable dtE = dtSACKiddNew.Tables[3];
        DataTable dtF = dtSACKiddNew.Tables[4];

        DataTable dtF55 = dtSACKiddNew.Tables[5];
        if (dtF55.Rows.Count > 0)
        {
            if (dtF55.Rows[0]["BalsabaType"].ToString() == "1")
            {
                rblPossiblie.Checked = true;
                Group1_CheckedChanged(rblPossiblie, null);
            }
            if (dtF55.Rows[0]["BalsabaType"].ToString() == "2")
            {
                rblIMPossiblie.Checked = true;
                Group1_CheckedChanged(rblPossiblie, null);
                ddlreasons.SelectedValue = dtF55.Rows[0]["Balsabareason"].ToString();
                ddlreasons.Enabled = false;
            }
            if (dtF55.Rows[0]["BalSabha"].ToString() == "1")
            {
                chkBalsabha.Checked = true;
                chkBalsabha.Enabled = false;
            }

            if (dtF55.Rows[0]["BalSabha_TB"].ToString() == "1")
            {
                rblBalsabaTB.Checked = true;
                rblBalsabaTB.Enabled = false;
            }
            if (dtF55.Rows[0]["BalSabha_FC"].ToString() == "1")
            {
                rblBalsabaFC.Checked = true;
                rblBalsabaTB.Enabled = false;
            }


        }
        int BalChat = 0;
        int BalOrg = 0;
        if (dtB.Rows.Count > 0)
        {
            if (dtB.Rows[0]["BalSabha_Orientation"].ToString() == "1")
            {
                chkOrientation.Checked = true;
                chkOrientation.Enabled = false;
                BalOrg = 1;
            }
            else
            {
                chkOrientation.Enabled = true;
                chkOrientation.Checked = false;
            }
            rblIMPossiblie.Enabled = false;
        }
        if (dtC.Rows.Count > 0)
        {
            if (dtC.Rows[0]["BalSabha_Chart"].ToString() == "1")
            {
                chkChat.Checked = true;
                chkChat.Enabled = false;
                BalChat = 1;
            }
            else
            {
                chkChat.Checked = false;
                chkChat.Enabled = true;
            }
            rblIMPossiblie.Enabled = false;
        }
        if (dtE.Rows.Count > 0)
        {
            rblIMPossiblie.Enabled = false;
            rblPossiblie.Enabled = false;
        }
        //if (BalChat == 1 && BalOrg == 1)
        //{
        //    chkBalSabhaFor.Checked = true;

        //}
        if (dtF.Rows.Count > 0)
        {
            chkKit.Checked = true;
            chkKit.Enabled = false;
        }
        else
        {
            //  chkKit.Checked = false;
        }
        if (dtD.Rows.Count > 0)
        {
            rblIMPossiblie.Enabled = false;
            rblPossiblie.Enabled = false;
        }
    }
    public void LoadDataschool()
    {
        string Dateof = txtDate.Text;
        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];

        string strQry = "";
        string conq = "";

        string userid = "";
        if (Convert.ToString(Session["user_level"]) != "")
        {

            if (Session["user_level"].ToString() == "19")
            {
                userid = "2";
                conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and ApproveStatus='FC' ";

            }
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                userid = "3";
                conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and ApproveStatus='B' ";

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        SqlParameter[] parm = new SqlParameter[]
             {
               new SqlParameter("@villagecode",  ddlVilage.SelectedValue),
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
                new SqlParameter("@User",ddlUser.SelectedValue),
                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@UserEntry",userid),

                 };

        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivityUpdateDataNew]", parm);

        #region check session value

        string wherecon = "", wherecon1 = "";
        string wherecon3 = "";
        wherecon = " where villagecode= '" + ddlVilage.SelectedValue + "' and schoolcode = '" + ddlSchool.SelectedValue + "'  and userentry=" + userid + " and  BalSabha_Orientation>0   and ( BalSabha_Orientation>0 or BalSabha_Chart>0)";

        wherecon1 = " where villagecode= '" + ddlVilage.SelectedValue + "' and schoolcode = '" + ddlSchool.SelectedValue + "' and userentry=" + userid + " and  BalSabha_Orientation>0  and BalSabha_Chart>0";
        wherecon3 = " where villagecode= '" + ddlVilage.SelectedValue + "' and schoolcode = '" + ddlSchool.SelectedValue + "' and userentry=" + userid + " and  BalSabha_Orientation>0";


        SqlParameter[] parm1 = new SqlParameter[]
             {
               new SqlParameter("@wherecon",  wherecon),
               new SqlParameter("@wherecon1",  wherecon1),

      };

        DataSet DtUserdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Sp_UserActivityDetail]", parm1);


        #endregion



        //   strQry = "   select * from tblActivityUpdate_School where VillageCode='" + ddlVilage.SelectedValue + "' and SchoolCode='" + ddlSchool.SelectedValue + "' and ActivityDate= '" + Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd") + "'  ";
        // DataTable dtUserVillage = objMain.LoadData(strQry);



        //DataTable dtGKP = objMain.LoadGKPDeatils(conq);
        //if (dtGKP.Rows.Count > 0)
        //{
        //    gvGkp.DataSource = dtGKP;
        //    gvGkp.DataBind();
        //}
        //else
        //{
        //    gvGkp.DataSource = dtGKP;
        //    gvGkp.DataBind();
        //}
        if (dtUserVillage.Rows.Count > 0)
        {

            if (dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "B" || dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "FC" || dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "I")
            {

                if (Session["user_level"].ToString() == "19" && dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "FC")
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    if (dtUserVillage.Rows[0]["ApproveStatus"].ToString() == "B")
                    {
                        btnsave.Visible = true;
                    }
                    else
                    {
                        btnsave.Visible = false;
                    }
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting_FC"].ToString()) == 1)
            {

                rblSMCFC.Checked = true;
            }
            idImage.Visible = false;
            lblMM.Text = "";
            ViewState["GUID_School"] = dtUserVillage.Rows[0]["GUID_School"].ToString();
            idImage.Visible = false;
            lblMM.Text = "";
            if (dtUserVillage.Rows[0]["Photo"].ToString().Length > 3)
            {
                imgComm2.Visible = true;
                lblCom1.Text = dtUserVillage.Rows[0]["Photo"].ToString();
            }
            if (dtUserVillage.Rows[0]["MtgPhoto"].ToString().Length > 3)
            {
                imgComm22.Visible = true;
                lblCom22.Text = dtUserVillage.Rows[0]["MtgPhoto"].ToString();
            }


            if (Convert.ToString(dtUserVillage.Rows[0]["SMCPresident"]) != "")
            {
                txtSMCPre.Text = dtUserVillage.Rows[0]["SMCPresident"].ToString();
            }

            #region "SMC"
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting"].ToString()) == 1)
            {
                chkSMC.Checked = true;
            }
            rblTb_Click(rblApprove, null);
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting_TB"].ToString()) == 1)
            {

                rblSMCTB.Checked = true;
                rblTb_Click(rblApprove, null);
                ddlGssTbname.SelectedValue = dtUserVillage.Rows[0]["TBCodesmc"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TBMeet"].ToString()) == 1)
            {
                rdTeamY.Checked = true;
                rblisTb_Click(rblApprove, null);
                ddlMMTb.SelectedValue = dtUserVillage.Rows[0]["TBCodesmc"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TBMeet"].ToString()) == 2)
            {
                rdTeamN.Checked = true;
            }

            ddlrec.SelectedValue = dtUserVillage.Rows[0]["MtgDetails"].ToString();
            ddlDatemeeting.SelectedValue = dtUserVillage.Rows[0]["MtgDetInRegister"].ToString();
            ddlWrite.SelectedValue = dtUserVillage.Rows[0]["MtgResolutions"].ToString();
            ddlF5.SelectedValue = dtUserVillage.Rows[0]["Signature"].ToString();
            txtmembers.Text = dtUserVillage.Rows[0]["MembersPresent"].ToString();
            ddlMeetingPrepare.SelectedValue = dtUserVillage.Rows[0]["AgendaPrepared"].ToString();
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Meeting_FC"].ToString()) == 1)
            {

                rblSMCFC.Checked = true;
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Other_TB"].ToString()) == 1)
            {

                rblothertb.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Other_FC"].ToString()) == 1)
            {

                rblotherfc.Checked = true;
            }


            txtOther.Text = dtUserVillage.Rows[0]["Others_Description"].ToString();






            string cmeeting = dtUserVillage.Rows[0]["SMC_Purpose"].ToString();

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
            string cmeeting1 = dtUserVillage.Rows[0]["SMC_OtherDiscussions"].ToString();

            string[] meeting1 = cmeeting1.Split(',');
            string TextMeeeting1 = "";
            foreach (string s in meeting1)
            {
                foreach (ListItem item in CBL_bookformat1.Items)
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
                txt_pbname1.Text = TextMeeeting1;

            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_OtherSIP"].ToString()) >= 0)
            {
                txtOtherSIPFC.Text = dtUserVillage.Rows[0]["SMC_OtherSIP"].ToString();
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_Mtg"].ToString()) >= 0)
            {
                txtsmcmeetinFC.Text = dtUserVillage.Rows[0]["SMC_Mtg"].ToString();
            }

            ddlMarge.SelectedValue = dtUserVillage.Rows[0]["SchoolMerge"].ToString();
            #endregion

            #region SmcOrient
            TxtSmcOther.Text = dtUserVillage.Rows[0]["SMC_OtherDiscussions_Oth"].ToString();
            if (TxtSmcOther.Text.Length > 1)
            {

                TxtSmcOther.Enabled = true;
            }
            else
            {
                TxtSmcOther.Enabled = false;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC"].ToString()) == 1)
            {
                chkNewSmc.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_TB"].ToString()) == 1)
            {

                rblSmcNew.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_FC"].ToString()) == 1)
            {

                rblSmcNew1.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMC_TotTrained"].ToString()) != 0)
            {
                txtTotalMember.Text = dtUserVillage.Rows[0]["SMC_TotTrained"].ToString();

            }

            if (TextMeeeting1.Length > 0)
            {
                txtTotalFmember.Text = dtUserVillage.Rows[0]["SMC_FemaleTrained"].ToString();
            }
            #endregion




            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMCDirector"].ToString()) == 1)
            {
                rdPSMCPY.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMCDirector"].ToString()) == 2)
            {
                rdPSMCPN.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMCRegister"].ToString()) == 1)
            {
                rdRegisterY.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SMCRegister"].ToString()) == 2)
            {
                rdRegisterN.Checked = true;
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT"].ToString()) == 1)
            {
                chkClT.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_TB"].ToString()) == 1)
            {

                rblCLTTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_FC"].ToString()) == 1)
            {

                rblCLTFC.Checked = true;
            }
            #region Subject

            string CltHindi = dtUserVillage.Rows[0]["CLTHindi"].ToString();
            string CLTMath = dtUserVillage.Rows[0]["CLTMath"].ToString();
            string CLTEnglish = dtUserVillage.Rows[0]["CLTEnglish"].ToString();
            string[] parts = CltHindi.Split(',');
            string[] parts1 = CLTMath.Split(',');
            string[] parts3 = CLTEnglish.Split(',');
            foreach (string part in parts)
            {

                if (part == "A")
                {
                    chkHindiA.Checked = true;
                }
                if (part == "B")
                {
                    chkHindiB.Checked = true;
                }
                if (part == "C")
                {
                    chkHindiC.Checked = true;
                }
                if (part == "D")
                {
                    chkHindiD.Checked = true;
                }
                if (part == "E")
                {
                    chkHindiE.Checked = true;
                }
            }
            foreach (string part1 in parts1)
            {
                if (part1 == "A")
                {
                    chkEnglishA.Checked = true;
                }
                if (part1 == "B")
                {
                    chkEnglishB.Checked = true;
                }
                if (part1 == "C")
                {
                    chkEnglishC.Checked = true;
                }
                if (part1 == "D")
                {
                    chkEnglishD.Checked = true;
                }
                if (part1 == "E")
                {
                    chkEnglishE.Checked = true;
                }
            }
            foreach (string part3 in parts3)
            {
                if (part3 == "A")
                {
                    chkMathA.Checked = true;
                }
                if (part3 == "B")
                {
                    chkMathB.Checked = true;
                }
                if (part3 == "C")
                {
                    chkMathC.Checked = true;
                }
                if (part3 == "D")
                {
                    chkMathD.Checked = true;
                }
                if (part3 == "E")
                {
                    chkMathE.Checked = true;
                }
            }

            #endregion


            if (Convert.ToInt32(dtUserVillage.Rows[0]["ContactOption"].ToString()) == 1)
            {
                rblConTB.Checked = true;
            }
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["ContactFCTB"].ToString()) == 2)
            //{

            //    rblConFC.Checked = true;
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["ContactOption"].ToString()) == 1)
            //{
            //    rbloption1.Checked = true;
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["ContactOption"].ToString()) == 2)
            //{

            //    rbloption2.Checked = true;
            //}


            string SchoolContactOption = dtUserVillage.Rows[0]["SchoolContactOption"].ToString();

            string[] SchoolContactOption1 = SchoolContactOption.Split(',');

            foreach (string s in SchoolContactOption1)
            {
                foreach (ListItem item in chkSchoolCOntact.Items)
                {
                    if (item.Value == s)
                    {
                        item.Selected = true;

                    }
                }
            }

            //strQry = "   select * from tblActivityUpdate_LifeskillGames where GUID_School='" + dtUserVillage.Rows[0]["GUID_School"].ToString() + "'  ";
            //DataTable dtGame = objMain.LoadData(strQry);
            //if (dtGame.Rows.Count > 0)
            //{
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Lifeskill_Games"].ToString()) == 1)
            {

                chklife.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Lifeskill_Games_TB"].ToString()) == 1)
            {

                rblLifeTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Lifeskill_Games_FC"].ToString()) == 1)
            {

                rblLifeFC.Checked = true;
            }
            #region Game

            string LifeSkillGameEntry = dtUserVillage.Rows[0]["LifeSkillGameEntry"].ToString();

            string[] Skill = LifeSkillGameEntry.Split(',');
            foreach (string Skill1 in Skill)
            {
                if (Skill1 == "1")
                {
                    chkGame1.Checked = true;
                }
                if (Skill1 == "2")
                {
                    chkGame2.Checked = true;
                }
                if (Skill1 == "3")
                {
                    chkGame3.Checked = true;
                }
                if (Skill1 == "4")
                {
                    chkGame4.Checked = true;
                }
                if (Skill1 == "5")
                {
                    chkGame5.Checked = true;
                }
            }
            #endregion
            //}
            #region Balsabha
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual"].ToString()) == 1)
            {
                //  chkPhysical.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha"].ToString()) == 1)
            {
                chkBalsabha.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_TB"].ToString()) == 1)
            {

                rblBalsabaTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_FC"].ToString()) == 1)
            {

                rblBalsabaFC.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Formation"].ToString()) != 0)
            {
                chkBalSabhaFor.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Orientation"].ToString()) != 0)
            {
                chkOrientation.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Chart"].ToString()) != 0)
            {
                chkChat.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalSabha_Kit"].ToString()) != 0)
            {
                chkKit.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BalsabaType"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BalsabaType"].ToString()) == 1)
                {
                    rblPossiblie.Checked = true;
                    Group1_CheckedChanged(rblPossiblie, null);
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BalsabaType"].ToString()) == 2)
                {
                    rblIMPossiblie.Checked = true;
                    Group1_CheckedChanged(rblPossiblie, null);
                    if (Convert.ToInt32(dtUserVillage.Rows[0]["Balsabareason"].ToString()) != 0)
                    {
                        ddlreasons.SelectedValue = dtUserVillage.Rows[0]["Balsabareason"].ToString();
                    }
                }
            }


            if (Convert.ToString(dtUserVillage.Rows[0]["BalSabha_Orientation"]) != "")
            {
                chkSession1.Checked = true;
                chkSession1.Enabled = false;

            }
            else
            {
                chkSession2.Checked = false;
                chkSession2.Enabled = false;
            }


            #endregion


            #region CLTTest
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Pretest_TB"].ToString()) == 1)
            {
                rblTestTBPre.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Pretest_FC"].ToString()) == 1)
            {
                rblTestpreFC.Checked = true;
            }
            if (dtUserVillage.Rows[0]["Clt_Pre_PC"].ToString() == "P")
            {
                rblPartialPre.Checked = true;
            }
            if (dtUserVillage.Rows[0]["Clt_Pre_PC"].ToString() == "C")
            {
                rblCompletePre.Checked = true;
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["CTL_Midtest_TB"].ToString()) == 1)
            {
                rblTestTBMid.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CTL_Midtest_FC"].ToString()) == 1)
            {
                rblTestMidFC.Checked = true;
            }
            if (dtUserVillage.Rows[0]["Clt_Mid_PC"].ToString() == "P")
            {
                rblPartialMid.Checked = true;
            }
            if (dtUserVillage.Rows[0]["Clt_Mid_PC"].ToString() == "C")
            {
                rblCompleteMid.Checked = true;
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Posttest_TB"].ToString()) == 1)
            {
                rblTestTBPost.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Posttest_FC"].ToString()) == 1)
            {
                rblTestPostFC.Checked = true;
            }
            if (dtUserVillage.Rows[0]["Clt_Post_PC"].ToString() == "P")
            {
                rblPartialPost.Checked = true;
            }
            if (dtUserVillage.Rows[0]["Clt_Post_PC"].ToString() == "C")
            {
                rblCompletePost.Checked = true;
            }

            #endregion

            #region SAC




            int month = 0;
            if (txtDate.Text != "")
            {
                month = Convert.ToInt32(b[1]);
            }
            if (month == 7)
            {
                txtHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();

                txtSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();


                txtAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();

                txtleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();

                txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();



                //txtSepHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();

                //txtSepSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();


                //txtsepAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                //txtSepAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                //txtSepleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();

                //txtSepleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();

                //txtSepGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                //txtSepBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

            }

            if (month == 10 || month == 11)
            {



                txtSepHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();

                txtSepSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();


                txtsepAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtSepAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtSepleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();

                txtSepleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();

                txtSepGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtSepBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

            }
            if (month == 1)
            {

                txtDescHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();



                txtDescSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();


                txtDescAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtDescAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtDescleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();

                txtdescleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();

                txtDescGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtDecBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

            }

            if (month == 3)
            {

                txtMarHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();



                txtMarSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();


                txtMarAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();

                txtMarAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();

                txtMarleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();

                txtMarleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();

                txtMarGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();

                txtMarBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();

            }
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString()) != 0)
            //{
            //    txtHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();

            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString()) != 0)
            //{
            //    txtSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();

            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString()) != 0)
            //{
            //    txtAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString()) != 0)
            //{
            //    txtAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString()) != 0)
            //{
            //    txtleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString()) != 0)
            //{
            //    txtleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            //{
            //    txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            //{
            //    txtBoyNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();
            //}
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate"].ToString()) == 1)
            {
                chkSACUpdate.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_TB"].ToString()) == 1)
            {
                rblSacTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_FC"].ToString()) == 1)
            {
                rblSacFB.Checked = true;
            }
            //divSafe.Style(
            //  divSafe.Attributes.Add.Style("background-color: #090;");
            // divSafe.Attributes.Add('style','color:green');






            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure"].ToString()) == 1)
            {
                chkPhysical.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_TB"].ToString()) == 1)
            {
                rblPhysicalTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_FC"].ToString()) == 1)
            {
                rblPhysicalFC.Checked = true;
            }




            if (Convert.ToInt32(dtUserVillage.Rows[0]["Classrooms"].ToString()) != 0)
            {
                txtClassRoom.Text = dtUserVillage.Rows[0]["Classrooms"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) != 0)
            {

                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 4)
                {
                    //txtdrinking.BackColor = Color.Green;
                    txtdrinking.BackColor = Color.Blue;

                    lbldriking.Text = "4";

                    //  txt1.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 1)
                {
                    txtdrinking.BackColor = Color.Green;
                    lbldriking.Text = "1";
                    //  txt1.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 2)
                {
                    txtdrinking.BackColor = Color.Orange;
                    lbldriking.Text = "2";
                    // txt1.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DrinkingWater"].ToString()) == 3)
                {
                    txtdrinking.BackColor = Color.Red;
                    lbldriking.Text = "3";
                    //  txt1.Text = "3";

                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 4)
                {
                    txtToilet.BackColor = Color.Blue;
                    //txtToilet.BackColor = Color.Green;
                    lblToilet.Text = "4";

                    //   txt2.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 1)
                {
                    txtToilet.BackColor = Color.Green;
                    lblToilet.Text = "1";
                    // txt2.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 2)
                {
                    txtToilet.BackColor = Color.Orange;
                    lblToilet.Text = "2";
                    // txt2.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsToilet"].ToString()) == 3)
                {
                    txtToilet.BackColor = Color.Red;
                    lblToilet.Text = "3";
                    //   txt2.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 4)
                {
                    txtElectricity.BackColor = Color.Blue;
                    lblElectricity.Text = "4";
                    //  txt3.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 1)
                {
                    txtElectricity.BackColor = Color.Green;
                    lblElectricity.Text = "1";
                    //  txt3.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 2)
                {
                    txtElectricity.BackColor = Color.Orange;
                    lblElectricity.Text = "2";

                    //  txt3.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Electricity"].ToString()) == 3)
                {
                    txtElectricity.BackColor = Color.Red;
                    lblElectricity.Text = "3";
                    //  txt3.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 4)
                {
                    txtPlay.BackColor = Color.Blue;
                    lblPlay.Text = "4";
                    //   txt4.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 1)
                {
                    txtPlay.BackColor = Color.Green;
                    lblPlay.Text = "1";
                    //   txt4.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 2)
                {
                    txtPlay.BackColor = Color.Orange;
                    lblPlay.Text = "2";
                    //  txt4.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Playground"].ToString()) == 3)
                {
                    txtPlay.BackColor = Color.Red;
                    lblPlay.Text = "3";
                    //  txt4.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 4)
                {
                    txtSlides.BackColor = Color.Blue;
                    lblSlides.Text = "4";
                    //   txt5.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 1)
                {
                    txtSlides.BackColor = Color.Green;
                    lblSlides.Text = "1";
                    //   txt5.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 2)
                {
                    txtSlides.BackColor = Color.Orange;
                    lblSlides.Text = "2";
                    //  txt5.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Slide"].ToString()) == 3)
                {
                    txtSlides.BackColor = Color.Red;
                    lblSlides.Text = "3";
                    //  txt5.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 4)
                {
                    txtBoundaryWall.BackColor = Color.Blue;
                    lblBoundaryWall.Text = "4";
                    //  txt6.Text = "4";

                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 1)
                {
                    txtBoundaryWall.BackColor = Color.Green;
                    lblBoundaryWall.Text = "1";
                    //    txt6.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 2)
                {
                    txtBoundaryWall.BackColor = Color.Orange;
                    lblBoundaryWall.Text = "2";
                    //  txt6.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoundaryWall"].ToString()) == 3)
                {
                    txtBoundaryWall.BackColor = Color.Red;
                    lblBoundaryWall.Text = "3";
                    //  txt6.Text = "3";
                }
            }



            if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 4)
                {
                    txtKitchen.BackColor = Color.Blue;

                    lblKitchen.Text = "4";
                    //   txt7.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 1)
                {
                    txtKitchen.BackColor = Color.Green;
                    lblKitchen.Text = "1";
                    //   txt7.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 2)
                {
                    txtKitchen.BackColor = Color.Orange;
                    lblKitchen.Text = "2";
                    // txt7.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Kitchen"].ToString()) == 3)
                {
                    txtKitchen.BackColor = Color.Red;
                    lblKitchen.Text = "3";
                    //   txt7.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 4)
                {
                    txtCltKit.BackColor = Color.Blue;

                    lblCltKit.Text = "4";
                    //  txt8.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 1)
                {
                    txtCltKit.BackColor = Color.Green;
                    lblCltKit.Text = "1";
                    //  txt8.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 2)
                {
                    txtCltKit.BackColor = Color.Orange;
                    lblCltKit.Text = "2";
                    //  txt8.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["CLT_Kit"].ToString()) == 3)
                {
                    txtCltKit.BackColor = Color.Red;
                    lblCltKit.Text = "3";
                    //  txt8.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 4)
                {
                    txtbook.BackColor = Color.Blue;

                    lblbook.Text = "4";
                    //  txt9.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 1)
                {
                    txtbook.BackColor = Color.Green;
                    lblbook.Text = "1";
                    // txt9.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 2)
                {
                    txtbook.BackColor = Color.Orange;
                    lblbook.Text = "2";
                    //  txt9.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Books"].ToString()) == 3)
                {
                    txtbook.BackColor = Color.Red;
                    lblbook.Text = "3";
                    // txt9.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 4)
                {
                    txtBoysToilet.BackColor = Color.Blue;
                    lblBoysToilet.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 1)
                {
                    txtBoysToilet.BackColor = Color.Green;
                    lblBoysToilet.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 2)
                {
                    txtBoysToilet.BackColor = Color.Orange;
                    lblBoysToilet.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysToilet"].ToString()) == 3)
                {
                    txtBoysToilet.BackColor = Color.Red;
                    lblBoysToilet.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 4)
                {
                    TextTapWater.BackColor = Color.Blue;
                    lblWaterSupply.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 1)
                {
                    TextTapWater.BackColor = Color.Green;
                    lblWaterSupply.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 2)
                {
                    TextTapWater.BackColor = Color.Orange;
                    lblWaterSupply.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterSupply"].ToString()) == 3)
                {
                    TextTapWater.BackColor = Color.Red;
                    lblWaterSupply.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 4)
                {
                    TxtTiling.BackColor = Color.Blue;
                    lblTilingToilet.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 1)
                {
                    TxtTiling.BackColor = Color.Green;
                    lblTilingToilet.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 2)
                {
                    TxtTiling.BackColor = Color.Orange;
                    lblTilingToilet.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingToilet"].ToString()) == 3)
                {
                    TxtTiling.BackColor = Color.Red;
                    lblTilingToilet.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 4)
                {
                    txtHandicapped.BackColor = Color.Blue;
                    lblHandicappedAccessibleToilet.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 1)
                {
                    txtHandicapped.BackColor = Color.Green;
                    lblHandicappedAccessibleToilet.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 2)
                {
                    txtHandicapped.BackColor = Color.Orange;
                    lblHandicappedAccessibleToilet.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["HandicappedAccessibleToilet"].ToString()) == 3)
                {
                    txtHandicapped.BackColor = Color.Red;
                    lblHandicappedAccessibleToilet.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 4)
                {
                    txtMultipleHandwashing.BackColor = Color.Blue;
                    lblMultipleHandwashingUnit.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 1)
                {
                    txtMultipleHandwashing.BackColor = Color.Green;
                    lblMultipleHandwashingUnit.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 2)
                {
                    txtMultipleHandwashing.BackColor = Color.Orange;
                    lblMultipleHandwashingUnit.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["MultipleHandwashingUnit"].ToString()) == 3)
                {
                    txtMultipleHandwashing.BackColor = Color.Red;
                    lblMultipleHandwashingUnit.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 4)
                {
                    txtTilingclassroom.BackColor = Color.Blue;
                    lblTilingClassroomFloor.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 1)
                {
                    txtTilingclassroom.BackColor = Color.Green;
                    lblTilingClassroomFloor.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 2)
                {
                    txtTilingclassroom.BackColor = Color.Orange;
                    lblTilingClassroomFloor.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TilingClassroomFloor"].ToString()) == 3)
                {
                    txtTilingclassroom.BackColor = Color.Red;
                    lblTilingClassroomFloor.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 4)
                {
                    txtBlackboards.BackColor = Color.Blue;
                    lblBlackboards.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 1)
                {
                    txtBlackboards.BackColor = Color.Green;
                    lblBlackboards.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 2)
                {
                    txtBlackboards.BackColor = Color.Orange;
                    lblBlackboards.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BlackboardsinClassrooms"].ToString()) == 3)
                {
                    txtBlackboards.BackColor = Color.Red;
                    lblBlackboards.Text = "3";
                }
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 4)
                {
                    txtProperpainting.BackColor = Color.Blue;
                    lblProperPainting.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 1)
                {
                    txtProperpainting.BackColor = Color.Green;
                    lblProperPainting.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 2)
                {
                    txtProperpainting.BackColor = Color.Orange;
                    lblProperPainting.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["ProperPaintingSchool"].ToString()) == 3)
                {
                    txtProperpainting.BackColor = Color.Red;
                    lblProperPainting.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 4)
                {
                    txtDisabledaccessible.BackColor = Color.Blue;
                    lblDisabledAccessibleRamp.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 1)
                {
                    txtDisabledaccessible.BackColor = Color.Green;
                    lblDisabledAccessibleRamp.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 2)
                {
                    txtDisabledaccessible.BackColor = Color.Orange;
                    lblDisabledAccessibleRamp.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["DisabledAccessibleRamp"].ToString()) == 3)
                {
                    txtDisabledaccessible.BackColor = Color.Red;
                    lblDisabledAccessibleRamp.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 4)
                {
                    txtAppropriateelectrical.BackColor = Color.Blue;
                    lblAppropriateElectricalWiring.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 1)
                {
                    txtAppropriateelectrical.BackColor = Color.Green;
                    lblAppropriateElectricalWiring.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 2)
                {
                    txtAppropriateelectrical.BackColor = Color.Orange;
                    lblAppropriateElectricalWiring.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["AppropriateElectricalWiring"].ToString()) == 3)
                {
                    txtAppropriateelectrical.BackColor = Color.Red;
                    lblAppropriateElectricalWiring.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 4)
                {
                    txtBoysUrinal.BackColor = Color.Blue;
                    lblBoysUrinal.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 1)
                {
                    txtBoysUrinal.BackColor = Color.Green;
                    lblBoysUrinal.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 2)
                {
                    txtBoysUrinal.BackColor = Color.Orange;
                    lblBoysUrinal.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["BoysUrinal"].ToString()) == 3)
                {
                    txtBoysUrinal.BackColor = Color.Red;
                    lblBoysUrinal.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 4)
                {
                    txtGirlsUrinal.BackColor = Color.Blue;
                    lblGirlsUrinal.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 1)
                {
                    txtGirlsUrinal.BackColor = Color.Green;
                    lblGirlsUrinal.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 2)
                {
                    txtGirlsUrinal.BackColor = Color.Orange;
                    lblGirlsUrinal.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["GirlsUrinal"].ToString()) == 3)
                {
                    txtGirlsUrinal.BackColor = Color.Red;
                    lblGirlsUrinal.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 4)
                {
                    txtFurniture.BackColor = Color.Blue;
                    lblFurniture.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 1)
                {
                    txtFurniture.BackColor = Color.Green;
                    lblFurniture.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 2)
                {
                    txtFurniture.BackColor = Color.Orange;
                    lblFurniture.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["Furniture"].ToString()) == 3)
                {
                    txtFurniture.BackColor = Color.Red;
                    lblFurniture.Text = "3";
                }
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) != 0)
            {
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 4)
                {
                    txtWaterStorage.BackColor = Color.Blue;
                    lblTapWaterFacility.Text = "4";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 1)
                {
                    txtWaterStorage.BackColor = Color.Green;
                    lblTapWaterFacility.Text = "1";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 2)
                {
                    txtWaterStorage.BackColor = Color.Orange;
                    lblTapWaterFacility.Text = "2";
                }
                if (Convert.ToInt32(dtUserVillage.Rows[0]["TapWaterFacility"].ToString()) == 3)
                {
                    txtWaterStorage.BackColor = Color.Red;
                    lblTapWaterFacility.Text = "3";
                }
            }


            if (Convert.ToInt32(dtUserVillage.Rows[0]["Teachers_Male"].ToString()) != 0)
            {
                txtMaleTeacher.Text = dtUserVillage.Rows[0]["Teachers_Male"].ToString();
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Teachers_Female"].ToString()) != 0)
            {
                txtFemaleTeacher.Text = dtUserVillage.Rows[0]["Teachers_Female"].ToString();
            }
            #endregion


            #region SAC Update
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate"].ToString()) == 1)
            {
                chkSACUpdate.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_TB"].ToString()) == 1)
            {
                rblSacTB.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SACUpdate_FC"].ToString()) == 1)
            {
                rblSacFB.Checked = true;
            }


            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString()) != 0)
            //{
            //    txtSMCMeeting.Text = dtUserVillage.Rows[0]["SAC_No_Of_Attended"].ToString();
            //}

            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString()) != 0)
            //{
            //    txtHealth.Text = dtUserVillage.Rows[0]["SAC_Periodic_Checkup"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString()) != 0)
            //{
            //    txtAdgirls.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Girls"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString()) != 0)
            //{
            //    txtAdBoy.Text = dtUserVillage.Rows[0]["SAC_Listing_Name_Of_Boys"].ToString();
            //}

            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString()) != 0)
            //{
            //    txtleftGirl.Text = dtUserVillage.Rows[0]["SAC_Girls_Left"].ToString();
            //}

            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString()) != 0)
            //{
            //    txtleftBoy.Text = dtUserVillage.Rows[0]["SAC_Boys_Left"].ToString();
            //}

            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString()) != 0)
            //{
            //    txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Boys_Not_Joined_School"].ToString();
            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString()) != 0)
            //{
            //    txtGirlNot.Text = dtUserVillage.Rows[0]["SAC_Girls_Not_Joined_School"].ToString();
            //}

            if (dtUserVillage.Rows[0]["BalSabaTBCode"].ToString().Length > 0)
            {

                divBTB_Click(rblApprove, null);
                ddlBalSabaTB.SelectedValue = dtUserVillage.Rows[0]["BalSabaTBCode"].ToString();
            }
            #endregion

            #region Anuanl Data
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual"].ToString()) == 1)
            {
                chkAnnual.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual"].ToString()) == 1)
            {
                chkSIPAnnaul.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Retention_Annual"].ToString()) == 1)
            {
                chkRetention.Checked = true;
            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Retention_Annual_TB"].ToString()) == 1)
            {
                chkRenTB.Checked = true;

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Retention_Annual_FC"].ToString()) == 1)
            {
                chkRenFC.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual_TB"].ToString()) == 1)
            {
                chkSIPTB.Checked = true;

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["SIP_Annual_FC"].ToString()) == 1)
            {
                chkSIPFC.Checked = true;
            }

            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_TB"].ToString()) == 1)
            {
                rblPhysicalTB.Checked = true;

            }
            if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_FC"].ToString()) == 1)
            {
                rblPhysicalFC.Checked = true;


            }
            if (dtUserVillage.Rows[0]["SIP_PC"].ToString() == "C")
            {
                chkSipComplete.Checked = true;
            }
            if (dtUserVillage.Rows[0]["SIP_PC"].ToString() == "P")
            {
                chkSipPartial.Checked = true;
            }


            //if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_TB"].ToString()) == 1)
            //{
            //    chkSipPartial.Checked = true;

            //}
            //if (Convert.ToInt32(dtUserVillage.Rows[0]["Infrastructure_FC"].ToString()) == 1)
            //{
            //    chkRenPartial.Checked = true;
            //}

            if (dtUserVillage.Rows[0]["Retention_PC"].ToString() == "P")
            {
                chkRenPartial.Checked = true;

            }
            if (dtUserVillage.Rows[0]["Retention_PC"].ToString() == "C")
            {
                chkComplete.Checked = true;
            }
            #endregion



        }
        else
        {
            SqlParameter[] parm10 = new SqlParameter[]
            {
               new SqlParameter("@villagecode",  ddlVilage.SelectedValue),
               new SqlParameter("@SchoolCode",  ddlSchool.SelectedValue),
                new SqlParameter("@User",ddlUser.SelectedValue),
                  new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate).ToString("yyyy-MM-dd")),
                  new SqlParameter("@UserEntry",userid),

                };

            int hh = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivityUpdateDataNew1]", parm10);


            ClearData();
            btnsave.Visible = true;
            ViewState["GUID_School"] = "";
        }
        hdnsession2.Value = "";
        pnlLife.Enabled = false;
        #region balshabha 15122021
        if (DtUserdata.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(DtUserdata.Tables[0].Rows[0]["Session1"]) != "")
            {
                string s = Convert.ToString(DtUserdata.Tables[0].Rows[0]["Session1"]);

                strQry = "   select BalSabha_FC,BalSabha_TB  from tblActivityUpdate_School " + wherecon3 + " and ActivityDate= '" + Convert.ToDateTime(s).ToString("yyyy-MM-dd") + "'  ";
                DataTable dt = objMain.LoadData(strQry);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["BalSabha_TB"].ToString()) == 1)
                    {

                        rblBalsabaTB.Checked = true;
                        rblBalsabaTB.Enabled = false;
                    }
                    if (Convert.ToInt32(dt.Rows[0]["BalSabha_FC"].ToString()) == 1)
                    {
                        rblBalsabaTB.Enabled = false;
                        rblBalsabaFC.Checked = true;
                    }
                }
            }
            else
            {
                if (this.ddlRemark.SelectedIndex > 0)
                {
                    chkSession1.Checked = false;

                }
            }
        }


        string session1 = "", session2 = "", Cdate = "";
        if (DtUserdata.Tables.Count > 0)
        {

            DateTime currentdate = Convert.ToDateTime(txtDate.Text);
            Cdate = currentdate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

            if (DtUserdata.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToString(DtUserdata.Tables[0].Rows[0]["Session1"]) != "")
                {
                    string s = Convert.ToString(DtUserdata.Tables[0].Rows[0]["Session1"]);
                    DateTime dt = DateTime.ParseExact(s.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);

                    session1 = dt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    hdnsession1.Value = Convert.ToString(DtUserdata.Tables[0].Rows[0]["Session1"]);


                    if (session1.Length > 0)
                    {
                        if (this.ddlRemark.SelectedIndex > 0)
                        {
                            if (Convert.ToDateTime(Cdate) == Convert.ToDateTime(session1))
                            {
                                chkSession1.Checked = true;
                                chkSession1.Enabled = true;
                            }
                            if (Convert.ToDateTime(Cdate) < Convert.ToDateTime(session1))
                            {

                                chkSession1.Checked = false;
                                chkSession1.Enabled = false;
                                rblBalsabaTB.Checked = false;
                                rblBalsabaFC.Checked = false;
                            }
                            if (Convert.ToDateTime(Cdate) > Convert.ToDateTime(session1))
                            {
                                chkSession1.Checked = true;
                                chkSession1.Enabled = false;
                            }
                        }
                    }
                    if (Convert.ToDateTime(Cdate) < Convert.ToDateTime(session1))
                    {


                        hdnsession1.Value = "";
                        ddlsession.SelectedIndex = 0;
                        GvReg.DataSource = null;
                        GvReg.DataBind();
                    }
                    else
                    {
                        GVregdatabind();
                    }



                }
            }
            if (DtUserdata.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToString(DtUserdata.Tables[1].Rows[0]["Session2"]) != "")
                {
                    chkSession1.Enabled = false;
                    string s1 = Convert.ToString(DtUserdata.Tables[1].Rows[0]["Session2"]);
                    DateTime dt1 = DateTime.ParseExact(s1.ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    hdnsession2.Value = Convert.ToString(DtUserdata.Tables[1].Rows[0]["Session2"]);
                    session2 = Convert.ToString(DtUserdata.Tables[1].Rows[0]["Session2"]);
                    if (Convert.ToDateTime(Cdate) < Convert.ToDateTime(s1))
                    {

                        ddlsession.SelectedIndex = 0;
                        GvReg.DataSource = null;
                        GvReg.DataBind();

                    }
                }
            }



            if (session1 != "")
            {

                if (Convert.ToDateTime(session1) < Convert.ToDateTime(Cdate))
                {
                    if (session2 != "")
                    {

                        DataTable dtLiff = DatabindRegNew2021("", 2);
                        if (GvReg.Rows.Count > 12)
                        {
                            Imgaddclass.Enabled = false;
                        }
                        else
                        {
                            Imgaddclass.Enabled = true;
                        }
                        if (Convert.ToDateTime(session1) < Convert.ToDateTime(session2))
                        {
                            chkSession1.Enabled = false;
                            chkSession2.Checked = true;
                            if (Convert.ToDateTime(session2) < Convert.ToDateTime(Cdate))
                            {
                                chkSession2.Enabled = false;
                                chkBalSabhaFor.Checked = true;
                                chkBalSabhaFor.Enabled = false;

                            }

                            hdnsession2.Value = Convert.ToString(DtUserdata.Tables[1].Rows[0]["Session2"]);
                            if (Convert.ToDateTime(Cdate) > Convert.ToDateTime(session2))
                            {
                                GvReg.Enabled = true;
                                chkSession2.Enabled = false;
                                if (GvReg.Rows.Count > 6)
                                {

                                    pnlLife.Enabled = true;
                                    rblBalsabaTB.Enabled = false;
                                    rblBalsabaFC.Enabled = false;
                                    chklife.Checked = true;
                                }
                            }
                            else
                            {
                                if (dtLiff.Rows.Count > 0)
                                {
                                    chklife.Checked = false;
                                    GvReg.Enabled = false;
                                    Imgaddclass.Enabled = false;
                                    pnlLife.Enabled = false;
                                    rblLifeTB.Checked = false;
                                    rblLifeFC.Checked = false;

                                }
                            }
                        }
                        if (Convert.ToDateTime(Cdate) == Convert.ToDateTime(session2))
                        {
                            if (dtLiff.Rows.Count > 0)
                            {
                                chkSession2.Enabled = false;
                            }
                            else
                            {
                                chkSession2.Enabled = true;
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (this.ddlRemark.SelectedIndex > 0)
                        {
                            chkSession2.Enabled = true;

                        }
                    }
                }
            }
            else
            {
                if (this.ddlRemark.SelectedIndex > 0)
                {
                    chkSession1.Checked = false;

                }

                chkSession2.Enabled = false;
                chkSession2.Checked = false;

            }

        }

        #endregion


    }
    public void txtx_change(object sender, EventArgs e)
    {
        ClearData();
        pnlMain.Enabled = true;
    }

    public void ClearData()
    {

        rblothertb.Checked = false;

        rblotherfc.Checked = false;
        chkother.Checked = false;

        txtPreSMCMeeting.Text = "";
        txtSMCMeeting.Text = "";
        txtSepSMCMeeting.Text = "";
        txtDescSMCMeeting.Text = "";
        txtMarSMCMeeting.Text = "";
        txtPrvHealth.Text = "";
        txtHealth.Text = "";
        txtSepHealth.Text = "";
        txtDescHealth.Text = "";
        txtMarHealth.Text = "";
        txtPreAdgirls.Text = "";
        txtAdgirls.Text = "";
        txtsepAdgirls.Text = "";
        txtDescAdgirls.Text = "";
        txtMarAdgirls.Text = "";
        txtPrvAdBoy.Text = "";
        txtAdBoy.Text = "";
        txtSepAdBoy.Text = "";
        txtDescAdBoy.Text = "";
        txtMarAdBoy.Text = "";
        txtPrvleftGirl.Text = "";
        txtleftGirl.Text = "";
        txtSepleftGirl.Text = "";
        txtDescleftGirl.Text = "";
        txtMarleftGirl.Text = "";
        txtPrevleftBoy.Text = "";
        txtleftBoy.Text = "";
        txtSepleftBoy.Text = "";
        txtdescleftBoy.Text = "";
        txtMarleftBoy.Text = "";
        txtPrvGirlNot.Text = "";
        txtGirlNot.Text = "";
        txtSepGirlNot.Text = "";
        txtDescGirlNot.Text = "";
        txtMarGirlNot.Text = "";
        txtprvBoyNot.Text = "";
        txtBoyNot.Text = "";
        txtSepBoyNot.Text = "";
        txtDecBoyNot.Text = "";
        txtMarBoyNot.Text = "";

        DivLiff.Visible = false;
        divBTB.Visible = false;
        txtCountDriking.Text = "0";
        gvGkp.Enabled = false;
        rblBalsabaTB.Checked = false;
        rblBalsabaFC.Checked = false;
        TextBox1.Text = "0";
        TextBox2.Text = "0";
        TextBox3.Text = "0";
        TextBox4.Text = "0";
        TextBox5.Text = "0";
        TextBox6.Text = "0";
        TextBox7.Text = "0";
        TextBox8.Text = "0";
        TextBox9.Text = "0";
        TextBox10.Text = "0";
        TextBox11.Text = "0";
        TextBox12.Text = "0";
        TextBox13.Text = "0";
        TextBox14.Text = "0";
        TextBox15.Text = "0";
        TextBox16.Text = "0";
        TextBox17.Text = "0";
        TextBox18.Text = "0";
        TextBox19.Text = "0";
        TextBox20.Text = "0";
        TextBox21.Text = "0";
        TextBox22.Text = "0";
        txt1.Text = "0";
        txt2.Text = "0";
        txt3.Text = "0";
        txt4.Text = "0";
        txt5.Text = "0";
        txt6.Text = "0";
        txt7.Text = "0";
        txt8.Text = "0";
        txt9.Text = "0";
        txt10.Text = "0";
        txt11.Text = "0";
        txt12.Text = "0";
        txt13.Text = "0";
        txt14.Text = "0";
        txt15.Text = "0";
        txt16.Text = "0";
        txt17.Text = "0";
        txt18.Text = "0";
        txt19.Text = "0";
        txt20.Text = "0";
        txt21.Text = "0";
        txt22.Text = "0";
        txt23.Text = "0";
        rblPossiblie.Checked = false;
        rblIMPossiblie.Checked = false;
        pnlBalTest1.Visible = false;
        //Anuj    //pnlBalTest.Visible = false;
        chkRenFC.Checked = false;
        chkSMC.Checked = false;
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;
        chkNewSmc.Checked = false;
        rblSmcNew.Checked = false;
        rblSmcNew1.Checked = false;
        chkNewSmc.Checked = false;
        rblSmcNew1.Checked = false;
        txtTotalMember.Text = "";
        txtTotalFmember.Text = "";
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;
        rblCLTTB.Checked = false;
        rblCLTFC.Checked = false;
        lbldriking.Text = "0";
        lblToilet.Text = "0";
        lblElectricity.Text = "0";
        lblCltKit.Text = "0";
        lblbook.Text = "0";
        lblKitchen.Text = "0";
        lblBoundaryWall.Text = "0";
        lblSlides.Text = "0";
        txt_pbname1.Text = "";
        lblPlay.Text = "0";
        txt_pbname.Text = "";
        chkAnnual.Checked = false;
        txtOther.Text = "";
        chkSIPAnnaul.Checked = false;
        chkRetention.Checked = false;
        chkSIPTB.Checked = false;
        chkRenTB.Checked = false;
        chkRenPartial.Checked = false;
        chkSipPartial.Checked = false;
        chkSIPFC.Checked = false;
        chkRenFC.Checked = false;
        chkSipPartial.Checked = false;
        chkRenPartial.Checked = false;
        chkSipComplete.Checked = false;
        chkComplete.Checked = false;
        txtdrinking.Enabled = true;
        txtToilet.Enabled = true;
        txtElectricity.Enabled = true;
        txtPlay.Enabled = true;
        txtSlides.Enabled = true;
        txtBoundaryWall.Enabled = true;
        txtKitchen.Enabled = true;
        txtCltKit.Enabled = true;
        txtbook.Enabled = true;
        txtClassRoom.Text = "";
        txtMaleTeacher.Text = "";
        txtFemaleTeacher.Text = "";
        chkPhysical.Checked = false;
        rblPhysicalTB.Checked = false;
        rblPhysicalFC.Checked = false;
        chklife.Checked = false;
        ViewState["GUID_School"] = "";
        chkHolding.Checked = false;
        chkSMC.Checked = false;
        rblSMCTB.Checked = true;
        rblSMCFC.Checked = false;
        txtOtherSIPFC.Text = "";
        txtsmcmeetinFC.Text = "";
        foreach (ListItem item in CBL_bookformat.Items) { item.Selected = false; }
        foreach (ListItem item in CBL_bookformat1.Items) { item.Selected = false; }
        chkClT.Checked = false;
        rblCLTTB.Checked = true;
        rblCLTFC.Checked = false;
        chkHindiA.Checked = false;
        chkHindiB.Checked = false;
        chkHindiC.Checked = false;
        chkHindiD.Checked = false;
        chkHindiE.Checked = false;
        chkEnglishA.Checked = false;
        chkEnglishB.Checked = false;
        chkEnglishC.Checked = false;
        chkEnglishD.Checked = false;
        chkEnglishE.Checked = false;
        chkMathA.Checked = false;
        chkMathB.Checked = false;
        chkMathC.Checked = false;
        chkMathD.Checked = false;
        chkMathE.Checked = false;
        chkGame1.Checked = false;
        chkGame2.Checked = false;
        chkGame3.Checked = false;
        chkGame4.Checked = false;
        chkGame5.Checked = false;
        chkBalsabha.Checked = false;
        rblBalsabaFC.Checked = false;

        chkBalSabhaFor.Checked = false;

        chkOrientation.Checked = false;

        chkChat.Checked = false;

        chkKit.Checked = false;

        rdPSMCPY.Checked = false;
        rdPSMCPN.Checked = false;
        rdRegisterY.Checked = false;
        rdRegisterN.Checked = false;
        rblTestTBPre.Checked = false;

        rblTestpreFC.Checked = false;

        rblPartialPre.Checked = false;

        rblCompletePre.Checked = false;

        rblTestTBMid.Checked = false;

        rblTestMidFC.Checked = false;

        rblPartialMid.Checked = false;

        rblCompleteMid.Checked = false;
        rblTestTBPost.Checked = false;

        rblTestPostFC.Checked = false;

        rblPartialPost.Checked = false;

        rblCompletePost.Checked = false;



        txtHealth.Text = "";

        txtSMCMeeting.Text = "";


        txtAdgirls.Text = "";

        txtAdBoy.Text = "";

        txtleftGirl.Text = "";

        txtleftBoy.Text = "";

        txtGirlNot.Text = "";

        txtBoyNot.Text = "";
        txtToilet.BackColor = Color.White;
        txtdrinking.BackColor = Color.White;

        txtElectricity.BackColor = Color.White;
        txtbook.BackColor = Color.White;
        txtPlay.BackColor = Color.White;
        txtSlides.BackColor = Color.White;
        txtBoundaryWall.BackColor = Color.White;
        txtKitchen.BackColor = Color.White;
        txtCltKit.BackColor = Color.White;


        txtToilet1.BackColor = Color.White;
        txtdrinking1.BackColor = Color.White;

        txtElectricity1.BackColor = Color.White;
        txtbook1.BackColor = Color.White;
        txtPlay1.BackColor = Color.White;
        txtSlides1.BackColor = Color.White;
        txtBoundaryWall1.BackColor = Color.White;
        txtKitchen1.BackColor = Color.White;
        txtCltKit1.BackColor = Color.White;



        txtToilet2.BackColor = Color.White;
        txtdrinking2.BackColor = Color.White;

        txtElectricity2.BackColor = Color.White;
        txtbook2.BackColor = Color.White;
        txtPlay2.BackColor = Color.White;
        txtSlides2.BackColor = Color.White;
        txtBoundaryWall2.BackColor = Color.White;
        txtKitchen2.BackColor = Color.White;
        txtCltKit2.BackColor = Color.White;

        txtFemaleTeacher.Text = "";

        txtMaleTeacher.Text = "";
        txtClassRoom.Text = "";

        ddlMeetingPrepare.SelectedIndex = 0;
        chkSACUpdate.Checked = false;

        rblSacTB.Checked = false;

        rblSacFB.Checked = false;

        txtSMCMeeting.Text = "";

        txtHealth.Text = "";

        txtAdgirls.Text = "";

        txtAdBoy.Text = "";

        txtleftGirl.Text = "";

        txtleftBoy.Text = "";

        txtGirlNot.Text = "";

        txtGirlNot.Text = "";
        chkSMC.Checked = false;
        rblSMCTB.Checked = false;
        rblSMCFC.Checked = false;
        chkClT.Checked = false;
        rblCLTTB.Checked = false;
        rblCLTFC.Checked = false;
        chkNewSmc.Checked = false;
        rblSmcNew.Checked = false;
        rblSmcNew1.Checked = false;
        chkAnnual.Checked = false;
        chkSIPAnnaul.Checked = false;
        chkRetention.Checked = false;

        chkSIPTB.Checked = false;
        chkRenTB.Checked = false;
        chkSIPFC.Checked = false;

        chkRenFC.Checked = false;
        chkSipPartial.Checked = false;
        chkRenPartial.Checked = false;


        chkSipComplete.Checked = false;
        chkComplete.Checked = false;
        rblBalsabaTB.Enabled = true;
        rblBalsabaFC.Enabled = true;

        chkSession1.Checked = false;
        chkSession2.Checked = false;
        chkSession1.Enabled = true;
        chkSession2.Enabled = false;
        Imgaddclass.Enabled = false;

        rblConTB.Checked = false;
        rblConFC.Checked = false;
        rbloption1.Checked = false;
        rbloption2.Checked = false;
        ddlMarge.Enabled = true;
        txtSMCPre.Text = "";
        trGssId.Visible = false;
        tre1.Visible = false;
        rdTeamY.Checked = false;
        rdTeamN.Checked = false;
        ddlrec.SelectedIndex = 0;
        ddlDatemeeting.SelectedIndex = 0;
        ddlWrite.SelectedIndex = 0;
        ddlF5.SelectedIndex = 0;
        txtmembers.Text = "";
        lblCom22.Text = "";
        lblTottal.Text = "";
        lblFemale.Text = "";
        lblmale.Text = "";

        Session["dtmc"] = null;
        gvSmc.DataSource = null;
        gvSmc.DataBind();

        txtBoysToilet.Enabled = true;
        TextTapWater.Enabled = true;
        TxtTiling.Enabled = true;
        txtHandicapped.Enabled = true;
        txtMultipleHandwashing.Enabled = true;
        txtTilingclassroom.Enabled = true;
        txtBlackboards.Enabled = true;
        txtProperpainting.Enabled = true;
        txtDisabledaccessible.Enabled = true;
        txtAppropriateelectrical.Enabled = true;
        txtBoysUrinal.Enabled = true;
        txtGirlsUrinal.Enabled = true;
        txtFurniture.Enabled = true;
        txtWaterStorage.Enabled = true;
        txtClassRoom.Text = "";
        txtMaleTeacher.Text = "";
        txtFemaleTeacher.Text = "";

        lblBoysToilet.Text = "0";
        lblWaterSupply.Text = "0";
        lblTilingToilet.Text = "0";
        lblHandicappedAccessibleToilet.Text = "0";
        lblMultipleHandwashingUnit.Text = "0";
        lblTilingClassroomFloor.Text = "0";
        lblBlackboards.Text = "0";
        lblProperPainting.Text = "0";
        lblDisabledAccessibleRamp.Text = "0";
        lblAppropriateElectricalWiring.Text = "0";
        lblBoysUrinal.Text = "0";
        lblGirlsUrinal.Text = "0";
        lblFurniture.Text = "";
        lblTapWaterFacility.Text = "0";
        txtBoysToilet.BackColor = Color.White;
        txtBoysToilet1.BackColor = Color.White;
        txtBoysToilet2.BackColor = Color.White;
        TextTapWater.BackColor = Color.White;
        TextTapWater1.BackColor = Color.White;
        TextTapWater2.BackColor = Color.White;
        TxtTiling.BackColor = Color.White;
        TxtTiling1.BackColor = Color.White;
        TxtTiling2.BackColor = Color.White;
        txtHandicapped.BackColor = Color.White;
        txtHandicapped1.BackColor = Color.White;
        txtHandicapped2.BackColor = Color.White;
        txtMultipleHandwashing.BackColor = Color.White;
        txtMultipleHandwashing1.BackColor = Color.White;
        txtMultipleHandwashing2.BackColor = Color.White;
        txtTilingclassroom.BackColor = Color.White;
        txtTilingclassroom1.BackColor = Color.White;
        txtTilingclassroom2.BackColor = Color.White;
        txtBlackboards.BackColor = Color.White;
        txtBlackboards1.BackColor = Color.White;
        txtBlackboards2.BackColor = Color.White;
        txtProperpainting.BackColor = Color.White;
        txtProperpainting1.BackColor = Color.White;
        txtProperpainting2.BackColor = Color.White;
        txtDisabledaccessible.BackColor = Color.White;
        txtDisabledaccessible1.BackColor = Color.White;
        txtDisabledaccessible2.BackColor = Color.White;
        txtAppropriateelectrical.BackColor = Color.White;
        txtAppropriateelectrical1.BackColor = Color.White;
        txtAppropriateelectrical2.BackColor = Color.White;
        txtBoysUrinal.BackColor = Color.White;
        txtBoysUrinal1.BackColor = Color.White;
        txtBoysUrinal2.BackColor = Color.White;
        txtGirlsUrinal.BackColor = Color.White;
        txtGirlsUrinal1.BackColor = Color.White;
        txtGirlsUrinal2.BackColor = Color.White;
        txtFurniture.BackColor = Color.White;
        txtFurniture1.BackColor = Color.White;
        txtFurniture2.BackColor = Color.White;
        txtWaterStorage.BackColor = Color.White;
        txtWaterStorage1.BackColor = Color.White;
        txtWaterStorage2.BackColor = Color.White;
        foreach (ListItem item in chkSchoolCOntact.Items) { item.Selected = false; }
    }
    public void LoadEnrolled()
    {

        string strQry19 = " select *  from [MSTtopicDiscuss]   where Flag=106 and [Language]=0  ";
        DataTable dtOther121 = objMain.LoadData(strQry19);
        objComman.BindDLLDatatable("mstSchool", dtOther121, "TopicDIscussID,TopicDiscussName", conditions, "TopicDIscussID", "asc", ddlMeetingPrepare, "TopicDiscussName", "TopicDIscussID", "Select");

        //DataRow dr = null;
        string strQry = " select *  from [MSTtopicDiscuss]   where Flag=29 and [Language]=0  ";
        DataTable dtOther = objMain.LoadData(strQry);
        //dtOther.Columns.Add(new DataColumn("ID", System.Type.GetType("System.Int32")));
        //dtOther.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
        //dr = dtOther.NewRow();
        //dr["ID"] = 52;
        //dr["Name"] = "Enrollment";
        //dtOther.Rows.Add(dr);

        //dr = dtOther.NewRow();
        //dr["ID"] = 53;
        //dr["Name"] = "Retention";
        //dtOther.Rows.Add(dr);

        //dr = dtOther.NewRow();
        //dr["ID"] = 54;
        //dr["Name"] = "Learning Level";
        //dtOther.Rows.Add(dr);
        //dr = dtOther.NewRow();

        //dr["ID"] = 55;
        //dr["Name"] = "others (specify)";
        //dtOther.Rows.Add(dr);

        CBL_bookformat.DataSource = dtOther;
        CBL_bookformat.DataTextField = "TopicDiscussName";
        CBL_bookformat.DataValueField = "TopicDIscussID";
        CBL_bookformat.DataBind();
        string strQry1 = " select *  from [MSTtopicDiscuss]   where Flag=30 and [Language]=0  ";
        DataTable dtOther1 = objMain.LoadData(strQry1);
        CBL_bookformat1.DataSource = dtOther1;
        CBL_bookformat1.DataTextField = "TopicDiscussName";
        CBL_bookformat1.DataValueField = "TopicDIscussID";
        CBL_bookformat1.DataBind();
        objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='BRE'", "LookupCode", "asc", ddlreasons, "Description", "LookupCode", "Select");

        string strQry11 = " select *  from [MSTtopicDiscuss]   where Flag=75 and [Language]=0  ";
        DataTable dtOther111 = objMain.LoadData(strQry11);
        chkSchoolCOntact.DataSource = dtOther111;
        chkSchoolCOntact.DataTextField = "TopicDiscussName";
        chkSchoolCOntact.DataValueField = "TopicDIscussID";
        chkSchoolCOntact.DataBind();

        string strQry33 = " select *  from [MSTtopicDiscuss]   where Flag=78 and [Language]=0  order by TopicDIscussID asc ";
        DataTable dtOther334 = objMain.LoadData(strQry33);

        objComman.BindDLLDatatable("mstSchool", dtOther334, "TopicDIscussID,TopicDiscussName", conditions, "TopicDIscussID", "asc", ddlrec, "TopicDiscussName", "TopicDIscussID", "Select");

        string strQry331 = " select *  from [MSTtopicDiscuss]   where Flag=77 and [Language]=0 order by TopicDIscussID asc ";
        DataTable dtOther33 = objMain.LoadData(strQry331);

        objComman.BindDLLDatatable("mstSchool", dtOther33, "TopicDIscussID,TopicDiscussName", conditions, "TopicDIscussID", "asc", ddlDatemeeting, "TopicDiscussName", "TopicDIscussID", "Select");

    }

    public void UserData()
    {
        conditions = "UserLevel=24";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "30")
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

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            conditions = "mst5Village.ClusterCode in('" + strVillage + "') ";

            strQry = "";
            strQry = "select VillageCode,VillageName  from mst5Village where mst5Village.ClusterCode in('" + strVillage + "')     ";
            strQry += " Union select mstActivityVillage.VillageCode,mstActivityVillage.VillageName  from mstActivityVillage    inner join mst5Village on mst5Village.VillageCode=mstActivityVillage.Villagecode where UserID='" + ddlUser.SelectedValue + "' and mst5Village.Fyear='" + Session["FinYear"].ToString() + "'   ";
            strQry += " Union ";
            strQry += "  select mst5Village.VillageCode,VillageName  from mst5Village  ";
            strQry += " inner join tblActivityUpdate_School on tblActivityUpdate_School.VillageCode=mst5Village.VillageCode  ";
            strQry += "  where mst5Village.ClusterCode in('" + Session["Cluseter"].ToString() + "' )   and UserID='" + ddlUser.SelectedValue + "'   ";

            DataTable dtVillage = objMain.LoadData(strQry);
            //objComman.BindDLLMasterTable("MstUser", "UserName as UserId,[FristName]+' ('+ UserName +')' as [UserName] ", dtUser, conditions, "", "", ddlUser, "UserName", "UserId", "Select");

            objComman.BindDLLMasterTable("mst5Village", "VillageCode,VillageName ", dtVillage, "", "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");

            //objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "VillageName", "", ddlVilage, "VillageName", "VillageCode", "Select");


        }
        //DataTable dt = objMain.GetActivityUserWiseMaxDateNew(ddlUser.SelectedValue, Session["Cluseter"].ToString());
        //if (dt.Rows.Count > 0   )
        //{
        //    if (Convert.ToString(dt.Rows[0]["ActivityDate"].ToString())!="")
        //    {
        //    CalendarExtenderTourdate.StartDate = Convert.ToDateTime(dt.Rows[0]["ActivityDate"].ToString()).AddDays(1);
        //    }
        //}
        pnlMain.Enabled = false;
    }

    //protected void txtdrinking_TextChanged(object sender, EventArgs e)
    //{
    //    int icount = 0;
    //    int iwaterpre = Convert.ToInt32(lbldriking.Text);
    //    if (iwaterpre == 1)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 3;
    //        }
    //        else if (icount == 1)
    //        {
    //            icount = 3;
    //        }
    //        else if (icount == 2)
    //        {
    //            icount = 3;
    //        }
    //        if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Green;
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

    //            icount++;
    //            lbldriking.Text = "1";
    //        }
    //        else if (icount == 4)
    //        {
    //            txtdrinking.BackColor = Color.Blue;
    //       //     btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);

    //            lbldriking.Text = "4";
    //            icount = 3;
    //        }

    //    }
    //    else if (iwaterpre == 2)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 2;
    //        }
    //        else if (icount == 1)
    //        {
    //            icount = 2;
    //        }
    //        if (icount == 1)
    //        {
    //            txtdrinking.BackColor = Color.Red;
    //        //    btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
    //            icount++;

    //            lbldriking.Text = "3";

    //        }
    //        else if (icount == 2)
    //        {
    //            txtdrinking.BackColor = Color.Orange;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
    //            icount++;

    //            lbldriking.Text = "2";
    //        }
    //        else if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Green;
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
    //            icount = 2;

    //            lbldriking.Text = "1";
    //        }

    //    }
    //    else if (iwaterpre == 3)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 3;
    //        }/*
    //        * else if (icount == 1) { icount = 3; } else if (icount == 2) {
    //        * icount = 3; }
    //        */
    //        if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Red;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
    //            icount--;

    //            lbldriking.Text = "3";
    //        }
    //        else

    //            if (icount == 2)
    //            {
    //                txtdrinking.BackColor = Color.Orange;
    //             //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
    //                icount--;

    //                lbldriking.Text = "2";
    //            }
    //            else if (icount == 1)
    //            {
    //                txtdrinking.BackColor = Color.Green;
    //              //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

    //                icount = 0;

    //                lbldriking.Text = "1";
    //            }

    //    }
    //    else if (iwaterpre == 4)
    //    {
    //        if (icount == 0)
    //        {
    //            icount = 4;
    //        }
    //        else if (icount == 1)
    //        {
    //            icount = 4;
    //        }
    //        else if (icount == 2)
    //        {
    //            icount = 4;
    //        }
    //        if (icount == 3)
    //        {
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);
    //            txtdrinking.BackColor = Color.Green;
    //              lbldriking.Text = "1";
    //            icount++;
    //        }
    //        else if (icount == 4)
    //        {
    //            txtdrinking.BackColor = Color.Blue;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
    //            lbldriking.Text = "4";

    //            icount = 3;
    //        }

    //    }
    //    else
    //    {
    //        if (icount == 1)
    //        {
    //            txtdrinking.BackColor = Color.Red;
    //            //btn_water.setBackgroundResource(R.drawable.bg_buttonroundred);
    //            icount++;

    //            lbldriking.Text = "3";

    //        }
    //        else if (icount == 2)
    //        {
    //            txtdrinking.BackColor = Color.Orange;
    //         //   btn_water.setBackgroundResource(R.drawable.bg_buttonroundorane);
    //            icount++;

    //            lbldriking.Text = "2";
    //        }
    //        else if (icount == 3)
    //        {
    //            txtdrinking.BackColor = Color.Green;
    //           // btn_water.setBackgroundResource(R.drawable.bg_buttonroundreen);

    //            // btn_water.setBackgroundResource(R.drawable.green_btn_radio_holo_light);
    //            icount++;

    //            lbldriking.Text = "1";
    //        }
    //        else if (icount == 4)
    //        {
    //          //  btn_water.setBackgroundResource(R.drawable.bg_buttonroundblue);
    //            txtdrinking.BackColor = Color.Blue;
    //            // btn_water.setBackgroundResource(R.drawable.purple_btn_radio_holo_light);
    //            icount++;

    //            lbldriking.Text = "4";
    //            icount = 0;
    //        }
    //        else if (icount == 0)
    //        {
    //            txtdrinking.BackColor = Color.White;
    //           /// btn_water.setBackgroundResource(R.drawable.bg_buttonroundwhite);

    //            icount++;

    //        }

    //    }
    //}
    protected void ddlVilage_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadSchool();
        pnlMain.Enabled = false;
        ddlRemark.SelectedIndex = 0;
        LoadTB();
    }
    public void LoadTB()
    {
        string strQry = "";
        strQry = "      select TBCode,TBname from mstTeamBalika mst  with(nolock) left join mst5Village V on V.VillageCode=mst.VillageCode   	or  V.refVillage16=mst.VillageCode	or V.refVillage17=mst.VillageCode	or  V.refVillage18=mst.VillageCode or  V.refVillage19=mst.VillageCode or  V.refVillage20=mst.VillageCode or  V.refVillage21=mst.VillageCode  or  V.refVillage22=mst.VillageCode  or  V.refVillage23=mst.VillageCode or  V.refVillage24=mst.VillageCode or  V.refVillage25=mst.VillageCode where  V.VillageCode='" + ddlVilage.SelectedValue + "'  ";
        DataTable dtVillageActivtiy = objMain.LoadData(strQry);
        Session["TBView"] = dtVillageActivtiy;

        DataTable dt = dtVillageActivtiy.Copy();
        DataTable dt1 = dtVillageActivtiy.Copy();
        DataTable dt2 = dtVillageActivtiy.Copy();
        objComman.BindDLLDatatable("mstSchool", dtVillageActivtiy, "TBCode,TBname", conditions, "TBname", "asc", ddlGssTbname, "TBname", "TBCode", "Select");
        objComman.BindDLLDatatable("mstSchool", dt, "TBCode,TBname", conditions, "TBname", "asc", ddlMMTb, "TBname", "TBCode", "Select");
        objComman.BindDLLDatatable("mstSchool", dt1, "TBCode,TBname", conditions, "TBname", "asc", ddlliffTb, "TBname", "TBCode", "Select");

        objComman.BindDLLDatatable("mstSchool", dt2, "TBCode,TBname", conditions, "TBname", "asc", ddlBalSabaTB, "TBname", "TBCode", "Select");


    }
    protected void rblTb_Click(object sender, EventArgs e)
    {
        if (rblSMCTB.Checked == true)
        {

            tre1.Visible = false;

            k1.Visible = false;
            k2.Visible = false;
            trGssId.Visible = true;
            rdTeamY.Checked = false;
            rdTeamN.Checked = false;

        }
        else
        {
            if (rdTeamY.Checked == true && rblSMCTB.Checked == false)
            {
                tre1.Visible = true;
            }
            else
            {
                tre1.Visible = false;
            }

            k1.Visible = true;
            k2.Visible = true;

            trGssId.Visible = false;
        }
    }
    protected void rblisTb_Click(object sender, EventArgs e)
    {

        if (rdTeamY.Checked == true)
        {
            tre1.Visible = true;
        }
        else
        {
            tre1.Visible = false;
        }
    }

    protected void divBTB_Click(object sender, EventArgs e)
    {

        if (rblBalsabaTB.Checked == true)
        {
            divBTB.Visible = true;
        }
        else
        {
            divBTB.Visible = false;
        }
    }
    protected void divLiff_Click(object sender, EventArgs e)
    {

        if (rblLifeTB.Checked == true)
        {
            DivLiff.Visible = true;
        }
        else
        {
            DivLiff.Visible = false;

        }
    }

    protected void ddlSchool_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRemark.SelectedIndex = 0;
        string query = "   select isnull(SchoolLevel,0) as SchoolLevel,WorkingStatus ,ManagementType,isnull(LSG,0) as LSG,isnull(BAlVal,0) BAlVal from mstSchool   where SchoolCode='" + this.ddlSchool.SelectedValue + "' ";

        DataTable dataTable2 = this.objMain.LoadData(query);
        Session["SchoolLevel"] = dataTable2.Rows[0]["SchoolLevel"].ToString();
        Session["LSG"] = dataTable2.Rows[0]["SchoolLevel"].ToString();
        Session["BAlVal"] = dataTable2.Rows[0]["BAlVal"].ToString();
        if (dataTable2.Rows[0]["SchoolLevel"].ToString() == "1")
        {
            lblend.Enabled = false;
            ddlMarge.Enabled = false;
            ddlMarge.SelectedIndex = 0;
        }
        else
        {
            ddlMarge.SelectedIndex = 0;

            lblend.Enabled = true;
            ddlMarge.Enabled = true;
        }
        GVregdatabind();

    }
    protected void ddlMarge_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["SchoolLevel"]) == "2")
        {
            if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
            {
                ddlMarge.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select only PS School')</script>", false);
                return;
            }
        }
        btnSerach_Click(btnSerach, null);
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        bool InsertD2d = false;
        for (int i = 0; i < Gv_Display.Rows.Count; i++)
        {
            DropDownList ddlStatus = ((DropDownList)Gv_Display.Rows[i].FindControl("ddlStatus"));
            Label lbUniqueCode = ((Label)Gv_Display.Rows[i].FindControl("lbUniqueCode"));
            Label lblStatus = ((Label)Gv_Display.Rows[i].FindControl("lbStatus"));
            RadioButtonList rblTBFC = ((RadioButtonList)Gv_Display.Rows[i].FindControl("rblTBFC"));
            Int32 Followupcount = 0;

            if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                Followupcount = 1;
            }

            if (lblStatus.Text == "2")
            {


                // SqlParameter[] cmdParameters = new SqlParameter[]
                //{
                //    new SqlParameter("@UniqueCode", lbUniqueCode.Text),
                //    new SqlParameter("@ActivityStatus", ddlStatus.SelectedValue),
                //    new SqlParameter("@TBorFC", rblTBFC.SelectedValue ),
                //    new SqlParameter("@ActivityDate",  DateTime.Now.ToString("yyyy-MM-dd")),

                //};
                // Int32 icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptDtdSaveActvity", cmdParameters);


            }
        }
        if (InsertD2d == true)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

        }
    }

    protected void Gv_Display_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlStatus = ((DropDownList)e.Row.FindControl("ddlStatus"));
            Label lbStatus = ((Label)e.Row.FindControl("lbStatus"));

            Label lbStatusNew = ((Label)e.Row.FindControl("lbStatusNew"));
            ddlStatus.SelectedValue = lbStatus.Text;
            Label lblTBFC = ((Label)e.Row.FindControl("lblTBFC"));

            RadioButtonList rblTBFC = ((RadioButtonList)e.Row.FindControl("rblTBFC"));
            if (lblTBFC.Text == "1")
            {
                rblTBFC.SelectedValue = "1";
            }
            if (lblTBFC.Text == "2")
            {
                rblTBFC.SelectedValue = "2";
            }
            if (lbStatus.Text == "3" || lbStatus.Text == "5")
            {
                ddlStatus.Enabled = false;
            }
            else
            {
                ddlStatus.Enabled = true;
            }

        }
    }


    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlStatus = (DropDownList)row1.FindControl("ddlStatus");

        Label lbStatus = (Label)row1.FindControl("lbStatus");


        Label lbUniqueCode = (Label)row1.FindControl("lbUniqueCode");
        Label lbStatusNew = (Label)row1.FindControl("lbStatusNew");
        //if (ddlStatus.SelectedValue.ToString() == "1")
        //{
        //    string strQry = "   select UniqueCode  from tblDTDMobileActivity2018   where UniqueCode='" + lbUniqueCode.Text + "' and ActivityStatus  ='" + ddlStatus.SelectedValue + "'  ";
        //    DataTable dtUniqueCode = objMain.LoadData(strQry);
        //    if (dtUniqueCode.Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Allready Contact')</script>", false);
        //        ddlStatus.SelectedIndex = 0;
        //        ModalPopupExtender.Show();
        //        return;
        //    }

        //}
        //else if (ddlStatus.SelectedValue.ToString() == "2")
        //{
        //    string strQry = "   select UniqueCode  from tblDTDMobileActivity2018   where UniqueCode='" + lbUniqueCode.Text + "' and ActivityStatus  = 1";
        //    DataTable dtUniqueCode = objMain.LoadData(strQry);
        //    if (dtUniqueCode.Rows.Count > 0)
        //    {
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Contact frist then Follow up')</script>", false);
        //        ddlStatus.SelectedIndex = 0;
        //        ModalPopupExtender.Show();
        //        return;
        //    }
        //}



        lbStatus.Text = "2";






        ModalPopupExtender.Show();
    }
    protected void lnkEnrool_OnClick(object sender, EventArgs e)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@villagecode",   ddlVilage.SelectedValue ),
              new SqlParameter("@Flag","1"),

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertActivityDTD", parm);

        SqlParameter[] parm1 = new SqlParameter[]
            {
       new SqlParameter("@villagecode",   ddlVilage.SelectedValue ),
              new SqlParameter("@Flag","2"),

                 };
        DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertActivityDTD", parm1);


        //string  strQry = " select [mst5village].EGVillagecode + '-' +RIGHT('0000' +  convert(varchar,serial), 4) as UniqueId,UniqueCode,RIGHT('0000' +  convert(varchar,serial), 4) as  UniqueIdNew,ActivityStatus as Status,HHNo,ChildName,FathersName from  [tblDTD] inner join mst5Village on mst5village.villagecode=tblDTD.villagecode or tblDTD.villagecode=mst5village.OldUniqueCode    or tblDTD.villagecode=mst5village.RefVillageCode   where  tblDTD.Status='1' and mst5village.Villagecode= '" + ddlVilage.SelectedValue + "'    and " + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))>=6  and (" + DateTime.Today.Year + " - (YEAR(SurvayDate)-isnull(AgeAson,0))<=14  ) and EduationStatus in(2,3,99)   and EnrollStatus=1 and DeleteFlag<>2";

        //  DataTable dataTable = objMain.LoadData(strQry);

        if (dataTable != null)
        {
            if (dataTable.Rows.Count > 0)
            {
                this.Gv_Display.DataSource = dataTable;
                this.Gv_Display.DataBind();
            }

            Session["D2dBind"] = dataTable;
        }
        this.txtSearch.Text = "";
        ModalPopupExtender.Show();
        ModalPopupExtender1.Hide();
    }
    public void LoadSchool()
    {
        conditions = " Villagecode='" + ddlVilage.SelectedValue + "'  ";

        objComman.BindDLL("Mstschool", "SchoolCode ,Name", conditions, "", "", ddlSchool, "Name", "SchoolCode", "Select");

    }
    protected void btnimgComm1_Click(object sender, EventArgs e)
    {

        imgMKS.ImageUrl = "TabletImage/" + lblMM.Text;
        MpexdrDistrict.Show();
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        //objComman.BindDLLNew("mstGKPDeatils", "Level", "SubjectID='" + ddlSubject.SelectedValue + "' and Fyear='" + Session["FinYear"] + "' ", "Level", "asc", ddlLevel, "Level", "Level", "Select");

        objComman.BindDLLNew("mstGKPDeatils", "Level", "SubjectID='" + ddlSubject.SelectedValue + "'  and FYear='" + Session["FinYear"].ToString() + "' ", "Level", "asc", ddlLevel, "Level", "Level", "Select");
        MpexdrDistrict8.Show();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        objComman.BindDLL("mstGKPDeatils", "NoofLevel as Session,[ID] as NoofLevel", "SubjectID='" + ddlSubject.SelectedValue + "' and FYear='" + Session["FinYear"].ToString() + "' and  Level='" + ddlLevel.SelectedValue + "'  ", "[ID] ", "asc", ddlSSession, "Session", "NoofLevel", "Select");

        // objComman.BindDLL("mstGKPDeatils", "'Session'+' '+ CONVERT(varchar,NoofLevel) as Session,NoofLevel", "SubjectID='" + ddlSubject.SelectedValue + "' and  Level='" + ddlLevel.SelectedValue + "'  and Fyear='" + Session["FinYear"] + "'", "'Session'+' '+ CONVERT(varchar,NoofLevel) ", "asc", ddlSSession, "Session", "NoofLevel", "Select");
        MpexdrDistrict8.Show();
    }
    protected void ddlSSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        //      objComman.BindDLL("mstGKPDeatils", "NoofLevel as Session,[ID] as NoofLevel", "SubjectID='" + ddlSubject.SelectedValue + "' and  Level='" + ddlLevel.SelectedValue + "'  ", "[ID] ", "asc", ddlSSession, "Session", "NoofLevel", "Select");

        SqlParameter[] cmdParameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", ddlSSession.SelectedValue),

                };
        DataTable dataTableCheck = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptLoadMain]", cmdParameters);


        objComman.BindDLLMasterTable("mst5Village", "ID,NoofLevel ", dataTableCheck, "", "NoofLevel", "", ddlSessionType, "NoofLevel", "ID", "Select");
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
        string lblSessionType = (gvr.FindControl("lblSessionType") as Label).Text;

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
        ddlSSession_SelectedIndexChanged(ddlSSession, null);
        if (lblSessionType == "1" || lblSessionType == "2")
        {
            ddlSessionType.SelectedValue = lblSessionType;
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

    #region GKP


    protected void LnkBtnBlockSMC_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueCode = (gvr.FindControl("lblCUniqueChildCode") as Label).Text;
        string lblsubjectid = (gvr.FindControl("lblName") as Label).Text;
        string lblLevelID = (gvr.FindControl("lblGender") as Label).Text;
        string lblSession = (gvr.FindControl("lblSession") as Label).Text;


        lblSCGuId.Text = UniqueCode;
        txtMemberSC.Text = lblsubjectid;
        txtmobile.Text = lblSession;
        ddlSgender.SelectedValue = lblLevelID;

        MpexdrDistrict9.Show();
        //Label lblStatus = (Label)gvr.FindControl("lblStatus");
        //Session["UnquieId"] = UniqueChildCode;
        //Session["StateCode"] = ddlState.SelectedValue;
        //Session["DistCode"] = ddlDistrict.SelectedValue;
        //Session["BlockCode"] = ddlBlock.SelectedValue;
        //Session["PhanyCode"] = ddlPanchayat.SelectedValue;
        //Session["VillCode"] = ddlVillage.SelectedValue;


    }
    protected void btnSaveGkp_Click(object sender, EventArgs e)
    {
        SaveDataGKP();
    }
    protected void btnAddGkp_Click(object sender, EventArgs e)
    {
        ddlSubject.SelectedIndex = 0;
        ddlLevel.Items.Clear();
        ddlSSession.Items.Clear();
        lblGuId.Text = "";
        ddlgender.SelectedIndex = 0;
        ddlSessionType.Items.Clear();
        MpexdrDistrict8.Show();
    }

    protected void btnAddSmc_Click(object sender, EventArgs e)
    {
        if (chkSMC.Checked == true)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select SMC ')</script>", false);
            this.chkSMC.Focus();
            return;
        }
        lblSCGuId.Text = "";
        txtMemberSC.Text = "";
        txtmobile.Text = "";
        UpdataSmcData();
        MpexdrDistrict9.Show();
    }
    protected void btnSaveSmc_Click(object sender, EventArgs e)
    {
        SaveDataSMC();
    }
    protected void gv_scOnDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView GV_Retention = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int dtcount = 0;
            CheckBox ddlAttendance = (CheckBox)e.Row.FindControl("ddlAttendanceSmc");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            if (lblStatus.Text == "1")
            {
                ddlAttendance.Checked = true;
            }
            else
            {
                ddlAttendance.Checked = false;
            }
        }
    }

    public void UpdataSmcData()
    {
        DataTable dtmc = ((DataTable)Session["dtmc"]);
        for (int i = 0; i < gvSmc.Rows.Count; i++)
        {
            CheckBox Attendance = (CheckBox)gvSmc.Rows[i].FindControl("ddlAttendanceSmc");

            Label lblCUniqueChildCode = (Label)gvSmc.Rows[i].FindControl("lblCUniqueChildCode");

            DataRow[] drmain = dtmc.Select("UniqueCode='" + lblCUniqueChildCode.Text + "'");
            if (drmain.Length > 0)
            {

                if (Attendance.Checked == true)
                {
                    drmain[0]["Present"] = 1;
                }


            }
        }
        Session["dtmc"] = dtmc;
    }
    protected void ddlAttendanceSmc_Changed(object sender, EventArgs e)
    {
        try
        {
            string[] arr;
            string Assessment = string.Empty;



            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            int rowIndex = row.RowIndex;

            Label lblUniqueCode = (Label)gvSmc.Rows[rowIndex].FindControl("lblCUniqueChildCode");
            DropDownList ddlact = (DropDownList)gvSmc.Rows[rowIndex].FindControl("ddlAttendanceSmc");
            DataTable dtmc = ((DataTable)Session["dtmc"]);



            DataRow[] drmain = dtmc.Select("UniqueCode='" + lblUniqueCode.Text + "'");
            if (drmain.Length > 0)
            {



                drmain[0]["Present"] = ddlact.SelectedValue;


            }
            Session["dtmc"] = dtmc;



        }
        catch
        {
        }
    }

    protected void SMCDelete_OnClick(object sender, EventArgs e)
    {
        UpdataSmcData();
        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        string UniqueChildCode = (gvSmc.DataKeys[index].Values["UniqueCode"].ToString());



        DataTable dtmc = ((DataTable)Session["dtmc"]);
        dtmc.Rows.Remove(dtmc.Rows[index]);

        SqlParameter[] parm = new SqlParameter[]
            {

              new SqlParameter("@uniquid",UniqueChildCode)

            };

        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteSMCMeeting", parm);
        if (dtmc.Rows.Count > 0)
        {
            Session["dtmc"] = dtmc;
            gvSmc.DataSource = dtmc;
            gvSmc.DataBind();

            int GCount = 0;
            int MCount = 0;
            DataRow[] dr = dtmc.Select("Gender='2'");

            if (dr.Length > 0)
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    GCount = GCount + 1;
                }
            }
            DataRow[] dr1 = dtmc.Select("Gender='1'");
            if (dr1.Length > 0)
            {
                for (int i = 0; i < dr1.Length; i++)
                {
                    MCount = MCount + 1;
                }
            }

            string kk = dtmc.Rows.Count.ToString();
            txtTotalMember.Text = kk;
            txtTotalFmember.Text = GCount.ToString();
            lblTottal.Text = kk;
            lblFemale.Text = GCount.ToString();
            lblmale.Text = MCount.ToString();
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }
        else
        {
            Session["dtmc"] = null;
            gvSmc.DataSource = null;
            gvSmc.DataBind();
            txtTotalMember.Text = "";
            txtTotalFmember.Text = "";
            lblTottal.Text = "";
            lblFemale.Text = "";
            lblmale.Text = "";
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }

        //if (result > 0)
        //{
        //    string conq = "";
        //    string Dateof = txtDate.Text;

        //    string[] b = Dateof.Split('/');

        //    string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        //    conq = "tblSMCAttendanceNew.ActivityDate =('" + FcDate + "')    and tblSMCAttendanceNew.Schoolcode='" + ddlSchool.SelectedValue + "'  ";
        //    DataTable dtGKP = LoadSMCDeatils(conq, "1");
        //    if (dtGKP.Rows.Count > 0)
        //    {
        //        gvSmc.DataSource = dtGKP;
        //        gvSmc.DataBind();


        //        int GCount = 0;
        //        int MCount = 0;
        //        DataRow[] dr = dtGKP.Select("Gender='2'");

        //        if (dr.Length > 0)
        //        {
        //            for (int i = 0; i < dr.Length; i++)
        //            {
        //                GCount = GCount + 1;
        //            }
        //        }
        //        DataRow[] dr1 = dtGKP.Select("Gender='1'");
        //        if (dr1.Length > 0)
        //        {
        //            for (int i = 0; i < dr1.Length; i++)
        //            {
        //                MCount = MCount + 1;
        //            }
        //        }

        //        string kk = dtGKP.Rows.Count.ToString();
        //        txtTotalMember.Text = kk;
        //        txtTotalFmember.Text = GCount.ToString();
        //        lblTottal.Text = kk;
        //        lblFemale.Text = GCount.ToString();
        //        lblmale.Text = MCount.ToString();
        //    }
        //    else
        //    {
        //        gvSmc.DataSource = null;
        //        gvSmc.DataBind();
        //    }

        //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        //}


    }

    public DataTable CreateDataDateSMC()
    {

        DataTable dtSubject = new DataTable();
        dtSubject.Columns.Add(new DataColumn("UniqueCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("VillageCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("SchoolCode", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("ActivityDate", System.Type.GetType("System.DateTime")));
        dtSubject.Columns.Add(new DataColumn("Name", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("Gender", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("TBFC", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("Mobile", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("Present", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("IsPrevEntry", System.Type.GetType("System.String")));
        dtSubject.Columns.Add(new DataColumn("UniqueMemberCode", System.Type.GetType("System.String")));

        Session["dtSmc"] = dtSubject;
        return dtSubject;
    }

    public int SaveDataEnrolmentHistory(string UniqueChildCode, string lookupCode, string PVal, string CVal, string UpdatedBy)
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

            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateENrolmentHistorySmc", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    public DataTable ALLQueryinPage(string Filter)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Filter ", Filter),


        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadAlltypeofPageQuerySmc", cmdParameters);
        return dt;
    }
    public void SaveDataSMC()
    {
        DataTable dtmc = null;
        string con = "";
        string Dateof = txtDate.Text;

        string[] b = Dateof.Split('/');

        string FcDate = b[2] + '-' + b[1] + '-' + b[0];
        if (Session["dtmc"] != null)
        {
            dtmc = ((DataTable)Session["dtmc"]);
        }
        else
        {
            dtmc = CreateDataDateSMC();
        }
        string strMainIDNo = objMain.Generate_RandomString(8);
        string GUId = "";
        string Flag = "";
        if (lblSCGuId.Text.Length > 2)
        {

            //string[] Str = { ddlSgender.SelectedValue, txtMemberSC.Text, txtmobile.Text };

            //DataTable dt = ALLQueryinPage(lblSCGuId.Text);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Columns.Count; i++)
            //    {
            //        if (dt.Rows[0][i].ToString() != Str[i])
            //        {
            //            concatstr = concatstr + "," + "'" + dt.Columns[i].ColumnName + "'";
            //            concatCvalstr = concatCvalstr + "," + Str[i];
            //            concatPvalstr = concatPvalstr + "," + dt.Rows[0][i].ToString();
            //        }
            //    }

            //}
            //if (concatstr != "")
            //{
            //    int Cunt = SaveDataEnrolmentHistory(lblSCGuId.Text, concatstr.Substring(1), concatPvalstr.Substring(1), concatCvalstr.Substring(1),Convert.ToString( Session["UserName"]));
            //}
            DataRow[] drmain = dtmc.Select("UniqueCode='" + lblSCGuId.Text + "'");
            if (drmain.Length > 0)
            {

                drmain[0]["Name"] = txtMemberSC.Text;
                drmain[0]["Gender"] = ddlSgender.SelectedValue;
                drmain[0]["TBFC"] = ddlSgender.SelectedItem.Text;
                drmain[0]["Mobile"] = txtmobile.Text;
            }
        }
        else
        {
            if (lblSCGuId.Text.Length > 2)
            {
                GUId = lblSCGuId.Text;
                Flag = "P";
            }
            else
            {
                GUId = objMain.Generate_RandomString(8);
                Flag = "I";
            }

            DataRow dr;
            dr = dtmc.NewRow();
            dr["UniqueCode"] = "";

            dr["UniqueMemberCode"] = GUId;
            dr["VillageCode"] = ddlVilage.SelectedValue;
            dr["SchoolCode"] = ddlSchool.SelectedValue;
            dr["ActivityDate"] = Convert.ToDateTime(FcDate);
            dr["Name"] = txtMemberSC.Text;
            dr["Gender"] = ddlSgender.SelectedValue;
            dr["TBFC"] = ddlSgender.SelectedItem.Text;
            dr["Mobile"] = txtmobile.Text;
            dr["IsPrevEntry"] = "1";

            dtmc.Rows.Add(dr);


            SqlParameter[] parm = new SqlParameter[]
            {



            new SqlParameter("@UniqueCode", GUId),
              new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
                new SqlParameter("@SchoolCode", ddlSchool.SelectedValue),
             new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate)),

            new SqlParameter("@Name", txtMemberSC.Text),
            new SqlParameter("@Gender", ddlSgender.SelectedValue),
            new SqlParameter("@Mobile", txtmobile.Text),
            new SqlParameter("@CreateBy", Session["UserName"].ToString()),

              new SqlParameter("@Flag", Flag),


              };
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSMC", parm);

        }
        int GCount = 0;
        int MCount = 0;
        if (dtmc.Rows.Count > 0)
        {

            Session["dtmc"] = dtmc;
            gvSmc.DataSource = dtmc;
            gvSmc.DataBind();

            DataRow[] dr = dtmc.Select("Gender='2'");

            if (dr.Length > 0)
            {
                for (int i = 0; i < dr.Length; i++)
                {
                    GCount = GCount + 1;
                }
            }
            DataRow[] dr1 = dtmc.Select("Gender='1'");
            if (dr1.Length > 0)
            {
                for (int i = 0; i < dr1.Length; i++)
                {
                    MCount = MCount + 1;
                }
            }


            string kk = dtmc.Rows.Count.ToString();
            txtTotalMember.Text = kk;
            txtTotalFmember.Text = GCount.ToString();
            lblTottal.Text = kk;
            lblFemale.Text = GCount.ToString();
            lblmale.Text = MCount.ToString();
        }

        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);
        //if (lblSCGuId.Text.Length > 2)
        //{
        //    con = "tblSMCAttendanceNew.ActivityDate =('" + FcDate + "')  and UniqueCode not in('" + lblSCGuId.Text + "') and Name='"+ txtMemberSC.Text + "'  and tblSMCAttendanceNew.Schoolcode='" + ddlSchool.SelectedValue + "'  ";
        //    ////con = "where ActivityDate =('" + FcDate + "') and GUID_GKP not in('" + lblGuId.Text + "')     and Schoolcode='" + ddlSchool.SelectedValue + "'  ";

        //    //DataTable dt = LoadSMCDeatils(con,"2");
        //    //if (dt.Rows.Count > 0)
        //    //{

        //    //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Alreday Exit')</script>", false);
        //    //    MpexdrDistrict9.Show();
        //    //    return;
        //    //}




        //}
        //else
        //{

        //    con = "";


        //}

        //string GUId = "";
        //string Flag = "";
        //string Approve = "";
        //if (Session["user_level"].ToString() == "19")
        //{
        //    Approve = "FC";
        //}
        //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        //{
        //    Approve = "B";
        //}
        //Int32 TB = 0;
        //Int32 FC = 0;
        //if (lblSCGuId.Text.Length > 2)
        //{
        //    GUId = lblSCGuId.Text;
        //    Flag = "P";
        //}
        //else
        //{
        //    GUId = objMain.Generate_RandomString(8);
        //    Flag = "I";
        //}
        //if (Convert.ToInt32(rblApprove.SelectedValue) == 1)
        //{
        //    FC = 1;
        //}
        //if (Convert.ToInt32(rblApprove.SelectedValue) == 2)
        //{
        //    TB = 1;
        //}

        //SqlParameter[] parm = new SqlParameter[]
        //    {



        //    new SqlParameter("@UniqueCode", GUId),
        //      new SqlParameter("@VillageCode", ddlVilage.SelectedValue),
        //        new SqlParameter("@SchoolCode", ddlSchool.SelectedValue),
        //     new SqlParameter("@ActivityDate",Convert.ToDateTime(FcDate)),

        //    new SqlParameter("@Name", txtMemberSC.Text),
        //    new SqlParameter("@Gender", ddlSgender.SelectedValue),
        //    new SqlParameter("@Mobile", txtmobile.Text),
        //    new SqlParameter("@CreateBy", Session["UserName"].ToString()),           

        //      new SqlParameter("@Flag", Flag),


        //      };
        //int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSMC", parm);

        //if (result > 0)
        //{
        //    string conq = "";
        //    //if (Session["user_level"].ToString() == "19")
        //    //{

        //    //    conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and ApproveStatus='FC' ";

        //    //}
        //    //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        //    //{

        //    //    conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' and ApproveStatus='B' ";

        //    //}

        //    conq = "tblSMCAttendanceNew.ActivityDate =('" + FcDate + "')    and tblSMCAttendanceNew.Schoolcode='" + ddlSchool.SelectedValue + "'  ";
        //    DataTable dtGKP = LoadSMCDeatils(conq,"1");
        //    if (dtGKP.Rows.Count > 0)
        //    {
        //        gvSmc.DataSource = dtGKP;
        //        gvSmc.DataBind();

        //        int GCount = 0;
        //        int MCount = 0;
        //        DataRow[] dr = dtGKP.Select("Gender='2'");

        //        if (dr.Length > 0)
        //        {
        //            for (int i = 0; i < dr.Length; i++)
        //            {
        //                GCount = GCount + 1;
        //            }
        //        }
        //        DataRow[] dr1 = dtGKP.Select("Gender='1'");
        //        if (dr1.Length > 0)
        //        {
        //            for (int i = 0; i < dr1.Length; i++)
        //            {
        //                MCount = MCount + 1;
        //            }
        //        }

        //        string kk = dtGKP.Rows.Count.ToString();
        //        txtTotalMember.Text = kk;
        //        txtTotalFmember.Text = GCount.ToString();
        //        lblTottal.Text = kk;
        //        lblFemale.Text = GCount.ToString();
        //        lblmale.Text = MCount.ToString();
        //    }

        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);

        //}
    }
    public DataTable LoadSMCDeatils(string WhereQuery, string Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", WhereQuery)    ,
                new SqlParameter("@Flag", Flag)    ,

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSMCDeatilsNew2025]", cmdParameters);
    }
    public void SaveDataGKP()
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
        if (ddlSessionType.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select SessionType')</script>", false);
            MpexdrDistrict8.Show();
            return;
        }
        if (lblGuId.Text.Length > 2)
        {
            con = "where ActivityDate =('" + FcDate + "') and GUID_GKP not in('" + lblGuId.Text + "')     and Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='" + ddlSSession.SelectedItem.Text + "'  ";

            DataTable dt = objMain.LoadCheckGkp(con);
            if (dt.Rows.Count > 0)
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Alreday Exit')</script>", false);
                MpexdrDistrict8.Show();
                return;
            }
            //if (ddlLevel.SelectedItem.Text == "L1")
            //{
            //    if (ddlSSession.SelectedItem.Text.ToString() == "Recap 1" || ddlSSession.SelectedItem.Text == "Recap 2")
            //    {
            //        con = "where Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='L0'    ";
            //        DataTable dtRec = objMain.LoadCheckGkp(con);

            //        if (dtRec.Rows.Count > 0 && dtRec.Rows.Count > 0)
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please  select Recap1 and Recap2 Frist')</script>", false);
            //            MpexdrDistrict8.Show();
            //            return;
            //        }

            //    }
            //}

            //if (ddlLevel.SelectedItem.Text == "L2")
            //{
            //    if (ddlSSession.SelectedItem.Text.ToString() == "Recap 1" || ddlSSession.SelectedItem.Text == "Recap 2")
            //    {

            //    }
            //    else
            //    {
            //        con = "where Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='Recap 1'   ";
            //        DataTable dtRec = objMain.LoadCheckGkp(con);


            //        con = "where Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'   and  Session='Recap 2' ";
            //        DataTable dtRec1 = objMain.LoadCheckGkp(con);
            //        if (dtRec.Rows.Count > 0 && dtRec.Rows.Count > 0)
            //        {
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please  select Recap1 and Reacp2 Frist')</script>", false);
            //            MpexdrDistrict8.Show();
            //            return;
            //        }
            //    }
            //}

        }
        else
        {
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                con = "where ActivityDate =('" + FcDate + "') and ApproveStatus<>'FC'   and Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='" + ddlSSession.SelectedItem.Text + "'  ";
                DataTable dt = objMain.LoadCheckGkp(con);
                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Alreday Exit')</script>", false);
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

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('This Activty Alreday Exit')</script>", false);
                    MpexdrDistrict8.Show();
                    return;
                }
            }
            con = "";
            //if (ddlLevel.SelectedItem.Text == "L1")
            //{
            //    if (ddlSSession.SelectedItem.Text.ToString() == "Recap 1" || ddlSSession.SelectedItem.Text == "Recap 2")
            //    {
            //        con = "where Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='L0'    ";
            //        DataTable dtRec = objMain.LoadCheckGkp(con);

            //        if (dtRec.Rows.Count > 0)
            //        {

            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please  Add  LevelID  L0 Frist')</script>", false);
            //            MpexdrDistrict8.Show();
            //            return;
            //        }

            //    }
            //}
            //if (ddlLevel.SelectedItem.Text == "L2")
            //{
            //    if (ddlSSession.SelectedItem.Text.ToString() == "Recap 1" || ddlSSession.SelectedItem.Text == "Recap 2")
            //    {

            //    }
            //    else
            //    {
            //        con = "where Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'  and  Session='Recap 1'   ";
            //        DataTable dtRec = objMain.LoadCheckGkp(con);


            //        con = "where Schoolcode='" + ddlSchool.SelectedValue + "' and  SubjectID='" + ddlSubject.SelectedValue + "'  and  LevelID='" + ddlLevel.SelectedValue + "'   and  Session='Recap 2' ";
            //        DataTable dtRec1 = objMain.LoadCheckGkp(con);
            //        if (dtRec.Rows.Count > 0 && dtRec.Rows.Count > 0)
            //        { 
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please  select Recap1 and Recap2 Frist')</script>", false);
            //            MpexdrDistrict8.Show();
            //            return;
            //        }
            //    }
            //}

        }

        string GUId = "";
        string Flag = "";
        string Approve = "";
        if (Session["user_level"].ToString() == "19")
        {
            Approve = "FC";
        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            Approve = "B";
        }
        Int32 TB = 0;
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
            FC = 1;
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
             new SqlParameter("@ApproveStatus", Approve),
              new SqlParameter("@Flag", Flag),
               new SqlParameter("@SessionType", ddlSessionType.SelectedValue),
                  new SqlParameter("@GKPSessionID", ddlSSession.SelectedValue),

              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateGkpNew", parm);

        if (result > 0)
        {
            string conq = "";

            conq = "ActivityDate =('" + FcDate + "')    and Schoolcode='" + ddlSchool.SelectedValue + "' ";




            DataTable dtGKP = objMain.LoadGKPDeatils(conq);
            if (dtGKP.Rows.Count > 0)
            {
                gvGkp.DataSource = dtGKP;
                gvGkp.DataBind();
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);

        }
    }
    protected void btnimgComm2_Click(object sender, EventArgs e)
    {
        // imgMKS.ImageUrl = Server.MapPath("~/TabletImage/" + lblCom1.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblCom1.Text;
        //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" +  lblCom1.Text);
        MpexdrDistrict.Show();
    }
    protected void btnimgComm22_Click(object sender, EventArgs e)
    {
        // imgMKS.ImageUrl = Server.MapPath("~/TabletImage/" + lblCom1.Text);
        imgMKS.ImageUrl = "TabletImage/" + lblCom22.Text;
        //imgMKS.ImageUrl = ResolveUrl("~/TabletImage/" +  lblCom1.Text);
        MpexdrDistrict.Show();
    }


    #endregion


    protected void ddlAttendance_Changed(object sender, EventArgs e)
    {
        try
        {
            string[] arr;
            string Assessment = string.Empty;


            if ((ddlsession.SelectedItem.Text == "Session-1") || (ddlsession.SelectedItem.Text == "Session-10") && Convert.ToString(Session["LSG"]) == "1")
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;

                Label lblUniqueCode = (Label)GvReg.Rows[rowIndex].FindControl("lblUniqueChildRCode");
                DropDownList Attendance = (DropDownList)GvReg.Rows[rowIndex].FindControl("ddlAttendance");
                LinkButton LinkButton = (LinkButton)GvReg.Rows[rowIndex].FindControl("lbtn1");

                if (Attendance.SelectedValue == "1")
                {
                    //Label lblUniqueCode = (Label)row.FindControl("lblUniqueChildCode");
                    LinkButton.Visible = true;
                    string UniqueCode = lblUniqueCode.Text;
                    Session["UniqueCode"] = UniqueCode;
                    string sessiondata = Convert.ToString(ddlsession.SelectedValue);
                    DataTable dt = ALLQueryinPage("", "", "", sessiondata, UniqueCode, "12");
                    if (dt.Rows.Count > 0)
                    {
                        Assessment = dt.Rows[0]["Assessment"].ToString();
                        if (Assessment != "")
                        {
                            arr = Assessment.ToString().Split(',');
                            for (int i = 0; i < 56; i++)
                            {

                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + arr[i]) as RadioButton).Checked = true;
                            }


                        }
                        else
                        {
                            #region Disabled Radio bUtton
                            trQ2.Visible = false;
                            trQ3.Visible = false;
                            trQ4.Visible = false;
                            trQ5.Visible = false;
                            trQ6.Visible = false;
                            trQ7.Visible = false;
                            trQ8.Visible = false;
                            trQ9.Visible = false;
                            trQ10.Visible = false;
                            trQ11.Visible = false;
                            trQ12.Visible = false;
                            trQ13.Visible = false;
                            trQ14.Visible = false;
                            trQ15.Visible = false;
                            trQ16.Visible = false;
                            trQ17.Visible = false;
                            trQ18.Visible = false;
                            trQ19.Visible = false;
                            trQ20.Visible = false;
                            trQ21.Visible = false;
                            trQ22.Visible = false;
                            trQ23.Visible = false;
                            trQ24.Visible = false;
                            trQ25.Visible = false;
                            trQ26.Visible = false;
                            trQ27.Visible = false;
                            trQ28.Visible = false;
                            trQ29.Visible = false;
                            trQ30.Visible = false;

                            trQ31.Visible = false;
                            trQ32.Visible = false;
                            trQ33.Visible = false;
                            trQ34.Visible = false;
                            trQ35.Visible = false;
                            trQ36.Visible = false;
                            trQ37.Visible = false;
                            trQ38.Visible = false;
                            trQ39.Visible = false;
                            trQ40.Visible = false;

                            trQ41.Visible = false;
                            trQ42.Visible = false;
                            trQ43.Visible = false;
                            trQ44.Visible = false;
                            trQ45.Visible = false;
                            trQ46.Visible = false;
                            trQ47.Visible = false;
                            trQ48.Visible = false;
                            trQ49.Visible = false;
                            trQ50.Visible = false;
                            trQ51.Visible = false;
                            trQ52.Visible = false;
                            trQ53.Visible = false;
                            trQ54.Visible = false;
                            trQ55.Visible = false;
                            trQ56.Visible = false;




                            #endregion

                            #region Unchecked Radio bUtton

                            for (int i = 0; i < 56; i++)
                            {
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 5) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 4) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 3) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 2) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 1) as RadioButton).Checked = false;
                            }

                            #endregion
                        }

                    }
                    else
                    {
                        #region Disabled Radio bUtton
                        trQ2.Visible = false;
                        trQ3.Visible = false;
                        trQ4.Visible = false;
                        trQ5.Visible = false;
                        trQ6.Visible = false;
                        trQ7.Visible = false;
                        trQ8.Visible = false;
                        trQ9.Visible = false;
                        trQ10.Visible = false;
                        trQ11.Visible = false;
                        trQ12.Visible = false;
                        trQ13.Visible = false;
                        trQ14.Visible = false;
                        trQ15.Visible = false;
                        trQ16.Visible = false;
                        trQ17.Visible = false;
                        trQ18.Visible = false;
                        trQ19.Visible = false;
                        trQ20.Visible = false;
                        trQ21.Visible = false;
                        trQ22.Visible = false;
                        trQ23.Visible = false;
                        trQ24.Visible = false;
                        trQ25.Visible = false;
                        trQ26.Visible = false;
                        trQ27.Visible = false;
                        trQ28.Visible = false;
                        trQ29.Visible = false;
                        trQ30.Visible = false;

                        trQ31.Visible = false;
                        trQ32.Visible = false;
                        trQ33.Visible = false;
                        trQ34.Visible = false;
                        trQ35.Visible = false;
                        trQ36.Visible = false;
                        trQ37.Visible = false;
                        trQ38.Visible = false;
                        trQ39.Visible = false;
                        trQ40.Visible = false;

                        trQ41.Visible = false;
                        trQ42.Visible = false;
                        trQ43.Visible = false;
                        trQ44.Visible = false;
                        trQ45.Visible = false;
                        trQ46.Visible = false;
                        trQ47.Visible = false;
                        trQ48.Visible = false;
                        trQ49.Visible = false;
                        trQ50.Visible = false;
                        trQ51.Visible = false;
                        trQ52.Visible = false;
                        trQ53.Visible = false;
                        trQ54.Visible = false;
                        trQ55.Visible = false;
                        trQ56.Visible = false;




                        #endregion

                        #region Unchecked Radio bUtton

                        for (int i = 0; i < 56; i++)
                        {
                            (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 5) as RadioButton).Checked = false;
                            (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 4) as RadioButton).Checked = false;
                            (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 3) as RadioButton).Checked = false;
                            (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 2) as RadioButton).Checked = false;
                            (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 1) as RadioButton).Checked = false;
                        }

                        #endregion
                    }
                    btnSaveAttendance.Visible = true;
                    ModalAddclass1.Show();
                }
                else
                {
                    LinkButton.Visible = false;
                    #region Unchecked Radio bUtton

                    for (int i = 0; i < 56; i++)
                    {
                        (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 5) as RadioButton).Checked = false;
                        (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 4) as RadioButton).Checked = false;
                        (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 3) as RadioButton).Checked = false;
                        (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 2) as RadioButton).Checked = false;
                        (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 1) as RadioButton).Checked = false;
                    }

                    #endregion
                }
            }
            else
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.NamingContainer;
                int rowIndex = row.RowIndex;
                LinkButton LinkButton = (LinkButton)GvReg.Rows[rowIndex].FindControl("lbtn1");
                DropDownList Attendance = (DropDownList)GvReg.Rows[rowIndex].FindControl("ddlAttendance");
                LinkButton.Visible = false;
                if (ddlsession.SelectedIndex <= 0)
                {
                    Attendance.SelectedIndex = 0;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Session')</script>", false);



                    return;
                }
            }
        }
        catch
        {
        }
    }

    protected void btnSaveAttendance_savedata(object sender, EventArgs e)
    {
        string Assessment = string.Empty;
        #region trQ2
        if (rdbQ1_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ1_5.Text;
        }
        else if (rdbQ1_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ1_4.Text;
        }
        else if (rdbQ1_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ1_3.Text;
        }
        else if (rdbQ1_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ1_2.Text;
        }
        else if (rdbQ1_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ1_1.Text;
        }
        #endregion

        #region trQ3
        if (rdbQ2_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ2_5.Text;
        }
        else if (rdbQ2_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ2_4.Text;
        }
        else if (rdbQ2_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ2_3.Text;
        }
        else if (rdbQ2_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ2_2.Text;
        }
        else if (rdbQ2_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ2_1.Text;
        }
        #endregion

        #region trQ4

        if (rdbQ3_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ3_5.Text;
        }
        else if (rdbQ3_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ3_4.Text;
        }
        else if (rdbQ3_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ3_3.Text;
        }
        else if (rdbQ3_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ3_2.Text;
        }
        else if (rdbQ3_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ3_1.Text;
        }

        #endregion

        #region trQ5

        if (rdbQ4_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ4_5.Text;
        }
        else if (rdbQ4_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ4_4.Text;
        }
        else if (rdbQ4_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ4_3.Text;
        }
        else if (rdbQ4_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ4_2.Text;
        }
        else if (rdbQ4_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ4_1.Text;
        }
        #endregion

        #region trQ6

        if (rdbQ5_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ5_5.Text;
        }
        else if (rdbQ5_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ5_4.Text;
        }
        else if (rdbQ5_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ5_3.Text;
        }
        else if (rdbQ5_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ5_2.Text;
        }
        else if (rdbQ5_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ5_1.Text;
        }
        #endregion

        #region trQ7

        if (rdbQ6_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ6_5.Text;
        }
        else if (rdbQ6_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ6_4.Text;
        }
        else if (rdbQ6_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ6_3.Text;
        }
        else if (rdbQ6_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ6_2.Text;
        }
        else if (rdbQ6_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ6_1.Text;
        }
        #endregion

        #region trQ8

        if (rdbQ7_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ7_5.Text;
        }
        else if (rdbQ7_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ7_4.Text;
        }
        else if (rdbQ7_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ7_3.Text;
        }
        else if (rdbQ7_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ7_2.Text;
        }
        else if (rdbQ7_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ7_1.Text;
        }
        #endregion

        #region trQ9

        if (rdbQ8_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ8_5.Text;
        }
        else if (rdbQ8_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ8_4.Text;
        }
        else if (rdbQ8_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ8_3.Text;
        }
        else if (rdbQ8_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ8_2.Text;
        }
        else if (rdbQ8_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ8_1.Text;
        }
        #endregion

        #region trQ10

        if (rdbQ9_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ9_5.Text;
        }
        else if (rdbQ9_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ9_4.Text;
        }
        else if (rdbQ9_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ9_3.Text;
        }
        else if (rdbQ9_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ9_2.Text;
        }
        else if (rdbQ9_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ9_1.Text;
        }
        #endregion

        #region trQ11

        if (rdbQ10_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ10_5.Text;
        }
        else if (rdbQ10_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ10_4.Text;
        }
        else if (rdbQ10_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ10_3.Text;
        }
        else if (rdbQ10_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ10_2.Text;
        }
        else if (rdbQ10_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ10_1.Text;
        }
        #endregion

        #region trQ12

        if (rdbQ11_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ11_5.Text;
        }
        else if (rdbQ11_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ11_4.Text;
        }
        else if (rdbQ11_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ11_3.Text;
        }
        else if (rdbQ11_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ11_2.Text;
        }
        else if (rdbQ11_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ11_1.Text;
        }
        #endregion

        #region trQ13

        if (rdbQ12_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ12_5.Text;
        }
        else if (rdbQ12_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ12_4.Text;
        }
        else if (rdbQ12_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ12_3.Text;
        }
        else if (rdbQ12_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ12_2.Text;
        }
        else if (rdbQ12_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ12_1.Text;
        }
        #endregion

        #region trQ14

        if (rdbQ13_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ13_5.Text;
        }
        else if (rdbQ13_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ13_4.Text;
        }
        else if (rdbQ13_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ13_3.Text;
        }
        else if (rdbQ13_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ13_2.Text;
        }
        else if (rdbQ13_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ13_1.Text;
        }
        #endregion

        #region trQ15

        if (rdbQ14_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ14_5.Text;
        }
        else if (rdbQ14_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ14_4.Text;
        }
        else if (rdbQ14_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ14_3.Text;
        }
        else if (rdbQ14_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ14_2.Text;
        }
        else if (rdbQ14_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ14_1.Text;
        }
        #endregion

        #region trQ16

        if (rdbQ15_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ15_5.Text;
        }
        else if (rdbQ15_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ15_4.Text;
        }
        else if (rdbQ15_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ15_3.Text;
        }
        else if (rdbQ15_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ15_2.Text;
        }
        else if (rdbQ15_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ15_1.Text;
        }
        #endregion

        #region trQ17

        if (rdbQ16_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ16_5.Text;
        }
        else if (rdbQ16_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ16_4.Text;
        }
        else if (rdbQ16_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ16_3.Text;
        }
        else if (rdbQ16_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ16_2.Text;
        }
        else if (rdbQ16_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ16_1.Text;
        }
        #endregion

        #region trQ18

        if (rdbQ17_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ17_5.Text;
        }
        else if (rdbQ17_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ17_4.Text;
        }
        else if (rdbQ17_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ17_3.Text;
        }
        else if (rdbQ17_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ17_2.Text;
        }
        else if (rdbQ17_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ17_1.Text;
        }
        #endregion

        #region trQ19

        if (rdbQ18_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ18_5.Text;
        }
        else if (rdbQ18_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ18_4.Text;
        }
        else if (rdbQ18_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ18_3.Text;
        }
        else if (rdbQ18_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ18_2.Text;
        }
        else if (rdbQ18_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ18_1.Text;
        }
        #endregion

        #region trQ20

        if (rdbQ19_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ19_5.Text;
        }
        else if (rdbQ19_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ19_4.Text;
        }
        else if (rdbQ19_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ19_3.Text;
        }
        else if (rdbQ19_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ19_2.Text;
        }
        else if (rdbQ19_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ19_1.Text;
        }
        #endregion

        #region trQ21

        if (rdbQ20_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ20_5.Text;
        }
        else if (rdbQ20_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ20_4.Text;
        }
        else if (rdbQ20_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ20_3.Text;
        }
        else if (rdbQ20_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ20_2.Text;
        }
        else if (rdbQ20_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ20_1.Text;
        }
        #endregion

        #region trQ22

        if (rdbQ21_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ21_5.Text;
        }
        else if (rdbQ21_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ21_4.Text;
        }
        else if (rdbQ21_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ21_3.Text;
        }
        else if (rdbQ21_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ21_2.Text;
        }
        else if (rdbQ21_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ21_1.Text;
        }
        #endregion

        #region trQ23

        if (rdbQ22_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ22_5.Text;
        }
        else if (rdbQ22_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ22_4.Text;
        }
        else if (rdbQ22_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ22_3.Text;
        }
        else if (rdbQ22_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ22_2.Text;
        }
        else if (rdbQ22_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ22_1.Text;
        }
        #endregion

        #region trQ24

        if (rdbQ23_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ23_5.Text;
        }
        else if (rdbQ23_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ23_4.Text;
        }
        else if (rdbQ23_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ23_3.Text;
        }
        else if (rdbQ23_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ23_2.Text;
        }
        else if (rdbQ23_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ23_1.Text;
        }
        #endregion

        #region trQ25

        if (rdbQ24_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ24_5.Text;
        }
        else if (rdbQ24_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ24_4.Text;
        }
        else if (rdbQ24_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ24_3.Text;
        }
        else if (rdbQ24_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ24_2.Text;
        }
        else if (rdbQ24_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ24_1.Text;
        }
        #endregion

        #region trQ26

        if (rdbQ25_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ25_5.Text;
        }
        else if (rdbQ25_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ25_4.Text;
        }
        else if (rdbQ25_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ25_3.Text;
        }
        else if (rdbQ25_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ25_2.Text;
        }
        else if (rdbQ25_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ25_1.Text;
        }
        #endregion

        #region trQ27

        if (rdbQ26_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ26_5.Text;
        }
        else if (rdbQ26_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ26_4.Text;
        }
        else if (rdbQ26_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ26_3.Text;
        }
        else if (rdbQ26_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ26_2.Text;
        }
        else if (rdbQ26_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ26_1.Text;
        }
        #endregion

        #region trQ28

        if (rdbQ27_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ27_5.Text;
        }
        else if (rdbQ27_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ27_4.Text;
        }
        else if (rdbQ27_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ27_3.Text;
        }
        else if (rdbQ27_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ27_2.Text;
        }
        else if (rdbQ27_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ27_1.Text;
        }
        #endregion

        #region trQ29

        if (rdbQ28_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ28_5.Text;
        }
        else if (rdbQ28_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ28_4.Text;
        }
        else if (rdbQ28_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ28_3.Text;
        }
        else if (rdbQ28_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ28_2.Text;
        }
        else if (rdbQ28_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ28_1.Text;
        }
        #endregion

        #region trQ30

        if (rdbQ29_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ29_5.Text;
        }
        else if (rdbQ29_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ29_4.Text;
        }
        else if (rdbQ29_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ29_3.Text;
        }
        else if (rdbQ29_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ29_2.Text;
        }
        else if (rdbQ29_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ29_1.Text;
        }
        #endregion

        #region trQ31

        if (rdbQ30_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ30_5.Text;
        }
        else if (rdbQ30_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ30_4.Text;
        }
        else if (rdbQ30_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ30_3.Text;
        }
        else if (rdbQ30_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ30_2.Text;
        }
        else if (rdbQ30_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ30_1.Text;
        }
        #endregion

        #region trQ32

        if (rdbQ31_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ31_5.Text;
        }
        else if (rdbQ31_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ31_4.Text;
        }
        else if (rdbQ31_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ31_3.Text;
        }
        else if (rdbQ31_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ31_2.Text;
        }
        else if (rdbQ31_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ31_1.Text;
        }
        #endregion

        #region trQ33

        if (rdbQ32_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ32_5.Text;
        }
        else if (rdbQ32_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ32_4.Text;
        }
        else if (rdbQ32_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ32_3.Text;
        }
        else if (rdbQ32_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ32_2.Text;
        }
        else if (rdbQ32_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ32_1.Text;
        }
        #endregion

        #region trQ34

        if (rdbQ33_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ33_5.Text;
        }
        else if (rdbQ33_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ33_4.Text;
        }
        else if (rdbQ33_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ33_3.Text;
        }
        else if (rdbQ33_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ33_2.Text;
        }
        else if (rdbQ33_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ33_1.Text;
        }
        #endregion

        #region trQ35

        if (rdbQ34_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ34_5.Text;
        }
        else if (rdbQ34_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ34_4.Text;
        }
        else if (rdbQ34_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ34_3.Text;
        }
        else if (rdbQ34_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ34_2.Text;
        }
        else if (rdbQ34_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ34_1.Text;
        }
        #endregion

        #region trQ36

        if (rdbQ35_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ35_5.Text;
        }
        else if (rdbQ35_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ35_4.Text;
        }
        else if (rdbQ35_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ35_3.Text;
        }
        else if (rdbQ35_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ35_2.Text;
        }
        else if (rdbQ35_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ35_1.Text;
        }
        #endregion

        #region trQ37

        if (rdbQ36_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ36_5.Text;
        }
        else if (rdbQ36_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ36_4.Text;
        }
        else if (rdbQ36_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ36_3.Text;
        }
        else if (rdbQ36_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ36_2.Text;
        }
        else if (rdbQ36_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ36_1.Text;
        }
        #endregion

        #region trQ38

        if (rdbQ37_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ37_5.Text;
        }
        else if (rdbQ37_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ37_4.Text;
        }
        else if (rdbQ37_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ37_3.Text;
        }
        else if (rdbQ37_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ37_2.Text;
        }
        else if (rdbQ37_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ37_1.Text;
        }
        #endregion

        #region trQ39

        if (rdbQ38_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ38_5.Text;
        }
        else if (rdbQ38_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ38_4.Text;
        }
        else if (rdbQ38_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ38_3.Text;
        }
        else if (rdbQ38_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ38_2.Text;
        }
        else if (rdbQ38_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ38_1.Text;
        }
        #endregion

        #region trQ40

        if (rdbQ39_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ39_5.Text;
        }
        else if (rdbQ39_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ39_4.Text;
        }
        else if (rdbQ39_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ39_3.Text;
        }
        else if (rdbQ39_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ39_2.Text;
        }
        else if (rdbQ39_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ39_1.Text;
        }
        #endregion
        #region trQ41

        if (rdbQ40_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ40_5.Text;
        }
        else if (rdbQ40_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ40_4.Text;
        }
        else if (rdbQ40_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ40_3.Text;
        }
        else if (rdbQ40_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ40_2.Text;
        }
        else if (rdbQ40_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ40_1.Text;
        }
        #endregion

        #region trQ42

        if (rdbQ41_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ41_5.Text;
        }
        else if (rdbQ41_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ41_4.Text;
        }
        else if (rdbQ41_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ41_3.Text;
        }
        else if (rdbQ41_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ41_2.Text;
        }
        else if (rdbQ41_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ41_1.Text;
        }
        #endregion

        #region trQ43

        if (rdbQ42_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ42_5.Text;
        }
        else if (rdbQ42_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ42_4.Text;
        }
        else if (rdbQ42_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ42_3.Text;
        }
        else if (rdbQ42_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ42_2.Text;
        }
        else if (rdbQ42_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ42_1.Text;
        }
        #endregion

        #region trQ44

        if (rdbQ43_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ43_5.Text;
        }
        else if (rdbQ43_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ43_4.Text;
        }
        else if (rdbQ43_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ43_3.Text;
        }
        else if (rdbQ43_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ43_2.Text;
        }
        else if (rdbQ43_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ43_1.Text;
        }
        #endregion

        #region trQ45

        if (rdbQ44_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ44_5.Text;
        }
        else if (rdbQ44_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ44_4.Text;
        }
        else if (rdbQ44_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ44_3.Text;
        }
        else if (rdbQ44_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ44_2.Text;
        }
        else if (rdbQ44_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ44_1.Text;
        }
        #endregion

        #region trQ46

        if (rdbQ45_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ45_5.Text;
        }
        else if (rdbQ45_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ45_4.Text;
        }
        else if (rdbQ45_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ45_3.Text;
        }
        else if (rdbQ45_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ45_2.Text;
        }
        else if (rdbQ45_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ45_1.Text;
        }
        #endregion

        #region trQ47

        if (rdbQ46_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ46_5.Text;
        }
        else if (rdbQ46_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ46_4.Text;
        }
        else if (rdbQ46_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ46_3.Text;
        }
        else if (rdbQ46_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ46_2.Text;
        }
        else if (rdbQ46_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ46_1.Text;
        }
        #endregion

        #region trQ48

        if (rdbQ47_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ47_5.Text;
        }
        else if (rdbQ47_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ47_4.Text;
        }
        else if (rdbQ47_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ47_3.Text;
        }
        else if (rdbQ47_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ47_2.Text;
        }
        else if (rdbQ47_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ47_1.Text;
        }
        #endregion

        #region trQ49

        if (rdbQ48_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ48_5.Text;
        }
        else if (rdbQ48_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ48_4.Text;
        }
        else if (rdbQ48_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ48_3.Text;
        }
        else if (rdbQ48_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ48_2.Text;
        }
        else if (rdbQ48_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ48_1.Text;
        }
        #endregion


        #region trQ50

        if (rdbQ49_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ49_5.Text;
        }
        else if (rdbQ49_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ49_4.Text;
        }
        else if (rdbQ49_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ49_3.Text;
        }
        else if (rdbQ49_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ49_2.Text;
        }
        else if (rdbQ49_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ49_1.Text;
        }
        #endregion

        #region trQ51

        if (rdbQ50_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ50_5.Text;
        }
        else if (rdbQ50_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ50_4.Text;
        }
        else if (rdbQ50_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ50_3.Text;
        }
        else if (rdbQ50_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ50_2.Text;
        }
        else if (rdbQ50_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ50_1.Text;
        }
        #endregion

        #region trQ52

        if (rdbQ51_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ51_5.Text;
        }
        else if (rdbQ51_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ51_4.Text;
        }
        else if (rdbQ51_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ51_3.Text;
        }
        else if (rdbQ51_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ51_2.Text;
        }
        else if (rdbQ51_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ51_1.Text;
        }
        #endregion

        #region trQ53

        if (rdbQ52_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ52_5.Text;
        }
        else if (rdbQ52_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ52_4.Text;
        }
        else if (rdbQ52_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ52_3.Text;
        }
        else if (rdbQ52_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ52_2.Text;
        }
        else if (rdbQ52_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ52_1.Text;
        }
        #endregion

        #region trQ54

        if (rdbQ53_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ53_5.Text;
        }
        else if (rdbQ53_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ53_4.Text;
        }
        else if (rdbQ53_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ53_3.Text;
        }
        else if (rdbQ53_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ53_2.Text;
        }
        else if (rdbQ53_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ53_1.Text;
        }
        #endregion

        #region trQ55

        if (rdbQ54_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ54_5.Text;
        }
        else if (rdbQ54_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ54_4.Text;
        }
        else if (rdbQ54_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ54_3.Text;
        }
        else if (rdbQ54_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ54_2.Text;
        }
        else if (rdbQ54_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ54_1.Text;
        }
        #endregion

        #region trQ56

        if (rdbQ55_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ55_5.Text;
        }
        else if (rdbQ55_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ55_4.Text;
        }
        else if (rdbQ55_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ55_3.Text;
        }
        else if (rdbQ55_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ55_2.Text;
        }
        else if (rdbQ55_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ55_1.Text;
        }
        #endregion


        if (rdbQ56_5.Checked)
        {
            Assessment = Assessment + "," + rdbQ56_5.Text;
        }
        else if (rdbQ56_4.Checked)
        {
            Assessment = Assessment + "," + rdbQ56_4.Text;
        }
        else if (rdbQ56_3.Checked)
        {
            Assessment = Assessment + "," + rdbQ56_3.Text;
        }
        else if (rdbQ56_2.Checked)
        {
            Assessment = Assessment + "," + rdbQ56_2.Text;
        }
        else if (rdbQ56_1.Checked)
        {
            Assessment = Assessment + "," + rdbQ56_1.Text;
        }

        Int32 MainResult = 0;
        ViewState["GUID_School"].ToString();
        int sessiondata = Convert.ToInt32(ddlsession.SelectedValue);

        if (Assessment.Substring(1).Split(',').Length.ToString() != "56")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select all values')</script>", false);
            ModalAddclass1.Show();
        }
        else
        {
            MainResult = InsertChildAssessment(Session["UniqueCode"].ToString(), Assessment.Substring(1), sessiondata);
        }

    }


    public int InsertChildAssessment(string UniqueChildRCode, string Assessment, int sessiondata)
    {

        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                new SqlParameter("@UniqueChildRCode", UniqueChildRCode),
                  new SqlParameter("@Assessment", Assessment),
                 new SqlParameter("@sessiondata", sessiondata)
        };

        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertChildAssessment]", cmdParameters);

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

    protected void rdb_Click(object sender, EventArgs e)
    {
        try
        {
            string s = (sender as RadioButton).GroupName;
            EnableDisableControl(s);
            ModalAddclass1.Show();
        }
        catch
        {
        }
    }

    protected void EnableDisableControl(string s)
    {
        try
        {

            if (s == "trQ2")
            {
                trQ2.Visible = true;
            }
            else if (s == "trQ3")
            {
                trQ3.Visible = true;
            }
            else if (s == "trQ4")
            {
                trQ4.Visible = true;
            }
            else if (s == "trQ5")
            {
                trQ5.Visible = true;
            }
            else if (s == "trQ5")
            {
                trQ5.Visible = true;
            }
            else if (s == "trQ6")
            {
                trQ6.Visible = true;
            }
            else if (s == "trQ7")
            {
                trQ7.Visible = true;
            }
            else if (s == "trQ8")
            {
                trQ8.Visible = true;
            }
            else if (s == "trQ9")
            {
                trQ9.Visible = true;
            }
            else if (s == "trQ10")
            {
                trQ10.Visible = true;
            }
            else if (s == "trQ11")
            {
                trQ11.Visible = true;
            }
            else if (s == "trQ12")
            {
                trQ12.Visible = true;
            }
            else if (s == "trQ12")
            {
                trQ12.Visible = true;
            }
            else if (s == "trQ13")
            {
                trQ13.Visible = true;
            }
            else if (s == "trQ14")
            {
                trQ14.Visible = true;
            }
            else if (s == "trQ15")
            {
                trQ15.Visible = true;
            }
            else if (s == "trQ16")
            {
                trQ16.Visible = true;
            }
            else if (s == "trQ17")
            {
                trQ17.Visible = true;
            }
            else if (s == "trQ18")
            {
                trQ18.Visible = true;
            }
            else if (s == "trQ19")
            {
                trQ19.Visible = true;
            }
            else if (s == "trQ20")
            {
                trQ20.Visible = true;
            }
            else if (s == "trQ21")
            {
                trQ21.Visible = true;
            }
            else if (s == "trQ22")
            {
                trQ22.Visible = true;
            }
            else if (s == "trQ23")
            {
                trQ23.Visible = true;
            }
            else if (s == "trQ24")
            {
                trQ24.Visible = true;
            }
            else if (s == "trQ25")
            {
                trQ25.Visible = true;
            }
            else if (s == "trQ26")
            {
                trQ26.Visible = true;
            }
            else if (s == "trQ27")
            {
                trQ27.Visible = true;
            }
            else if (s == "trQ28")
            {
                trQ28.Visible = true;
            }
            else if (s == "trQ29")
            {
                trQ29.Visible = true;
            }
            else if (s == "trQ30")
            {
                trQ30.Visible = true;
            }
            else if (s == "trQ31")
            {
                trQ31.Visible = true;
            }
            else if (s == "trQ32")
            {
                trQ32.Visible = true;
            }
            else if (s == "trQ33")
            {
                trQ33.Visible = true;
            }
            else if (s == "trQ34")
            {
                trQ34.Visible = true;
            }
            else if (s == "trQ35")
            {
                trQ35.Visible = true;
            }
            else if (s == "trQ36")
            {
                trQ36.Visible = true;
            }
            else if (s == "trQ37")
            {
                trQ37.Visible = true;
            }
            else if (s == "trQ38")
            {
                trQ38.Visible = true;
            }
            else if (s == "trQ39")
            {
                trQ39.Visible = true;
            }
            else if (s == "trQ40")
            {
                trQ40.Visible = true;
            }
            else if (s == "trQ41")
            {
                trQ41.Visible = true;
            }
            else if (s == "trQ42")
            {
                trQ42.Visible = true;
            }
            else if (s == "trQ43")
            {
                trQ43.Visible = true;
            }
            else if (s == "trQ44")
            {
                trQ44.Visible = true;
            }
            else if (s == "trQ45")
            {
                trQ45.Visible = true;
            }
            else if (s == "trQ46")
            {
                trQ46.Visible = true;
            }
            else if (s == "trQ47")
            {
                trQ47.Visible = true;
            }
            else if (s == "trQ48")
            {
                trQ48.Visible = true;
            }
            else if (s == "trQ49")
            {
                trQ49.Visible = true;
            }
            else if (s == "trQ50")
            {
                trQ50.Visible = true;
            }
            else if (s == "trQ51")
            {
                trQ51.Visible = true;
            }
            else if (s == "trQ52")
            {
                trQ52.Visible = true;
            }
            else if (s == "trQ53")
            {
                trQ53.Visible = true;
            }
            else if (s == "trQ54")
            {
                trQ54.Visible = true;
            }
            else if (s == "trQ55")
            {
                trQ55.Visible = true;
            }
            else if (s == "trQ56")
            {
                trQ56.Visible = true;
            }
        }
        catch
        {
        }
    }

    protected void LnkBtnView_ffOnClick(object sender, EventArgs e)
    {
        try
        {
            LinkButton bt = (LinkButton)sender;
            GridViewRow gvr = (GridViewRow)bt.NamingContainer;
            int rowIndex = gvr.RowIndex;
            string[] arr;
            //DropDownList Attendance = (DropDownList)GvReg.Rows[rowIndex].FindControl("ddlAttendance");
            string Assessment;
            if ((ddlsession.SelectedItem.Text == "Session-1") || (ddlsession.SelectedItem.Text == "Session-10"))
            {

                Label lblUniqueCode = (Label)GvReg.Rows[rowIndex].FindControl("lblUniqueChildRCode");
                DropDownList Attendance = (DropDownList)GvReg.Rows[rowIndex].FindControl("ddlAttendance");

                if (Attendance.SelectedValue == "1")
                {
                    //Label lblUniqueCode = (Label)row.FindControl("lblUniqueChildCode");
                    string UniqueCode = lblUniqueCode.Text;
                    Session["UniqueCode"] = UniqueCode;
                    string sessiondata = Convert.ToString(ddlsession.SelectedValue);
                    DataTable dt = ALLQueryinPage("", "", "", sessiondata, UniqueCode, "12");
                    if (dt.Rows.Count > 0)
                    {
                        Assessment = dt.Rows[0]["Assessment"].ToString();
                        if (Assessment != "")
                        {
                            #region Disabled Radio bUtton
                            trQ2.Visible = true;
                            trQ3.Visible = true;
                            trQ4.Visible = true;
                            trQ5.Visible = true;
                            trQ6.Visible = true;
                            trQ7.Visible = true;
                            trQ8.Visible = true;
                            trQ9.Visible = true;
                            trQ10.Visible = true;
                            trQ11.Visible = true;
                            trQ12.Visible = true;
                            trQ13.Visible = true;
                            trQ14.Visible = true;
                            trQ15.Visible = true;
                            trQ16.Visible = true;
                            trQ17.Visible = true;
                            trQ18.Visible = true;
                            trQ19.Visible = true;
                            trQ20.Visible = true;
                            trQ21.Visible = true;
                            trQ22.Visible = true;
                            trQ23.Visible = true;
                            trQ24.Visible = true;
                            trQ25.Visible = true;
                            trQ26.Visible = true;
                            trQ27.Visible = true;
                            trQ28.Visible = true;
                            trQ29.Visible = true;
                            trQ30.Visible = true;

                            trQ31.Visible = true;
                            trQ32.Visible = true;
                            trQ33.Visible = true;
                            trQ34.Visible = true;
                            trQ35.Visible = true;
                            trQ36.Visible = true;
                            trQ37.Visible = true;
                            trQ38.Visible = true;
                            trQ39.Visible = true;
                            trQ40.Visible = true;

                            trQ41.Visible = true;
                            trQ42.Visible = true;
                            trQ43.Visible = true;
                            trQ44.Visible = true;
                            trQ45.Visible = true;
                            trQ46.Visible = true;
                            trQ47.Visible = true;
                            trQ48.Visible = true;
                            trQ49.Visible = true;
                            trQ50.Visible = true;
                            trQ51.Visible = true;
                            trQ52.Visible = true;
                            trQ53.Visible = true;
                            trQ54.Visible = true;
                            trQ55.Visible = true;
                            trQ56.Visible = true;
                            #endregion

                            #region Unchecked Radio bUtton

                            for (int i = 0; i < 56; i++)
                            {
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 5) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 4) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 3) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 2) as RadioButton).Checked = false;
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + 1) as RadioButton).Checked = false;
                            }

                            #endregion

                            arr = Assessment.ToString().Split(',');
                            for (int i = 0; i < 56; i++)
                            {
                                (pnl_Addclass1.FindControl("rdbQ" + (i + 1) + '_' + arr[i]) as RadioButton).Checked = true;
                            }

                            btnSaveAttendance.Visible = false;
                            ModalAddclass1.Show();
                        }
                        else
                        {
                        }

                    }

                }
            }

        }
        catch
        {
        }
    }

}