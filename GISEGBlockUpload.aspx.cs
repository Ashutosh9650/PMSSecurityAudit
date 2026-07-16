using Ionic.Zip;
using NetTopologySuite;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
public partial class GISEGBlockUpload : System.Web.UI.Page
{
    public static readonly string conStr = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ConStr"]);

    private static string geoServerUrl = "https://geo1server.educategirls.ngo/geoserver";
    private static string username = "admin";
    private static string password = "geoserver17";
    private static string workspace = "EG"; // change as needed
    private static string datastore = "my_datastore"; // you can use layerName as datastore too

    //static string conStr = "Data Source=10.2.0.4,9433;Initial Catalog=PMS;User=egpuse4pms23;Password=FCBl0ckB@l!ka123;Connection Timeout=30;Max Pool Size=200";
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



    [WebMethod]
    public static string UploadToGeoServer(string fileName, string base64, string layetype)
    {
        // =========================================================
        // FIX 1: FORCE TLS 1.2 (MAIN FIX FOR YOUR ERROR)
        // =========================================================
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        string geoserverUrl = "https://geo1server.educategirls.ngo/geoserver/rest/workspaces/EG/datastores/";
        string geoserverUser = "admin";
        string geoserverPass = "geoserver17";

        try
        {
            // Convert Base64 → Bytes
            byte[] zipBytes = Convert.FromBase64String(base64);

            // Detect real zip/shp name
            string realZipName = GetRealZipName(zipBytes);
            string storeName = Path.GetFileNameWithoutExtension(realZipName);

            string postUrl = geoserverUrl + storeName + "/file.shp";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM [PMS].[dbo].[MapLayers] " +
                    "WHERE [GeoserverLayerName] = @storeName", con))
                {
                    da.SelectCommand.Parameters.Add("@storeName", SqlDbType.VarChar).Value = storeName;
                    da.Fill(dt);
                }
            }
            if (dt.Rows.Count == 0)
            {
                // =========================================================
                // FIX 2: REQUEST SETTINGS FOR STABLE UPLOAD
                // =========================================================
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(postUrl);
                req.Method = "PUT";
                req.ContentType = "application/zip";
                req.KeepAlive = false;                     // FIX 3: prevent connection abort
                req.ProtocolVersion = HttpVersion.Version10; // FIX 4: avoid chunked encoding
                req.Timeout = 300000;                      // 5 min
                req.ReadWriteTimeout = 300000;

                // Authentication
                string auth = Convert.ToBase64String(Encoding.ASCII.GetBytes(geoserverUser + ":" + geoserverPass));
                req.Headers["Authorization"] = "Basic " + auth;

                // =========================================================
                // WRITE ZIP TO REQUEST STREAM
                // =========================================================
                using (Stream reqStream = req.GetRequestStream())
                    reqStream.Write(zipBytes, 0, zipBytes.Length);

                // =========================================================
                // GET RESPONSE FROM GEOSERVER
                // =========================================================
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                {

                    // Insert DB record
                    using (SqlConnection conn = new SqlConnection(conStr))
                    {
                        conn.Open();
                        string q = @"INSERT INTO MapLayers (LayerName,GeoserverLayerName,LayerType)
                             VALUES (@LayerName,@GeoserverLayerName,@layetype)";

                        using (SqlCommand cmd = new SqlCommand(q, conn))
                        {
                            cmd.Parameters.AddWithValue("@LayerName", fileName);      // store name
                            cmd.Parameters.AddWithValue("@GeoserverLayerName", storeName); // UI name
                            cmd.Parameters.AddWithValue("@layetype", layetype);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return "✔ Shapefile published successfully!";
                }

            }
            else
            {
                return "✔ Shapefile already exist";
            }
        }
        catch (WebException ex)
        {
            string responseText = "";
            int code = 0;

            if (ex.Response != null)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    responseText = reader.ReadToEnd();

                code = (int)((HttpWebResponse)ex.Response).StatusCode;
            }

            if (code == 0)
                return "NETWORK ERROR: " + ex.Message;

            if (code == 404)
                return "❌ 404 Not Found – Workspace/datastore missing.\n" + responseText;

            if (code == 400)
                return "❌ 400 Bad Request – Invalid shapefile/zip.\n" + responseText;

            if (code == 500)
                return "❌ 500 Internal Server Error – GeoServer failed.\n" + responseText;

            return "ERROR (" + code + "): " + responseText;
        }
        catch (Exception ex)
        {
            return "EXCEPTION: " + ex.Message;
        }
    }

    public static string GetRealZipName(byte[] zipBytes)
    {
        using (var ms = new MemoryStream(zipBytes))
        using (var zip = Ionic.Zip.ZipFile.Read(ms))
        {
            // Loop through entries because C# 5 does not support LINQ well here
            foreach (var entry in zip.Entries)
            {
                if (entry.FileName.EndsWith(".shp", StringComparison.OrdinalIgnoreCase))
                {
                    // Example: "EG Village.shp" → "EG Village.zip"
                    string shpName = Path.GetFileNameWithoutExtension(entry.FileName);
                    return shpName + ".zip";
                }
            }
        }

        // fallback if no .shp found
        return "uploaded.zip";
    }




    #region Anand
    [WebMethod]
    public static List<object> GetLayers(string layerType)
    {
        List<object> list = new List<object>();
        string query = "Select a.LayerID,a.LayerName,a.Workspace,a.GeoServerURL,a.IsActive,a.GeoserverLayerName,a.flag,a.createdDate,\r\ncase when a.LayerType=1 then 'State' when a.LayerType=2 then 'District'  when a.LayerType=3 then 'Block' when a.LayerType=4 then 'Village' End as LayerType FROM MapLayers a inner join (Select Distinct LayerID from GIS_Village) b on a.LayerID=b.LayerID WHERE a.IsActive = 1 and flag='P' order by a.LayerName";
        //string query = "SELECT a.* FROM MapLayers a  WHERE a.IsActive = 1 and flag='P' order by a.LayerName";
        if (layerType != "")
        {
            query = "Select a.LayerID,a.LayerName,a.Workspace,a.GeoServerURL,a.IsActive,a.GeoserverLayerName,a.flag,a.createdDate,\r\ncase when a.LayerType=1 then 'State' when a.LayerType=2 then 'District'  when a.LayerType=3 then 'Block' when a.LayerType=4 then 'Village' End as LayerType FROM MapLayers a inner join (Select Distinct LayerID from GIS_Village) b on a.LayerID=b.LayerID WHERE a.IsActive = 1 and flag='P' and a.LayerType=" + layerType + " order by a.LayerName";
        }

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
    public static string GetWFSLayer(string wfsUrl)
    {
        System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        try
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0"); // Some servers require a User-Agent
                client.Headers.Add("Content-Type", "application/json");
                // If authentication needed:
                // client.Credentials = new NetworkCredential("username", "password");

                string geojson = client.DownloadString(wfsUrl);
                return geojson;
            }
        }
        catch (Exception ex)
        {
            return "ERROR: {ex.Message}";
        }
    }
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (!fuFile.HasFile)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please select geojson file');", true);
    //        //lblMsg.Text = "Please select geojson file";
    //        return;
    //    }
    //    if (Convert.ToString(txt_shpFileName.Text) == "")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please enter shape file name');", true);
    //        //lblMsg.Text = "Please enter shape file name";
    //        return;
    //    }
    //    if (ddlLayerType.SelectedValue == "0")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Select Layer Type');", true);
    //        //lblMsg.Text = "Please Select Layer Type";
    //        return;
    //    }
    //    if (ddlLayerType.SelectedValue == "1")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('State layer import is under progress, please upload village layer');", true);
    //        //lblMsg.Text = "State layer import is under progress, please upload village layer";
    //        return;
    //    }
    //    if (ddlLayerType.SelectedValue == "2")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('District layer import is under progress, please upload village layer');", true);
    //        //lblMsg.Text = "District layer import is under progress, please upload village layer";
    //        return;
    //    }
    //    if (ddlLayerType.SelectedValue == "3")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Block layer import is under progress, please upload village layer');", true);
    //        //lblMsg.Text = "Block layer import is under progress, please upload village layer";
    //        return;
    //    }

    //    string fileExtension = Path.GetExtension(fuFile.PostedFile.FileName).ToLower();

    //    if (fileExtension != ".geojson")
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please upload a valid file (geojson).');", true);
    //        //lblMsg.Text = "Please upload a valid file (geojson).";
    //        return;
    //    }


    //    string json = new StreamReader(fuFile.PostedFile.InputStream).ReadToEnd();
    //    try
    //    {
    //        if (ddlLayerType.SelectedValue == "4")
    //        {
    //            if (CheckShpName() == "-1")
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Layer Name alrady exists');", true);
    //                //lblMsg.Text = "Layer Name alrady exists";
    //                return;
    //            }
    //            ImportGeoJson(json);
    //            //lblMsg.Text = "GeoJSON imported successfully ✔";
    //            ScriptManager.RegisterStartupScript(this,this.GetType(),"alertMessage","alert('Layer imported successfully ✔');", true);
    //            //divmaplayer.Visible = true;
    //        }
    //        //else if (ddlLayerType.SelectedValue == "3")
    //        //{
    //        //    //if (CheckShpName() == "-1")
    //        //    //{
    //        //    //    lblMsg.Text = "Layer Name alrady exists";
    //        //    //    return;
    //        //    //}
    //        //    ImportGeoJson_Block(json);
    //        //    lblMsg.Text = "GeoJSON imported successfully ✔";
    //        //    //divmaplayer.Visible = true;
    //        //}

    //    }
    //    catch 
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(),
    //"hideLoader", "hideloader();", true);
    //        if (ex.Message.Contains("Violation of PRIMARY KEY"))
    //        {
    //            //lblMsg.Text = "Layer already exists";
    //            //return;
    //        }
    //        //lblMsg.Text = "Error: " + ex.Message;
    //    }
    //    finally
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(),
    //"hideLoader", "hideloader();", true);
    //    }
    //    ScriptManager.RegisterStartupScript(this, this.GetType(),
    //"hideLoader", "hideloader();", true);
    //}
    private static string ImportGeoJson(string json, string fileName, string layertype, int layerid)
    {
        try
        {
            JObject root = JObject.Parse(json);
            JArray features = (JArray)root["features"];
            if (features.Count == 0) return "No features found in GeoJSON.";

            DataTable dt = new DataTable();

            dt.Columns.Add("polygon", typeof(string));
            dt.Columns.Add("VillageID", typeof(string));
            dt.Columns.Add("DistrictID", typeof(string));
            dt.Columns.Add("StateID", typeof(string));

            dt.Columns.Add("StateName", typeof(string));
            dt.Columns.Add("DistrictName", typeof(string));
            dt.Columns.Add("SubDistrictID", typeof(string));
            dt.Columns.Add("SubDistrictName", typeof(string));
            dt.Columns.Add("BlockID", typeof(string));
            dt.Columns.Add("BlockName", typeof(string));
            dt.Columns.Add("VillageName", typeof(string));

            dt.Columns.Add("lat", typeof(string));
            dt.Columns.Add("long", typeof(string));
            dt.Columns.Add("LayerID", typeof(string));
            dt.Columns.Add("Level", typeof(string));
            dt.Columns.Add("TRU", typeof(string));

            foreach (var feature in features)
            {
                DataRow row = dt.NewRow();

                JObject geom = (JObject)feature["geometry"];
                row["polygon"] = GeoJsonToWkt(geom);

                JObject p = feature["properties"] as JObject;

                row["VillageID"] = GetValue(p, "VILLAGE_CO");
                row["DistrictID"] = GetValue(p, "DIST_CODE");
                row["StateID"] = GetValue(p, "STATE_CODE");

                row["StateName"] = GetValue(p, "STATE_NAME");
                row["DistrictName"] = GetValue(p, "DIST_NAME");
                row["SubDistrictID"] = GetValue(p, "SUB_DIST_C");
                row["SubDistrictName"] = GetValue(p, "SUB_DIST_N");
                row["BlockID"] = GetValue(p, "BLOCK_NO");
                row["BlockName"] = GetValue(p, "BLOCK_NAME");
                row["VillageName"] = GetValue(p, "NAME");

                row["lat"] = GetValue(p, "LAT");
                row["long"] = GetValue(p, "LONG");
                row["LayerID"] = layerid;

                row["Level"] = GetValue(p, "LEVEL");
                row["TRU"] = GetValue(p, "TRU");

                dt.Rows.Add(row);
            }

            // Bulk insert and return any error from SQL
            string result = BulkInsertIntoGISVillage(dt, layerid);

            return result; // "SUCCESS" or error message
        }
        catch (JsonException ex)
        {
            return "ERROR";
        }
        catch
        {
            return "ERROR";
        }
    }


    private void ImportGeoJson_Block(string json)
    {
        JObject root = JObject.Parse(json);
        JArray features = (JArray)root["features"];
        if (features.Count == 0) return;

        DataTable dt = new DataTable();

        dt.Columns.Add("polygon", typeof(string));   // renamed
        //dt.Columns.Add("ID", typeof(string));
        dt.Columns.Add("BlockCode", typeof(string));
        dt.Columns.Add("BlockName", typeof(string));

        foreach (var feature in features)
        {
            DataRow row = dt.NewRow();

            JObject geom = (JObject)feature["geometry"];
            row["polygon"] = GeoJsonToWkt(geom);   // renamed

            JObject p = feature["properties"] as JObject;

            row["BlockCode"] = GetValue(p, "SUB_DIST_C");
            row["BlockName"] = GetValue(p, "SUB_DIST_N");


            dt.Rows.Add(row);
        }


        BulkInsertIntoGISBlock(dt);
        //string layerid = saveShpName();
        //CreateGeometryBlock("GIS_Block_Admin",layerid);
    }
    private static object GetValue(JObject props, string key)
    {
        if (props != null && props[key] != null && props[key].Type != JTokenType.Null)
            return props[key].ToString();

        return DBNull.Value;
    }
    //private static void BulkInsertIntoGISVillage(DataTable dt)
    //{
    //    using (SqlBulkCopy bulk = new SqlBulkCopy(conStr))
    //    {
    //        bulk.DestinationTableName = "GIS_Village";
    //        bulk.BatchSize = 5000;
    //        bulk.BulkCopyTimeout = 0;

    //        bulk.ColumnMappings.Add("polygon", "polygon");   // renamed
    //        bulk.ColumnMappings.Add("VillageID", "VillageID");
    //        bulk.ColumnMappings.Add("DistrictID", "DistrictID");
    //        bulk.ColumnMappings.Add("StateID", "StateID");


    //        bulk.ColumnMappings.Add("StateName", "StateName");
    //        bulk.ColumnMappings.Add("DistrictName", "DistrictName");
    //        bulk.ColumnMappings.Add("SubDistrictID", "SubDistrictID");
    //        bulk.ColumnMappings.Add("SubDistrictName", "SubDistrictName");
    //        bulk.ColumnMappings.Add("BlockID", "BlockID");
    //        bulk.ColumnMappings.Add("BlockName", "BlockName");
    //        bulk.ColumnMappings.Add("VillageName", "VillageName");

    //        bulk.ColumnMappings.Add("lat", "lat");
    //        bulk.ColumnMappings.Add("long", "long");
    //        bulk.ColumnMappings.Add("LayerID", "LayerID");

    //        bulk.ColumnMappings.Add("Level", "Level");
    //        bulk.ColumnMappings.Add("TRU", "TRU");

    //        bulk.WriteToServer(dt);
    //    }
    //    // Call merge procedure after bulk insert
    //    //ExecuteMergeProcedure();
    //}

    //private static void BulkInsertIntoGISVillage(DataTable dt,int layerid)
    //{
    //    using (SqlConnection con = new SqlConnection(conStr))
    //    {
    //        con.Open();

    //        using (SqlTransaction tran = con.BeginTransaction())
    //        {
    //            try
    //            {
    //                using (SqlBulkCopy bulk = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, tran))
    //                {
    //                    bulk.DestinationTableName = "GIS_Village";
    //                    bulk.BatchSize = 5000;
    //                    bulk.BulkCopyTimeout = 0;

    //                    bulk.ColumnMappings.Add("polygon", "polygon");
    //                    bulk.ColumnMappings.Add("VillageID", "VillageID");
    //                    bulk.ColumnMappings.Add("DistrictID", "DistrictID");
    //                    bulk.ColumnMappings.Add("StateID", "StateID");
    //                    bulk.ColumnMappings.Add("StateName", "StateName");
    //                    bulk.ColumnMappings.Add("DistrictName", "DistrictName");
    //                    bulk.ColumnMappings.Add("SubDistrictID", "SubDistrictID");
    //                    bulk.ColumnMappings.Add("SubDistrictName", "SubDistrictName");
    //                    bulk.ColumnMappings.Add("BlockID", "BlockID");
    //                    bulk.ColumnMappings.Add("BlockName", "BlockName");
    //                    bulk.ColumnMappings.Add("VillageName", "VillageName");
    //                    bulk.ColumnMappings.Add("lat", "lat");
    //                    bulk.ColumnMappings.Add("long", "long");
    //                    bulk.ColumnMappings.Add("LayerID", "LayerID");
    //                    bulk.ColumnMappings.Add("Level", "Level");
    //                    bulk.ColumnMappings.Add("TRU", "TRU");

    //                    bulk.WriteToServer(dt);
    //                }

    //                tran.Commit();
    //            }
    //            catch
    //            {
    //                tran.Rollback();
    //                using (SqlConnection conn = new SqlConnection(conStr))
    //                {
    //                    conn.Open();
    //                    string q = @"Delete from MapLayers where LayerID=@LayerID";

    //                    using (SqlCommand cmd = new SqlCommand(q, conn))
    //                    {
    //                        cmd.Parameters.AddWithValue("@LayerID", layerid);
    //                        cmd.ExecuteNonQuery();
    //                    }
    //                }
    //                throw;
    //            }
    //        }
    //    }
    //}

    private static string BulkInsertIntoGISVillage(DataTable dt, int layerid)
    {
        try
        {
            // 1️⃣ Bulk insert into temp table
            using (SqlBulkCopy bulk = new SqlBulkCopy(conStr))
            {
                bulk.DestinationTableName = "GIS_Village_Temp";
                bulk.BatchSize = 5000;
                bulk.BulkCopyTimeout = 0;

                bulk.ColumnMappings.Add("polygon", "polygon");
                bulk.ColumnMappings.Add("VillageID", "VillageID");
                bulk.ColumnMappings.Add("DistrictID", "DistrictID");
                bulk.ColumnMappings.Add("StateID", "StateID");
                bulk.ColumnMappings.Add("StateName", "StateName");
                bulk.ColumnMappings.Add("DistrictName", "DistrictName");
                bulk.ColumnMappings.Add("SubDistrictID", "SubDistrictID");
                bulk.ColumnMappings.Add("SubDistrictName", "SubDistrictName");
                bulk.ColumnMappings.Add("BlockID", "BlockID");
                bulk.ColumnMappings.Add("BlockName", "BlockName");
                bulk.ColumnMappings.Add("VillageName", "VillageName");
                bulk.ColumnMappings.Add("lat", "lat");
                bulk.ColumnMappings.Add("long", "long");
                bulk.ColumnMappings.Add("LayerID", "LayerID");
                bulk.ColumnMappings.Add("Level", "Level");
                bulk.ColumnMappings.Add("TRU", "TRU");

                bulk.WriteToServer(dt);
            }

            // 2️⃣ Execute merge procedure
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("MergeGISVillage", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@LayerID", SqlDbType.Int).Value = layerid;

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return "SUCCESS";
        }
        catch (SqlException ex)
        {
            return "ERROR";
        }
        catch
        {
            return "ERROR";
        }
    }
    //private static string BulkInsertIntoGISVillage(DataTable dt, int layerid)
    //{
    //    try
    //    {
    //        // 1️⃣ Bulk insert into temp table
    //        using (SqlConnection con = new SqlConnection(conStr))
    //        {
    //            con.Open();

    //            // 1️⃣ Create the temp table first
    //            using (SqlCommand cmdCreate = new SqlCommand(@"
    //                CREATE TABLE #GIS_Village_Temp
    //                (
    //                    polygon NVARCHAR(MAX),
    //                    VillageID VARCHAR(10),
    //                    DistrictID VARCHAR(10),
    //                    StateID VARCHAR(10),
    //                    StateName VARCHAR(100),
    //                    DistrictName VARCHAR(100),
    //                    SubDistrictID VARCHAR(10),
    //                    SubDistrictName VARCHAR(100),
    //                    BlockID VARCHAR(10),
    //                    BlockName NVARCHAR(200),
    //                    VillageName NVARCHAR(300),
    //                    lat VARCHAR(100),
    //                    long VARCHAR(100),
    //                    LayerID INT,
    //                    Level VARCHAR(250),
    //                    TRU VARCHAR(250)
    //                );", con))
    //            {
    //                cmdCreate.ExecuteNonQuery();
    //            }

    //            // 2️⃣ Bulk copy into temp table
    //            using (SqlBulkCopy bulk = new SqlBulkCopy(con))
    //            {
    //                bulk.DestinationTableName = "#GIS_Village_Temp"; // works now
    //                bulk.BatchSize = 5000;
    //                bulk.BulkCopyTimeout = 0;

    //                bulk.ColumnMappings.Add("polygon", "polygon");
    //                bulk.ColumnMappings.Add("VillageID", "VillageID");
    //                bulk.ColumnMappings.Add("DistrictID", "DistrictID");
    //                bulk.ColumnMappings.Add("StateID", "StateID");
    //                bulk.ColumnMappings.Add("StateName", "StateName");
    //                bulk.ColumnMappings.Add("DistrictName", "DistrictName");
    //                bulk.ColumnMappings.Add("SubDistrictID", "SubDistrictID");
    //                bulk.ColumnMappings.Add("SubDistrictName", "SubDistrictName");
    //                bulk.ColumnMappings.Add("BlockID", "BlockID");
    //                bulk.ColumnMappings.Add("BlockName", "BlockName");
    //                bulk.ColumnMappings.Add("VillageName", "VillageName");
    //                bulk.ColumnMappings.Add("lat", "lat");
    //                bulk.ColumnMappings.Add("long", "long");
    //                bulk.ColumnMappings.Add("LayerID", "LayerID");
    //                bulk.ColumnMappings.Add("Level", "Level");
    //                bulk.ColumnMappings.Add("TRU", "TRU");

    //                bulk.WriteToServer(dt);
    //            }

    //            // 3️⃣ Call the stored procedure (it will see #GIS_Village_Temp)
    //            using (SqlCommand cmd = new SqlCommand("MergeGISVillage_NEW", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@LayerID", SqlDbType.Int).Value = layerid;

    //                cmd.ExecuteNonQuery();
    //            }
    //        }


    //        return "SUCCESS";
    //    }
    //    catch (SqlException ex)
    //    {
    //        return "ERROR";
    //    }
    //    catch 
    //    {
    //        return "ERROR";
    //    }
    //}


    private void BulkInsertIntoGISBlock(DataTable dt)
    {
        using (SqlBulkCopy bulk = new SqlBulkCopy(conStr))
        {
            bulk.DestinationTableName = "GIS_Block_Admin";
            bulk.BatchSize = 5000;
            bulk.BulkCopyTimeout = 0;

            bulk.ColumnMappings.Add("polygon", "polygon");   // renamed
            bulk.ColumnMappings.Add("BlockCode", "BlockCode");
            bulk.ColumnMappings.Add("BlockName", "BlockName");


            bulk.WriteToServer(dt);
        }
    }

    private void ExecuteMergeProcedure()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Merge_GIS_Village1", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    private static void CreateGeometry(string tableName, int layerid)
    {
        string sql =
            "UPDATE [" + tableName + "] " +
            "SET Geom = geometry::STGeomFromText([polygon], 4326) " +
            "WHERE layerid=" + layerid + " and Geom IS NULL AND [polygon] IS NOT NULL";

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    private void CreateGeometryBlock(string tableName, string layerid)
    {
        string sql =
            "UPDATE [" + tableName + "] " +
            "SET Geom = geometry::STGeomFromText([polygon], 4326) " +
            "WHERE layerid=" + layerid + " and Geom IS NULL AND [polygon] IS NOT NULL";

        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    private static string GeoJsonToWkt(JObject geom)
    {
        if (geom == null) return null;

        string type = geom["type"].ToString();
        JArray coords = (JArray)geom["coordinates"];

        if (type == "Point")
        {
            return "POINT(" + coords[0] + " " + coords[1] + ")";
        }
        else if (type == "LineString")
        {
            string s = "LINESTRING(";
            for (int i = 0; i < coords.Count; i++)
            {
                s += coords[i][0] + " " + coords[i][1];
                if (i < coords.Count - 1) s += ",";
            }
            s += ")";
            return s;
        }
        else if (type == "Polygon")
        {
            string s = "POLYGON(";
            for (int i = 0; i < coords.Count; i++)
            {
                s += "(";
                for (int j = 0; j < coords[i].Count(); j++)
                {
                    s += coords[i][j][0] + " " + coords[i][j][1];
                    if (j < coords[i].Count() - 1) s += ",";
                }
                s += ")";
                if (i < coords.Count - 1) s += ",";
            }
            s += ")";
            return s;
        }
        else if (type == "MultiPolygon")
        {
            string s = "MULTIPOLYGON(";
            for (int i = 0; i < coords.Count; i++)
            {
                s += "(";
                for (int j = 0; j < coords[i].Count(); j++)
                {
                    s += "(";
                    for (int k = 0; k < coords[i][j].Count(); k++)
                    {
                        s += coords[i][j][k][0] + " " + coords[i][j][k][1];
                        if (k < coords[i][j].Count() - 1) s += ",";
                    }
                    s += ")";
                    if (j < coords[i].Count() - 1) s += ",";
                }
                s += ")";
                if (i < coords.Count - 1) s += ",";
            }
            s += ")";
            return s;
        }

        return null;
    }

    private static string CheckShpName(string layertype, string layername)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@LayerType",layertype),
            new SqlParameter("@LayerName",layername)
            };

            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Check_Layer_Name", p);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToString(dt.Rows[0]["InsertedId"]);
            }
            else
            {
                return "-1";
            }

        }
        catch
        {
            return "-1";
            //return "Error: " + ex.Message;
        }
    }

    //private static string saveShpName()
    //{
    //    string layerid = "";
    //    try
    //    {
    //        SqlParameter[] p = new SqlParameter[] {
    //        new SqlParameter("@LayerType",ddlLayerType.SelectedValue),
    //        new SqlParameter("@LayerName",Convert.ToString(txt_shpFileName.Text))
    //        };

    //        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Save_MapLayer", p);

    //        if (dt.Rows.Count > 0)
    //        {
    //            layerid = Convert.ToString(dt.Rows[0]["InsertedId"]);
    //        }
    //        else
    //        {
    //            layerid = "-1";
    //        }
    //    }
    //    catch 
    //    {
    //        layerid = "-1";
    //    }
    //    return layerid;
    //}


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
    public static void ExportLayer(string LayerType, string layerid)
    {
        try
        {
            // Define parameters for the stored procedure
            SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@LayerType", LayerType),
            new SqlParameter("@LayerID", layerid)
        };

            // Call your stored procedure and get the DataTable
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Export_Layer", p);

            if (dt.Rows.Count > 0)
            {
                // First, export the data to Excel
                ExportToExcel1(dt);

                // Then, return the DataTable (if needed)
                // If you need to return data to the client, you can return the DataTable here or in a different method
            }
            else
            {
                //throw new Exception("Error: No data returned from the stored procedure.");
            }
        }
        catch
        {
            // Log error (you can add your own logging mechanism here)
            //throw new Exception("Error: " + ex.Message);
        }
    }


    private static void ExportToExcel1(DataTable dt)
    {
        // Create a GridView to use for the export
        System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();

        gv.DataSource = dt;
        gv.DataBind();

        // Clear the response
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ExportedData.xls");
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

        // Write the data to the output stream
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                gv.RenderControl(hw);
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();

                // Instead of Response.End(), use CompleteRequest from HttpContext
                HttpContext.Current.Response.End();  // This is fine for ending the response
                HttpContext.Current.ApplicationInstance.CompleteRequest();  // Ensure the request is completed
            }
        }
    }





    //protected void btnMaplayer_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        SqlParameter[] p = new SqlParameter[] {
    //        new SqlParameter("@LayerType",ddlLayerType.SelectedValue)
    //        };

    //        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Map_Layer", p);

    //    }
    //    catch
    //    {

    //    }
    //}

    protected void btnexport_Click(object sender, EventArgs e)
    {
        try
        {
            // Get the layerid from the session
            string layerid = hiddenLayerId.Value; // Ensure the session key matches the one you stored

            // Check if layerid is null or empty
            if (string.IsNullOrEmpty(layerid))
            {
                // Handle the case where layerid is not available in the session
                // You can show an error message or take some other action
                //Label1.Text = "Please select layer";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please select any layer to import');", true);
                return; // Exit if layerid is not available
            }

            // Retrieve LayerType from the dropdown
            string layerType = "4";

            // Define parameters for the stored procedure
            SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@LayerType", layerType),
            new SqlParameter("@LayerID", layerid)
            };

            // Call your stored procedure and get the DataTable
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Export_Layer", p);

            // Export the data to Excel
            if (dt.Rows.Count > 0)
            {

                ExportToExcel(dt);

            }
            else
            {
                // Handle the case where no data is returned from the stored procedure
                //Label1.Text = "Error: No data returned from the stored procedure.";
            }
        }
        catch
        {

            // Handle the exception (you can log it or display an error message)
            //Label1.Text = "Error: " + ex.Message;

        }

    }

    private void ExportToExcel(DataTable dt)
    {
        DataTable exportDt = dt;

        System.Web.UI.WebControls.GridView gv =
        new System.Web.UI.WebControls.GridView();

        gv.DataSource = exportDt;
        gv.DataBind();

        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.AddHeader("content-disposition", "attachment;filename=GIS_Export.xls");
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
    public static void DeleteLayer(string layerid)
    {

        try
        {
            SqlParameter[] p = new SqlParameter[] {
            new SqlParameter("@LayerID",layerid)
            };

            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Delete_MapLayer", p);
        }
        catch
        {

        }
    }


    [WebMethod]
    public static string ExportShapefile(string fileName, object geojson, string layertype)
    {
        string msg = "";
        string name = "";
        if (geojson == null)
            throw new Exception("GeoJSON is missing");

        if (string.IsNullOrWhiteSpace(fileName))
            fileName = "Export";
        string dbfilename = fileName;
        fileName = fileName.Replace(" ", "_");

        // 1️⃣ Deserialize GeoJSON
        string geoJsonText = JsonConvert.SerializeObject(geojson);
        //GIS_Village Import
        if (CheckShpName(layertype, fileName) == "1")
        {



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

                // ---- DBF FIELD NAME FIX (<=10 chars) ----
                AttributesTable newAttributes = new AttributesTable();
                string[] attrNames = f.Attributes.GetNames();
                Dictionary<string, int> usedNames = new Dictionary<string, int>();

                for (int i = 0; i < attrNames.Length; i++)
                {
                    string original = attrNames[i];
                    string baseName = original.Length > 10
                        ? original.Substring(0, 10)
                        : original;

                    string finalName = baseName;

                    if (usedNames.ContainsKey(baseName))
                    {
                        usedNames[baseName]++;
                        finalName = baseName.Substring(0, 8) + usedNames[baseName];
                    }
                    else
                    {
                        usedNames[baseName] = 1;
                    }

                    // If it's a JToken/JValue, extract CLR value or string
                    object rawVal = f.Attributes[original];

                    if (rawVal != null && rawVal.GetType().FullName == "Newtonsoft.Json.Linq.JValue")
                    {
                        rawVal = ((Newtonsoft.Json.Linq.JValue)rawVal).Value;
                    }
                    else if (rawVal is Newtonsoft.Json.Linq.JObject || rawVal is Newtonsoft.Json.Linq.JArray)
                    {
                        // Convert complex JSON value to a string (DBF can't store structured types)
                        rawVal = rawVal.ToString();
                    }

                    // Normalize null / DBNull to empty string so GetHeader can infer string type
                    if (rawVal == null || rawVal == DBNull.Value)
                    {
                        rawVal = "";
                    }

                    // If numeric types are boxed as other IConvertible types they are kept as-is.
                    // Otherwise fall back to string to ensure a supported DBF type.
                    Type t = rawVal.GetType();
                    if (!(t == typeof(string) ||
                          t == typeof(int) || t == typeof(long) ||
                          t == typeof(float) || t == typeof(double) || t == typeof(decimal) ||
                          t == typeof(bool) || t == typeof(DateTime)))
                    {
                        rawVal = rawVal.ToString();
                    }

                    newAttributes.Add(finalName, rawVal);
                }

                // ✅ ADD FEATURE
                featuresList.Add(new Feature(g, newAttributes));
            }

            if (featuresList.Count == 0)
                throw new Exception("No valid geometries after repair");

            // 3️⃣ Output folder
            string folder = HttpContext.Current.Server.MapPath(Comman.GetImagePath("GeoPublishPath") + "/");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            name = fileName + "_" + DateTime.Now.Ticks;
            string shpPath = Path.Combine(folder, name + ".shp");

            // 4️⃣ Write shapefile
            ShapefileDataWriter writer = new ShapefileDataWriter(shpPath, gf);
            writer.Header = ShapefileDataWriter.GetHeader(featuresList[0], featuresList.Count);
            writer.Write(featuresList);

            // 5️⃣ Create .prj (WGS84)
            File.WriteAllText(Path.Combine(folder, name + ".prj"),
                "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\"," +
                "SPHEROID[\"WGS 84\",6378137,298.257223563]]," +
                "PRIMEM[\"Greenwich\",0],UNIT[\"degree\",0.0174532925199433]]");

            // 6️⃣ ZIP using Ionic.Zip (DotNetZip)
            string zipPath = Path.Combine(folder, name + ".zip");
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            using (ZipFile zip = new ZipFile())
            {
                string[] exts = { ".shp", ".shx", ".dbf", ".prj" };

                foreach (string ext in exts)
                {
                    string file = Path.Combine(folder, name + ext);
                    if (File.Exists(file))
                        zip.AddFile(file, "");
                }

                zip.Save(zipPath);
            }
            msg = PublishShapefileZip(geoServerUrl, "EG", name, folder, username, password, layertype, dbfilename, geoJsonText, fileName);


        }
        else
        {
            msg = "Layer name already exists";
        }



        return "" + msg + " [" + name + " ]";
    }

    public static string PublishShapefileZip(
        string geoServerUrl,
        string workspace,
        string datastore,
        string zipFilePath,
        string username,
        string password, string layertype, string dbfilename, string json, string fileName)
    {
        string url = geoServerUrl.TrimEnd('/') +
            "/rest/workspaces/" + workspace +
            "/datastores/" + datastore +
            "/file.shp";

        byte[] zipBytes = File.ReadAllBytes(zipFilePath + "/" + datastore + ".zip");

        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM [PMS].[dbo].[MapLayers] " +
                "WHERE [GeoserverLayerName] = @storeName", con))
            {
                da.SelectCommand.Parameters.Add("@storeName", SqlDbType.VarChar).Value = datastore;
                da.Fill(dt);
            }
        }
        if (dt.Rows.Count == 0)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
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
                    int newLayerId = 0;
                    // Insert DB record
                    using (SqlConnection conn = new SqlConnection(conStr))
                    {
                        conn.Open();
                        string q = @"INSERT INTO MapLayers (LayerName,GeoserverLayerName,LayerType,flag)
                             VALUES (@LayerName,@GeoserverLayerName,@layetype,'P');
                                SELECT CAST(SCOPE_IDENTITY() AS int);";

                        using (SqlCommand cmd = new SqlCommand(q, conn))
                        {
                            cmd.Parameters.AddWithValue("@LayerName", dbfilename);      // store name
                            cmd.Parameters.AddWithValue("@GeoserverLayerName", datastore); // UI name
                            cmd.Parameters.AddWithValue("@layetype", layertype);
                            //cmd.ExecuteNonQuery();
                            newLayerId = (int)cmd.ExecuteScalar();
                        }
                    }

                    if (newLayerId > 0)
                    {

                        string result = ImportGeoJson(json, fileName, layertype, newLayerId);

                        if (result != "SUCCESS")
                        {
                            return "Some Error Occured";
                        }
                    }
                    else
                    {
                        return "Some Error Occured";
                    }
                }
            }


        }
        else
        {
            return "Shapefile already exist";
        }
        return "Shapefile published successfully";

    }


    #endregion
}

















