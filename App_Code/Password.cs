using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Password
/// </summary>
public class Password
{
    private static readonly int Password_saltArraySize = Comman.PasswordSaltSize;
    //public Password()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    public static string CreateSalt(int arraysize)
    {
        // Generate a cryptographic random number using the cryptographic
        // service provider
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] buff = new byte[arraysize];
        rng.GetBytes(buff);
        // Return a Base64 string representation of the random number
        return Convert.ToBase64String(buff);
    }
    public string CreatePasswordHashSecurityAudit(string pwd)
    {

        return pwd;
    }
    public static string CreatePasswordHash(string pwd)
    {
        string saltAndPwd = String.Concat(pwd, Password_saltArraySize.ToString());
        HashAlgorithm hashAlgorithm = SHA512.Create();
        List<byte> pass = new List<byte>(Encoding.Unicode.GetBytes(saltAndPwd));
        string hashedPwd = Convert.ToBase64String(hashAlgorithm.ComputeHash(pass.ToArray()));
        hashedPwd = String.Concat(hashedPwd, Password_saltArraySize.ToString());
        return hashedPwd;
    }

    public string CreatePasswordHashNew(string pwd)
    {
        string saltAndPwd = String.Concat(pwd, Password_saltArraySize.ToString());
        HashAlgorithm hashAlgorithm = SHA512.Create();
        List<byte> pass = new List<byte>(Encoding.Unicode.GetBytes(saltAndPwd));
        string hashedPwd = Convert.ToBase64String(hashAlgorithm.ComputeHash(pass.ToArray()));
        hashedPwd = String.Concat(hashedPwd, Password_saltArraySize.ToString());
        return hashedPwd;
    }

    //public static string CreatePasswordHash(string pwd)
    //{
    //    string saltAndPwd = String.Concat(pwd, Password_saltArraySize.ToString());
    //    HashAlgorithm hashAlgorithm = SHA256.Create();
    //    List<byte> pass = new List<byte>(Encoding.Unicode.GetBytes(saltAndPwd));
    //    string hashedPwd = Convert.ToBase64String(hashAlgorithm.ComputeHash(pass.ToArray()));
    //    hashedPwd = String.Concat(hashedPwd, Password_saltArraySize.ToString());
    //    return hashedPwd;
    //}

    public static bool VerifyPassword(string userPassword, string dbUserPassword)
    {
        bool passwordMatch = false;
        if (dbUserPassword.Equals(""))
            return false;
        string salt = dbUserPassword.Substring(dbUserPassword.Length - CreateSalt(Password_saltArraySize).Length);
        string hashedPasswordAndSalt = CreatePasswordHash(userPassword);
        passwordMatch = hashedPasswordAndSalt.Equals(dbUserPassword);
        return passwordMatch;
    }

    public static bool CheckPasswordAgainstPolicy(string username, string password)
    {
        bool passwordValid = true;
        string patthern = @"^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d]).*$";
        // string patthern = @"^.*(?=.{6,})(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[$]).*$"; 
        //Password should contain one upper case letter, one lower case letter, one special character and one digit and should be atlease of six characters
        if (!Regex.IsMatch(password, patthern))
        {
            passwordValid = false;
        }
        //else if (password.ToLower().Contains(username.ToLower()) || CheckPatterInString(username, password))
        //else if (CheckPatterInString(username, password))
        //{
        //    passwordValid = false;
        //}
        return passwordValid;
    }

    public static string checkpasswordCharacters(string password)
    {
        string msg = "";
        Regex len = new Regex("^.{6,15}$");
        Regex num = new Regex("\\d");
        // Regex alphaL = new Regex("\\D");
        Regex alphaL = new Regex("[a-z]");
        Regex alphaU = new Regex("[A-Z]");
        // Regex special = new Regex("[><#@=._!$-]"); 

        if (!len.IsMatch(password))
        {
            msg += "-Minimum password length is 6 characters <br/>";
        }
        if (!num.IsMatch(password))
        {
            msg += "-Please enter atleast one numeric value <br/>";
        }
        if (!alphaL.IsMatch(password))
        {
            msg += "-Please enter atleast one lower case alphabet <br/>";
        }
        if (!alphaU.IsMatch(password))
        {
            msg += "-Please enter atleast one upper case alphabet <br/>";
        }
        //if (!special.IsMatch(password))
        //{
        //    msg += "-Please enter atleast one special Character <br/>";
        //}

        return msg;

    }
    // private static string checkpsdpattern(string str)
    // {    
    //string msg="";

    //string password = "456tgfd>";
    //Regex len = new Regex("^.{8,10}$");
    //Regex num = new Regex("\\d");
    //Regex alpha = new Regex("\\D");
    //Regex special = new Regex("[><%#@]"); // Put  here more special characters

    //if (len.IsMatch(password) && num.IsMatch(password) && alpha.IsMatch(password) && special.IsMatch(password))
    //{
    //    // successful match - write your code here
    //}
    // return msg;


    // }

    private static bool CheckPatterInString(string username, string password)
    {
        bool passwordValid = false;

        for (int i = 0; i < username.Length - 3; i++)
        {
            if (password.ToLower().Contains(username.ToLower().Substring(i, 3)))
            {
                passwordValid = true;
                break;
            }
        }
        return passwordValid;
    }

    public static string EncryptSHA512Managed(string password)
    {
        UnicodeEncoding uEncode = new UnicodeEncoding();
        byte[] bytPassword = uEncode.GetBytes(password);
        SHA512Managed sha = new SHA512Managed();
        byte[] hash = sha.ComputeHash(bytPassword);
        return Convert.ToBase64String(hash);
    }

    public static string GetCrypt(string text)
    {
        string hash = "";
        SHA512 alg = SHA512.Create();
        byte[] result = alg.ComputeHash(Encoding.UTF8.GetBytes(text));
        hash = Encoding.UTF8.GetString(result);
        return hash;
    }

    public static bool RestrictSpecialChars(string str)//without space
    {
        Regex myRegularExpression = new Regex("^[a-zA-Z0-9]+$");
        return myRegularExpression.IsMatch(str);
    }

    public static bool RestrictSpecialChars_spaces(string str)//with space also
    {
        Regex myRegularExpression = new Regex("^[a-zA-Z0-9\x20]+$");
        return myRegularExpression.IsMatch(str);
    }

    public static bool Restrictonly_numeric(string str)//with space also
    {
        Regex myRegularExpression = new Regex("^[0-9]+$");
        return myRegularExpression.IsMatch(str);
    }


    //all others also
    //public bool RestrictSpecialCharacters(string str)
    //{
    //    // Regex myRegularExpression = new Regex("^[a-zA-Z0-9]+$");// alphanumeric without space
    //    // Regex myRegularExpression = new Regex("^[a-zA-Z0-9\x20]+$");//alphanumeric with space  
    //    // Regex myRegularExpression = new Regex("^[0-9]+$");//numeric only 
    //    //  Regex myRegularExpression = new Regex("^[0-9\x20]+$");//numeric with space 
    //    //  Regex myRegularExpression = new Regex("^[a-zA-Z]+$");//char only
    //    Regex myRegularExpression = new Regex("^[a-zA-Z\x20]+$");//char with space 
    //    return myRegularExpression.IsMatch(str);
    //}
}