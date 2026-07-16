using System;
using System.Data;
using System.IO;
public partial class frmdownloadtraining : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Convert.ToString(Session["username"]) != "")
            {

                if (Request.QueryString["ID"] != null)
                {
                    string IDImage = Request.QueryString["ID"];
                    string filename = "";
                    string sFileDir = Comman.GetImagePath("TabletImagePath") + "/";
                    filename = sFileDir + "TabletImage\\" + IDImage;
                    filename = sFileDir + IDImage;


                    Response.ContentType = ".jpg";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                    Response.TransmitFile(filename);
                    Response.End();

                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }
        }




    }
    public void LoadData()
    {
        string filename = "";
        string IDImage = "";
        string strQry2 = "";
        strQry2 += " select PicName from tblRandomSessionPhototemp where SchoolCode='930F1813F0F44E799D2AE08E9'  ";

        DataTable dtUseryyy = objMain.LoadData(strQry2);
        var kk = 0;
        for (kk = 0; kk < dtUseryyy.Rows.Count; kk++)
        {
            IDImage = dtUseryyy.Rows[kk]["PicName"].ToString();
            string sFileDir = Comman.GetImagePath("TabletImagePath") + "/";
            filename = sFileDir + "TabletImage\\" + IDImage;
            filename = sFileDir + IDImage;



            if (!File.Exists(filename))
            {

            }
            else
            {
                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();
            }
        }
    }
}