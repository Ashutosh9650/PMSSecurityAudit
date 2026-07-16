<%@ WebHandler Language="C#" Class="HandlerImage" %>

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
public class HandlerImage : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        if (context.Request.Files.Count > 0)
        {
            //Fetch the Uploaded File.
            HttpPostedFile postedFile = context.Request.Files[0];

            //Set the Folder Path.
            string folderPath = context.Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
            string User = "";
            string FormID ="IMG-Payment";
            //Set the File Name.
            string fileName = Path.GetFileName(postedFile.FileName);
            string exten = Path.GetExtension(postedFile.FileName);
            /// string      Fullfilename = "" + FormID + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            //string      Fullfilename = "" + clsMain.TraveUserID  + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            string      Fullfilename ="" + clsMain.TraveUserID   + "_" + "" + FormID + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
            //Save the File in Folder.
            postedFile.SaveAs(folderPath + Fullfilename);
            clsMain.TravelImageID  = Fullfilename;
            //Send File details in a JSON Response.
            string json = new JavaScriptSerializer().Serialize(
                new
                {
                    name = fileName
                });
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
        }

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}