using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
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
using System.Windows.Documents;
public partial class GISMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

    }

    public static readonly string ConnStr = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ConStr"]);

    #region WebMethods

    [WebMethod]
    public static List<MISVillageDto> GetMISVillages(string query = null, string year = null, string state = null, string district = null, string block = null, int status = 0)
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
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_MSTVillages", p);

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new MISVillageDto
            {
                VillageCode = row["VillageCode"].ToString(),
                VCode = row["VCode"].ToString(),
                VillageName = row["VillageName"].ToString(),
                lat = row["lat"].ToString(),
                lon = row["lon"].ToString(),
                AdminDistrictName = row["AdminDistrictName"].ToString(),
                MainBlockName = row["MainBlockName"].ToString()
            });
        }

        return list;
    }

    

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

            if(dt.Rows.Count > 0)
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
    public static List<MappedVillages> GetMappingVillages(string misName, string egCode, string fyear, string district, string block)
    {
        var list = new List<MappedVillages>();

        SqlParameter[] p = new SqlParameter[] {
              new SqlParameter("@villagename",misName),
              new SqlParameter("@egvillagecode",egCode),
              new SqlParameter("@fyear",fyear),
              new SqlParameter("@districtname",district),
               new SqlParameter("@blockname",block)
        };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GET_Mapping_Suggestions", p);

        foreach (DataRow row in dt.Rows)
        {
            list.Add(new MappedVillages
            {
                SlNo = row["SlNo"].ToString(),
                VillageID = row["VillageID"].ToString(),
                GISVillageName = row["GISVillageName"].ToString(),
                DistrictName = row["DistrictName"].ToString(),
                BlockName = row["BlockName"].ToString(),
                DistanceKM = row["DistanceKM"].ToString(),
                EG_VillageCode = row["EGVillageCode"].ToString(),
                MatchScore = Convert.ToInt32(row["MatchScore"]),
                Flag = Convert.ToInt32(row["Flag"]),
                lat = row["lat"].ToString(),
                lon = row["lon"].ToString()
            });
        }

        return list;
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
    public static string GetDistrictVillages(string district,string districtname)
    {
        // ✅ FORCE TLS 1.2 (CRITICAL FIX)
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string url =
            "https://geo1server.educategirls.ngo/geoserver/EGTest/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EGTest:lyr_layer_Villages" +
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
    public static string GetBlockVillages(string district, string block)
    {
        // ✅ FIX TLS 1.2 for .NET 4.0
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string url =
            "https://geo1server.educategirls.ngo/geoserver/EGTest/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EGTest:lyr_layer_block_Villages" +
            "&maxFeatures=5000&outputFormat=application/json" +
            "&viewparams=BlockCode:" + HttpUtility.UrlEncode(block);

        using (WebClient wc = new WebClient())
        {
            wc.Headers.Add(HttpRequestHeader.Accept, "application/json");
            wc.Encoding = Encoding.UTF8;
            wc.Proxy = null; // avoids proxy-related connection failures

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
            "https://geo1server.educategirls.ngo/geoserver/EGTest/ows" +
            "?service=WFS&version=1.0.0&request=GetFeature" +
            "&typeName=EGTest:lyr_Village_Mapping_Suggestions" +
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

    #endregion

        #region DTOs
    public class MISVillageDto
    {
        public string VillageCode { get; set; }
        public string VCode { get; set; }
        public string VillageName { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string AdminDistrictName { get; set; }
        public string MainBlockName { get; set; }

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

    public class MappedVillages { public string SlNo; public string VillageID; public string GISVillageName; public string DistrictName; public string BlockName; public string DistanceKM; public string EG_VillageCode; public int MatchScore; public int Flag; public string lat; public string lon; }

    public class MappingDto { public string MapID; public string MISVillageID; public string MISVillageName; public string LayerVillageID; public string LayerVillageName; }
    public class SuggestResponse { public LayerVillageDto LayerVillage; public double Score; }
    public class SuggestLayerForMIS { public LayerVillageDto LayerVillage; public double Score; }
    public class SuggestMISForLayer { public MISVillageDto MISVillage; public double Score; }
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