using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using System.Web.Configuration;
using System.Web.UI;
public partial class login : System.Web.UI.Page
{
    string ConStr = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ConStr"]);
    public System.Data.DataTable dtSessionGlobal = new DataTable();
    clsMain Objmain = new clsMain();
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            // Response.Redirect("http://educategirls.ngo/pms/");
            // string ip = Server.HtmlEncode(Request.UserHostAddress);



        }

    }





    PageStatePersister pageStatePersister;
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            // Unlike as exemplified in the MSDN docs, we cannot simply return a new PageStatePersister
            // every call to this property, as it causes problems
            return pageStatePersister ?? (pageStatePersister = new SessionPageStatePersister(this));
        }
    }

    public void Userlogin()
    {

        try
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            if (!string.IsNullOrWhiteSpace(username) &&
                !string.IsNullOrWhiteSpace(password))
            {
                string EnpPassword = txtPassword.Text;

                SqlParameter[] paramsToStore = new SqlParameter[]
        {
        new SqlParameter("@UserName",txtUserName.Text),
        };

                DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Login_CheckUser", paramsToStore);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(
                        Page,
                        GetType(),
                        "Message",
                        "<script>alert('Invalid User ID or Password');</script>",
                        false);

                    return;
                }

                bool itlogin = true;


                if (ds.Tables[0].Rows.Count > 0)
                {



                    if (ds.Tables.Count > 0 && itlogin == true)
                    {
                        bool allowlogin = true;
                        string Cdaytime = "";
                        if (ds.Tables[0].Rows.Count > 0)
                        {



                            if (allowlogin)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    bool checkpass = Password.VerifyPassword(EnpPassword, ds.Tables[0].Rows[0]["Password"].ToString());
                                    if (checkpass)
                                    {
                                        LoadYear();
                                        Session.Clear();


                                        // FIX: never store the plaintext password in session state.                                        
                                        Session["username"] = ds.Tables[0].Rows[0]["UserName"].ToString();

                                        Session["StateCode"] = ds.Tables[0].Rows[0]["StateCode"].ToString();

                                        Session["DistrictCode"] = ds.Tables[0].Rows[0]["DistrictCode"].ToString();
                                        Session["BlockCode"] = ds.Tables[0].Rows[0]["BlockCode"].ToString();
                                        try
                                        {
                                            //var host = Dns.GetHostEntry(Dns.GetHostName());
                                            //string hostName = Dns.GetHostName();
                                            //string IPadd = "";
                                            //string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                                            //foreach (var ip in host.AddressList)
                                            //{
                                            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
                                            //    {
                                            //        IPadd = ip.ToString();
                                            //    }
                                            //}

                                            string IPAdd1 = string.Empty;
                                            IPAdd1 = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                            if (string.IsNullOrEmpty(IPAdd1))
                                                IPAdd1 = Request.ServerVariables["REMOTE_ADDR"];


                                            int LOGID = LoginLogoutInsertUpdate(ds.Tables[0].Rows[0]["UserName"].ToString(), IPAdd1);

                                            Session["LOGID"] = LOGID;
                                        }
                                        catch (Exception ex)
                                        {
                                            System.Diagnostics.Trace.TraceError(ex.ToString());
                                        }
                                        DataTable dtdist = ds.Tables[1];
                                        DataTable dtBlock = ds.Tables[2];
                                        DataTable dtdist1 = ds.Tables[3];
                                        DataTable dtdistMul = ds.Tables[4];

                                        DataTable dtGIS = ds.Tables[5];
                                        if (dtGIS.Rows.Count > 0)
                                        {
                                            Session["DistrictCodeGIS"] = dtGIS.Rows[0]["DistrictCode"].ToString();
                                        }
                                        else
                                        {
                                            Session["DistrictCodeGIS"] = "";
                                        }
                                        DataTable dtGIS6 = ds.Tables[7];
                                        if (dtGIS6.Rows.Count > 0)
                                        {
                                            Session["DistrictCodeGIS2025"] = dtGIS6.Rows[0]["DistrictCode"].ToString();
                                        }
                                        else
                                        {
                                            Session["DistrictCodeGIS2025"] = "";
                                        }
                                        DataTable dtGISbl = ds.Tables[6];
                                        if (dtGISbl.Rows.Count > 0)
                                        {
                                            Session["BlockCodeGIS"] = dtGISbl.Rows[0]["BlockCode"].ToString();
                                        }
                                        else
                                        {
                                            Session["BlockCodeGIS"] = "";
                                        }

                                        DataTable dtGIS86 = ds.Tables[8];
                                        if (dtGIS86.Rows.Count > 0)
                                        {
                                            Session["DistrictCodeGIS2026"] = dtGIS86.Rows[0]["DistrictCode"].ToString();
                                        }
                                        else
                                        {
                                            Session["DistrictCodeGIS2026"] = "";
                                        }
                                        //Session["DistrictCodeGIS"] = dtGIS.Rows[0]["DistrictCode"].ToString();




                                        string dist = "";
                                        string distMul = "";
                                        string Block = "";

                                        if (dtdistMul.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dtdistMul.Rows)
                                            {
                                                distMul += "'" + dr["DistrictCode"] + "'" + ",";
                                            }
                                        }


                                        if (dtdist.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dtdist.Rows)
                                            {
                                                dist += "'" + dr["DistrictCode"] + "'" + ",";
                                                if (Convert.ToString(dr["DistrictCode"]).Length > 5 && Convert.ToString(dr["isMulti"]) == "1")
                                                {

                                                    dist += "'" + dr["RDistrictCode"] + "'" + ",";
                                                }
                                            }
                                        }

                                        if (dtdist1.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dtdist1.Rows)
                                            {
                                                dist += "'" + dr["DistrictCode"] + "'" + ",";
                                            }
                                        }
                                        if (dtBlock.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dtBlock.Rows)
                                            {
                                                Block += "'" + dr["BlockCode"] + "'" + ",";
                                            }
                                        }
                                        if (dist.Length > 0)
                                        {
                                            dist = dist.Substring(0, dist.LastIndexOf(","));
                                        }
                                        if (Block.Length > 0)
                                        {
                                            Block = Block.Substring(0, Block.LastIndexOf(","));
                                        }
                                        if (distMul.Length > 0)
                                        {
                                            distMul = distMul.Substring(0, distMul.LastIndexOf(","));
                                        }
                                        Session["DistrictCode"] = dist;
                                        Session["BlockCode"] = Block;

                                        Session["DistrictCodeNew"] = dist;
                                        Session["BlockCodeNew"] = Block;

                                        Session["DistrictCodeMul"] = distMul;

                                        Session["Villagecode"] = ds.Tables[0].Rows[0]["Villagecode"].ToString();
                                        Session["user_level"] = ds.Tables[0].Rows[0]["UserLevel"].ToString();
                                        Session["UserID"] = ds.Tables[0].Rows[0]["UserID"].ToString();
                                        Session["UserRole"] = ds.Tables[0].Rows[0]["UserRole"].ToString();
                                        Session["EmpName"] = ds.Tables[0].Rows[0]["EmpName"].ToString();
                                        Session["user_level_Role"] = ds.Tables[0].Rows[0]["RID"].ToString();
                                        Session["NewDistrictCode"] = ds.Tables[0].Rows[0]["NewDistrictCode"].ToString();
                                        Session["NewBlockCode"] = ds.Tables[0].Rows[0]["NewBlockCode"].ToString();
                                        if (ds.Tables[0].Rows[0]["ForcePassword"] != DBNull.Value && !string.IsNullOrWhiteSpace(ds.Tables[0].Rows[0]["ForcePassword"].ToString()))
                                        {
                                            Session["ForcePassword"] = Convert.ToInt32(ds.Tables[0].Rows[0]["ForcePassword"]);
                                        }

                                        //LoadCheck();
                                        string IPAdd = string.Empty;
                                        IPAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                                        if (string.IsNullOrEmpty(IPAdd))
                                            IPAdd = Request.ServerVariables["REMOTE_ADDR"];

                                        //  GetCountryByIP(IPAdd);
                                        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");

                                        using (SqlConnection con = new SqlConnection(SqlHelper.mainConnectionString))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("USP_UpdateUserLogin", con))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                cmd.Parameters.Add("@LoginDateTime", SqlDbType.DateTime)
                                                    .Value = DateTime.Now;

                                                cmd.Parameters.Add("@IPAddress", SqlDbType.NVarChar, 100)
                                                    .Value = IPAdd;

                                                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100)
                                                    .Value = Session["username"].ToString();

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        Response.Redirect("Dashboard.aspx", false);



                                    }
                                    else
                                    {
                                        string mmsg = "";
                                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Password is wrong')</script>", false);
                                        return;
                                    }
                                }
                                else
                                {
                                    string mmsg = "";
                                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Username or password is wrong')</script>", false);
                                    return;
                                }
                            }
                        }
                        else
                        { }
                    }

                    else
                    {
                        string mmsg = "Inactive User";
                        // lblMessage.Text = mmsg;
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid userid/Password')</script>", false);
                    //lblMessage.Text = mmsg;

                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid userid/Password')</script>", false);

            }
        }
        catch (Exception ex)
        {
            // FIX (CWE-209 Improper Error Handling): log the full exception on the server only,
            // and show the user a generic message. Never echo ex.Message / stack traces / DB
            // errors to the client. Guard against index errors from message parsing.
            System.Diagnostics.Trace.TraceError("Login error: " + ex);
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message",
                "<script type='text/javascript'>alert('Unable to sign in. Please try again later.');</script>", false);
            return;
        }

    }
    public int LoginLogoutInsertUpdate(string UserID, string IPAddess)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "rptUserLoginLogout";
        dbSqlCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = UserID;
        dbSqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = IPAddess;
        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "I";
        dbSqlCommand.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = "0";

        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.ToString());
            //return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }

    public void LoadYear()
    {
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;


        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;

        DataRow dr;


        string mYear1 = GivenYear1.ToString();
        for (int j = 0; j < 1; j++)
        {
            if (m > 3)
            {

                Session["FinYear"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));

                //get last  two digits (eg: 10 from 2010);

            }
            else
            {

                Session["FinYear"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                //y = y - 1;




            }

        }


        //}


    }

    protected void Encryption(bool EncryptoValue)
    {
        try
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            ConfigurationSection sec = config.GetSection("connectionStrings");
            if (EncryptoValue == true)
            {
                sec.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            }
            else
            {
                sec.SectionInformation.UnprotectSection();
            }
            config.Save();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError(ex.ToString());
            // lblMessage.Text = mmsg;
        }

    }
    public void LoadCheck()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        string IPadd = "";
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                IPadd = ip.ToString();
            }
        }

        //string myExternalIP;
        //string strHostName = System.Net.Dns.GetHostName();
        //string clientIPAddress = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
        //string clientip = clientIPAddress.ToString();
        //System.Net.HttpWebRequest request =

        //(System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create("http://www.whatismyip.org");

        //request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE" +

        //    "6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        //System.Net.HttpWebResponse response =

        //(System.Net.HttpWebResponse)request.GetResponse();

        //using (System.IO.StreamReader reader = new

        //StreamReader(response.GetResponseStream()))
        //{

        //    myExternalIP = reader.ReadToEnd();

        //    reader.Close();

        //}
        GetLocation(IPadd.ToString());
        //lblip.Text = myExternalIP.ToString();
    }
    private DataTable GetLocation(string ipaddress)
    {
        WebRequest rssReq = WebRequest.Create("http://freegeoip.appspot.com/xml/" + ipaddress);
        WebProxy px = new WebProxy("http://freegeoip.appspot.com/xml/" + ipaddress, true);
        rssReq.Proxy = px;
        rssReq.Timeout = 2000;
        try
        {
            WebResponse rep = rssReq.GetResponse();
            System.Xml.XmlTextReader xtr = new System.Xml.XmlTextReader(rep.GetResponseStream());
            DataSet ds = new DataSet();
            ds.ReadXml(xtr);
            return ds.Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        Userlogin();
    }



    // FIX (CWE-89 SQL Injection): These helpers previously executed an arbitrary,
    // caller-supplied query string via `new SqlCommand(strQry, Con)`. Any concatenated
    // user input reaching them was directly injectable. They now REQUIRE the caller to
    // pass a parameterized command text plus SqlParameter values, so user data can never
    // be interpreted as SQL. Callers must NEVER build `sql` by concatenating user input;
    // use @parameters and pass values through `parameters`.
    public DataTable GetResultFromQuery(string sql, params SqlParameter[] parameters)
    {
        DataTable dtResult = new DataTable();
        using (SqlConnection con = new SqlConnection(ConStr))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.CommandTimeout = 30;
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            try
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dtResult);
                }
            }
            catch (Exception ex)
            {
                // FIX (CWE-209 Improper Error Handling): log server-side, do not swallow silently
                // and do not surface DB errors/stack traces to the client.
                System.Diagnostics.Trace.TraceError("GetResultFromQuery failed: " + ex);
                throw;
            }
        }
        return dtResult;
    }

    public void ExecutenonQuery(string sql, params SqlParameter[] parameters)
    {
        using (SqlConnection con = new SqlConnection(ConStr))
        using (SqlCommand cmd = new SqlCommand(sql, con))
        {
            cmd.CommandTimeout = 30;
            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("ExecutenonQuery failed: " + ex);
                throw;
            }
        }
    }


}