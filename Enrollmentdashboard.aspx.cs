using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Drawing;
public partial class Enrollmentdashboard : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["username"]) != "")
        {
            //Bindrpt();
            //div1.Visible = true;
            //lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ////MpexdrDistrict.Show();
            //if (Convert.ToString(Session["username"]) == "EGE3078" || Convert.ToString(Session["username"]) == "SuperAdmin")
            //{
            //    //div1.Visible = true;
            //    //lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //    //Bindrpt();
            //}
            //else
            //{
            //  // div1.Visible = false;
            //}
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "29" || Session["user_level"].ToString() == "136" || Session["user_level"].ToString() == "137" || Session["user_level"].ToString() == "145")
            {
                LoadData();
            }
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }


    public void LoadData()
    {
        string Con = " 1=1  ";
        string Con1 = " 1=1  ";
        Int32 Flag = 1;
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "29" || Session["user_level"].ToString() == "136")
        {
            Con += " and v.DistrictCode='" + Session["NewDistrictCode"].ToString() + "'";
            Con1 += " and DistrictCode='" + Session["NewDistrictCode"].ToString() + "'";
        }
        if (Session["user_level"].ToString() == "145" )
        {
            Con += " and v.DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ")";
            Con1 += " and DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ")";
        }
        if (Session["user_level_Role"].ToString() == "4")
        {
            Con +=" and v.BlockCOde='"+ Session["NewBlockCode"].ToString()+"'";
            Con1 += " and BlockCOde='" + Session["NewBlockCode"].ToString() + "'";
        }

        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "136" || Session["user_level"].ToString() == "145")
        {
            Flag = 1;
            DivBlock.Visible = true;
            DivBlockCount.Visible = true;
            dvValdation.Visible = true;
            dvValdation1.Visible = true;
            divManual1.Visible = true;
            divManual.Visible = true;
            divG1.Visible = true;
            divG.Visible = true;
        }
        if (Session["user_level"].ToString() == "19" || Session["user_level"].ToString() == "137")
        {
            Flag = 2;
            DivBlock.Visible = false;
            DivBlockCount.Visible = false;

            dvValdation.Visible = false;
            dvValdation1.Visible = false;
            divManual1.Visible = true;
            divManual.Visible = true;
            divG1.Visible = false;
            divG.Visible = false;
            dvcv1.Visible = false;
            dvcv.Visible = false;
        }
        if (Session["user_level"].ToString() == "29")
        {
            divManual1.Visible = false;
            divManual.Visible = false;
            dvValdation.Visible = true;
            dvValdation1.Visible = true;
            divG1.Visible = true;
            divG.Visible = true;
            DivBlock.Visible = false;
            DivBlockCount.Visible = false;
            dvcv1.Visible = false;
            dvcv.Visible = false;

        }
        SqlParameter[] parm = new SqlParameter[]
                        {
                   new SqlParameter("@Con",  Con),
                       new SqlParameter("@Dist",  Con1),
                        new SqlParameter("@Flag",  Flag),
                              
          
                             };
        DataSet dt = GetDataSetNew(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDeskBoardCOuntNew2020]", parm);
        if (dt.Tables[0].Rows.Count > 0)
        {
            Label1.Text = dt.Tables[0].Rows[0]["Icount"].ToString();
        }
        if (dt.Tables[1].Rows.Count > 0)
        {
            LblA.Text = dt.Tables[1].Rows[0]["Icount"].ToString();
        }
        if (dt.Tables[2].Rows.Count > 0)
        {
            Label2.Text = dt.Tables[2].Rows[0]["Icount"].ToString();
        }
         if (dt.Tables[3].Rows.Count > 0)
        {
            lblDuplicate.Text = dt.Tables[3].Rows[0]["Icount"].ToString();
        }

        if (dt.Tables[4].Rows.Count > 0)
        {
            Label3.Text = dt.Tables[4].Rows[0]["Icount"].ToString();
        }

    }

    public static DataSet GetDataSetNew(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            PrepareCommandNew(cmd, conn, cmdType, cmdText, cmdParameters);
            da.SelectCommand = new SqlCommand();
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
        }
    }

    public static void PrepareCommandNew(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        if (conn.State != ConnectionState.Open)
            conn.Open();
        cmd.Connection = conn;

        cmd.CommandType = cmdType;
        cmd.CommandText = cmdText;
        cmd.CommandTimeout = 0;
        if (cmdParameters != null)
        {
            foreach (SqlParameter param in cmdParameters)
            {
                cmd.Parameters.Add(param);
            }
        }
    }

}