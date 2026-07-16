using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using System;
using System.Activities.Debugger;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using System.Windows.Documents;

public partial class VillageMapping : System.Web.UI.Page
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

    //[WebMethod]
    //public static List<MISVillageDto> GetMISVillages(string query = null)
    //{
    //    var list = new List<MISVillageDto>();
    //    using (var con = new SqlConnection(ConnStr))
    //    using (var cmd = new SqlCommand("SELECT  VillageCode, VillageName, DistrictCode FROM mst5Village WHERE (@q IS NULL OR VillageName LIKE '%' + @q + '%' ) ORDER BY VillageName", con))
    //    {
    //        cmd.Parameters.AddWithValue("@q", (object)query ?? DBNull.Value);
    //        con.Open();
    //        using (var r = cmd.ExecuteReader())
    //        {
    //            while (r.Read())
    //            {
    //                list.Add(new MISVillageDto
    //                {
    //                    MISVillageID = r.GetString(0),
    //                    VillageName = r.GetString(1),
    //                    District = r.IsDBNull(2) ? null : r.GetString(2)
    //                });
    //            }
    //        }
    //    }
    //    return list;
    //}

    //[WebMethod]
    //public static object GetMISVillages(string Year, string State, string District, string Block)
    //{
    //    DataTable dtVillage = Comman.Select_All_Data(
    //        "[PMS].[dbo].[mst5Village]",
    //        "VillageCode, VillageName",
    //        " DistrictCode='" + District +
    //        "' and BlockCode='" + Block +
    //        "' and StateCode='" + State +
    //        "' and left(Fyear,4)='" + Year + "' ",
    //        " VillageName", "Asc", "Y"
    //    );

    //    List<object> list = new List<object>();

    //    foreach (DataRow row in dtVillage.Rows)
    //    {
    //        list.Add(new
    //        {
    //            VillageCode = row["VillageCode"].ToString(),
    //            VillageName = row["VillageName"].ToString()
    //        });
    //    }

    //    return list;
    //}
    [WebMethod]
    public static List<MISVillageDto> GetMISVillages(string query = null,string year = null,string state = null,string district = null,string block = null, int status = 0)
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
                VillageName = row["VillageName"].ToString()
            });
        }

        //using (var con = new SqlConnection(ConnStr))
        //using (var cmd = new SqlCommand(@"
        //SELECT VillageCode, VillageName
        //FROM mst5Village
        //WHERE 
        //   (@q IS NULL OR VillageName LIKE '%' + @q + '%')
        //   AND (@year = 0 OR left(Fyear,4) = @year)
        //   AND (@state IS NULL OR StateCode = @state)
        //   AND (@district IS NULL OR DistrictCode = @district)
        //   AND (@block IS NULL OR BlockCode = @block)
        //ORDER BY VillageName", con))
        //{
        //    cmd.Parameters.AddWithValue("@q", (object)query ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@year", year);
        //    cmd.Parameters.AddWithValue("@state", (object)state ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@district", (object)district ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@block", (object)block ?? DBNull.Value);

        //    con.Open();
        //    using (var r = cmd.ExecuteReader())
        //    {
        //        while (r.Read())
        //        {
        //            list.Add(new MISVillageDto
        //            {
        //                VillageCode = r.GetString(0),
        //                VillageName = r.GetString(1)
        //            });
        //        }
        //    }
        //}

        return list;
    }

    //[WebMethod]
    //public static List<LayerVillageDto> GetLayerVillages(string query = null)
    //{
    //    var list = new List<LayerVillageDto>();
    //    using (var con = new SqlConnection(ConnStr))
    //    using (var cmd = new SqlCommand("SELECT  VillageID, VILLAGE as VillageName, DistrictID FROM GIS_Village a inner join VILLAGE_BOUNDARY b on a.VillageID=b.ID WHERE (@q IS NULL OR VILLAGE LIKE '%' + @q + '%' ) ORDER BY VillageName", con))
    //    {
    //        cmd.Parameters.AddWithValue("@q", (object)query ?? DBNull.Value);
    //        con.Open();
    //        using (var r = cmd.ExecuteReader())
    //        {
    //            while (r.Read())
    //            {
    //                list.Add(new LayerVillageDto
    //                {
    //                    LayerVillageID = r.GetString(0),
    //                    VillageName = r.GetString(1),
    //                    LayerInfo = r.IsDBNull(2) ? null : r.GetString(2)
    //                });
    //            }
    //        }
    //    }
    //    return list;
    //}

    //[WebMethod]
    //public static object GetLayerVillages(string Year, string State, string District, string Block)
    //{
    //    DataTable dtLayerVillage = Comman.Select_All_Data(
    //        "[PMS].[dbo].[mst5Village] a left join GIS_Village b on a.EGVillageCode=b.EG_VillageCode",
    //        "a.VillageName , b.VillageID",
    //        " a.DistrictCode='" + District +
    //        "' and a.BlockCode='" + Block +
    //        "' and a.StateCode='" + State +
    //        "' and left(a.Fyear,4)='" + Year + "' ",
    //        " VillageName", "Asc", "Y"
    //    );

    //    List<object> list = new List<object>();

    //    foreach (DataRow row in dtLayerVillage.Rows)
    //    {
    //        list.Add(new
    //        {
    //            VillageCode = row["VillageID"].ToString(),
    //            VillageName = row["VillageName"].ToString()
    //        });
    //    }

    //    return list;
    //}
    [WebMethod]
    public static List<LayerVillageDto> GetLayerVillages(
    string query = null,
    string year = null,
    string state = null,
    string district = null,
    string block = null)
    {
        var list = new List<LayerVillageDto>();

        using (var con = new SqlConnection(ConnStr))
        //using (var cmd = new SqlCommand(@"
        //SELECT a.VillageName , b.VillageID[VillageCode]
        //FROM [mst5Village] a left join GIS_Village b on a.EGVillageCode=b.EG_VillageCode
        //WHERE 
        //   (@q IS NULL OR VillageName LIKE '%' + @q + '%')
        //   AND (@year = 0 OR left(a.Fyear,4) = @year)
        //   AND (@state IS NULL OR a.StateCode = @state)
        //   AND (@district IS NULL OR a.DistrictCode = @district)
        //   AND (@block IS NULL OR a.BlockCode = @block)
        //ORDER BY a.VillageName", con))
        //{
        //    cmd.Parameters.AddWithValue("@q", (object)query ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@year", year);
        //    cmd.Parameters.AddWithValue("@state", (object)state ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@district", (object)district ?? DBNull.Value);
        //    cmd.Parameters.AddWithValue("@block", (object)block ?? DBNull.Value);

        //    con.Open();
        //    using (var r = cmd.ExecuteReader())
        //    {
        //        while (r.Read())
        //        {
        //            list.Add(new LayerVillageDto
        //            {
        //                VillageCode = r.GetString(0),
        //                VillageName = r.GetString(1)
        //            });
        //        }
        //    }
        //}
        using (var cmd = new SqlCommand("SELECT  VillageID[VillageCode], VILLAGE as VillageName, DistrictID FROM GIS_Village a inner join VILLAGE_BOUNDARY b on a.VillageID=b.ID WHERE (@q IS NULL OR VILLAGE LIKE '%' + @q + '%' ) ORDER BY VillageName", con))
        {
            cmd.Parameters.AddWithValue("@q", (object)query ?? DBNull.Value);
            con.Open();
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    list.Add(new LayerVillageDto
                    {
                        VillageCode = r.GetString(0),
                        VillageName = r.GetString(1)
                    });
                }
            }
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
    //    [WebMethod]
    //    public static List<MappingDto> GetMappings()
    //    {
    //        var list = new List<MappingDto>();
    //        using (var con = new SqlConnection(ConnStr))
    //        using (var cmd = new SqlCommand(@"
    //                SELECT  m.VillageID [MapID], m.EG_VillageCode [MISVillageID], mv.VillageName [MISVillageName], m.VillageID [LayerVillageID]
    //, mv.VillageName [LayerVillageName]
    //FROM GIS_Village m
    //INNER JOIN mst5Village mv ON mv.EGVillageCode = m.EG_VillageCode", con))
    //        {
    //            con.Open();
    //            using (var r = cmd.ExecuteReader())
    //            {
    //                while (r.Read())
    //                {
    //                    list.Add(new MappingDto
    //                    {
    //                        MapID = r.GetString(0),
    //                        MISVillageID = r.GetString(1),
    //                        MISVillageName = r.GetString(2),
    //                        LayerVillageID = r.GetString(3),
    //                        LayerVillageName = r.GetString(4)
    //                    });
    //                }
    //            }
    //        }
    //        return list;
    //    }
    [WebMethod]
    public static List<MappingDto> GetMappings(string query, string year, string state, string district, string block)
    {
        var list = new List<MappingDto>();

        using (var con = new SqlConnection(ConnStr))
        using (var cmd = new SqlCommand(@"
                    SELECT  m.VillageID [MapID], m.EG_VillageCode [MISVillageID], mv.VillageName [MISVillageName], m.VillageID [LayerVillageID]
    , mv.VillageName [LayerVillageName]
    FROM GIS_Village m
    INNER JOIN mst5Village mv ON mv.EGVillageCode = m.EG_VillageCode
WHERE 1=1  AND (@year = 0 OR left(mv.Fyear,4) = @year)
           AND (@state IS NULL OR mv.StateCode = @state)
           AND (@district IS NULL OR mv.DistrictCode = @district)
           AND (@block IS NULL OR mv.BlockCode = @block)
", con))
        {
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@state", (object)state ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@district", (object)district ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@block", (object)block ?? DBNull.Value);

            con.Open();
            using (var r = cmd.ExecuteReader())
            {
                while (r.Read())
                {
                    list.Add(new MappingDto
                    {
                        MapID = r.GetString(0),
                        MISVillageID = r.GetString(1),
                        MISVillageName = r.GetString(2),
                        LayerVillageID = r.GetString(3),
                        LayerVillageName = r.GetString(4)
                    });
                }
            }
        }

        return list;
    }

    //[WebMethod]
    //public static SuggestResponse[] GetSuggestions(string misName, int topN = 10)
    //{
    //    if (string.IsNullOrWhiteSpace(misName)) return new SuggestResponse[0];

    //    // load layer villages
    //    var layers = GetLayerVillages(null);

    //    // compute similarity
    //    var scored = layers.Select(l => new {
    //        Layer = l,
    //        //Score = JaroWinkler(misName?.Trim().ToLowerInvariant() ?? "", l.VillageName?.Trim().ToLowerInvariant() ?? "")
    //        Score = JaroWinkler(
    //(misName != null ? misName.Trim().ToLowerInvariant() : ""),
    //(l.VillageName != null ? l.VillageName.Trim().ToLowerInvariant() : ""))
    //})
    //    .Where(x => x.Score > 0.5) // only show reasonable matches (adjust threshold)
    //    .OrderByDescending(x => x.Score)
    //    .Take(topN)
    //    .Select(x => new SuggestResponse { LayerVillage = x.Layer, Score = x.Score })
    //    .ToArray();

    //    return scored;
    //}

    [WebMethod]
    public static List<MappedVillages> GetMappingVillages(string misName, string egCode,string fyear,string district,string block)
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
                EG_VillageCode = row["EG_VillageCode"].ToString(),
                MatchScore = Convert.ToInt32(row["MatchScore"]),
                Flag = Convert.ToInt32(row["Flag"]),
                lat= row["lat"].ToString(),
                lon= row["lon"].ToString()
            });
        }

        return list;
    }

    [WebMethod]
    public static SuggestLayerForMIS[] GetSuggestionsForMIS(
    string misName,
    string year,
    string state,
    string district,
    string block,
    int topN = 10)
    {
        if (string.IsNullOrWhiteSpace(misName))
            return new SuggestLayerForMIS[0];

        var layers = GetLayerVillages(null, year, state, district, block);

        return layers
            .Select(l => new {
                Layer = l,
                Score = JaroWinkler(misName.ToLower(), l.VillageName.ToLower())
            })
            .Where(x => x.Score > 0.2)
            .OrderByDescending(x => x.Score)
            .Take(topN)
            .Select(x => new SuggestLayerForMIS
            {
                LayerVillage = x.Layer,
                Score = x.Score
            })
            .ToArray();
    }

    [WebMethod]
    public static SuggestMISForLayer[] GetSuggestionsForLayer(
        string layerName,
        string year,
        string state,
        string district,
        string block,
        int topN = 10)
    {
        if (string.IsNullOrWhiteSpace(layerName))
            return new SuggestMISForLayer[0];

        // Load filtered MIS villages
        var mis = GetMISVillages(null,year, state, district, block);

        var scored = mis
            .Select(m => new
            {
                MIS = m,
                Score = JaroWinkler(
                    layerName.Trim().ToLowerInvariant(),
                    m.VillageName.Trim().ToLowerInvariant()
                )
            })
            .Where(x => x.Score > 0.20)
            .OrderByDescending(x => x.Score)
            .Take(topN)
            .Select(x => new SuggestMISForLayer
            {
                MISVillage = x.MIS,
                Score = x.Score
            })
            .ToArray();

        return scored;
    }


    //[WebMethod]
    //public static SuggestLayerForMIS[] GetSuggestionsForMIS(string misName, int topN = 10)
    //{
    //    if (string.IsNullOrWhiteSpace(misName)) return new SuggestLayerForMIS[0];

    //    var layers = GetLayerVillages(null);
    //    var scored = layers.Select(l => new { Layer = l, Score = JaroWinkler(misName.Trim().ToLowerInvariant(), l.VillageName.Trim().ToLowerInvariant()) })
    //        .Where(x => x.Score > 0.2)
    //        .OrderByDescending(x => x.Score)
    //        .Take(topN)
    //        .Select(x => new SuggestLayerForMIS { LayerVillage = x.Layer, Score = x.Score })
    //        .ToArray();

    //    return scored;
    //}

    //[WebMethod]
    //public static SuggestMISForLayer[] GetSuggestionsForLayer(string layerName, int topN = 10)
    //{
    //    if (string.IsNullOrWhiteSpace(layerName)) return new SuggestMISForLayer[0];

    //    var mis = GetMISVillages(null);
    //    var scored = mis.Select(m => new { MIS = m, Score = JaroWinkler(layerName.Trim().ToLowerInvariant(), m.VillageName.Trim().ToLowerInvariant()) })
    //        .Where(x => x.Score > 0.2)
    //        .OrderByDescending(x => x.Score)
    //        .Take(topN)
    //        .Select(x => new SuggestMISForLayer { MISVillage = x.MIS, Score = x.Score })
    //        .ToArray();

    //    return scored;
    //}

   
    #endregion

    #region DTOs
    public class MISVillageDto
    {
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        
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

        public class MappedVillages { public string SlNo; public string VillageID; public string GISVillageName; public string DistrictName; public string BlockName; public string DistanceKM; public string EG_VillageCode; public int MatchScore;public int Flag; public string lat; public string lon; }

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