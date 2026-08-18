using PMS.Crypto.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
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
                cookie.HttpOnly = true;
                cookie.Secure = true;
                cookie.SameSite = SameSiteMode.Strict;
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

    public string MaskSensitiveData(string plainText, string maskType = "NAME")
    {
        if (string.IsNullOrEmpty(plainText)) return plainText;

        switch ((maskType ?? "").ToUpperInvariant())
        {
            case "MOBILE":
            case "PHONE":
            case "CONTACT":
                return MaskMobile(plainText);

            case "AADHAAR":
            case "AADHAR":
                return MaskAadhaar(plainText);

            case "EMAIL":
                return MaskEmail(plainText);

            case "DOB":
            case "DATE":
                return MaskDob(plainText);

            case "ID":
            case "SSSMID":
            case "SAMAGRA":
                return MaskId(plainText);

            case "LOCATION":
                return MaskLocation(plainText);

            case "NAME":
            default:
                return MaskName(plainText);
        }
    }

    #region Standard PII Masking Routines
    private string MaskLocation(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText)) return plainText;

        plainText = plainText.Trim();

        if (plainText.Contains(","))
        {
            string[] parts = plainText.Split(',');
            if (parts.Length == 2)
            {
                return MaskSingleCoordinate(parts[0].Trim()) + ", " + MaskSingleCoordinate(parts[1].Trim());
            }
        }

        return MaskSingleCoordinate(plainText);
    }

    private string MaskSingleCoordinate(string coord)
    {
        if (string.IsNullOrWhiteSpace(coord)) return coord;

        int decimalIdx = coord.IndexOf('.');

        if (decimalIdx > 0 && decimalIdx + 2 < coord.Length)
        {
            string visiblePart = coord.Substring(0, decimalIdx + 2);
            int maskLength = coord.Length - visiblePart.Length;
            return visiblePart + new string('*', maskLength);
        }

        if (coord.Length > 2)
        {
            return coord.Substring(0, 2) + new string('*', coord.Length - 2);
        }

        return coord;
    }

    private string MaskName(string name)
    {
        string[] words = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            string word = words[i];
            if (word.Length > 1)
            {
                words[i] = word[0] + new string('*', word.Length - 1);
            }
        }
        return string.Join(" ", words);
    }

    private string MaskMobile(string mobile)
    {
        string digits = Regex.Replace(mobile, @"\D", "");
        if (digits.Length >= 10)
        {
            int len = digits.Length;
            return digits.Substring(0, 2) + new string('*', len - 4) + digits.Substring(len - 2);
        }
        return MaskName(mobile);
    }

    private string MaskAadhaar(string aadhaar)
    {
        string digits = Regex.Replace(aadhaar, @"\D", "");
        if (digits.Length == 12)
        {
            return "XXXX-XXXX-" + digits.Substring(8);
        }
        return "XXXX-XXXX-XXXX";
    }

    private string MaskEmail(string email)
    {
        if (!email.Contains("@")) return MaskName(email);

        string[] parts = email.Split('@');
        string username = parts[0];
        string domain = parts[1];

        if (username.Length > 2)
        {
            username = username[0] + new string('*', username.Length - 2) + username[username.Length - 1];
        }
        else if (username.Length > 0)
        {
            username = username[0] + "*";
        }

        return username + "@" + domain;
    }

    private string MaskDob(string dobStr)
    {
        if (string.IsNullOrWhiteSpace(dobStr))
            return dobStr;

        dobStr = dobStr.Trim();


        if (dobStr.StartsWith("XX/XX/", StringComparison.OrdinalIgnoreCase))
            return dobStr;


        string[] formats = new string[]
        {
        "dd/MM/yyyy", "dd-MM-yyyy", "d/M/yyyy", "d-M-yyyy",
        "yyyy-MM-dd", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss",
        "dd/MM/yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss",
        "MM/dd/yyyy", "MMM d yyyy hh:mmtt", "MMM dd yyyy hh:mmtt"
        };

        DateTime dobDate;


        if (DateTime.TryParseExact(dobStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dobDate) ||
            DateTime.TryParse(dobStr, CultureInfo.GetCultureInfo("en-IN"), DateTimeStyles.None, out dobDate) ||
            DateTime.TryParse(dobStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out dobDate))
        {
            if (dobDate.Year <= 1900)
                return "XX/XX/XXXX";

            return "XX/XX/" + dobDate.Year.ToString();
        }


        Match match = Regex.Match(dobStr, @"(19|20)\d{2}");
        if (match.Success)
        {
            if (match.Value == "1900")
                return "XX/XX/XXXX";

            return "XX/XX/" + match.Value;
        }

        return "XX/XX/XXXX";
    }

    private string MaskId(string idStr)
    {
        if (idStr.Length > 4)
        {
            return new string('*', idStr.Length - 4) + idStr.Substring(idStr.Length - 4);
        }
        return idStr;
    }

    #endregion

    public string UdfDateDiffinYrMonDay(DateTime dateFrom, DateTime dateTo)
    {
        int years = dateTo.Year - dateFrom.Year;

        if (dateFrom.AddYears(years) > dateTo)
        {
            years--;
        }

        return years.ToString();
    }

    public void ProcessRowAgeAndDob(DataRow row, string decryptedDOB, string fieldName = "")
    {
        DataTable dt = row.Table;
        DateTime dobDate;

        if (!string.IsNullOrEmpty(decryptedDOB) && decryptedDOB != "01/01/1900" && decryptedDOB != "1900-01-01" && DateTime.TryParse(decryptedDOB, out dobDate))
        {
            if (dt.Columns.Contains("DOB"))
            {
                string plainDobStr = dobDate.ToString("dd/MM/yyyy");

                if (!string.IsNullOrEmpty(fieldName))
                {
                    var dob = MaskDob(decryptedDOB);
                    row[fieldName] = dob;
                }
            }

            if (dt.Columns.Contains("EnrolmentDate") && row["EnrolmentDate"] != DBNull.Value && dt.Columns.Contains("Age"))
            {
                string enrolDateStr = row["EnrolmentDate"].ToString();
                DateTime enrolmentDate;

                string[] allowedFormats = {
                      "dd/MM/yyyy",
                      "dd-MM-yyyy HH:mm:ss",
                      "dd/MM/yyyy HH:mm:ss",
                      "yyyy-MM-dd HH:mm:ss"
                };

                if (!string.IsNullOrEmpty(enrolDateStr) &&
                    DateTime.TryParseExact(enrolDateStr, allowedFormats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out enrolmentDate))
                {
                    row["Age"] = UdfDateDiffinYrMonDay(dobDate, enrolmentDate);
                }
                else
                {
                    row["Age"] = "";
                }
            }
        }
        else
        {
            if (dt.Columns.Contains("DOB")) row["DOB"] = "";
            if (dt.Columns.Contains("Age")) row["Age"] = "";
        }
    }

    public string DecryptMatchingWithSessionMasking(string encryptedData, string fieldName, bool? applyOnField = true, Dictionary<string, FieldMaskConfig> preLoadedConfig = null)
    {
        if (string.IsNullOrEmpty(encryptedData))
            return encryptedData;

        // First decrypt the data using matching key
        string decryptedValue = CryptoService.Decrypt(encryptedData);

        // Check if this field should be masked using session config
        if (applyOnField.HasValue && applyOnField.Value)
        {
            Tuple<bool, string> maskingDetails = RoleMaskingConfig.Instance.GetMaskingDetails(fieldName, preLoadedConfig);

            if (maskingDetails.Item1)
            {
                return this.MaskSensitiveData(decryptedValue, maskingDetails.Item2);
            }
        }

        return decryptedValue;
    }

    public string ToTitleCase(string inputString)
    {
        if (string.IsNullOrEmpty(inputString))
        {
            return string.Empty;
        }

        char[] chars = inputString.ToLower().ToCharArray();

        chars[0] = char.ToUpper(chars[0]);

        HashSet<char> delimiters = new HashSet<char>
        {
            ' ', ';', ':', '!', '?', ',', '.', '_', '-', '/', '&', '\'', '('
        };

        for (int i = 0; i < inputString.Length; i++)
        {
            char currentChar = inputString[i];

            if (delimiters.Contains(currentChar))
            {
                if (i + 1 < inputString.Length)
                {
                    if (currentChar == '\'' && char.ToUpper(inputString[i + 1]) == 'S')
                    {
                        continue;
                    }
                    chars[i + 1] = char.ToUpper(chars[i + 1]);
                }
            }
        }
        return new string(chars);
    }
}

