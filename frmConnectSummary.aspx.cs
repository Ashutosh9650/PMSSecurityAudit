using ClosedXML.Excel;
using Ionic.Zip;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class frmConnectSummary : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    SqlInjection sqlInjection = new SqlInjection();
    DataTableMaskingHelper dataTableMaskingHelper = new DataTableMaskingHelper();



    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {




                if (!IsPostBack)
                {
                    LoadYear();
                    LoadUserLeavel();
                    ViewState["1"] = "ss";
                    ViewState["Annual"] = "";
                    ViewState["D2dUser"] = "";



                    LinkButton5.Visible = true;
                    LinkButton6.Visible = true;
                    LinkButton12.Visible = true;
                    LinkButton7.Visible = true;

                    CreateDataTableAge();
                    if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
                    {
                        Button3.Text = "Contact Summary";
                        A4.Visible = true;
                        A1.Visible = true;
                        A2.Visible = false;


                        A5.Visible = false;
                        A6.Visible = false;
                        A7.Visible = false;
                        A8.Visible = false;
                        A9.Visible = false;
                        A10.Visible = false;
                        A11.Visible = false;
                        A15.Visible = false;
                    }
                    else
                    {
                        Button3.Text = "Contact Report District Summary";
                        A1.Visible = true;
                        A2.Visible = true;

                        A4.Visible = true;
                        A5.Visible = true;
                        A6.Visible = true;
                        A7.Visible = true;
                        A8.Visible = true;
                        A9.Visible = true;
                        A10.Visible = true;
                        A11.Visible = true;
                        A15.Visible = true;
                    }


                }


                // btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
    }


    //public void LoadYear()
    //{
    //    DataTable dtYear = objComman.Generate_Financial_Year();
    //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

    //    ddlYear.SelectedIndex = 1;
    //    //}


    //}
    public void CreateDataTableAge()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("Type", System.Type.GetType("System.String"));

        dt.Columns.Add("ID", System.Type.GetType("System.Int32"));
        DataRow dr;

        dr = dt.NewRow();
        dr[0] = "4";
        dr[1] = "4";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "5";
        dr[1] = "5";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "6";
        dr[1] = "6";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "7";
        dr[1] = "7";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "8";
        dr[1] = "8";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "9";
        dr[1] = "9";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "10";
        dr[1] = "10";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "11";
        dr[1] = "11";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "12";
        dr[1] = "12";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "13";
        dr[1] = "13";
        dt.Rows.Add(dr);
        dr = dt.NewRow();
        dr[0] = "14";
        dr[1] = "14";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "15";
        dr[1] = "15";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "16";
        dr[1] = "16";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "17";
        dr[1] = "17";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr[0] = "18";
        dr[1] = "18";
        dt.Rows.Add(dr);

        chkAge.DataSource = dt;
        chkAge.DataTextField = "Type";
        chkAge.DataValueField = "ID";
        chkAge.DataBind();



    }
    protected void gvReportNew_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 4;

            //  HeaderCell.ColumnSpan = 5;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 2;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Target vs Contact Status of OOSG";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 8;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Target vs Contact Status of OOSB";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 8;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Remaning OOSG";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 4;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Remaning OOSB";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 25;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            gvReportNew.Controls[0].Controls.AddAt(0, HeaderGridRow);








            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 2;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "TOTAL";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "TOTAL";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 2;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Total";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "5 to 6 Yrs";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "7 to 9";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "10 to 14";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Total";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 1;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvReportNew.Controls[0].Controls.AddAt(1, HeaderGridRow1);


        }
    }
    protected void ContactSummary_Click(object sender, EventArgs e)
    {



        ViewState["Button"] = "9000";

        GV_DynamicGrid.Visible = false;
        gvReport.Visible = true;
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;

        GV_DynamicGrid.Visible = false;

        LoadContactSummary("1");


    }

    protected void ClusterWise_Click(object sender, EventArgs e)
    {



        ViewState["Button"] = "9001";

        GV_DynamicGrid.Visible = false;
        GV_DynamicGrid.Visible = false;
        gvReport.Visible = false;
        gvReportNew.Visible = true;
        gvReportClusterOutrich.Visible = false;
        gvReportCluster.Visible = false;

        LoadContactClusterSummary("2");




    }



    protected void Outreach_Click(object sender, EventArgs e)
    {



        ViewState["Button"] = "9005";
        GV_DynamicGrid.Visible = false;

        gvReport.Visible = false;
        gvReportNew.Visible = false;
        gvReportCluster.Visible = true;
        gvReportClusterOutrich.Visible = false;
        gvReportNew.Visible = false;

        LoadContactClusterOutReach("1");



    }

    public void LoadContactClusterSummary(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";


        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        //if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //{
        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        //}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //{
        //    Int32 ih = 0;
        //    Int32 iK = 0;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        iK = Convert.ToInt32(Year1[1]);
        //        ih = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        iK = Convert.ToInt32(Year1[0]);
        //        ih = Convert.ToInt32(Year1[0]); ;
        //    }

        //    int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        //    {
        //        ih = 2019;
        //        mMonth = 12;
        //    }

        //    string fDate = "";
        //    string tate = "";
        //    DateTime trmDate;
        //    DateTime frmDate;
        //    fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
        //    frmDate = Convert.ToDateTime(fDate);


        //    tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
        //    trmDate = Convert.ToDateTime(tate);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
        //        trmDate = Convert.ToDateTime(tate);
        //    }

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        //    {

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
        //        frmDate = Convert.ToDateTime(fDate);

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
        //        frmDate = Convert.ToDateTime(fDate);
        //    }

        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        //}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        //{
        //    string fDate = "";
        //    string tate = "";
        //    DateTime trmDate;
        //    DateTime frmDate;
        //    Int32 ih = 0;
        //    Int32 iK = 0;


        //    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        iK = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        iK = Convert.ToInt32(Year1[0]);
        //    }

        //    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
        //    {
        //        ih = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        ih = Convert.ToInt32(Year1[0]);
        //    }
        //    int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        //    {
        //        ih = 2019;
        //        mMonth = 12;
        //    }

        //    fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
        //    frmDate = Convert.ToDateTime(fDate);

        //    tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
        //    trmDate = Convert.ToDateTime(tate);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
        //        trmDate = Convert.ToDateTime(tate);
        //    }

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        //    {

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
        //        frmDate = Convert.ToDateTime(fDate);

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
        //        frmDate = Convert.ToDateTime(fDate);
        //    }
        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        //}


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        if (ddlPhan.Length > 0)
        {
            ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        }
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummary(conditions1 + Con, conditions1, "2", ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["DtTrargetC"] = dtMain;
            GenerateExcelNewCluster("ContactClusterwisesummary");
            //gvReportNew.DataSource = dtMain;
            //gvReportNew.DataBind();

            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReportNew.DataSource = null;
            gvReportNew.DataBind();
        }




    }
    protected void gvReportCluster_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 4;

            //  HeaderCell.ColumnSpan = 5;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 1;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Status of Contact";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 18;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Enrolled";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "FollowUp";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Ineligible";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 30;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            gvReportCluster.Controls[0].Controls.AddAt(0, HeaderGridRow);





            GridView HeaderGrid11 = (GridView)sender;
            GridViewRow HeaderGridRow11 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow11.CssClass = "gridnewheadercss";
            TableCell HeaderCell11;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell11.ColumnSpan = 4;

            //  HeaderCell11.ColumnSpan = 5;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 1;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);




            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            gvReportCluster.Controls[0].Controls.AddAt(1, HeaderGridRow11);






            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 2, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 1;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled Info by Parents";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvReportCluster.Controls[0].Controls.AddAt(2, HeaderGridRow1);

        }

    }
    public DataTable rptContactSummary(string WhereQuery, string conditions1, string Flag, string Fyear, Int32 yYear)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
            new SqlParameter("@Con", conditions1),
                new SqlParameter("@Flag", Flag),
            new SqlParameter("@Fyear", Fyear),
            new SqlParameter("@yYear", yYear),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummary]", cmdParameters);
    }
    protected void OutreachCluster_Click(object sender, EventArgs e)
    {



        ViewState["Button"] = "9007";

        GV_DynamicGrid.Visible = false;

        gvReport.Visible = false;
        gvReportNew.Visible = false;
        gvReportCluster.Visible = false;
        gvReportClusterOutrich.Visible = true;


        LoadContactClusterOutReachNew("2");


    }
    public void LoadContactClusterOutReachNew(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";


        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        //if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //{
        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        //}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //{
        //    Int32 ih = 0;
        //    Int32 iK = 0;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        iK = Convert.ToInt32(Year1[1]);
        //        ih = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        iK = Convert.ToInt32(Year1[0]);
        //        ih = Convert.ToInt32(Year1[0]); ;
        //    }

        //    int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        //    {
        //        ih = 2019;
        //        mMonth = 12;
        //    }

        //    string fDate = "";
        //    string tate = "";
        //    DateTime trmDate;
        //    DateTime frmDate;
        //    fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
        //    frmDate = Convert.ToDateTime(fDate);


        //    tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
        //    trmDate = Convert.ToDateTime(tate);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
        //        trmDate = Convert.ToDateTime(tate);
        //    }

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        //    {

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
        //        frmDate = Convert.ToDateTime(fDate);

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
        //        frmDate = Convert.ToDateTime(fDate);
        //    }

        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        //}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        //{
        //    string fDate = "";
        //    string tate = "";
        //    DateTime trmDate;
        //    DateTime frmDate;
        //    Int32 ih = 0;
        //    Int32 iK = 0;


        //    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        iK = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        iK = Convert.ToInt32(Year1[0]);
        //    }

        //    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
        //    {
        //        ih = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        ih = Convert.ToInt32(Year1[0]);
        //    }
        //    int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        //    {
        //        ih = 2019;
        //        mMonth = 12;
        //    }

        //    fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
        //    frmDate = Convert.ToDateTime(fDate);

        //    tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
        //    trmDate = Convert.ToDateTime(tate);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
        //        trmDate = Convert.ToDateTime(tate);
        //    }

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        //    {

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
        //        frmDate = Convert.ToDateTime(fDate);

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
        //        frmDate = Convert.ToDateTime(fDate);
        //    }
        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        //}


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummaryOutReach(conditions1 + Con, conditions1, "2", ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["ClusteTrargetCNew"] = dtMain;
            //gvReportClusterOutrich.DataSource = dtMain;
            //gvReportClusterOutrich.DataBind();
            GenerateExcelOutReachNew("ClusterwiseOutreach");
            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReportClusterOutrich.DataSource = null;
            gvReportClusterOutrich.DataBind();
        }




    }

    public void LoadContactClusterOutReach(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";


        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');



        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        //}
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummaryOutReach(conditions1 + Con, conditions1, "1", ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["ClusteTrargetC"] = dtMain;
            //gvReportCluster.DataSource = dtMain;
            //gvReportCluster.DataBind();
            GenerateExcelOutReach("BlockwiseOutreach");
            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReportCluster.DataSource = null;
            gvReportCluster.DataBind();
        }




    }

    public DataTable rptContactSummaryOutReach(string WhereQuery, string conditions1, string Flag, string Fyear, Int32 Yyear)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
            new SqlParameter("@Flag", Flag),
              new SqlParameter("@Fyear",Fyear),
                new SqlParameter("@Yyear",Yyear),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummaryOutReach]", cmdParameters);
    }
    public void LoadContactSummary(string Flag)
    {
        string conditions1 = "";
        DataTable dtMain;
        Int32 Icount = 5;
        Int32 TotalMonth = 0;
        Int32 year = 0;
        string Con = "";


        Session["ABC"] = "B";

        string Year = ddlYear.SelectedItem.Text;
        string[] Year1 = Year.Split('-');

        //if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        //{
        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd") + "') ";
        //}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        //{
        //    Int32 ih = 0;
        //    Int32 iK = 0;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        iK = Convert.ToInt32(Year1[1]);
        //        ih = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        iK = Convert.ToInt32(Year1[0]);
        //        ih = Convert.ToInt32(Year1[0]); ;
        //    }

        //    int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        //    {
        //        ih = 2019;
        //        mMonth = 12;
        //    }

        //    string fDate = "";
        //    string tate = "";
        //    DateTime trmDate;
        //    DateTime frmDate;
        //    fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
        //    frmDate = Convert.ToDateTime(fDate);


        //    tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "25";
        //    trmDate = Convert.ToDateTime(tate);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
        //        trmDate = Convert.ToDateTime(tate);
        //    }

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        //    {

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
        //        frmDate = Convert.ToDateTime(fDate);

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
        //        frmDate = Convert.ToDateTime(fDate);
        //    }

        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";

        ////}
        //if (Convert.ToInt32(ddlType.SelectedValue) == 3)
        //{
        //    string fDate = "";
        //    string tate = "";
        //    DateTime trmDate;
        //    DateTime frmDate;
        //    Int32 ih = 0;
        //    Int32 iK = 0;


        //    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        iK = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        iK = Convert.ToInt32(Year1[0]);
        //    }

        //    if (Convert.ToInt32(ddlToMonth.SelectedValue) == 1 || Convert.ToInt32(ddlToMonth.SelectedValue) == 2 || Convert.ToInt32(ddlToMonth.SelectedValue) == 3)
        //    {
        //        ih = Convert.ToInt32(Year1[1]);
        //    }
        //    else
        //    {
        //        ih = Convert.ToInt32(Year1[0]);
        //    }
        //    int mMonth = Convert.ToInt32(ddlMonth.SelectedValue) - 1;
        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 1)
        //    {
        //        ih = 2019;
        //        mMonth = 12;
        //    }

        //    fDate = (ih) + "-" + "" + Convert.ToString(mMonth) + "" + "-" + "26";
        //    frmDate = Convert.ToDateTime(fDate);

        //    tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlToMonth.SelectedValue)) + "" + "-" + "25";
        //    trmDate = Convert.ToDateTime(tate);

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        //    {
        //        tate = (iK) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "31";
        //        trmDate = Convert.ToDateTime(tate);
        //    }

        //    if (Convert.ToInt32(ddlMonth.SelectedValue) == 4)
        //    {

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "26";
        //        frmDate = Convert.ToDateTime(fDate);

        //        fDate = (ih) + "-" + "" + Convert.ToString(Convert.ToInt32(ddlMonth.SelectedValue)) + "" + "-" + "01";
        //        frmDate = Convert.ToDateTime(fDate);
        //    }
        //    Con += " and tblDTDMobileActivity.ActivityDate between ('" + Convert.ToDateTime(frmDate).ToString("yyyy-MM-dd") + "') and ('" + Convert.ToDateTime(trmDate).ToString("yyyy-MM-dd") + "') ";


        //}


        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        //}
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        //if (ddlVillage.Length > 0)
        //{
        //    ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        //}
        if (ddlYear.SelectedIndex > 0)
        {
            conditions1 = conditions1 + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions1 = conditions1 + " and    mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions1 = conditions1 + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions1 = conditions1 + " and  mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        dtMain = rptContactSummary(conditions1 + Con, conditions1, Flag, ddlYear.SelectedItem.Text, Convert.ToInt32(ddlYear.SelectedValue));
        ViewState["dt"] = dtMain;
        if (dtMain.Rows.Count > 0)
        {
            Session["DtTrarget"] = dtMain;
            //gvReport.DataSource = dtMain;
            //gvReport.DataBind();
            GenerateExcelNewfff("ContactBlockwisesummary");
            // GenerateExcelNew("dddddsf");
        }
        else
        {
            gvReport.DataSource = null;
            gvReport.DataBind();
        }




    }
    protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (Session["ABC"].ToString() == "B")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {


                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow.CssClass = "gridnewheadercss";
                TableCell HeaderCell;

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell.ColumnSpan = 4;

                //  HeaderCell.ColumnSpan = 5;

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 1;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);




                HeaderCell = new TableCell();
                HeaderCell.Text = "Target vs Contact Status of OOSG";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 8;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Target vs Contact Status of OOSB";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 8;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);



                HeaderCell = new TableCell();
                HeaderCell.Text = "Remaning OOSG";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 4;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);


                HeaderCell = new TableCell();
                HeaderCell.Text = "Remaning OOSB";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.ColumnSpan = 25;

                HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
                HeaderGridRow.Cells.Add(HeaderCell);
                gvReport.Controls[0].Controls.AddAt(0, HeaderGridRow);








                GridView HeaderGrid1 = (GridView)sender;
                GridViewRow HeaderGridRow1 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
                HeaderGridRow1.CssClass = "gridnewheadercss";
                TableCell HeaderCell1;

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


                HeaderCell1.ColumnSpan = 1;

                //  HeaderCell1.ColumnSpan = 5;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "TOTAL";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "TOTAL";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 2;



                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);



                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);



                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "5 to 6 Yrs";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);


                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "7 to 9";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;

                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);




                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "10 to 14";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);

                HeaderCell1 = new TableCell();
                HeaderCell1.Text = "Total";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell1.ColumnSpan = 1;
                HeaderCell1.CssClass = "gridnewheadercss";
                HeaderGridRow1.Cells.Add(HeaderCell1);

                gvReport.Controls[0].Controls.AddAt(1, HeaderGridRow1);

            }
        }
    }
    protected void gvReportClusterOutrich_RowCreated(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.Header)
        {


            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.CssClass = "gridnewheadercss";
            TableCell HeaderCell;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell.ColumnSpan = 4;

            //  HeaderCell.ColumnSpan = 5;

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 2;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);




            HeaderCell = new TableCell();
            HeaderCell.Text = "Status of Contact";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 18;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "Enrolled";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "FollowUp";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 24;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "Ineligible";
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell.ColumnSpan = 31;

            HeaderCell.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow.Cells.Add(HeaderCell);
            gvReportClusterOutrich.Controls[0].Controls.AddAt(0, HeaderGridRow);





            GridView HeaderGrid11 = (GridView)sender;
            GridViewRow HeaderGridRow11 = new GridViewRow(1, 1, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow11.CssClass = "gridnewheadercss";
            TableCell HeaderCell11;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell11.ColumnSpan = 4;

            //  HeaderCell11.ColumnSpan = 5;

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 2;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);




            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 9;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);



            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 12;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);

            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Female";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            HeaderCell11 = new TableCell();
            HeaderCell11.Text = "Male";
            HeaderCell11.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell11.ColumnSpan = 15;

            HeaderCell11.BackColor = ColorTranslator.FromHtml("#DC2717");
            HeaderGridRow11.Cells.Add(HeaderCell11);


            gvReportClusterOutrich.Controls[0].Controls.AddAt(1, HeaderGridRow11);






            GridView HeaderGrid1 = (GridView)sender;
            GridViewRow HeaderGridRow1 = new GridViewRow(1, 2, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow1.CssClass = "gridnewheadercss";
            TableCell HeaderCell1;

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;


            HeaderCell1.ColumnSpan = 2;

            //  HeaderCell1.ColumnSpan = 5;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled With SR";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Follow Up";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ineligible";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "NRSTC";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "KGBV";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;



            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Aanganwadi";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Mainstream";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Enrolled Info by Parents";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Ready to be Enrolled";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-School Distance";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;
            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Not Ready-Other Reason";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);




            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Migration";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Overage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);



            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Underage";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Typing Error";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);


            HeaderCell1 = new TableCell();
            HeaderCell1.Text = "Death";
            HeaderCell1.HorizontalAlign = HorizontalAlign.Center;

            HeaderCell1.ColumnSpan = 3;

            HeaderCell1.CssClass = "gridnewheadercss";
            HeaderGridRow1.Cells.Add(HeaderCell1);

            gvReportClusterOutrich.Controls[0].Controls.AddAt(2, HeaderGridRow1);

        }

    }

    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public void LoadYear()
    {
        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }


    //public void LoadYear()
    //{
    //    DateTime GivenDate = DateTime.Now;
    //    int GivenYear = GivenDate.Year;
    //    int m = GivenDate.Month;

    //    DataTable dt = null;
    //    //ddlYear.Items.Add("--Select--","0");
    //    int y = GivenDate.Year;


    //    DateTime GivenDate1 = DateTime.Now;
    //    int GivenYear1 = GivenDate1.Year;
    //    DataTable dtYear = CreateDataTable();
    //    DataRow dr;
    //    if (ddlYear.SelectedIndex < 0)
    //    {

    //        string mYear1 = GivenYear1.ToString();
    //        for (int j = 0; j < 1; j++)
    //        {
    //            if (m > 3)
    //            {
    //                dr = dtYear.NewRow();
    //                dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
    //                dr["ID"] = y;
    //                dtYear.Rows.Add(dr);
    //                dr = dtYear.NewRow();
    //                dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
    //                dr["ID"] = y - 1;
    //                dtYear.Rows.Add(dr);
    //                //get last  two digits (eg: 10 from 2010);

    //            }
    //            else
    //            {

    //                Int32 m7 = y + 1;
    //                dr = dtYear.NewRow();
    //                dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
    //                //y = y - 1;
    //                dr["ID"] = y;
    //                dtYear.Rows.Add(dr);
    //                dr = dtYear.NewRow();
    //                dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
    //                //y = y - 1;
    //                dr["ID"] = y - 1;

    //                dtYear.Rows.Add(dr);


    //            }

    //        }

    //    }

    //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

    //    objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");



    //    ddlYear.SelectedIndex = 1;



    //}
    public DataTable Generate_Financial_Year()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Month < 4 ? DateTime.Today.Year : DateTime.Today.Year + 1;
        for (int i = stYr; i > 2016; i--)
        {
            dr = dt.NewRow();
            dr[0] = (i - 1).ToString();
            dr[1] = (i - 1).ToString() + "-" + (i).ToString();
            dt.Rows.Add(dr);
        }
        dt.AcceptChanges();
        return dt;
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            AlllStateCode();
            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "UserName='" + Session["username"].ToString() + "' ";
            //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            AlllStateCode();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

            ChkState.Enabled = true;
            chkDistrict.Enabled = true;
        }
        else
        {
            AlllStateCode();
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            ////objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            //string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State  where   " + conditions + "  order by StateName   ";
            //DataTable dtState = objMain.LoadData(strQry1);
            //ChkState.DataSource = dtState;
            //ChkState.DataTextField = "StateName";
            //ChkState.DataValueField = "StateCode";
            //ChkState.DataBind();
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }
            // ChkState.SelectedIndex = 1;
            ChkState.Enabled = false;
            chkDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }
            conditions = "";
            //  conditions = "StateCode in(" + ddlState + ") and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            conditions = "UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "'  order by DistrictName   ";


            // string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";


            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();

            if (Session["user_level_Role"].ToString() == "2")
            {
                foreach (ListItem item in chkDistrict.Items)
                {

                    item.Selected = true;

                }
                ddlDistrict_SelectedIndexChanged(ddlState, null);
            }
            //foreach (ListItem item in chkDistrict.Items)
            //{

            //    item.Selected = true;
            //    break;
            //}
            //ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {

            string ddlState = "";

            foreach (ListItem item in ChkState.Items)
            {
                if (item.Selected)
                {

                    ddlState += "'" + item.Value + "'" + ",";


                }
            }

            if (ddlState.Length > 0)
            {
                ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
            }
            conditions = "";
            conditions = "StateCode in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            //  objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
            string strQry1 = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            DataTable dtDistrict = objMain.LoadData(strQry1);
            // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

            chkDistrict.DataSource = dtDistrict;
            chkDistrict.DataTextField = "DistrictName";
            chkDistrict.DataValueField = "DistrictCode";
            chkDistrict.DataBind();
            string strQry;
            strQry = "Select * from mst2District where   DistrictCode in(" + Session["DistrictCode"].ToString() + ")";
            DataTable dtcountCheck = objMain.LoadData(strQry);
            if (dtcountCheck.Rows.Count > 0)
            {
                if (dtcountCheck.Rows.Count == 1)
                {
                    ddlYear.Enabled = false;
                }
                else
                {
                    ddlYear.Enabled = true;
                }
            }
            else
            {
                ddlYear.Enabled = true;
            }


            //ddlDistrict.SelectedIndex = 1;
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
            ddlDistrict_SelectedIndexChanged(chkDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    public void FillCBState()
    {
        conditions = "";
        // objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");


        //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
        string strQry1 = "  SELECT StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM mst1State   order by StateName   ";
        DataTable dtState = objMain.LoadData(strQry1);
        ChkState.DataSource = dtState;
        ChkState.DataTextField = "StateName";
        ChkState.DataValueField = "StateCode";
        ChkState.DataBind();

    }


    public void FillCBDist()
    {
        string ddlState = "";
        DataTable dtDistrict = null;
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode in(" + ddlState + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = " mst2District.StateCode in(" + ddlState + ") and UserName='" + Session["username"].ToString() + "' ";
        }
        else
        {

            conditions = "StateCode  in(" + ddlState + ") and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";

        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            //if (ddlYear.SelectedValue.ToString() == "2016")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}

            //if (ddlYear.SelectedValue.ToString() == "2017")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where EGDistrictCode in(     SELECT distinct mst2District.EGDistrictCode  FROM MstusermultipleDist     where   " + conditions + " )  and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            //if (ddlYear.SelectedValue.ToString() == "2018")
            //{

            //    string strQry1 = "  SELECT distinct mst2District.DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist   inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode  where   " + conditions + " and Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName   ";
            //    dtDistrict = objMain.LoadData(strQry1);
            //}
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            dtDistrict = objMain.LoadData(strQry1);
        }
        else
        {
            string strQry = "  SELECT DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM mst2District where " + conditions + "  order by DistrictName   ";
            dtDistrict = objMain.LoadData(strQry);
        }

        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkDistrict.DataSource = dtDistrict;
        chkDistrict.DataTextField = "DistrictName";
        chkDistrict.DataValueField = "DistrictCode";
        chkDistrict.DataBind();

        if (Session["user_level_Role"].ToString() == "2")
        {
            foreach (ListItem item in chkDistrict.Items)
            {

                item.Selected = true;

            }
        }

    }
    protected void ContactReport_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            getreportContactDeatlisosg(1);
        }
        else
        {
            getreportContactDeatlis(1);
        }

    }
    protected void ContactRepordt_Click(object sender, EventArgs e)
    {
        getreportContactDeatlisosg2023(1);
    }
    protected void ContactReport_1(object sender, EventArgs e)
    {

        getreportContactDeatlisosg(1);

    }
    protected void ContactReport4_Click(object sender, EventArgs e)
    {

        getreportContactDeatlis(3);

    }
    protected void ContactReport15_Click(object sender, EventArgs e)
    {

        getreportContactDeatlis(2);

    }
    public void getreportContactDeatlisosg2023(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }




        if (ddlGender.SelectedIndex > 0)
        {
            conditions += " and [Gender]='" + ddlGender.SelectedItem.Text + "'";
        }
        if (ddlTpye.SelectedIndex > 0)
        {
            conditions += " and [Contact Status]='" + ddlTpye.SelectedItem.Text + "'";
        }

        string Age = "";
        foreach (ListItem item in chkAge.Items)
        {
            if (item.Selected)
            {

                Age += "" + item.Value + "" + ",";



            }
        }
        string AgeEnGrouopp = "";

        if (Age.Length > 0)
        {
            Age = Age.Substring(0, Age.LastIndexOf(","));

            conditions += " and [D2D Age-Current Year] in(" + Age + ")";
        }
        //else
        //{
        //    conditions = " and dbo.udfDateDiffinYrMonDay(tblEnrolment.dob,EnrolmentDate) in(5,6,7,8,9,10,11,12,13,14)";

        //}

        DataTable dataTable = null;
        string FileName = "";



        if (Flag == 1)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
              new SqlParameter("@Condition", conditions),
              new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptContactStatusReport2024]", cmdParameters);
            FileName = "Contact Status Report";

            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "House", "Child Name", "Father Name", "DOB", "Contact DOB", /*"D2D Age-Current Year",*/ "Child Name_SR", "Father Name_SR", "Mother Name_SR", "DOB_SR", /*"Respondent Relation with Child",*/ "Mobile No", "Date of Birth", "Child SSSMID", "Family SSSMID");
            else if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "House", "Child Name", "Contact Child Name", "Father Name", "Contact Father Name", "DOB", "Contact DOB", /*"D2D Age-Current Year",*/ /*"Respondent Relation with Child",*/ "Mobile No", "Date of Birth", "Child SSSMID", "Family SSSMID");
            else
                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "House", "Child Name", "Father Name", "DOB", /*"D2D Age-Current Year",*/ /*"Respondent Relation with Child",*/ "Mobile No", "Date of Birth", "Child SSSMID", "Family SSSMID");
        }

        if (Flag == 2)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                    new SqlParameter("@Condition", conditions),
                    new SqlParameter("@Con", ddlYear.SelectedItem.Text),
                    new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactMobileTargetD2dDetials15to18", cmdParameters);
            FileName = "ContactReport(15to18)";

            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mauhalla", "House", "Child Name", "Father Name", "D2D Age-Current Year", "DOB", "SamagraID", "Mobile No", "Alternate Mobile Number", "Alternate mobile OwneName");

        }
        if (Flag == 3)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                 new SqlParameter("@Condition", conditions),
                 new SqlParameter("@Con", ddlYear.SelectedItem.Text),
                 new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactMobileTargetD2dDetialsFourYear", cmdParameters);
            FileName = "ContactReport(4 Year)";

            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mauhalla", "House", "D2D Age-Current Year", "DOB", "Child Name", "Father Name", "SamagraID", "Mobile No", "Alternate Mobile Number", "Alternate mobile OwneName", "Relation with Child");
        }

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            ReportDownload("Contact Status Report", "Contact Summary Report");
            ExportToCSVFile(dataTable, FileName);
        }
    }

    public void getreportContactDeatlisosg(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        DataTable dataTable = null;
        string FileName = "";

        if (Flag == 1)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
               new SqlParameter("@Condition", conditions),
               new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2dOSG2024]", cmdParameters);
            FileName = "Contact Detail Report";

            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "House", "Child Name", "Father Name", /*"D2D Age-Current Year",*/
                    "Contact DOB", "Child Name_SR", "Father Name_SR", "Mother Name_SR", "DOB_SR", /*"Respondent Relation with Child",*/ 
                    "Mobile No", "Date of Birth", /*"Migration Place",*/ "Child SSSMID", "Family SSSMID", "Latitude", "Longitude");

            else if (Convert.ToInt32(ddlYear.SelectedValue) >= 2024)
                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "House", "Child Name", "Father Name", /*"D2D Age-Current Year",*/ 
                    "Contact DOB", "Child Name_SR", "Father Name_SR", "Mother Name_SR", "DOB_SR", /*"Respondent Relation with Child",*/ 
                    "Mobile No", "Date of Birth", /*"Migration Place",*/ "Child SSSMID", "Family SSSMID", "Latitude", "Longitude");
            else
                dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mohalla", "House", "Child Name", "Father Name", /*"D2D Age-Current Year",*/ 
                    "Child Name_SR", "Father Name_SR", "Mother Name_SR", "DOB_SR", /*"Respondent Relation with Child",*/
                    "Mobile No", "Date of Birth", /*"Migration Place",*/ "Child SSSMID", "Family SSSMID", "Latitude", "Longitude");
        }

        if (Flag == 2)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
               new SqlParameter("@Condition", conditions),
               new SqlParameter("@Con", ddlYear.SelectedItem.Text),
               new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactMobileTargetD2dDetials15to18", cmdParameters);
            FileName = "ContactReport(15to18)";

            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mauhalla", "House", "Child Name", "Father Name", "D2D Age-Current Year", "DOB", "SamagraID", "Mobile No", "Alternate Mobile Number", "Alternate mobile OwneName");

        }

        if (Flag == 3)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                 new SqlParameter("@Condition", conditions),
                 new SqlParameter("@Con", ddlYear.SelectedItem.Text),
                 new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactMobileTargetD2dDetialsFourYear", cmdParameters);
            FileName = "ContactReport(4 Year)";

            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mauhalla", "House", "D2D Age-Current Year", "DOB", "Child Name", "Father Name", "SamagraID", "Mobile No", "Alternate Mobile Number", "Alternate mobile OwneName", "Relation with Child");
        }


        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            ReportDownload("Contact Detail Report", "Contact Summary Report");
            ExportToCSVFile(dataTable, FileName);
        }
    }

    public void getreportContactDeatlis(Int32 Flag)
    {
        conditions = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlBlock = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}
        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        if (ddlYear.SelectedIndex > 0)
        {
            conditions = conditions + "    mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions = conditions + "  and  mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions = conditions + " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions = conditions + " and mst5Village.BlockCode in(" + ddlBlock + ") ";
        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        if (ddlTpye.SelectedIndex > 0)
        {
            conditions += " and [Contact Status]='" + ddlTpye.SelectedItem.Text + "'";
        }

        if (ddlGender.SelectedIndex > 0)
        {
            conditions += " and [Gender]='" + ddlGender.SelectedItem.Text + "'";
        }
        DataTable dataTable = null;
        string FileName = "";



        if (Flag == 1)
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                   new SqlParameter("@Condition", conditions),
                   new SqlParameter("@Con", ddlYear.SelectedItem.Text),
                   new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptContactMobileTargetD2dDetials]", cmdParameters);
            FileName = "ContactReport";
        }

        if (Flag == 2)
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                  new SqlParameter("@Condition", conditions),
                  new SqlParameter("@Con", ddlYear.SelectedItem.Text),
                  new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactMobileTargetD2dDetials15to18", cmdParameters);
            FileName = "ContactReport(15to18)";

            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mauhalla", "House", "Child Name", "Father Name", "D2D Age-Current Year", "DOB", "SamagraID", "Mobile No", "Alternate Mobile Number", "Alternate mobile OwneName");
        }
        if (Flag == 3)
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                  new SqlParameter("@Condition", conditions),
                  new SqlParameter("@Con", ddlYear.SelectedItem.Text),
                  new SqlParameter("@FYear", ddlYear.SelectedValue),
            };

            dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactMobileTargetD2dDetialsFourYear", cmdParameters);
            FileName = "ContactReport(4 Year)";

            dataTableMaskingHelper.DecryptAndMaskDataTable(dataTable, "Mauhalla", "House", "D2D Age-Current Year", "DOB", "Child Name", "Father Name", "SamagraID", "Mobile No", "Alternate Mobile Number", "Alternate mobile OwneName", "Relation with Child");
        }

        ViewState["dt"] = dataTable;
        if (dataTable.Rows.Count > 0)
        {
            ExportToCSVFile(dataTable, FileName);
        }
    }

    protected void LnkMobileDataReport_OnClick(object sender, EventArgs e)
    {

        ReportMobileActivityStatus(1);

    }
    protected void LnkMobileDataReport15_OnClick(object sender, EventArgs e)
    {

        ReportMobileActivityStatus(2);

    }

    public void ReportMobileActivityStatus(int Flag)
    {
        //string condition = string.Empty;

        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlState = "";

        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlState += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlState.Length > 0)
        {
            ddlState = ddlState.Substring(0, ddlState.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}

        if (ddlVillage.Length > 0)
        {
            ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        }
        conditions = "";
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += " mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlState.Length > 0)
        {
            conditions += " and mst5Village.StateCode in(" + ddlState + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }
        if (ddlBlock.Length > 0)
        {
            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ")  ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ")  ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }


        DataTable dt = null;

        if (Flag == 1)
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                    new SqlParameter("@condtion", conditions),
                    new SqlParameter("@Year", ddlYear.SelectedValue),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportMobileActivityStatus]", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(ddlYear.SelectedValue) == 2017 || Convert.ToInt32(ddlYear.SelectedValue) == 2018)
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "House", "ChildName", "Father Name", "CurrentAge");
                else if (Convert.ToInt32(ddlYear.SelectedValue) == 2019)
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Age");
                else if (Convert.ToInt32(ddlYear.SelectedValue) == 2020)
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Age", "Latitude", "Longitude", "Class");
                else if (Convert.ToInt32(ddlYear.SelectedValue) == 2021)
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Alternate Mobile Number", "Alternate mobile Owner Name", "Latitude", "Longitude", "Class");
                else if (Convert.ToInt32(ddlYear.SelectedValue) == 2022)
                    dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Alternate Mobile Number", "Alternate mobile Owner Name", "Age");

                ExportToCSVFile(dt, "EnrDailyStatus");
            }
        }
        if (Flag == 2)
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                 new SqlParameter("@condtion", conditions),
                 new SqlParameter("@Year", ddlYear.SelectedValue),
            };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "ReportMobileActivityStatus15to18", cmdParameters);
            if (dt.Rows.Count > 0)
            {
                dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB", "SamgraID/GovtID", "Mobile No", "Alternate Mobile Number", "Alternate mobile Owner Name", "Latitude", "Longitude", "Age");
                ExportToCSVFile(dt, "EnrDailyStatus(15to18)");
            }
        }
    }

    protected void ddlTpye_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Annual"] = "";
        ViewState["D2dUser"] = "";
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();
    }

    public void AlllStateCode()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", "" ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {

            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", Convert.ToString(Session["username"]) ),
                    new SqlParameter("@StateCode",  ""),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
               };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();
        }
        else
        {
            SqlParameter[] par1 = new SqlParameter[]
                  {
                      new SqlParameter("@user_level_Role",  Convert.ToString(Session["user_level_Role"])),
                      new SqlParameter("@UserName", Convert.ToString(Session["username"]) ),
                    new SqlParameter("@StateCode", Convert.ToString(Session["StateCode"]) ),
                       new SqlParameter("@Year",  ddlYear.SelectedValue),
                  };
            DataTable dtAllState = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
            ChkState.DataSource = dtAllState;
            ChkState.DataTextField = "StateName";
            ChkState.DataValueField = "StateCode";
            ChkState.DataBind();

            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

        }

    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            AlllStateCode();
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {

            }
            else
            {
                foreach (ListItem item in ChkState.Items)
                {

                    item.Selected = false;

                }
            }
            if (Session["user_level_Role"].ToString() == "2")
            {

                //conditions = "UserName='" + Session["username"].ToString() + "' ";
                //string strQry1 = "  SELECT distinct mst1State.StateCode, dbo.TitleCase(upper(StateName))  as StateName FROM MstusermultipleDist inner join mst2District on mst2District.DistrictCode=MstusermultipleDist.DistrictCode inner join mst1State on mst1State.StateCode=mst2District.StateCode where   " + conditions + "  order by StateName   ";
                //DataTable dtState = objMain.LoadData(strQry1);
                //ChkState.DataSource = dtState;
                //ChkState.DataTextField = "StateName";
                //ChkState.DataValueField = "StateCode";
                //ChkState.DataBind();
            }
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = true;

            }

            ddlState_SelectedIndexChanged(chkDistrict, null);
            if (Session["user_level_Role"].ToString() == "3" || Session["user_level_Role"].ToString() == "4")
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {

                        item.Selected = true;

                    }
                }
            }

            ddlDistrict_SelectedIndexChanged(chkDistrict, null);


        }
        else
        {
            foreach (ListItem item in ChkState.Items)
            {

                item.Selected = false;

            }
            chkDistrict.Items.Clear();
            chkBlock.Items.Clear();

        }

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            Button3.Text = "Contact Summary";
            LinkButton5.Text = "Contact Detail Report";
            A4.Visible = true;
            A1.Visible = true;
            A2.Visible = false;

            A5.Visible = false;
            A6.Visible = false;
            A7.Visible = false;
            A8.Visible = false;
            A9.Visible = false;
            A10.Visible = false;
            A11.Visible = false;
            A15.Visible = false;
        }
        else
        {
            Button3.Text = "Contact Report District Summary";

            LinkButton5.Text = "Contact Report";
            A1.Visible = true;
            A2.Visible = true;

            A4.Visible = true;
            A5.Visible = true;
            A6.Visible = true;
            A7.Visible = true;
            A8.Visible = true;
            A9.Visible = true;
            A10.Visible = true;
            A11.Visible = true;
            A15.Visible = true;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        chkDistrict.Items.Clear();
        chkBlock.Items.Clear();
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();

    }

    public void FillCBBock()
    {
        conditions = "";
        string ddlDistrict = "";

        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }

        if (Session["user_level_Role"].ToString() == "2")
        {
            if (ddlDistrict.Length > 0)
            {
            }
            else
            {
                if (chkDistrict.Items.Count > 0)
                {
                    foreach (ListItem item in chkDistrict.Items)
                    {
                        ddlDistrict += "'" + item.Value + "'" + ",";
                        item.Selected = true;
                        break;
                    }
                    if (ddlDistrict.Length > 0)
                    {
                        ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
                    }
                }
            }


        }
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode in(" + ddlDistrict + ") ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")   and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode in(" + ddlDistrict + ")  ";
        }
        //     objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        string strQry = "  SELECT BlockCode, dbo.TitleCase(upper(BlockName))  as BlockName FROM mst3Block where " + conditions + "  order by BlockName   ";
        DataTable dtDistrict = objMain.LoadData(strQry);
        // objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        chkBlock.DataSource = dtDistrict;
        chkBlock.DataTextField = "BlockName";
        chkBlock.DataValueField = "BlockCode";
        chkBlock.DataBind();

        if (Session["user_level_Role"].ToString() == "4")
        {
            if (chkBlock.Items.Count > 0)
            {
                foreach (ListItem item in chkBlock.Items)
                {

                    item.Selected = true;

                }
            }
            chkBlock.Enabled = false;

        }

    }


    protected void LnkAnnualPlan_OnClick(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            LoadContactSumarry(1);

        }
        else
        {
            ViewState["1"] = 101;

            LoadAnnualData(1);
            gvReportNew.Visible = false;
            gvReportClusterOutrich.Visible = false;
            gvReportCluster.Visible = false;
        }
    }
    protected void LnkAnnualPlanFC_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 102;

        LoadAnnualData(2);
        gvReportNew.Visible = false;
        gvReportClusterOutrich.Visible = false;
        gvReportCluster.Visible = false;


    }

    protected void LnkAnnual_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 1602;
        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2023)
        {
            LoadQualityAlert2023(2);
        }
        else
        {
            LoadQualityAlert(2);

        }
    }
    protected void LnkAnnua1_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 1602;

        LoadQualityProject(2);



    }

    public void LoadQualityAlert2023(int Flag)
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }





        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   where  mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mstCluster.BlockCode in(" + ddlBlock + ") ";


        }

        // conditions += " and Gender=" + ddlGender.SelectedValue + "";


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
            new SqlParameter("@Year",ddlYear.SelectedValue),

        };
        DataTable dt = null;
        DataTable dt11 = null;
        ///rptD2dOSSAlterreportFinalReport20232024   rptD2dOSSAlterreportFinalReport2023
        DataSet dt1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2dOSSAlterreportFinalReport2025]", cmdParameters);

        dt = dt1.Tables[0];
        ///  dt11 = dt1.Tables[1];
        if (dt.Rows.Count > 0)
        {
            ReportDownload("Contact Quality Alert", "Contact Summary Report");
            //GenerateExcelNewBlock2023Alter2024(dt);
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2025)
            {
                GenerateExcelNewBlock2023Alter2025(dt1.Tables[0], dt1);
            }
            else
            {
                GenerateExcelNewBlock2023Alter2024(dt1.Tables[0], dt1);
            }
        }
        //ViewState["Annual"] = dt1.Tables[1];
        //GV_DynamicGrid.Visible = true;
        //GV_DynamicGrid.DataSource = null;
        //GV_DynamicGrid.DataBind();




        //if (dt.Rows.Count > 0)
        //{
        //    GV_DynamicGrid.DataSource = dt;
        //    GV_DynamicGrid.DataBind();
        //}
        //else
        //{
        //    GV_DynamicGrid.DataSource = null;
        //    GV_DynamicGrid.DataBind();
        //}




    }

    private void GenerateExcelNewBlock2023Alter2025(DataTable dt, DataSet ds)
    {
        try
        {

            ds.Tables[1].Columns.Remove("Flag");
            ds.Tables[1].Columns.Remove("FlagNew");
            ds.Tables[1].Columns.Remove("stateCode");
            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\D2DContactQualityAlert2025.xlsx");
            var ws = wb.Worksheet(2);
            var ws1 = wb.Worksheet(3);
            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A2:AP" + ii;
            string str1 = "o";
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            ws1.Cell(3, 1).InsertData(ds.Tables[1].Rows);
            Int32 ii1 = Convert.ToInt32(ds.Tables[1].Rows.Count) + 2;
            string str11 = "A1:D" + ii1;

            ws1.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws1.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws1.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws1.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 12 };
                //% School Visit
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 13 };
                //% School Denial
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 19)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 16 };
                //% Children Enrolled -School
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 17 };
                //% Community Contacted
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 19 };
                //%Girls contacted with TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 51 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 27 };
            //    //%# Villages Where Different HH Contacted With Same Lat-Long
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }

            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 1)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 29 };
            //    //% Ineligible Contact
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }

            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 22 };
                //% Ineligible Contact
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 23 };
                //% % Divyaang
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 23 };
                //Average child Contact per day during Home Visit
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 25 };
                //# Staff not trained D2D Contact
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 27 };
                //% EIBP
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 28 };
                //% EIBP (before 1st Apr)
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 28 };
                //# % Parent/Child Denial (NRTE)
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 29 };
                //# % Child Denial
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 30 };
                //% RTE
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 31 };
                //% % Confirmed RTE with Proper Document
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 32 };
                ///% RTE with less/ no Document availability
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 34 };
                ///# Different HH with Same Mobile number
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 35 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 38 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 39 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            filepath = StartupPath + "\\QualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {
            throw;
        }


    }

    private void GenerateExcelNewBlock2023Alter2024(DataTable dt, DataSet ds)
    {
        try
        {

            ds.Tables[1].Columns.Remove("Flag");
            ds.Tables[1].Columns.Remove("FlagNew");
            ds.Tables[1].Columns.Remove("stateCode");
            // string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));

            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\D2DContactQualityAlert.xlsx");
            var ws = wb.Worksheet(2);
            var ws1 = wb.Worksheet(3);
            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A2:AM" + ii;
            string str1 = "o";
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            ws1.Cell(3, 1).InsertData(ds.Tables[1].Rows);
            Int32 ii1 = Convert.ToInt32(ds.Tables[1].Rows.Count) + 2;
            string str11 = "A1:D" + ii1;

            ws1.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws1.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws1.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws1.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 12 };
                //% School Visit
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 13 };
                //% School Denial
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 19)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 16 };
                //% Children Enrolled -School
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 17 };
                //% Community Contacted
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 19 };
                //%Girls contacted with TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 51 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 27 };
            //    //%# Villages Where Different HH Contacted With Same Lat-Long
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }

            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 1)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 29 };
            //    //% Ineligible Contact
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }

            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 22 };
                //% Ineligible Contact
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 23 };
                //% % Divyaang
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 23 };
                //Average child Contact per day during Home Visit
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 25 };
                //# Staff not trained D2D Contact
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 27 };
                //% EIBP
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 28 };
                //% EIBP (before 1st Apr)
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 28 };
                //# % Parent/Child Denial (NRTE)
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 29 };
                //# % Child Denial
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 30 };
                //% RTE
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 31 };
                //% % Confirmed RTE with Proper Document
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 32 };
                ///% RTE with less/ no Document availability
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 34 };
                ///# Different HH with Same Mobile number
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 35 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 38 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 39 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            filepath = StartupPath + "\\QualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {
            throw;
        }


    }

    private void GenerateExcelNewBlock2023Alter2024(DataTable dt)
    {
        try
        {

            //   dt11.Columns.Remove("Flag");

            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\D2DContactQualityAlert.xlsx");
            var ws = wb.Worksheet(2);
            // var ws1 = wb.Worksheet(3);
            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A2:AM" + ii;
            string str1 = "o";
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            //ws1.Cell(2, 1).InsertData(dt11.Rows);
            //Int32 ii1 = Convert.ToInt32(dt11.Rows.Count) + 2;
            //string str11 = "A2:D" + ii1;

            //ws1.Range(str11).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            //ws1.Range(str11).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            //ws1.Range(str11).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            //ws1.Range(str11).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 12 };
                //% School Visit
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 50 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 13 };
                //% School Denial
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 11 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 19)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 16 };
                //% Children Enrolled -School
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 17 };
                //% Community Contacted
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 19 };
                //%Girls contacted with TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 50)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 51 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 90)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 80)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 27 };
            //    //%# Villages Where Different HH Contacted With Same Lat-Long
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }

            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 1)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 29 };
            //    //% Ineligible Contact
            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }

            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 22 };
                //% Ineligible Contact
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 23 };
                //% % Divyaang
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 23 };
                //Average child Contact per day during Home Visit
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 25 };
                //# Staff not trained D2D Contact
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) >= 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 27 };
                //% EIBP
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 30 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 40)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 28 };
                //% EIBP (before 1st Apr)
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 >= 10 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 <= 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.LightGreen;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 28 };
                //# % Parent/Child Denial (NRTE)
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 29 };
                //# % Child Denial
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 30)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 30 };
                //% RTE
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Amber;
                    }
                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 31 };
                //% % Confirmed RTE with Proper Document
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }
            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 32 };
                ///% RTE with less/ no Document availability
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 34 };
                ///# Different HH with Same Mobile number
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }

                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 10)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 35 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 20)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }

            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 38 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            for (int x = 2; x < dt.Rows.Count + 2; x++)
            {
                int[] arcols = { 39 };
                //% Villages- Contact without TB Support
                for (int y = 0; y < arcols.Length; y++)
                {
                    if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
                    {
                    }
                    else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 1)
                    {
                        ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
                    }

                }
            }


            filepath = StartupPath + "\\QualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {
            throw;
        }


    }
    public void LoadQualityAlert(int Flag)
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }





        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   where  mstCluster.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mstCluster.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mstCluster.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mstCluster.BlockCode in(" + ddlBlock + ") ";


        }

        conditions += " and Gender=" + ddlGender.SelectedValue + "";


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),


        };
        DataTable dt = null;


        DataSet dt1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAlterContactReport2022New]", cmdParameters);

        dt = dt1.Tables[0];
        if (dt.Rows.Count > 0)
        {
            ReportDownload("Contact Quality Alert", "Contact Summary Report");
            GenerateExcelNewBlock2023Alter(dt);
        }
        //ViewState["Annual"] = dt1.Tables[1];
        //GV_DynamicGrid.Visible = true;
        //GV_DynamicGrid.DataSource = null;
        //GV_DynamicGrid.DataBind();




        //if (dt.Rows.Count > 0)
        //{
        //    GV_DynamicGrid.DataSource = dt;
        //    GV_DynamicGrid.DataBind();
        //}
        //else
        //{
        //    GV_DynamicGrid.DataSource = null;
        //    GV_DynamicGrid.DataBind();
        //}




    }

    public void ReportDownload(string Rname, string ModuleName)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
         {
        new SqlParameter("@fname", Rname),
            new SqlParameter("@Username", Convert.ToString(Session["username"])),
            new SqlParameter("@ModuleName", ModuleName),


       };
        int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertDownloadReport2023]", cmdParameters);
    }



    public void LoadQualityProject(int Flag)
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        string ddlStatecode = "";
        string Statanme = "";
        string DistrinctName = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";

                Statanme += "" + item.Text + "" + ",";
            }
        }
        if (Statanme.Length > 0)
        {
            Statanme = Statanme.Substring(0, Statanme.LastIndexOf(","));
        }
        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";

                DistrinctName += "" + item.Text + "" + ",";
            }
        }

        if (DistrinctName.Length > 0)
        {
            DistrinctName = DistrinctName.Substring(0, DistrinctName.LastIndexOf(","));
        }
        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }





        string condition = string.Empty;
        string conditi = string.Empty;
        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   where  mst2District.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
            conditi += "   where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";
        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and mst2District.StateCode in(" + ddlStatecode + ") ";
            conditi += " and mst5Village.StateCode in(" + ddlStatecode + ") ";
        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst2District.DistrictCode in(" + ddlDistrict + ") ";
            conditi += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";
        }





        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
              new SqlParameter("@Con1",conditi),

        };
        DataTable dt = null;
        DataTable dtS = null;


        DataSet dt1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptDarpanProjectReport]", cmdParameters);

        dt = dt1.Tables[0];
        dtS = dt1.Tables[1];
        if (dt.Rows.Count > 0)
        {
            GenerateProjectDatban(dt, dtS, Statanme, DistrinctName);
        }
        //ViewState["Annual"] = dt1.Tables[1];
        //GV_DynamicGrid.Visible = true;
        //GV_DynamicGrid.DataSource = null;
        //GV_DynamicGrid.DataBind();




        //if (dt.Rows.Count > 0)
        //{
        //    GV_DynamicGrid.DataSource = dt;
        //    GV_DynamicGrid.DataBind();
        //}
        //else
        //{
        //    GV_DynamicGrid.DataSource = null;
        //    GV_DynamicGrid.DataBind();
        //}




    }
    private void GenerateProjectDatban(DataTable dt, DataTable dtS, string Statanme, string DistrinctName)
    {
        try
        {




            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\ProjectDarpan.xlsx");
            var ws = wb.Worksheet(1);
            var ws1 = wb.Worksheet(2);
            ws.Cell(5, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A2:U" + ii;
            ws.Cell(2, 2).Value = Statanme;
            ws.Cell(2, 4).Value = DistrinctName;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            dtS.Columns.Remove("PlanType");
            ws1.Cell(3, 1).InsertData(dtS.Rows);
            Int32 ii1 = Convert.ToInt32(dtS.Rows.Count) + 2;
            string str1 = "A2:o" + ii1;

            ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

            filepath = StartupPath + "\\ProjectDarpan" + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {
            throw;
        }


    }

    private void GenerateExcelNewBlock2023Alter(DataTable dt)
    {
        try
        {




            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\ContactQualityNew.xlsx");
            var ws = wb.Worksheet(1);

            ws.Cell(2, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 2;
            string str = "A2:AH" + ii;
            string str1 = "o";
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            //for (int x = 2; x < dt.Rows.Count+2; x++)
            //{
            //    int[] arcols = {15 };

            //        for (int y = 0; y < arcols.Length; y++)
            //        {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value)*100 < 50)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.OrangePeel;
            //        }
            //    }
            //}

            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 19 };

            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 60 && Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) < 90 )
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) <60)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Orange;
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) > 90)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Red;
            //        }
            //    }
            //}

            //for (int x = 2; x < dt.Rows.Count + 2; x++)
            //{
            //    int[] arcols = { 21 };

            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 > 70)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 70)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Orange;
            //        }
            //    }
            //}
            //for (int x = 13; x < dt.Rows.Count; x++)
            //{
            //    int[] arcols = { 21 };

            //    for (int y = 0; y < arcols.Length; y++)
            //    {
            //        if (Convert.ToString(ws.Cell(x, arcols[y]).Value) == "")
            //        {
            //        }
            //        else if (Convert.ToDecimal(ws.Cell(x, arcols[y]).Value) * 100 < 50)
            //        {
            //            ws.Cell(x, arcols[y]).Style.Fill.BackgroundColor = XLColor.Yellow;
            //        }
            //    }
            //}
            filepath = StartupPath + "\\QualityAlert " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

        }
        catch
        {
            throw;
        }


    }
    public void LoadAnnualData(int Flag)
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }





        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "     Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and BlockCode in(" + ddlBlock + ") ";


        }




        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
                new SqlParameter("@Flag",Flag),
                    new SqlParameter("@Fyear",ddlYear.SelectedItem.Text),
                        new SqlParameter("@Yyear",ddlYear.SelectedValue),

        };
        DataTable dt = null;


        DataSet dt1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummaryFolloupNew2023]", cmdParameters);

        dt = dt1.Tables[0];

        ViewState["Annual"] = dt1.Tables[1];
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();




        if (dt.Rows.Count > 0)
        {
            GV_DynamicGrid.DataSource = dt;
            GV_DynamicGrid.DataBind();
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }




    }
    private void ExporttoExcel(GridView Gv, DataTable table, string FileName)
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


    protected void btnImport_Click(object sender, EventArgs e)
    {

        if (ViewState["1"].ToString() == "101")
        {
            if (Convert.ToInt32(ddlYear.SelectedValue) < 2022)
            {
                DataTable dt = (DataTable)ViewState["Annual"];
                GenerateExcelNew("BlockWise");
            }
            else
            {
                DataTable dt = (DataTable)ViewState["Annual"];
                GenerateExcelNew2023("BlockWise");
            }

        }
        if (ViewState["1"].ToString() == "102")
        {

            if (Convert.ToInt32(ddlYear.SelectedValue) < 2022)
            {
                DataTable dt = (DataTable)ViewState["Annual"];
                GenerateExcelNewBlock("ClusterWise");
            }
            else
            {
                DataTable dt = (DataTable)ViewState["Annual"];
                GenerateExcelNewBlock2023("ClusterWise");
            }




        }
        if (ViewState["Button"].ToString() == "9000")
        {
            DataTable dt = Session["DtTrargetC"] as DataTable;
            GenerateExcelNewfff("ContactBlockwisesummary");
        }
        if (ViewState["Button"].ToString() == "9005")
        {
            DataTable dt = Session["ClusteTrargetC"] as DataTable;
            GenerateExcelOutReach("BlockwiseOutreach");
        }
        if (ViewState["Button"].ToString() == "9007")
        {
            DataTable dt = Session["ClusteTrargetCNew"] as DataTable;
            GenerateExcelOutReachNew("ClusterwiseOutreach");
        }

        if (ViewState["Button"].ToString() == "9001")
        {
            DataTable dt = ViewState["dt"] as DataTable;
            GenerateExcelNewCluster("ContactClusterwisesummary");
        }
    }
    private void GenerateExcelOutReachNew(string FIleName)
    {
        try
        {



            DataTable dt = Session["ClusteTrargetCNew"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'> Status of Contact</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'>Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'> FollowUp	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='36' style='" + HeaderStyle + "  width:2%;'>  Ineligible</th>");
                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='9'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='9' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");


                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");

                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");


                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>Block</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  so-rotate: 90; width:2%;'>Cluster Name</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }
                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 1 || c == 0)
                        {
                            if (c == 1)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                            }

                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }
    private void GenerateExcelOutReach(string FIleName)
    {
        try
        {



            DataTable dt = Session["ClusteTrargetC"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                //HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='18' style='" + HeaderStyle + "  width:2%;'> Status of Contact</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'>Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='24' style='" + HeaderStyle + "  width:2%;'> FollowUp	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='30' style='" + HeaderStyle + "  width:2%;'>  Ineligible</th>");
                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='9'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='9' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12'  style='" + HeaderStyle + "  width:2%;'>Female</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='15' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");


                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'> Male	</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Female</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='12' style='" + HeaderStyle + "  width:2%;'>  Male</th>");

                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled With SR</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Follow Up</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ineligible</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> NRSTC</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>KGBV</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Aanganwadi</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Mainstream</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Enrolled Info by Parents</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Ready to be Enrolled</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-School Distance</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Not Ready-Other Reason</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> Migration</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Overage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Underage</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Typing Error</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'>Death</th>");


                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'>Block</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  mso-rotate: 90;  width:2%;'> 	10 to 14</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }
                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 0)
                        {

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");


                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }
    private void GenerateExcelNewfff(string FIleName)
    {
        try
        {



            DataTable dt = Session["DtTrarget"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1'  rowspan='1' style='" + HeaderStyle + "  width:2%;'>Block</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSB</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSB</th>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='3' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");






                HttpContext.Current.Response.Write("</tr>");



                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BlockName	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");


                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1



                #endregion


                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 0)
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                //HttpContext.Current.Response.Write("<tr>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                //HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }


    private void GenerateExcelNewCluster(string FIleName)
    {
        try
        {



            DataTable dt = Session["DtTrargetC"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");
                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='25' ' style='text-align:left;border:.2pt solid windowtext;'></td>");

                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";
                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2'  rowspan='2' style='" + HeaderStyle + "  width:2%;'>Block</th>");


                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='8' style='" + HeaderStyle + "  width:2%;'> Target vs Contact Status of OOSB</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSG</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'> Remaning OOSB</th>");
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='2' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");


                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");

                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                //HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                HttpContext.Current.Response.Write("</tr>");



                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");

                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BlockName	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	ClusterName	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");


                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Target	</th>");
                HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Achievement	</th>");



                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");

                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 5 to 6 Yrs</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 7 to 9</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> 	10 to 14</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'> TOTAL</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");


                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");
                //HttpContext.Current.Response.Write("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 		</th>");

                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";
                String ToallRowStyle = "border:.2pt solid windowtext; font-weight:100; font-size:11pt;rowspan=2;border:.2pt solid windowtext;";

                String RowStyeYellow = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";
                String RowStyeRed = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FF0000;";
                String RowStyeGreen = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#008000;";





                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                    #region Row1



                    #endregion


                    HttpContext.Current.Response.Write("</tr>");


                }

                HttpContext.Current.Response.Write("<tr>");
                for (int J = 0; J < 1; J++)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c == 1 || c == 0)
                        {
                            if (c == 1)
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                            }
                            else
                            {
                                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                            }
                        }
                        else
                        {
                            string Col = dt.Columns[c].ColumnName;
                            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                        }
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                HttpContext.Current.Response.Write("<td style='" + HeaderStyle + "'></td>");
                HttpContext.Current.Response.Write("</tr>");


                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }
    private void GenerateExcelNew(string FIleName)
    {
        try
        {


            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            //{
            //    FIleName = "StaffTrainingTraget";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            //{
            //    FIleName = "VillageLevelPlan";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            //{
            FIleName = "ContactReportDistrictSummary";
            //}
            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");





                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='27' style='text-align:Center;border:.2pt solid windowtext;'>D2D Contact Summary Report</td>");
                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");


                HttpContext.Current.Response.Write("<th class='header' colspan='3'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Followup Girls</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header'  colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Followup Boys</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";



                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1
                HttpContext.Current.Response.Write("<tr>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (c == 0 || c == 1 || c == 2)
                    {
                        if (c == 2)
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                        }
                        else
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                        }
                    }
                    else
                    {
                        string Col = "[" + dt.Columns[c].ColumnName + "]";
                        int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                #endregion


                HttpContext.Current.Response.Write("</tr>");




                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }


    private void GenerateExcelNew2023(string FIleName)
    {
        try
        {


            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            //{
            //    FIleName = "StaffTrainingTraget";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            //{
            //    FIleName = "VillageLevelPlan";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            //{

            DataTable dt = ViewState["Annual"] as DataTable;



            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\ContactDistrictSummary.xlsx");
            var ws = wb.Worksheet(1);
            dt.Columns.Remove("Rowno");
            ws.Cell(5, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
            string str = "A4:BF" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            filepath = StartupPath + "\\ContactReportDistrictSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            //if (dt.Rows.Count > 0)
            //{

            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.ClearContent();
            //    HttpContext.Current.Response.ClearHeaders();
            //    HttpContext.Current.Response.Buffer = true;
            //    HttpContext.Current.Response.ContentType = "application/ms-excel";
            //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //    string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            //    HttpContext.Current.Response.Charset = "utf-8";
            //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //    HttpContext.Current.Response.Write("<table  >");





            //    HttpContext.Current.Response.Write("<tr>");
            //    HttpContext.Current.Response.Write("<td colspan='50' style='text-align:Center;border:.2pt solid windowtext;'>D2D Contact Summary Report</td>");
            //    HttpContext.Current.Response.Write("</tr>");
            //    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

            //    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='3'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'>Female</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'>Male</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>Overall</th>");
            //    HttpContext.Current.Response.Write("</tr>");

            //    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");


            //    HttpContext.Current.Response.Write("<th class='header' colspan='3'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

            //    HttpContext.Current.Response.Write("<th class='header' colspan='11' style='" + HeaderStyle + "  width:2%;'>OOSG-Follow up</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>OOSG-Ineligible</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header'  colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='11' style='" + HeaderStyle + "  width:2%;'>OOSB-Follow up</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>OOSG-Ineligible</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

            //    HttpContext.Current.Response.Write("</tr>");

            //    String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";



            //    int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

            //    for (int j = 0; j < columnscount; j++)
            //    {
            //        HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "   mso-rotate: 90;width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
            //    }

            //    HttpContext.Current.Response.Write("</tr>");


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {




            //        HttpContext.Current.Response.Write("<tr>");
            //        //HttpContext.Current.Response.Write("<td >Direct</td>");
            //        for (int c = 0; c < dt.Columns.Count; c++)
            //        {


            //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


            //        }
            //    }
            //    #region Row1
            //    HttpContext.Current.Response.Write("<tr>");
            //    for (int c = 0; c < dt.Columns.Count; c++)
            //    {
            //        if (c == 0 || c == 1 || c == 2)
            //        {
            //            if (c == 2)
            //            {
            //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
            //            }
            //            else
            //            {
            //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
            //            }
            //        }
            //        else
            //        {
            //            string Col = "[" + dt.Columns[c].ColumnName + "]";
            //            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

            //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
            //        }
            //    }
            //    HttpContext.Current.Response.Write("</tr>");

            //    #endregion


            //    HttpContext.Current.Response.Write("</tr>");




            //    HttpContext.Current.Response.Write("</table>");
            //    HttpContext.Current.Response.Flush();
            //    HttpContext.Current.Response.End();
            //}
        }
        catch
        {

            throw;
        }


    }
    private void GenerateExcelNewBlock2023(string FIleName)
    {
        try
        {

            DataTable dt = ViewState["Annual"] as DataTable;



            string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
            string filepath = "";
            XLWorkbook wb = new XLWorkbook();
            wb = new XLWorkbook(StartupPath + "\\ContactBlockSummary.xlsx");
            var ws = wb.Worksheet(1);
            dt.Columns.Remove("Rowno");
            ws.Cell(5, 1).InsertData(dt.Rows);
            Int32 ii = Convert.ToInt32(dt.Rows.Count) + 4;
            string str = "A4:BG" + ii;
            ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

            ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
            ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);
            filepath = StartupPath + "\\ContactReportBlockSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
            wb.SaveAs(filepath);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
            Response.WriteFile(filepath);

            Response.End();
            if (File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            //{
            //    FIleName = "StaffTrainingTraget";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            //{
            //    FIleName = "VillageLevelPlan";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            //{
            //FIleName = "ContactReportBlockSummary";
            ////}
            //DataTable dt = ViewState["Annual"] as DataTable;
            //if (dt.Rows.Count > 0)
            //{

            //    HttpContext.Current.Response.Clear();
            //    HttpContext.Current.Response.ClearContent();
            //    HttpContext.Current.Response.ClearHeaders();
            //    HttpContext.Current.Response.Buffer = true;
            //    HttpContext.Current.Response.ContentType = "application/ms-excel";
            //    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            //    string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


            //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

            //    HttpContext.Current.Response.Charset = "utf-8";
            //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //    HttpContext.Current.Response.Write("<table  >");





            //    HttpContext.Current.Response.Write("<tr>");
            //    HttpContext.Current.Response.Write("<td colspan='52' style='text-align:Center;border:.2pt solid windowtext;'>D2D Contact Summary Report</td>");
            //    HttpContext.Current.Response.Write("</tr>");
            //    String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

            //    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='5'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'>Female</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='20' style='" + HeaderStyle + "  width:2%;'>Male</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='7' style='" + HeaderStyle + "  width:2%;'>Overall</th>");
            //    HttpContext.Current.Response.Write("</tr>");
            //    HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");


            //    HttpContext.Current.Response.Write("<th class='header' colspan='5'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

            //    HttpContext.Current.Response.Write("<th class='header' colspan='11' style='" + HeaderStyle + "  width:2%;'>OOSG-Follow up</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>OOSG-Ineligible</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header'  colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='11' style='" + HeaderStyle + "  width:2%;'>OOSB-Follow up</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='4' style='" + HeaderStyle + "  width:2%;'>OOSG-Ineligible</th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
            //    HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

            //    HttpContext.Current.Response.Write("</tr>");

            //    String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";



            //    int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

            //    for (int j = 0; j < columnscount; j++)
            //    {
            //        HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + " mso-rotate: 90; width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
            //    }

            //    HttpContext.Current.Response.Write("</tr>");


            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {




            //        HttpContext.Current.Response.Write("<tr>");
            //        //HttpContext.Current.Response.Write("<td >Direct</td>");
            //        for (int c = 0; c < dt.Columns.Count; c++)
            //        {


            //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


            //        }
            //    }
            //    #region Row1
            //    HttpContext.Current.Response.Write("<tr>");
            //    for (int c = 0; c < dt.Columns.Count; c++)
            //    {
            //        if (c == 0 || c == 1 || c == 2 || c == 3 || c == 4)
            //        {
            //            if (c == 4)
            //            {
            //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
            //            }
            //            else
            //            {
            //                HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
            //            }
            //        }
            //        else
            //        {
            //            string Col = "[" + dt.Columns[c].ColumnName + "]";
            //            int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

            //            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
            //        }
            //    }
            //    HttpContext.Current.Response.Write("</tr>");

            //    #endregion


            //    HttpContext.Current.Response.Write("</tr>");




            //    HttpContext.Current.Response.Write("</table>");
            //    HttpContext.Current.Response.Flush();
            //    HttpContext.Current.Response.End();
            //}
        }
        catch
        {

            throw;
        }


    }

    private void GenerateExcelNewBlock(string FIleName)
    {
        try
        {


            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 1)
            //{
            //    FIleName = "StaffTrainingTraget";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 2)
            //{
            //    FIleName = "VillageLevelPlan";
            //}
            //if (Convert.ToInt32(ddlTpye.SelectedValue) == 3)
            //{
            FIleName = "ContactReportBlockSummary";
            //}
            DataTable dt = ViewState["Annual"] as DataTable;
            if (dt.Rows.Count > 0)
            {

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                string Fullfilename = "" + FIleName + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";


                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Fullfilename + "");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<table  >");





                HttpContext.Current.Response.Write("<tr>");
                HttpContext.Current.Response.Write("<td colspan='29' style='text-align:Center;border:.2pt solid windowtext;'>D2D Contact Summary Report</td>");
                HttpContext.Current.Response.Write("</tr>");
                String HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";


                HttpContext.Current.Response.Write("<tr style='font-width:bold;'>");


                HttpContext.Current.Response.Write("<th class='header' colspan='5'  rowspan='1' style='" + HeaderStyle + "  width:2%;'> </th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Followup</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header'  colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='5' style='" + HeaderStyle + "  width:2%;'>Followup</th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");
                HttpContext.Current.Response.Write("<th class='header' colspan='1' style='" + HeaderStyle + "  width:2%;'></th>");

                HttpContext.Current.Response.Write("</tr>");

                String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";



                int columnscount = GV_DynamicGrid.HeaderRow.Cells.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<th class='header' style='" + HeaderStyle + "  width:2%;'> " + GV_DynamicGrid.HeaderRow.Cells[j].Text + "</th>");
                }

                HttpContext.Current.Response.Write("</tr>");


                for (int i = 0; i < dt.Rows.Count; i++)
                {




                    HttpContext.Current.Response.Write("<tr>");
                    //HttpContext.Current.Response.Write("<td >Direct</td>");
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {


                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + dt.Rows[i][c] + "</td>");


                    }
                }
                #region Row1
                HttpContext.Current.Response.Write("<tr>");
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (c == 0 || c == 1 || c == 2 || c == 3 || c == 4)
                    {
                        if (c == 4)
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "TOTAL" + "</td>");
                        }
                        else
                        {
                            HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + "" + "</td>");
                        }
                    }
                    else
                    {
                        string Col = "[" + dt.Columns[c].ColumnName + "]";
                        int sum = Convert.ToInt32(dt.Compute("SUM(" + Col + ")", string.Empty));

                        HttpContext.Current.Response.Write("<td style='" + RowStyle + "'>" + sum + "</td>");
                    }
                }
                HttpContext.Current.Response.Write("</tr>");

                #endregion


                HttpContext.Current.Response.Write("</tr>");




                HttpContext.Current.Response.Write("</table>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
        catch
        {

            throw;
        }


    }

    protected void GV_DynamicGrid1_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DynamicGrid.PageIndex = e.NewPageIndex;
        if (Session["Annual"] != null)
        {

            DataTable Dt = Session["Annual"] as DataTable;
            GV_DynamicGrid.DataSource = Dt;
            GV_DynamicGrid.DataBind();
        }
    }






    private void ExportToCSVFile(DataTable dtTable, string filePath)
    {
        if (dtTable != null)
        {
            StringBuilder sbldr = new StringBuilder();
            if (dtTable.Columns.Count != 0)
            {
                foreach (DataColumn col in dtTable.Columns)
                {
                    sbldr.Append(col.ColumnName + ',');
                }
                sbldr.Append("\r\n");
                foreach (DataRow row in dtTable.Rows)
                {
                    foreach (DataColumn column in dtTable.Columns)
                    {

                        sbldr.Append(Convert.ToString(row[column]).Replace(",", "  ").Replace("\r", "").Replace("\n", "") + ',');
                    }
                    sbldr.Append("\r\n");

                }
            }
            string sFileDir = Server.MapPath(Comman.GetImagePath("DataBackupPath"));
            string Fullfilename = "" + filePath + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".csv";
            string path = sFileDir + Fullfilename;
            File.WriteAllText(path, sbldr.ToString());

            FileStream fs = null;//, fs2=null;
            try
            {
                string path1 = Fullfilename;
                string foldername = Server.MapPath(Comman.GetImagePath("DataBackupPath") + path1 + "");
                string datafolder = path1.Substring(0, path1.Length - 4);
                //  string[] file = Directory.GetFiles(foldername);

                string fullPath = Request.MapPath(Comman.GetImagePath("DataBackupPath") + datafolder + "" + ".zip");
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(foldername, "");
                    //    zip.AddFiles(file, foldername);
                    zip.Save(Server.MapPath(Comman.GetImagePath("DataBackupPath") + datafolder + "" + ".zip"));
                }



                HttpResponse Response = HttpContext.Current.Response; Response.Clear(); Response.ClearHeaders(); Response.Charset = "UTF-8";
                fs = File.Open(fullPath, FileMode.Open);
                byte[] bytBytes = new byte[(fs.Length)];
                fs.Read(bytBytes, 0, (int)fs.Length);
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + datafolder + "" + ".zip");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(bytBytes);

                if (File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                if (File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                Response.Flush();
                Response.End();
            }

            catch (System.Exception ex)
            {
                Response.Clear();

                //string mmsg = ex.Message;
                //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
            }
            finally
            {
                fs.Dispose();
                Response.Clear();

            }

            //str.Write(sbldr.ToString());
            //Response.ContentType = "Application/x-msexcel";
            //Response.AddHeader("content-disposition", "attachment;filename=test.csv");
            //Response.Write(sbldr.ToString());
            //Response.End();
        }
    }


    public void LoadContactSumarry(int Flag)
    {

        string ddlBlock = "";
        string ddlDistrict = "";

        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }





        string condition = string.Empty;

        if (ddlYear.SelectedIndex > 0)
        {
            conditions += "   where  v.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        }
        if (ddlStatecode.Length > 0)
        {
            conditions += " and v.StateCode in(" + ddlStatecode + ") ";

        }
        if (ddlDistrict.Length > 0)
        {
            conditions += " and v.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and v.BlockCode in(" + ddlBlock + ") ";


        }
        //if (ddlYear.SelectedIndex > 0)
        //{
        //    conditions += "   where  mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

        //}
        //if (ddlStatecode.Length > 0)
        //{
        //    conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

        //}
        //if (ddlDistrict.Length > 0)
        //{
        //    conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        //}

        //if (ddlBlock.Length > 0)
        //{

        //    conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";


        //}



        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con",conditions),
               new SqlParameter("@Year",ddlYear.SelectedValue),

        };
        DataTable dt = null;

        // rptOSCSummary2023 
        DataSet dt1 = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptOSCSummary2025]", cmdParameters);

        Session["OSCSummary"] = dt1;
        GV_DynamicGrid.Visible = true;
        GV_DynamicGrid.DataSource = null;
        GV_DynamicGrid.DataBind();

        if (dt1.Tables[0].Rows.Count > 0)
        {
            ReportDownload("Contact Summary", "Contact Summary Report");
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2026)
            {
                MultipuExeclProcess2026();
            }
            else if (Convert.ToInt32(ddlYear.SelectedValue) == 2025)
            {
                MultipuExeclProcess2025();
            }
            else
            {
                MultipuExeclProcess();
            }
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }
    }

    protected void ContactSummaryNew_Click(object sender, EventArgs e)
    {
        LoadContactSumarry(1);
    }
    public void MultipuExeclProcess2026()
    {
        DataSet dtMain1 = Session["OSCSummary"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ContactSummary2026.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);

        DataTable dt = dtMain1.Tables[0];

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:BI" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];


        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
        string str1 = "A2:AY" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];


        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str2 = "A2:AW" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt3 = dtMain1.Tables[3];


        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii3 = Convert.ToInt32(dt3.Rows.Count) + 1;
        string str3 = "A2:AU" + ii3;
        ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\ContactSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }
    public void MultipuExeclProcess2025()
    {
        DataSet dtMain1 = Session["OSCSummary"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ContactSummary2025.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);

        DataTable dt = dtMain1.Tables[0];

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:BC" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];


        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
        string str1 = "A2:AS" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];


        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str2 = "A2:AR" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt3 = dtMain1.Tables[3];


        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii3 = Convert.ToInt32(dt3.Rows.Count) + 1;
        string str3 = "A2:AP" + ii3;
        ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\ContactSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }
    public void MultipuExeclProcess()
    {
        DataSet dtMain1 = Session["OSCSummary"] as DataSet;
        string StartupPath = Server.MapPath(Comman.GetImagePath("ExportPath"));
        string filepath = "";
        XLWorkbook wb = new XLWorkbook();
        wb = new XLWorkbook(StartupPath + "\\ContactSummary.xlsx");
        var ws = wb.Worksheet(1);
        var ws1 = wb.Worksheet(2);
        var ws2 = wb.Worksheet(3);
        var ws3 = wb.Worksheet(4);

        DataTable dt = dtMain1.Tables[0];

        //DataTable dt1 = dtMain1.Tables[1];

        //dt1.Columns.Remove("RowNo");
        ws.Cell(2, 1).InsertData(dt.Rows);
        Int32 ii = Convert.ToInt32(dt.Rows.Count) + 1;
        string str = "A2:BD" + ii;
        ws.Range(str).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws.Range(str).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws.Range(str).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt1 = dtMain1.Tables[1];


        ws1.Cell(2, 1).InsertData(dt1.Rows);
        Int32 ii1 = Convert.ToInt32(dt1.Rows.Count) + 1;
        string str1 = "A2:AT" + ii1;
        ws1.Range(str1).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws1.Range(str1).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws1.Range(str1).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);

        DataTable dt2 = dtMain1.Tables[2];


        ws2.Cell(2, 1).InsertData(dt2.Rows);
        Int32 ii2 = Convert.ToInt32(dt2.Rows.Count) + 1;
        string str2 = "A2:AR" + ii2;
        ws2.Range(str2).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws2.Range(str2).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws2.Range(str2).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);


        DataTable dt3 = dtMain1.Tables[3];


        ws3.Cell(2, 1).InsertData(dt3.Rows);
        Int32 ii3 = Convert.ToInt32(dt3.Rows.Count) + 1;
        string str3 = "A2:AP" + ii3;
        ws3.Range(str3).Style.Border.SetTopBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin);

        ws3.Range(str3).Style.Border.SetLeftBorder(XLBorderStyleValues.Thin);
        ws3.Range(str3).Style.Border.SetRightBorder(XLBorderStyleValues.Thin);






        filepath = StartupPath + "\\ContactSummary " + "_" + System.DateTime.Now.ToString("hhssmmfff") + ".xlsx";
        wb.SaveAs(filepath);
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filepath));
        Response.WriteFile(filepath);

        Response.End();
        if (File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }

    }

    protected void LnkEnrolment_OnClick(object sender, EventArgs e)
    {
        ViewState["1"] = 118;

        LoadEnrollData(0);

    }
    public void LoadEnrollData(int Flag)
    {
        string conditions = "";
        string ddlBlock = "";
        string ddlDistrict = "";
        string ddlPhan = "";
        string ddlVillage = "";
        string ddlStatecode = "";
        foreach (ListItem item in ChkState.Items)
        {
            if (item.Selected)
            {

                ddlStatecode += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlStatecode.Length > 0)
        {
            ddlStatecode = ddlStatecode.Substring(0, ddlStatecode.LastIndexOf(","));
        }
        foreach (ListItem item in chkDistrict.Items)
        {
            if (item.Selected)
            {

                ddlDistrict += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlDistrict.Length > 0)
        {
            ddlDistrict = ddlDistrict.Substring(0, ddlDistrict.LastIndexOf(","));
        }
        foreach (ListItem item in chkBlock.Items)
        {
            if (item.Selected)
            {

                ddlBlock += "'" + item.Value + "'" + ",";


            }
        }

        if (ddlBlock.Length > 0)
        {
            ddlBlock = ddlBlock.Substring(0, ddlBlock.LastIndexOf(","));
        }

        //foreach (ListItem item in ddlPanchayat.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlPhan += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlPhan.Length > 0)
        //{
        //    ddlPhan = ddlPhan.Substring(0, ddlPhan.LastIndexOf(","));
        //}
        //foreach (ListItem item in chkVillage.Items)
        //{
        //    if (item.Selected)
        //    {

        //        ddlVillage += "'" + item.Value + "'" + ",";


        //    }
        //}

        //if (ddlVillage.Length > 0)
        //{
        //    ddlVillage = ddlVillage.Substring(0, ddlVillage.LastIndexOf(","));
        //}



        string condition = string.Empty;
        if (Flag == 2)
        {
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
        }
        else
        {
            if (ddlYear.SelectedIndex > 0)
            {
                conditions += "    and mst5Village.Fyear = '" + ddlYear.SelectedItem.Text + "' ";

            }
            if (ddlStatecode.Length > 0)
            {
                conditions += " and mst5Village.StateCode in(" + ddlStatecode + ") ";

            }
        }

        if (ddlDistrict.Length > 0)
        {
            conditions += " and mst5Village.DistrictCode in(" + ddlDistrict + ") ";

        }

        if (ddlBlock.Length > 0)
        {

            conditions += " and mst5Village.BlockCode in(" + ddlBlock + ") ";

        }
        if (ddlPhan.Length > 0)
        {
            conditions += " and mst5Village.PanchayatCode in(" + ddlPhan + ") ";
        }
        if (ddlVillage.Length > 0)
        {
            conditions += " and mst5Village.VillageCode in(" + ddlVillage + ") ";
        }
        if (ddlGender.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlYear.SelectedValue) >= 2020)
                conditions += " and Gender ='" + ddlGender.SelectedItem.Text + "' ";
            else
            {
                conditions += " and tblDTD.Gender = '" + (ddlGender.SelectedItem.Text == "Male" ? "1" : "2") + "' ";
            }
        }


        string Age = "";
        foreach (ListItem item in chkAge.Items)
        {
            if (item.Selected)
            {
                Age += "" + item.Value + "" + ",";
            }
        }
        string AgeEnGrouopp = "";

        if (Age.Length > 0)
        {
            Age = Age.Substring(0, Age.LastIndexOf(","));

            conditions += " and [Current Age] in(" + Age + ")";
        }


        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition",conditions),
            new SqlParameter("@Fyear",ddlYear.SelectedValue)
        };

        DataTable dt = null;
        dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptEnrollTargetD2dDetials]", cmdParameters);

        if (Convert.ToInt32(ddlYear.SelectedValue) >= 2020)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "Mother Name", "DOB"/*, "Current Age", "Age"*/);
        else if (Convert.ToInt32(ddlYear.SelectedValue) < 2020)
            dataTableMaskingHelper.DecryptAndMaskDataTable(dt, "Mauhalla", "House", "Child Name", "Father Name", "DOB"/*, "Current Age", "Age"*/);

        ViewState["D2dUser"] = dt;

        GV_DynamicGrid.Visible = true;
        if (dt.Rows.Count > 0)
        {
            ReportDownload("Enrollment Target Raw Data", "Contact Summary Report");
            ExportToCSVFile(dt, "EnrollmentTargetRawData");
        }
        else
        {
            GV_DynamicGrid.DataSource = null;
            GV_DynamicGrid.DataBind();
        }
    }
}