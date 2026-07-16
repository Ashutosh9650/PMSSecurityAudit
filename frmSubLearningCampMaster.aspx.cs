using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class frmSubLearningCampMaster : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions = string.Empty, Flag = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["username"]) != "")
            {
                GridBindLearningMasterCamp();
                CampNumber();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
    }


    public void CampNumber()
    {
        DataTable dt = new DataTable();

        dt = GetsubCampNo();
        ddlCampNo.DataSource = dt;
        ddlCampNo.DataValueField = "ID";
        ddlCampNo.DataTextField = "Name";
        ddlCampNo.DataBind();
        ddlCampNo.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void btnAddCamp_Click(object sender, EventArgs e)
    {
        Clear();
        ModalLearningCamp.Show();
    }
    public DataTable GetCampSubExit(int campNo)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
              new SqlParameter("@CampNo",  campNo),
            };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetSubCampExit", parm);
        return dt;
    }
    public DataTable GridBindSubLearningMasterCamp()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetBindSubLearningMasterCamp");
        return dt;
    }
    public DataTable GetsubCampNo()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetsubCampNo]");
        return dt;
    }
    public void Clear()
    {
        ddlCampNo.SelectedIndex = 0;
        txtCampDurationInWeek.Text = string.Empty;
        txtSessioninCamp.Text = string.Empty;
        txtSessioninWeek.Text = string.Empty;
        txtHindiBaselineSessionNo.Text = string.Empty;
        txtHindiEndlineSessionNo.Text = string.Empty;
        txtMathBaselineSessionNo.Text = string.Empty;
        txtMathEndlineSessionNo.Text = string.Empty;
        txtHindiBaselineHeading1.Text = string.Empty;
        txtHindiBaselineHeading2.Text = string.Empty;
        txtMathBaselineHeading1.Text = string.Empty;
        txtMathBaselineHeading2.Text = string.Empty;
        txtHindiBaselineEndlineMaxScore.Text = string.Empty;
        txtMathBaselineEndlineMaxScore.Text = string.Empty;
        txtHindiBaselineEndlineHeading2Active.Text = string.Empty;
        txtMathBaselineEndlineHeading2Active.Text = string.Empty;
        txtHindiBaselineEndlineMaxScore1.Text = string.Empty;
        txtMathBaselineEndlineMaxScore1.Text = string.Empty;
        txtHindiBaselineHeading4.Text = string.Empty; txtMathBaselineHeading4.Text = string.Empty;
        txtHindiBaselineEndlineMaxHinidiScore3.Text = string.Empty; txtHindiBaselineEndlineMaxScore3.Text = string.Empty;
        txtMathBaselineEndlineHeading4Active.Text = string.Empty;
        txtHindiBaselineEndlineHeading4Active.Text = string.Empty;
        txtMathBaselineHeading3.Text = string.Empty;
        txtHindiBaselineHeading3.Text = string.Empty;
        txtHindiBaselineEndlineMaxScore2.Text = string.Empty;
        txtHindiBaselineEndlineMaxScore22.Text = string.Empty;
        txtHindiBaselineEndlineHeading3Active.Text = string.Empty;
        txtMathBaselineEndlineHeading3Active.Text = string.Empty;
        btnSave.Text = "Save";

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
        ModalLearningCamp.Show();
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
        try
        {
            string UserName = Session["username"].ToString();
            int CampNo = 0; int CampId = 0;
            string CampDurationInWeek, HindiBaselineEndlineMaxScore1, MathBaselineEndlineMaxScore1, SessioninCamp, SessioninWeek, HindiBaselineSessionNo, HindiEndlineSessionNo, MathBaselineSessionNo, MathEndlineSessionNo, HindiBaselineHeading1, HindiBaselineHeading2, MathBaselineHeading1, MathBaselineHeading2, HindiBaselineEndlineMaxScore, MathBaselineEndlineMaxScore, HindiBaselineEndlineHeading2Active, MathBaselineEndlineHeading2Active, HindiBaselineHeading3Name, MathBaselineHeading3Name, HindiBaselineEndlineHeading3Active, MathBaselineEndlineHeading3Active, HindiBaselineEndlineMaxScore2, MathBaselineEndlineMaxScore2, operation = string.Empty;

            if (hdnCampID.Value == "")
            {
                CampId = 0;

            }
            else
            {

                CampId = Convert.ToInt32(hdnCampID.Value);
            }

            CampNo = Convert.ToInt32(ddlCampNo.SelectedValue);
            CampDurationInWeek = txtCampDurationInWeek.Text;
            SessioninCamp = txtSessioninCamp.Text;
            SessioninWeek = txtSessioninWeek.Text;
            HindiBaselineSessionNo = txtHindiBaselineSessionNo.Text;
            HindiEndlineSessionNo = txtHindiEndlineSessionNo.Text;
            MathBaselineSessionNo = txtMathBaselineSessionNo.Text;
            MathEndlineSessionNo = txtMathEndlineSessionNo.Text;
            HindiBaselineHeading1 = txtHindiBaselineHeading1.Text;
            HindiBaselineHeading2 = txtHindiBaselineHeading2.Text;
            MathBaselineHeading1 = txtMathBaselineHeading1.Text;
            MathBaselineHeading2 = txtMathBaselineHeading2.Text;
            HindiBaselineEndlineMaxScore = txtHindiBaselineEndlineMaxScore.Text;
            MathBaselineEndlineMaxScore = txtMathBaselineEndlineMaxScore.Text;
            HindiBaselineEndlineHeading2Active = txtHindiBaselineEndlineHeading2Active.Text;
            MathBaselineEndlineHeading2Active = txtMathBaselineEndlineHeading2Active.Text;

            HindiBaselineEndlineMaxScore1 = txtHindiBaselineEndlineMaxScore1.Text;
            MathBaselineEndlineMaxScore1 = txtMathBaselineEndlineMaxScore1.Text;


            HindiBaselineHeading3Name = txtHindiBaselineHeading3.Text;
            MathBaselineHeading3Name = txtMathBaselineHeading3.Text;
            HindiBaselineEndlineHeading3Active = txtHindiBaselineEndlineHeading3Active.Text;
            MathBaselineEndlineHeading3Active = txtMathBaselineEndlineHeading3Active.Text;
            HindiBaselineEndlineMaxScore2 = txtHindiBaselineEndlineMaxScore2.Text;
            MathBaselineEndlineMaxScore2 = txtHindiBaselineEndlineMaxScore22.Text;




            if (CampNo != 0)
            {

                if (btnSave.Text == "Update") //Update
                {
                    operation = "U";

                    int Result = InsertUpdateSubLearningCampMaster(CampId, CampNo, CampDurationInWeek, SessioninCamp, SessioninWeek, HindiBaselineSessionNo, HindiEndlineSessionNo, MathBaselineSessionNo, MathEndlineSessionNo, HindiBaselineHeading1, HindiBaselineHeading2, MathBaselineHeading1, MathBaselineHeading2, HindiBaselineEndlineMaxScore, MathBaselineEndlineMaxScore, HindiBaselineEndlineHeading2Active, MathBaselineEndlineHeading2Active, UserName, operation, HindiBaselineEndlineMaxScore1, MathBaselineEndlineMaxScore1, HindiBaselineHeading3Name, MathBaselineHeading3Name, HindiBaselineEndlineHeading3Active, MathBaselineEndlineHeading3Active, HindiBaselineEndlineMaxScore2, MathBaselineEndlineMaxScore2, txtHindiBaselineHeading4.Text, txtMathBaselineHeading4.Text, txtHindiBaselineEndlineMaxHinidiScore3.Text, txtHindiBaselineEndlineMaxScore3.Text, txtMathBaselineEndlineHeading4Active.Text, txtHindiBaselineEndlineHeading4Active.Text);

                    if (Result > 0)
                    {
                        Clear();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Sucessfully')</script>", false);
                        GridBindLearningMasterCamp();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                    }


                }
                else // Insert
                {
                    operation = "I";

                    DataTable dt = new DataTable();

                    dt = GetCampSubExit(CampNo);

                    if (dt.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Camp Number Already Exit,Please Select Another Camp Number')</script>", false);
                        ModalLearningCamp.Show();
                    }
                    else
                    {
                        int Result = InsertUpdateSubLearningCampMaster(CampId, CampNo, CampDurationInWeek, SessioninCamp, SessioninWeek, HindiBaselineSessionNo, HindiEndlineSessionNo, MathBaselineSessionNo, MathEndlineSessionNo, HindiBaselineHeading1, HindiBaselineHeading2, MathBaselineHeading1, MathBaselineHeading2, HindiBaselineEndlineMaxScore, MathBaselineEndlineMaxScore, HindiBaselineEndlineHeading2Active, MathBaselineEndlineHeading2Active, UserName, operation, HindiBaselineEndlineMaxScore1, MathBaselineEndlineMaxScore1, HindiBaselineHeading3Name, MathBaselineHeading3Name, HindiBaselineEndlineHeading3Active, MathBaselineEndlineHeading3Active, HindiBaselineEndlineMaxScore2, MathBaselineEndlineMaxScore2, txtHindiBaselineHeading4.Text, txtMathBaselineHeading4.Text, txtHindiBaselineEndlineMaxHinidiScore3.Text, txtHindiBaselineEndlineMaxScore3.Text, txtMathBaselineEndlineHeading4Active.Text, txtHindiBaselineEndlineHeading4Active.Text);

                        if (Result > 0)
                        {
                            Clear();
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                            GridBindLearningMasterCamp();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Unsuccesfull')</script>", false);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Camp Number')</script>", false);
            }



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //public int InsertUpdateLearningCampMaster(int CampId, int campNo, string campDurationInWeek, string sessioninCamp, string sessioninWeek, string hindiBaselineSessionNo, string hindiEndlineSessionNo, string mathBaselineSessionNo, string mathEndlineSessionNo, string hindiBaselineHeading1, string hindiBaselineHeading2, string mathBaselineHeading1, string mathBaselineHeading2, string hindiBaselineEndlineMaxScore, string mathBaselineEndlineMaxScore, string hindiBaselineEndlineHeading2Active, string mathBaselineEndlineHeading2Active, string userName, string operation,string  HindiBaselineEndlineMaxScore1, string MathBaselineEndlineMaxScore1,string HindiBaselineHeading3Name, string MathBaselineHeading3Name,string HindiBaselineEndlineHeading3Active,string MathBaselineEndlineHeading3Active,string HindiBaselineEndlineMaxScore2, string MathBaselineEndlineMaxScore2,string hh4,string MM4,string hhs4,string mms4,string ahs4,string ams4)
    //{
    //    SqlParameter[] cmdParameters = new SqlParameter[]
    //    {
    //                new SqlParameter("@CampId", CampId),
    //                new SqlParameter("@campNo", campNo),
    //                new SqlParameter("@campDurationInWeek", campDurationInWeek),
    //                new SqlParameter("@sessioninCamp", sessioninCamp),
    //                new SqlParameter("@sessioninWeek", sessioninWeek),
    //                new SqlParameter("@hindiBaselineSessionNo", hindiBaselineSessionNo),
    //                new SqlParameter("@hindiEndlineSessionNo", hindiEndlineSessionNo),
    //                new SqlParameter("@mathBaselineSessionNo", mathBaselineSessionNo),
    //                new SqlParameter("@mathEndlineSessionNo", mathEndlineSessionNo),
    //                new SqlParameter("@hindiBaselineHeading1", hindiBaselineHeading1),
    //                new SqlParameter("@hindiBaselineHeading2", hindiBaselineHeading2),
    //                new SqlParameter("@mathBaselineHeading1", mathBaselineHeading1),
    //                new SqlParameter("@mathBaselineHeading2", mathBaselineHeading2),
    //                new SqlParameter("@hindiBaselineEndlineMaxScore", hindiBaselineEndlineMaxScore),
    //                new SqlParameter("@mathBaselineEndlineMaxScore", mathBaselineEndlineMaxScore),
    //                new SqlParameter("@hindiBaselineEndlineHeading2Active", hindiBaselineEndlineHeading2Active),
    //                new SqlParameter("@mathBaselineEndlineHeading2Active", mathBaselineEndlineHeading2Active),
    //                new SqlParameter("@userName", userName),
    //                new SqlParameter("@operation", operation),
    //                 new SqlParameter("@HindiBaselineEndlineMaxScore1", HindiBaselineEndlineMaxScore1),
    //                new SqlParameter("@MathBaselineEndlineMaxScore1", MathBaselineEndlineMaxScore1),

    //                 new SqlParameter("@HindiBaselineHeading3Name", HindiBaselineHeading3Name),
    //                    new SqlParameter("@MathBaselineHeading3Name", MathBaselineHeading3Name),
    //                      new SqlParameter("@HindiBaselineEndlineHeading3Active", HindiBaselineEndlineHeading3Active),
    //                       new SqlParameter("@MathBaselineEndlineHeading3Active", MathBaselineEndlineHeading3Active),
    //                       new SqlParameter("@HindiBaselineEndlineMaxScore2", HindiBaselineEndlineMaxScore2),
    //                          new SqlParameter("@MathBaselineEndlineMaxScore2", MathBaselineEndlineMaxScore2),

    //                           new SqlParameter("@HindiBaselineHeading4Name", hh4),
    //                    new SqlParameter("@MathBaselineHeading4Name", MM4),
    //                      new SqlParameter("@HindiBaselineEndlineMaxScore4", hhs4),
    //                       new SqlParameter("@MathBaselineEndlineMaxScore4", mms4),
    //                       new SqlParameter("@MathBaselineEndlineHeading4Active", ahs4),
    //                          new SqlParameter("@HindiBaselineEndlineHeading4Active", ams4),



    //};
    //    return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateLearningCampMaster", cmdParameters);
    //}

    #region Anuj
    public int InsertUpdateSubLearningCampMaster(int CampId, int campNo, string campDurationInWeek, string sessioninCamp, string sessioninWeek, string hindiBaselineSessionNo, string hindiEndlineSessionNo, string mathBaselineSessionNo, string mathEndlineSessionNo, string hindiBaselineHeading1, string hindiBaselineHeading2, string mathBaselineHeading1, string mathBaselineHeading2, string hindiBaselineEndlineMaxScore, string mathBaselineEndlineMaxScore, string hindiBaselineEndlineHeading2Active, string mathBaselineEndlineHeading2Active, string userName, string operation, string HindiBaselineEndlineMaxScore1, string MathBaselineEndlineMaxScore1, string HindiBaselineHeading3Name, string MathBaselineHeading3Name, string HindiBaselineEndlineHeading3Active, string MathBaselineEndlineHeading3Active, string HindiBaselineEndlineMaxScore2, string MathBaselineEndlineMaxScore2, string hh4, string MM4, string hhs4, string mms4, string ahs4, string ams4)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@CampId", CampId),
                    new SqlParameter("@campNo", campNo),
                    new SqlParameter("@campDurationInWeek", campDurationInWeek),
                    new SqlParameter("@sessioninCamp", sessioninCamp),
                    new SqlParameter("@sessioninWeek", sessioninWeek),
                    new SqlParameter("@hindiBaselineSessionNo", hindiBaselineSessionNo),
                    new SqlParameter("@hindiEndlineSessionNo", hindiEndlineSessionNo),
                    new SqlParameter("@mathBaselineSessionNo", mathBaselineSessionNo),
                    new SqlParameter("@mathEndlineSessionNo", mathEndlineSessionNo),
                    new SqlParameter("@hindiBaselineHeading1", hindiBaselineHeading1),
                    new SqlParameter("@hindiBaselineHeading2", hindiBaselineHeading2),
                    new SqlParameter("@mathBaselineHeading1", mathBaselineHeading1),
                    new SqlParameter("@mathBaselineHeading2", mathBaselineHeading2),
                    new SqlParameter("@hindiBaselineEndlineMaxScore", hindiBaselineEndlineMaxScore),
                    new SqlParameter("@mathBaselineEndlineMaxScore", mathBaselineEndlineMaxScore),
                    new SqlParameter("@hindiBaselineEndlineHeading2Active", hindiBaselineEndlineHeading2Active),
                    new SqlParameter("@mathBaselineEndlineHeading2Active", mathBaselineEndlineHeading2Active),
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@operation", operation),
                     new SqlParameter("@HindiBaselineEndlineMaxScore1", HindiBaselineEndlineMaxScore1),
                    new SqlParameter("@MathBaselineEndlineMaxScore1", MathBaselineEndlineMaxScore1),

                     new SqlParameter("@HindiBaselineHeading3Name", HindiBaselineHeading3Name),
                        new SqlParameter("@MathBaselineHeading3Name", MathBaselineHeading3Name),
                          new SqlParameter("@HindiBaselineEndlineHeading3Active", HindiBaselineEndlineHeading3Active),
                           new SqlParameter("@MathBaselineEndlineHeading3Active", MathBaselineEndlineHeading3Active),
                           new SqlParameter("@HindiBaselineEndlineMaxScore2", HindiBaselineEndlineMaxScore2),
                              new SqlParameter("@MathBaselineEndlineMaxScore2", MathBaselineEndlineMaxScore2),

                               new SqlParameter("@HindiBaselineHeading4Name", hh4),
                        new SqlParameter("@MathBaselineHeading4Name", MM4),
                          new SqlParameter("@HindiBaselineEndlineMaxScore4", hhs4),
                           new SqlParameter("@MathBaselineEndlineMaxScore4", mms4),
                           new SqlParameter("@MathBaselineEndlineHeading4Active", ahs4),
                              new SqlParameter("@HindiBaselineEndlineHeading4Active", ams4),

                              
    
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSubLearningCampMaster", cmdParameters);
    }
    #endregion

    public void GridBindLearningMasterCamp()
    {
        DataTable dt = new DataTable();
        dt = GridBindSubLearningMasterCamp();
        Session["dtGridLearningCampMaster"] = dt;
        GridLearningCampMaster.DataSource = (DataTable)dt;
        GridLearningCampMaster.DataBind();
    }

    protected void GridLearningCampMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridLearningCampMaster.DataSource = (DataTable)Session["dtGridLearningCampMaster"];
            GridLearningCampMaster.PageIndex = e.NewPageIndex;
            GridLearningCampMaster.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GridLearningCampMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("EditData"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                hdnCampID.Value = this.GridLearningCampMaster.DataKeys[index]["CampID"].ToString();
                ddlCampNo.SelectedValue = this.GridLearningCampMaster.DataKeys[index]["CampNumber"].ToString();
                txtCampDurationInWeek.Text = this.GridLearningCampMaster.DataKeys[index]["CampDurationInWeek"].ToString();
                txtSessioninCamp.Text = this.GridLearningCampMaster.DataKeys[index]["SessionInCamp"].ToString();
                txtSessioninWeek.Text = this.GridLearningCampMaster.DataKeys[index]["SessionInWeek"].ToString();
                txtHindiBaselineSessionNo.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineSessionNo"].ToString();
                txtHindiEndlineSessionNo.Text = this.GridLearningCampMaster.DataKeys[index]["HindiEndSessionNo"].ToString();
                txtMathBaselineSessionNo.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineSessionNo"].ToString();
                txtMathEndlineSessionNo.Text = this.GridLearningCampMaster.DataKeys[index]["MathEndSessionNo"].ToString();
                txtHindiBaselineHeading1.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineHeading1Name"].ToString();
                txtHindiBaselineHeading2.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineHeading2Name"].ToString();
                txtMathBaselineHeading1.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineHeading1Name"].ToString();
                txtMathBaselineHeading2.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineHeading2Name"].ToString();
                txtHindiBaselineEndlineMaxScore.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineMaxScore"].ToString();
                txtMathBaselineEndlineMaxScore.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineMaxScore"].ToString();
                txtHindiBaselineEndlineHeading2Active.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineHeading2Active"].ToString();
                txtMathBaselineEndlineHeading2Active.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineHeading2Active"].ToString();

                txtHindiBaselineEndlineMaxScore1.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineMaxScore1"].ToString(); ;
                txtMathBaselineEndlineMaxScore1.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineMaxScore1"].ToString();

                txtHindiBaselineHeading3.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineHeading3Name"].ToString();
                txtMathBaselineHeading3.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineHeading3Name"].ToString();

                txtHindiBaselineEndlineMaxScore2.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineMaxScore2"].ToString();
                txtHindiBaselineEndlineMaxScore22.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineMaxScore2"].ToString();
                txtHindiBaselineEndlineHeading3Active.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineHeading3Active"].ToString();


                txtMathBaselineEndlineHeading3Active.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineHeading3Active"].ToString();


                txtHindiBaselineHeading4.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineHeading4Name"].ToString();
                txtMathBaselineHeading4.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineHeading4Name"].ToString();
                txtHindiBaselineEndlineMaxHinidiScore3.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineMaxScore4"].ToString();
                txtHindiBaselineEndlineMaxScore3.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineMaxScore4"].ToString();
                txtHindiBaselineEndlineHeading4Active.Text = this.GridLearningCampMaster.DataKeys[index]["HindiBaselineEndlineHeading4Active"].ToString();
                txtMathBaselineEndlineHeading4Active.Text = this.GridLearningCampMaster.DataKeys[index]["MathBaselineEndlineHeading4Active"].ToString();

                btnSave.Text = "Update";
                ModalLearningCamp.Show();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        if (e.CommandName.Equals("DelefffteData"))
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                hdnCampID.Value = GridLearningCampMaster.DataKeys[index]["CampID"].ToString();
                int CampID = Convert.ToInt32(hdnCampID.Value);
                string operation = "D";
                string UserName = Session["username"].ToString();
                int Result = objMain.InsertUpdateLearningCampMaster(CampID, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UserName, operation);

                if (Result > 0)
                {
                    if (operation == "D")
                    {
                        Clear();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Learning Camp Master Data Deleted sucessfully')</script>", false);
                        GridBindLearningMasterCamp();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("Label2") as Label).Text;


        hdnCampID.Value = UniqueChildCode;
        int CampID = Convert.ToInt32(hdnCampID.Value);
        string operation = "D";
        string UserName = Session["username"].ToString();
        int Result = InsertUpdateSubLearningCampMaster(CampID, 0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", UserName, operation, "", "", "", "", "", "", "", "", "", "", "", "", "", "");

        if (Result > 0)
        {
            if (operation == "D")
            {
                Clear();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Learning Camp Master Data Deleted sucessfully')</script>", false);
                GridBindLearningMasterCamp();
            }
        }


    }
    protected void btnReprot_Click(object sender, EventArgs e)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@ffh",""),
            
            
		};
        DataTable dt = null;


        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetBindSubLearningMasterCampRport]", cmdParameters);

        if (dt.Rows.Count > 0)
        {
            // ExporttoExcel(dt, "LearningCampMaster");
            ExportToExcelNew(dt, "LearningCampMaster");
        }

    }
    public void ExportToExcelNew(DataTable dt, string FileName)
    {
        Response.ClearContent();
        Response.Buffer = true;
        string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", Fullfilename));
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = System.Text.Encoding.Unicode;
        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        DataTable dt1 = dt;
        string str = string.Empty;
        foreach (DataColumn dtcol in dt1.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt1.Rows)
        {
            str = "";
            for (int j = 0; j < dt1.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
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
                Response.ContentEncoding = System.Text.Encoding.UTF8;
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
        catch (Exception ex)
        {

            throw;
        }
    }
}