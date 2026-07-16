using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmTravelMatrix2024Fare : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    int sumFooterValue = 0;
    int TravelCostWithincluster = 0;
    int TravelCostWithinclusterOut = 0;
    int PerDiem = 0;
    int Accommodation = 0;
    int Conveyance = 0;
    int Expanses = 0;
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
                ddlYear.Enabled = false;

                ViewState["1"] = "ss";

                //if (Request.QueryString["ID"] != null)
                //{
                //     ddlState.SelectedValue=Convert.ToString(Session["Scode"] );
                //    ddlState_SelectedIndexChanged(ddlState, null);
                //   ddlDistrict.SelectedValue = Convert.ToString(Session["Dcode"]);
                //    ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

                //    ddlBlock.SelectedValue = Convert.ToString(Session["Bcode"]);
                //    ddlBlock_SelectedIndexChanged(ddlDistrict, null);
                //    ddlCluster.SelectedValue = Convert.ToString(Session["Ccode"]);
                //    ddlCluster_SelectedIndexChanged(ddlDistrict, null);
                //    ddlFC.SelectedValue= Convert.ToString(Session["FCcode"]);
                //    ddlMonth.SelectedValue = Convert.ToString(Session["MMmonth"]);
                //    btnSearch_Click(btnAdd, null);

                //}

            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }
    protected void btnDown_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LoadDatadownload();
    }
    public void LoadDatadownload()
    {

        string con = "";
        if (ddlCluster.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Cluster')</script>", false);
            return;
        }

        SqlParameter[] parameters1 = new SqlParameter[]
        {
                 new SqlParameter("@Action",4),
                 new SqlParameter("@CLusterCode",ddlCluster.SelectedValue),

        };
        DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportFareMatrix", parameters1);
        string sameVillage = "";
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            sameVillage = dt1.Rows[0]["aVillageName"].ToString();

            HttpContext.Current.Session["aVillageName"] = dt1.Rows[0]["aVillageName"].ToString();
            Session["VillageName"] = dt1.Rows[0]["VillageName"].ToString();
            Session["District"] = dt1.Rows[0]["District"].ToString();
            Session["Block"] = dt1.Rows[0]["Block"].ToString();

        }

        SqlParameter[] parameters2 = new SqlParameter[]
        {
                 new SqlParameter ("@Action",1),
                 new SqlParameter ("@Con",""),
                 new SqlParameter("@CLusterCode",ddlCluster.SelectedValue),
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptReportFareMatrix", parameters2);
        if (dt != null && dt.Rows.Count > 0)
        {
            dt.Rows[0]["District"] = Session["District"].ToString();
            dt.Rows[0]["Block"] = Session["Block"].ToString();
        }

        if (dt != null)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (sameVillage == item["Villagename"].ToString())
                {
                    item["District"] = Session["District"].ToString();
                    item["Block"] = Session["Block"].ToString();

                }
                else
                {
                    item["District"] = "0";
                    item["Block"] = "0";
                    item["VillageType"] = "Village";
                }
            }
        }
        ExporttoCSV(dt, "TravelFare");
    }
    private void ExporttoCSV(DataTable table, string filename1)
    {
        string filePath = filename1;

        var dataTable = table;
        StringBuilder sbldr = new StringBuilder();
        List<string> columnNames = new List<string>();
        List<string> rows = new List<string>();


        if (dataTable.Columns.Count != 0)
        {
            foreach (DataColumn col in dataTable.Columns)
            {
                sbldr.Append(col.ColumnName + ',');
            }
            sbldr.Append("\r\n");
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn column in dataTable.Columns)
                {


                    sbldr.Append(Convert.ToString(row[column]).Replace(",", "- ").Replace("\r", "").Replace("\n", "") + ',');

                }
                sbldr.Append("\r\n");

            }
        }
        //    foreach (DataColumn col in dataTable.Columns)
        //{
        //    builder.Append(col.ColumnName + ',');
        //}
        //builder.Append("\r\n");
        //foreach (DataRow row in dataTable.Rows)
        //{
        //    List<string> currentRow = new List<string>();

        //    foreach (DataColumn column in dataTable.Columns)
        //    {
        //        if (column.ColumnName == "Start Entry Location" || column.ColumnName == "End Entry Location")
        //        {
        //            builder.Append(row[column].ToString().Replace(",", "-"));
        //        }
        //        else
        //        {
        //            builder.Append(row[column].ToString().Replace("\r", "").Replace("\n", ""));
        //        }




        //    }
        //    builder.Append("\r\n");
        //    // rows.Add(string.Join(",", currentRow.ToArray()));

        //}



        //   builder.Append(string.Join("\n", rows.ToArray()));

        string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath")); ;
        string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
        string path = sFileDir + Fullfilename;
        File.WriteAllText(path, sbldr.ToString());

        FileStream fs = null;//, fs2=null;
        try
        {
            string path1 = Fullfilename;
            string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + "/" + path1 + "");
            string datafolder = path1.Substring(0, path1.Length - 4);
            //  string[] file = Directory.GetFiles(foldername);

            string fullPath = Request.MapPath("~/DataBackup/" + datafolder + "" + ".zip");
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(foldername, "");
                //    zip.AddFiles(file, foldername);
                zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath")) + "/" + datafolder + "" + ".zip");
            }



            HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
            fs = File.Open(fullPath, FileMode.Open);
            byte[] bytBytes = new byte[(fs.Length)];
            fs.Read(bytBytes, 0, (int)fs.Length);
            fs.Close();
            Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(bytBytes);






            if (File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            if (File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            Response.Flush();
            Response.End();
        }

        catch (System.Exception ex)
        {
            Response.Clear();

            //string mmsg = ex.Message;
            //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
        }
        finally
        {
            fs.Dispose();
            Response.Clear();

        }
    }

    public void GETDETAILS()
    {

        SqlParameter[] parameters1 = new SqlParameter[]
        {
                 new SqlParameter("@Action",4),
                 new SqlParameter("@CLusterCode",ddlCluster.SelectedValue),

        };
        DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters1);
        string sameVillage = "";
        if (dt1 != null && dt1.Rows.Count > 0)
        {
            sameVillage = dt1.Rows[0]["aVillageName"].ToString();

            HttpContext.Current.Session["aVillageName"] = dt1.Rows[0]["aVillageName"].ToString();
            Session["VillageName"] = dt1.Rows[0]["VillageName"].ToString();
            Session["District"] = dt1.Rows[0]["District"].ToString();
            Session["Block"] = dt1.Rows[0]["Block"].ToString();

        }

        SqlParameter[] parameters2 = new SqlParameter[]
        {
                 new SqlParameter ("@Action",1),
                 new SqlParameter ("@Con",""),
                 new SqlParameter("@CLusterCode",ddlCluster.SelectedValue),
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters2);
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    dt.Rows[0]["District"] = Session["District"].ToString();
        //    dt.Rows[0]["Block"] = Session["Block"].ToString();
        //}

        if (dt != null)
        {
            foreach (DataRow item in dt.Rows)
            {
                if (sameVillage == item["Villagename"].ToString())
                {

                    item["District"] = HttpContext.Current.Session["District"].ToString();
                    item["Block"] = HttpContext.Current.Session["Block"].ToString();
                }
                else
                {
                    item["VillageType"] = "Village";
                }
            }
        }

        var strhtml = GenerateHtmlTableWithTextBox(dt);
        rptTravelfare.InnerHtml = strhtml;
    }


    [WebMethod]
    public static string LoadData(string clustercode)
    {
        DataTable dt = null;
        string sameVillage = "";
        SqlParameter[] parameters1 = new SqlParameter[]
        {
           new SqlParameter("@Action",4),
            new SqlParameter("@CLusterCode",clustercode),

        };
        DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters1);

        if (dt1 != null && dt1.Rows.Count > 0)
        {
            sameVillage = dt1.Rows[0]["aVillageName"].ToString();
            HttpContext.Current.Session["aVillageName"] = dt1.Rows[0]["aVillageName"].ToString();
            HttpContext.Current.Session["VillageName"] = dt1.Rows[0]["VillageName"].ToString();
            HttpContext.Current.Session["District"] = dt1.Rows[0]["District"].ToString();
            HttpContext.Current.Session["Block"] = dt1.Rows[0]["Block"].ToString();



            SqlParameter[] parameters2 = new SqlParameter[]
            {
           new SqlParameter ("@Action",1),
           new SqlParameter ("@Con",""),
              new SqlParameter("@CLusterCode",clustercode),
            };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters2);

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    dt.Rows[0]["District"] = HttpContext.Current.Session["District"].ToString();
            //    dt.Rows[0]["Block"] = HttpContext.Current.Session["Block"].ToString();
            //}


            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (sameVillage == item["Villagename"].ToString())
                    {
                        item["District"] = HttpContext.Current.Session["District"].ToString();
                        item["Block"] = HttpContext.Current.Session["Block"].ToString();
                    }
                    else
                    {
                        item["VillageType"] = "Village";
                    }
                }
            }
        }
        var strhtml = "";
        if (dt != null && dt.Rows.Count > 0)
        {
            strhtml = GenerateHtmlTableWithTextBox(dt);
            //var strhtml = "";}
        }
        else
        {
            strhtml = "No Data Found";
        }

        return strhtml;
    }
    public static string GenerateHtmlTableWithTextBox(DataTable dt)
    {
        DataView dv = dt.DefaultView;
        //dv.Sort = "VillageType";




        StringBuilder sb = new StringBuilder();
        string ColValue = string.Empty;
        string BaseVillage = string.Empty;
        sb.Append("<table border='1' style='border-collapse:collapse; width:100%;' Id='tblrpt'>");


        sb.Append("<thead><tr>");
        foreach (DataColumn column in dv.Table.Columns)
        {

            string C = column.ColumnName;


            sb.AppendFormat("<th style='background-color: #f2f2f2; padding: 8px; '>{0}</th>", column.ColumnName);
        }
        sb.Append("</tr></thead>");

        sb.Append("<tbody>");

        int icoount = dv.Table.Columns.Count;
        foreach (DataRowView row in dv)
        {
            sb.Append("<tr>");
            for (int i = 0; i < dv.Table.Columns.Count; i++)
            {
                string columnName = dv.Table.Columns[i].ColumnName;

                var item = row[i];

                if (columnName == "VillageName" || columnName == "VillageType")
                {
                    string Colvalue = "";
                    HttpContext.Current.Session["bEGVillageCode"] = null;
                    System.Text.RegularExpressions.Match match = Regex.Match(item.ToString(), @"\((\d+)\)");
                    if (match.Success)
                    {

                        string value = match.Groups[1].Value;
                        Colvalue = value;
                        if (columnName == "VillageName")
                        {
                            HttpContext.Current.Session["bEGVillageCode"] = Colvalue;
                        }

                    }
                    sb.AppendFormat("<td style='padding: 8px;'><label>{0}</label></td>", item);
                }

                else if (columnName == "District")
                {
                    var aVillageName = GetCode(HttpContext.Current.Session["aVillageName"].ToString());
                    if (HttpContext.Current.Session["bEGVillageCode"] != null)
                    {
                        BaseVillage = HttpContext.Current.Session["bEGVillageCode"].ToString();
                    }
                    if (BaseVillage == aVillageName)
                    {
                        sb.AppendFormat("<td style='padding: 8px;'><input type='text' oninput='validateInput(event)' class='textbox' value='{0}' style='width: 80%; height:15px;' /></td>", item);
                    }
                    else
                    {
                        sb.AppendFormat("<td style='padding: 8px;'><input type='text' disabled='disabled' oninput='validateInput(event)' class='textbox' value='{0}' style='width: 80%; height:15px;' /></td>", item);

                    }

                }

                else if (columnName == "Block")
                {
                    var aVillageName = GetCode(HttpContext.Current.Session["aVillageName"].ToString());
                    if (HttpContext.Current.Session["bEGVillageCode"] != null)
                    {
                        BaseVillage = HttpContext.Current.Session["bEGVillageCode"].ToString();
                    }
                    if (BaseVillage == aVillageName)
                    {
                        sb.AppendFormat("<td style='padding: 8px;'><input type='text' oninput='validateInput(event)' class='textbox' value='{0}' style='width: 80%; height:15px;' /></td>", item);
                    }
                    else
                    {
                        sb.AppendFormat("<td style='padding: 8px;'><input type='text' disabled='disabled' oninput='validateInput(event)' class='textbox' value='{0}' style='width: 80%; height:15px;' /></td>", item);
                    }
                }

                else
                {
                    sb.AppendFormat("<td style='padding: 8px;'><input type='text' oninput='validateInput(event)' class='textbox' value='{0}' style='width: 80%; height:15px;' /></td>", item);
                }
            }
            sb.Append("</tr>");
        }


        sb.Append("</tbody>");


        sb.Append("</table>");

        return sb.ToString() + "___" + icoount.ToString();
    }




    [WebMethod]
    public static string LoadDataTravelFare(string JSONDATA, string clustercode)
    {
        try
        {
            string bEGVillageCode = string.Empty;
            int rowno = 0;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, string>> tableData = serializer.Deserialize<List<Dictionary<string, string>>>(JSONDATA);
            List<Dictionary<string, string>> uniqueTableData = new List<Dictionary<string, string>>();

            foreach (var row in tableData)
            {
                bool isDuplicate = false;


                foreach (var existingRow in uniqueTableData)
                {

                    bool isRowDuplicate = row.All(pair =>
                        existingRow.ContainsKey(pair.Key) &&
                        existingRow[pair.Key] == pair.Value
                    );

                    if (isRowDuplicate)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    uniqueTableData.Add(row);
                }
            }


            foreach (var row in uniqueTableData)
            {

                foreach (var column in row)
                {

                    string columnName = column.Key;
                    string txtValue = column.Value;
                    string Colvalue = string.Empty;




                    if (columnName == "VillageName")
                    {
                        HttpContext.Current.Session["bEGVillageCode"] = null;
                        System.Text.RegularExpressions.Match match = Regex.Match(txtValue, @"\((\d+)\)");
                        if (match.Success)
                        {
                            string value = match.Groups[1].Value;
                            Colvalue = value;
                            if (columnName == "VillageName")
                            {
                                HttpContext.Current.Session["bEGVillageCode"] = Colvalue;
                            }

                        }
                    }
                    else
                    {

                        if ((columnName != "Block" && columnName != "District") && (!string.IsNullOrEmpty(txtValue) && columnName != "VillageType"))
                        {
                            System.Text.RegularExpressions.Match match = Regex.Match(columnName, @"\((\d+)\)");
                            if (match.Success)
                            {

                                string value = match.Groups[1].Value;
                                Colvalue = value;

                            }

                            if (HttpContext.Current.Session["bEGVillageCode"] != null)
                            {
                                bEGVillageCode = HttpContext.Current.Session["bEGVillageCode"].ToString();
                            }

                            SqlParameter[] parameters = new SqlParameter[]
                            {
                                new SqlParameter("@Action",2),
                                new SqlParameter("@bEGVillageCode",bEGVillageCode),
                                new SqlParameter("@AEGVillageCode",Colvalue),
                                new SqlParameter("@Value",txtValue),
                                   new SqlParameter("@CLusterCode",clustercode),
                            };

                            rowno = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters);

                        }

                        else
                        {

                            if (HttpContext.Current.Session["bEGVillageCode"] != null)
                            {
                                bEGVillageCode = HttpContext.Current.Session["bEGVillageCode"].ToString();
                            }

                            if ((columnName == "District") && !string.IsNullOrEmpty(txtValue))
                            {
                                var AEGVillageCode = GetCode(HttpContext.Current.Session["aVillageName"].ToString());
                                SqlParameter[] parameters1 = new SqlParameter[]
                               {
                                    new SqlParameter("@Action",3),
                                    new SqlParameter("@bEGVillageCode",bEGVillageCode),
                                    new SqlParameter("@AEGVillageCode", AEGVillageCode),
                                    new SqlParameter("@Value",txtValue),
                                       new SqlParameter("@CLusterCode",clustercode),
                               };
                                rowno = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters1);

                            }

                            if ((columnName == "Block") && !string.IsNullOrEmpty(txtValue))
                            {
                                var AEGVillageCode = GetCode(HttpContext.Current.Session["aVillageName"].ToString());
                                SqlParameter[] parameters1 = new SqlParameter[]
                               {
                                    new SqlParameter("@Action",5),
                                    new SqlParameter("@bEGVillageCode",bEGVillageCode),
                                    new SqlParameter("@AEGVillageCode", AEGVillageCode),
                                    new SqlParameter("@Value",txtValue),
                                       new SqlParameter("@CLusterCode",clustercode),
                               };
                                rowno = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parameters1);

                            }


                        }


                    }

                }
            }

            if (rowno > 0)
            {
                return "OK";
            }
            else
            {
                return "ERROR";
            }

        }
        catch (Exception ex)
        {

            return "Error: " + ex.Message;
        }
    }




    public static string GetCode(string CharValue)
    {
        string Value = string.Empty;
        System.Text.RegularExpressions.Match match = Regex.Match(CharValue.ToString(), @"\((\d+)\)");
        if (match.Success)
        {
            string value = match.Groups[1].Value;
            Value = value;
        }
        return Value;
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

            ddlCluster.Items.Clear();

        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlCluster.Items.Clear();

        }

    }

    public void UserLevelFilter()
    {





        string strQry = "";
        string Cond = "Module='Travel Matrix'";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtTravelMatrix = objMain.LoadData(strQry);

        if (dtTravelMatrix.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtTravelMatrix.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtTravelMatrix.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtTravelMatrix.Rows[0]["Delete_status"].ToString());

            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
        }
        //if (vDelete == true)
        //{

        //    btnDelete.Visible = true;
        //}
        //else
        //{

        //    btnDelete.Visible = false;
        //}

        //if (vADD == true)
        //{
        //    btnsave.Enabled = true;

        //}
        //else
        //{
        //    btnsave.Enabled = false;

        //}
        //if (vVerify == true)
        //{



        //}
        //if (vVerify == true || vADD == true)
        //{
        //    btnsave.Enabled = true;

        //}
        //else
        //{
        //    btnsave.Enabled = false;

        //}

    }
    public void LoadYear()
    {

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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();


    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBock();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();

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
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' ";
        objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddlCluster, "ClusterName", "ClusterCode", "--Select--");



    }
    protected void ddlCluster_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillFC();
    }
    public void FillFC()
    {
        conditions = "ActiveStatus =1 And UserLevel=24 ";
        if (ddlBlock.SelectedIndex > 0)
        {
            conditions += " and BlockCode ='" + ddlBlock.SelectedValue + "'  ";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            conditions += " and VillageCode ='" + ddlCluster.SelectedValue + "' ";
        }

        objComman.BindDLL("mstuser", "UserName  ,UserName +' ('+ FristName +')' as UserID ", conditions, "UserName", "asc", ddlFC, "UserID", "UserName", "Select");

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


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        TravelCostWithincluster = 0;
        TravelCostWithinclusterOut = 0;
        PerDiem = 0;
        Accommodation = 0;
        Conveyance = 0;
        Expanses = 0;
        sumFooterValue = 0;
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        GETDETAILS();
    }
    public void LoadData()
    {

        string con = "";
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }
        if (ddlMonth.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select month')</script>", false);
            return;
        }
        int mYear = 0;

        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        if (ddlBlock.SelectedIndex > 0)
        {
            con += " and mst3Block.BlockCode ='" + ddlBlock.SelectedValue + "'";
        }
        if (ddlCluster.SelectedIndex > 0)
        {
            con += " and mstCluster.ClusterCode ='" + ddlCluster.SelectedValue + "'";
        }
        if (ddlFC.SelectedIndex > 0)
        {
            con += "and tblTravelMatrixDeatils2024.UserId ='" + ddlFC.SelectedValue + "'";
        }
        con += "  and [mMonth]='" + ddlMonth.SelectedValue + "'  and [mYear]='" + mYear + "'";


        SqlParameter[] parm1 = new SqlParameter[]
      {
             new SqlParameter("@Con",""),


      };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadDataTravelFare", parm1);
        //  btnAdd.Visible = false;
        if (dt.Rows.Count > 0)
        {

            //gvTravekDatewise.DataSource = dt;
            //gvTravekDatewise.DataBind();

        }
        else
        {
            //gvTravekDatewise.DataSource = null;
            //gvTravekDatewise.DataBind();
        }
    }
    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblTotalPay = (Label)e.Row.FindControl("lblTotalPay");
            Label lbltotalExpens = (Label)e.Row.FindControl("TotalExpensBO");
            Label lblvehicle = (Label)e.Row.FindControl("lblvehicle");
            Label lblAccommodation = (Label)e.Row.FindControl("lblAccommodation");
            Label lblPerDim = (Label)e.Row.FindControl("lblPerDim");
            Label lblClusteroutTotalAmountKM = (Label)e.Row.FindControl("lblClusteroutTotalAmountKM");
            Label lblClusterTotalAmountKM = (Label)e.Row.FindControl("lblClusterTotalAmountKM");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            LinkButton LinkButton1 = (LinkButton)e.Row.FindControl("LinkButton1");

            if (Convert.ToString(Session["user_level"]) == "128")
            {
                if (lblStatus.Text == "3")
                {
                    LinkButton1.Text = "Unhold";
                }
                if (lblStatus.Text == "5")
                {
                    LinkButton1.Text = "Hold";
                }
            }
            if (Convert.ToString(Session["user_level"]) == "91")
            {
                if (lblStatus.Text == "4")
                {
                    LinkButton1.Text = "Rejected";
                }

            }
            if (Convert.ToString(Session["user_level"]) == "124")
            {
                if (lblStatus.Text == "6")
                {
                    LinkButton1.Text = "Rejected";
                    LinkButton1.Enabled = true;
                }
                else
                {
                    LinkButton1.Text = "Approve";
                    LinkButton1.Enabled = false;
                }

            }
            if (lblTotalPay.Text != "")
            {
                sumFooterValue += Convert.ToInt32(lblTotalPay.Text);
            }
            if (lbltotalExpens.Text != "")
            {
                Expanses += Convert.ToInt32(lbltotalExpens.Text);
            }
            if (lblvehicle.Text != "")
            {
                Conveyance += Convert.ToInt32(lblvehicle.Text);
            }
            if (lblAccommodation.Text != "")
            {
                Accommodation += Convert.ToInt32(lblAccommodation.Text);
            }
            if (lblPerDim.Text != "")
            {
                PerDiem += Convert.ToInt32(lblPerDim.Text);
            }
            if (lblClusteroutTotalAmountKM.Text != "")
            {
                TravelCostWithinclusterOut += Convert.ToInt32(lblClusteroutTotalAmountKM.Text);
            }
            if (lblClusterTotalAmountKM.Text != "")
            {
                TravelCostWithincluster += Convert.ToInt32(lblClusterTotalAmountKM.Text);
            }



        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotal");
            lbl.Text = sumFooterValue.ToString();
            Label lbltotalExpens = (Label)e.Row.FindControl("lbltotalExpens");
            lbltotalExpens.Text = Expanses.ToString();
            Label lbltotalvehicle = (Label)e.Row.FindControl("lbltotalvehicle");
            lbltotalvehicle.Text = Conveyance.ToString();
            Label lbltotalAccommodation = (Label)e.Row.FindControl("lbltotalAccommodation");
            lbltotalAccommodation.Text = Accommodation.ToString();
            Label lbltotalPerDim = (Label)e.Row.FindControl("lbltotalPerDim");
            lbltotalPerDim.Text = PerDiem.ToString();
            Label lbltotalClusteroutTotalAmountKM = (Label)e.Row.FindControl("lbltotalClusteroutTotalAmountKM");
            lbltotalClusteroutTotalAmountKM.Text = TravelCostWithinclusterOut.ToString();

            Label lbltotalClusterTotalAmountKM = (Label)e.Row.FindControl("lbltotalClusterTotalAmountKM");
            lbltotalClusterTotalAmountKM.Text = TravelCostWithincluster.ToString();
        }
    }


    public void LoadDataDeatils(string Fdate, string Todate)
    {


        if (ddlFC.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select FC')</script>", false);
            return;
        }
        if (ddlMonth.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select month')</script>", false);
            return;
        }
        int mYear = 0;

        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        SqlParameter[] parm1 = new SqlParameter[]
      {
           new SqlParameter("@Fromdate", Convert.ToDateTime(Fdate).ToString("yyyy-MM-dd")),
            new SqlParameter("@Todate", Convert.ToDateTime(Todate).ToString("yyyy-MM-dd")),
             new SqlParameter("@UserName", ddlFC.SelectedValue),
             new SqlParameter("@month", ddlMonth.SelectedValue),
              new SqlParameter("@Myear",mYear),
                 new SqlParameter("@UserRole",Convert.ToString(Session["user_level_Role"])),


      };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024", parm1);
        //if (dt.Rows.Count > 0)
        //{
        //    gvTravekDatewise.DataSource = dt;
        //    gvTravekDatewise.DataBind();
        //}
        //else
        //{
        //    gvTravekDatewise.DataSource = null;
        //    gvTravekDatewise.DataBind();
        //}

    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        Session["Scode"] = ddlState.SelectedValue;
        Session["Dcode"] = ddlDistrict.SelectedValue;
        Session["Bcode"] = ddlBlock.SelectedValue;

        Session["Ccode"] = ddlCluster.SelectedValue;
        Session["FCcode"] = ddlFC.SelectedValue;
        Session["MMmonth"] = ddlMonth.SelectedValue;

        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblPlanUniqueCode") as Label).Text;
        string FFlag = "2";
        Response.Redirect("~/frmTravelMatrixWithClusters.aspx?ID=" + ddlCluster.SelectedValue + "," + ddlMonth.SelectedValue + "," + ddlFC.SelectedValue + "," + FFlag + "," + UniqueChildCode + "");

    }

    protected void lnl_Action(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        LinkButton ddlLabTest1 = (LinkButton)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        Label lblFromNo = (Label)row1.FindControl("lblFromNo");
        Label lblMyear = (Label)row1.FindControl("lblMyear");
        Label lblUserID = (Label)row1.FindControl("lblUserID");
        LinkButton LinkButton1 = (LinkButton)row1.FindControl("LinkButton1");
        int Icount = 0;
        int Status = 0;
        string Flag = "";
        if (Convert.ToString(Session["user_level"]) == "128")
        {
            string hh = "";
            if (Convert.ToString(Session["user_level"]) == "128" && LinkButton1.Text == "Unhold")
            {
                Status = 5;
                Flag = "1";
                hh = "Hold";
            }
            if (Convert.ToString(Session["user_level"]) == "128" && LinkButton1.Text == "hold")
            {
                Status = 3;
                Flag = "2";
                hh = "UnHold";
            }
            int mYear = 0;



            SqlParameter[] cmdParameters1 = new SqlParameter[]
                              {
                        new SqlParameter("@FromNo", lblFromNo.Text),
                          new SqlParameter("@mYear",""+mYear +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserID.Text +" "),
                          new SqlParameter("@Status", Status),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                    new SqlParameter("@Flag",Flag),

                              };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixApproveHold", cmdParameters1);

            if (Icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + hh + " sucessfully')</script>", false);

                if (Convert.ToString(Session["user_level"]) == "128" && LinkButton1.Text == "Unhold")
                {
                    LinkButton1.Text = "hold";
                }
                else if (Convert.ToString(Session["user_level"]) == "128" && LinkButton1.Text == "hold")
                {
                    LinkButton1.Text = "Unhold";
                }
            }
        }
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            lblFromNoEdit.Text = lblFromNo.Text;
            lblUserIDEdit.Text = lblUserID.Text;
            txtResone.Text = "";
            MPE_Entry.Show();
        }
        if (Convert.ToString(Session["user_level"]) == "124")
        {
            lblFromNoEdit.Text = lblFromNo.Text;
            lblUserIDEdit.Text = lblUserID.Text;
            txtResone.Text = "";
            MPE_Entry.Show();
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int mYear = 0;
        int Status = 0;
        if (Convert.ToString(Session["user_level"]) == "91")
        {
            Status = 7;
        }
        if (Convert.ToString(Session["user_level"]) == "124")
        {
            Status = 9;
        }
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            mYear = Convert.ToInt32(ddlYear.SelectedValue);
        }
        int Icount = 0;
        SqlParameter[] cmdParameters1 = new SqlParameter[]
                            {
                        new SqlParameter("@FromNo", lblFromNoEdit.Text),
                          new SqlParameter("@mYear",""+mYear +" "),
                         new SqlParameter("@mMonth",""+ddlMonth.SelectedValue +" "),

                        new SqlParameter("@UserID", ""+ lblUserIDEdit.Text +" "),
                          new SqlParameter("@Status", Status),

                                new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                     new SqlParameter("@Remark", txtResone.Text),


                            };
        Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixReject", cmdParameters1);

        if (Icount > 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reject sucessfully')</script>", false);
            LoadData();
        }

    }




}