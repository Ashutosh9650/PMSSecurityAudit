using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangeMaster : System.Web.UI.Page
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
                LoadUserLeavel();
                //GVCluster.DataSource = null;
                //GVCluster.DataBind();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
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

    public void SaveData()
    {
        Int32 icount = 0;
        if (Convert.ToInt32(ddlType.SelectedValue) == 4)
        {
            foreach (GridViewRow Itemst in GVVillage.Rows)
            {
                #region SaveData
                Label EGBlock = Itemst.FindControl("EGBlock") as Label;
                DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

                Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
                //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;


                Label lblPanchayatCode = Itemst.FindControl("lblPanchayatCode") as Label;
                DropDownList ddlPanchayat = Itemst.FindControl("ddlPanchayat") as DropDownList;

                TextBox lblVillageName = Itemst.FindControl("lblVillageName") as TextBox;
                TextBox lblVillageCode = Itemst.FindControl("lblVillageCode") as TextBox;
                Label lblUniqueVillageName = Itemst.FindControl("lblUniqueVillageName") as Label;
                Label lblUniqueVillageCode = Itemst.FindControl("lblUniqueVillageCode") as Label;

                Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

                string VillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageName.Text.Trim());

                string VillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageCode.Text.Trim());

                string UniqueVillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageName.Text.Trim());
                string UniqueVillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageCode.Text.Trim());
                string msg = "";
                if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || lblPanchayatCode.Text != ddlPanchayat.SelectedValue.ToString() || VillageName != UniqueVillageName || VillageCode != UniqueVillageCode)
                {


                    SqlParameter[] par1 = new SqlParameter[]
               {
                              new SqlParameter("@BlockCode",  ddlGBlockName.SelectedValue),
                           new SqlParameter("@ClusterCode",  ""),
                           new SqlParameter("@Pchy",  ddlPanchayat.SelectedValue),
                            new SqlParameter("@PchyName",  ""),
                           new SqlParameter("@villagecode",  VillageCode),
                           new SqlParameter("@VillageName",  VillageName),
                            new SqlParameter("@OldVillageCode",  UniqueVillageCode),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                               new SqlParameter("@flag",  1),



               };

                    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);




                }
                #endregion
            }

            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnSerach_Click(btnSerach, null);
            }
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            #region 3
            foreach (GridViewRow Itemst in GVBlock.Rows)
            {
                TextBox EGBlock = Itemst.FindControl("lblBlockCode") as TextBox;
                DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;
                DropDownList ddlMainBlockName = Itemst.FindControl("ddlMainBlockName") as DropDownList;
                Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

                string msg = "";
                if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString())
                {

                    SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode",  ddlGBlockName.SelectedValue),
                           new SqlParameter("@ClusterCode",  ddlMainBlockName.SelectedItem.Text),
                           new SqlParameter("@Pchy",  ""),
                            new SqlParameter("@PchyName",  ""),
                           new SqlParameter("@villagecode",  ""),
                           new SqlParameter("@VillageName",  ""),
                            new SqlParameter("@OldVillageCode",  lblUniqueCode.Text),
                             new SqlParameter("@UnqId",   ddlMainBlockName.SelectedValue),
                               new SqlParameter("@flag",  2),



                        };

                    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


                }

            }

            #endregion
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnSerach_Click(btnSerach, null);
            }
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            #region 3
            foreach (GridViewRow Itemst in GVBlock.Rows)
            {
                Label EGBlock = Itemst.FindControl("EGBlock") as Label;
                DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

                //Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
                //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;



                TextBox lblClusterCode = Itemst.FindControl("lblClusterCode") as TextBox;
                TextBox lblClusterName = Itemst.FindControl("lblClusterName") as TextBox;

                Label lblUniquePanchayatCode = Itemst.FindControl("lblUniquePanchayatCode") as Label;
                Label lblUniquePanchayatName = Itemst.FindControl("lblUniquePanchayatName") as Label;


                Label lblUniqueClusterCode = Itemst.FindControl("lblUniqueClusterCode") as Label;
                Label lblUniqueClusterName = Itemst.FindControl("lblUniqueClusterName") as Label;

                Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

                string ClusterCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblClusterCode.Text.Trim());

                string ClusterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblClusterName.Text.Trim());

                string UniqueClusterCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueClusterCode.Text.Trim());
                string UniqueClusterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueClusterName.Text.Trim());
                string msg = "";
                if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || ClusterName != UniqueClusterName || ClusterCode != UniqueClusterCode)
                {

                    SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode",  ddlGBlockName.SelectedValue),
                           new SqlParameter("@ClusterCode",  ClusterCode),
                           new SqlParameter("@Pchy",  ClusterName),
                            new SqlParameter("@PchyName",  ClusterName),
                           new SqlParameter("@villagecode",  ""),
                           new SqlParameter("@VillageName",  ""),
                            new SqlParameter("@OldVillageCode",  UniqueClusterCode),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                               new SqlParameter("@flag", 3),



                        };

                    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


                }

            }

            #endregion
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnSerach_Click(btnSerach, null);
            }
        }


        if (Convert.ToInt32(ddlType.SelectedValue) == 5)
        {
            #region 5
            foreach (GridViewRow Itemst in GvSchool.Rows)
            {

                DropDownList ddlVillageName = Itemst.FindControl("ddlVillageName") as DropDownList;



                Label lblDiseCode = Itemst.FindControl("lblDiseCode") as Label;



                Label lblVillageCode = Itemst.FindControl("lblVillageCode") as Label;
                Label lblUniqueName = Itemst.FindControl("lblUniqueName") as Label;
                TextBox lblSchoolName = Itemst.FindControl("lblSchoolName") as TextBox;

                Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;


                string SchoolName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblSchoolName.Text.Trim());
                string UniqueName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueName.Text.Trim());
                string msg = "";
                if (lblVillageCode.Text != ddlVillageName.SelectedValue.ToString() || SchoolName != UniqueName)
                {

                    SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode", ""),
                           new SqlParameter("@ClusterCode",  lblVillageCode.Text),
                           new SqlParameter("@Pchy",  ""),
                            new SqlParameter("@PchyName",  ""),
                           new SqlParameter("@villagecode", ddlVillageName.SelectedValue),
                           new SqlParameter("@VillageName", SchoolName),
                            new SqlParameter("@OldVillageCode",  lblDiseCode.Text),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                               new SqlParameter("@flag", 4),



                        };

                    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


                }

            }

            #endregion
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnSerach_Click(btnSerach, null);
            }
        }


        if (Convert.ToInt32(ddlType.SelectedValue) == 6)
        {
            #region 6
            foreach (GridViewRow Itemst in gvSchoolMarge.Rows)
            {

                DropDownList ddlVillageName = Itemst.FindControl("ddlVillageName") as DropDownList;

                DropDownList ddlMargeName = Itemst.FindControl("ddlMargeName") as DropDownList;


                Label lblDiseCode = Itemst.FindControl("lblDiseCode") as Label;



                Label lblVillageCode = Itemst.FindControl("lblVillageCode") as Label;
                Label lblUniqueName = Itemst.FindControl("lblUniqueName") as Label;

                Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;



                string UniqueName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueName.Text.Trim());
                string msg = "";
                if (ddlMargeName.SelectedIndex > 0 && ddlMargeName.SelectedValue.ToString() != lblDiseCode.Text.Trim())
                {

                    SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode", ""),
                           new SqlParameter("@ClusterCode",  lblDiseCode.Text),
                           new SqlParameter("@Pchy",  ""),
                            new SqlParameter("@PchyName",  ""),
                           new SqlParameter("@villagecode", ddlVillageName.SelectedValue),
                           new SqlParameter("@VillageName", ""),
                            new SqlParameter("@OldVillageCode",  ddlMargeName.SelectedValue),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                               new SqlParameter("@flag",5),



                        };

                    icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "UpdateVillageData", par1);


                }

            }

            #endregion
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnSerach_Click(btnSerach, null);
            }
        }
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblUniqueCode") as Label).Text;

        Label EGBlock = gvr.FindControl("EGBlock") as Label;
        DropDownList ddlGBlockName = gvr.FindControl("ddlGBlockName") as DropDownList;

        Label lblClusterCode = gvr.FindControl("lblClusterCode") as Label;
        DropDownList ddlClusert = gvr.FindControl("ddlClusert") as DropDownList;


        Label lblPanchayatCode = gvr.FindControl("lblPanchayatCode") as Label;
        DropDownList ddlPanchayat = gvr.FindControl("ddlPanchayat") as DropDownList;

        TextBox lblVillageName = gvr.FindControl("lblVillageName") as TextBox;
        TextBox lblVillageCode = gvr.FindControl("lblVillageCode") as TextBox;
        Label lblUniqueVillageName = gvr.FindControl("lblUniqueVillageName") as Label;
        Label lblUniqueVillageCode = gvr.FindControl("lblUniqueVillageCode") as Label;

        Label lblUniqueCode = gvr.FindControl("lblUniqueCode") as Label;

        string VillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageName.Text.Trim());

        string VillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageCode.Text.Trim());

        string UniqueVillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageName.Text.Trim());
        string UniqueVillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageCode.Text.Trim());
        string msg = "";

        SqlParameter[] par1 = new SqlParameter[]
   {
                              new SqlParameter("@BlockCode",  ddlBlock.SelectedValue),
                           new SqlParameter("@ClusterCode",  ddlClusert.SelectedValue),
                           new SqlParameter("@Pchy",  ddlPanchayat.SelectedValue),
                           new SqlParameter("@villagecode",  VillageCode),
                              new SqlParameter("@OldVillageCode",  UniqueVillageCode),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                           new SqlParameter("@flag",  1),



   };

        int res1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMasterData", par1);





        if (res1 > 0)
        {
            btnSerach_Click(btnSerach, null);
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }


    }



    protected void btn_School_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string UniqueChildCode = (gvr.FindControl("lblUniqueCode") as Label).Text;






        string msg = "";

        SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode",  ""),
                           new SqlParameter("@ClusterCode", ""),
                           new SqlParameter("@Pchy",  ""),
                           new SqlParameter("@villagecode",  ""),
                              new SqlParameter("@OldVillageCode", ""),
                             new SqlParameter("@UnqId",  UniqueChildCode),
                           new SqlParameter("@flag",  4),



                        };

        int res1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMasterData", par1);





        if (res1 > 0)
        {
            btnSerach_Click(btnSerach, null);
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }

    }
    protected void btn_PhyDelete_Click(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;


        Label EGBlock = gvr.FindControl("EGBlock") as Label;
        DropDownList ddlGBlockName = gvr.FindControl("ddlGBlockName") as DropDownList;

        //Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
        //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;


        TextBox lblPanchayatCode = gvr.FindControl("lblPanchayatCode") as TextBox;
        TextBox txtPanchayatName = gvr.FindControl("lblPanchayatName") as TextBox;

        TextBox lblClusterCode = gvr.FindControl("lblClusterCode") as TextBox;
        TextBox lblClusterName = gvr.FindControl("lblClusterName") as TextBox;

        Label lblUniquePanchayatCode = gvr.FindControl("lblUniquePanchayatCode") as Label;
        Label lblUniquePanchayatName = gvr.FindControl("lblUniquePanchayatName") as Label;


        Label lblUniqueClusterCode = gvr.FindControl("lblUniqueClusterCode") as Label;
        Label lblUniqueClusterName = gvr.FindControl("lblUniqueClusterName") as Label;

        Label lblUniqueCode = gvr.FindControl("lblUniqueCode") as Label;

        string PanchayatName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPanchayatName.Text.Trim());

        string PanchayatCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblPanchayatCode.Text.Trim());

        string UniquePanchayatName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniquePanchayatName.Text.Trim());
        string UniquePanchayatCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniquePanchayatCode.Text.Trim());
        string msg = "";
        if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        {
            conditions = "   PanchayatCode='" + UniquePanchayatCode + "' and Fyear='2023-2024' ";

            string strQry = "";
            strQry = "  SELECT Villagecode from mst5Village where " + conditions + " ";
            DataTable dt = objMain.LoadData(strQry);

            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please delete Village before deleting this Panchayat');", true);

                // bt.Attributes.Add("onclick", "javascript:return " + "confirm(' Enrollment link in D2D Please confirm if you want to Deleted?  ')");

                //int res1 = objMain.DeleteEnrollMentData(UniqueChildCode, "D");

                //if (res1 > 0)
                //{
                //    LoadData();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                //}
            }
            else
            {
                SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode",  ddlBlock.SelectedValue),
                           new SqlParameter("@ClusterCode",  ""),
                           new SqlParameter("@Pchy",  PanchayatCode),
                           new SqlParameter("@villagecode",  ""),
                              new SqlParameter("@OldVillageCode",  ""),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                           new SqlParameter("@flag",  2),



                        };

                int res1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMasterData", par1);





                if (res1 > 0)
                {
                    btnSerach_Click(btnSerach, null);
                    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                }
            }
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            conditions = "   ClusterCode='" + lblUniqueClusterCode.Text + "' and Fyear='2023-2024' ";

            string strQry = "";
            strQry = "  SELECT Villagecode from mst5Village where " + conditions + " ";
            DataTable dt = objMain.LoadData(strQry);

            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Please delete Village before deleting this Cluster');", true);

                // bt.Attributes.Add("onclick", "javascript:return " + "confirm(' Enrollment link in D2D Please confirm if you want to Deleted?  ')");

                //int res1 = objMain.DeleteEnrollMentData(UniqueChildCode, "D");

                //if (res1 > 0)
                //{
                //    LoadData();
                //    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                //}
            }
            else
            {
                SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@BlockCode",  ddlBlock.SelectedValue),
                           new SqlParameter("@ClusterCode", lblUniqueClusterCode.Text),
                           new SqlParameter("@Pchy",  PanchayatCode),
                           new SqlParameter("@villagecode",  ""),
                              new SqlParameter("@OldVillageCode",  ""),
                             new SqlParameter("@UnqId",  lblUniqueCode.Text),
                           new SqlParameter("@flag",  2),



                        };

                int res1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMasterData", par1);





                if (res1 > 0)
                {
                    btnSerach_Click(btnSerach, null);
                    ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

                }
            }
        }

    }
    private Boolean Validation()
    {
        try
        {

            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                #region 4
                foreach (GridViewRow Itemst in GVVillage.Rows)
                {
                    Label EGBlock = Itemst.FindControl("EGBlock") as Label;
                    DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

                    Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
                    //  DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;


                    Label lblPanchayatCode = Itemst.FindControl("lblPanchayatCode") as Label;
                    DropDownList ddlPanchayat = Itemst.FindControl("ddlPanchayat") as DropDownList;

                    TextBox lblVillageName = Itemst.FindControl("lblVillageName") as TextBox;
                    TextBox lblVillageCode = Itemst.FindControl("lblVillageCode") as TextBox;
                    Label lblUniqueVillageName = Itemst.FindControl("lblUniqueVillageName") as Label;
                    Label lblUniqueVillageCode = Itemst.FindControl("lblUniqueVillageCode") as Label;

                    Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

                    string VillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageName.Text.Trim());

                    string VillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblVillageCode.Text.Trim());

                    string UniqueVillageName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageName.Text.Trim());
                    string UniqueVillageCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueVillageCode.Text.Trim());
                    string msg = "";
                    if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || lblPanchayatCode.Text != ddlPanchayat.SelectedValue.ToString() || VillageName != UniqueVillageName || VillageCode != UniqueVillageCode)
                    {
                        if (ddlGBlockName.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Block ')</script>", false);
                            return false;
                        }
                        if (ddlPanchayat.SelectedIndex <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Panchayat ')</script>", false);
                            return false;
                        }

                        if (VillageName == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  VillageName ')</script>", false);
                            return false;
                        }

                        if (VillageCode == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  VillageCode ')</script>", false);
                            return false;
                        }
                        conditions = " UniqueCode<>'" + lblUniqueCode.Text.ToString() + "'   and VillageCode='" + UniqueVillageCode + "' and Fyear='2023-2024' ";

                        msg = "   VillageCode=" + VillageCode + " ";

                        SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@Con",  conditions),
                            new SqlParameter("@Flag",  1)


                        };
                        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ValidationVillage", par1);
                        if (dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Data " + msg + " ')</script>", false);
                            return false;
                        }

                    }

                }
                #endregion
            }


            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                #region 3
                foreach (GridViewRow Itemst in GVBlock.Rows)
                {
                    Label EGBlock = Itemst.FindControl("EGBlock") as Label;
                    DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

                    //Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
                    //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;


                    TextBox lblPanchayatCode = Itemst.FindControl("lblPanchayatCode") as TextBox;
                    TextBox txtPanchayatName = Itemst.FindControl("lblPanchayatName") as TextBox;


                    Label lblUniquePanchayatCode = Itemst.FindControl("lblUniquePanchayatCode") as Label;
                    Label lblUniquePanchayatName = Itemst.FindControl("lblUniquePanchayatName") as Label;

                    Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

                    string PanchayatName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtPanchayatName.Text.Trim());

                    string PanchayatCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblPanchayatCode.Text.Trim());

                    string UniquePanchayatName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniquePanchayatName.Text.Trim());
                    string UniquePanchayatCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniquePanchayatCode.Text.Trim());
                    string msg = "";
                    if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || PanchayatName != UniquePanchayatName || PanchayatCode != UniquePanchayatCode)
                    {
                        if (ddlGBlockName.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Block ')</script>", false);
                            return false;
                        }

                        if (PanchayatName == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter  PanchayatName ')</script>", false);
                            return false;
                        }

                        if (PanchayatCode == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter  PanchayatCode ')</script>", false);
                            return false;
                        }
                        //conditions = " UniqueCode<>'" + PanchayatCode + "' and  PanchayatCode='" + UniquePanchayatCode + "' and Fyear='2023-2024'  ";

                        //msg = "   PanchayatCode=" + PanchayatCode + "   ";

                        //SqlParameter[] par1 = new SqlParameter[]
                        //{
                        //      new SqlParameter("@Con",  conditions),
                        //     new SqlParameter("@Flag",  2)


                        //};
                        //DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ValidationVillage", par1);
                        //if (dt.Rows.Count > 0)
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Data " + msg + " ')</script>", false);
                        //    return false;
                        //}

                    }

                }
                #endregion
            }


            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                #region 2
                foreach (GridViewRow Itemst in GVBlock.Rows)
                {
                    Label EGBlock = Itemst.FindControl("EGBlock") as Label;
                    DropDownList ddlGBlockName = Itemst.FindControl("ddlGBlockName") as DropDownList;

                    //Label lblClusterCode = Itemst.FindControl("lblClusterCode") as Label;
                    //DropDownList ddlClusert = Itemst.FindControl("ddlClusert") as DropDownList;

                    TextBox lblClusterCode = Itemst.FindControl("lblClusterCode") as TextBox;
                    TextBox lblClusterName = Itemst.FindControl("lblClusterName") as TextBox;

                    Label lblUniquePanchayatCode = Itemst.FindControl("lblUniquePanchayatCode") as Label;
                    Label lblUniquePanchayatName = Itemst.FindControl("lblUniquePanchayatName") as Label;


                    Label lblUniqueClusterCode = Itemst.FindControl("lblUniqueClusterCode") as Label;
                    Label lblUniqueClusterName = Itemst.FindControl("lblUniqueClusterName") as Label;

                    Label lblUniqueCode = Itemst.FindControl("lblUniqueCode") as Label;

                    string ClusterCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblClusterCode.Text.Trim());

                    string ClusterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblClusterName.Text.Trim());

                    string UniqueClusterCode = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueClusterCode.Text.Trim());
                    string UniqueClusterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(lblUniqueClusterName.Text.Trim());
                    string msg = "";
                    if (EGBlock.Text != ddlGBlockName.SelectedValue.ToString() || ClusterName != UniqueClusterName || ClusterCode != ClusterName)
                    {
                        if (ddlGBlockName.SelectedIndex <= 0)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select  Block ')</script>", false);
                            return false;
                        }

                        if (ClusterName == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter  ClusterName ')</script>", false);
                            return false;
                        }

                        if (ClusterCode == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter  ClusterCode ')</script>", false);
                            return false;
                        }
                        conditions = " UniqueCode<>'" + lblUniqueCode.Text.ToString() + "' and ClusterCode='" + ClusterCode + "' and Fyear='2023-2024'  ";

                        msg = "   ClusterCode=" + ClusterCode + "   ";

                        SqlParameter[] par1 = new SqlParameter[]
                        {
                              new SqlParameter("@Con",  conditions),
                             new SqlParameter("@Flag",  3)


                        };
                        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ValidationVillage", par1);
                        if (dt.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Duplicate Data " + msg + " ')</script>", false);
                            return false;
                        }

                    }

                }
                #endregion
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
        //if (Session["GridViewData"] != null)
        //{
        //    UpdateData();
        //    int ret = 0; 
        //    DataTable Dt = Session["GridViewData"] as DataTable;

        //    // DataRow[] dr = Dt.Select(Cond);
        //    for (int i = 0; i < Dt.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(ddlType.SelectedValue)== 2)
        //        {
        //            string SchoolCode = Dt.Rows[i]["DISECode"].ToString();
        //            Int32 WorkingStatus =Convert.ToInt32(Dt.Rows[i]["WorkingStatus"].ToString());
        //            Int32 Management = Convert.ToInt32(Dt.Rows[i]["Management"].ToString());
        //            ret = objComman.Update_SchoolWorkingStatus(SchoolCode, WorkingStatus, Management);

        //        }
        //        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //        {
        //            string VillageCode = Dt.Rows[i]["VillageCode"].ToString();
        //            string ClusterCode = Dt.Rows[i]["ClusterCode"].ToString();
        //            ret = objComman.Update_VillageCluster(VillageCode, ClusterCode);

        //        }
        //    }

        //    if (ret > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
        //    }
        //}
    }

    private int Update_AnnualExamStatus(string str, string UID, string p)
    {
        int iReturnValue = 0;
        try
        {
            iReturnValue = objComman.Update_AnnualExamStatus(str, UID, Flag);
        }
        catch (Exception exp)
        {

        }
        return iReturnValue;
    }


    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }
    }
    public void FillGrid()
    {
        try
        {
            conditions = "";
            string conditionsCLuster = "";
            //if (ddlState.SelectedIndex > 0)
            //{
            //    conditions = " where V.StateCode='" + ddlState.SelectedValue + "'";
            //    conditionsCLuster = " where D.StateCode='" + ddlState.SelectedValue + "'";
            //}
            //if (ddlDistrict.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            //    conditionsCLuster = conditionsCLuster + " and mstCluster.DistrictCode='" + ddlDistrict.SelectedValue + "'";
            //}
            //if (ddlBlock.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.BlockCode='" + ddlBlock.SelectedValue + "'";
            //}

            //if (ddlVillage.SelectedIndex > 0)
            //{
            //    conditions = conditions + " and V.VillageCode='" + ddlVillage.SelectedValue + "'";
            //}
            DataSet dttabletdata = new DataSet();
            string condition = "";
            SqlParameter[] para11 = new SqlParameter[] {

            new SqlParameter("@DistCode",ddlDistrict.SelectedValue),

            };

            dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetMasterLoadForValidation", para11);

            DataTable dt1 = dttabletdata.Tables[0];
            Session["MBlock"] = dt1;
            DataTable dt2 = dttabletdata.Tables[1];
            Session["MCluster"] = dt2;

            DataTable dt3 = dttabletdata.Tables[2];
            Session["Mpanchy"] = dt3;

            DataTable dt4 = dttabletdata.Tables[3];
            Session["MVill"] = dt4;

            DataTable dt5 = dttabletdata.Tables[4];
            Session["MainBlock"] = dt5;
            DataTable dtBlock = null;
            DataTable dtvillage = null;
            if (Convert.ToInt32(ddlType.SelectedValue) == 1 || Convert.ToInt32(ddlType.SelectedValue) == 2 || Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                #region
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and  mst2District.DistrictCode='" + ddlDistrict.SelectedValue + "'";

                    }
                    SqlParameter[] par1 = new SqlParameter[]
                 {
                      new SqlParameter("@Condition",  conditions),
                      new SqlParameter("@Flag", 1 ),


                 };
                    dtBlock = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ChangeMaterData", par1);

                    //  Session["DTcluster"] = DTcluster;
                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and  mst2District.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  mstCluster.BlockCode='" + ddlBlock.SelectedValue + "'";
                        }

                    }
                    SqlParameter[] par1 = new SqlParameter[]
                 {
                      new SqlParameter("@Condition",  conditions),
                      new SqlParameter("@Flag", 2 ),


                 };
                    dtBlock = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ChangeMaterData", par1);

                    //  Session["DTcluster"] = DTcluster;
                }

                if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                {
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and  mst2District.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  mstPanchayat.BlockCode='" + ddlBlock.SelectedValue + "'";
                        }

                    }
                    SqlParameter[] par1 = new SqlParameter[]
                 {
                      new SqlParameter("@Condition",  conditions),
                      new SqlParameter("@Flag", 3 ),


                 };
                    dtBlock = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ChangeMaterData", par1);

                    //  Session["DTcluster"] = DTcluster;
                }
                //    SqlParameter[] par = new SqlParameter[]
                //{
                //      new SqlParameter("@Condition",  conditions),
                //      new SqlParameter("@Flag",  ddlType.SelectedValue),


                //};

                //DataTable DT = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportClusterChange", par);
                //Session["GridViewData"] = DT;
                GVBlock.Visible = true;
                GVVillage.Visible = false;
                GvSchool.Visible = false;
                gvSchoolMarge.Visible = false;
                if (dtBlock.Rows.Count > 0)
                {
                    GVBlock.DataSource = dtBlock;
                    GVBlock.DataBind();
                }
                else
                {
                    GVBlock.DataSource = null;
                    GVBlock.DataBind();

                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                {

                    GVBlock.Columns[5].Visible = false;
                    GVBlock.Columns[6].Visible = false;

                    GVBlock.Columns[7].Visible = true;
                    GVBlock.Columns[8].Visible = true;
                    GVBlock.Columns[9].Visible = false;
                    GVBlock.Columns[10].Visible = false;

                }
                if (Convert.ToInt32(ddlType.SelectedValue) == 1)
                {



                    GVBlock.Columns[5].Visible = true;
                    GVBlock.Columns[6].Visible = true;

                    GVBlock.Columns[7].Visible = false;
                    GVBlock.Columns[8].Visible = false;

                    GVBlock.Columns[9].Visible = false;
                    GVBlock.Columns[10].Visible = false;

                }

                if (Convert.ToInt32(ddlType.SelectedValue) == 3)
                {

                    GVBlock.Columns[5].Visible = true;
                    GVBlock.Columns[6].Visible = true;

                    GVBlock.Columns[7].Visible = false;
                    GVBlock.Columns[8].Visible = false;

                    GVBlock.Columns[9].Visible = true;
                    GVBlock.Columns[10].Visible = true;
                }
                //else
                //{
                //    GVCluster.Columns[9].Visible = false;
                //    GVCluster.Columns[10].Visible = false;
                //    GVCluster.Columns[11].Visible = false;
                //    GVCluster.Columns[12].Visible = false;
                //    GVCluster.Columns[13].Visible = true;
                //}
                #endregion
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {
                #region
                if (Convert.ToInt32(ddlType.SelectedValue) == 4)
                {
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and  mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  mst5Village.BlockCode='" + ddlBlock.SelectedValue + "'";
                        }
                        if (ddlVillage.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  mst5Village.Villagecode='" + ddlVillage.SelectedValue + "'";
                        }

                    }
                    SqlParameter[] par1 = new SqlParameter[]
                 {
                      new SqlParameter("@Condition",  conditions),
                      new SqlParameter("@Flag", 4 ),


                 };
                    dtvillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ChangeMaterData", par1);
                    GVBlock.Visible = false;
                    GVVillage.Visible = true;
                    GvSchool.Visible = false;
                    gvSchoolMarge.Visible = false;
                    if (dtvillage.Rows.Count > 0)
                    {
                        GVVillage.DataSource = dtvillage;
                        GVVillage.DataBind();
                    }
                    else
                    {
                        GVVillage.DataSource = null;
                        GVVillage.DataBind();

                    }
                    //  Session["DTcluster"] = DTcluster;
                }
                #endregion
            }

            if (Convert.ToInt32(ddlType.SelectedValue) == 5)
            {
                #region
                if (Convert.ToInt32(ddlType.SelectedValue) == 5)
                {
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and  v.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  v.BlockCode='" + ddlBlock.SelectedValue + "'";
                        }
                        if (ddlVillage.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  v.Villagecode='" + ddlVillage.SelectedValue + "'";
                        }

                    }
                    SqlParameter[] par1 = new SqlParameter[]
                 {
                      new SqlParameter("@Condition",  conditions),
                      new SqlParameter("@Flag", 5 ),


                 };
                    dtvillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ChangeMaterData", par1);
                    GVBlock.Visible = false;
                    GVVillage.Visible = false;
                    GvSchool.Visible = true;
                    gvSchoolMarge.Visible = false;
                    if (dtvillage.Rows.Count > 0)
                    {
                        GvSchool.DataSource = dtvillage;
                        GvSchool.DataBind();
                    }
                    else
                    {
                        GvSchool.DataSource = null;
                        GvSchool.DataBind();

                    }
                    //  Session["DTcluster"] = DTcluster;
                }
                #endregion
            }


            if (Convert.ToInt32(ddlType.SelectedValue) == 6)
            {
                #region
                if (Convert.ToInt32(ddlType.SelectedValue) == 6)
                {
                    if (ddlDistrict.SelectedIndex > 0)
                    {
                        conditions = conditions + " and  v.DistrictCode='" + ddlDistrict.SelectedValue + "'";
                        if (ddlBlock.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  v.BlockCode='" + ddlBlock.SelectedValue + "'";
                        }
                        if (ddlVillage.SelectedIndex > 0)
                        {
                            conditions = conditions + " and  v.Villagecode='" + ddlVillage.SelectedValue + "'";
                        }

                    }
                    SqlParameter[] par1 = new SqlParameter[]
                 {
                      new SqlParameter("@Condition",  conditions),
                      new SqlParameter("@Flag", 5 ),


                 };
                    dtvillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ChangeMaterData", par1);
                    GVBlock.Visible = false;
                    GVVillage.Visible = false;
                    GvSchool.Visible = false;
                    gvSchoolMarge.Visible = true;

                    if (dtvillage.Rows.Count > 0)
                    {
                        Session["SchoolMarage"] = dtvillage;
                        gvSchoolMarge.DataSource = dtvillage;
                        gvSchoolMarge.DataBind();

                    }
                    else
                    {
                        gvSchoolMarge.DataSource = null;
                        gvSchoolMarge.DataBind();

                    }
                    //  Session["DTcluster"] = DTcluster;
                }
                #endregion
            }
        }
        catch (Exception)
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

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and Fyear='2023-2024'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode=  '" + Session["DistrictCode"].ToString() + "' and Fyear='2023-2024' ";


        }

        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");



    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and Fyear='2023-2024' ";
        }
        if (Session["user_level"].ToString() == "19")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode=  '" + Session["BlockCode"].ToString() + "' and Fyear='2023-2024'";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and Fyear='2023-2024' ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCVillage()
    {
        conditions = "";
        conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  and mstPanchayat.Fyear='2023-2024' and mst5Village.Fyear='2023-2024'";

        string strQry = "  SELECT mst5Village.VillageCode,dbo.TitleCase(upper(mst5Village.VillageName)) + ' (' + dbo.TitleCase(upper(mst5Village.EgVillageCode)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        //objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", CBVillage, "VillageName", "VillageCode", "Select");

        objComman.BindDLLDatatable("mst5Village", dtVillage, "VillageCode, VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "--Select--");
    }
    public void FillSchool()
    {
        conditions = "";
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions = "BlockCode ='" + ddlBlock.SelectedValue + "' and Fyear='2023-2024' ";
        }
        if (ddlVillage.SelectedIndex > 0)
        {
            conditions = "VillageCode ='" + ddlVillage.SelectedValue + "' and Fyear='2023-2024' ";
        }

        objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");

    }

    #endregion

    #region   SelectedIndexChanged Methods
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    //protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
    //    {
    //        lblShool.Visible = false;
    //        ddlSchool.Visible = false;
    //    }
    //    else
    //    {
    //        lblShool.Visible = true;
    //        ddlSchool.Visible = true;
    //    }
    //}
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        //  FillSchool();
    }


    #endregion

    protected void GV_Cluster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UpdateData();
        //GVCluster.PageIndex = e.NewPageIndex;
        //if (Session["GridViewData"] != null)
        //{
        //    DataTable dt = Session["GridViewData"] as DataTable;
        //    GVCluster.DataSource = dt;
        //    GVCluster.DataBind();
        //}


    }
    public void UpdateData()
    {

        //DataTable dt = (DataTable)Session["GridViewData"];

        //for (int i = 0; i < GVCluster.Rows.Count; i++)
        //{
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //    {


        //        DropDownList ddlWorkingStatus = (DropDownList)GVCluster.Rows[i].FindControl("ddlWorkingStatus");
        //        DropDownList ddlManagement = (DropDownList)GVCluster.Rows[i].FindControl("ddlManagement");
        //        Label lblDISECode = (Label)GVCluster.Rows[i].FindControl("lblDISECode");

        //        DataRow[] dr = dt.Select("DISECode='" + Convert.ToString(lblDISECode.Text) + "'");
        //        if (dr.Length > 0)
        //        {

        //            dr[0]["WorkingStatus"] = ddlWorkingStatus.SelectedValue;
        //            dr[0]["Management"] = ddlManagement.SelectedValue;




        //        }

        //    }
        //    if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //    {



        //        DropDownList ddlClusterCode = (DropDownList)GVCluster.Rows[i].FindControl("ddlClusterCode");

        //        Label lblVillageCode = (Label)GVCluster.Rows[i].FindControl("lblVillageCode");

        //        DataRow[] dr = dt.Select("VillageCode='" + Convert.ToString(lblVillageCode.Text) + "'");
        //        if (dr.Length > 0)
        //        {

        //            dr[0]["ClusterCode"] = ddlClusterCode.SelectedValue;





        //        }

        //    }



        //}
        //Session["GridViewData"] = dt;

    }



    protected void GVBlock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            if (Convert.ToInt32(ddlType.SelectedValue) == 1)
            {
                TextBox lblBlockName = (TextBox)e.Row.FindControl("lblBlockName");
                DropDownList ddlGBlockName = (DropDownList)e.Row.FindControl("ddlGBlockName");
                ddlGBlockName.Visible = false;
                lblBlockName.Visible = true;
            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
            {
                Label lblDistri55ctNaf1 = (Label)e.Row.FindControl("lblDistri55ctNaf1");
                TextBox lblBlockCode = (TextBox)e.Row.FindControl("lblBlockCode");
                TextBox lblBlockName = (TextBox)e.Row.FindControl("lblBlockName");
                DropDownList ddlGBlockName = (DropDownList)e.Row.FindControl("ddlGBlockName");

                DropDownList ddlMainBlockName = (DropDownList)e.Row.FindControl("ddlGBlockName");

                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and Fyear='2023-2024' ";

                DataTable dt1 = Session["MBlock"] as DataTable;

                dt1.DefaultView.RowFilter = conditions;


                ddlGBlockName.DataSource = dt1.DefaultView.Table;
                ddlGBlockName.DataTextField = "BlockName";
                ddlGBlockName.DataValueField = "BlockCode";
                ddlGBlockName.DataBind();
                ddlGBlockName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));


            }
            if (Convert.ToInt32(ddlType.SelectedValue) == 3)
            {
                Label lblDistri55ctNaf1 = (Label)e.Row.FindControl("lblDistri55ctNaf1");
                TextBox lblBlockCode = (TextBox)e.Row.FindControl("lblBlockCode");
                TextBox lblBlockName = (TextBox)e.Row.FindControl("lblBlockName");
                DropDownList ddlMainBlockName = (DropDownList)e.Row.FindControl("ddlMainBlockName");
                TextBox lblMainMainBlockCode = (TextBox)e.Row.FindControl("lblMainMainBlockCode");

                DropDownList ddlGBlockName = (DropDownList)e.Row.FindControl("ddlGBlockName");
                Label EGBlock = (Label)e.Row.FindControl("EGBlock");
                lblBlockCode.Visible = false;
                EGBlock.Visible = true;
                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and Fyear='2023-2024' ";

                DataTable dt1 = Session["MBlock"] as DataTable;

                dt1.DefaultView.RowFilter = conditions;

                ddlGBlockName.DataSource = dt1.DefaultView.Table;
                ddlGBlockName.DataTextField = "BlockName";
                ddlGBlockName.DataValueField = "BlockCode";
                ddlGBlockName.DataBind();
                ddlGBlockName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));

                // objComman.BindDLLDatatable("mst3Block", dt1.DefaultView.Table, "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlGBlockName, "BlockName", "BlockCode", "--Select--");
                ddlGBlockName.Visible = true;
                lblBlockName.Visible = false;
                ddlGBlockName.SelectedValue = lblBlockCode.Text;

                DataTable dt2 = Session["MainBlock"] as DataTable;
                dt2.DefaultView.RowFilter = conditions;
                ddlMainBlockName.DataSource = dt2.DefaultView.Table;
                ddlMainBlockName.DataTextField = "MainBlockName";
                ddlMainBlockName.DataValueField = "MainBlockCode";
                ddlMainBlockName.DataBind();
                ddlMainBlockName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));

                //objComman.BindDLLDatatable("mst3Block", dt1.DefaultView.Table, "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlGBlockName, "BlockName", "BlockCode", "--Select--");
                ddlGBlockName.Visible = true;
                lblBlockName.Visible = false;
                ddlGBlockName.SelectedValue = lblBlockCode.Text;
                ddlMainBlockName.SelectedValue = lblMainMainBlockCode.Text;
            }


        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
        }
    }

    protected void GvSchoolMarge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (Convert.ToInt32(ddlType.SelectedValue) == 6)
            {

                Label lblDistri55ctNaf1 = (Label)e.Row.FindControl("lblDistri55ctNaf1");

                string conditions = "";
                DropDownList ddlVillageName = (DropDownList)e.Row.FindControl("ddlVillageName");
                DropDownList ddlMargeName = (DropDownList)e.Row.FindControl("ddlMargeName");

                Label EGBlock = (Label)e.Row.FindControl("EGBlock");
                Label lblClusterCode = (Label)e.Row.FindControl("lblClusterCode");
                Label lblPanchayatCode = (Label)e.Row.FindControl("lblPanchayatCode");
                Label lblVillageCode = (Label)e.Row.FindControl("lblVillageCode");

                Label lblUniqueDISECode = (Label)e.Row.FindControl("lblUniqueDISECode");


                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and BlockCode= '" + EGBlock.Text + "'  and Fyear='2023-2024' and PanchayatCode='" + lblPanchayatCode.Text + "' and Villagecode='" + lblVillageCode.Text + "'   ";
                DataTable dt1 = null;

                dt1 = (DataTable)Session["SchoolMarage"];


                dt1.DefaultView.RowFilter = conditions;



                ddlMargeName.DataSource = dt1.DefaultView.Table;
                ddlMargeName.DataTextField = "Name";
                ddlMargeName.DataValueField = "DiseCode";
                ddlMargeName.DataBind();
                ddlMargeName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
                // objComman.BindDLLDatatable("mst3Block",dt1.DefaultView.Table, "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlGBlockName, "BlockName", "BlockCode", "--Select--");



                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and BlockCode= '" + EGBlock.Text + "'  and Fyear='2023-2024' and PanchayatCode='" + lblPanchayatCode.Text + "'  ";
                DataTable dt2 = null;

                dt2 = (DataTable)Session["MVill"];



                dt2.DefaultView.RowFilter = conditions;



                ddlVillageName.DataSource = dt2.DefaultView.Table;
                ddlVillageName.DataTextField = "VillageName";
                ddlVillageName.DataValueField = "VillageCode";
                ddlVillageName.DataBind();
                ddlVillageName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
                // objComman.BindDLLDatatable("mst3Block",dt1.DefaultView.Table, "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlGBlockName, "BlockName", "BlockCode", "--Select--");

                ddlVillageName.SelectedValue = lblVillageCode.Text;


            }


        }

    }
    protected void GvSchool_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            if (Convert.ToInt32(ddlType.SelectedValue) == 5)
            {

                Label lblDistri55ctNaf1 = (Label)e.Row.FindControl("lblDistri55ctNaf1");

                string conditions = "";
                DropDownList ddlVillageName = (DropDownList)e.Row.FindControl("ddlVillageName");

                Label EGBlock = (Label)e.Row.FindControl("EGBlock");
                Label lblClusterCode = (Label)e.Row.FindControl("lblClusterCode");
                Label lblPanchayatCode = (Label)e.Row.FindControl("lblPanchayatCode");
                Label lblVillageCode = (Label)e.Row.FindControl("lblVillageCode");

                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and BlockCode= '" + EGBlock.Text + "'  and Fyear='2023-2024' and PanchayatCode='" + lblPanchayatCode.Text + "'  ";
                DataTable dt1 = null;

                dt1 = (DataTable)Session["MVill"];



                dt1.DefaultView.RowFilter = conditions;



                ddlVillageName.DataSource = dt1.DefaultView.Table;
                ddlVillageName.DataTextField = "VillageName";
                ddlVillageName.DataValueField = "VillageCode";
                ddlVillageName.DataBind();
                ddlVillageName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
                // objComman.BindDLLDatatable("mst3Block",dt1.DefaultView.Table, "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlGBlockName, "BlockName", "BlockCode", "--Select--");

                ddlVillageName.SelectedValue = lblVillageCode.Text;

            }


        }

    }

    protected void GVGVVillage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            if (Convert.ToInt32(ddlType.SelectedValue) == 4)
            {

                Label lblDistri55ctNaf1 = (Label)e.Row.FindControl("lblDistri55ctNaf1");

                string conditions = "";
                DropDownList ddlGBlockName = (DropDownList)e.Row.FindControl("ddlGBlockName");
                // DropDownList ddlClusert = (DropDownList)e.Row.FindControl("ddlClusert");
                DropDownList ddlPanchayat = (DropDownList)e.Row.FindControl("ddlPanchayat");


                Label EGBlock = (Label)e.Row.FindControl("EGBlock");
                Label lblClusterCode = (Label)e.Row.FindControl("lblClusterCode");
                Label lblPanchayatCode = (Label)e.Row.FindControl("lblPanchayatCode");
                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "'  and Fyear='2023-2024' ";
                DataTable dt1 = null;
                DataTable dt2 = null;
                DataTable dt3 = null;
                dt1 = (DataTable)Session["MBlock"];

                dt2 = (DataTable)Session["MCluster"];

                dt3 = (DataTable)Session["Mpanchy"] as DataTable;

                dt1.DefaultView.RowFilter = conditions;



                ddlGBlockName.DataSource = dt1.DefaultView.Table;
                ddlGBlockName.DataTextField = "BlockName";
                ddlGBlockName.DataValueField = "BlockCode";
                ddlGBlockName.DataBind();
                ddlGBlockName.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
                // objComman.BindDLLDatatable("mst3Block",dt1.DefaultView.Table, "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlGBlockName, "BlockName", "BlockCode", "--Select--");

                ddlGBlockName.SelectedValue = EGBlock.Text;

                //conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "' and BlockCode= '" + EGBlock.Text + "'  and Fyear='2023-2024' ";
                //dt2.DefaultView.RowFilter = conditions;



                //ddlClusert.DataSource = dt2.DefaultView.Table;
                //ddlClusert.DataTextField = "ClusterName";
                //ddlClusert.DataValueField = "ClusterCode";
                //ddlClusert.DataBind();
                //ddlClusert.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));

                ////objComman.BindDLLDatatable("mstCluster", dt2.DefaultView.Table, "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", conditions, "ClusterName", "asc", ddlClusert, "ClusterName", "ClusterCode", "--Select--");

                //ddlClusert.SelectedValue = lblClusterCode.Text;

                conditions = "DistrictCode ='" + lblDistri55ctNaf1.Text + "' and  BlockCode='" + EGBlock.Text + "'  and Fyear='2023-2024' ";
                dt3.DefaultView.RowFilter = conditions;

                ddlPanchayat.DataSource = dt3.DefaultView.Table;
                ddlPanchayat.DataTextField = "PanchayatName";
                ddlPanchayat.DataValueField = "PanchayatCode";
                ddlPanchayat.DataBind();
                ddlPanchayat.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
                //objComman.BindDLLDatatable("mstPanchayat", dt3.DefaultView.Table, "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");
                ddlPanchayat.SelectedValue = lblPanchayatCode.Text;

            }


        }

    }


    protected void ddlGBlockName_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TextBox txtCampaignName = ((TextBox)GVIgnored.FindControl("txtCampaignName"));

        //string str = GVIgnored.SelectedRow.Cells[1].Text;
        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlGBlockName = (DropDownList)row1.FindControl("ddlGBlockName");



        //DropDownList ddlLabTest = (DropDownList)sender;
        //GridViewRow row = (GridViewRow)ddlLabTest.NamingContainer;
        //DropDownList ddlClusert = (DropDownList)row.FindControl("ddlClusert");



        DropDownList ddlLabTest11 = (DropDownList)sender;
        GridViewRow row11 = (GridViewRow)ddlLabTest11.NamingContainer;
        DropDownList ddlPanchayat = (DropDownList)row11.FindControl("ddlPanchayat");

        if (Convert.ToInt32(ddlType.SelectedValue) == 4)
        {

            DataTable dt2 = Session["MCluster"] as DataTable;

            DataTable dt3 = Session["Mpanchy"] as DataTable;

            //conditions = " BlockCode= '" + ddlGBlockName.SelectedValue + "'  and Fyear='2023-2024' ";
            //dt2.DefaultView.RowFilter = conditions;

            //ddlClusert.DataSource = dt2.DefaultView.Table;
            //ddlClusert.DataTextField = "ClusterName";
            //ddlClusert.DataValueField = "ClusterCode";
            //ddlClusert.DataBind();
            //ddlClusert.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));
            //objComman.BindDLLDatatable("mstCluster", dt2.DefaultView.Table, "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", conditions, "ClusterName", "asc", ddlClusert, "ClusterName", "ClusterCode", "--Select--");

            conditions = "  BlockCode='" + ddlGBlockName.Text + "'  and Fyear='2023-2024' ";
            dt3.DefaultView.RowFilter = conditions;

            ddlPanchayat.DataSource = dt3.DefaultView.Table;
            ddlPanchayat.DataTextField = "PanchayatName";
            ddlPanchayat.DataValueField = "PanchayatCode";
            ddlPanchayat.DataBind();
            ddlPanchayat.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --Select--"));

            //objComman.BindDLLDatatable("mstPanchayat", dt3.DefaultView.Table, "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");

        }


    }




}

