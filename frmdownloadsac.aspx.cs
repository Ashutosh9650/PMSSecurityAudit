using System;
using System.Data;
using System.IO;
public partial class frmdownloadsac : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["username"]) != "")
            {
                string IDImage = Request.QueryString["ID"];
                IDImage = Path.GetFileName(IDImage ?? "");

                if (string.IsNullOrEmpty(IDImage))
                {
                    Response.Write("No image specified.");
                    Response.End();
                    return;
                }

                string sFileDir = Server.MapPath(Comman.GetImagePath("TabletImagePath") + "/");
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
            else
            {
                Response.Redirect("Login.aspx", false);

            }
        }




    }
   
}