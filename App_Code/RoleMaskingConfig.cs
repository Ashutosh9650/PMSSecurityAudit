using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class FieldMaskConfig
{
    public int IsMasked { get; set; }
    public string MaskType { get; set; }
}

public class RoleMaskingConfig
{
    private const string SESSION_KEY = "RoleMaskingConfig";
    
    private static readonly object _syncLock = new object();
    private static RoleMaskingConfig _instance;

    public static RoleMaskingConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new RoleMaskingConfig();
                    }
                }
            }
            return _instance;
        }
    }

    private RoleMaskingConfig() { }

    public void LoadConfigToSession(int roleId)
    {
        try
        {
            var config = LoadMaskingConfigsFromDatabase(roleId);

            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[SESSION_KEY] = config;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error loading config to session: " + ex.Message);
        }
    }

    private Dictionary<string, FieldMaskConfig> LoadMaskingConfigsFromDatabase(int roleId)
    {
        var configs = new Dictionary<string, FieldMaskConfig>(StringComparer.OrdinalIgnoreCase);

        try
        {
            string query = "SELECT FieldName, IsMasked, ISNULL(MaskType, 'NAME') AS MaskType FROM MstRoleMaskingConfig WHERE RID = @RID ORDER BY FieldName";

            SqlParameter[] parameters = new SqlParameter[]
            {
                 new SqlParameter("@RID", roleId)
            };
            var dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.Text, query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string fieldName = Convert.ToString(row["FieldName"]);
                    int isMasked = Convert.ToInt32(row["IsMasked"]);
                    string maskType = Convert.ToString(row["MaskType"]);

                    configs[fieldName] = new FieldMaskConfig
                    {
                        IsMasked = isMasked,
                        MaskType = string.IsNullOrWhiteSpace(maskType) ? "NAME" : maskType
                    };
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error loading MstRoleMaskingConfig from database: " + ex.Message);
        }

        return configs;
    }

    public Dictionary<string, FieldMaskConfig> GetConfigFromSession()
    {
        try
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                var config = HttpContext.Current.Session[SESSION_KEY] as Dictionary<string, FieldMaskConfig>;
                if (config != null)
                {
                    return config;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error getting config from session: " + ex.Message);
        }

        return new Dictionary<string, FieldMaskConfig>();
    }
    
    public Tuple<bool, string> GetMaskingDetails(string fieldName, Dictionary<string, FieldMaskConfig> config)
    {
        if (string.IsNullOrEmpty(fieldName))
            return Tuple.Create(false, "NAME");

        if (config == null)
        {
            config = RoleMaskingConfig.Instance.GetConfigFromSession();
        }

        if (config == null || config.Count == 0 || string.IsNullOrEmpty(fieldName)) 
            return Tuple.Create(false, "NAME");

         if (config.ContainsKey(fieldName))
         {
            var item = config[fieldName];
            return Tuple.Create(item.IsMasked == 1, item.MaskType);
         }


        string normIncoming = Normalize(fieldName);

        foreach (var kvp in config)
        {
            if (kvp.Value.IsMasked == 1)
            {
                string normDbKey = Normalize(kvp.Key);

                if (normDbKey == normIncoming ||
                    normDbKey.EndsWith(normIncoming) ||
                    normIncoming.EndsWith(StripPrefix(normDbKey)))
                {
                    return Tuple.Create(true, kvp.Value.MaskType);
                }
            }
        }

        return Tuple.Create(false, "NAME");
    }

    private string Normalize(string input)
    {
        if (string.IsNullOrEmpty(input)) return "";
        return input.Replace(" ", "").Replace("_", "").Replace("-", "").ToLowerInvariant();
    }

    private string StripPrefix(string input)
    {
        int idx = input.IndexOf('_');
        return (idx >= 0 && idx < input.Length - 1) ? input.Substring(idx + 1) : input;
    }
 
    public void ClearSessionConfig()
    {
        try
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Remove(SESSION_KEY);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error clearing session config: " + ex.Message);
        }
    }
}
