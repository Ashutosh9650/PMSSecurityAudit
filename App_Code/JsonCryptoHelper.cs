using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PMS.Crypto.Core;
using System;
using System.Collections.Generic;

public class JsonCryptoHelper
{
    public string EncryptJsonFields(string jsonString, string[] fieldsToEncrypt)
    {
        if (string.IsNullOrEmpty(jsonString)) return jsonString;

        try
        {
            JToken token = JToken.Parse(jsonString);

            HashSet<string> targetFields = new HashSet<string>(fieldsToEncrypt, StringComparer.OrdinalIgnoreCase);

            EncryptFieldsRecursive(token, targetFields);

            return token.ToString(Formatting.None);
        }
        catch (Exception)
        {
            return jsonString;
        }
    }

    private void EncryptFieldsRecursive(JToken token, HashSet<string> fieldsToEncrypt)
    {
        if (token.Type == JTokenType.Object)
        {
            foreach (JProperty property in token.Children<JProperty>())
            {
                if (fieldsToEncrypt.Contains(property.Name))
                {
                    if (property.Value != null &&
                       (property.Value.Type == JTokenType.String ||
                        property.Value.Type == JTokenType.Integer ||
                        property.Value.Type == JTokenType.Float))
                    {
                        string originalValue = property.Value.ToString();
                        if (!string.IsNullOrEmpty(originalValue))
                        {
                            string encryptedValue = string.Empty;

                            // BouncyCastle
                            encryptedValue = CryptoService.Encrypt(originalValue);

                            property.Value = encryptedValue;
                        }
                    }
                }
                else
                {
                    EncryptFieldsRecursive(property.Value, fieldsToEncrypt);
                }
            }
        }
        else if (token.Type == JTokenType.Array)
        {
            foreach (JToken child in token.Children())
            {
                EncryptFieldsRecursive(child, fieldsToEncrypt);
            }
        }
    }
}