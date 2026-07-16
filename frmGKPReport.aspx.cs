using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class frmGKPReport : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    string conditions="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

               LoadYear();
               //Bindgrid();

            
             

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

           

        }

    }
    protected void btn_display_Click(object sender, System.EventArgs e)
    {
        gridbindNew();
        ///ModalAlert.Show();
    }
    protected void btnAddGkp_Click(object sender, System.EventArgs e)
    {
        ddMainlLevel.SelectedIndex = 0;
        TextBox4.Text = "";
        DropDownList2.SelectedIndex = 0;
        ModalAlert.Show();
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        DataTable dt =  ViewState["D2dUser"]  as DataTable;
        ExporttoExcel(DGV_CLT, dt, "GKPMaster");
    }

    private void ExporttoExcel(GridView Gv, DataTable table, string FileName)
    {
        try
        {

      
        if (table != null)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            string Fullfilename = "" + FileName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + " ");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            //am getting my grid's column headers
            int columnscount = Gv.HeaderRow.Cells.Count;


            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(Gv.HeaderRow.Cells[j].Text);
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //if (Page != null)
        //{
        //    Page.VerifyRenderingInServerForm(this);
        //}

        /* Verifies that the control is rendered */
    }
    public void gridbindNew()
    {
        string condition = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "  where  Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }



        SqlParameter[] cmdParameters = new SqlParameter[]
		{
			new SqlParameter("@Con", conditions),
 

            	
		};
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptGKPMater]", cmdParameters);

        ViewState["D2dUser"] = dt;





        DGV_CLT.DataSource = dt;
        DGV_CLT.DataBind();
     

        
    }
    protected void btnSave_Click(object sender, System.EventArgs e)
    {
       


      
       
    }
  
 
    protected void GvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlstt = (DropDownList)e.Row.FindControl("ddlstt");
            DropDownList ddlLevel = (DropDownList)e.Row.FindControl("ddlLevel");
            Label txtMainTypeID = (Label)e.Row.FindControl("txtMainTypeID");
            Label lblLevelID = (Label)e.Row.FindControl("lblLevelID");
            ddlstt.SelectedValue = txtMainTypeID.Text;
            ddlLevel.SelectedValue = lblLevelID.Text;
        }
    }
    protected void Dgv_LeftGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label subject = (Label)e.Row.FindControl("SubjectID");
            if (subject.Text == "1")
            {
                subject.Text = "Hindi";
            }
            if (subject.Text == "2")
            {
                subject.Text = "English";
            }
            if (subject.Text == "3")
            {
                subject.Text = "Maths";
            }
          
        }

    }

   


}