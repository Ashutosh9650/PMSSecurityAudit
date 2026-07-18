using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FrmActivityClusterSearchNew : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    string conditions = "";
    Comman objComman = new Comman();
    DataTable dtMain = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            if (!IsPostBack)
            {
                btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");

                LoadData();



                if (Request.QueryString["ID"] != null)
                {
                    #region #Back
                    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                    {
                        ddlBlock.Enabled = true;
                    }
                    else
                    {
                        ddlBlock.Enabled = false;
                    }
                    string QueryString = Request.QueryString["ID"];
                    string[] a = QueryString.Split(',');

                    ddlBlock.SelectedValue = Session["BlockCodeAct"].ToString();

                    //TxtFromDate.Text = a[1].ToString();
                    //txtDate.Text = a[2].ToString();
                    //btnSerach.Visible = false;

                    if (Convert.ToString(Session["Back"]) == "1")
                    {
                        txtDate.Text = a[2].ToString();
                        TxtFromDate.Text = a[1].ToString();
                        btnSerach_Click(btnSerach, null);
                        Session["Back"] = "";
                    }

                    #endregion
                }
                else
                {
                    #region Main

                    btnBack.Visible = false;
                    btnSerach.Visible = true;
                    txtDate.Enabled = true;
                    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                    {
                        #region Impact
                        ddlBlock.SelectedIndex = 1;
                        ddlBlock_OnSelectedIndexChanged(ddlBlock, null);
                        ddlBlock.Enabled = true;
                        dtMain = objMain.GetActivityUpdateDateWiseBlockWiseNew(ddlBlock.SelectedValue, "2", "B");
                        if (dtMain.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            dtMain = objMain.GetActivityUpdateDateWiseBlockWise(ddlBlock.SelectedValue, "2", "B");
                        }
                        if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() != "")
                        {
                            #region DataSelection
                            DateTime Activitydate1 = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                            DateTime Activitydate;
                            if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                            {
                                Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                            }
                            else
                            {
                                Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                            }
                            TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            string fromDate = TxtFromDate.Text;

                            string[] d = fromDate.Split('/');
                            string afromDate = d[2] + '-' + d[1] + '-' + d[0];
                            DateTime dttest = Activitydate.AddDays(14);
                            txtDate.Text = dttest.ToString("dd/MM/yyyy");
                            string ToDate = txtDate.Text;
                            string[] c = ToDate.Split('/');
                            string aToDate = c[2] + '-' + c[1] + '-' + c[0];
                            Int32 maxdate = Convert.ToInt32(c[0]);
                            if (d[1].ToString() != "")
                            {

                                if (Convert.ToInt32(d[1]) != 3)
                                {

                                    if (Convert.ToInt32(d[0]) <= 25 && Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                                    {
                                        maxdate = (25 / Convert.ToInt32(d[1])) / Convert.ToInt32(d[2]);
                                        string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "25";
                                        //DateTime dttest1 = Activitydate.AddDays(maxdate);

                                        //txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                        DateTime dttest1 = Convert.ToDateTime(data);
                                        txtDate.Text = dttest1.ToString("dd/MM/yyyy");

                                        maxdate = 25;
                                    }
                                    else
                                    {
                                        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                    }


                                }
                                else if (Convert.ToInt32(d[1]) == 3)
                                {
                                    if (Convert.ToInt32(c[0]) >= 25 || Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                                    {
                                        //maxdate = 31 / Convert.ToInt32(d[1]) / Convert.ToInt32(d[2]);
                                        string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "31";
                                        //DateTime dttest1 = Activitydate.AddDays(maxdate);
                                        // txtDate.Text = data;
                                        DateTime dttest1 = Convert.ToDateTime(data);
                                        txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                        maxdate = 31;
                                    }

                                    else if (maxdate > DateTime.Now.Day)
                                    {
                                        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                    }
                                }

                                else if (maxdate > DateTime.Now.Day)
                                {
                                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                }

                            }
                            #endregion

                        }
                        else
                        {

                            TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            //DateTime dttest = DateTime.Now.AddDays(14);
                            //txtDate.Text = dttest.ToString("dd/MM/yyyy");
                        }

                        if (Convert.ToString(Session["Back"]) == "1")
                        {
                            txtDate.Text = Session["Todate"].ToString();
                            btnSerach_Click(btnSerach, null);
                            Session["Back"] = "";
                        }
                        txtDate.Enabled = true;
                        btnBack.Visible = true;
                        #endregion
                    }



                    if (Session["user_level"].ToString() == "19")
                    {
                        Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
                        //  DataTable dtMain = objMain.GetActivityUpdateDateWiseBlockWise(Session["NewBlockCode"].ToString(), "2", "FC");
                        dtMain = objMain.GetActivityUpdateDateWiseBlockWiseNew(ddlBlock.SelectedValue, "2", "FC");
                        if (dtMain.Rows.Count > 0)
                        {
                        }
                        else
                        {
                            dtMain = objMain.GetActivityUpdateDateWiseBlockWise(ddlBlock.SelectedValue, "2", "FC");
                        }
                        if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() != "")
                        {
                            #region DataSelection
                            DateTime Activitydate1 = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                            DateTime Activitydate;
                            if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                            {
                                Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                            }
                            else
                            {
                                Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                            }
                            TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            string fromDate = TxtFromDate.Text;

                            string[] d = fromDate.Split('/');
                            string afromDate = d[2] + '-' + d[1] + '-' + d[0];
                            DateTime dttest = Activitydate.AddDays(14);
                            txtDate.Text = dttest.ToString("dd/MM/yyyy");
                            string ToDate = txtDate.Text;
                            string[] c = ToDate.Split('/');
                            string aToDate = c[2] + '-' + c[1] + '-' + c[0];
                            Int32 maxdate = Convert.ToInt32(c[0]);
                            if (d[1].ToString() != "")
                            {

                                if (Convert.ToInt32(d[1]) != 3)
                                {

                                    if (Convert.ToInt32(d[0]) <= 25 && Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                                    {
                                        maxdate = (25 / Convert.ToInt32(d[1])) / Convert.ToInt32(d[2]);
                                        string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "25";
                                        //DateTime dttest1 = Activitydate.AddDays(maxdate);

                                        //txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                        DateTime dttest1 = Convert.ToDateTime(data);
                                        txtDate.Text = dttest1.ToString("dd/MM/yyyy");

                                        maxdate = 25;
                                    }
                                    else
                                    {
                                        //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                    }


                                }
                                else if (Convert.ToInt32(d[1]) == 3)
                                {
                                    if (Convert.ToInt32(c[0]) >= 25 || Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                                    {
                                        //maxdate = 31 / Convert.ToInt32(d[1]) / Convert.ToInt32(d[2]);
                                        string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "31";
                                        //DateTime dttest1 = Activitydate.AddDays(maxdate);
                                        // txtDate.Text = data;
                                        DateTime dttest1 = Convert.ToDateTime(data);
                                        txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                        maxdate = 31;
                                    }

                                    else if (maxdate > DateTime.Now.Day)
                                    {
                                        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                    }
                                }

                                else if (maxdate > DateTime.Now.Day)
                                {
                                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                                }

                            }
                            #endregion

                        }
                        else
                        {

                            TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            //DateTime dttest = DateTime.Now.AddDays(14);
                            //txtDate.Text = dttest.ToString("dd/MM/yyyy");
                        }

                        if (Convert.ToString(Session["Back"]) == "1")
                        {
                            txtDate.Text = Session["Todate"].ToString();
                            btnSerach_Click(btnSerach, null);
                            Session["Back"] = "";
                        }
                    }


                    #endregion
                }



            }


            Session["FromDate"] = TxtFromDate.Text;
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
    }
    public void ReferPage()
    {
        #region Main

        btnBack.Visible = false;
        btnSerach.Visible = true;
        txtDate.Enabled = true;
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            #region Impact
            ddlBlock.SelectedIndex = 1;
            ddlBlock_OnSelectedIndexChanged(ddlBlock, null);
            ddlBlock.Enabled = true;
            dtMain = objMain.GetActivityUpdateDateWiseBlockWiseNew(ddlBlock.SelectedValue, "2", "B");
            if (dtMain.Rows.Count > 0)
            {
            }
            else
            {
                dtMain = objMain.GetActivityUpdateDateWiseBlockWise(ddlBlock.SelectedValue, "2", "B");
            }
            if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() != "")
            {
                #region DataSelection
                DateTime Activitydate1 = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                DateTime Activitydate;
                if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                {
                    Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                }
                else
                {
                    Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                }
                TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                string fromDate = TxtFromDate.Text;

                string[] d = fromDate.Split('/');
                string afromDate = d[2] + '-' + d[1] + '-' + d[0];
                DateTime dttest = Activitydate.AddDays(14);
                txtDate.Text = dttest.ToString("dd/MM/yyyy");
                string ToDate = txtDate.Text;
                string[] c = ToDate.Split('/');
                string aToDate = c[2] + '-' + c[1] + '-' + c[0];
                Int32 maxdate = Convert.ToInt32(c[0]);
                if (d[1].ToString() != "")
                {

                    if (Convert.ToInt32(d[1]) != 3)
                    {

                        if (Convert.ToInt32(d[0]) <= 25 && Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                        {
                            maxdate = (25 / Convert.ToInt32(d[1])) / Convert.ToInt32(d[2]);
                            string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "25";
                            //DateTime dttest1 = Activitydate.AddDays(maxdate);

                            //txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                            DateTime dttest1 = Convert.ToDateTime(data);
                            txtDate.Text = dttest1.ToString("dd/MM/yyyy");

                            maxdate = 25;
                        }
                        else
                        {
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }


                    }
                    else if (Convert.ToInt32(d[1]) == 3)
                    {
                        if (Convert.ToInt32(c[0]) >= 25 || Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                        {
                            //maxdate = 31 / Convert.ToInt32(d[1]) / Convert.ToInt32(d[2]);
                            string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "31";
                            //DateTime dttest1 = Activitydate.AddDays(maxdate);
                            // txtDate.Text = data;
                            DateTime dttest1 = Convert.ToDateTime(data);
                            txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                            maxdate = 31;
                        }

                        else if (maxdate > DateTime.Now.Day)
                        {
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                    }

                    else if (maxdate > DateTime.Now.Day)
                    {
                        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                }
                #endregion

            }
            else
            {

                TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //DateTime dttest = DateTime.Now.AddDays(14);
                //txtDate.Text = dttest.ToString("dd/MM/yyyy");
            }

            if (Convert.ToString(Session["Back"]) == "1")
            {
                txtDate.Text = Session["Todate"].ToString();
                btnSerach_Click(btnSerach, null);
                Session["Back"] = "";
            }
            txtDate.Enabled = true;
            btnBack.Visible = true;
            #endregion
        }



        if (Session["user_level"].ToString() == "19")
        {
            Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
            //  DataTable dtMain = objMain.GetActivityUpdateDateWiseBlockWise(Session["NewBlockCode"].ToString(), "2", "FC");
            dtMain = objMain.GetActivityUpdateDateWiseBlockWiseNew(ddlBlock.SelectedValue, "2", "FC");
            if (dtMain.Rows.Count > 0)
            {
            }
            else
            {
                dtMain = objMain.GetActivityUpdateDateWiseBlockWise(ddlBlock.SelectedValue, "2", "FC");
            }
            if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() != "")
            {
                #region DataSelection
                DateTime Activitydate1 = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                DateTime Activitydate;
                if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                {
                    Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                }
                else
                {
                    Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                }
                TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                string fromDate = TxtFromDate.Text;

                string[] d = fromDate.Split('/');
                string afromDate = d[2] + '-' + d[1] + '-' + d[0];
                DateTime dttest = Activitydate.AddDays(14);
                txtDate.Text = dttest.ToString("dd/MM/yyyy");
                string ToDate = txtDate.Text;
                string[] c = ToDate.Split('/');
                string aToDate = c[2] + '-' + c[1] + '-' + c[0];
                Int32 maxdate = Convert.ToInt32(c[0]);
                if (d[1].ToString() != "")
                {

                    if (Convert.ToInt32(d[1]) != 3)
                    {

                        if (Convert.ToInt32(d[0]) <= 25 && Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                        {
                            maxdate = (25 / Convert.ToInt32(d[1])) / Convert.ToInt32(d[2]);
                            string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "25";
                            //DateTime dttest1 = Activitydate.AddDays(maxdate);

                            //txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                            DateTime dttest1 = Convert.ToDateTime(data);
                            txtDate.Text = dttest1.ToString("dd/MM/yyyy");

                            maxdate = 25;
                        }
                        else
                        {
                            //txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }


                    }
                    else if (Convert.ToInt32(d[1]) == 3)
                    {
                        if (Convert.ToInt32(c[0]) >= 25 || Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                        {
                            //maxdate = 31 / Convert.ToInt32(d[1]) / Convert.ToInt32(d[2]);
                            string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "31";
                            //DateTime dttest1 = Activitydate.AddDays(maxdate);
                            // txtDate.Text = data;
                            DateTime dttest1 = Convert.ToDateTime(data);
                            txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                            maxdate = 31;
                        }

                        else if (maxdate > DateTime.Now.Day)
                        {
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }
                    }

                    else if (maxdate > DateTime.Now.Day)
                    {
                        txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                }
                #endregion

            }
            else
            {

                TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //DateTime dttest = DateTime.Now.AddDays(14);
                //txtDate.Text = dttest.ToString("dd/MM/yyyy");
            }

            if (Convert.ToString(Session["Back"]) == "1")
            {
                txtDate.Text = Session["Todate"].ToString();
                btnSerach_Click(btnSerach, null);
                Session["Back"] = "";
            }
        }


        #endregion
    }
    protected void ddlBlock_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
            {
                #region Impact
                dtMain = objMain.GetActivityUpdateDateWiseBlockWiseNew(ddlBlock.SelectedValue, "2", "B");
                if (dtMain.Rows.Count > 0)
                {
                }
                else
                {
                    dtMain = objMain.GetActivityUpdateDateWiseBlockWise(ddlBlock.SelectedValue, "2", "B");
                }

                if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() != "")
                {
                    #region DataSelection
                    DateTime Activitydate1 = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                    DateTime Activitydate;
                    if (Activitydate1.Day == 1 && Activitydate1.Month == 4)
                    {
                        Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(0);
                    }
                    else
                    {
                        Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString()).AddDays(1);
                    }
                    TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    string fromDate = TxtFromDate.Text;

                    string[] d = fromDate.Split('/');
                    string afromDate = d[2] + '-' + d[1] + '-' + d[0];
                    DateTime dttest = Activitydate.AddDays(14);
                    txtDate.Text = dttest.ToString("dd/MM/yyyy");
                    string ToDate = txtDate.Text;
                    string[] c = ToDate.Split('/');
                    string aToDate = c[2] + '-' + c[1] + '-' + c[0];
                    Int32 maxdate = Convert.ToInt32(c[0]);
                    if (d[1].ToString() != "")
                    {

                        if (Convert.ToInt32(d[1]) != 3)
                        {

                            if (Convert.ToInt32(d[0]) <= 25 && Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                            {
                                maxdate = (25 / Convert.ToInt32(d[1])) / Convert.ToInt32(d[2]);
                                string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "25";
                                //DateTime dttest1 = Activitydate.AddDays(maxdate);

                                //txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                DateTime dttest1 = Convert.ToDateTime(data);
                                txtDate.Text = dttest1.ToString("dd/MM/yyyy");

                                maxdate = 25;
                            }
                            else
                            {
                                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }


                        }
                        else if (Convert.ToInt32(d[1]) == 3)
                        {
                            if (Convert.ToInt32(c[0]) >= 25 || Convert.ToInt32(d[1]) != Convert.ToInt32(c[1]))
                            {
                                //maxdate = 31 / Convert.ToInt32(d[1]) / Convert.ToInt32(d[2]);
                                string data = Convert.ToInt32(d[2]).ToString() + '-' + d[1] + '-' + "31";
                                //DateTime dttest1 = Activitydate.AddDays(maxdate);
                                // txtDate.Text = data;
                                DateTime dttest1 = Convert.ToDateTime(data);
                                txtDate.Text = dttest1.ToString("dd/MM/yyyy");
                                maxdate = 31;
                            }

                            else if (maxdate > DateTime.Now.Day)
                            {
                                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                        }

                        else if (maxdate > DateTime.Now.Day)
                        {
                            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        }

                    }
                    #endregion
                }
                else
                {

                    TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //DateTime dttest = DateTime.Now.AddDays(14);
                    //txtDate.Text = dttest.ToString("dd/MM/yyyy");
                }


                txtDate.Enabled = true;
                btnBack.Visible = true;
                Session["BlockName"] = ddlBlock.SelectedItem.Text;
                Session["BlockCodeAct"] = ddlBlock.SelectedValue;
                #endregion
            }


            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();

            gvVillageActivity.DataSource = null;
            gvVillageActivity.DataBind();
            gvOffice.DataSource = null;
            gvOffice.DataBind();
        }
        catch (Exception)
        {

            throw;
        }
    }
    public void LoadDataBlock(string blockName)
    {


        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            string strQry = "";

            strQry = "Select * from mst3Block  where DistrictCode='" + Session["NewDistrictCode"].ToString() + "' and BlockName='" + blockName + "' ";


            DataTable dtBlock = objMain.LoadData(strQry);

            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = false;
            ddlBlock.SelectedValue = dtBlock.Rows[0]["BlockCode"].ToString();
            Session["BlockName"] = blockName;
            Session["BlockCodeAct"] = dtBlock.Rows[0]["BlockCode"].ToString();
        }
        else if (Session["user_level"].ToString() == "145")
        {
            string strQry = "";

            strQry = "Select * from mst3Block  where DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ") and BlockName='" + blockName + "' ";


            DataTable dtBlock = objMain.LoadData(strQry);

            conditions = "   DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ") ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = false;
            ddlBlock.SelectedValue = dtBlock.Rows[0]["BlockCode"].ToString();
            Session["BlockName"] = blockName;
            Session["BlockCodeAct"] = dtBlock.Rows[0]["BlockCode"].ToString();
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "'   and mst2District.FYear ='" + Session["FinYear"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
            Session["BlockCodeAct"] = Session["NewBlockCode"].ToString();
        }




    }
    public void LoadData()
    {


        conditions = "";
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
        {
            conditions = "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = true;
        }
        else if (Session["user_level"].ToString() == "145")
        {
            conditions = "   DistrictCode in(" + Session["DistrictCodeMul"].ToString() + ")  ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");
            ddlBlock.Enabled = true;
        }
        else
        {

            conditions = conditions + "  DistrictCode='" + Session["NewDistrictCode"].ToString() + "'  and BlockCode ='" + Session["NewBlockCode"].ToString() + "' ";



            objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

            ddlBlock.Enabled = false;

            ddlBlock.SelectedValue = Session["NewBlockCode"].ToString();
        }




    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["Backlk"] = 1;
        Response.Redirect("~/FrmActivityBlockWiseSearch.aspx");
    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        //  DGV_Report.Visible = true;
        Gv_Profile_Search.Visible = false;
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];





        DateTime d1 = Convert.ToDateTime(afromDate);
        DateTime d2 = Convert.ToDateTime(aToDate);
        int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days) == -1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);

            return;
        }
        if (Math.Round(Days) > 14)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 14 day can be selected')</script>", false);

            return;
        }

        SqlParameter[] parm = new SqlParameter[]
             {
               new SqlParameter("@fDate",  afromDate),
               new SqlParameter("@todate",  aToDate),


                 };

        DataTable dtUserVillage = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityUdate]", parm);
        //DGV_Report.DataSource = dtUserVillage;
        //DGV_Report.DataBind();
        //ViewState["dtUserVillage"] = dtUserVillage;
    }



    protected void Export_To_Excel(object sender, EventArgs e)
    {
        DataTable dt = ViewState["dtUserVillage"] as DataTable;
        // ExporttoExcel(DGV_Report, dt);


    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        Session["FromData"] = TxtFromDate.Text;
        Session["Todate"] = txtDate.Text;
        Response.Redirect("~/FrmReportActivityClusterSearch.aspx?ID=" + ddlBlock.SelectedValue + "");

    }

    public void ApproveData()
    {
        int MainResultVillage = 0;
        int MainResultSchool = 0;
        int MainResultOffice = 0;
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        DateTime d1 = Convert.ToDateTime(afromDate);
        DateTime d2 = Convert.ToDateTime(aToDate);
        int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days) == -1)
        {

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);

            return;
        }
        if (Math.Round(Days) > 14)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 14 day can be selected')</script>", false);

            return;
        }
        if (txtDate.Text != "" && Convert.ToDateTime(txtDate.Text) > DateTime.Today)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Should not be future date')</script>", false);

            return;
        }



        #region Check BlackData

        string Query = " SELECT   CONVERT(varchar,dateadd(d,number-1,'" + afromDate + "'),103) as ActivityDate from Numbers WHERE Number<=DATEDIFF(day,('" + afromDate + "'),CONVERT(datetime,'" + aToDate + "')+1)";
        DataTable dtBlackAll = objMain.LoadData(Query);


        string QueryCluseter = "select mstCluster.ClusterName,mstCluster.ClusterCode from mst5Village ";
        QueryCluseter += " inner join mstCluster on mstCluster.ClusterCode=mst5Village.ClusterCode where mst5Village.BlockCode='" + ddlBlock.SelectedValue + "'  and  len(EGClusterCode)>2  group by mstCluster.ClusterName,mstCluster.ClusterCode ";
        DataTable dtBlackCluseter = objMain.LoadData(QueryCluseter);

        for (int dr = 0; dr < dtBlackCluseter.Rows.Count; dr++)
        {

            DataRow[] drNew = null;
            for (int r = 0; r < dtBlackAll.Rows.Count; r++)
            {
                //if (dtEditblackData.Rows.Count > 0)
                //{
                //for (int i = 0; i < dtEditblackData.Rows.Count; i++)
                //{
                DateTime dateValue = Convert.ToDateTime(dtBlackAll.Rows[r]["ActivityDate"].ToString());
                string str = dateValue.ToString("ddd");
                if (str == "Sun")
                {
                }
                else
                {
                    DataTable dtMainRecord = objMain.GetActivityDateWiseBlankRecord(Convert.ToDateTime(dtBlackAll.Rows[r]["ActivityDate"]).ToString("yyyy-MM-dd"), aToDate, dtBlackCluseter.Rows[dr]["ClusterCode"].ToString(), 6);
                    //drNew = dtMainRecord.Select("ActivityDate='" + dtBlackAll.Rows[r]["ActivityDate"] + "' and VillageCode='" + dtBlackCluseter.Rows[dr]["VillageCode"] + "'  ");
                    if (dtMainRecord.Rows.Count > 0)
                    {


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Should not be blank please update  black activity  in Next Page')</script>", false);

                        return;

                    }
                }


            }
        }
        #endregion

        string condation = "";
        string condationOffice = "";

        if (Session["user_level"].ToString() == "19")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and blk.blockcode='" + Session["NewBlockCode"].ToString() + "' and  UserEntry ='2' and ApproveStatus='FC'  ";
            condationOffice = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and blk.blockcode='" + Session["NewBlockCode"].ToString() + "' and   ApproveStatus='FC'  ";
            /// condationOffice1 = "Registrationdate between('" + afromDate + "') and '" + aToDate + "' and blk.blockcode='" + Session["NewBlockCode"].ToString() + "' and   ApproveStatus='FC'  ";

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and blk.blockcode='" + ddlBlock.SelectedValue.ToString() + "' and  UserEntry ='3' and ApproveStatus='B'  ";
            condationOffice = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and blk.blockcode='" + ddlBlock.SelectedValue.ToString() + "' and   ApproveStatus='B'  ";
            // condationOffice1 = "Registrationdate between('" + afromDate + "') and '" + aToDate + "' and blk.blockcode='" + ddlBlock.SelectedValue.ToString() + "' and   ApproveStatus='B'  ";

        }
        try
        {
            DataTable dtApprove = objMain.LoadSchoolActivtiyApproveNew(condation, 1);
            DataTable dtApproveVillage = objMain.LoadVillageActivtiyApproveNew(condation, 1);

            DataTable dtApproveOffice = objMain.LoadOfficeActivtiyApprove(condationOffice, 1);
            DataTable dtApproveGKP = objMain.LoadGKPActivtiyApprove(condationOffice, 1);

            if (dtApprove.Rows.Count > 0 || dtApproveVillage.Rows.Count > 0 || dtApproveOffice.Rows.Count > 0 || dtApproveGKP.Rows.Count > 0)
            {
                if (dtApprove.Rows.Count > 0)
                {
                    String[] arColoumn = { "ApproveStatus" };
                    DataTable dtDistinct = dtApprove.DefaultView.ToTable(true, arColoumn);
                    string Statas = "";
                    foreach (DataRow Item in dtDistinct.Rows)
                    {
                        #region ApproveSchool
                        if (Session["user_level"].ToString() == "19")
                        {
                            Statas = "B";
                            if (Item["ApproveStatus"].ToString() == "FC")
                            {

                            }

                            if (Item["ApproveStatus"].ToString() == "I")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Impact Officer Allready Approve  in School Activity ')</script>", false);
                                // btnSerach_Click(btnSerach, null);
                                return;

                            }
                        }

                        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                        {
                            Statas = "I";
                            if (Item["ApproveStatus"].ToString() == "FC")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Frist  Approve By BO in School Activity')</script>", false);
                                //  btnSerach_Click(btnSerach, null);
                                return;

                            }


                        }
                        #endregion
                    }

                    MainResultSchool = objMain.ActivitySchoolStatusUpdateNew(Statas, condation, 1);


                    //DataTable dtApproveData = objMain.LoadSchoolActivtiyApproveNew(condation, 2);
                    //foreach (DataRow dr in dtApproveData.Rows)
                    //{

                    //    string Newcondation = " GUID_School='" + dr["GUID_School"].ToString() + "'  ";
                    //    MainResultSchool = objMain.ActivitySchoolStatusUpdateNew(Statas, condation);

                    //}
                }

                if (dtApproveVillage.Rows.Count > 0)
                {
                    String[] arColoumn = { "ApproveStatus" };
                    DataTable dtDistinct = dtApproveVillage.DefaultView.ToTable(true, arColoumn);
                    string Statas = "";
                    foreach (DataRow Item in dtDistinct.Rows)
                    {
                        #region ApproveVillage
                        if (Session["user_level"].ToString() == "19")
                        {
                            Statas = "B";
                            if (Item["ApproveStatus"].ToString() == "FC")
                            {

                            }

                            if (Item["ApproveStatus"].ToString() == "I")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Impact Officer Allready Approve in Village Activity')</script>", false);
                                // btnSerach_Click(btnSerach, null);
                                return;

                            }
                        }

                        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                        {
                            Statas = "I";
                            if (Item["ApproveStatus"].ToString() == "FC")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Frist  Approve By BO in Village Activity ')</script>", false);
                                // btnSerach_Click(btnSerach, null);
                                return;

                            }


                        }
                        #endregion
                    }
                    MainResultSchool = objMain.ActivitySchoolStatusUpdateNew(Statas, condation, 2);
                    //DataTable dtApproveData = objMain.LoadVillageActivtiyApproveNew(condation, 2);
                    //foreach (DataRow dr in dtApproveData.Rows)
                    //{
                    //    string Newcondation = " GUID_Village='" + dr["GUID_Village"].ToString() + "'  ";
                    //    MainResultVillage = objMain.ActivityVillageStatusUpdate(Statas, Newcondation);
                    //}
                }

                if (dtApproveGKP.Rows.Count > 0)
                {
                    if (Session["user_level"].ToString() == "19")
                    {

                        objMain.LoadGKPActivtiyApprove(condationOffice, 2);
                    }
                    else
                    {
                        objMain.LoadGKPActivtiyApprove(condationOffice, 3);
                    }

                }



                Int32 MainResultOffice2 = 0;
                if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                {
                    MainResultOffice2 = objMain.ActivityeApproveStatus(ddlBlock.SelectedValue, Convert.ToDateTime(afromDate), Convert.ToDateTime(aToDate), 1);
                }
                else
                {
                    MainResultOffice2 = objMain.ActivityeApproveStatus(ddlBlock.SelectedValue, Convert.ToDateTime(afromDate), Convert.ToDateTime(aToDate), 2);
                }

                if (dtApproveOffice.Rows.Count > 0)
                {
                    String[] arColoumn = { "ApproveStatus" };
                    DataTable dtDistinct = dtApproveOffice.DefaultView.ToTable(true, arColoumn);
                    string Statas = "";
                    foreach (DataRow Item in dtDistinct.Rows)
                    {
                        #region dtApproveOffice
                        if (Session["user_level"].ToString() == "19")
                        {
                            Statas = "B";
                            if (Item["ApproveStatus"].ToString() == "FC")
                            {

                            }

                            if (Item["ApproveStatus"].ToString() == "I")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Impact Officer Allready Approve in Office Activity')</script>", false);
                                btnSerach_Click(btnSerach, null);
                                return;

                            }
                        }

                        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
                        {
                            Statas = "I";
                            if (Item["ApproveStatus"].ToString() == "FC")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Frist  Approve By BO in Office Activity ')</script>", false);
                                btnSerach_Click(btnSerach, null);
                                return;

                            }


                        }
                        #endregion
                    }
                    MainResultSchool = objMain.ActivitySchoolStatusUpdateNew(Statas, condationOffice, 3);
                    //DataTable dtApproveData = objMain.LoadOfficeActivtiyApprove(condationOffice, 2);
                    //foreach (DataRow dr in dtApproveData.Rows)
                    //{
                    //    string Newcondation = " GUID_Office='" + dr["GUID_Office"].ToString() + "'  ";
                    //    MainResultOffice = objMain.ActivityOfficeStatusUpdate(Statas, Newcondation);
                    //}
                }

                if (dtApproveGKP.Rows.Count > 0)
                {
                    //String[] arColoumn = { "ApproveStatus" };
                    //DataTable dtDistinct = dtApproveGKP.DefaultView.ToTable(true, arColoumn);
                    //string Statas = "";
                    //foreach (DataRow Item in dtDistinct.Rows)
                    //{
                    //    #region dtApproveOffice
                    //    if (Session["user_level"].ToString() == "19")
                    //    {
                    //        Statas = "B";
                    //        if (Item["ApproveStatus"].ToString() == "FC")
                    //        {

                    //        }

                    //        if (Item["ApproveStatus"].ToString() == "I")
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Impact Officer Allready Approve in Office Activity')</script>", false);
                    //            btnSerach_Click(btnSerach, null);
                    //            return;

                    //        }
                    //    }

                    //    if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                    //    {
                    //        Statas = "I";
                    //        if (Item["ApproveStatus"].ToString() == "FC")
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Frist  Approve By BO in Office Activity ')</script>", false);
                    //            btnSerach_Click(btnSerach, null);
                    //            return;

                    //        }


                    //    }
                    //    #endregion
                    //}

                }


                if (MainResultSchool > 0 || MainResultVillage > 0 || MainResultOffice > 0 || MainResultOffice2 > 0)
                {
                    DataTable dtMain = null;
                    //if (Session["user_level"].ToString() == "19")
                    //{
                    //    dtMain = objMain.GetActivityUpdateDateWiseBlockWise(Session["NewBlockCode"].ToString(), "2", "FC");
                    //}
                    //if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30")
                    //{
                    //    dtMain = objMain.GetActivityUpdateDateWiseBlockWise(Session["NewBlockCode"].ToString(), "3", "B");

                    //}
                    //if (dtMain.Rows.Count > 0 && dtMain.Rows[0]["ActivityDate"].ToString() != "")
                    //{
                    //    DateTime Activitydate = Convert.ToDateTime(dtMain.Rows[0]["ActivityDate"].ToString());
                    //    TxtFromDate.Text = Activitydate.ToString("dd/MM/yyyy");

                    //}
                    //else
                    //{
                    //    TxtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");


                    //}
                    ReferPage();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve sucessfully')</script>", false);
                    btnSerach_Click(btnSerach, null);
                }



            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Record Found ')</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {

        btnApprove.Attributes.Add("onclick", "javascript:return " + "confirm('Please confirm if you want to approve? ')");
        ApproveData();
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {

            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }




    }


    protected void btnSerach_Click(object sender, EventArgs e)
    {
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select To Date')</script>", false);
            return;
        }

        gvVillageOffice.DataSource = null;
        gvVillageOffice.DataBind();

        gvVillageDeatial.DataSource = null;
        gvVillageDeatial.DataBind();
        gvVillageWise.DataSource = null;
        gvVillageWise.DataBind();
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        DateTime d1 = Convert.ToDateTime(afromDate);
        DateTime d2 = Convert.ToDateTime(aToDate);
        int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        TimeSpan t = d2 - d1;

        double Days = Convert.ToDouble(t.TotalDays);
        if (Math.Sign(Days) == -1)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);

            return;
        }
        if (Math.Round(Days) > 14)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 14 day can be selected')</script>", false);

            return;
        }
        DataTable dtCheck = objMain.GetActivityUpdateDateWiseBlockWiseNew(ddlBlock.SelectedValue, "2", "FC");
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            if (dtCheck.Rows.Count > 0)
            {
                DateTime Activitydate1 = Convert.ToDateTime(dtCheck.Rows[0]["ActivityDate"].ToString());
                if (Activitydate1 < d2)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('BO Last Approval Date " + Activitydate1.ToString("yyyy-MM-dd") + " Select less then ToDate')</script>", false);

                    return;
                }
            }
        }
        LoadSerarchSchoolActivity();
        LoadSearchVillageActivity();
        LoadSearchOfficeActivtiy();

    }

    public DataTable LoadActivtiyAllClusterWise(string fdate, string toDate, string userName, string WhereQuery, string WhereQuery1, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@WhereQueryNew", WhereQuery1),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyAllClusterWiseNew20222023]", cmdParameters);
    }
    public void LoadSerarchSchoolActivity()
    {
        Session["dt"] = null;
        // DGV_Report.Visible = false;
        Gv_Profile_Search.Visible = true;



        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        //DateTime d1 = Convert.ToDateTime(afromDate);
        //DateTime d2 = Convert.ToDateTime(aToDate);
        //int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        //TimeSpan t = d2 - d1;

        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 7)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Max 7 day can be selected')</script>", false);
        //    return;
        //}
        DataTable dtMain = null;
        string con = "";
        string con1 = "";
        //if (Session["user_level"].ToString() == "19")
        //{
        //    con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
        //    dtMain = objMain.LoadSchoolActivtiyCluseter(afromDate, aToDate, ddlBlock.SelectedValue, con);

        //}
        Gv_Profile_Search.DataSource = null;
        Gv_Profile_Search.DataBind();


        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC' and UserEntry=2 and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
            con1 = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC'  and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";

            dtMain = LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con, con1, 1);
            // dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, ddlBlock.SelectedValue, con, 2);

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con1 = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B'  and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";

            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B' and UserEntry=3 and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            dtMain = LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con, con1, 1);
            //dtMain = objMain.LoadSchoolActivtiyForAllTypeReport(afromDate, aToDate, ddlBlock.SelectedValue, con,3);

        }
        string condation = "";
        //if (Session["user_level"].ToString() == "19" )
        //{
        //     condation = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='2'  ";
        //}
        // if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" )
        //{
        //      condation= "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and UserID='" + ddlBlock.SelectedValue + "' and  UserEntry ='3' ";
        //}

        // DataTable dtApprove = objMain.LoadSchoolActivtiyApprove(condation);

        // Session["dtApprove"] = dtApprove;

        int count = 0;
        if (dtMain.Rows.Count > 0)
        {
            #region School

            btnApprove.Visible = true;
            string strGSS = "TBHandholding";
            DataRow[] dr = dtMain.Select("School='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 2;
                Item1["School"] = "TBHandholding";
            }

            string strGSS3 = "School Count";
            DataRow[] dr3 = dtMain.Select("School='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 1;
                Item1["School"] = "School Count";
            }

            //string strGSS4 = "Retention";
            //DataRow[] dr4 = dtMain.Select("School='" + strGSS4 + "'");
            //if (dr4.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 3;
            //    Item1["School"] = "Retention";
            //}

            string strGSS5 = "School infra update";
            DataRow[] dr5 = dtMain.Select("School='" + strGSS5 + "'");
            if (dr5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 9;
                Item1["School"] = "School infra update";
            }
            string strGSS56 = "SMC Meeting";
            DataRow[] dr6 = dtMain.Select("School='" + strGSS56 + "'");
            if (dr6.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 5;
                Item1["School"] = "SMC Meeting";
            }


            string strGSS1 = "SAC Quarter Update";
            DataRow[] dr1 = dtMain.Select("School='" + strGSS1 + "'");
            if (dr1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);

                Item1["SRNo"] = 6;

                Item1["School"] = "SAC Quarter Update";
            }


            //string strGSS123 = "Bal Sabha";
            //DataRow[] dr21 = dtMain.Select("School='" + strGSS123 + "'");
            //if (dr21.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 7;
            //    Item1["School"] = "Bal Sabha";
            //}
            string strGSS1231 = "School Contact";
            DataRow[] dr211 = dtMain.Select("School='" + strGSS1231 + "'");
            if (dr211.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 8;
                Item1["School"] = "School Contact";
            }

            //string strGSS12311 = "Life Skill Game 2";
            //DataRow[] dr2111 = dtMain.Select("School='" + strGSS12311 + "'");
            //if (dr2111.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 9;
            //    Item1["School"] = "Life Skill Game 2";
            //}
            //string Game3 = "Life Skill Game 3";
            //DataRow[] drGame3 = dtMain.Select("School='" + Game3 + "'");
            //if (drGame3.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 10;
            //    Item1["School"] = "Life Skill Game 3";
            //}
            //string Game4 = "Life Skill Game 4";
            //DataRow[] drGame4 = dtMain.Select("School='" + Game4 + "'");
            //if (drGame4.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 11;
            //    Item1["School"] = "Life Skill Game 4";
            //}
            //string Game5 = "Life Skill Game 5";
            //DataRow[] drGame5 = dtMain.Select("School='" + Game5 + "'");
            //if (drGame5.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 12;
            //    Item1["School"] = "Life Skill Game 5";
            //}


            //string CLt = "CLT";
            //DataRow[] drCLt = dtMain.Select("School='" + CLt + "'");
            //if (drCLt.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 13;
            //    Item1["School"] = "CLT";
            //}



            //string CLt1 = "Learning Baseline";
            //DataRow[] drCLt1 = dtMain.Select("School='" + CLt1 + "'");
            //if (drCLt1.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 14;
            //    Item1["School"] = "Learning Baseline";
            //}

            //string CLt2 = "Learning  Midline";
            //DataRow[] drCLt2 = dtMain.Select("School='" + CLt2 + "'");
            //if (drCLt2.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 15;
            //    Item1["School"] = "Learning  Midline";
            //}

            //string CLt3 = "Learning  Endline";
            //DataRow[] drCLt3 = dtMain.Select("School='" + CLt3 + "'");
            //if (drCLt3.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 16;
            //    Item1["School"] = "Learning  Endline";

            //}

            //string CLt4 = "Learning  Endline";
            //DataRow[] drCLt4 = dtMain.Select("School='" + CLt4 + "'");
            //if (drCLt4.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);


            //    Item1["SRNo"] = 16;
            //    Item1["School"] = "Learning  Endline";
            //}


            string CLt5 = "Other Activity";
            DataRow[] drCLt5 = dtMain.Select("School='" + CLt5 + "'");
            if (drCLt5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);


                Item1["SRNo"] = 17;
                Item1["School"] = "Other Activity";
            }

            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                Gv_Profile_Search.Columns[i].Visible = true;
                Gv_Profile_Search.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }


            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            Gv_Profile_Search.DataSource = dt;
            Gv_Profile_Search.DataBind();
            #endregion

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)Gv_Profile_Search.Rows[r].Cells[i].FindControl("lblCol_" + (i + 1)));
                    Label TxtTotla = ((Label)Gv_Profile_Search.Rows[r].Cells[i].FindControl("TxtTotla"));
                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }
                        if (total == 0)
                        {
                        }
                        else
                        {
                            TxtTotla.Text = total.ToString();
                        }
                    }
                }
            }
            Gv_Profile_Search.Rows[7].Visible = false;

            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            if (Gv_Profile_Search.HeaderRow.Cells.Count == 17)
            {
                for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
                {
                    #region ApproveBy
                    var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
            else
            {
                for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
                {
                    #region ApproveBy
                    var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }

        }
        else
        {
            Gv_Profile_Search.DataSource = null;
            Gv_Profile_Search.DataBind();
        }

        //    return;
    }

    public void LoadSearchVillageActivity()
    {
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        //DateTime d1 = Convert.ToDateTime(afromDate);
        //DateTime d2 = Convert.ToDateTime(aToDate);
        //int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        //TimeSpan t = d2 - d1;

        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 7)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
        //    return;
        //}
        string con = " ";
        DataTable dtMain = null;

        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
            //dtMain = objMain.LoadVillageActivtiyCluseterNew(afromDate, aToDate, ddlBlock.SelectedValue, con);
            dtMain = objMain.LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con, "FC", 2);

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            // dtMain = objMain.LoadVillageActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            //dtMain = objMain.LoadVillageActivtiyCluseterNewIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
            dtMain = objMain.LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con, "B", 2);
        }


        int count = 0;

        if (dtMain.Rows.Count > 0)
        {
            btnApprove.Visible = true;

            string strGSS = "Village Count";
            DataRow[] dr = dtMain.Select("Village='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();

                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Village Count";
                Item1["SRNo"] = 1;
            }

            string strGSS3 = "TB Handholding";
            DataRow[] dr3 = dtMain.Select("Village='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "TB Handholding";
                Item1["SRNo"] = 2;
            }

            string strGSS4 = "GSS";
            DataRow[] dr4 = dtMain.Select("Village='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "GSS";
                Item1["SRNo"] = 3;
            }
            string strGSS41 = "MM";
            DataRow[] dr41 = dtMain.Select("Village='" + strGSS41 + "'");
            if (dr41.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "MM";
                Item1["SRNo"] = 4;
            }

            string strGSS5 = "Other Community Meeting 1";
            DataRow[] dr5 = dtMain.Select("Village='" + strGSS5 + "'");
            if (dr5.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other Community Meeting 1";
                Item1["SRNo"] = 5;
            }

            string strGSS56 = "Other Community Meeting 2";
            DataRow[] dr56 = dtMain.Select("Village='" + strGSS56 + "'");
            if (dr56.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other Community Meeting 2";
                Item1["SRNo"] = 6;
            }
            string strGSS562 = "Community Contact";
            DataRow[] dr6 = dtMain.Select("Village='" + strGSS562 + "'");
            if (dr6.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Community Contact";
                Item1["SRNo"] = 7;
            }

            //string strGSS5621 = "Enrollment (6 yrs)";
            //DataRow[] dr61 = dtMain.Select("Village='" + strGSS5621 + "'");
            //if (dr61.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);



            //    Item1["Village"] = "Enrollment (6 yrs)";
            //    Item1["SRNo"] = 8;
            //}
            //string strGSS56211 = "Enrollment (7-14 yrs)";
            //DataRow[] dr611 = dtMain.Select("Village='" + strGSS56211 + "'");
            //if (dr611.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);



            //    Item1["Village"] = "Enrollment (7-14 yrs)";
            //    Item1["SRNo"] = 9;
            //}

            //string strGSS562111 = "Ineligible";
            //DataRow[] dr6111 = dtMain.Select("Village='" + strGSS562111 + "'");
            //if (dr6111.Length > 0)
            //{


            //}
            //else
            //{
            //    DataRow Item1;
            //    Item1 = dtMain.NewRow();
            //    dtMain.Rows.Add(Item1);



            //    Item1["Village"] = "Ineligible";
            //    Item1["SRNo"] = 10;
            //}


            string strGSS1 = "Support";
            DataRow[] dr1 = dtMain.Select("Village='" + strGSS1 + "'");
            if (dr1.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Support";
                Item1["SRNo"] = 11;
            }
            string strGSS11 = "Other Activity";
            DataRow[] dr11 = dtMain.Select("Village='" + strGSS11 + "'");
            if (dr11.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other Activity";
                Item1["SRNo"] = 12;
            }


            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                gvVillageActivity.Columns[i].Visible = true;
                gvVillageActivity.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }


            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            gvVillageActivity.DataSource = dt;
            gvVillageActivity.DataBind();

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)gvVillageActivity.Rows[r].Cells[i].FindControl("lblColV_" + (i + 1)));
                    Label TxtTotla = ((Label)gvVillageActivity.Rows[r].Cells[i].FindControl("TxtTotlaV"));
                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }
                        if (total == 0)
                        {
                        }
                        else
                        {
                            TxtTotla.Text = total.ToString();
                        }
                    }
                }
            }

            gvVillageActivity.Rows[9].Visible = false;
            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;
            if (gvVillageActivity.HeaderRow.Cells.Count == 17)
            {
                for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
            else
            {
                for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
            //for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count; Index++)
            //{
            //    #region ApproveBy
            //    var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

            //    #endregion
            //    firstCell.Controls.Clear();

            //    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            //}
        }
        else
        {
            gvVillageActivity.DataSource = null;
            gvVillageActivity.DataBind();
        }

    }



    public void LoadSearchOfficeActivtiy()
    {
        Session["dt"] = null;

        //if (ddlBlock.SelectedIndex <= 0)
        //{

        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select User')</script>", false);
        //    return;
        //}


        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];


        //DateTime d1 = Convert.ToDateTime(afromDate);
        //DateTime d2 = Convert.ToDateTime(aToDate);
        //int month = Convert.ToInt32(c[1]) - Convert.ToInt32(d[1]);
        //TimeSpan t = d2 - d1;

        //double Days = Convert.ToDouble(t.TotalDays);
        //if (Math.Sign(Days) == -1)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Vaild Date')</script>", false);
        //    return;
        //}
        //if (Math.Round(Days) >= 7)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Date rang 7 Day')</script>", false);
        //    return;
        //}
        string con = " ";
        DataTable dtMain = null;

        if (Session["user_level"].ToString() == "19")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='FC'  and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
            //  dtMain = objMain.LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con,3);
            dtMain = objMain.LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con, "", 3);

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B'  and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            dtMain = objMain.LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con, "", 3);
            // dtMain = objMain.LoadActivtiyAllClusterWise(afromDate, aToDate, ddlBlock.SelectedValue, con,3);
        }
        int count = 0;

        if (dtMain.Rows.Count > 0)
        {
            btnApprove.Visible = true;

            string strGSSVillage = "Village Count";
            DataRow[] drGSSVillage = dtMain.Select("Village='" + strGSSVillage + "'");
            if (drGSSVillage.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Village Count";
                Item1["SRNo"] = "1";

            }



            string strGSS = "Meeting";
            DataRow[] dr = dtMain.Select("Village='" + strGSS + "'");
            if (dr.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Meeting";
                Item1["SRNo"] = "2";

            }

            string strGSS3 = "Other_specify";
            DataRow[] dr3 = dtMain.Select("Village='" + strGSS3 + "'");
            if (dr3.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Other_specify";
                Item1["SRNo"] = "4";
            }

            string strGSS4 = "Training";
            DataRow[] dr4 = dtMain.Select("Village='" + strGSS4 + "'");
            if (dr4.Length > 0)
            {


            }
            else
            {
                DataRow Item1;
                Item1 = dtMain.NewRow();
                dtMain.Rows.Add(Item1);



                Item1["Village"] = "Training";
                Item1["SRNo"] = "3";
            }

            //string strGSS5 = "Other Community Meeting";


            for (int i = 2; i < dtMain.Columns.Count; i++)
            {
                gvOffice.Columns[i].Visible = true;
                gvOffice.Columns[i].HeaderText = dtMain.Columns[i].ColumnName;
            }

            DataView dataview = dtMain.DefaultView;
            dataview.Sort = "SRNo";
            DataTable dt = dataview.ToTable();
            gvOffice.DataSource = dt;
            gvOffice.DataBind();

            for (int r = 0; r < dt.Rows.Count; r++)
            {
                decimal total = 0;
                for (int i = 2; i < dt.Columns.Count; i++)
                {
                    Label lbl = ((Label)gvOffice.Rows[r].Cells[i].FindControl("lblColO_" + (i + 1)));
                    Label TxtTotla = ((Label)gvOffice.Rows[r].Cells[i].FindControl("TxtTotlaO"));
                    if (lbl != null)
                    {
                        lbl.Text = Convert.ToString(dt.Rows[r][i]);
                        if (lbl.Text != "")
                        {
                            total += Convert.ToDecimal(lbl.Text);
                        }
                        if (total == 0)
                        {
                        }
                        else
                        {
                            TxtTotla.Text = total.ToString();
                        }
                    }
                }
            }


            // gvVillageActivity.HeaderRow.Cells[count].Text = "T.B.Hand Holding";
            DataRow[] drApp = null;

            gvOffice.Rows[4].Visible = false;
            if (gvOffice.HeaderRow.Cells.Count == 17)
            {
                for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvOffice.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
            else
            {
                for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
                {
                    #region ApproveBy
                    var firstCell = gvOffice.HeaderRow.Cells[Index];

                    #endregion
                    firstCell.Controls.Clear();

                    firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

                }
            }
        }
        else
        {
            gvOffice.DataSource = null;
            gvOffice.DataBind();
        }

    }
    //protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string strQry = "";
    //    if (ddlBlock.SelectedIndex > 0)
    //    {
    //        strQry = "   select Villagecode  from MstUser   where UserName='" + ddlBlock.SelectedValue + "' ";
    //        DataTable dtUserVillage = objMain.LoadData(strQry);

    //        string strVillage = dtUserVillage.Rows[0]["Villagecode"].ToString();

    //        conditions = "mst5Village.VillageCode in(" + strVillage + ") ";

    //     //   objComman.BindDLL("mst5Village", "VillageCode,VillageName ", conditions, "", "", ddlVilage, "VillageName", "VillageCode", "Select");


    //    }
    //}


    protected void TestGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string quantity = e.Row.Cells[3].Text;

            //foreach (TableCell cell in e.Row.Cells)
            //{

            //        cell.BackColor = Color.Red;

            //}
        }
    }
    //protected void Gv_Profile_Search_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "GVUIO")
    //    {
    //        int iIndex = Convert.ToInt32(e.CommandArgument);
    //        string VDate = Gv_Profile_Search.DataKeys[iIndex]["VDate"].ToString();
    //        Response.Redirect("./frmMobileVillageProfile.aspx?ID=" + ddlVilage.SelectedValue + "," + ddlBlock.SelectedValue + "," + VDate + "");
    //    }

    protected void LnkSchool_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;

        string con1 = "";
        string con2 = "";
        string UniqueCode = (gvr.FindControl("lblUn1") as Label).Text;

        if (UniqueCode == "GKP")
        {
            con2 = " and  LEN(LevelID) >0   ";
        }

        if (UniqueCode == "SIP Annual")
        {
            con1 = " and tblActivityUpdate_School.SIP_Annual>0 ";
        }
        else if (UniqueCode == "Retention")
        {
            con1 = " and tblActivityUpdate_School.Retention_Annual>0 ";
        }

        else if (UniqueCode == "SMC Meeting")
        {
            con1 = " and  tblActivityUpdate_School.SMC_Meeting >0 ";
        }
        else if (UniqueCode == "SAC Quarter Update")
        {
            con1 = " and  tblActivityUpdate_School.SACUpdate >0  ";
        }
        else if (UniqueCode == "School infra update")
        {
            con1 = " and  tblActivityUpdate_School.Infrastructure >0  ";
        }
        else if (UniqueCode == "Bal Sabha")
        {
            con1 = " and  tblActivityUpdate_School.BalSabha >0  ";
        }
        else if (UniqueCode == "School Contact")
        {
            con1 = " and    len(SchoolContactOption)>0   ";
        }
        //else if (UniqueCode == "Life Skill Game 2")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%2%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 3")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%3%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 4")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%4%' and  Lifeskill_Games>0  ";
        //}
        //else if (UniqueCode == "Life Skill Game 5")
        //{
        //    con1 = " and   LifeSkillGameEntry like '%5%' and  Lifeskill_Games>0  ";
        //}
        else if (UniqueCode == "CLT")
        {
            con1 = "and  tblActivityUpdate_School.CLT>0  ";
        }
        else if (UniqueCode == "Learning Baseline")
        {
            con1 = " and  CLT_Pretest>0 ";
        }
        else if (UniqueCode == "Learning Midline" || UniqueCode == "Learning  Midline")
        {
            con1 = "  and    CTL_Midtest>0 ";
        }
        else if (UniqueCode == "Learning Endline" || UniqueCode == "Learning  Endline")
        {
            con1 = " and  CLT_Posttest>0";
        }
        else if (UniqueCode == "Other Activity")
        {
            con1 = "  and    len(Others_Description)>0 ";
        }
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];
        string con;


        DataTable dtMain = null;
        if (Session["user_level"].ToString() == "19")
        {
            if (con2.Length > 0)
            {
                con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "'  and ApproveStatus='FC' and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
                dtMain = objMain.GetGKPWiseActivity(con + con2);

            }
            else
            {

                con = " where ActivityDate between('" + afromDate + "') and  '" + aToDate + "' and   ApproveStatus='FC' and UserEntry=2 and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
                dtMain = objMain.GetSchoolActivtiy(con + con1);
            }

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            if (con2.Length > 0)
            {
                con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B'  and mst5village.BlockCode='" + ddlBlock.SelectedValue + "'  ";
                dtMain = objMain.GetGKPWiseActivity(con + con2);
            }
            else
            {
                con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B' and UserEntry=3 and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
                dtMain = objMain.GetSchoolActivtiy(con + con1);
            }
            // dtMain = objMain.LoadSchoolActivtiyCluseterIO(afromDate, aToDate, ddlBlock.SelectedValue, con);
        }

        if (dtMain.Rows.Count > 0)
        {
            gvVillageWise.DataSource = dtMain;
            gvVillageWise.DataBind();
        }
        else
        {
            gvVillageWise.DataSource = null;
            gvVillageWise.DataBind();
        }
        gvVillageDeatial.Visible = false;
        gvVillageWise.Visible = true;
        gvVillageOffice.Visible = false;
        if (Gv_Profile_Search.Rows.Count > 0)
        {
            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }

        ModalPopupExtender.Show();
    }
    protected void LnkVillage_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string con1 = "";
        Int32 Flag = 1;
        string UniqueCode = (gvr.FindControl("lblvllV_2") as Label).Text;
        if (UniqueCode == "TB Handholding")
        {
            con1 = " and TBHandholding >0 ";
        }
        else if (UniqueCode == "GSS")
        {
            con1 = "and  len(GSS_Agenda)>0   and GSSEnrollHault=1  ";
        }
        else if (UniqueCode == "MM")
        {
            con1 = " and  MM_Mtg>0  ";
        }
        else if (UniqueCode == "Other Community Meeting 1")
        {
            con1 = " and  Com_mtg>0  ";
        }
        else if (UniqueCode == "Other Community Meeting 2")
        {
            con1 = " and  Com_mtg2>0   ";
        }
        else if (UniqueCode == "Community Contact")
        {
            con1 = " and  ComContact>0  ";
        }



        else if (UniqueCode == "Enrollment (6 yrs)")
        {
            con1 = " and  ActivityStatus=5  and AgeAson =6";
            Flag = 2;
        }
        else if (UniqueCode == "Enrollment (7-14 yrs)")
        {
            con1 = "and    ActivityStatus=5  and AgeAson >=7 and AgeAson <=14   ";
            Flag = 2;
        }
        else if (UniqueCode == "Ineligible")
        {
            con1 = "and    ActivityStatus=3    ";
            Flag = 3;
        }

        else if (UniqueCode == "Support")
        {
            con1 = " and    Support>0  ";
        }
        else if (UniqueCode == "Other Activity")
        {
            con1 = " and    len(Others_Desc)>1   ";
        }

        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];
        string con;


        DataTable dtMain = null;
        if (Session["user_level"].ToString() == "19")
        {
            con = " where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC' and UserEntry=2 and mst5village.BlockCode='" + Session["NewBlockCode"].ToString() + "' ";
            string d2d = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + ddlBlock.SelectedValue + "'";
            if (Flag == 1)
            {
                dtMain = objMain.GeVillageActivtiy(con + con1, Flag);
            }
            else
            {
                dtMain = objMain.GeVillageActivtiy(d2d + con1, Flag);
            }

        }
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B' and UserEntry=3 and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";
            string d2d = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and mst5village.BlockCode='" + ddlBlock.SelectedValue + "'";
            if (Flag == 1)
            {
                dtMain = objMain.GeVillageActivtiy(con + con1, Flag);
            }
            else
            {
                dtMain = objMain.GeVillageActivtiy(d2d + con1, Flag);
            }
        }

        if (dtMain.Rows.Count > 0)
        {
            gvVillageDeatial.DataSource = dtMain;
            gvVillageDeatial.DataBind();
        }
        else
        {
            gvVillageDeatial.DataSource = null;
            gvVillageDeatial.DataBind();

        }

        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {

            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        gvVillageOffice.Visible = false;
        gvVillageDeatial.Visible = true;
        gvVillageWise.Visible = false;
        ModalPopupExtender.Show();
    }
    protected void LnkOffice_OnClick(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueCode = (gvr.FindControl("lbooff") as Label).Text;
        string con1 = "";
        string con = "";
        DataTable dtMain = null;
        if (UniqueCode == "Meeting")
        {
            con1 = " and Meeting>0  ";
        }
        else if (UniqueCode == "Training")
        {
            con1 = "and Training>0  ";
        }
        else if (UniqueCode == "Other Activity")
        {
            con1 = " and   Other_FC>0  ";
        }
        string fromDate = TxtFromDate.Text;
        string[] d = fromDate.Split('/');
        string afromDate = d[2] + '-' + d[1] + '-' + d[0];

        string ToDate = txtDate.Text;
        string[] c = ToDate.Split('/');
        string aToDate = c[2] + '-' + c[1] + '-' + c[0];
        if (Session["user_level"].ToString() == "39" || Session["user_level"].ToString() == "30" || Session["user_level"].ToString() == "145")
        {
            con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='B' and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";

            dtMain = objMain.GetOfficeWiseActivity(con + con1);

        }
        if (Session["user_level"].ToString() == "19")
        {
            con = "where ActivityDate between('" + afromDate + "') and '" + aToDate + "' and ApproveStatus='FC' and mst5village.BlockCode='" + ddlBlock.SelectedValue + "' ";

            dtMain = objMain.GetOfficeWiseActivity(con + con1);

        }
        if (dtMain.Rows.Count > 0)
        {
            gvVillageOffice.DataSource = dtMain;
            gvVillageOffice.DataBind();
        }
        else
        {
            gvVillageOffice.DataSource = null;
            gvVillageOffice.DataBind();


        }
        if (Gv_Profile_Search.Rows.Count > 0)
        {

            for (int Index = 1; Index < Gv_Profile_Search.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = Gv_Profile_Search.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvVillageActivity.Rows.Count > 0)
        {

            for (int Index = 1; Index < gvVillageActivity.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvVillageActivity.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        if (gvOffice.Rows.Count > 0)
        {
            for (int Index = 1; Index < gvOffice.HeaderRow.Cells.Count - 1; Index++)
            {
                #region ApproveBy
                var firstCell = gvOffice.HeaderRow.Cells[Index];

                #endregion
                firstCell.Controls.Clear();

                firstCell.Controls.Add(new HyperLink { NavigateUrl = "./FrmActivityDatewiseSearch.aspx?ID=" + firstCell.Text + "," + TxtFromDate.Text + "," + txtDate.Text + "", Text = firstCell.Text });

            }
        }
        gvVillageOffice.Visible = true;
        gvVillageDeatial.Visible = false;
        gvVillageWise.Visible = false;
        ModalPopupExtender.Show();
    }
}

