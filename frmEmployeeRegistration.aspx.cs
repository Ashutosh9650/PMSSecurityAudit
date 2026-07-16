using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Frm_frmEmployeeRegistration : System.Web.UI.Page
{
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
            if (!IsPostBack)
            {
                fillleftgrid();


                fillrole();
                txtEmpCode.Text = "";



                txtEmpCode.Enabled = false;
                txtFristName.Enabled = false;

                ddllevel.Enabled = false;

            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }



    //ddlemployee


    public void fillrole()
    {
        string cond = "";


        if (Session["user_level"].ToString() == "1")
        {


        }
        else
        {
            cond = "Role_Level not in(1)";
        }




        DataTable dtrole = Select_All_Data("mstuserrole", "*", cond, "Role_id", "");
        //DataTable dtrole = Select_All_Data("mstuserrole", "*", "", "", "");
        if (dtrole.Rows.Count > 0)
        {
            ddllevel.DataSource = dtrole;
            ddllevel.DataTextField = "Role";
            ddllevel.DataValueField = "Role_Level";
            ddllevel.DataBind();
            ddllevel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }

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
            SqlParameter[] paramv = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
            throw;
        }
        return dtcombo;
    }




    public void fillleftgrid()
    {

        string Condtion = "";

        //if (Session["user_level"].ToString() == "79")
        //{
        //    Condtion = "EmployeeType=24  ";

        //}

        //Condtion = "  EmployeeType in('1','2','3','4','5','7','8')";
        DataTable dtEmp = Select_All_Data("tblemployeedetails", "top 50 *", Condtion, "EmployeeID", "desc");
        if (dtEmp.Rows.Count > 0)
        {
            dgvleftgrid.DataSource = dtEmp;
            dgvleftgrid.DataBind();
            ViewState["Serach"] = dtEmp;
        }
        else
        {
            ViewState["Serach"] = null;
            dgvleftgrid.DataSource = null;
            dgvleftgrid.DataBind();
        }
    }
    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgvleftgrid.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            dgvleftgrid.DataSource = dt;
            dgvleftgrid.DataBind();
        }

    }
    protected void dgvleftgrid_rowcommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "Show")
            {
                ViewState["Flag"] = "U";
                int iIndex = Convert.ToInt32(e.CommandArgument);
                string id = (dgvleftgrid.DataKeys[iIndex]["EmployeeID"].ToString());
                ViewState["id"] = id;
                fillcontrols(id);





                txtEmpCode.Enabled = false;
                txtFristName.Enabled = true;

                ddllevel.Enabled = true;
                //hdnGranteeStatus.Value = "Update";

            }
        }
        catch
        {
            throw;
        }
    }

    protected void fillcontrols(string UserID)
    {
        string condition = "";
        condition = "EmployeeID='" + UserID + "'";
        DataTable dt = Select_All_Data("tblemployeedetails", "*", condition, "", "");
        if (dt.Rows.Count > 0)
        {
            txtEmpCode.Text = dt.Rows[0]["EmployeeId"].ToString();
            //if (dt.Rows[0]["Staffid"].ToString() != "")
            //{
            //    ddlemployee.SelectedValue = dt.Rows[0]["Staffid"].ToString();
            //}

            //else
            //{
            //    ddlemployee.SelectedIndex = -1;
            //}




            if (dt.Rows[0]["EmployeeType"].ToString() != "")
            {
                ddllevel.SelectedValue = dt.Rows[0]["EmployeeType"].ToString();
            }

            else
            {
                ddllevel.SelectedIndex = -1;
            }
            if (dt.Rows[0]["EmpImageName"].ToString() != "")
            {
                //string sFileDir = Server.MapPath(Comman.GetImagePath("ImgPage") + dtmstM.Rows[0]["ImagePath"].ToString().Trim() + "");
                //string sFileDir = Request.PhysicalApplicationPath + "images\\";
                string imagename = dt.Rows[0]["EmpImageName"].ToString().Trim();
                ViewState["ImagePath"] = imagename;
                imgMKS.ImageUrl = ResolveUrl("~/EmpImg/" + imagename);
            }
            else
            {
                ViewState["ImagePath"] = "";

                imgMKS.ImageUrl = null;
            }

            DateTime DateJoing = Convert.ToDateTime(dt.Rows[0]["DateJoined"].ToString());
            txtJoingDate.Text = DateJoing.ToString("dd/MM/yyy");

            if (Convert.ToString(dt.Rows[0]["DateofBirth"].ToString()) == "01/01/1900 00:00:00" || Convert.ToString(dt.Rows[0]["DateofBirth"].ToString()) == "")
            {
                txtBirth.Text = "";
            }
            else
            {
                DateTime BDate = Convert.ToDateTime(dt.Rows[0]["DateofBirth"].ToString());
                txtBirth.Text = BDate.ToString("dd/MM/yyy");
            }

            txtFristName.Text = dt.Rows[0]["Firstname"].ToString();


            //txtHindi.Text = dt.Rows[0]["NameInHindi"].ToString();
            txtEmail.Text = dt.Rows[0]["EmaillID"].ToString();
            txtMobile.Text = dt.Rows[0]["MobileNo"].ToString();
            //txtAddress.Text = dt.Rows[0]["PostalAddress"].ToString();

            ddlGender.SelectedValue = dt.Rows[0]["Gender"].ToString().Trim();
        }

    }

    protected void btn_Add_click(object sender, EventArgs e)
    {
        ddllevel.SelectedIndex = -1;
        //ddldistrict.SelectedIndex = -1;
        //ddlblbock.SelectedIndex = -1;

        txtEmpCode.Text = "";
        txtFristName.Text = "";


        ViewState["Flag"] = "I";
        ViewState["id"] = DBNull.Value;

        ddlGender.SelectedIndex = 0;
        txtJoingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txtBirth.Text = "";
        txtEmpCode.Enabled = true;
        txtFristName.Enabled = true;
        ViewState["ImagePath"] = "";
        ddllevel.Enabled = true;

    }

    private string GetCheckBoxListSelection(CheckBoxList chbx)
    {
        string[] cblItems;
        ArrayList cblSelections = new ArrayList();
        string a = "";

        foreach (ListItem item in chbx.Items)
        {
            if (item.Selected)
            {
                cblSelections.Add(item.Value);
                a += "'" + item.Value + "'" + ",";
            }
        }
        return a;
        //cblItems =(string[])cblSelections.ToArray(typeof(string));
        //return string.Join(",", cblItems);
    }

    public static byte[] HashPassword(string password)
    {
        var provider = new SHA1CryptoServiceProvider();
        var encoding = new UnicodeEncoding();
        return provider.ComputeHash(encoding.GetBytes(password));
    }

    public void CreateDataTableUserDetails()
    {

        dtUserDeatils = new DataTable();

        dtUserDeatils.Columns.Add(new DataColumn("UserID", System.Type.GetType("System.Int32")));
        dtUserDeatils.Columns.Add(new DataColumn("Statecode", System.Type.GetType("System.String")));
        dtUserDeatils.Columns.Add(new DataColumn("DistrictCode", System.Type.GetType("System.String")));
        dtUserDeatils.Columns.Add(new DataColumn("BlockCode", System.Type.GetType("System.String")));
        dtUserDeatils.Columns.Add(new DataColumn("Villagecode", System.Type.GetType("System.String")));

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string userlevel = "", statecode = "", districtcode = "", blockcode = "", villagecode = "", FristName = "", LastName = "", cpw = "", staffid = "";
        Int32 Gender = 0;

        if (ddllevel.SelectedIndex > 0)
        {
            userlevel = ddllevel.SelectedValue.ToString();
        }

        string BirhtDate = "";

        if (txtBirth.Text == "")
        {


        }
        else
        {
            string Bdate = txtBirth.Text;
            string[] D = Bdate.Split('/');
            BirhtDate = D[2] + '-' + D[1] + '-' + D[0];

        }

        string jdate = txtJoingDate.Text;
        string[] b = jdate.Split('/');
        string JoingDate = b[2] + '-' + b[1] + '-' + b[0];



        if ((txtFristName.Text != null) || (txtFristName.Text != ""))
        {
            FristName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtFristName.Text);

        }
        string Fullfilename = "";
        if (Convert.ToString(ViewState["ImagePath"]).Length > 10)
        {
            Fullfilename = Convert.ToString(ViewState["ImagePath"]);
        }
        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 102400)
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
            Fullfilename = "" + txtEmpCode.Text + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
        }


        string sFileDir = Server.MapPath(Comman.GetImagePath("EmpImg") + "/");

        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
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

        SqlParameter[] parm = new SqlParameter[]
            {


            new SqlParameter("@EmpCode", txtEmpCode.Text.Trim()),
            new SqlParameter("@EmployeeType", userlevel),
            new SqlParameter("@Firstname",FristName.Trim() ),
            new SqlParameter("@Lastname", LastName),

            new SqlParameter("@EmaillID", txtEmail.Text),
            new SqlParameter("@MobileNo", txtMobile.Text),
            new SqlParameter("@PostalAddress", ""),

                  new SqlParameter("@District", districtcode),
                     new SqlParameter("@block", blockcode),
                 new SqlParameter("@Pincode", ""),
                      new SqlParameter("@JoingDate",JoingDate),
                        new SqlParameter("@BirthDay",BirhtDate),
                          new SqlParameter("@Gender",ddlGender.SelectedValue),
             new SqlParameter("@flag", ViewState["Flag"].ToString()),
              new SqlParameter("@EmpImageName", Fullfilename),


              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_insert_update_employeedetails2026", parm);



        if (result > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);

            fillleftgrid();



        }



    }
    //protected void Txtuser_TextChanged(object sender, EventArgs e)
    //{
    //    UniquUserName(txtEmpCode.Text);
    //}
    [System.Web.Services.WebMethod]
    public static string CheckUserID(string useroremail)
    {
        string retval = "";

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@UserName", useroremail.Trim())
        };
        using (DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "checkEmployeeCoderAvailability", p))


            if (dt.Rows.Count > 0)
            {
                retval = "true";

            }
            else
            {

                retval = "false";
            }

        return retval;

    }

    private void UniquUserName(string username)
    {
        try
        {
            SqlParameter[] pa = new SqlParameter[]
     {
     new SqlParameter("@UserName", username ),

     };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "checkEmployeeCoderAvailability", pa);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Employee Code Already Exist!.')</script>", false);
                txtEmpCode.Text = "";
            }

        }
        catch
        {
            throw;
        }


    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {
        FillGridNew();
    }
    public DataTable Select_All_DataNew(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? "  " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramv = new SqlParameter[]
                   {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                   };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramv);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
            throw;
        }
        return dtcombo;
    }

    public void FillGridNew()
    {
        try
        {
            string conditions = "where 1=1";
            string conditionsCLuster = "";

            if (ddlType.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    conditions = conditions + " and Username like '" + txtSearchUser.Text + "%'";
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    conditions = conditions + " and Firstname like '" + txtSearchUser.Text + "%'";
                }
            }


            DataTable dtuser = null;



            dtuser = Select_All_DataNew("tblemployeedetails", "*", conditions, "EmployeeID", "ASC");




            if (dtuser.Rows.Count > 0)
            {
                dgvleftgrid.DataSource = dtuser;
                dgvleftgrid.DataBind();
                ViewState["Serach"] = dtuser;
            }
            else
            {
                dgvleftgrid.DataSource = null;
                dgvleftgrid.DataBind();
                ViewState["Serach"] = null;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
}