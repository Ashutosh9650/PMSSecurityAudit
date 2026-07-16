using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class FrmDistrictMaster : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                FillDistrictGrid();
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

    }
    protected void FillDistrictGrid()
    {
        string Year = Session["FinYear"].ToString();
        DataTable Dt = objMain.GetDistrictGridData(Year);
        if (Dt.Rows.Count > 0)
        {
            GV_District.DataSource = Dt;
            GV_District.DataBind();
        }
        else
        {
            GV_District.DataSource = null;
            GV_District.DataBind();
        }
    }
    protected void Btn_Save_OnClick(object sender, EventArgs e)
    {
        Boolean ret = false;
        for (int i = 0; i < GV_District.Rows.Count; i++)
        {
            Label LblDistrictCode = (Label)GV_District.Rows[i].FindControl("LblDistrictCode");
            TextBox TxtYear = (TextBox)GV_District.Rows[i].FindControl("TxtYear");
            string Year = TxtYear.Text;
            string DistrictCode = LblDistrictCode.Text;
            if (Year != "" && DistrictCode != "")
            {
              
            }
            if (ret == true)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update sucessfully')</script>", false);
            }
        }
    }
}