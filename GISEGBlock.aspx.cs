using NetTopologySuite;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;

public partial class GIS : System.Web.UI.Page
{
    public static readonly string conStr = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ConStr"]);

    //static string conStr = "Data Source=10.2.0.4,9433;Initial Catalog=PMS04March2019;User=egpuse4pms23;Password=FCBl0ckB@l!ka123;Connection Timeout=30;Max Pool Size=200";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && Request.QueryString["download"] == "1")
        {
            DataTable dt = Session["EXPORT_DATA_Get_Villages"] as DataTable;

            if (dt != null)
            {
                ExportToExcel(dt, 1);
            }
        }
    }

    [WebMethod]
    public static string GetYear()
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT Distinct Fyear FROM [mstSchool] where Fyear='2026-2027' and Fyear is not null ORDER BY [Fyear] Desc", con);
            da.Fill(dt);
        }
        return JsonConvert.SerializeObject(dt);
    }

    //[WebMethod]
    //public static string GetStates()
    //{
    //    DataTable dt = new DataTable();
    //    using (SqlConnection con = new SqlConnection(conStr))
    //    {
    //        SqlDataAdapter da = new SqlDataAdapter(
    //            "SELECT [StateCode] as StateId,[StateName] as StateName FROM [dbo].[mst1State] ORDER BY [StateName]", con);
    //        da.Fill(dt);
    //    }
    //    return JsonConvert.SerializeObject(dt);
    //}

    [WebMethod]
    public static string GetStates(string ValidID)
    {
        string conditions = "";
        if (ValidID == "2023")
            conditions = "StateCode=9 ";
        else
            conditions = "StateCode = 9A ";
        string userlevel = Convert.ToString(HttpContext.Current.Session["user_level_Role"]);
        string statecode = Convert.ToString(HttpContext.Current.Session["StateCode"]);
        string username = Convert.ToString(HttpContext.Current.Session["username"]);
        if (statecode.ToString() != "")
        {

            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  userlevel),
                      new SqlParameter("@UserName",username ),
                    new SqlParameter("@StateCode", statecode),
                       new SqlParameter("@Year",  ValidID),
               };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);


            conditions = "StateCode='" + statecode.ToString() + "' ";
            return JsonConvert.SerializeObject(dt);
        }

        else
        {
            SqlParameter[] par1 = new SqlParameter[]
              {
                      new SqlParameter("@user_level_Role",  userlevel),
                      new SqlParameter("@UserName",username ),
                    new SqlParameter("@StateCode", statecode),
                       new SqlParameter("@Year",  ValidID),
              };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);

            return JsonConvert.SerializeObject(dt);
        }

    }

    [WebMethod]
    public static string GetDistricts(string stateId, string Fyear, string layertype)
    {
        string districtCode = "";
        System.Data.DataTable dt = new DataTable();
        string user_level_Role = Convert.ToString(HttpContext.Current.Session["user_level_Role"]);
        if (user_level_Role == "1")
        {

        }

        else
        {
            string[] items = Convert.ToString(HttpContext.Current.Session["DistrictCodeGIS2026"]).Split('#');
            districtCode = items[0];
        }
        if (user_level_Role == "1")
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                if (layertype == "4" || layertype == "5")
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT [DistrictCode] AS DistrictId, [DistrictName] AS DistrictName " +
                        "FROM [dbo].[mst2District] " +
                        "WHERE [StateCode] = @StateId and [Fyear] = @Fyear ORDER BY [DistrictName]", con))
                    {
                        da.SelectCommand.Parameters.Add("@StateId", SqlDbType.VarChar).Value = stateId;
                        da.SelectCommand.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = Fyear;
                        da.Fill(dt);
                    }
                }
                else if (layertype == "3")
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT distinct [DistrictCode] AS DistrictId,[EGDistrictCode], [DistrictName] AS DistrictName,HexColorCode as color, case when geom is not null then 1 else 0 end as ismapped FROM [dbo].[mst2District] WHERE CAST(StateCode AS VARCHAR) = @StateId and [Fyear] = @Fyear ORDER BY [DistrictName]", con))
                    {
                        da.SelectCommand.Parameters.Add("@StateId", SqlDbType.VarChar).Value = stateId;
                        da.SelectCommand.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = Fyear;
                        da.Fill(dt);
                    }
                }
            }
        }
        else
        {

            using (SqlConnection con = new SqlConnection(conStr))
            {
                if (layertype == "4" || layertype == "5")
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT [DistrictCode] AS DistrictId, [DistrictName] AS DistrictName " +
                        "FROM [dbo].[mst2District] " +
                        "WHERE [StateCode] = @StateId and [Fyear] = @Fyear and [districtCode] = @districtCode ORDER BY [DistrictName]", con))
                    {
                        da.SelectCommand.Parameters.Add("@StateId", SqlDbType.VarChar).Value = stateId;
                        da.SelectCommand.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = Fyear;
                        da.SelectCommand.Parameters.Add("@districtCode", SqlDbType.VarChar).Value = districtCode;
                        da.Fill(dt);
                    }
                }
                else if (layertype == "3")
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT distinct [DistrictCode] AS DistrictId,[EGDistrictCode], [DistrictName] AS DistrictName,HexColorCode as color, case when geom is not null then 1 else 0 end as ismapped FROM [dbo].[mst2District] WHERE CAST(StateCode AS VARCHAR) = @StateId and [Fyear] = @Fyear and [districtCode] = @districtCode  ORDER BY [DistrictName]", con))
                    {
                        da.SelectCommand.Parameters.Add("@StateId", SqlDbType.VarChar).Value = stateId;
                        da.SelectCommand.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = Fyear;
                        da.SelectCommand.Parameters.Add("@districtCode", SqlDbType.VarChar).Value = districtCode;
                        da.Fill(dt);
                    }
                }
            }
        }
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string GetBlocks(string districtId, string Fyear)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT distinct [BlockCode] as BlockId,[BlockName] as BlockName,colorCode as color,[EGBlockCode] as EGBlock, case when geom is not null then 1 else 0 end as ismapped FROM [dbo].[mst3Block] WHERE [DistrictCode]=@DistrictId and Fyear = @Fyear ORDER BY BlockName", con);
            da.SelectCommand.Parameters.Add("@DistrictId", SqlDbType.VarChar).Value = districtId;
            da.SelectCommand.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = Fyear;
            da.Fill(dt);
        }
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string GetCluster(string blockid, string Fyear)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT distinct [ClusterCode] as ClusterId,[ClusterName] as ClusterName,colorCode as color,[EGClusterCode] as EGClusterCode, case when geom is not null then 1 else 0 end as ismapped FROM [dbo].[mstCluster] WHERE [BlockCode]=@BlockId and Fyear = @Fyear ORDER BY ClusterName", con);
            da.SelectCommand.Parameters.Add("@BlockId", SqlDbType.VarChar).Value = blockid;
            da.SelectCommand.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = Fyear;
            da.Fill(dt);
        }
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string Getselectedvillage(string BlockId)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "select  geom.STAsText() AS WKT,vl.VillageName from [GIS_Village] gv  left join (select EGVillageCode,VillageName from  [mst5Village] where blockcode = @BlockId) vl on gv.EG_VillageCode = vl.EGVillageCode", con);
            da.SelectCommand.Parameters.Add("@BlockId", SqlDbType.VarChar).Value = BlockId;
            da.Fill(dt);
        }
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static List<object> GetLayers()
    {
        List<object> list = new List<object>();
        string query = "SELECT * FROM MapLayers WHERE IsActive = 1";

        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var workspace = dr["Workspace"].ToString();
                var layerName = dr["LayerName"].ToString();
                var GeoserverLayerName = dr["GeoserverLayerName"].ToString();

                list.Add(new
                {
                    LayerID = dr["LayerID"],
                    LayerName = layerName,
                    Workspace = workspace,
                    GeoServerURL = dr["GeoServerURL"],
                    LayerType = dr["LayerType"],
                    IsActive = dr["IsActive"],
                    GeoServerLayer = GeoserverLayerName
                });
            }
            con.Close();
        }
        return list;
    }

    [WebMethod]
    public static string GetGeoServerLayer(string url)
    {
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        Console.WriteLine("GeoServer URL: " + url); // DEBUG
        try
        {
            url = url.Trim('"');
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0");
                client.Encoding = System.Text.Encoding.UTF8;
                client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                return client.DownloadString(url); // ✅ RAW GEOJSON
            }
        }
        catch (Exception ex)
        {
            return "{\"error\":\"" + ex.Message.Replace("\"", "'") + "\"}";
        }
    }

    private static string geoServerUrl = "https://geo1server.educategirls.ngo/geoserver";
    private static string username = "admin";
    private static string password = "geoserver17";
    private static string workspace = "EG"; // change as needed
    private static string datastore = "my_datastore"; // you can use layerName as datastore too

    [WebMethod]
    public static string ExportShapefile(string fileName, object geojson, string layertype)
    {
        if (geojson == null)
            throw new Exception("GeoJSON is missing");

        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "Export";
        string dbfilename = fileName;
        fileName = fileName.Replace(" ", "_");

        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM [PMS].[dbo].[MapLayers] " +
                "WHERE [LayerName] = @storeName", con))
            {
                da.SelectCommand.Parameters.Add("@storeName", SqlDbType.VarChar).Value = dbfilename;
                da.Fill(dt);
            }
        }
        if (dt.Rows.Count == 0)
        {

            // 1️⃣ Deserialize GeoJSON
            string geoJsonText = JsonConvert.SerializeObject(geojson);
            GeoJsonReader reader = new GeoJsonReader();
            FeatureCollection fc = reader.Read<FeatureCollection>(geoJsonText);

            GeometryFactory gf =
                (GeometryFactory)NtsGeometryServices.Instance.CreateGeometryFactory(4326);

            List<IFeature> featuresList = new List<IFeature>();

            // 2️⃣ Fix geometries + DBF-safe attributes
            foreach (IFeature f in fc.Features)
            {
                if (f.Geometry == null)
                    continue;

                Geometry g = (Geometry)f.Geometry;

                // Fix invalid geometries
                if (!g.IsValid)
                    g = (Geometry)g.Buffer(0);

                if (g == null || g.IsEmpty)
                    continue;

                // Normalize to MultiPolygon
                if (g is Polygon)
                    g = (Geometry)gf.CreateMultiPolygon(new Polygon[] { (Polygon)g });


                // 🔹 get block code from attributes
                if (!f.Attributes.Exists("EGBLOCKCOD"))
                    continue;

                string egDistrictCode = f.Attributes["EGDISTCOD"].ToString();
                string egBlockCode = f.Attributes["EGBLOCKCOD"].ToString();
                string ClusterCode = f.Attributes["CLUSTERCODE"].ToString();
                string FYear = f.Attributes["FYear"].ToString();

                // 🔹 update database geometry



                if (layertype == "3")
                {
                    UpdateGeometryInDatabase(egDistrictCode, FYear, g, layertype);
                    UpdateLatLongInDatabase(egDistrictCode, FYear, g, layertype);
                }
                else if (layertype == "4")
                {
                    UpdateGeometryInDatabase(egBlockCode, FYear, g, layertype);
                    UpdateLatLongInDatabase(egBlockCode, FYear, g, layertype);
                }
                else if (layertype == "5")
                {
                    UpdateGeometryInDatabase(ClusterCode, FYear, g, layertype);
                }

                //// ---- DBF FIELD NAME FIX (<=10 chars) ----
                //AttributesTable newAttributes = new AttributesTable();
                //string[] attrNames = f.Attributes.GetNames();
                //Dictionary<string, int> usedNames = new Dictionary<string, int>();

                //for (int i = 0; i < attrNames.Length; i++)
                //{
                //    string original = attrNames[i];
                //    string baseName = original.Length > 10
                //        ? original.Substring(0, 10)
                //        : original;

                //    string finalName = baseName;

                //    if (usedNames.ContainsKey(baseName))
                //    {
                //        usedNames[baseName]++;
                //        finalName = baseName.Substring(0, 8) + usedNames[baseName];
                //    }
                //    else
                //    {
                //        usedNames[baseName] = 1;
                //    }

                //    newAttributes.Add(finalName, f.Attributes[original]);
                //}

                //// ✅ ADD FEATURE
                //featuresList.Add(new Feature(g, newAttributes));
            }

            //if (featuresList.Count == 0)
            //    throw new Exception("No valid geometries after repair");

            //// 3️⃣ Output folder
            //string folder = HttpContext.Current.Server.MapPath(Comman.GetImagePath("SurveyAnsPath") + "/");
            //if (!Directory.Exists(folder))
            //    Directory.CreateDirectory(folder);

            //string name = fileName + "_" + DateTime.Now.Ticks;
            //string shpPath = Path.Combine(folder, name + ".shp");

            //// 4️⃣ Write shapefile
            //ShapefileDataWriter writer = new ShapefileDataWriter(shpPath, gf);
            //writer.Header = ShapefileDataWriter.GetHeader(featuresList[0], featuresList.Count);
            //writer.Write(featuresList);

            //// 5️⃣ Create .prj (WGS84)
            //File.WriteAllText(Path.Combine(folder, name + ".prj"),
            //    "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\"," +
            //    "SPHEROID[\"WGS 84\",6378137,298.257223563]]," +
            //    "PRIMEM[\"Greenwich\",0],UNIT[\"degree\",0.0174532925199433]]");

            //// 6️⃣ ZIP using Ionic.Zip (DotNetZip)
            //string zipPath = Path.Combine(folder, name + ".zip");
            //if (File.Exists(zipPath))
            //    File.Delete(zipPath);

            //using (ZipFile zip = new ZipFile())
            //{
            //    string[] exts = { ".shp", ".shx", ".dbf", ".prj" };

            //    foreach (string ext in exts)
            //    {
            //        string file = Path.Combine(folder, name + ext);
            //        if (File.Exists(file))
            //            zip.AddFile(file, "");
            //    }

            //    zip.Save(zipPath);
            //}
            //string msg = PublishShapefileZip(geoServerUrl, "EG", name, folder, username, password, layertype, dbfilename);
            //return ""+ "msg" + " [" + name + " ]";
            return "✔ Shapefile save successfully!";
        }
        else
        {
            return "✔ Shapefile already exist";
        }
    }

    public static string PublishShapefileZip(
        string geoServerUrl,
        string workspace,
        string datastore,
        string zipFilePath,
        string username,
        string password, string layertype, string dbfilename)
    {
        string url = geoServerUrl.TrimEnd('/') +
            "/rest/workspaces/" + workspace +
            "/datastores/" + datastore +
            "/file.shp";

        byte[] zipBytes = File.ReadAllBytes(zipFilePath + "/" + datastore + ".zip");


        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "PUT";
        request.ContentType = "application/zip";
        request.KeepAlive = false;                     // FIX 3: prevent connection abort
        request.ProtocolVersion = HttpVersion.Version10; // FIX 4: avoid chunked encoding
        request.Timeout = 300000;                      // 5 min
        request.ReadWriteTimeout = 300000;
        request.ContentLength = zipBytes.Length;

        string auth = Convert.ToBase64String(
            Encoding.ASCII.GetBytes(username + ":" + password));
        request.Headers["Authorization"] = "Basic " + auth;

        using (Stream reqStream = request.GetRequestStream())
        {
            reqStream.Write(zipBytes, 0, zipBytes.Length);
        }

        using (HttpWebResponse response =
               (HttpWebResponse)request.GetResponse())
        {
            if (response.StatusCode != HttpStatusCode.Created &&
                response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("GeoServer publish failed: " + response.StatusCode);
            }
            else
            {
                // Insert DB record
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    conn.Open();
                    string q = @"INSERT INTO MapLayers (LayerName,GeoserverLayerName,LayerType)
                             VALUES (@LayerName,@GeoserverLayerName,@layetype)";

                    using (SqlCommand cmd = new SqlCommand(q, conn))
                    {
                        cmd.Parameters.AddWithValue("@LayerName", dbfilename);      // store name
                        cmd.Parameters.AddWithValue("@GeoserverLayerName", datastore); // UI name
                        cmd.Parameters.AddWithValue("@layetype", layertype);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }



        return "✔ Shapefile published successfully!";

    }

    private static void UpdateGeometryInDatabase(
     string egCode, string FYear,
     Geometry geom, string layerType)
    {
        string wkt = geom.AsText();
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            conn.Open();
            if (layerType == "4")
            {
                using (SqlCommand cmd = new SqlCommand(@"
        UPDATE mst3Block
        SET geom = geometry::STGeomFromText(@wkt, 4326)
        WHERE EGBlockCode = @code and Fyear = @Fyear", conn))
                {
                    cmd.Parameters.Add("@wkt", SqlDbType.NVarChar).Value = wkt;
                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = egCode;
                    cmd.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = FYear;
                    cmd.ExecuteNonQuery();
                }
            }
            else if (layerType == "3")
            {
                using (SqlCommand cmd = new SqlCommand(@"
        UPDATE mst2District
        SET geom = geometry::STGeomFromText(@wkt, 4326)
        WHERE EGDistrictCode = @code and Fyear = @Fyear", conn))
                {
                    cmd.Parameters.Add("@wkt", SqlDbType.NVarChar).Value = wkt;
                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = egCode;
                    cmd.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = FYear;
                    cmd.ExecuteNonQuery();
                }
            }
            else if (layerType == "5")
            {
                using (SqlCommand cmd = new SqlCommand(@"
        UPDATE mstCluster
        SET geom = geometry::STGeomFromText(@wkt, 4326)
        WHERE ClusterCode = @code and Fyear = @Fyear", conn))
                {
                    cmd.Parameters.Add("@wkt", SqlDbType.NVarChar).Value = wkt;
                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = egCode;
                    cmd.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = FYear;
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
    private static void UpdateLatLongInDatabase(
 string egCode, string FYear,
 Geometry geom, string layerType)
    {
        string wkt = geom.AsText();
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            conn.Open();
            if (layerType == "4")
            {
                using (SqlCommand cmd = new SqlCommand(@"
    UPDATE mst3Block
    SET B_lat = geom.STCentroid().STY,B_long=geom.STCentroid().STX
    WHERE EGBlockCode = @code and Fyear = @Fyear", conn))
                {
                    cmd.Parameters.Add("@wkt", SqlDbType.NVarChar).Value = wkt;
                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = egCode;
                    cmd.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = FYear;
                    cmd.ExecuteNonQuery();
                }
            }
            if (layerType == "3")
            {
                using (SqlCommand cmd = new SqlCommand(@"
    UPDATE  mst2District
    SET D_lat = geom.STCentroid().STY,D_long=geom.STCentroid().STX
    WHERE EGDistrictCode = @code and Fyear = @Fyear", conn))
                {
                    cmd.Parameters.Add("@wkt", SqlDbType.NVarChar).Value = wkt;
                    cmd.Parameters.Add("@code", SqlDbType.VarChar).Value = egCode;
                    cmd.Parameters.Add("@Fyear", SqlDbType.VarChar).Value = FYear;
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }

    [WebMethod]
    public static void DeleteLayer(string districtcode, string blockcode, string ClusterCode, string fyear)
    {

        try
        {
            SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@DistrictCode",districtcode),
             new SqlParameter("@BlockCode",blockcode),
             new SqlParameter("@ClusterCode",ClusterCode),
              new SqlParameter("@FYear",fyear)
            };

            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Delete_Digitalized_Layer", p);
        }
        catch
        {

        }
    }

    [WebMethod]
    public static string isDigitalize(string districtcode, string blockcode, string clustercode, string fyear)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@DistrictCode", districtcode),
            new SqlParameter("@BlockCode", blockcode),
            new SqlParameter("@ClusterCode", clustercode),
            new SqlParameter("@FYear", fyear)
        };

            DataTable dt = SqlHelper.GetDataTable(
                SqlHelper.mainConnectionString,
                CommandType.StoredProcedure,
                "SP_GET_Digitalized",
                p
            );

            if (dt.Rows.Count == 0 || dt.Rows[0]["isDigitalize"] == DBNull.Value)
                return "0"; // no data or null

            return dt.Rows[0]["isDigitalize"].ToString(); // should now return "1" or "0"
        }
        catch (Exception ex)
        {
            return "ERROR: " + ex.Message;
        }
    }

    [WebMethod(EnableSession = true)]
    public static string ExportMappedData(string fyear, string district, string block, string cluster)
    {
        SqlParameter[] p = new SqlParameter[] {
        new SqlParameter("@year",2025),
        new SqlParameter("@district",district),
        new SqlParameter("@block",block),
        new SqlParameter("@cluster",cluster)
    };

        DataTable dt = SqlHelper.GetDataTable(
            SqlHelper.mainConnectionString,
            CommandType.StoredProcedure,
            "Get_Villages", p);

        if (dt.Rows.Count == 0)
            return "NO_DATA";

        // Store data temporarily (Session)
        HttpContext.Current.Session["EXPORT_DATA_Get_Villages"] = dt;

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
}



