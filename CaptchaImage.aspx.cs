using System;
using System.Drawing;
using System.Drawing.Imaging;

public partial class CaptchaImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = GenerateCode();

        Session["Captcha"] = code;

        Bitmap bmp = new Bitmap(100, 35);

        Graphics g = Graphics.FromImage(bmp);

        g.Clear(Color.White);

        Font font = new Font("Arial", 13, FontStyle.Bold);

        g.DrawString(code, font, Brushes.Black, 8, 5);

        Response.Clear();

        Response.ContentType = "image/png";

        bmp.Save(Response.OutputStream, ImageFormat.Png);

        bmp.Dispose();

        g.Dispose();

        Response.End();
    }
    private string GenerateCode()
    {
        Random r = new Random();

        return r.Next(100000, 999999).ToString();
    }
}