using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class AESEncryption
{
    private const string SecretKey = "";



    public static string GetKey(string uniqueChildCode)
    {
        string secretKey = string.Empty;

        string sql = @"SELECT TOP 1 UniqueCode
                   FROM tblOOSC
                   WHERE UniqueChildCode = @UniqueChildCode";

        SqlParameter[] param =
        {
        new SqlParameter("@UniqueChildCode", SqlDbType.VarChar)
        {
            Value = uniqueChildCode
        }
    };

        DataSet ds = SqlHelper.GetDataSet(
            SqlHelper.mainConnectionString,
            CommandType.Text,
            sql,
            param);

        if (ds != null &&
            ds.Tables.Count > 0 &&
            ds.Tables[0].Rows.Count > 0)
        {
            secretKey = Convert.ToString(ds.Tables[0].Rows[0]["UniqueCode"]);
        }

        return secretKey;
    }
    private static byte[] GetKeyBytes(string uniqueChildCode)
    {
        string secretKey = GetKey(uniqueChildCode);

        using (SHA256 sha = SHA256.Create())
        {
            return sha.ComputeHash(
                Encoding.UTF8.GetBytes(secretKey));
        }
    }
    public static string Encrypt(string plainText, string textKey)
    {
        // Normalize Key (32 bytes)
        string keyString = NormalizeKey(textKey);

        // Normalize IV (16 bytes)
        string ivString = NormalizeIV(textKey);

        byte[] key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = Encoding.UTF8.GetBytes(ivString);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aes.CreateEncryptor();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs =
                    new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }


    public static string Decrypt(string cipherText, string textKey)
    {
        string keyString = NormalizeKey(textKey);
        string ivString = NormalizeIV(textKey);

        byte[] key = Encoding.UTF8.GetBytes(keyString);
        byte[] iv = Encoding.UTF8.GetBytes(ivString);

        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aes.CreateDecryptor();

            using (MemoryStream ms = new MemoryStream(buffer))
            using (CryptoStream cs =
                new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }

    public static string NormalizeKey(string textKey)
    {
        if (textKey.Length == 32)
            return textKey;

        if (textKey.Length < 32)
            return textKey.PadRight(32, '0');

        return textKey.Substring(0, 32);
    }

    public static string NormalizeIV(string textKey)
    {
        if (textKey.Length == 16)
            return textKey;

        if (textKey.Length < 16)
            return textKey.PadRight(16, '0');

        return textKey.Substring(0, 16);
    }


    #region Old

    private static byte[] GetKeyOld()
    {
        using (SHA256 sha = SHA256.Create())
        {
            return sha.ComputeHash(Encoding.UTF8.GetBytes(SecretKey));
        }
    }

    public static byte[] EncryptOld(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return null;

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKeyOld();
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (CryptoStream cs = new CryptoStream(
                    ms,
                    aes.CreateEncryptor(),
                    CryptoStreamMode.Write))
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(text);
                }

                return ms.ToArray();
            }
        }
    }

    public static string DecryptOld(byte[] encryptedData)
    {
        if (encryptedData == null || encryptedData.Length < 16)
            return string.Empty;

        using (Aes aes = Aes.Create())
        {
            aes.Key = GetKeyOld();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            byte[] iv = new byte[16];
            Array.Copy(encryptedData, 0, iv, 0, 16);
            aes.IV = iv;

            using (MemoryStream ms = new MemoryStream(
                encryptedData,
                16,
                encryptedData.Length - 16))
            using (CryptoStream cs = new CryptoStream(
                ms,
                aes.CreateDecryptor(),
                CryptoStreamMode.Read))
            using (StreamReader sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }

    #endregion
}