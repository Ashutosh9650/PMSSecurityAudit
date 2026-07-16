using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for BLLSecurity
/// </summary>
//namespace BLLSecurity
//{

public class SqlInjection : IHttpModule//CommonBLL,
{

    //Defines the set of characters that will be checked. 
    //You can add to this list, or remove items from this list, as appropriate for your site. 



    public void Dispose()
    {
        //no-op  
    }

    //Tells ASP.NET that there is code to run during BeginRequest 
    public void Init(HttpApplication app)
    {
        app.BeginRequest += new EventHandler(app_BeginRequest);
        app.PreRequestHandlerExecute += new EventHandler(app_PreRequestHandlerExecute);
    }
    public bool CheckInputBoolNew(string parameter)
    {
        bool chkStatus = false;
        return chkStatus;
    }

    void app_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        HttpApplication context = (sender as HttpApplication);
        context.Response.ApplyAppPathModifier("www.mycii.in");
        //throw new NotImplementedException();
    }

    //For each incoming request, check the query-string, form and cookie values for suspicious values. 
    void app_BeginRequest(object sender, EventArgs e)
    {
        HttpApplication app = sender as HttpApplication;
        if (app == null || app.Context == null || app.Context.Request == null)
            return;

        HttpRequest Request = app.Context.Request;

        // 1. Validate Query String Parameters (GET parameters)
        if (Request.QueryString != null)
        {
            foreach (string key in Request.QueryString)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    CheckInput(Request.QueryString[key]);
                }
            }
        }

        // 2. Validate Form Data (POST parameters)
        if (Request.Form != null)
        {
            foreach (string key in Request.Form)
            {
                if (!string.IsNullOrEmpty(key))
                {
                    CheckInput(Request.Form[key]);
                }
            }
        }

        // 3. Validate Cookies (Safely handling multi-value/sub-key cookies)
        if (Request.Cookies != null)
        {
            foreach (string key in Request.Cookies)
            {
                HttpCookie cookie = Request.Cookies[key];
                if (cookie != null)
                {
                    if (cookie.HasKeys)
                    {
                        foreach (string subKey in cookie.Values)
                        {
                            CheckInput(cookie.Values[subKey]);
                        }
                    }
                    else
                    {
                        CheckInput(cookie.Value);
                    }
                }
            }
        }

        // 4. Validate HTTP Headers (Attackers often inject SQLi/XSS here)
        if (Request.Headers != null)
        {
            string[] targetHeaders = { "User-Agent", "Referer", "Host", "X-Forwarded-For" };
            foreach (string header in targetHeaders)
            {
                string headerValue = Request.Headers[header];
                if (!string.IsNullOrEmpty(headerValue))
                {
                    CheckInput(headerValue);
                }
            }
        }
 
    }

    //The utility method that performs the blacklist comparisons 
    //You can change the error handling, and error redirect location to whatever makes sense for your site. 

    public void CheckInput(string parameter)
    {
        bool chkCond = false;

        if (chkCond)
        {
            HttpContext.Current.Response.Redirect("~/ADMIN/Security", true);
        }

    }

    public bool CheckInputBool(string parameter)
    {
        bool chkStatus = false;

        return chkStatus;
    }

    private readonly byte[] key = Encoding.UTF8.GetBytes("EGKeyAESAdityaEG"); //replace with your own key

    public string Encrypt(string plainText)
    {
        byte[] encryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        return Convert.ToBase64String(encryptedBytes);
    }

    public string Decrypt(string encryptedText)
    {
        byte[] decryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        }
        return Encoding.UTF8.GetString(decryptedBytes);
    }


}
//}
