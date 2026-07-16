using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class GISVillageMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        if (!IsPostBack && Request.QueryString["download"] == "1")
        {
            DataTable dt = Session["EXPORT_DATA"] as DataTable;

            if (dt != null)
            {
                ExportToExcel(dt, 1);
            }
        }

        if (!IsPostBack && Request.QueryString["download"] == "2")
        {
            DataTable dt = Session["EXPORT_DATA_MAPPED"] as DataTable;

            if (dt != null)
            {
                ExportToExcel(dt, 2);
            }
        }

    }

    public static readonly string ConnStr = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ConStr"]);

    #region WebMethods

    [WebMethod(EnableSession = true)]
    public static string RunFuzzyLogic(string fyear, string district, string block)
    {
        SqlParameter[] p = new SqlParameter[] {
        new SqlParameter("@fyear",fyear),
        new SqlParameter("@districtcode",district),
        new SqlParameter("@blockcode",block)
    };

        DataTable dt = SqlHelper.GetDataTable(
            SqlHelper.mainConnectionString,
            CommandType.StoredProcedure,
            "sp_runfuzzymapping", p);

        if (dt.Rows.Count == 0)
            return "NO_DATA";

        // Store data temporarily (Session)
        HttpContext.Current.Session["EXPORT_DATA"] = dt;

        return "READY";
    }

    [WebMethod(EnableSession = true)]
    public static string ExportMappedData(string fyear, string district, string block)
    {
        SqlParameter[] p = new SqlParameter[] {
        new SqlParameter("@year",fyear),
        new SqlParameter("@district",district),
        new SqlParameter("@block",block)
    };

        DataTable dt = SqlHelper.GetDataTable(
            SqlHelper.mainConnectionString,
            CommandType.StoredProcedure,
            "Get_MappedVillagesReport", p);

        if (dt.Rows.Count == 0)
            return "NO_DATA";

        // Store data temporarily (Session)
        HttpContext.Current.Session["EXPORT_DATA_MAPPED"] = dt;

        return "READY";
    }


    private void ExportToExcel(DataTable dt, int flag)
    {
        string filename = "";
        if (flag == 1)
        {
            filename = "VillageMappingSuggestions";
        }
        if (flag == 2)
        {
            filename = "MappedVillages";
        }
        DataTable exportDt = dt;

        System.Web.UI.WebControls.GridView gv =
        new System.Web.UI.WebControls.GridView();

        gv.DataSource = exportDt;
        gv.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
        Response.ContentType = "application/vnd.ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gv.RenderControl(hw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }

    [WebMethod]
    public static List<MISVillageDto> GetMISVillages(string query = null, string year = null, string state = null, string district = null, string block = null, int status = 1)
    {
        var list = new List<MISVillageDto>();

        SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@q",(object)query ?? DBNull.Value),
              new SqlParameter("@year",year),
               new SqlParameter("@state",(object)state ?? DBNull.Value),
                new SqlParameter("@district",(object)district ?? DBNull.Value),
                 new SqlParameter("@block",(object)block ?? DBNull.Value),
                 new SqlParameter("@status",(object)status ?? DBNull.Value)
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_MappedVillages", p);

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new MISVillageDto
            {
                SN = row["SN"].ToString(),
                EGVillageCode = row["EGVillageCode"].ToString(),
                MatchingID = row["MatchingID"].ToString(),
                EGVillageName = row["EGVillageName"].ToString(),
                GISVillageID = row["GISVillageID"].ToString(),
                GISVillageName = row["GISVillageName"].ToString(),
                Lat = row["Lat"].ToString(),
                Lon = row["Lon"].ToString(),
                AdminDistrictName = row["AdminDistrictName"].ToString(),
                MainBlockName = row["MainBlockName"].ToString(),
                GISDistrictName = row["GISDistrictName"].ToString(),
                GISBlockName = row["GISBlockName"].ToString()
            });
        }

        return list;
    }

    [WebMethod]
    public static string SaveMappings(string csvValues, string fyear)
    {
        if (string.IsNullOrWhiteSpace(csvValues))
            return "No data provided.";

        var values = csvValues
                        .Split(',')
                        .Select(v => v.Trim())
                        .Where(v => !string.IsNullOrEmpty(v))
                        .Distinct()
                        .ToList();

        int total = values.Count;
        int success = 0;
        int failed = 0;

        foreach (var val in values)
        {
            // Split by hyphen
            string[] parts = val.Split('-');

            if (parts.Length != 2)
            {
                failed++;
                continue;
            }

            string villageCode = parts[0].Trim();
            string villageIdStr = parts[1].Trim();

            string villageId;
            //if (!string.TryParse(villageIdStr, out villageId))
            //{
            //    failed++;
            //    continue;
            //}

            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@VillageCode", villageCode),
            new SqlParameter("@VillageId", villageIdStr),
            new SqlParameter("@fyear", fyear),
            new SqlParameter("@RowsUpdated", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            SqlHelper.ExecuteNonQuery(
                SqlHelper.mainConnectionString,
                CommandType.StoredProcedure,
                "usp_UpdateVillageMapping",
                parameters
            );

            int rowsUpdated = Convert.ToInt32(parameters[3].Value);

            if (rowsUpdated > 0)
                success++;
            else
                failed++;
        }

        // Return final summary
        return string.Format(
            "Processing completed. Total: {0}, Successful: {1}, Failed: {2}.",
            total, success, failed
        );
    }

    [WebMethod]
    public static int DeleteMapping(string villageCode, string fyear)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[]
            {
            new SqlParameter("@villageCode", villageCode),
            new SqlParameter("@fyear", fyear)
            };

            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Remove_Mapping", p);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["RowsUpdated"]);
            }

            return 0;
        }
        catch (Exception)
        {
            return -1; // error
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetUnmappedVillages(
    string query,
    string year,
    string state,
    string district,
    string block,
    int status)
    {
        var sb = new StringBuilder();

        SqlParameter[] p = new SqlParameter[]
        {
        new SqlParameter("@q", (object)query ?? DBNull.Value),
        new SqlParameter("@year", year),
        new SqlParameter("@state", (object)state ?? DBNull.Value),
        new SqlParameter("@district", (object)district ?? DBNull.Value),
        new SqlParameter("@block", (object)block ?? DBNull.Value),
        new SqlParameter("@status", status)
        };

        DataTable dt = SqlHelper.GetDataTable(
            SqlHelper.mainConnectionString,
            CommandType.StoredProcedure,
            "Get_UnmappedVillages",
            p
        );

        if (dt.Rows.Count == 0)
            return "<div class='small' style='margin-left:30px;'>No unmapped villages</div>";

        sb.Append(@"
<table id='tblLocDetails1'
       class='table table-hover table-bordered table-striped'
       style='width:100%'>
<thead>
<tr>
<th>SN</th>
    <th>Block</th>
    <th>Admin District</th>
    <th>Admin Block</th>
    <th>Village</th>
    <th>EG Village Code</th>
    <th>Layer Village Code</th>
</tr>
</thead>
<tbody>");
        foreach (DataRow row in dt.Rows)
        {
            sb.Append(@"
<tr class='mis-row'
    data-id='" + row["VillageCode"] + @"'
    data-name='" + row["VillageName"] + @"'
    data-admindistrictname='" + row["AdminDistrictName"] + @"'
    data-mainblockname='" + row["AdminBlockName"] + @"'>

<td>" + row["SN"] + @"</td>
    <td>" + row["BlockName"] + @"</td>
    <td>" + row["AdminDistrictName"] + @"</td>
    <td>" + row["AdminBlockName"] + @"</td>
    <td>" + row["VillageName"] + @"</td>
    <td>" + row["VillageCode"] + @"</td>
    <td>
        <input type='text'
               class='form-control gis-code'
               maxlength='10'
               onkeypress='return onlyNumbers(event)' />
    </td>
</tr>");
        }

        sb.Append("</tbody></table>");

        return sb.ToString();
    }


    //[WebMethod]
    //public static List<UnmappedVillageDto> GetGISVillages(string query = null, string year = null, string state = null, string district = null, string block = null, int status = 0)
    //{
    //    var list = new List<UnmappedVillageDto>();

    //    SqlParameter[] p = new SqlParameter[] {
    //        new SqlParameter("@q",(object)query ?? DBNull.Value),
    //          new SqlParameter("@year",year),
    //           new SqlParameter("@state",(object)state ?? DBNull.Value),
    //            new SqlParameter("@district",(object)district ?? DBNull.Value),
    //             new SqlParameter("@block",(object)block ?? DBNull.Value),
    //             new SqlParameter("@status",2)
    //    };
    //    DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_MSTVillages", p);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        list.Add(new UnmappedVillageDto
    //        {
    //            VillageCode = row["VillageCode"].ToString(),
    //            VCode = row["VCode"].ToString(),
    //            VillageName = row["VillageName"].ToString(),
    //            lat = row["lat"].ToString(),
    //            lon = row["lon"].ToString(),
    //            AdminDistrictName = row["AdminDistrictName"].ToString(),
    //            MainBlockName = row["MainBlockName"].ToString()
    //        });
    //    }

    //    return list;
    //}
    ////[WebMethod]
    //public static List<MappedVillages> GetMappingVillages(string misName, string egCode, string fyear, string district, string block)
    //{
    //    var list = new List<MappedVillages>();

    //    SqlParameter[] p = new SqlParameter[] {
    //          new SqlParameter("@villagename",misName),
    //          new SqlParameter("@egvillagecode",egCode),
    //          new SqlParameter("@fyear",fyear),
    //          new SqlParameter("@districtname",district),
    //           new SqlParameter("@blockname",block)
    //    };
    //    DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GET_Mapping_Suggestions", p);

    //    foreach (DataRow row in dt.Rows)
    //    {
    //        list.Add(new MappedVillages
    //        {
    //            SlNo = row["SlNo"].ToString(),
    //            VillageID = row["VillageID"].ToString(),
    //            GISVillageName = row["GISVillageName"].ToString(),
    //            DistrictName = row["DistrictName"].ToString(),
    //            BlockName = row["BlockName"].ToString(),
    //            DistanceKM = row["DistanceKM"].ToString(),
    //            EG_VillageCode = row["EGVillageCode"].ToString(),
    //            MatchScore = Convert.ToInt32(row["MatchScore"]),
    //            Flag = Convert.ToInt32(row["Flag"]),
    //            lat = row["lat"].ToString(),
    //            lon = row["lon"].ToString()
    //        });
    //    }

    //    return list;
    //}


    [WebMethod]
    public static string SaveVillages(List<VillageSaveDto> villages)
    {
        try
        {
            foreach (var v in villages)
            {
                SqlParameter[] p = new SqlParameter[]
                {
                new SqlParameter("@EGVillageCode", v.egVillageCode),
                new SqlParameter("@VillageName", v.VillageName),
                new SqlParameter("@GISVillageCode", v.VillageCode)
                };

                SqlHelper.ExecuteNonQuery(
                    SqlHelper.mainConnectionString,
                    CommandType.StoredProcedure,
                    "Save_VillageMapping",   // your stored procedure
                    p
                );
            }

            return "SUCCESS";
        }
        catch (Exception ex)
        {
            return "ERROR: " + ex.Message;
        }
    }

    [System.Web.Services.WebMethod]
    public static string SaveSuggestedVillages(List<VillageMappingVM> villages)
    {
        if (villages == null || villages.Count == 0)
            return "No data received";

        foreach (var v in villages)
        {

            SqlParameter[] p = new SqlParameter[]
            {
                new SqlParameter("@GISVillageCode", v.VillageID),
                new SqlParameter("@VillageName", v.VillageName),
                new SqlParameter("@EGVillageCode", v.EG_VillageCode)
            };

            SqlHelper.ExecuteNonQuery(
                SqlHelper.mainConnectionString,
                CommandType.StoredProcedure,
                "Save_VillageMapping",   // your stored procedure
                p
            );


        }

        return "SUCCESS";
    }


    [WebMethod]
    public static string getadmindistrict(string district)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[] {
              new SqlParameter("@districtCode",district),
              new SqlParameter("@blockCode","")
        };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "getAdminDistrictBlock", p);

            if (dt.Rows.Count > 0)
            {
                string admindistrictname = Convert.ToString(dt.Rows[0]["AdminDistrictName"]);
                return admindistrictname;
            }
            else
            {
                return district;
            }

        }
        catch (Exception ex)
        {
            return "ERROR: " + ex.Message;
        }
    }


    [WebMethod]
    public static string getadminBlock(string district, string block)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[] {
              new SqlParameter("@districtCode",district),
              new SqlParameter("@blockCode",block)
        };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "getAdminDistrictBlock", p);

            if (dt.Rows.Count > 0)
            {
                string adminblockname = Convert.ToString(dt.Rows[0]["MainBlockName"]);
                return adminblockname;
            }
            else
            {
                return block;
            }

        }
        catch (Exception ex)
        {
            return "ERROR: " + ex.Message;
        }
    }

    [WebMethod]
    public static object SaveVillageMappings(List<MapSaveDto> list)
    {
        try
        {
            foreach (var item in list)
            {
                SqlParameter[] p = new SqlParameter[]
                {
                new SqlParameter("@EGVillageCode", item.EGVillageCode),
                new SqlParameter("@GISVillageCode", item.VillageID)
                };

                SqlHelper.ExecuteNonQuery(
                    SqlHelper.mainConnectionString,
                    CommandType.StoredProcedure,
                    "Save_VillageMapping",   // your stored procedure
                    p
                );
            }
            return "SUCCESS";
        }
        catch (Exception ex)
        {
            return "ERROR: " + ex.Message;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetMappingVillages(string fyear, string district, string block)
    {
        var sb = new StringBuilder();

        SqlParameter[] p = new SqlParameter[]
        {
        new SqlParameter("@fyear", fyear),
        new SqlParameter("@district", district),
        new SqlParameter("@block", block)
        };

        DataTable dt = SqlHelper.GetDataTable(
            SqlHelper.mainConnectionString,
            CommandType.StoredProcedure,
            "GET_GIS_Villages",
            p
        );

        if (dt.Rows.Count == 0)
            return "<div class='small' style='margin-left:30px;'>No suggestions</div>";

        sb.Append(@"
<table id='suggestTable'
       class='table table-hover table-bordered table-striped'
       style='table-layout:fixed;width:100%'>
<thead>
<tr>
<th>SN</th>
    <th>Block</th>
    <th>Village ID</th>
    <th>Village Name</th>
</tr>
</thead>
<tbody>");

        foreach (DataRow row in dt.Rows)
        {
            sb.Append(@"
<tr class='suggest-row'
    data-villageid='" + row["VillageID"] + @"'
    data-villagename='" + row["VillageName"] + @"'
    data-district='" + row["DistrictName"] + @"'
    data-block='" + row["BlockName"] + @"'
    data-lat='" + row["Lat"] + @"'
    data-lon='" + row["Lon"] + @"'>

<td>" + row["SN"] + @"</td>
    <td>" + row["BlockName"] + @"</td>
    <td>" + row["VillageID"] + @"</td>
    <td>" + row["VillageName"] + @"</td>
</tr>");
        }

        sb.Append("</tbody></table>");

        return sb.ToString();
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<object> GetVillagePolygon(string villageCode)
    {
        var list = new List<object>();

        string query = @"SELECT Lat, Lon FROM tbl_VillageLocation2024 WHERE VillageCode = @VillageCode ORDER BY SNO";
        using (var con = new SqlConnection(ConnStr))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@VillageCode", villageCode);
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(new { Lat = dr["Lat"], Lon = dr["Lon"] });
                    }
                }
            }
        }

        return list;
    }

    [WebMethod]
    public static string GetDistrictVillages(string district, string districtname)
    {
        // ✅ FORCE TLS 1.2 (CRITICAL FIX)
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string url =
            "https://geo1server.educategirls.ngo/geoserver/EG/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EG:lyr_layer_Villages" +
            "&maxFeatures=5000&outputFormat=application/json" +
            "&viewparams=DistrictCode:" + HttpUtility.UrlEncode(district) +
            ";DistrictName:" + HttpUtility.UrlEncode(districtname);

        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add(HttpRequestHeader.Accept, "application/json");
            wc.Encoding = System.Text.Encoding.UTF8;

            return wc.DownloadString(url);
        }
    }

    [WebMethod]
    public static string GetBlockVillages(string district, string block, string fyear)
    {
        // ✅ FIX TLS 1.2 for .NET 4.0
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string url =
            "https://geo1server.educategirls.ngo/geoserver/EG/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EG:lyr_layer_block_Villages" +
            "&maxFeatures=5000&outputFormat=application/json" +
            "&viewparams=BlockCode:" + HttpUtility.UrlEncode(block) +
            ";DistrictCode:" + HttpUtility.UrlEncode(district) +
            ";FYear:" + HttpUtility.UrlEncode(fyear);

        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add(HttpRequestHeader.Accept, "application/json");
            wc.Encoding = Encoding.UTF8;
            wc.Proxy = null; // avoids proxy-related connection failures

            return wc.DownloadString(url);
        }
    }

    [WebMethod]
    public static string GetMappedVillage(string villageid)
    {
        // ✅ TLS 1.2 fix for .NET 4.0
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string url =
            "https://geo1server.educategirls.ngo/geoserver/EG/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EG:vw_mappedVillage" +
            "&maxFeatures=50&outputFormat=application/json" +
            "&viewparams=villageid:" + HttpUtility.UrlEncode(villageid);

        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add(HttpRequestHeader.Accept, "application/json");
            wc.Encoding = Encoding.UTF8;
            wc.Proxy = null;

            return wc.DownloadString(url);
        }
    }

    [WebMethod]
    public static string GetVillageMappingSuggestions(
    string villagename,
    string egvillagecode,
    string fyear,
    string districtname,
    string blockname)
    {
        // ✅ TLS 1.2 fix for .NET 4.0
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string url =
            "https://geo1server.educategirls.ngo/geoserver/EG/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EG:lyr_Village_Mapping_Suggestions" +
            "&maxFeatures=50&outputFormat=application/json" +
            "&viewparams=villagename:" + HttpUtility.UrlEncode(villagename) +
            ";egvillagecode:" + HttpUtility.UrlEncode(egvillagecode) +
            ";fyear:" + HttpUtility.UrlEncode(fyear) +
            ";districtname:" + HttpUtility.UrlEncode(districtname) +
            ";blockname:" + HttpUtility.UrlEncode(blockname);

        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add(HttpRequestHeader.Accept, "application/json");
            wc.Encoding = Encoding.UTF8;
            wc.Proxy = null;

            return wc.DownloadString(url);
        }
    }

    [WebMethod]
    public static string Map_SavedLayer(string LayerType, string layerid)
    {
        try
        {
            // Define parameters for the stored procedure
            SqlParameter[] p = new SqlParameter[] {
                new SqlParameter("@LayerType", LayerType),
                new SqlParameter("@LayerID", layerid)
            };

            // Call your stored procedure and return the inserted ID
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Save_MapedLayer", p);

            if (dt.Rows.Count > 0)
            {
                return "Mapping Successful";
            }
            else
            {
                return "Mapping Successful";
            }
        }
        catch
        {
            // Log error (you can add your own logging mechanism here)
            //return "Error: " + ex.Message;
            return "Mapping Un-Successful";
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string RenderMisTable(List<MisDto> list)
    {
        if (list == null)
            return "<div style='color:red'>LIST IS NULL</div>";

        var sb = new StringBuilder();

        sb.Append(@"
            <table id='tblLocDetails'
                   class='table table-hover table-bordered table-striped'
                   style='width:100%'>
            <thead>
         <tr>
                <th>SN</th>
                <th>MatchingID</th>
                <th>EG Village Name</th>
                <th>Admin Block</th>
                <th>Layer Village ID</th>
                <th>Layer Village Name</th>
                <th>Layer Block</th>
                <th>Lat</th>
                <th>Long</th>
                <th>Action</th>
            </tr>
            </thead>
            <tbody>");

        foreach (var v in list)
        {
            string lon = string.IsNullOrEmpty(v.Lon) ? "" : v.Lon;

            sb.Append(@"
            <tr class='mis-row'
                data-villagecode='" + v.EGVillageCode + @"'
                data-giscode='" + v.GISVillageID + @"'
                data-lat='" + v.Lat + @"'
                data-lon='" + lon + @"'>

                <td>" + v.SN + @"</td>
                <td>" + v.MatchingID + @"</td>
                <td>" + v.EGVillageName + @"</td>
                <td>" + v.MainBlockName + @"</td>
                <td>" + v.GISVillageID + @"</td>
                <td>" + v.GISVillageName + @"</td>
                <td>" + v.GISBlockName + @"</td>
                <td>" + v.Lat + @"</td>
                <td>" + lon + @"</td>
                <td>
                    <button type='button' class='btn btn-danger btn-sm delete-btn'>
                        <i class='fa fa-trash-o'></i>
                    </button>
                </td>
            </tr>");
        }

        sb.Append("</tbody></table>");

        return sb.ToString();
    }



    #endregion

    #region DTOs
    public class MisDto
    {
        public string SN { get; set; }
        public string AdminDistrictName { get; set; }
        public string EGVillageCode { get; set; }
        public string EGVillageName { get; set; }
        public string GISBlockName { get; set; }
        public string GISDistrictName { get; set; }
        public string GISVillageID { get; set; }
        public string GISVillageName { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string MainBlockName { get; set; }
        public string MatchingID { get; set; }
    }



    public class VillageMappingVM
    {
        public int VillageID { get; set; }
        public string VillageName { get; set; }
        public string District { get; set; }
        public string Block { get; set; }
        public string EG_VillageCode { get; set; }
    }

    public class UnmappedVillageDto
    {

        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string AdminDistrictName { get; set; }
        public string AdminBlockName { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }


    }
    public class MappingRequest
    {
        public string CommaSeparatedValues { get; set; }
    }

    public class MISVillageDto
    {
        public string SN { get; set; }
        public string EGVillageCode { get; set; }
        public string MatchingID { get; set; }
        public string EGVillageName { get; set; }
        public string GISVillageID { get; set; }
        public string GISVillageName { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string AdminDistrictName { get; set; }
        public string MainBlockName { get; set; }
        public string GISDistrictName { get; set; }
        public string GISBlockName { get; set; }

    }
    public class VillageSaveDto
    {
        public string egVillageCode { get; set; }   // MIS Village Code
        public string VillageName { get; set; }
        public string VillageCode { get; set; }     // EG Village Code
    }
    public class MapSaveDto
    {
        public int VillageID { get; set; }
        public string EGVillageCode { get; set; }
    }
    public class LayerVillageDto { public string VillageCode; public string VillageName; }

    public class MappedVillages { public string StateName; public string VillageID; public string VillageName; public string DistrictName; public string BlockName; public string Lat; public string Lon; }

    public class MappingDto { public string MapID; public string MISVillageID; public string MISVillageName; public string LayerVillageID; public string LayerVillageName; }
    public class SuggestResponse { public LayerVillageDto LayerVillage; public double Score; }
    public class SuggestLayerForMIS { public LayerVillageDto LayerVillage; public double Score; }
    #endregion

    #region String similarity (Jaro-Winkler)
    // Implementation adapted for readability. Good enough for name matching; tweak threshold as needed.
    public static double JaroWinkler(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1))
            return string.IsNullOrEmpty(s2) ? 1.0 : 0.0;
        if (string.IsNullOrEmpty(s2))
            return 0.0;

        double jaro = JaroDistance(s1, s2);

        // Winkler adjustment for prefixes
        int prefix = 0;
        int prefixLimit = 4;
        for (int i = 0; i < Math.Min(Math.Min(s1.Length, s2.Length), prefixLimit); i++)
        {
            if (s1[i] == s2[i]) prefix++;
            else break;
        }
        double scaling = 0.1;
        return jaro + prefix * scaling * (1 - jaro);
    }

    private static double JaroDistance(string s1, string s2)
    {
        int s1Len = s1.Length;
        int s2Len = s2.Length;

        if (s1Len == 0 || s2Len == 0) return 0;

        int matchDistance = Math.Max(s1Len, s2Len) / 2 - 1;

        bool[] s1Matches = new bool[s1Len];
        bool[] s2Matches = new bool[s2Len];

        int matches = 0;
        for (int i = 0; i < s1Len; i++)
        {
            int start = Math.Max(0, i - matchDistance);
            int end = Math.Min(i + matchDistance + 1, s2Len);
            for (int j = start; j < end; j++)
            {
                if (s2Matches[j]) continue;
                if (s1[i] != s2[j]) continue;
                s1Matches[i] = true;
                s2Matches[j] = true;
                matches++;
                break;
            }
        }

        if (matches == 0) return 0.0;

        double t = 0;
        int k = 0;
        for (int i = 0; i < s1Len; i++)
        {
            if (!s1Matches[i]) continue;
            while (!s2Matches[k]) k++;
            if (s1[i] != s2[k]) t++;
            k++;
        }
        t /= 2.0;

        return (matches / (double)s1Len + matches / (double)s2Len + (matches - t) / matches) / 3.0;
    }
    #endregion
}