using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
public partial class GISCluster : System.Web.UI.Page
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
                ddlYear.SelectedValue = "2025";
                string userlevelrole = Convert.ToString(Session["user_level_Role"]);
                if (userlevelrole == "1")
                {
                    ddlYear.Enabled = false;
                    ddlState.Enabled = true;
                    ddlDistrict.Enabled = true;
                    ddlBlock.Enabled = true;
                }
                else if (userlevelrole == "4")
                {
                    ddlYear.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlBlock.Enabled = false;
                }
                else
                {
                    ddlDistrict.SelectedIndex = 1;
                    ddlYear.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = true;
                    ddlBlock.Enabled = true;
                }
                objMain.ReportDownload("Cluster", "Cluster", Convert.ToString(Session["username"]));
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }
        //Grid_Add_Headers(GVMain);
    }
    [WebMethod(EnableSession = true)]
    public static string Get_MapDetails(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4)
    {

        //string strFlag = "";
        //string s = "";
        //if (ValidID.Length > 6)
        //{
        //    s = ValidID;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}
        //string LanguageID = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        //if (LanguageID != "")
        //{ }
        //else { LanguageID = "1"; }

        //SqlParameter[] p = new SqlParameter[] {
        //    new SqlParameter("FYear","2023-2024"),
        //      new SqlParameter("StateID","9"),
        //       new SqlParameter("DistrictID","2EB646C9A3BA423EB9C8D49E8"),
        //        new SqlParameter("BlockID","4CAB33CBCEF74D88AB553E86C"),
        //         new SqlParameter("ClusterID","AA722DC830104BD38F782E526")
        //};
        string userlevel = Convert.ToString(HttpContext.Current.Session["user_level"]);
        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("FYear",ValidID),
              new SqlParameter("StateID",ValidID1),
               new SqlParameter("DistrictID",ValidID2),
                new SqlParameter("BlockID",ValidID3),
                 new SqlParameter("ClusterID",ValidID4),
                 new SqlParameter("userlevel",userlevel),

        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_ClusterInfo2026", p);
        DataTable dtc = dt.Copy();
        if (dtc.Columns.Contains("DistrictCode"))
        { dtc.Columns.Remove("DistrictCode"); }
        if (dtc.Columns.Contains("BlockCode"))
        { dtc.Columns.Remove("BlockCode"); }
        if (dtc.Columns.Contains("Villagecode"))
        { dtc.Columns.Remove("Villagecode"); }
        if (dtc.Columns.Contains("ClusterCode"))
        { dtc.Columns.Remove("ClusterCode"); }
        if (dtc.Columns.Contains("SchoolCode"))
        { dtc.Columns.Remove("SchoolCode"); }
        if (dtc.Columns.Contains("latlong"))
        { dtc.Columns.Remove("latlong"); }

        HttpContext.Current.Session["tblLocDetails2"] = dtc;
        DataTable dtExport = dtc.Copy();
        HttpContext.Current.Session["tblLocDetails6"] = dtExport;
        if (ValidID3.Length > 7)
        {

        }
        StringBuilder sb = new StringBuilder();
        // sb.Append("");
        sb.Append("<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12' style='padding:0px'>");
        sb.Append("<table class='table table-striped table-bordered filtered-table' id='tblLocDetails'>");
        sb.Append("<thead><tr>");
        //if(dt.Columns[2].ColumnName.ToLower()=="cluster")
        //{
        //    if (userlevel == "91")
        //    {
        //        sb.Append("<th>#</th>");
        //    }
        //    if (userlevel == "39" || userlevel == "1")
        //    {
        //        sb.Append("<th>#</th>");
        //    }

        //}
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName.ToLower() == "blockcode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "clustercode")
            {
                sb.Append("<th style='display:none;'>" + dt.Columns[i].ColumnName + "</th>");
            }
            else if (dt.Columns[i].ColumnName.ToLower() == "latlong")
            {
                sb.Append("<th style='display:none;'>" + dt.Columns[i].ColumnName + "</th>");
            }
            else if (dt.Columns[i].ColumnName.ToLower() == "schoolcode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "districtcode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "villagecode")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "isapproved")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "status")
            { }
            else if (dt.Columns[i].ColumnName.ToLower() == "remarks")
            { }

            else if (dt.Columns[i].ColumnName.ToLower() == "cluster")
            {
                sb.Append("<th colspan='2' style='width:118px !important'>Cluster</th>");
            }
            else if (dt.Columns[i].ColumnName.ToLower() == "colorcode")
            {
                // sb.Append("<th></th>");
            }
            else if (dt.Columns[i].ColumnName.ToLower() == "finalsubmit")
            {
                // sb.Append("<th></th>");
            }

            else
            {
                sb.Append("<th class='common-header'>" + dt.Columns[i].ColumnName + "</th>");
            }
        }
        sb.Append("</tr></thead><tbody>");
        for (int r = 0; r < dt.Rows.Count; r++)
        {
            string loc = "";
            int finalsubmit = 0;
            string latlong = "";
            int rn = r + 1;
            //if (dt.Columns[2].ColumnName.ToLower() == "cluster")
            //{
            //    if (userlevel == "91")
            //    {
            //        int checkStat = Convert.ToInt32(dt.Rows[r]["isapproved"]);
            //        sb.Append("<tr><td " + (checkStat == 1 ? "style='background-color:green;'" : checkStat == 2 ? "style='background-color:red;'" : "") + "></td>");
            //        //sb.Append("<tr><td " + (checkStat == 1 ? "style='background-color:green;'" : checkStat == 2 ? "style='background-color:red;'" : "") + "><input type='checkbox' id='cb" + rn + "' name='cb" + rn + "'" + (checkStat >0 ? "hidden" : "") + "/></td>");
            //    }
            //    if (userlevel == "39" || userlevel == "1")
            //    {
            //        int checkStat = Convert.ToInt32(dt.Rows[r]["isapproved"]);
            //        sb.Append("<tr><td " + (checkStat == 1 ? "style='background-color:green;'" : checkStat == 2 ? "style='background-color:red;'" : "") + "></td>");
            //    }
            //}

            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (c == 0)
                {
                    //sb.Append("<td style=' style='word-wrap: break-word'>" + dt.Rows[r][c] + "</td>");
                    loc = Convert.ToString(dt.Rows[r][c]);
                    if (dt.Columns[c].ColumnName.ToLower() == "clustercode")
                    {
                        sb.Append("<td style='display:none;'>'" + loc + "'</td>");
                    }

                }
                else if (c == 1)
                {
                    //sb.Append("<td style=' style='word-wrap: break-word'>" + dt.Rows[r][c] + "</td>");
                    latlong = Convert.ToString(dt.Rows[r][c]);
                    if (dt.Columns[c].ColumnName.ToLower() == "latlong")
                    {
                        sb.Append("<td style='display:none;'>'" + latlong + "'</td>");
                    }

                }
                else
                {
                    //if (dt.Columns[c].ColumnName.ToLower() == "blockcode")

                    //{
                    //    loc = Convert.ToString(dt.Rows[r][c]);

                    //}
                    if (dt.Columns[c].ColumnName.ToLower() == "block")
                    {
                        //string lin = "onclick=Go_to_Location('" + latlong + "','')";
                        string lin = "onclick=showloader();bindBlock('blockclick','" + loc + "');bindSchools('blockclick','" + loc + "');ZoomToLatLong();hideloader();";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "village")
                    {
                        string lin = "onclick=showloader();bindClusterVillage('villageclick','" + loc + "');bindSchools('villageclick','" + loc + "');ZoomToLatLong();hideloader();";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "cluster")
                    {
                        string lin = "onclick=showloader();bindClusterVillage('clusterclick','" + loc + "');bindSchools('clusterclick','" + loc + "');ZoomToLatLong();hideloader();";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "school")
                    {
                        string lin = "onclick=Go_to_Location('" + latlong + "','')";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "district")
                    {
                        string lin = "onclick=Go_to_Location('" + latlong + "','')";
                        sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    }
                    //else if (dt.Columns[c].ColumnName.ToLower() == "finalsubmit")
                    //{
                    //    finalsubmit = Convert.ToInt32(Convert.ToString(dt.Rows[r][c]).Trim());
                    //    sb.Append("<td style='display:none;'>" + dt.Rows[r][c] + "</td>");
                    //}
                    else if (dt.Columns[c].ColumnName.ToLower() == "update cluster village")
                    {
                        if (finalsubmit == 0)
                        {
                            string isclustervillage = Convert.ToString(Convert.ToString(dt.Rows[r][c]).Trim());
                            if (isclustervillage == "0")
                            {
                                //if (userlevel == "39" || userlevel == "1")
                                //{
                                string lin = "onclick=UpdateCluster('" + loc + "','');";
                                sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">Update</a></td>");
                                //}
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }

                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "make cluster village")
                    {
                        if (finalsubmit == 0)
                        {
                            string isclustervillage2 = Convert.ToString(Convert.ToString(dt.Rows[r][c]).Trim());

                            if (isclustervillage2 == "0")
                            {
                                //if (userlevel == "39" || userlevel == "1")
                                //{
                                string lin = "onclick=SetFirstVillage('" + loc + "','');";
                                sb.Append("<td class='common-cell'> <a href='javascript:void(0);' " + lin + ">Set Cluster</a></td>");
                                //}
                            }
                            else
                            {
                                sb.Append("<td></td>");
                            }
                        }

                        else
                        {
                            sb.Append("<td></td>");
                        }

                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "colorcode")
                    {
                        sb.Append("<td style = 'background-color:" + Convert.ToString(dt.Rows[r][c]) + ";width:10px !important'></td>");
                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "isapproved")
                    {

                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "status")
                    {

                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "remarks")
                    {

                    }
                    else if (dt.Columns[c].ColumnName.ToLower() == "finalsubmit")
                    {

                    }

                    //else if (dt.Columns[c].ColumnName.ToLower() == "approve cluster")
                    //{
                    //    string lin = "onclick=ApproveCluster('" + loc + "','')";
                    //    sb.Append("<td> <a href='javascript:void(0);' " + lin + ">" + Convert.ToString(dt.Rows[r][c]).Trim() + "</a></td>");
                    //}
                    else
                    {
                        sb.Append("<td class='common-cell'>" + dt.Rows[r][c] + "</td>");
                    }

                }
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody></table>");
        sb.Append("</div>");
        string str = sb.ToString();
        return sb.ToString();


    }


    [WebMethod(EnableSession = true)]
    public static string Get_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5, string ValidID6, string ValidID7)
    {

        //string strFlag = "";
        //string s = "";
        //if (ValidID.Length > 6)
        //{
        //    s = ValidID;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}
        //string LanguageID = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        //if (LanguageID != "")
        //{ }
        //else { LanguageID = "1"; }

        string status = "";

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                 new SqlParameter("@numberOfClusters",ValidID4),
                 new SqlParameter("@DistanceToCover",ValidID5),
                 new SqlParameter("@numberofOOSCs",ValidID6),
                 new SqlParameter("@generateflag",ValidID7)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster generated successfully !!";
            }
            else if (status.ToLower() == "cluster already generated !!")
            {
                status = "Cluster already generated, please regenerate !!";
            }
            else
            {
                status = "Cluster not generated, please try again !!";
            }


        }
        else
        {
            status = "Cluster not generated, please try again !!";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string Approve_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5)
    {

        string status = "";
        string s = "";
        string strFlag = "";
        if (ValidID4.Length > 6)
        {
            s = ValidID4;
            string[] subs = s.Split('#');
            strFlag = subs[0];
        }
        else
        {
            strFlag = ValidID;
        }

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                 new SqlParameter("@Cluser_code",strFlag),
                 new SqlParameter("@username",ValidID5),
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Approve_cluster2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster approved !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not approved, please try again !!";
            }
            else
            {
                status = "Cluster not approved, please try again !!";
            }

        }
        else
        {
            status = "Cluster not approved, please try again !!";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string Reject_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5, string ValidID6)
    {

        string status = "";
        string s = "";
        string strFlag = "";
        if (ValidID4.Length > 6)
        {
            s = ValidID4;
            string[] subs = s.Split('#');
            strFlag = subs[0];
        }
        else
        {
            strFlag = ValidID;
        }

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                 new SqlParameter("@Cluser_code",strFlag),
                  new SqlParameter("@remarks",ValidID5),
                  new SqlParameter("@username",ValidID6),
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Reject_cluster2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster rejected !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not rejected, please try again !!";
            }
            else
            {
                status = "Cluster not rejected, please try again !!";
            }

        }
        else
        {
            status = "Cluster not rejected, please try again !!";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string Unlock_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4)
    {

        string status = "";
        //string s = "";
        //string strFlag = "";
        //if (ValidID4.Length > 6)
        //{
        //    s = ValidID4;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                new SqlParameter("@username",ValidID4),
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Unlock_cluster2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster unlocked !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not unlocked, please try again !!";
            }
            else
            {
                status = "Cluster not unlocked, please try again !!";
            }

        }
        else
        {
            status = "Cluster not unlocked, please try again !!";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string Delete_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5)
    {

        string status = "";
        string s = "";
        string strFlag = "";
        if (ValidID4.Length > 6)
        {
            s = ValidID4;
            string[] subs = s.Split('#');
            strFlag = subs[0];
        }
        else
        {
            strFlag = ValidID;
        }

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                 new SqlParameter("@Cluser_code",strFlag),
                 new SqlParameter("@username",ValidID5),
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Delete_cluster2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster deleted !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not deleted, please try again !!";
            }
            else
            {
                status = "Cluster not deleted, please try again !!";
            }

        }
        else
        {
            status = "Cluster not deleted, please try again !!";
        }
        return status;


    }
    [WebMethod(EnableSession = true)]
    public static string Submit_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4)
    {

        string status = "";
        //string s = "";
        //string strFlag = "";
        //if (ValidID4.Length > 6)
        //{
        //    s = ValidID4;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                new SqlParameter("@username",ValidID4)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Submit_cluster_forApproval2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster submitted !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not submitted, please try again !!";
            }
            else
            {
                status = "Cluster not submitted, please try again !!";
            }

        }
        else
        {
            status = "Cluster not submitted, please try again !!";
        }
        return status;


    }
    [WebMethod(EnableSession = true)]
    public static string Submit_Cluster_BO(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4)
    {

        string status = "";
        //string s = "";
        //string strFlag = "";
        //if (ValidID4.Length > 6)
        //{
        //    s = ValidID4;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                new SqlParameter("@username",ValidID4)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Submit_cluster_BO2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster submitted !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not submitted, please try again !!";
            }
            else
            {
                status = "Cluster not submitted, please try again !!";
            }

        }
        else
        {
            status = "Cluster not submitted, please try again !!";
        }
        return status;


    }
    [WebMethod(EnableSession = true)]
    public static string Submit_Cluster_Info(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {
        string submited = "";
        string approved = "";
        string status = "";
        //string s = "";
        //string strFlag = "";
        //if (ValidID4.Length > 6)
        //{
        //    s = ValidID4;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Submit_cluster_forApproval_Info2026", p);
        if (dt.Rows.Count > 0)
        {
            submited = Convert.ToString(dt.Rows[0]["FinalSubmit"]);
            approved = Convert.ToString(dt.Rows[0]["isApproved"]);
            status = submited + "#" + approved;

        }
        else
        {
            status = "fail";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string Submit_Cluster_Info_BO(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {

        string status = "";
        //string s = "";
        //string strFlag = "";
        //if (ValidID4.Length > 6)
        //{
        //    s = ValidID4;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Submit_cluster_BO_Info2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["FinalSubmit"]);

        }
        else
        {
            status = "fail";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string Get_Approval_Status_Info(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {

        string status = "";
        //string s = "";
        //string strFlag = "";
        //if (ValidID4.Length > 6)
        //{
        //    s = ValidID4;
        //    string[] subs = s.Split('#');
        //    strFlag = subs[0];
        //}
        //else
        //{
        //    strFlag = ValidID;
        //}

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GET_Approval_Status2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["isApproved"]);

        }
        else
        {
            status = "fail";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static object Get_Generate_Cluster_Info(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {

        string status = "";
        string noof_Villages = "";
        string noof_OOSC = "";
        string max_Distance = "";

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3)
        };

        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Generate_Cluster_Condition2025", p);
        if (dt.Rows.Count > 0)
        {
            noof_Villages = Convert.ToString(dt.Rows[0]["noof_Villages"]);
            noof_OOSC = Convert.ToString(dt.Rows[0]["noof_OOSC"]);
            max_Distance = Convert.ToString(dt.Rows[0]["max_Distance"]);
            status = noof_Villages + "#" + noof_OOSC + "#" + max_Distance;

        }
        else
        {
            status = noof_Villages + "#" + noof_OOSC + "#" + max_Distance;
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string UpdateVillageCluster(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5, string ValidID6, string ValidID7, string ValidID8, string ValidID9)
    {

        string status = "";

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                 new SqlParameter("@Cluser_code",ValidID4),
                 new SqlParameter("@Village_code",ValidID5),
                 new SqlParameter("@Update_Cluser_code",ValidID6),
                 new SqlParameter("@numberOfVillages",ValidID7),
                 new SqlParameter("@DistanceToCover",ValidID8),
                 new SqlParameter("@numberofOOSCs",ValidID9),
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Update_Village_cluster_GIS2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster updated !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster not updated, please try again !!";
            }
            else
            {
                status = "Cluster not updated, please try again !!";
            }

        }
        else
        {
            status = "Cluster not updated, please try again !!";
        }
        return status;


    }

    [WebMethod(EnableSession = true)]
    public static string UpdateFirstVillage(string ValidID, string ValidID1, string ValidID2, string ValidID3, string ValidID4, string ValidID5, string ValidID6)
    {

        string status = "";

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@FYear",ValidID),
              new SqlParameter("@State_code",ValidID1),
               new SqlParameter("@District_code",ValidID2),
                new SqlParameter("@block_code",ValidID3),
                 new SqlParameter("@Cluser_code",ValidID4),
                 new SqlParameter("@Village_code",ValidID5),
                 new SqlParameter("@VillageName",ValidID6)
        };
        //int n = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_chitrakoot_Clusters_AllSide", p);
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Update_First_Village_cluster_GIS2025", p);
        if (dt.Rows.Count > 0)
        {
            status = Convert.ToString(dt.Rows[0]["Status"]);
            if (status.ToLower() == "success")
            {
                status = "Cluster village Updated !!";
            }
            else if (status.ToLower() == "fail")
            {
                status = "Cluster village not updated, please try again !!";
            }
            else
            {
                status = "Cluster not updated, please try again !!";
            }

        }
        else
        {
            status = "Cluster village not updated, please try again !!";
        }
        return status;


    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //Extract_All_To_Export();

        DataTable dt = new DataTable();
        dt = (HttpContext.Current.Session["tblLocDetails2"] as DataTable);




        ExeclHeatMap(dt);
    }

    public void ExeclHeatMap(DataTable dtMain1)
    {

        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        DataTable dt = dtMain1;

        if (dtMain1.Columns.Contains("Update Cluster Village"))
        {
            wb = new XLWorkbook(StartupPath + "\\GIS_Based_ClusteringV.xlsx");
        }
        else
        {
            wb = new XLWorkbook(StartupPath + "\\GIS_Based_Clustering.xlsx");
        }
        Int32 ii54 = Convert.ToInt32(dt.Rows.Count) + 3;

        string str55 = "";
        if (dtMain1.Columns.Contains("Update Cluster Village"))
        {
            str55 = "A2:D" + ii54;
        }
        else if (dtMain1.Columns.Contains("ColorCode"))
        {
            str55 = "A2:Y" + ii54;
        }
        else
        {
            str55 = "A2:Y" + ii54;
        }

        if (dtMain1.Columns.Contains("ColorCode"))
            dtMain1.Columns.Remove("ColorCode");
        if (dtMain1.Columns.Contains("Status"))
            dtMain1.Columns.Remove("Status");
        if (dtMain1.Columns.Contains("Remarks"))
            dtMain1.Columns.Remove("Remarks");
        if (dtMain1.Columns.Contains("isapproved"))
            dtMain1.Columns.Remove("isapproved");
        if (dtMain1.Columns.Contains("FinalSubmit"))
            dtMain1.Columns.Remove("FinalSubmit");
        var ws = wb.Worksheet(1);

        for (int x = 0; x < dt.Columns.Count; x++)
        {

            ws.Cell(2, x + 1).Value = dt.Columns[x].ColumnName;
        }

        ws.Cell(3, 1).InsertData(dt.Rows);


        ws.Range(str55).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str55).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str55).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        filepath = StartupPath + "\\GIS_Based_Clustering" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }
    public void Extract_All_To_Export()
    {
        XLWorkbook wb = new XLWorkbook();
        DataTable dtClw = new DataTable();
        string AppName = "GIS_Based_Clustering";//ddl_Rerservior.SelectedItem.Text;// TypeName = ddl_DataType.SelectedItem.Text;
        string filename = AppName + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss");
        DataTable dt = new DataTable();
        dt = (HttpContext.Current.Session["tblLocDetails2"] as DataTable);
        DataTable dtrpt = dt.Copy();
        if (dtrpt.Columns.Contains("ColorCode"))
            dtrpt.Columns.Remove("ColorCode");
        if (dtrpt.Columns.Contains("Status"))
            dtrpt.Columns.Remove("Status");
        if (dtrpt.Columns.Contains("Remarks"))
            dtrpt.Columns.Remove("Remarks");
        if (dtrpt.Columns.Contains("isapproved"))
            dtrpt.Columns.Remove("isapproved");
        if (dtrpt.Columns.Contains("FinalSubmit"))
            dtrpt.Columns.Remove("FinalSubmit");
        var ws = wb.Worksheets.Add(dtrpt, "GIS_Based_Clustering");

        var NewRows = ws.Row(1);
        NewRows.InsertRowsAbove(1);

        ws.Range("A1:F1").Merge();
        ws.Range("A1:F1").Value = "GIS Based Clustering";
        ws.Range("A1:F1").Style.Font.SetFontSize(12);
        ws.Range("A1:F1").Style.Font.SetBold();
        ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

        Export_TO_Excel(wb, filename);

    }
    protected void Export_TO_Excel(XLWorkbook wb, string filename)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        catch
        {
            throw;
        }

    }

    [WebMethod]
    public static string GetGeoJson(string url)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        using (var client = new WebClient())
        {
            client.Encoding = System.Text.Encoding.UTF8;
            return client.DownloadString(url);
        }
    }
}