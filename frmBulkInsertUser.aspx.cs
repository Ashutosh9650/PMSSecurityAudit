using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmBulkInsertUser : System.Web.UI.Page
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


                //GVCluster.DataSource = null;
                //GVCluster.DataBind();
                LoadDataDist();

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

    }

    public void LoadDataDist()
    {
        SqlParameter[] par1 = new SqlParameter[]
                {

                      new SqlParameter("@Flag", "1" ),


                };
        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadTempUserMasterDistrict", par1);

        DataTable dtDist = ds.Tables[0];
        if (dtDist.Rows.Count > 0)
        {
            objComman.BindDLLDatatableV("mst2District", dtDist, "District,dbo.TitleCase(upper(District)) as District", conditions, "District", "asc", ddlDistrict, "District", "District", "--Select--");


        }
        DataTable dtDesignation = ds.Tables[1];

        if (dtDesignation.Rows.Count > 0)
        {
            objComman.BindDLLDatatableV("mst2District", dtDesignation, "Designation as Designation,dbo.TitleCase(upper(Designation)) as Designation", conditions, "Designation", "asc", ddlDesignation, "Designation", "Designation", "--Select--");


        }

    }
    protected void gvD2d_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVVillage.PageIndex = e.NewPageIndex;
        if (ViewState["dtvillage"] != null)
        {
            DataTable dt = ViewState["dtvillage"] as DataTable;
            GVVillage.DataSource = dt;
            GVVillage.DataBind();

        }
    }
    protected void btnSerach_Click(object sender, EventArgs e)
    {

        if (ddlType.SelectedIndex <= 0)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Type')</script>", false);
            return;
        }


        FillGrid();
    }

    //public void SaveData()
    //{
    //    Int32 icount = 0;
    //    if (Convert.ToInt32(ddlType.SelectedValue) == 4)
    //    {
    //        foreach (GridViewRow Itemst in GVVillage.Rows)
    //        {
    //            #region SaveData
    //            Label EGBlock = Itemst.FindControl("EGBlock") as Label;
    //            DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

    //            Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
    //            //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;


    //            Label lblPanchayatCode = Itemst.FindControl("lblPanchayatCode") as Label;
    //            DropDownList ddlPanchayat = Itemst.FindControl("ddlPanchayat") as DropDownList;

    //              TextBox lblVillageName = Itemst.FindControl("lblVillageName") as TextBox;
    //              TextBox lblVillageCode = Itemst.FindControl("lblVillageCode") as TextBox;
    //              Label lblUniqueVillageName = Itemst.FindControl("lblUniqueVillageName") as Label;
    //              Label lblUniqueVillageCode = Itemst.FindControl("lblUniqueVillageCode") as Label;

    //              Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

    //             string VillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageName.Text.Trim());

    //              string VillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageCode.Text.Trim());

    //            string UniqueVillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageName.Text.Trim());
    //             string UniqueVillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageCode.Text.Trim());
    //             string msg = "";
    //             if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || lblPanchayatCode.Text != ddlPanchayat.SelectedValue.ToString() || VillageName != UniqueVillageName || VillageCode != UniqueVillageCode)
    //             {


    //                         SqlParameter[] par1 = new SqlParameter[]
    //                    {
    //                          new SqlParameter("@BlockCode",  ddlGBlockName.SelectedValue),
    //                       new SqlParameter("@ClusterCode",  ""),
    //                       new SqlParameter("@Pchy",  ddlPanchayat.SelectedValue),
    //                        new SqlParameter("@PchyName",  ""),
    //                       new SqlParameter("@villagecode",  VillageCode),
    //                       new SqlParameter("@VillageName",  VillageName),
    //                        new SqlParameter("@OldVillageCode",  UniqueVillageCode),
    //                         new SqlParameter("@UnqId",  lblUniqueCode.Text),
    //                           new SqlParameter("@flag",  1),



    //                    };

    //                         icount= SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);




    //             }
    //            #endregion
    //        }

    //        if (icount > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
    //            btnSerach_Click(btnSerach, null);
    //        }
    //    }
    //    if (Convert.ToInt32(ddlType.SelectedValue) == 3)
    //    {
    //        #region 3
    //        foreach (GridViewRow Itemst in GVBlock.Rows)
    //        {
    //            TextBox EGBlock = Itemst.FindControl("lblBlockCode") as TextBox;
    //            DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;
    //            DropDownList ddlMainBlockName = Itemst.FindControl("ddlMainBlockName") as DropDownList;
    //            Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

    //            string msg = "";
    //            if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() )
    //            {

    //                SqlParameter[] par1 = new SqlParameter[]
    //                    {
    //                          new SqlParameter("@BlockCode",  ddlGBlockName.SelectedValue),
    //                       new SqlParameter("@ClusterCode",  ddlMainBlockName.SelectedItem.Text),
    //                       new SqlParameter("@Pchy",  ""),
    //                        new SqlParameter("@PchyName",  ""),
    //                       new SqlParameter("@villagecode",  ""),
    //                       new SqlParameter("@VillageName",  ""),
    //                        new SqlParameter("@OldVillageCode",  lblUniqueCode.Text),
    //                         new SqlParameter("@UnqId",   ddlMainBlockName.SelectedValue),
    //                           new SqlParameter("@flag",  2),



    //                    };

    //                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


    //            }

    //        }

    //        #endregion
    //        if (icount > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
    //            btnSerach_Click(btnSerach, null);
    //        }
    //    }
    //    if (Convert.ToInt32(ddlType.SelectedValue) ==2)
    //    {
    //        #region 3
    //        foreach (GridViewRow Itemst in GVBlock.Rows)
    //        {
    //            Label EGBlock = Itemst.FindControl("EGBlock") as Label;
    //            DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

    //            //Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
    //            //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;



    //            TextBox lblClusterCode = Itemst.FindControl("lblClusterCode") as TextBox;
    //            TextBox lblClusterName = Itemst.FindControl("lblClusterName") as TextBox;

    //            Label lblUniquePanchayatCode = Itemst.FindControl("lblUniquePanchayatCode") as Label;
    //            Label lblUniquePanchayatName = Itemst.FindControl("lblUniquePanchayatName") as Label;


    //            Label lblUniqueClusterCode = Itemst.FindControl("lblUniqueClusterCode") as Label;
    //            Label lblUniqueClusterName = Itemst.FindControl("lblUniqueClusterName") as Label;

    //            Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

    //            string ClusterCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblClusterCode.Text.Trim());

    //            string ClusterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblClusterName.Text.Trim());

    //            string UniqueClusterCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueClusterCode.Text.Trim());
    //            string UniqueClusterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueClusterName.Text.Trim());
    //            string msg = "";
    //            if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || ClusterName != UniqueClusterName || ClusterCode != UniqueClusterCode)
    //            {

    //                SqlParameter[] par1 = new SqlParameter[]
    //                    {
    //                          new SqlParameter("@BlockCode",  ddlGBlockName.SelectedValue),
    //                       new SqlParameter("@ClusterCode",  ClusterCode),
    //                       new SqlParameter("@Pchy",  ClusterName),
    //                        new SqlParameter("@PchyName",  ClusterName),
    //                       new SqlParameter("@villagecode",  ""),
    //                       new SqlParameter("@VillageName",  ""),
    //                        new SqlParameter("@OldVillageCode",  UniqueClusterCode),
    //                         new SqlParameter("@UnqId",  lblUniqueCode.Text),
    //                           new SqlParameter("@flag", 3),



    //                    };

    //                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


    //            }

    //        }

    //        #endregion
    //        if (icount > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
    //            btnSerach_Click(btnSerach, null);
    //        }
    //    }


    //    if (Convert.ToInt32(ddlType.SelectedValue) == 5)
    //    {
    //        #region 5
    //        foreach (GridViewRow Itemst in GvSchool.Rows)
    //        {

    //            DropDownList ddlVillageName = Itemst.FindControl("ddlVillageName") as DropDownList;



    //            Label lblDiseCode = Itemst.FindControl("lblDiseCode") as Label;



    //            Label lblVillageCode = Itemst.FindControl("lblVillageCode") as Label;
    //            Label lblUniqueName = Itemst.FindControl("lblUniqueName") as Label;
    //            TextBox lblSchoolName = Itemst.FindControl("lblSchoolName") as TextBox;

    //            Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;


    //            string SchoolName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblSchoolName.Text.Trim());
    //            string UniqueName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueName.Text.Trim());
    //            string msg = "";
    //            if (lblVillageCode.Text != ddlVillageName.SelectedValue.ToString() || SchoolName != UniqueName )
    //            {

    //                SqlParameter[] par1 = new SqlParameter[]
    //                    {
    //                          new SqlParameter("@BlockCode", ""),
    //                       new SqlParameter("@ClusterCode",  lblVillageCode.Text),
    //                       new SqlParameter("@Pchy",  ""),
    //                        new SqlParameter("@PchyName",  ""),
    //                       new SqlParameter("@villagecode", ddlVillageName.SelectedValue),
    //                       new SqlParameter("@VillageName", SchoolName),
    //                        new SqlParameter("@OldVillageCode",  lblDiseCode.Text),
    //                         new SqlParameter("@UnqId",  lblUniqueCode.Text),
    //                           new SqlParameter("@flag", 4),



    //                    };

    //                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


    //            }

    //        }

    //        #endregion
    //        if (icount > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
    //            btnSerach_Click(btnSerach, null);
    //        }
    //    }


    //    if (Convert.ToInt32(ddlType.SelectedValue) == 6)
    //    {
    //        #region 6
    //        foreach (GridViewRow Itemst in gvSchoolMarge.Rows)
    //        {

    //            DropDownList ddlVillageName = Itemst.FindControl("ddlVillageName") as DropDownList;

    //            DropDownList ddlMargeName = Itemst.FindControl("ddlMargeName") as DropDownList;


    //            Label lblDiseCode = Itemst.FindControl("lblDiseCode") as Label;



    //            Label lblVillageCode = Itemst.FindControl("lblVillageCode") as Label;
    //            Label lblUniqueName = Itemst.FindControl("lblUniqueName") as Label;

    //            Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;



    //            string UniqueName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueName.Text.Trim());
    //            string msg = "";
    //            if (ddlMargeName.SelectedIndex > 0 && ddlMargeName.SelectedValue.ToString() != lblDiseCode.Text.Trim())
    //            {

    //                SqlParameter[] par1 = new SqlParameter[]
    //                    {
    //                          new SqlParameter("@BlockCode", ""),
    //                       new SqlParameter("@ClusterCode",  lblDiseCode.Text),
    //                       new SqlParameter("@Pchy",  ""),
    //                        new SqlParameter("@PchyName",  ""),
    //                       new SqlParameter("@villagecode", ddlVillageName.SelectedValue),
    //                       new SqlParameter("@VillageName", ""),
    //                        new SqlParameter("@OldVillageCode",  ddlMargeName.SelectedValue),
    //                         new SqlParameter("@UnqId",  lblUniqueCode.Text),
    //                           new SqlParameter("@flag",5),



    //                    };

    //                icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


    //            }

    //        }

    //        #endregion
    //        if (icount > 0)
    //        {
    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
    //            btnSerach_Click(btnSerach, null);
    //        }
    //    }
    //}





    private Boolean Validation()
    {
        try
        {
            foreach (GridViewRow Itemst in GVVillage.Rows)
            {

                int ind = Itemst.DataItemIndex;
                TextBox lblPass = (TextBox)GVVillage.Rows[ind].FindControl("lblPass");
                CheckBox chk = (CheckBox)GVVillage.Rows[ind].FindControl("Chk_Location");

                DropDownList ddlDistrict = (DropDownList)GVVillage.Rows[ind].FindControl("ddlDistrict");
                DropDownList ddlState = (DropDownList)GVVillage.Rows[ind].FindControl("ddlState");
                DropDownList ddlRole = (DropDownList)GVVillage.Rows[ind].FindControl("ddlRole");

                DropDownList ddlGBlockName = (DropDownList)GVVillage.Rows[ind].FindControl("ddlGBlockName");
                DropDownList ddlCluster = (DropDownList)GVVillage.Rows[ind].FindControl("ddlCluster");
                if (chk.Checked == true)
                {
                    string strQry = "Select * from Mstuserrole where   Role_Level =" + ddlRole.SelectedValue + "";
                    DataTable dtrole = objMain.LoadData(strQry);

                    if (lblPass.Text == "")
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Password')</script>", false);
                        return false;
                    }
                    if (ddlRole.SelectedIndex <= 0)
                    {

                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Role')</script>", false);
                        return false;
                    }

                    if (dtrole.Rows[0]["RID"].ToString() == "2")
                    {

                        if (ddlState.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
                            return false;
                        }
                    }
                    else if (dtrole.Rows[0]["RID"].ToString() == "3")
                    {

                        if (ddlState.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
                            return false;
                        }
                        if (ddlDistrict.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
                            return false;
                        }
                    }
                    else if (dtrole.Rows[0]["RID"].ToString() == "4")
                    {

                        if (ddlState.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
                            return false;
                        }
                        if (ddlDistrict.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
                            return false;
                        }
                        if (ddlGBlockName.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
                            return false;
                        }
                    }
                    else if (dtrole.Rows[0]["RID"].ToString() == "5")
                    {
                        if (ddlState.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State')</script>", false);
                            return false;
                        }
                        if (ddlDistrict.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select District')</script>", false);
                            return false;
                        }
                        if (ddlGBlockName.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Block')</script>", false);
                            return false;
                        }

                        if (ddlCluster.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Cluster')</script>", false);
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
        if (!Validation())
            return;
        SaveData();


        //    if (ret > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
        //    }
        //}
    }

    public void SaveData()
    {
        int result1 = 0;
        foreach (GridViewRow Itemst in GVVillage.Rows)
        {

            int ind = Itemst.DataItemIndex;
            Label lbUserName = (Label)GVVillage.Rows[ind].FindControl("lbUserName");
            TextBox lblPass = (TextBox)GVVillage.Rows[ind].FindControl("lblPass");
            Label lblEmployeeName = (Label)GVVillage.Rows[ind].FindControl("lblEmployeeName");

            Label lblEMail = (Label)GVVillage.Rows[ind].FindControl("lblEMail");

            Label lblContactNo = (Label)GVVillage.Rows[ind].FindControl("lblContactNo");
            Label lblGender = (Label)GVVillage.Rows[ind].FindControl("lblGender");
            Label lblJoined = (Label)GVVillage.Rows[ind].FindControl("lblJoined");
            Label lblLeaving = (Label)GVVillage.Rows[ind].FindControl("lblLeaving");


            CheckBox chk = (CheckBox)GVVillage.Rows[ind].FindControl("Chk_Location");

            DropDownList ddlDistrict = (DropDownList)GVVillage.Rows[ind].FindControl("ddlDistrict");
            DropDownList ddlState = (DropDownList)GVVillage.Rows[ind].FindControl("ddlState");
            DropDownList ddlRole = (DropDownList)GVVillage.Rows[ind].FindControl("ddlRole");

            DropDownList ddlGBlockName = (DropDownList)GVVillage.Rows[ind].FindControl("ddlGBlockName");
            DropDownList ddlCluster = (DropDownList)GVVillage.Rows[ind].FindControl("ddlCluster");
            if (chk.Checked == true)
            {
                string pw = Password.CreatePasswordHash(lblPass.Text);
                SqlParameter[] parm = new SqlParameter[]
                            {


                            new SqlParameter("@EmpCode", lbUserName.Text),
                            new SqlParameter("@EmployeeType",ddlRole.SelectedValue),
                            new SqlParameter("@Firstname", lblEmployeeName.Text.Trim()),
                            new SqlParameter("@Lastname", ""),

                            new SqlParameter("@EmaillID", lblEMail.Text ),
                            new SqlParameter("@MobileNo",lblContactNo.Text ),
                            new SqlParameter("@PostalAddress", ""),

                                  new SqlParameter("@District", ""),
                                     new SqlParameter("@block", ""),
                                 new SqlParameter("@Pincode", ""),
                                      new SqlParameter("@JoingDate",Convert.ToDateTime(lblJoined.Text).ToString("yyyy-MM-dd")),
                                        new SqlParameter("@BirthDay",Convert.ToDateTime(lblLeaving.Text).ToString("yyyy-MM-dd")),
                                          new SqlParameter("@Gender",lblGender.Text),
                             new SqlParameter("@flag","I"),

                              };
                int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_insert_update_Bulkemployeedetails", parm);

                string State = "";
                string Dist = "";
                string Block = "";
                string Cluster = "";
                if (ddlState.SelectedIndex > 0)
                {
                    State = ddlState.SelectedValue;
                }
                if (ddlDistrict.SelectedIndex > 0)
                {
                    Dist = ddlDistrict.SelectedValue;
                }
                if (ddlGBlockName.SelectedIndex > 0)
                {
                    Block = ddlGBlockName.SelectedValue;
                }
                if (ddlCluster.SelectedIndex > 0)
                {
                    Cluster = ddlCluster.SelectedValue;
                }

                SqlParameter[] parm1 = new SqlParameter[]
                {

                    new SqlParameter("@userlevel", ddlRole.SelectedValue),
                    new SqlParameter("@statecode",State),
                    new SqlParameter("@district", Dist),
                    new SqlParameter("@block", Block),
                    new SqlParameter("@village", Cluster),
                    new SqlParameter("@uname", lbUserName.Text.Trim()),
                    new SqlParameter("@pw", pw),
                    new SqlParameter("@staffid", lbUserName.Text.Trim()),
                    new SqlParameter("@flag", ""),
                    new SqlParameter("@uid","0"),
                    new SqlParameter("@UserType", "1"),
                    new SqlParameter("@SerialNo", "0"),
                    new SqlParameter("@fristName",lblEmployeeName.Text ),
                    new SqlParameter("@LastName", ""),
                    new SqlParameter("@UserOnline","1"),
                    new SqlParameter("@UserOffline","0"),
                    new SqlParameter("@IMEINo", ""),
                    new SqlParameter("@CreateBy",  "Admin"),

                };
                result1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_insert_update_Blukusermaster", parm1);

            }
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>", false);
        FillGrid();
    }



    public void FillGrid()
    {
        try
        {
            conditions = "";


            DataSet dttabletdata = new DataSet();

            SqlParameter[] para11 = new SqlParameter[] {

            new SqlParameter("@DistCode",""),

            };

            dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetMasterLoadForValidationUser", para11);

            DataTable dt1 = dttabletdata.Tables[0];
            Session["MBlock"] = dt1;
            DataTable dt2 = dttabletdata.Tables[1];
            Session["MCluster"] = dt2;

            DataTable dt3 = dttabletdata.Tables[2];
            Session["Mpanchy"] = dt3;

            DataTable dt4 = dttabletdata.Tables[3];
            Session["MVill"] = dt4;

            DataTable dt5 = dttabletdata.Tables[4];
            Session["Mdist"] = dt5;
            DataTable dt6 = dttabletdata.Tables[5];
            Session["MRole"] = dt6;
            DataTable dt7 = dttabletdata.Tables[6];
            Session["MSatate"] = dt7;

            DataTable dtvillage = null;

            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                #region
                string COnd = " ";

                if (ddlDistrict.SelectedIndex > 0 || ddlDesignation.SelectedItem.Text != "----Select----")
                {
                    COnd = " and District='" + ddlDistrict.SelectedItem.Text + "'";
                }

                if (ddlDistrict.SelectedIndex > 0)
                {
                    if (ddlDesignation.SelectedItem.Text != "----Select----")
                    {
                        COnd += " and Designation='" + ddlDesignation.SelectedItem.Text + "'";
                    }
                }
                SqlParameter[] par1 = new SqlParameter[]
             {

                        new SqlParameter("@COnd", COnd ),
                      new SqlParameter("@Flag", ddlType.SelectedValue ),


             };
                dtvillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadTempUserMaster", par1);

                if (dtvillage.Rows.Count > 0)
                {
                    GVVillage.DataSource = dtvillage;
                    GVVillage.DataBind();
                    ViewState["dtvillage"] = dtvillage;
                    lblCount.Text = dtvillage.Rows.Count.ToString();
                }
                else
                {
                    GVVillage.DataSource = null;
                    GVVillage.DataBind();

                }
                //  Session["DTcluster"] = DTcluster;

                #endregion
            }


        }
        catch (Exception)
        {

            throw;
        }

    }








    protected void GVGVVillage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {


                string conditions = "";
                DropDownList ddlRole = (DropDownList)e.Row.FindControl("ddlRole");
                DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlState");
                DropDownList ddlDistrict = (DropDownList)e.Row.FindControl("ddlDistrict");
                DropDownList ddlGBlockName = (DropDownList)e.Row.FindControl("ddlGBlockName");
                DropDownList ddlClusert = (DropDownList)e.Row.FindControl("ddlClusert");


                Label lblPMSRole = (Label)e.Row.FindControl("lblPMSRole");

                Label lblPMSState = (Label)e.Row.FindControl("lblPMSState");
                Label lblPMSDist = (Label)e.Row.FindControl("lblPMSDist");
                Label lblPMSBlock = (Label)e.Row.FindControl("lblPMSBlock");
                //conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and Fyear='2017-2018' ";
                DataTable dt1 = null;
                DataTable dt2 = null;
                DataTable dt3 = null;
                //dt1 = (DataTable)Session["MBlock"] ;

                //dt2 = (DataTable) Session["MCluster"] ;

                dt3 = (DataTable)Session["MRole"] as DataTable;

                //dt1.DefaultView.RowFilter = conditions;



                ddlRole.DataSource = dt3;
                ddlRole.DataTextField = "Role";
                ddlRole.DataValueField = "Role_Level";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));


                dt1 = (DataTable)Session["MSatate"] as DataTable;

                //dt1.DefaultView.RowFilter = conditions;



                ddlState.DataSource = dt1;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "StateCode";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));


            }


        }

    }
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TextBox txtCampaignName = ((TextBox)GVIgnored.FindControl("txtCampaignName"));

        //string str = GVIgnored.SelectedRow.Cells[1].Text;
        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlRole = (DropDownList)row1.FindControl("ddlRole");
        DropDownList ddlState = (DropDownList)row1.FindControl("ddlState");
        DropDownList ddlDistrict = (DropDownList)row1.FindControl("ddlDistrict");
        DropDownList ddlGBlockName = (DropDownList)row1.FindControl("ddlGBlockName");
        DropDownList ddlCluster = (DropDownList)row1.FindControl("ddlCluster");

        ddlState.SelectedIndex = 0;
        ddlDistrict.Items.Clear();
        ddlGBlockName.Items.Clear();
        ddlCluster.Items.Clear();
        ddlDistrict.Enabled = false;
        ddlGBlockName.Enabled = false;
        ddlCluster.Enabled = false;

        string strQry;
        strQry = "Select * from Mstuserrole where   Role_Level =" + ddlRole.SelectedValue + "";
        DataTable dtrole = objMain.LoadData(strQry);



        if (dtrole.Rows[0]["RID"].ToString() == "1")
        {
            ddlState.Enabled = true;
        }
        else if (dtrole.Rows[0]["RID"].ToString() == "2")
        {
            ddlState.Enabled = true;

        }
        else if (dtrole.Rows[0]["RID"].ToString() == "3")
        {
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;

        }
        else if (dtrole.Rows[0]["RID"].ToString() == "4")
        {
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
            ddlGBlockName.Enabled = true;

        }
        else if (dtrole.Rows[0]["RID"].ToString() == "5")
        {
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
            ddlGBlockName.Enabled = true;
            ddlCluster.Enabled = true;
        }
    }
    protected void ddlSate_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlState = (DropDownList)row1.FindControl("ddlState");
        DropDownList ddlDistrict = (DropDownList)row1.FindControl("ddlDistrict");

        DropDownList ddlGBlockName = (DropDownList)row1.FindControl("ddlGBlockName");
        DropDownList ddlCluster = (DropDownList)row1.FindControl("ddlCluster");
        ddlGBlockName.Items.Clear();
        ddlCluster.Items.Clear();
        DataTable dt = Session["Mdist"] as DataTable;
        conditions = " StateCode= '" + ddlState.SelectedValue + "'   ";
        dt.DefaultView.RowFilter = conditions;

        ddlDistrict.DataSource = dt.DefaultView.Table;
        ddlDistrict.DataTextField = "DistrictName";
        ddlDistrict.DataValueField = "DistrictCode";
        ddlDistrict.DataBind();
        ddlDistrict.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlDistrict = (DropDownList)row1.FindControl("ddlDistrict");
        DropDownList ddlState = (DropDownList)row1.FindControl("ddlState");

        DropDownList ddlGBlockName = (DropDownList)row1.FindControl("ddlGBlockName");
        DropDownList ddlCluster = (DropDownList)row1.FindControl("ddlCluster");
        ddlGBlockName.Items.Clear();
        ddlCluster.Items.Clear();

        DataTable dt = Session["MBlock"] as DataTable;
        conditions = " DistrictCode= '" + ddlDistrict.SelectedValue + "'  ";
        dt.DefaultView.RowFilter = conditions;

        ddlGBlockName.DataSource = dt.DefaultView.Table;
        ddlGBlockName.DataTextField = "BlockName";
        ddlGBlockName.DataValueField = "BlockCode";
        ddlGBlockName.DataBind();
        ddlGBlockName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
    }

    protected void ddlGBlockName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TextBox txtCampaignName = ((TextBox)GVIgnored.FindControl("txtCampaignName"));

        //string str = GVIgnored.SelectedRow.Cells[1].Text;
        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlGBlockName = (DropDownList)row1.FindControl("ddlGBlockName");


        DropDownList ddlDistrict = (DropDownList)row1.FindControl("ddlDistrict");
        DropDownList ddlState = (DropDownList)row1.FindControl("ddlState");


        DropDownList ddlCluster = (DropDownList)row1.FindControl("ddlCluster");


        ddlCluster.Items.Clear();


        DataTable dt = Session["MCluster"] as DataTable;
        conditions = " BlockCode= '" + ddlGBlockName.SelectedValue + "'  ";
        dt.DefaultView.RowFilter = conditions;

        ddlCluster.DataSource = dt.DefaultView.Table;
        ddlCluster.DataTextField = "ClusterName";
        ddlCluster.DataValueField = "ClusterCode";
        ddlCluster.DataBind();
        ddlCluster.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
    }




}

