using System;
using System.IO;

public partial class frmdownloadGkp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                string IDImage = Request.QueryString["ID"];
                IDImage = Path.GetFileName(IDImage ?? "");

                if (string.IsNullOrEmpty(IDImage))
                {
                    Response.Write("No image specified.");
                    Response.End();
                    return;
                }

                string sFileDir = Server.MapPath(Comman.GetImagePath("GKPPath") + "/");
                string filename = Path.Combine(sFileDir, IDImage);

                if (File.Exists(filename))
                {
                    Response.Clear();
                    Response.ContentType = "image/jpeg";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage);
                    Response.TransmitFile(filename);
                    Response.End();
                }
                else
                {
                    Response.Write("Image not found.");
                    Response.End();
                }
             
            }


        }
    }
}