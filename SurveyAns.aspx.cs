using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class SurveyAns : System.Web.UI.Page
{
    [System.Runtime.InteropServices.ComVisible(true)]
    int FormID;
    clsMain objBLL = new clsMain();
    public class KeyPressEventArgs : EventArgs { }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //lblstste.Text = "Bihar";
            //hdnStateId.Value = "4";
            //lbldistrict.Text = Session["District"] == null ? "" : Session["District"].ToString();
            //hdndistrict.Value = Session["DistrictID"] == null ? "0" : Session["DistrictID"].ToString();
            //hdnblock.Value = Session["BlockID"] == null ? "0" : Session["BlockID"].ToString();
            //string GUID = Session["GUID"] == null ? "0" : Session["GUID"].ToString();
            //FillState();
            //FillYear();
            //hdnmonth.Value = Session["Month"] == null ? "0" : Session["Month"].ToString();
            //ddlyear.Value = Session["Year"] == null ? "0" : Session["Year"].ToString();
            //if (GUID == "0")
            //{
            //    hdnformid.Value = "1296";
            //    FillQuestion(1296, 0);
            //}
            //else
            //{
            //    hdnformid.Value = Session["FormID"].ToString();
            //    ViewQuestion(Session["FormID"].ToString(), 0);
            //}
            if (Request.QueryString["ID"] != null)
            {
                string QueryString = Request.QueryString["ID"];

                FillFormNameLink(QueryString);
                if (ddlForm.SelectedIndex > 0)
                {
                    FillParticiparticipate(ddlForm.SelectedValue);
                    // FillQuestion(Convert.ToInt32(ddlForm.SelectedValue));

                    Session["FormID"] = ddlForm.SelectedValue;
                    Session["Fdate"] = DateTime.Now;
                }
            }
            else
            {
                //FillFormName();
                //FillQuestion(Convert.ToInt32(ddlForm.SelectedValue));
                //Session["FormID"] = ddlForm.SelectedValue;
            }
            //userid.InnerText = Session["UserID"].ToString();
        }
    }
    public void FillState()
    {
        //DataTable dtstate = new DataTable();
        //dtstate = objBLL.Exec_Procedure("GetState_For_Flood");
        //ddlstate.DataSource = dtstate;
        //ddlstate.DataValueField = "StateID";
        //ddlstate.DataTextField = "State";
        //ddlstate.DataBind();block
        //ddlstate.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    public void Filldistrict(string StateID)
    {
        //DataTable dtdistrict = new DataTable();
        //dtdistrict = objBLL.Get_DataFor1Filter("GetDistrict_For_Flood", StateID);
        //ddldistrict.DataSource = dtdistrict;
        //ddldistrict.DataValueField = "DistrictID";
        //ddldistrict.DataTextField = "District";
        //ddldistrict.DataBind();
        //ddldistrict.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    public DataTable Get_DataFor3Filter(string ProcedureName, string Filter1, string Filter2, string Filter3)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                            new SqlParameter("@Filter3",Filter3),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    public void FillFormNameLink(string QueryString)
    {
        //string UserID = Session["UserID"].ToString();

        DataTable dt = new DataTable();
        //int FormLevel;

        dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevelFormLink", "", QueryString, "");
        //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");
        if (dt.Rows.Count > 0)
        {
            ddlForm.DataSource = dt;
            ddlForm.DataTextField = "FormName";
            ddlForm.DataValueField = "FormID";
            ddlForm.DataBind();
            lblmsg.Text = dt.Rows[0]["FormName"].ToString();
            ddlForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

            ddlForm.SelectedIndex = 1;
        }
        else
        {
            ddlForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

        }



    }

    public void FillParticiparticipate(string QueryString)
    {
        //string UserID = Session["UserID"].ToString();

        DataTable dt = new DataTable();
        //int FormLevel;

        dt = Get_DataFor3Filter("USP_GetSurveyParticiparticipate", "", QueryString, "");
        //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");
        if (dt.Rows.Count > 0)
        {
            ddlParticipate.DataSource = dt;
            ddlParticipate.DataTextField = "ParticiparticipateName";
            ddlParticipate.DataValueField = "ParticiparticipateID";
            ddlParticipate.DataBind();

            ddlParticipate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

            ddlParticipate.SelectedIndex = 0;
            Session["ParticipartName"] = dt;
        }
        else
        {
            ddlParticipate.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));

        }



    }

    public void FillFormName()
    {
        //string UserID = Session["UserID"].ToString();

        DataTable dt = new DataTable();
        //int FormLevel;

        dt = Get_DataFor3Filter("USP_GetSurveyOnAgencyAndLevelForm", "", ddlForm.SelectedValue.ToString(), "");
        //dt = objBLL.Select_All_Data("MSTForm", "FormID,FormName", "IsDeleted = 0 and FormLevel = " + FormLevel  + " ", "", "");

        ddlForm.DataSource = dt;
        ddlForm.DataTextField = "FormName";
        ddlForm.DataValueField = "FormID";
        ddlForm.DataBind();
        ddlForm.Items.Insert(0, new System.Web.UI.WebControls.ListItem("------Select-------", "0"));
        ddlForm.SelectedValue = "6";
        lblmsg.Text = dt.Rows[0]["FormName"].ToString();

    }

    public DataTable Get_DataFor2Filter(string ProcedureName, string Filter1, string Filter2)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    public void FillQuestion(int FormID)
    {
        DataTable dt = new DataTable();
        //hdnconditions.Value = Condition.ToString();
        dt = Get_DataFor3Filter("USP_GetQuestionTraining20242025User", "0", FormID.ToString(), "0");

        //  USP_GetQuestionTraining2024User
        Session["Ism"] = dt;
        //DataListQuestion.DataSource = dt;
        //DataListQuestion.DataBind();
        fillQuestions(dt);
        //SetSkipLogicAttribute();
    }
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt2 = null;
        DataTable dtP = HttpContext.Current.Session["ParticipartName"] as DataTable;
        string FID = HttpContext.Current.Session["FormID"] as string;
        string ll = "0";

        string ParticiparticipateName = "";
        clsMain objBLL1 = new clsMain();
        if (dtP.Rows.Count > 0)
        {
            DataRow[] dr = dtP.Select("EGcode='" + txtP.Text.Trim() + "'");
            if (dr.Length > 0)
            {
                ddlParticipate.SelectedValue = Convert.ToString(dr[0]["ParticiparticipateID"]);
                dt2 = objBLL1.Get_DataFor8Filter("rptQuestionvaldation2026", FID, Convert.ToString(dr[0]["ParticiparticipateID"]));
                if (dt2.Rows.Count > 0)
                {
                    ll = Convert.ToString(dr[0]["ParticiparticipateID"]);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Data Already Submitted')</script>", false);
                    dialog.Text = "";
                    Savebutton.Text = "";
                    return;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Participant not Found')</script>", false);
                dialog.Text = "";
                Savebutton.Text = "";
                return;
            }
        }
        if (ddlParticipate.SelectedIndex > 0)
        {
            FillQuestion(Convert.ToInt32(ddlForm.SelectedValue));
        }
        else
        {
            dialog.Text = "";
            Savebutton.Text = "";
        }

        DataTable dt = objBLL1.Get_DataFor8Filter("rptQuestionvaldation2026", ddlForm.SelectedValue, ddlParticipate.SelectedValue);

    }

    public void fillQuestions(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        string Type, Questionid, Length;
        DataTable dtTempMSCommon = new DataTable();
        int QuestionType = 0;
        DataTable dtMSCommon = Get_DataFor2Filter("USP_GetoptionsforWebSurvey", "4", ddlForm.SelectedValue);
        int GID = 0;
        int MGID = 0;
        sb.Append("<div class='container mt-3'> <div class='col-xl-12 col-lg-12 col-md-12 col-sm-12 m - auto p-0'><form class='form-horizontal'><table id='questions'  width='100%'> ");
        int icount = 0;
        int GroupCOunt = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Questionid = dt.Rows[i]["QuestionId"].ToString();
            Length = dt.Rows[i]["MaxLenght"].ToString();
            QuestionType = Convert.ToInt32(dt.Rows[i]["QuestionType"].ToString());
            Type = gettypeofQuestion(dt.Rows[i]["QestionTypeID"].ToString(), dt.Rows[i]["Flag"].ToString(), dtMSCommon, dtTempMSCommon, Questionid, Length, Convert.ToInt32(dt.Rows[i]["MaskValidation"].ToString()), dt.Rows[i]["Value"].ToString(), dt.Rows[i]["QuestionAns"].ToString());
            icount = icount + 1;
            if (dt.Rows[i]["QestionTypeID"].ToString() == "9" && dt.Rows[i]["GroupID"].ToString() == "1")
            {
                GID = Convert.ToInt32(Questionid);
                DataRow[] dr = dt.Select("GroupID=" + GID + "");
                if (dr.Length > 0)
                {
                    icount = 0;
                    GroupCOunt = dr.Length;
                }
                sb.Append("<tr><td><table id='questions1'  class='w-100'><tr class=" + Questionid + " ><td style='width:50px'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td >" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "  </td></tr>");

                // sb.Append("<tr class='header' style='background-color:#354ea0; font-size:15px; font-weight:bold;'><td style='width:100px;vertical-align: top'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td colspan = '2'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </td></tr>");

                // sb.Append("<fieldset class='box -border'> <legend class='box-border'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "</legend>");
                //  sb.Append("<tr class='header' style='background-color:#354ea0; font-size:15px; font-weight:bold;'><td style='width:100px;vertical-align: top'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td colspan = '2'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " <table id='WebSurtte' class='table table-bordered' style='font-weight: 400;font-size: 14px;'  width='100%'>");
            }
            else if (dt.Rows[i]["QestionTypeID"].ToString() == "9")
            {

                //  sb.Append("<tr class='header' style='background-color:#354ea0; font-size:15px; font-weight:bold;'><td style='width:100px;vertical-align: top'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td colspan = '2'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </td></tr>");
                sb.Append("<tr><td><table  id='questions1' class='w-100'><tr class=" + Questionid + " ><td style='width:50px'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td >" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "  </td></tr></table></td></tr> ");

                // sb.Append("<fieldset class='box -border'> <legend class='box-border'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "</legend>");
            }
            else if (Convert.ToInt32(dt.Rows[i]["GroupID"].ToString()) > 0)
            {
                string Idmm = "";
                if (Convert.ToInt32(dt.Rows[i]["IsQuestionMandatory"]) == 1)
                {
                    Idmm = "<lable style = 'color: Red' > *</ lable >";
                }
                //  sb.Append("<tr class='header' style='background-color:#354ea0; font-size:15px; font-weight:bold;'><td style='width:100px;vertical-align: top'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td colspan = '2'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </td></tr>");
                sb.Append("<tr><td><td><table  id='questions1' class='w-100'><tr class=" + Questionid + " ><td style='width:50px'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td >" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "   " + Idmm + "</td></tr><tr><td style='width:50px'><td>" + Type + "</td></tr></table></td></td></tr> ");

                // sb.Append("<fieldset class='box -border'> <legend class='box-border'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "</legend>");
            }
            else
            {
                string Idmm = "";
                if (QuestionType == 2)
                {

                    // sb.Append("<tr class=" + Questionid + " style='background-color:#ffdfba'><td style='width:2%'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td width='48%' class='fs'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </td>");
                    string img = ResolveUrl("~/Survey/" + dt.Rows[i]["ImageUpload"].ToString());


                    if (Convert.ToInt32(dt.Rows[i]["IsQuestionMandatory"]) == 1)
                    {
                        Idmm = "<lable style = 'color: Red' > *</ lable >";
                    }
                    sb.Append("<tr><td><table  id='questions1' class='w-100'><tr class=" + Questionid + " ><td style='width:50px'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td class='fs' >" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "  </br><img  Height = '80px' Width = '100px' BorderStyle = 'Ridge' BorderWidth = '1px' src = " + img + "> " + Idmm + "</td></tr><tr><td style='width:50px'><td>" + Type + "</td></tr></table></td></tr> ");

                    //sb.Append("<tr class=" + Questionid + " style='background-color:#ffdfba'><td style='width:2%'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td width='48%' class='fs'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + " </br><img  Height = '80px' Width = '100px' BorderStyle = 'Ridge' BorderWidth = '1px' src = " + img + "> " + Idmm + " </td>");
                }
                else
                {
                    if (Convert.ToInt32(dt.Rows[i]["IsQuestionMandatory"]) == 1)
                    {
                        // Idmm = "<span style = 'color: Red' > *</ span >";

                        Idmm = "<lable style = 'color: Red' > *</ lable >";
                    }
                    //  sb.Append("<tr class=" + Questionid + " style='background-color:#ffdfba'><td style='width:2%'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td width='48%' class='fs'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "   " + Idmm + "</td>");
                    sb.Append("<tr><td><table  id='questions1' class='w - 100'><tr class=" + Questionid + " ><td style='width:50px'><span> " + dt.Rows[i]["QuestionNo"].ToString() + " </span></td><td class='fs'>" + dt.Rows[i]["Question"].ToString().Replace("'", "") + "   " + Idmm + "</td></tr><tr><td style='width:50px'><td>" + Type + "</td></tr></table></td></tr> ");

                }
                //  sb.Append("<td width='50%'> " + Type + " </td></tr>");
            }
            if (dt.Rows[i]["GroupID"].ToString() != "1")
            {

                if (icount == GroupCOunt)
                {
                    sb.Append("</table></td></tr>");
                }

            }
            MGID = Convert.ToInt32(dt.Rows[i]["GroupID"]);

        }
        //if (GID > 0)
        //{
        //    sb.Append("</table></td></tr>");
        //}
        sb.Append("</table></form></div></div>");
        dialog.Text = sb.ToString();
        StringBuilder sb2 = new StringBuilder();
        sb2.Append("<input type='button' name='Submit' class='btn btn-primary px-5' id='IDSub' value='Submit' onclick='savedata()'/>");
        Savebutton.Text = sb2.ToString();
        //14300/14301/14302
    }

    public string gettypeofQuestion(string Qtype, string flag, DataTable dtcommon, DataTable dttempcommon, string Questionid, string Length, int MaskValidation, string value, string QuestionAns)
    {
        //string Ntextboxhtml = "<input type='text' class='form-control' maxlength='" + Length + "' onchange='return checkDec(this);' Style='margin-top: 5px' id='" + Questionid + "' name='Numeric' placeholder='Numeric Value'>";
        string Dtextboxhtml = "<input type='date' class='form-control'  Style='margin-top: 5px' id='" + Questionid + "' name='Date' placeholder='dd/MM/yyyy'>";
        string Timetextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' id='" + Questionid + "' name='Time' placeholder='hh:mm:ss'>";
        string Imagehtml = "<span class='glyphicon glyphicon-picture' Style='margin-top: 5px; font-size:18px'></span>";
        string FingerPrnthtml = "<img src='images/fingerprint-2-512.png' alt='Finger Print' Style='height:25px; width:25px;'/> ";
        string Imgtextboxhtml = "<input type='file' class='form-control' Style='margin-top: 5px;'  onchange='Imageuploaddata(" + Questionid + "," + "lbl" + Questionid + ")'  id='" + Questionid + "' name='fname'><asp:HiddenField runat='server' id='" + "lbl" + Questionid + "'  Value=''  /> ";
        //string Imgtextboxhtml = "<input type='file' class='form-control' Style='margin-top: 5px'  id='File' name='fname'> ";

        string Ntextboxpercentage = "<input type='text' class='form-control' maxlength='" + Length + "' onchange='FN14351(" + Questionid + ")' Style='margin-top: 5px' id='" + Questionid + "' name='Numeric' placeholder='Numeric Value'>";


        if (Qtype == "1")
        {
            string Stextboxhtml = "";

            if (MaskValidation == 0)
            {
                Stextboxhtml = "<input type='text' class='form-control' maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='Text Box' >";

            }

            if (MaskValidation == 1)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return validateFristNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 2)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return NotOnlyNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 3)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return validateOnlyText(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 4)
            {
                Stextboxhtml = "<input type='text'  class='form-control'  onchange='return CheckMobile(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 5)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return ValidateEmail(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 6)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return checkDec(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 7)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return alphanumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }

            Qtype = Stextboxhtml;
        }
        else if (Qtype == "2")
        {
            string Stextboxhtml = "";

            if (MaskValidation == 0)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return isNumberKey(this,event);' maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='Numeric Value' >";

            }

            if (MaskValidation == 1)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return validateFristNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 2)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return NotOnlyNumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 3)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return validateOnlyText(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 4)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return CheckMobile(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 5)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onchange='return ValidateEmail(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 6)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return checkDec(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }
            if (MaskValidation == 7)
            {
                Stextboxhtml = "<input type='text' class='form-control'  onkeyup='return alphanumeric(this);'  maxlength='" + Length + "' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='" + value + "' >";

            }

            Qtype = Stextboxhtml;
            //DataTable dt = Select_All_Data("tblQuestionMapping", "*", "ParentQuestionId = " + Questionid + "", "", "");
            ////--------- if skiplogic apply
            //if (dt.Rows.Count > 0)
            //{
            //    Qtype = Ntextboxpercentage;
            //}
            //else
            //{
            //    Qtype = Ntextboxhtml;
            //}

        }

        else if (Qtype == "6")
        {
            Qtype = Imgtextboxhtml;
        }
        else if (Qtype == "Finger Print")
        {
            Qtype = FingerPrnthtml;
        }
        else if (Qtype == "11")
        {
            string Stextboxhtml = "";

            Stextboxhtml = "<input type='text' maxlength='" + Length + "' Style='width:250px;height:60px;' id='" + Questionid + "' name='Text' placeholder='Text Box' >";

            //  Stextboxhtml = "<textarea  type='text' class='form-control' maxlength='" + Length + "' Style='width:250px;height:150px;' id='" + Questionid + "' name='textarea'  placeholder='Multiline Text' >";
            Qtype = Stextboxhtml;

        }
        else if (Qtype == "3")
        {
            Qtype = Dtextboxhtml;
            if (MaskValidation == 8)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>NotallowFeatureDate(" + Questionid + ")</script>", false);
            }
            if (MaskValidation == 9)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "", "<SCRIPT LANGUAGE='javascript'>NotallowPastDate(" + Questionid + ")</script>", false);
            }

        }
        else if (Qtype == "8")
        {
            Qtype = Timetextboxhtml;
        }
        else if (Qtype == "4")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();


            DataTable dt = Select_All_Data("tblQuestionMapping", "*", "ParentQuestionId = " + Questionid + "", "", "");
            //--------- if skiplogic apply
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dttempcommon.Rows.Count; i++)
                {
                    Qtype = Qtype + "<input type='radio' onchange='SetLogic(" + dttempcommon.Rows[i]["ID"] + "," + Questionid + ")' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "<br />";
                }
            }
            else
            {
                if (QuestionAns.Length > 0)
                {
                    string[] words = QuestionAns.Trim().Split(',');
                    foreach (var word in words)
                    {
                        for (int i = 0; i < dttempcommon.Rows.Count; i++)
                        {
                            if (dttempcommon.Rows[i]["ID"].ToString() == word)
                            {
                                Qtype = Qtype + "<input type='radio' class='inp'  value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'><em class='labs'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "</em><br />";
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dttempcommon.Rows.Count; i++)
                    {
                        Qtype = Qtype + "<input type='radio'  class='inp'  value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'><em class='labs'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "</em><br />";
                    }
                }
            }


        }
        else if (Qtype == "5")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();
            if (QuestionAns.Length > 0)
            {
                string[] words = QuestionAns.Trim().Split(',');
                foreach (var word in words)
                {
                    for (int i = 0; i < dttempcommon.Rows.Count; i++)
                    {
                        if (dttempcommon.Rows[i]["ID"].ToString() == word)
                        {
                            Qtype = Qtype + "<input type='checkbox' class='inp'  value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'><em class='labs'>" + dttempcommon.Rows[i]["Value"].ToString().Trim().Replace("'", "") + "</em> <br />";

                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dttempcommon.Rows.Count; i++)
                {
                    Qtype = Qtype + "<input type='checkbox' class='inp' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'><em class='labs'> " + dttempcommon.Rows[i]["Value"].ToString().Trim().Replace("'", "") + "</em> <br />";
                }
            }
        }


        else if (Qtype == "10")
        {
            Qtype = "";
            DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
            if (dr2.Length > 0)
                dttempcommon = dr2.CopyToDataTable();
            Qtype = Qtype + "<select  class='form-control' id='" + Questionid + "' Name='Dropdown'>";

            if (QuestionAns.Length > 0)
            {

                Qtype = Qtype + "<option type='checkbox'  value=" + 0 + ">--Select --</option>";

                string[] words = QuestionAns.Trim().Split(',');
                foreach (var word in words)
                {
                    for (int i = 0; i < dttempcommon.Rows.Count; i++)
                    {

                        if (dttempcommon.Rows[i]["ID"].ToString() == word)
                        {
                            if (i == 0)
                            {

                                Qtype = Qtype + "<option type='checkbox' value=" + dttempcommon.Rows[i]["ID"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
                            }
                            else

                            {

                                Qtype = Qtype + "<option type='checkbox'  value=" + dttempcommon.Rows[i]["ID"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dttempcommon.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        Qtype = Qtype + "<option type='checkbox'  value=" + i + ">--Select --</option>";
                        Qtype = Qtype + "<option type='checkbox' value=" + dttempcommon.Rows[i]["ID"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
                    }
                    else
                    {
                        Qtype = Qtype + "<option type='checkbox'  value=" + dttempcommon.Rows[i]["ID"] + ">" + dttempcommon.Rows[i]["Value"] + "</option>";
                    }
                }
            }
            Qtype = Qtype + "</select>";
        }
        return Qtype;
    }
    public DataTable GetFieldAns(string StateID, string FormID, string QuestionId, string falg, string Switch, string userid)
    {
        DataTable dt = new DataTable();
        SqlParameter[] parm = new SqlParameter[]
           {
             new SqlParameter("@StateID", StateID),
             new SqlParameter("@FormID", FormID),
             new SqlParameter("@QuestionId", QuestionId),
             new SqlParameter("@falg", falg),
             new SqlParameter("@Switch", Switch),
             new SqlParameter("@userid", userid),
           };

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "USP_GetoptionwithAns_DiffrentLanguage", parm);

        return dt;
    }



    public bool CheckSkipLogic(int questionID, int nextQuestionID, int flagValue, DataTable dtLogic)
    {
        return dtLogic.AsEnumerable().Where(x => x.Field<int?>("ParentQuestionId") == questionID && x.Field<int?>("DependentQuestionId") == nextQuestionID && x.Field<string>("FlagValue").ToString() == flagValue.ToString()).Any();
    }

    protected void txtage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int Age = Convert.ToInt32(txtage.Text.Trim());
            txtage.ForeColor = System.Drawing.Color.Black;
            if (Age < 5 || Age > 100)
            {
                txtage.Text = "";
                ErrorAge.Visible = true;
            }
            else
            {
                ErrorAge.Visible = false;
            }
        }
        catch (Exception)
        {
            txtage.Text = "Not Valid Input";
            txtage.ForeColor = System.Drawing.Color.Red;
            ErrorAge.Visible = true;
        }

    }

    public void CheckMandatory(string message)
    {
        lblmandotrymsg.Text = message;
        mdlMendetory.Show();
    }

    public DataTable Formevaluation()
    {
        DataTable dt = new DataTable();
        DataColumn FormEvalGUID = new DataColumn("FormEvalGUID", typeof(System.String));
        DataColumn QuestionID = new DataColumn("QuestionID", typeof(System.Int32));
        DataColumn QuestionValue = new DataColumn("QuestionValue", typeof(System.String));
        dt.Columns.AddRange(new DataColumn[] { FormEvalGUID, QuestionID, QuestionValue });
        return dt;
    }

    public static string Generate_RandomString()
    {
        Thread.Sleep(1);
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, 30).Select(s => s[random.Next(s.Length)]).ToArray()) + DateTime.Now.ToString("yyyyMMddhhmmssFFFFFFF");
        return result.ToString();
    }

    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdnStateId.Value = ddlstate.SelectedValue;
        Filldistrict(ddlstate.SelectedValue);
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdndistrict.Value = ddldistrict.SelectedValue.ToString();
    }


    public void clear()
    {
        ddlstate.SelectedValue = "0";
        ddldistrict.SelectedValue = "0";
        txtage.Text = "";
        ddlgender.SelectedValue = "0";
    }

    public void FillYear()
    {
        var currentYear = DateTime.Today.Year;
        for (int i = 0; i <= 10; i++)
        {
            // Now just add an entry that's the current year minus the counter
            ddlyear.Items.Add((currentYear + i).ToString());
        }
    }

    public void ShowMessage(string Msg)
    {
        lblmessage.Text = Msg;
        ModalPopfs.Show();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        //Response.Redirect("Login");
    }

    public string fileupload(FileUpload fplUpload)
    {
        if (fplUpload.HasFile)
        {
            try
            {
                string Extension = Path.GetExtension(fplUpload.PostedFile.FileName);
                hdnExtension.Value = Extension;

                if ((Extension == ".jpg") || (Extension == ".jpeg") || (Extension == ".png") || (Extension == ".xls") || (Extension == ".xlsx") || (Extension == ".docx") || (Extension == ".pdf"))
                {
                    string FileName = Convert.ToString(fplUpload.PostedFile.FileName).Trim();//Path.GetFileName(excelFileUpload.PostedFile.FileName);
                                                                                             //ViewState["FileName"] = FileName;
                    hdnfilename.Value = FileName + DateTime.Now.ToString();
                    string FilePath = Server.MapPath(Comman.GetImagePath("SurveyFilesPath") + "/") + FileName;
                    fplUpload.SaveAs(FilePath);

                }
                else
                {
                    //StatusLabel.Text = "Upload status: Only jpg/jpeg/png/xls/doc/pdf files are accepted!";
                }


            }
            catch
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }



        }
        else
        {
            //StatusLabel.Text = "Upload status: Choose A File Heare";
        }
        return hdnfilename.Value;
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string Savefileupload(string Fil)
    {

        string FilePath = "";
        //if (fplUpload.HasFile)
        //{
        //    try
        //    {
        //        //string Extension = Path.GetExtension(fplUpload.PostedFile.FileName);


        //        //if ((Extension == ".jpg") || (Extension == ".jpeg") || (Extension == ".png") || (Extension == ".xls") || (Extension == ".xlsx") || (Extension == ".docx") || (Extension == ".pdf"))
        //        //{
        //        //    string FileName = Convert.ToString(fplUpload.PostedFile.FileName).Trim();//Path.GetFileName(excelFileUpload.PostedFile.FileName);
        //        //                                                                             //ViewState["FileName"] = FileName;

        //        //    FilePath = HttpContext.Current.Server.MapPath(Comman.GetImagePath("SurveyFilesPath") + "/") + FileName;
        //        //    fplUpload.SaveAs(FilePath);

        //        //}
        //        //else
        //        //{
        //        //    //StatusLabel.Text = "Upload status: Only jpg/jpeg/png/xls/doc/pdf files are accepted!";
        //        //}


        //    }
        //    catch
        //    {
        //        //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
        //    }



        //}
        //else
        //{
        //    //StatusLabel.Text = "Upload status: Choose A File Heare";
        //}
        return FilePath;
    }
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string Savedata(string data, string StateID, string DistrictID, string Blockid, string FormID, string FinalFlag, string Year, string Month)
    {
        string returndata = "";
        try
        {
            DateTime Fdate = Convert.ToDateTime(HttpContext.Current.Session["Fdate"]);
            DataTable dtCheck = HttpContext.Current.Session["Ism"] as DataTable;
            string FormEvalGUID = "", GUID = "";
            string UserID = "0";
            string GUIDold = "0";
            string Flag = "0";
            if (GUIDold == "0")
            {
                GUID = Guid.NewGuid().ToString();
                FormEvalGUID = GUID + DateTime.Now;
            }
            else
            {
                FormEvalGUID = GUIDold;
            }
            DataTable finaldt = (DataTable)JsonConvert.DeserializeObject(data, (typeof(DataTable)));
            DataColumn newCol = new DataColumn("FormEvalGUID", typeof(string));
            newCol.DefaultValue = FormEvalGUID.ToString();
            newCol.AllowDBNull = true;
            finaldt.Columns.Add(newCol);
            string FID = HttpContext.Current.Session["FormID"] as string;
            string point = "";
            clsMain objBLL = new clsMain();
            DataTable dtcheck = objBLL.Get_DataFor8Filter("rptQuestionvaldation2026", FID, DistrictID);
            if (dtcheck.Rows.Count > 0)
            {
                returndata = "Data Already Submitted" + "___" + Flag;
            }
            else
            {



                for (int i = 0; i < dtCheck.Rows.Count; i++)
                {
                    int IsQuestionMandatory = Convert.ToInt32(dtCheck.Rows[i]["IsQuestionMandatory"]);
                    int QuestionId = Convert.ToInt32(dtCheck.Rows[i]["QuestionId"]);

                    string QuestionNo = Convert.ToString(dtCheck.Rows[i]["QuestionNo"]);
                    if (IsQuestionMandatory == 1)
                    {
                        if (finaldt != null)
                        {
                            DataRow[] dr = finaldt.Select("QuestionId ='" + QuestionId + "'");
                            if (dr.Length > 0)
                            {

                            }
                            else
                            {
                                returndata += "Question No:" + QuestionNo.ToString() + "  Mandatory\n";
                            }
                        }
                    }
                }
                if (returndata.Length > 0)
                {
                    returndata = returndata + "___" + Flag;
                }
                else
                {
                    DataTable dt = UploadFormEvaluation(finaldt, FID, FormEvalGUID.Trim(), "0", DistrictID, "0", "0", FinalFlag, "0", "0", Fdate);

                    if (dt != null)
                    {
                        point = dt.Rows[0]["ReturnValue"].ToString();
                        // rptSurveyAnsScore
                        if (point == "1")
                        {
                            string mg = "Data Submitted Successfully.";
                            clsMain objBLL1 = new clsMain();
                            DataTable dtEv = objBLL1.Get_DataFor8Filter("rptSurveyAnsScore2026", FID, DistrictID);
                            if (dtEv.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dtEv.Rows[0]["Score"]) > 0)
                                {
                                    mg = "Data Submitted Successfully.  Your Total Score : " + dtEv.Rows[0]["Score"] + "";
                                }

                            }
                            Flag = "1";
                            HttpContext.Current.Session["Flag"] = null;
                            HttpContext.Current.Session["GUID"] = FormEvalGUID.Trim();
                            returndata = mg + "___" + Flag;

                        }
                        else if (point == "2")
                        {
                            HttpContext.Current.Session["Flag"] = null;
                            HttpContext.Current.Session["GUID"] = FormEvalGUID.Trim();
                            returndata = "Data Update Successfully." + "___" + Flag;
                        }
                        else if (point == "3")
                        {
                            HttpContext.Current.Session["Formid"] = "1296";
                            returndata = "Data Final Submitted." + "___" + Flag;
                        }
                        else
                        {
                            returndata = "Please Fill Form." + "___" + Flag;
                        }
                    }
                    else
                    {
                        returndata = "Please Fill Form." + '-' + Flag;
                    }
                }

            }
        }
        catch
        {
            returndata = "Please Fill Form." + "___" + "0";
        }

        return returndata;
    }

    public static DataTable UploadFormEvaluation(DataTable Questions_Table, string FormID, string FormEvalGUID, string StateID, string DistrictID, string BlockID, string UserID, string FinalFlag, string Year, string Month, DateTime Fdate)
    {
        DataTable dt = new DataTable();
        SqlParameter[] parm = new SqlParameter[]
           {
             new SqlParameter("@UserDefineTable", Questions_Table),
             new SqlParameter("@FormID", FormID),
             new SqlParameter("@FormEvalGUID", FormEvalGUID),
             new SqlParameter("@StateID", StateID),
             new SqlParameter("@DistrictID", DistrictID),
             new SqlParameter("@BlockID", BlockID),
             new SqlParameter("@UserID", UserID),
             new SqlParameter("@FinalFlag", FinalFlag),
             new SqlParameter("@Year", Year),
             new SqlParameter("@Month", Month),
                  new SqlParameter("@Fdate", Fdate),
           };

        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "CopyQuestionfromOtherFormBank2025", parm);

        return dt;
    }
    public DataTable Get_DataFor4Filter(string ProcedureName, string Filter1, string Filter2, string Filter3, string Filter4)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
{
        new SqlParameter("@Filter1",Filter1),
        new SqlParameter("@Filter2",Filter2),
        new SqlParameter("@Filter3",Filter3),
                                   new SqlParameter("@Filter4",Filter4),

};
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string getlogic(string Questionid, string Option, string isflagvalue)
    {
        clsMain objBLL = new clsMain();
        DataTable dt = objBLL.Get_DataFor3Filter("USP_get_QuestionLogic_web", "1296", Questionid, Option);
        if (dt.Rows.Count > 0)
        {
            if (isflagvalue == "Y")
            {
                return dt.Rows[0]["FlagValue"].ToString();
            }
            else
            {
                return dt.Rows[0]["DependentQuestionId"].ToString();
            }
        }
        else
        {
            return Option;
        }
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string getDistrict(string StateID)
    {

        //DataTable dt = bkl.Get_DataFor1Filter("GetDistrict_For_Flood", StateID);
        DataTable dt = null;
        return JsonConvert.SerializeObject(dt);
    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string getBlock(string DistrictID)
    {
        string returndata = "";
        DataTable dt = null;
        DataTable dtP = HttpContext.Current.Session["ParticipartName"] as DataTable;
        string FID = HttpContext.Current.Session["FormID"] as string;
        string ll = "0";

        string ParticiparticipateName = "";
        clsMain objBLL1 = new clsMain();
        if (dtP.Rows.Count > 0)
        {
            DataRow[] dr = dtP.Select("EGcode='" + DistrictID.Trim() + "'");
            if (dr.Length > 0)
            {
                returndata = "Submitted" + "___" + dr[0]["ParticiparticipateID"] + "___" + dr[0]["ParticiparticipateName"];

                dt = objBLL1.Get_DataFor8Filter("rptQuestionvaldation2026", FID, Convert.ToString(dr[0]["ParticiparticipateID"]));
                if (dt.Rows.Count > 0)
                {
                    ll = Convert.ToString(dr[0]["ParticiparticipateID"]);
                    returndata = "Data Already Submitted" + "___" + ll + "___" + dr[0]["ParticiparticipateName"];
                }

            }
            else
            {
                returndata = "Participant not Found" + "___" + ll + "___" + ParticiparticipateName;
            }
        }
        //dt = objBLL1.Get_DataFor8Filter("rptQuestionvaldation2026",  FID, DistrictID);
        //if (dt.Rows.Count>0)
        //{
        //    returndata = "Data Already Submitted";
        //}
        //else
        //{
        //    returndata = "Validation";
        //}

        return returndata;
    }

    #region VIEW Question Response
    public void ViewQuestion(string FormID, int Condition)
    {
        DataTable dt = new DataTable();
        hdnconditions.Value = Condition.ToString();
        dt = Get_DataFor3Filter("USP_GetQuestionFor_View", FormID.ToString(), Session["GUID"].ToString(), Condition.ToString());
        //DataListQuestion.DataSource = dt;
        //DataListQuestion.DataBind();

        //SetSkipLogicAttribute();
    }



    public string gettypeofResponse(string Qtype, string flag, DataTable dtcommon, DataTable dttempcommon, string Questionid, string MaxLength, string QuestionValue)
    {
        //string Ntextboxhtml = "<input type='text' class='form-control' maxlength='" + MaxLength + "' onkeyup='return CheckNumeric(event)' Style='margin-top: 5px' id='" + Questionid + "' name='Numeric' placeholder='Numeric Value' value='" + QuestionValue + "'>";
        //string Stextboxhtml = "<input type='text' class='form-control' maxlength='" + MaxLength + "' onkeyup='CheckSpecial(event)' Style='margin-top: 5px' id='" + Questionid + "' name='Text' placeholder='Text Value' value='" + QuestionValue + "'>";
        //string Dtextboxhtml = "<input type='text' class='form-control mydate' Style='margin-top: 5px' id='" + Questionid + "' name='Date' placeholder='dd/MM/yyyy' value='" + QuestionValue + "'>";
        //string Timetextboxhtml = "<input type='text' class='form-control' Style='margin-top: 5px' id='" + Questionid + "' name='Time' placeholder='hh:mm:ss'>" + QuestionValue + "";
        //string Imagehtml = "<span class='glyphicon glyphicon-picture' Style='margin-top: 5px; font-size:18px'></span>";
        //string FingerPrnthtml = "<img src='images/fingerprint-2-512.png' alt='Finger Print' Style='height:25px; width:25px;'/> ";

        //string Ntextboxpercentage = "<input type='text' class='form-control' maxlength='" + MaxLength + "' onchange='FN14351(" + Questionid + ")' Style='margin-top: 5px' id='" + Questionid + "' name='Numeric' placeholder='Numeric Value' value='" + QuestionValue + "'>";


        //if (Qtype == "1")
        //{
        //    Qtype = Stextboxhtml;
        //}
        //else if (Qtype == "2")
        //{

        //    DataTable dt = Select_All_Data("tblQuestionMapping", "*", "ParentQuestionId = " + Questionid + "", "", "");
        //    //--------- if skiplogic apply
        //    if (dt.Rows.Count > 0)
        //    {
        //        Qtype = Ntextboxpercentage;
        //    }
        //    else
        //    {
        //        Qtype = Ntextboxhtml;
        //    }

        //}
        //else if (Qtype == "6")
        //{
        //    Qtype = Imagehtml;
        //}
        //else if (Qtype == "Finger Print")
        //{
        //    Qtype = FingerPrnthtml;
        //}
        //else if (Qtype == "3")
        //{
        //    Qtype = Dtextboxhtml;
        //}
        //else if (Qtype == "8")
        //{
        //    Qtype = Timetextboxhtml;
        //}
        //else if (Qtype == "4")
        //{
        //    Qtype = "";
        //    DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
        //    if (dr2.Length > 0)
        //        dttempcommon = dr2.CopyToDataTable();


        //    DataTable dt = Select_All_Data("tblQuestionMapping", "*", "ParentQuestionId = " + Questionid + "", "", "");
        //    //--------- if skiplogic apply
        //    if (dt.Rows.Count > 0)
        //    {

        //        for (int i = 0; i < dttempcommon.Rows.Count; i++)
        //        {
        //            QuestionValue = QuestionValue == null ? "9999" : QuestionValue;
        //            if (Convert.ToInt32(dttempcommon.Rows[i]["ID"]) == Convert.ToInt32(QuestionValue))
        //            {
        //                Qtype = Qtype + "<input type='radio' onchange='SetLogic(" + dttempcommon.Rows[i]["ID"] + "," + Questionid + ")' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "' checked><span id='" + Questionid + "_" + dttempcommon.Rows[i]["ID"] + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "</span><br />";
        //            }
        //            else
        //            {
        //                Qtype = Qtype + "<input type='radio' onchange='SetLogic(" + dttempcommon.Rows[i]["ID"] + "," + Questionid + ")' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'><span id='" + Questionid + "_" + dttempcommon.Rows[i]["ID"] + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "</span><br />";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < dttempcommon.Rows.Count; i++)
        //        {
        //            QuestionValue = QuestionValue == null ? "9999" : QuestionValue;
        //            if (Convert.ToInt32(dttempcommon.Rows[i]["ID"]) == Convert.ToInt32(QuestionValue))
        //            {
        //                Qtype = Qtype + "<input type='radio' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "' checked><span id='" + Questionid + "_" + dttempcommon.Rows[i]["ID"] + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "</span><br />";
        //            }
        //            else
        //            {
        //                Qtype = Qtype + "<input type='radio' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'><span id='" + Questionid + "_" + dttempcommon.Rows[i]["ID"] + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "</span><br />";
        //            }
        //        }
        //    }


        //}
        //else if (Qtype == "5")
        //{
        //    Qtype = "";
        //    DataRow[] dr2 = dtcommon.Select("Flag = " + flag);
        //    if (dr2.Length > 0)
        //        dttempcommon = dr2.CopyToDataTable();

        //    for (int i = 0; i < dttempcommon.Rows.Count; i++)
        //    {
        //        QuestionValue = QuestionValue == null ? "9999" : QuestionValue;
        //        if (Convert.ToInt32(dttempcommon.Rows[i]["ID"]) == Convert.ToInt32(QuestionValue))
        //        {
        //            Qtype = Qtype + "<input type='checkbox' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "' checked>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "<br />";
        //        }
        //        else
        //        {
        //            Qtype = Qtype + "<input type='checkbox' value='" + dttempcommon.Rows[i]["ID"] + "' name='" + Questionid + "'>" + dttempcommon.Rows[i]["Value"].ToString().Replace("'", "") + "<br />";
        //        }
        //    }
        //}

        return Qtype;
    }


    #endregion

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string getMonth()
    {
        //BLL bkl = new BLL();
        DataTable dt = null;
        return JsonConvert.SerializeObject(dt);
    }
    public DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName)
                    };


            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
        }
        return dtcombo;
    }

}