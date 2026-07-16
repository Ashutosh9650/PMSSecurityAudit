<%@ WebHandler Language="C#" Class="UploadShapefile" %>

using System;
using System.Web;
using System.IO;

public class UploadShapefile : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        HttpPostedFile file = context.Request.Files["file"];
        if (file == null)
        {
            context.Response.Write("No file");
            return;
        }

        using (StreamReader reader = new StreamReader(file.InputStream))
        {
            string geoJsonText = reader.ReadToEnd();
            context.Response.Write(geoJsonText);
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }
}
