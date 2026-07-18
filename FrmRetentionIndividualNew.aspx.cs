using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FrmRetentionIndividualNew : System.Web.UI.Page
{

    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    string conditions = string.Empty, Flag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                LoadYear();
                LoadUserLeavel();
                GV_Retention.DataSource = null;
                GV_Retention.DataBind();
                UserLevelFilter();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (ddlFCTakeDataAttendance.SelectedValue == "1" || ddlFCTakeDataAttendance.SelectedValue == "2" || ddlFCTakeDataAttendance.SelectedValue == "3")
            FillGrid();
        {
            DeleteH(ddlSchool.SelectedValue, Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue));
        }
    }
    public int DeleteH(string SchoolCode, int NameofChildAvailable)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@Flag", NameofChildAvailable),

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteRetionData", cmdParameters);
    }
    protected void FCTakeDataAttendance_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlFCTakeDataAttendance.SelectedValue == "1" || ddlFCTakeDataAttendance.SelectedValue == "3")
        {
            Lblreason.Visible = false;
            ddlreson.Visible = false;
            lblreason2.Visible = false;
            ddlTeacherallow.Visible = false;
            GV_Retention.Visible = true;
            ddlreson.SelectedValue = "0";
            FillGrid();
        }
        else if (ddlFCTakeDataAttendance.SelectedValue == "2")
        {
            Lblreason.Visible = true;
            ddlreson.Visible = true;
            lblreason2.Visible = false;
            ddlTeacherallow.Visible = false;
            GV_Retention.Visible = false;

        }
    }

    protected void ddlreson_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlreson.SelectedValue != "0")
        {
            Lblreason.Visible = true;
            ddlreson.Visible = true;
            lblreason2.Visible = false;
            ddlTeacherallow.Visible = false;
            ddlTeacherallow.SelectedIndex = -1;
        }
        else
        {
            lblreason2.Visible = false;
            ddlTeacherallow.Visible = false;
            GV_Retention.Visible = true;
        }
    }

    protected void ddlTeacherallow_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlTeacherallow.SelectedValue == "1")
        {
            GV_Retention.Visible = true;
            FillGrid();
        }
        else if (ddlTeacherallow.SelectedValue == "2")
        {
            GV_Retention.Visible = false;
        }
    }

    public void UserLevelFilter()
    {

        string strQry = "";
        string Cond = "Module='Retention Individual'";
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


        string strQry11 = "select LookupCode,Description from mstLookup where LookupFlag='EFA'  ";
        DataTable dt8373 = objMain.LoadData(strQry11);
        Session["dt8373"] = dt8373;

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
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

        }
        if (vVerify == true)
        {



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
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        //if (ddlFCTakeDataAttendance.SelectedValue == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select attendance register')</script>", false);
        //    return;
        //}
        FillGrid();
        GV_Retention.Visible = true;
    }
    private Boolean Validation()
    {
        try
        {
            DataTable Dt = Session["GridViewData"] as DataTable;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                if (Dt.Rows[i]["TempID"].ToString() == "1" || Dt.Rows[i]["TempID"].ToString() == "2" || Dt.Rows[i]["TempID"].ToString() == "0")
                {
                    string ReasonforAbsent = Dt.Rows[i]["ReasonforAbsent"].ToString();
                    string ReasonOther = Dt.Rows[i]["ReasonOther"].ToString();
                    if (ReasonforAbsent == "99")
                    {
                        if (ReasonOther == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter other Reason ')</script>", false);
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {

            throw;
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
    protected void btnsave_Click(object sender, EventArgs e)
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

        int Retention_ID = 0, ReasonnotTakingData = 0, FCTakeDataAttendance = 0, ID = 0;
        string Uniquecode = "", villagecode = "", schoolcode = "", flag = "", Teacherallow = "", UniquecodeNew = "", CreatedBy = "";
        DateTime AttendanceLastdate = System.DateTime.MinValue;

        if (Convert.ToInt32(ddlMarge.SelectedIndex) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School Status?')</script>", false);
            return;

        }
        if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
        {
            if (Convert.ToInt32(ddlSchoolMarger.SelectedIndex) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Marge School?')</script>", false);
                return;

            }
        }
        if ((ddlFCTakeDataAttendance.SelectedValue) != "")
        {
            #region  mainfilter
            if (ddlFCTakeDataAttendance.SelectedValue == "1" || ddlFCTakeDataAttendance.SelectedValue == "3")
            {
                GV_Retention.Visible = true;
            }
            else if (ddlFCTakeDataAttendance.SelectedValue == "2")
            {
                //Lblreason.Visible = true;
                //ddlreson.Visible = true;

                //lblreason2.Visible = false;
                //ddlTeacherallow.Visible = false;
            }
            else if (ddlFCTakeDataAttendance.SelectedValue == "")
            {
                GV_Retention.Visible = false;

                //Lblreason.Visible = false;
                //ddlreson.Visible = false;
                //lblreason2.Visible = false;
                //ddlTeacherallow.Visible = false;
            }
            else if (ddlreson.SelectedValue != "0")
            {
                //lblreason2.Visible = true;
                //ddlTeacherallow.Visible = true;
            }
            if (ddlVillage.SelectedValue != "0" && ddlVillage.SelectedValue != null)
            {
                villagecode = ddlVillage.SelectedValue;
            }
            if (ddlSchool.SelectedValue != "0" && ddlSchool.SelectedValue != null)
            {
                schoolcode = ddlSchool.SelectedValue;
            }
            if (ddlreson.SelectedValue != "0")
            {
                ReasonnotTakingData = Convert.ToInt32(ddlreson.SelectedValue);
            }
            if (ddlTeacherallow.SelectedValue != "0")
            {
                Teacherallow = Convert.ToString(ddlTeacherallow.SelectedValue);
            }

            #endregion
            int Result = 0; bool temp = false;
            #region grid vaildation
            for (int i = 0; i < GV_Retention.Rows.Count; i++)
            {

                DropDownList ddlNameofChildAvailable = (DropDownList)GV_Retention.Rows[i].FindControl("ddlNameofChildAvailable");
                DropDownList ddlSupportforChildRegularty = (DropDownList)GV_Retention.Rows[i].FindControl("ddlSupportforChildRegularty");
                DropDownList ddlReasonforchildnotinReg = (DropDownList)GV_Retention.Rows[i].FindControl("ddlReasonforchildnotinReg");
                DropDownList ddlPresentClass = (DropDownList)GV_Retention.Rows[i].FindControl("ddlPresentClass");
                DropDownList ddlIsChildAvailableClassToday = (DropDownList)GV_Retention.Rows[i].FindControl("ddlIsChildAvailableClassToday");
                DropDownList ddlChildPrestent_Last2Week = (DropDownList)GV_Retention.Rows[i].FindControl("ddlChildPrestent_Last2Week");
                int NameofChildAvailable = Convert.ToInt32(ddlNameofChildAvailable.SelectedValue);
                int SupportforChildRegularty = Convert.ToInt32(ddlSupportforChildRegularty.SelectedValue);
                int ReasonforchildnotinReg = Convert.ToInt32(ddlReasonforchildnotinReg.SelectedValue);
                int PresentClass = Convert.ToInt32(ddlPresentClass.SelectedValue);
                int IsChildAvailableClassToday = Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue);
                int ChildPrestent_Last2Week = Convert.ToInt32(ddlChildPrestent_Last2Week.SelectedValue);
                TextBox txtAttendanceLastdate = (TextBox)GV_Retention.Rows[i].FindControl("txtAttendanceLastdate");
                TextBox txtSr = (TextBox)GV_Retention.Rows[i].FindControl("txtSr");
                DropDownList ddlsr = (DropDownList)GV_Retention.Rows[i].FindControl("ddlsr");
                DropDownList ddlGradeResone = (DropDownList)GV_Retention.Rows[i].FindControl("ddlGradeResone");

                if (NameofChildAvailable > 0 && (ddlFCTakeDataAttendance.SelectedValue == "1" || ddlFCTakeDataAttendance.SelectedValue == "3"))
                {
                    if (ddlGradeResone.Enabled == true)
                    {
                        if (ddlGradeResone.SelectedIndex <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select What is the reason for the child not progressing to the next grade?')</script>", false);
                            return;
                        }
                    }

                    if (NameofChildAvailable <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Is Name of Child Available in Attendance Register 2025-2026?')</script>", false);
                        return;
                    }
                    if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
                    {
                        if (Convert.ToInt32(ddlsr.SelectedValue) <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Is the child  SR number correct?')</script>", false);
                            return;
                        }
                        if (Convert.ToInt32(ddlsr.SelectedValue) == 2 && txtSr.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter SR number ')</script>", false);
                            return;
                        }
                    }

                    if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
                    {
                        if (Convert.ToInt32(ddlsr.SelectedValue) <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Is the child  SR number correct?')</script>", false);
                            return;
                        }
                        if (Convert.ToInt32(ddlsr.SelectedValue) == 2 && txtSr.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter SR number ')</script>", false);
                            return;
                        }
                    }
                    if (NameofChildAvailable == 1 || NameofChildAvailable == 3)
                    {
                        if (SupportforChildRegularty <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Does the child need any support for regularization?')</script>", false);
                            return;

                        }

                        if (PresentClass <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Please enter the current class of the child')</script>", false);
                            return;

                        }
                        if (SupportforChildRegularty == 2)
                        {
                            if (txtAttendanceLastdate.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Last Attendance Date')</script>", false);
                                return;
                            }
                        }
                    }
                    if (NameofChildAvailable == 2)
                    {
                        if (ReasonforchildnotinReg <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select the reason for not getting the child')</script>", false);
                            return;

                        }
                        if (ReasonforchildnotinReg == 339)
                        {
                            if (IsChildAvailableClassToday <= 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select the Dropout reason for not getting the child')</script>", false);
                                return;
                            }
                        }
                    }



                }
                else if (ddlFCTakeDataAttendance.SelectedValue == "2")
                {
                    if (IsChildAvailableClassToday == 1)
                    {
                        if (PresentClass <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Please enter the current class of the child')</script>", false);
                            return;

                        }
                    }

                    if (IsChildAvailableClassToday == 2)
                    {
                        if (ChildPrestent_Last2Week <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Has Child Come to School in the Last 2 Weeks?')</script>", false);
                            return;

                        }
                        else if (ChildPrestent_Last2Week == 1)
                        {
                            if (PresentClass <= 0)
                            {

                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert(' Please enter the current class of the child')</script>", false);
                                return;

                            }

                        }
                    }


                }

                if (NameofChildAvailable > 0 || IsChildAvailableClassToday > 0)
                {
                    if (temp == false)
                    {
                        if (Uniquecode == "")
                        {
                            temp = true;

                        }
                    }
                }
            }
            #endregion

            if (temp == true)
            {

                flag = "I";
                UniquecodeNew = objMain.Generate_RandomString(8);
                FCTakeDataAttendance = Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue);
                if (Convert.ToString(Session["username"]) != "" && Convert.ToString(Session["username"]) != null)
                {
                    CreatedBy = Convert.ToString(Session["username"]);
                }
                ID = InsertUpdateRetentionIndividualMain(UniquecodeNew, villagecode, schoolcode, FCTakeDataAttendance, ReasonnotTakingData, Teacherallow, flag, Retention_ID, CreatedBy, ddlSchoolMarger.SelectedValue, ddlMarge.SelectedValue);
            }



            for (int i = 0; i < GV_Retention.Rows.Count; i++)
            {
                DropDownList ddlNameofChildAvailable = (DropDownList)GV_Retention.Rows[i].FindControl("ddlNameofChildAvailable");
                DropDownList ddlSupportforChildRegularty = (DropDownList)GV_Retention.Rows[i].FindControl("ddlSupportforChildRegularty");
                DropDownList ddlReasonforchildnotinReg = (DropDownList)GV_Retention.Rows[i].FindControl("ddlReasonforchildnotinReg");
                DropDownList ddlPresentClass = (DropDownList)GV_Retention.Rows[i].FindControl("ddlPresentClass");
                DropDownList ddlIsChildAvailableClassToday = (DropDownList)GV_Retention.Rows[i].FindControl("ddlIsChildAvailableClassToday");
                DropDownList ddlChildPrestent_Last2Week = (DropDownList)GV_Retention.Rows[i].FindControl("ddlChildPrestent_Last2Week");
                TextBox txtAttendanceLastdate = (TextBox)GV_Retention.Rows[i].FindControl("txtAttendanceLastdate");
                Label LBLTempId = (Label)GV_Retention.Rows[i].FindControl("LBLTempId");
                int NameofChildAvailable = Convert.ToInt32(ddlNameofChildAvailable.SelectedValue);
                int SupportforChildRegularty = Convert.ToInt32(ddlSupportforChildRegularty.SelectedValue);
                int ReasonforchildnotinReg = Convert.ToInt32(ddlReasonforchildnotinReg.SelectedValue);
                int PresentClass = Convert.ToInt32(ddlPresentClass.SelectedValue);
                int IsChildAvailableClassToday = Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue);
                int ChildPrestent_Last2Week = Convert.ToInt32(ddlChildPrestent_Last2Week.SelectedValue);

                TextBox txtSr = (TextBox)GV_Retention.Rows[i].FindControl("txtSr");
                DropDownList ddlsr = (DropDownList)GV_Retention.Rows[i].FindControl("ddlsr");
                DropDownList ddlGradeResone = (DropDownList)GV_Retention.Rows[i].FindControl("ddlGradeResone");
                string srno = "";
                int sr = 0;

                if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
                {

                    if (Convert.ToInt32(ddlsr.SelectedValue) == 2 && txtSr.Text != "")
                    {
                        srno = txtSr.Text;

                        sr = Convert.ToInt32(ddlsr.SelectedValue);
                    }
                    sr = Convert.ToInt32(ddlsr.SelectedValue);
                }
                if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
                {

                    if (Convert.ToInt32(ddlsr.SelectedValue) == 2 && txtSr.Text != "")
                    {
                        srno = txtSr.Text;


                    }
                    sr = Convert.ToInt32(ddlsr.SelectedValue);
                }
                DateTime recorddate = System.DateTime.MinValue;

                if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 1)
                {
                    if (Convert.ToInt32(ddlSupportforChildRegularty.SelectedValue) == 2)
                    {
                        DateTime AttendanceLastdate1 = Convert.ToDateTime(txtAttendanceLastdate.Text);
                        AttendanceLastdate = AttendanceLastdate1;
                    }
                }
                else if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 2)
                {
                    if (Convert.ToInt32(IsChildAvailableClassToday) == 1)
                    {
                        AttendanceLastdate = DateTime.MinValue;
                    }
                }
                if (Convert.ToString(LBLTempId.Text) != "")
                {
                    Uniquecode = LBLTempId.Text;
                }
                else
                {
                    Uniquecode = UniquecodeNew;
                }
                if (Convert.ToString(Session["username"]) != "" && Convert.ToString(Session["username"]) != null)
                {
                    CreatedBy = Convert.ToString(Session["username"]);
                }

                if (NameofChildAvailable > 0 || IsChildAvailableClassToday > 0)
                {
                    string C_ID = GV_Retention.DataKeys[i]["UniqueChildCode"].ToString();
                    Result = UpdateRetentionIndividualNew(C_ID, NameofChildAvailable, SupportforChildRegularty, ReasonforchildnotinReg, PresentClass, IsChildAvailableClassToday, ChildPrestent_Last2Week, Uniquecode, CreatedBy, AttendanceLastdate, sr, srno, ddlGradeResone.SelectedValue);
                }
                else
                {
                    string C_ID = GV_Retention.DataKeys[i]["UniqueChildCode"].ToString();
                    Result = UpdateRetentionIndividualNew(C_ID, 0, 0, 0, 0, 0, 0, "", CreatedBy, AttendanceLastdate, sr, srno, ddlGradeResone.SelectedValue);

                }
            }
            if (Result > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Successfull')</script>", false);

                FillGrid();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Unsuccessfull')</script>", false);
            }
        }

    }
    protected void LnkDownloadPDF_OnClick(object sender, EventArgs e)
    {
        try
        {

            conditions = "";
            if (ddlYear.SelectedIndex > 0)
            {
                conditions = " where V.FYear='" + ddlYear.SelectedItem.Text + "'";
            }
            if (ddlState.SelectedIndex > 0)
            {
                conditions = conditions + " and V.StateCode='" + ddlState.SelectedValue + "'";
            }
            if (ddlDistrict.SelectedIndex > 0)
            {
                conditions = conditions + " and V.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            }
            if (ddlBlock.SelectedIndex > 0)
            {
                conditions = conditions + " and V.BlockCode='" + ddlBlock.SelectedValue + "'";
            }

            if (ddlVillage.SelectedIndex > 0)
            {
                conditions = conditions + " and V.VillageCode='" + ddlVillage.SelectedValue + "'";
            }
            if (ddlSchool.SelectedIndex > 0)
            {
                conditions = conditions + " and Sch.SchoolCode='" + ddlSchool.SelectedValue + "'";
            }

            SqlParameter[] par = new SqlParameter[]
        {
  new SqlParameter("@Condition",  conditions),

        };

            DataTable DtPDF = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Retention_IndividualPdF", par);
            if (DtPDF.Rows.Count > 0)
            {
                //DtPDF.Columns.Remove("UniqueChildCode");
                //DtPDF.Columns.Remove("TempId");
                //DtPDF.Columns.Add("Annual Exam Status");                
                //  GeneratePDF(DtPDF, "");
            }
        }
        catch
        {

            throw;
        }
    }
    private int Update_AnnualExamStatus(string str, string UID, string p, string ReasonforAbsent, string ReasonOther)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = Update_AnnualExamStatusNew(str, UID, Flag, ReasonforAbsent, ReasonOther);
        }
        catch (Exception exp)
        {

        }
        return iReturnValue;
    }


    public int Update_AnnualExamStatusNew(string str, string UID, string Flag, string ReasonforAbsent, string ReasonOther)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Annual_Exam_Status]";
                dbSqlCommand.Parameters.AddWithValue("@str", str);
                dbSqlCommand.Parameters.AddWithValue("@UID", UID);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                dbSqlCommand.Parameters.AddWithValue("@ReasonforAbsent", ReasonforAbsent);
                dbSqlCommand.Parameters.AddWithValue("@ReasonOther", ReasonOther);
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

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            if (Session["user_level_Role"].ToString() == "1")
            {
            }
            else
            {
                ddlDistrict.SelectedIndex = 1;
            }
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);


            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();

            ddlVillage.Items.Clear();
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
        //}

        {
            string cond = "Flag=71 and Language = 0";
            objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", cond, "TopicDiscussName", "Desc", ddlreson, "TopicDiscussName", "TopicDIscussID", "--Select--");
            ddlreson.Enabled = true;
            ddlreson.Enabled = true;

            Lblreason.Visible = false;
            ddlreson.Visible = false;
            lblreason2.Visible = false;
            ddlTeacherallow.Visible = false;
            GV_Retention.Visible = false;

        }

        conditions = "";
        conditions = "LookupFlag ='CS' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description1", conditions, "LookupCode", "asc", ddlMarge, "Description1", "LookupCode", "Select");



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
        conditions = "";
        AlllStateCode();


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

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = true;
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

    protected void ddlAnnual4444_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;
        DropDownList ddlNameofChildAvailable = (DropDownList)row1.FindControl("ddlNameofChildAvailable");
        DropDownList ddlsr = (DropDownList)row1.FindControl("ddlsr");
        TextBox txtSr = (TextBox)row1.FindControl("txtSr");
        if (Convert.ToInt32(ddlsr.SelectedValue) == 2)
        {


            txtSr.Text = "";
            txtSr.Enabled = true;
        }
        else
        {
            txtSr.Text = "";
            txtSr.Enabled = false;
        }
    }
    protected void ddlAnnual444_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;


        DropDownList ddlNameofChildAvailable = (DropDownList)row1.FindControl("ddlNameofChildAvailable");
        DropDownList ddlSupportforChildRegularty = (DropDownList)row1.FindControl("ddlSupportforChildRegularty");
        DropDownList ddlReasonforchildnotinReg = (DropDownList)row1.FindControl("ddlReasonforchildnotinReg");
        DropDownList ddlPresentClass = (DropDownList)row1.FindControl("ddlPresentClass");
        DropDownList ddlIsChildAvailableClassToday = (DropDownList)row1.FindControl("ddlIsChildAvailableClassToday");
        DropDownList ddlChildPrestent_Last2Week = (DropDownList)row1.FindControl("ddlChildPrestent_Last2Week");
        DropDownList ddlsr = (DropDownList)row1.FindControl("ddlsr");
        TextBox txtAttendanceLastdate = (TextBox)row1.FindControl("txtAttendanceLastdate");
        TextBox txtSr = (TextBox)row1.FindControl("txtSr");
        ddlsr.SelectedIndex = 0;
        if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 1)
        {
            if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 1 || Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
            {

                ddlSupportforChildRegularty.Enabled = true;
                ddlPresentClass.Enabled = true;
                ddlReasonforchildnotinReg.Enabled = false;
                ddlSupportforChildRegularty.SelectedIndex = 0;
                ddlPresentClass.SelectedIndex = 0;
                ddlReasonforchildnotinReg.SelectedIndex = 0;
                txtAttendanceLastdate.Enabled = true;
                txtAttendanceLastdate.Text = "";

                ddlIsChildAvailableClassToday.Enabled = false;
                ddlIsChildAvailableClassToday.SelectedIndex = 0;
                ddlsr.Enabled = false;
                if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
                {
                    ddlsr.Enabled = true;
                }
                if (Convert.ToInt32(ddlMarge.SelectedValue) == 1 && Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
                {
                    ddlsr.Enabled = true;
                }


            }
            else if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 2)
            {
                ddlSupportforChildRegularty.Enabled = false;
                ddlPresentClass.Enabled = false;
                ddlReasonforchildnotinReg.Enabled = true;

                ddlSupportforChildRegularty.SelectedIndex = 0;
                ddlPresentClass.SelectedIndex = 0;
                ddlReasonforchildnotinReg.SelectedIndex = 0;
                txtAttendanceLastdate.Enabled = false;
                txtAttendanceLastdate.Text = "";
                ddlIsChildAvailableClassToday.Enabled = false;
                ddlIsChildAvailableClassToday.SelectedIndex = 0;
                ddlsr.Enabled = false;
            }
            else
            {
                txtAttendanceLastdate.Enabled = false;
                ddlSupportforChildRegularty.Enabled = true;
                ddlPresentClass.Enabled = true;
                ddlReasonforchildnotinReg.Enabled = true;
                ddlSupportforChildRegularty.SelectedIndex = 0;
                ddlPresentClass.SelectedIndex = 0;
                ddlReasonforchildnotinReg.SelectedIndex = 0;
                txtAttendanceLastdate.Text = "";
                ddlIsChildAvailableClassToday.Enabled = false;
                ddlIsChildAvailableClassToday.SelectedIndex = 0;
                ddlsr.Enabled = false;
            }
        }
        if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 2)
        {
            if (Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue) == 1)
            {
                ddlPresentClass.Enabled = true;
                ddlChildPrestent_Last2Week.Enabled = false;
                ddlPresentClass.SelectedIndex = 0;
                ddlChildPrestent_Last2Week.SelectedIndex = 0;
                txtAttendanceLastdate.Enabled = false;
                txtAttendanceLastdate.Text = "";
            }
            else if (Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue) == 1)
            {
                ddlPresentClass.Enabled = false;
                ddlChildPrestent_Last2Week.Enabled = true;
                ddlPresentClass.SelectedIndex = 0;
                ddlChildPrestent_Last2Week.SelectedIndex = 0;
                txtAttendanceLastdate.Enabled = false;
                txtAttendanceLastdate.Text = "";

            }
            else
            {
                ddlPresentClass.Enabled = false;
                ddlChildPrestent_Last2Week.Enabled = false;
                ddlPresentClass.SelectedIndex = 0;
                ddlChildPrestent_Last2Week.SelectedIndex = 0;
                txtAttendanceLastdate.Enabled = false;
                txtAttendanceLastdate.Text = "";

            }
        }

    }
    protected void onselected_SupportforChildRegularty(object sender, EventArgs e)
    {


        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;


        DropDownList ddlNameofChildAvailable = (DropDownList)row1.FindControl("ddlNameofChildAvailable");
        DropDownList ddlSupportforChildRegularty = (DropDownList)row1.FindControl("ddlSupportforChildRegularty");
        TextBox txtAttendanceLastdate = (TextBox)row1.FindControl("txtAttendanceLastdate");
        DropDownList ddlIsChildAvailableClassToday = (DropDownList)row1.FindControl("ddlIsChildAvailableClassToday");
        ddlIsChildAvailableClassToday.Enabled = false;
        ddlIsChildAvailableClassToday.SelectedIndex = 0;
        if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 1 || Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
        {
            if (Convert.ToInt32(ddlSupportforChildRegularty.SelectedValue) == 2)
            {

                txtAttendanceLastdate.Enabled = true;
                txtAttendanceLastdate.Text = "";

            }
            else
            {
                txtAttendanceLastdate.Enabled = false;
                txtAttendanceLastdate.Text = "";
            }
        }
        else
        {
            txtAttendanceLastdate.Enabled = false;
            txtAttendanceLastdate.Text = "";
        }

    }


    protected void ddlPresentClass_SupportforChildRegularty(object sender, EventArgs e)
    {


        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;


        DropDownList ddlPresentClass = (DropDownList)row1.FindControl("ddlPresentClass");

        Label lblClass = (Label)row1.FindControl("lblClass");
        DropDownList ddlGradeResone = (DropDownList)row1.FindControl("ddlGradeResone");
        ddlGradeResone.Enabled = false;
        ddlGradeResone.SelectedIndex = 0;
        if (ddlPresentClass.SelectedIndex > 0)
        {
            int Pr = Convert.ToInt32(lblClass.Text) + 1;
            int cur = Convert.ToInt32(ddlPresentClass.SelectedItem.Text);
            if (Pr == cur)
            {
                ddlGradeResone.Enabled = false;
            }
            else
            {
                ddlGradeResone.Enabled = true;
            }
        }


    }


    protected void ddlReasonforchildnotinReg_SupportforChildRegularty(object sender, EventArgs e)
    {


        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;


        DropDownList ddlReasonforchildnotinReg = (DropDownList)row1.FindControl("ddlReasonforchildnotinReg");
        DropDownList ddlSupportforChildRegularty = (DropDownList)row1.FindControl("ddlSupportforChildRegularty");
        TextBox txtAttendanceLastdate = (TextBox)row1.FindControl("txtAttendanceLastdate");
        DropDownList ddlIsChildAvailableClassToday = (DropDownList)row1.FindControl("ddlIsChildAvailableClassToday");

        if (Convert.ToInt32(ddlReasonforchildnotinReg.SelectedValue) == 339)
        {

            ddlIsChildAvailableClassToday.Enabled = true;
            ddlIsChildAvailableClassToday.SelectedIndex = 0;

        }
        else
        {
            ddlIsChildAvailableClassToday.Enabled = false;
            ddlIsChildAvailableClassToday.SelectedIndex = 0;
        }


    }

    public void FillGrid()
    {
        try
        {
            string cond = "";
            conditions = "";
            //if (ddlYear.SelectedIndex > 0)
            //{
            //    conditions = " where V.FYear='" + ddlYear.SelectedItem.Text + "'";
            //}
            //if (ddlState.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.StateCode='" + ddlState.SelectedValue + "'";
            //}
            //if (ddlDistrict.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            //}
            //if (ddlBlock.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.BlockCode='" + ddlBlock.SelectedValue + "'";
            //}

            //if (ddlVillage.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.VillageCode='" + ddlVillage.SelectedValue + "'";
            //}
            if (ddlVillage.SelectedIndex > 0)
            {
                conditions = conditions + " and tblEnrolment_Retention2026.TempVcode='" + ddlVillage.SelectedValue + "'";
            }
            if (ddlSchool.SelectedIndex > 0)
            {
                conditions = conditions + " and tblEnrolment_Retention2026.TempScode='" + ddlSchool.SelectedValue + "'";
                cond = " where SchoolCode='" + ddlSchool.SelectedValue + "'";
            }
            //if (ddlSchool.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and Sch.SchoolCode='" + ddlSchool.SelectedValue + "'";
            //    cond = " where  SchoolCode='" + ddlSchool.SelectedValue + "'";
            //}
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School ')</script>", false);
                return;
            }

            if (ddlFCTakeDataAttendance.SelectedValue == "1" || ddlFCTakeDataAttendance.SelectedValue == "3")
            {
                conditions = conditions + " and isnull(NameofChildAvailable,0)  >= 0 ";

            }
            else
            {
                conditions = conditions + " and isnull(IsChildAvailableClassToday,0)  >= 0 and isnull( NameofChildAvailable ,0)= 0";
            }



            SqlParameter[] par = new SqlParameter[]
        {
  new SqlParameter("@Condition",  conditions),
  new SqlParameter("@con",cond),

        };

            DataSet Ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_GET_Retention_IndividualNew2023", par);
            if (Ds != null)
            {
                DataTable DT = Ds.Tables[0];
                DataTable dt1 = Ds.Tables[1];

                Session["GridViewData"] = DT;
                if (DT.Rows.Count > 0)
                {
                    GV_Retention.DataSource = DT;
                    GV_Retention.DataBind();


                    for (int i = 0; i < GV_Retention.Rows.Count; i++)
                    {

                        DropDownList ddlNameofChildAvailable = (DropDownList)GV_Retention.Rows[i].FindControl("ddlNameofChildAvailable");
                        DropDownList ddlSupportforChildRegularty = (DropDownList)GV_Retention.Rows[i].FindControl("ddlSupportforChildRegularty");
                        DropDownList ddlReasonforchildnotinReg = (DropDownList)GV_Retention.Rows[i].FindControl("ddlReasonforchildnotinReg");
                        DropDownList ddlPresentClass = (DropDownList)GV_Retention.Rows[i].FindControl("ddlPresentClass");
                        DropDownList ddlIsChildAvailableClassToday = (DropDownList)GV_Retention.Rows[i].FindControl("ddlIsChildAvailableClassToday");
                        DropDownList ddlChildPrestent_Last2Week = (DropDownList)GV_Retention.Rows[i].FindControl("ddlChildPrestent_Last2Week");
                        TextBox txtAttendanceLastdate = (TextBox)GV_Retention.Rows[i].FindControl("txtAttendanceLastdate");
                        ddlNameofChildAvailable.SelectedValue = GV_Retention.DataKeys[i]["NameofChildAvailable"].ToString();
                        ddlSupportforChildRegularty.Text = GV_Retention.DataKeys[i]["SupportforChildRegularty"].ToString();
                        ddlReasonforchildnotinReg.SelectedValue = GV_Retention.DataKeys[i]["ReasonforchildnotinReg"].ToString();
                        ddlPresentClass.SelectedValue = GV_Retention.DataKeys[i]["PresentClass"].ToString();
                        ddlIsChildAvailableClassToday.SelectedValue = GV_Retention.DataKeys[i]["IsChildAvailableClassToday"].ToString();
                        ddlChildPrestent_Last2Week.SelectedValue = GV_Retention.DataKeys[i]["ChildPrestent_Last2Week"].ToString();
                        DropDownList ddlGradeResone = (DropDownList)GV_Retention.Rows[i].FindControl("ddlGradeResone");
                        ddlIsChildAvailableClassToday.Enabled = false;
                        txtAttendanceLastdate.Enabled = false;

                        TextBox txtSr1 = (TextBox)GV_Retention.Rows[i].FindControl("txtSr");

                        DropDownList ddlsr = (DropDownList)GV_Retention.Rows[i].FindControl("ddlsr");
                        ddlsr.SelectedValue = GV_Retention.DataKeys[i]["IsSRRight"].ToString();
                        txtSr1.Text = GV_Retention.DataKeys[i]["NewSR"].ToString();
                        ddlGradeResone.SelectedValue = GV_Retention.DataKeys[i]["GradeResone"].ToString();

                        if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 1)
                        {
                            if (Convert.ToInt32(ddlGradeResone.SelectedValue) > 0)
                            {
                                ddlGradeResone.Enabled = true;
                            }
                            if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 1 || Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
                            {

                                if (Convert.ToInt32(ddlsr.SelectedValue) == 0)
                                {
                                    ddlsr.Enabled = false;
                                    ddlsr.Enabled = false;
                                }
                                else
                                {
                                    ddlsr.Enabled = true;
                                    ddlsr.Enabled = true;
                                }


                                ddlSupportforChildRegularty.Enabled = true;
                                ddlPresentClass.Enabled = true;
                                ddlReasonforchildnotinReg.Enabled = false;

                                if (Convert.ToInt32(ddlSupportforChildRegularty.SelectedValue) == 2)
                                {
                                    txtAttendanceLastdate.Enabled = true;
                                }
                                else
                                {
                                    txtAttendanceLastdate.Enabled = false;
                                    txtAttendanceLastdate.Text = "";
                                }
                            }
                            else if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 2)
                            {
                                txtAttendanceLastdate.Enabled = false;
                                txtAttendanceLastdate.Text = "";
                                ddlSupportforChildRegularty.Enabled = false;
                                ddlPresentClass.Enabled = false;
                                ddlReasonforchildnotinReg.Enabled = true;

                                ddlIsChildAvailableClassToday.Enabled = true;

                            }
                            else
                            {
                                txtAttendanceLastdate.Text = "";
                                ddlSupportforChildRegularty.Enabled = true;
                                ddlPresentClass.Enabled = true;
                                ddlReasonforchildnotinReg.Enabled = true;
                            }
                        }
                        if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 2)
                        {
                            txtAttendanceLastdate.Enabled = false;
                            txtAttendanceLastdate.Text = "";
                            if (Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue) == 1)
                            {
                                ddlPresentClass.Enabled = true;
                                ddlChildPrestent_Last2Week.Enabled = false;
                            }
                            else if (Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue) == 2)
                            {
                                if (Convert.ToInt32(ddlChildPrestent_Last2Week.SelectedValue) == 1)
                                {

                                    ddlPresentClass.Enabled = true;
                                    ddlChildPrestent_Last2Week.Enabled = true;
                                }
                                if (Convert.ToInt32(ddlChildPrestent_Last2Week.SelectedValue) == 2)
                                {

                                    ddlPresentClass.Enabled = false;
                                    ddlChildPrestent_Last2Week.Enabled = true;
                                }
                            }
                            else
                            {
                                ddlPresentClass.Enabled = true;
                                ddlChildPrestent_Last2Week.Enabled = true;
                            }
                        }
                        if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 1 || Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 3)
                        {
                            if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 1 || Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 3)
                            {
                                ddlSupportforChildRegularty.Enabled = true;
                                ddlPresentClass.Enabled = true;
                                ddlReasonforchildnotinReg.Enabled = false;
                                if (Convert.ToInt32(ddlSupportforChildRegularty.SelectedValue) == 2)
                                {
                                    txtAttendanceLastdate.Enabled = true;
                                    DateTime tDate = Convert.ToDateTime(GV_Retention.DataKeys[i]["LastPresentDate"].ToString());

                                    txtAttendanceLastdate.Text = tDate.ToString("dd/MM/yyy");
                                }
                                else
                                {
                                    txtAttendanceLastdate.Enabled = false;
                                    txtAttendanceLastdate.Text = "";
                                }
                            }
                            else if (Convert.ToInt32(ddlNameofChildAvailable.SelectedValue) == 2)
                            {
                                txtAttendanceLastdate.Enabled = false;
                                txtAttendanceLastdate.Text = "";
                                ddlSupportforChildRegularty.Enabled = false;
                                ddlPresentClass.Enabled = false;
                                ddlReasonforchildnotinReg.Enabled = true;
                            }
                            else
                            {
                                ddlSupportforChildRegularty.Enabled = true;
                                ddlPresentClass.Enabled = true;
                                ddlReasonforchildnotinReg.Enabled = true;
                            }
                        }
                        if (Convert.ToInt32(ddlFCTakeDataAttendance.SelectedValue) == 2)
                        {
                            if (Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue) == 1)
                            {
                                ddlPresentClass.Enabled = true;
                                ddlChildPrestent_Last2Week.Enabled = false;
                            }
                            else if (Convert.ToInt32(ddlIsChildAvailableClassToday.SelectedValue) == 2)
                            {
                                if (Convert.ToInt32(ddlChildPrestent_Last2Week.SelectedValue) == 1)
                                {

                                    ddlPresentClass.Enabled = true;
                                    ddlChildPrestent_Last2Week.Enabled = true;
                                }
                                if (Convert.ToInt32(ddlChildPrestent_Last2Week.SelectedValue) == 2)
                                {

                                    ddlPresentClass.Enabled = false;
                                    ddlChildPrestent_Last2Week.Enabled = true;
                                }
                            }
                            else
                            {
                                ddlPresentClass.Enabled = true;
                                ddlChildPrestent_Last2Week.Enabled = true;
                            }
                        }

                        //ddlSupportforChildRegularty.Enabled = false;
                        //ddlReasonforchildnotinReg.Enabled = false;
                        //ddlPresentClass.Enabled = false;
                        //ddlChildPrestent_Last2Week.Enabled = false;
                    }
                }
                else
                {
                    lblTotalChildren.Text = "";
                    lblEntryComplete.Text = "";
                    GV_Retention.DataSource = null;
                    GV_Retention.DataBind();

                }
                //if(Convert.ToInt32(ddlMarge.SelectedValue)==2)
                //{
                //    GV_Retention.Columns[8].Visible = true;
                //    GV_Retention.Columns[9].Visible = true;
                //}
                //else
                //{
                //    GV_Retention.Columns[8].Visible = false;
                //    GV_Retention.Columns[9].Visible = false;
                //}

                if (dt1.Rows.Count > 0)
                {
                    if (dt1.Rows.Count == 1)
                    {
                        if (dt1.Rows[0]["ImageName"].ToString() != "")
                        {
                            ImgShow.Visible = true;
                            hdnMKID.Value = dt1.Rows[0]["ImageName"].ToString();
                        }
                        else { ImgShow.Visible = false; }
                    }
                    if (dt1.Rows.Count == 2)
                    {
                        if (dt1.Rows[0]["ImageName"].ToString() != "")
                        {
                            ImgShow.Visible = true;
                            hdnMKID.Value = dt1.Rows[0]["ImageName"].ToString();
                            hdnMKID2.Value = dt1.Rows[1]["ImageName"].ToString();
                        }
                        else { ImgShow.Visible = false; }
                    }
                    if (dt1.Rows.Count == 3)
                    {

                        if (dt1.Rows[0]["ImageName"].ToString() != "")
                        {
                            ImgShow.Visible = true;
                            hdnMKID.Value = dt1.Rows[0]["ImageName"].ToString();
                            hdnMKID2.Value = dt1.Rows[1]["ImageName"].ToString();
                            hdnMKID3.Value = dt1.Rows[2]["ImageName"].ToString();
                        }
                        else { ImgShow.Visible = false; }
                    }
                    if (dt1.Rows.Count == 4)
                    {

                        if (dt1.Rows[0]["ImageName"].ToString() != "")
                        {
                            ImgShow.Visible = true;
                            hdnMKID.Value = dt1.Rows[0]["ImageName"].ToString();
                            hdnMKID2.Value = dt1.Rows[1]["ImageName"].ToString();
                            hdnMKID3.Value = dt1.Rows[2]["ImageName"].ToString();
                            hdnMKID4.Value = dt1.Rows[3]["ImageName"].ToString();
                        }
                        else { ImgShow.Visible = false; }
                    }
                }
            }



        }
        catch
        {

            throw;
        }

    }
    #region Fill Master Data
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
            if (Convert.ToInt32(ddlYear.SelectedValue) == 2023 && ddlState.SelectedValue == "8")
            {
                if (Convert.ToString(Session["NewDistrictCode"]) == "B7E9D766AC59492CB59167710")
                {
                    conditions = " DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";

                }
                else
                {
                    conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in('33995C4E8A524E26A96111586','6BBFEC8FECDC45DB8E82F0B6A','DCEF975217D94FC98DB0063A3','E10D59036DCC46258BEACFC47') and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";
                }
            }
            else
            {
                conditions = "StateCode  in('" + ddlState.SelectedValue + "') and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";

                //conditions = " RDistrictCode  in(select DistrictCode from mst2District where DistrictCode in(select districtcode from MstUser where UserName = '" + Convert.ToString(Session["username"]) + "'))  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";
            }

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
    public void FillCVillage()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' ";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");


    }
    public void FillSchool()
    {
        //  string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'  union Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "' ";
        string strQry = "Select SchoolCode,Name from mstSchool      inner join (select MainVillagecode,MainSchoolcode from tblEnrolment_Retention2026 group by MainVillagecode,MainSchoolcode  ) tblEnrolment_Retention2026 on tblEnrolment_Retention2026.MainSchoolcode=[mstSchool].SchoolCode where tblEnrolment_Retention2026.MainVillagecode ='" + ddlVillage.SelectedValue + "'    union Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "' ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");

        string strQry1 = "Select SchoolCode,Name from mstSchool      inner join (select MainVillagecode,MainSchoolcode from tblEnrolment_Retention2026 group by MainVillagecode,MainSchoolcode  ) tblEnrolment_Retention2026 on tblEnrolment_Retention2026.MainSchoolcode=[mstSchool].SchoolCode where tblEnrolment_Retention2026.MainVillagecode ='" + ddlVillage.SelectedValue + "'    union Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "' ";

        DataTable dtSchool1 = objMain.LoadData(strQry1);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool1, conditions, "Name", "asc", ddlSchoolMarger, "Name", "SchoolCode", "Select");


    }

    #endregion

    #region   SelectedIndexChanged Methods
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;
        locking();
    }
    public void locking()
    {
        string strQry = "Select * from mstModuleLocking  where [FromNameNew]='Retention Individual' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";

        btnsave.Enabled = true;
        btnDelete.Enabled = true;

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');



        DateTime date1;
        DateTime date2;
        DataTable dtModel = objMain.LoadData(strQry);
        if (dtModel.Rows.Count > 0)
        {

            date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
            date2 = DateTime.Now.Date;

            if (date2 > date1)
            {

                btnsave.Enabled = false;
                btnDelete.Enabled = false;
            }

        }
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        FillSchool();
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool();
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;
    }
    protected void ddlschool_SelectedIndexChanged(object sender, EventArgs e)
    {
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;
        ddlMarge.SelectedIndex = 0;
        ddlSchoolMarger.SelectedIndex = 0;
    }


    protected void ddlschoolgg_SelectedIndexChanged(object sender, EventArgs e)
    {
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;
        divMar.Visible = false;
        ddlSchoolMarger.SelectedIndex = 0;
        if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
        {
            divMar.Visible = true;
        }
        if (Convert.ToInt32(ddlMarge.SelectedValue) == 1)
        {
            div2.Visible = true;
            ddlFCTakeDataAttendance.SelectedIndex = -1;
            ddlreson.SelectedValue = "0";
            ddlTeacherallow.SelectedIndex = -1;
        }
    }
    protected void ddlschoolmm_SelectedIndexChanged(object sender, EventArgs e)
    {
        div2.Visible = false;
        ddlFCTakeDataAttendance.SelectedIndex = -1;
        ddlreson.SelectedValue = "0";
        ddlTeacherallow.SelectedIndex = -1;
        GV_Retention.Visible = false;

        if (ddlSchoolMarger.SelectedIndex >= 0)
        {
            div2.Visible = true;
            ddlFCTakeDataAttendance.SelectedIndex = -1;
            ddlreson.SelectedValue = "0";
            ddlTeacherallow.SelectedIndex = -1;
        }
    }
    #endregion

    protected void GV_Retention_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // UpdateData();
        GV_Retention.PageIndex = e.NewPageIndex;
        if (Session["GridViewData"] != null)
        {
            DataTable dt = Session["GridViewData"] as DataTable;
            GV_Retention.DataSource = dt;
            GV_Retention.DataBind();
        }


    }
    public void UpdateData()
    {

        DataTable dt = (DataTable)Session["GridViewData"];

        for (int i = 0; i < GV_Retention.Rows.Count; i++)
        {
            string C_ID = GV_Retention.DataKeys[i]["UniqueChildCode"].ToString();
            DropDownList ddlAnnual = (DropDownList)GV_Retention.Rows[i].FindControl("ddlAnnual");
            DropDownList ddlReason = (DropDownList)GV_Retention.Rows[i].FindControl("ddlReason");
            TextBox txtOther = (TextBox)GV_Retention.Rows[i].FindControl("txtOther");

            DataRow[] dr = dt.Select("UniqueChildCode='" + Convert.ToString(C_ID) + "'");
            if (dr.Length > 0)
            {

                dr[0]["TempId"] = ddlAnnual.SelectedValue;
                if (Convert.ToInt32(ddlAnnual.SelectedValue) == 2)
                {
                    dr[0]["ReasonforAbsent"] = ddlReason.SelectedValue;

                }
                else
                {
                    dr[0]["ReasonforAbsent"] = "0";
                }

                if (Convert.ToInt32(ddlReason.SelectedValue) == 99)
                {
                    dr[0]["ReasonOther"] = txtOther.Text;

                }
                else
                {
                    dr[0]["ReasonOther"] = "";
                }
            }

        }
        Session["GridViewData"] = dt;

    }
    protected void GV_Retention_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView GV_Retention = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DropDownList ddlNameofChildAvailable = (DropDownList)e.Row.FindControl("ddlNameofChildAvailable");
            DropDownList ddlSupportforChildRegularty = (DropDownList)e.Row.FindControl("ddlSupportforChildRegularty");
            DropDownList ddlReasonforchildnotinReg = (DropDownList)e.Row.FindControl("ddlReasonforchildnotinReg");
            DropDownList ddlPresentClass = (DropDownList)e.Row.FindControl("ddlPresentClass");
            DropDownList ddlIsChildAvailableClassToday = (DropDownList)e.Row.FindControl("ddlIsChildAvailableClassToday");
            DropDownList ddlChildPrestent_Last2Week = (DropDownList)e.Row.FindControl("ddlChildPrestent_Last2Week");
            DropDownList ddlGradeResone = (DropDownList)e.Row.FindControl("ddlGradeResone");
            TextBox txtAttendanceLastdate = (TextBox)e.Row.FindControl("txtAttendanceLastdate");
            DropDownList ddlsr = (DropDownList)e.Row.FindControl("ddlsr");
            TextBox txtSr = (TextBox)e.Row.FindControl("txtSr");


            AjaxControlToolkit.CalendarExtender CalendarExtendere21 = (AjaxControlToolkit.CalendarExtender)e.Row.FindControl("CalendarExtender1");

            CalendarExtendere21.EndDate = DateTime.Now;
            if (ddlFCTakeDataAttendance.SelectedValue == "0")
            {
                //GV_Retention.Columns[7].Visible = true;
                //GV_Retention.Columns[8].Visible = true;
                //GV_Retention.Columns[9].Visible = true;
                //GV_Retention.Columns[10].Visible = true;
                //GV_Retention.Columns[11].Visible = true;
                //GV_Retention.Columns[12].Visible = true;
                txtAttendanceLastdate.Text = "";

            }
            else if (ddlFCTakeDataAttendance.SelectedValue == "1" || ddlFCTakeDataAttendance.SelectedValue == "3")
            {

                GV_Retention.Columns[7].Visible = true;
                GV_Retention.Columns[8].Visible = true;
                GV_Retention.Columns[9].Visible = true;
                GV_Retention.Columns[10].Visible = true;

                GV_Retention.Columns[13].Visible = false;
                GV_Retention.Columns[11].Visible = true;
                if (Convert.ToInt32(ddlsr.SelectedValue) == 0)
                {
                    ddlsr.Enabled = false;
                    txtSr.Enabled = false;
                }
                else
                {
                    ddlsr.Enabled = true;
                    txtSr.Enabled = true;
                }


                //if (Convert.ToInt32(ddlMarge.SelectedValue) == 2)
                //{
                GV_Retention.Columns[8].Visible = true;
                GV_Retention.Columns[9].Visible = true;
                //}
                //else if(Convert.ToInt32(ddlNameofChildAvailable.SelectedValue)==3 )
                //{
                //    GV_Retention.Columns[8].Visible = true;
                //    GV_Retention.Columns[9].Visible = true;
                //}
                //else
                //{
                //    GV_Retention.Columns[8].Visible = false;
                //    GV_Retention.Columns[9].Visible = false;
                //}
                if (ddlNameofChildAvailable.SelectedValue == "1" || ddlNameofChildAvailable.SelectedValue == "3")
                {
                    if (ddlSupportforChildRegularty.SelectedValue == "2")
                    {
                        txtAttendanceLastdate.Enabled = true;
                    }
                }
                else if (ddlNameofChildAvailable.SelectedValue == "2")
                {
                    txtAttendanceLastdate.Text = "";
                    txtAttendanceLastdate.Enabled = false;
                }
                if (ddlNameofChildAvailable.SelectedValue == "0")
                {
                    txtAttendanceLastdate.Text = "";
                    txtAttendanceLastdate.Enabled = true;
                }
            }
            else if (ddlFCTakeDataAttendance.SelectedValue == "2")
            {
                GV_Retention.Columns[7].Visible = false;
                GV_Retention.Columns[8].Visible = false;
                GV_Retention.Columns[9].Visible = false;
                GV_Retention.Columns[10].Visible = true;
                GV_Retention.Columns[11].Visible = true;
                GV_Retention.Columns[12].Visible = false;
                GV_Retention.Columns[13].Visible = true;
            }



            //objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", "Flag=73 and Language=0", "TopicDIscussID", "asc", ddlquestion1, "TopicDiscussName", "TopicDIscussID", "Select");

            //objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", "Flag=73 and Language=0", "TopicDIscussID", "asc", ddlquestion2, "TopicDiscussName", "TopicDIscussID", "Select");

            objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", "Flag=72 and Language=0", "TopicDIscussID", "asc", ddlReasonforchildnotinReg, "TopicDiscussName", "TopicDIscussID", "Select");

            objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='CL' and description in  ('1','2','3','4','5','6','7','8','9','10','11','12')", "LookupCode", "asc", ddlPresentClass, "Description", "LookupCode", "Select");
            objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='DRE' ", "LookupCode", "asc", ddlIsChildAvailableClassToday, "Description", "LookupCode", "Select");
            objComman.BindDLL("mstLookup", "LookupCode,Description", "LookupFlag='CLE' ", "LookupCode", "asc", ddlGradeResone, "Description", "LookupCode", "Select");



            //objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", "Flag=73 and Language=0", "TopicDIscussID", "asc", ddlquestion5, "TopicDiscussName", "TopicDIscussID", "Select");
            //objComman.BindDLL("MSTtopicDiscuss", "TopicDIscussID,TopicDiscussName", "Flag=73 and Language=0", "TopicDIscussID", "asc", ddlquestion6, "TopicDiscussName", "TopicDIscussID", "Select");


        }
    }
    protected void ddlSourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlSourse = (DropDownList)row1.FindControl("ddlSourse");

        TextBox txt55Oyther = (TextBox)row1.FindControl("txt55Oyther");
        if (ddlSourse.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlSourse.SelectedValue) == 223)
            {
                txt55Oyther.Enabled = true;
            }
            else
            {
                txt55Oyther.Enabled = false;
            }
        }
    }
    protected void ddlAnnual_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlAnnual = (DropDownList)row1.FindControl("ddlAnnual");
        DropDownList ddlAbsent = (DropDownList)row1.FindControl("ddlAbsent");
        DropDownList ddlPresent = (DropDownList)row1.FindControl("ddlPresent");

        if (ddlAnnual.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlAnnual.SelectedValue) == 209)
            {

                ddlAbsent.Enabled = true;
                ddlPresent.Enabled = false;
                ddlPresent.SelectedIndex = 0;
            }
            else if (Convert.ToInt32(ddlAnnual.SelectedValue) == 208)
            {
                ddlPresent.Enabled = true;
                ddlAbsent.Enabled = false;
                ddlAbsent.SelectedIndex = 0;
            }
            else
            {
                ddlAbsent.Enabled = false;
                ddlAbsent.SelectedIndex = 0;
                ddlPresent.Enabled = false;
                ddlPresent.SelectedIndex = 0;
            }
        }
        else
        {

            ddlAbsent.Enabled = false;
            ddlAbsent.SelectedIndex = 0;
            ddlPresent.Enabled = false;
            ddlPresent.SelectedIndex = 0;
        }

    }

    protected void ImgShow_Click(object sender, EventArgs e)
    {


        //string Imagefile4 = hdnMKID4.Value + ".jpg";
        //EduImg4.ImageUrl = ResolveUrl("https://www.educategirls.ngo/TabletImage/" + Imagefile4);

        // for local //
        string Imagefile1 = hdnMKID.Value;
        EduImg.ImageUrl = ResolveUrl("~/TabletImage/" + Imagefile1);
        string Imagefile2 = hdnMKID2.Value;
        EduImg2.ImageUrl = ResolveUrl("~/TabletImage/" + Imagefile2);
        string Imagefile3 = hdnMKID3.Value;
        EduImg3.ImageUrl = ResolveUrl("~/TabletImage/" + Imagefile3);
        string Imagefile4 = hdnMKID4.Value;
        EduImg4.ImageUrl = ResolveUrl("~/TabletImage/" + Imagefile4);
        //---------------------------------------//

        // form main //
        //string Imagefile1 = hdnMKID.Value;
        //EduImg.ImageUrl = ResolveUrl("https://www.educategirls.ngo/TabletImage/" + Imagefile1);
        //string Imagefile2 = hdnMKID2.Value;
        //EduImg2.ImageUrl = ResolveUrl("https://www.educategirls.ngo/TabletImage/" + Imagefile2);
        //string Imagefile3 = hdnMKID3.Value;
        //EduImg3.ImageUrl = ResolveUrl("https://www.educategirls.ngo/TabletImage/" + Imagefile3);
        //string Imagefile4 = hdnMKID4.Value;
        //EduImg4.ImageUrl = ResolveUrl("https://www.educategirls.ngo/TabletImage/" + Imagefile4);
        //---------------------------------//



        Modalimages.Show();
    }

    //protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    DropDownList ddlLabTest1 = (DropDownList)sender;
    //    GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

    //    DropDownList ddlReason = (DropDownList)row1.FindControl("ddlReason");
    //    TextBox txtOther = (TextBox)row1.FindControl("txtOther");
    //    if (ddlReason.SelectedIndex > 0)
    //    {
    //        if (Convert.ToInt32(ddlReason.SelectedValue) == 99)
    //        {

    //            txtOther.Enabled = true;
    //        }
    //        else
    //        {
    //            txtOther.Enabled = false;
    //        }
    //    }
    //    else
    //    {
    //        txtOther.Enabled = false;
    //    }
    //}
    //protected void GV_Retention_RowCreated(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {

    //        GridViewRow HeaderRow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //        TableCell HeaderCell2 = new TableCell();           
    //        HeaderCell2.Text = "";
    //        HeaderCell2.ColumnSpan = 6;
    //        HeaderRow.Cells.Add(HeaderCell2);

    //        HeaderCell2 = new TableCell(); 
    //        HeaderCell2.Text = "Annual Exam Status";
    //        HeaderCell2.ColumnSpan = 3;
    //        HeaderRow.Cells.Add(HeaderCell2);

    //        GV_Retention.Controls[0].Controls.AddAt(0, HeaderRow);



    //        GridViewRow HeaderRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        TableCell HeaderCell = new TableCell();
    //        HeaderCell.Text = "SNO.";
    //        HeaderRow1.Cells.Add(HeaderCell);



    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Unique ID";
    //        HeaderRow1.Cells.Add(HeaderCell);



    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Stuent Name";
    //        HeaderRow1.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Father's Name";
    //        HeaderRow1.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Admission Class"; //Annual Exam Status
    //        HeaderRow1.Cells.Add(HeaderCell);


    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "SR No.";
    //        HeaderRow1.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Present";
    //        HeaderRow1.Cells.Add(HeaderCell);



    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "Absent";
    //        HeaderRow1.Cells.Add(HeaderCell);

    //        HeaderCell = new TableCell();
    //        HeaderCell.Text = "None";
    //        HeaderRow1.Cells.Add(HeaderCell);
    //        //HeaderCell.CssClass = "gridnewheadercss";
    //        //HeaderRow.Attributes.Add("class", "Grid");
    //        //HeaderRow1.Attributes.Add("class", "Grid");
    //        HeaderRow.Attributes.Add("class", "HeaderClassCsss");
    //        HeaderRow1.Attributes.Add("class", "HeaderClassCsss");

    //        GV_Retention.Controls[0].Controls.AddAt(1, HeaderRow1);

    //    }

    //}

    //private void GeneratePDF(DataTable dataTable, string rptnm)
    //{
    //    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 20f, 0f);
    //    System.IO.MemoryStream mStream = new System.IO.MemoryStream();
    //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, mStream);
    //    int cols = dataTable.Columns.Count;
    //    int rows = dataTable.Rows.Count;
    //    // pdfDoc.Header = new HeaderFooter(new Phrase("" + rptnm + "", new iTextSharp.text.Font(iTextSharp.text.Font.HELVETICA, 15, 5, iTextSharp.text.Color.RED)), false);
    //    pdfDoc.Header = new HeaderFooter(new Phrase("" + rptnm + "", new iTextSharp.text.Font(iTextSharp.text.Font.BOLD, 12, 5, iTextSharp.text.Color.BLUE)), false);

    //    pdfDoc.Open();
    //    iTextSharp.text.Table pdfTable = new iTextSharp.text.Table(cols, rows);
    //    pdfTable.BorderWidth = 1;
    //    pdfTable.Width = 100;
    //    pdfTable.Padding = 0;
    //    pdfTable.Spacing = 1;


    //    for (int i = 0; i < cols; i++)
    //    {

    //        Cell cellCols = new Cell();
    //        cellCols.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#f1f1f1"));
    //        iTextSharp.text.Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
    //        Chunk chunkCols = new Chunk(dataTable.Columns[i].ColumnName, ColFont);
    //        cellCols.Add(chunkCols);
    //        // cellCols.NoWrap = false;
    //        cellCols.Width = 1000;
    //        pdfTable.AddCell(cellCols);

    //    }
    //    //creating table data (actual result)

    //    for (int k = 0; k < rows; k++)
    //    {
    //        for (int j = 0; j < cols; j++)
    //        {
    //            Cell cellRows = new Cell();
    //            if (k % 2 == 0)
    //            {
    //                cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#FFFfff")); ;
    //                cellRows.Width = 200;

    //            }
    //            else { cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#FFFfff")); }
    //            iTextSharp.text.Font RowFont = FontFactory.GetFont(FontFactory.HELVETICA, 9);
    //            Chunk chunkRows = new Chunk(dataTable.Rows[k][j].ToString(), RowFont);
    //            cellRows.Add(chunkRows);

    //            pdfTable.AddCell(cellRows);


    //        }


    //    }

    //    pdfDoc.Add(pdfTable);
    //    Paragraph footer = new Paragraph("*This is System Generated Copy", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.ITALIC));
    //    footer.Alignment = Element.ALIGN_LEFT;


    //    pdfDoc.Add(footer);

    //    pdfDoc.NewPage();
    //    pdfDoc.Close();
    //    Response.ContentType = "application/octet-stream";
    //    Response.AddHeader("Content-Disposition", "attachment; filename=Annual_Exam_Status_Report" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + ".pdf");

    //    Response.Clear();
    //    Response.BinaryWrite(mStream.ToArray());
    //    Response.End();

    //}

    protected void btnDataApprove_Click(object sender, EventArgs e)
    {
        if (Session["GridViewData"] != null)
        {
            DataTable dt = (DataTable)Session["GridViewData"];

            int Result = 0;

            for (int i = 0; i < GV_Retention.Rows.Count; i++)
            {
                string C_ID = GV_Retention.DataKeys[i]["UniqueChildCode"].ToString();
                DropDownList ddlSourse = (DropDownList)GV_Retention.Rows[i].FindControl("ddlSourse");
                TextBox txt55Oyther = (TextBox)GV_Retention.Rows[i].FindControl("txt55Oyther");
                DropDownList ddlAnnual = (DropDownList)GV_Retention.Rows[i].FindControl("ddlAnnual");
                DropDownList ddlPresent = (DropDownList)GV_Retention.Rows[i].FindControl("ddlPresent");
                DropDownList ddlAbsent = (DropDownList)GV_Retention.Rows[i].FindControl("ddlAbsent");
                int SourceofData = Convert.ToInt32(ddlSourse.SelectedValue);
                string SourceOther = txt55Oyther.Text.Trim();
                int StatusofRetention = Convert.ToInt32(ddlAnnual.SelectedValue);
                int PresentStatus = Convert.ToInt32(ddlPresent.SelectedValue);
                int AbsentReason = Convert.ToInt32(ddlAbsent.SelectedValue);

                if (SourceofData > 0 || StatusofRetention > 0)
                {
                    if (StatusofRetention <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Status of Retention')</script>", false);
                        return;

                    }
                    if (SourceofData == 223)
                    {
                        if (SourceOther == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Fill Other Status')</script>", false);
                            return;
                        }
                    }

                    if (StatusofRetention <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Status of Retention')</script>", false);
                        return;

                    }

                    if (StatusofRetention == 208)
                    {
                        if (PresentStatus == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Present Status')</script>", false);
                            return;
                        }
                    }
                    else if (StatusofRetention == 209)
                    {
                        if (AbsentReason == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Absent Reason')</script>", false);
                            return;
                        }
                    }
                    Result = UpdateRetentionIndividualApprove(C_ID);

                }
            }
            if (Result > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve Successfull')</script>", false);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Approval')</script>", false);
            }
            FillGrid();
        }
    }
    public int UpdateRetentionIndividual(string UniqueChildCode, int sourceofData, string sourceOther, int statusofRetention, int presentStatus, int absentReason)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode", UniqueChildCode),
            new SqlParameter("@sourceofData", sourceofData),
            new SqlParameter("@sourceOther", sourceOther),
            new SqlParameter("@statusofRetention", statusofRetention),
            new SqlParameter("@presentStatus", presentStatus),
            new SqlParameter("@absentReason", absentReason)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_updateRetentionIndividuals", cmdParameters);
    }

    public int UpdateRetentionIndividualNew(string UniqueChildCode, int NameofChildAvailable, int SupportforChildRegularty, int ReasonforchildnotinReg, int PresentClass, int IsChildAvailableClassToday, int ChildPrestent_Last2Week, string Uniquecode, string CreatedBy, DateTime AttendanceLastdate, int IsSRRight, string NewSR, string GradeResone)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode", UniqueChildCode),
            new SqlParameter("@NameofChildAvailable", NameofChildAvailable),
            new SqlParameter("@SupportforChildRegularty", SupportforChildRegularty),
            new SqlParameter("@ReasonforchildnotinReg", ReasonforchildnotinReg),
            new SqlParameter("@PresentClass", PresentClass),
            new SqlParameter("@IsChildAvailableClassToday", IsChildAvailableClassToday),
            new SqlParameter("@ChildPrestent_Last2Week", ChildPrestent_Last2Week),
            new SqlParameter("@ChildUniqueID", Uniquecode),
            new SqlParameter("@CreatedBy", CreatedBy),
            new SqlParameter("@AttendanceLastdate",(AttendanceLastdate).ToString("yyyy-MM-dd")),
             new SqlParameter("@IsSRRight", IsSRRight),
              new SqlParameter("@NewSR", NewSR),
                new SqlParameter("@GradeResone", GradeResone),

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_updateRetentionIndividualNew", cmdParameters);
    }

    public int InsertUpdateRetentionIndividualMain(string Uniquecode, string villagecode, string schoolcode, int FCTakeDataAttendance, int ReasonnotTakingData, string Teacherallow, string flag, int Retention_ID, string CreatedBy, string MargeSchool, string SchoolStatus)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {   new SqlParameter("@Outputresult",SqlDbType.Int),
            new SqlParameter("@Flag",flag),
            new SqlParameter("@Retention_ID",Retention_ID),
            new SqlParameter("@Uniquecode", Uniquecode),
            new SqlParameter("@villagecode", villagecode),
            new SqlParameter("@schoolcode", schoolcode),
            new SqlParameter("@FCTakeDataAttendance", FCTakeDataAttendance),
            new SqlParameter("@ReasonnotTakingData", ReasonnotTakingData),
            new SqlParameter("@Teacherallow", Teacherallow),
            new SqlParameter("@CreatedBy", CreatedBy),

              new SqlParameter("@MargeSchool", MargeSchool),
                 new SqlParameter("@SchoolStatus", SchoolStatus)

        };
        cmdParameters[0].Direction = ParameterDirection.Output;
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_InsertupdateRetentionIndividualMain", cmdParameters);
        result = Convert.ToInt32(cmdParameters[0].Value);
        return result;
    }



    public int UpdateRetentionIndividualApprove(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode", UniqueChildCode)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_updateRetentionIndividualsApproval", cmdParameters);
    }
}