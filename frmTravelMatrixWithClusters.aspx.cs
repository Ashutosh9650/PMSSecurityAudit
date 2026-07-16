using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class frmTravelMatrixWithClusters : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                clsMain.TraveUserID = Convert.ToString(Session["username"]);
                if (Request.QueryString["ID"] != null)
                {
                    divcityType.Visible = false;
                    // divcityMeal.Visible = false;
                    // divDim.Visible = false;
                    //divExpense.Visible = false;
                    divExpense.Attributes.Add("style", "display:none;");
                    divMode.Visible = false;
                    div1.Visible = false;
                    div2.Visible = false;
                    chkENtry.Checked = false;
                    string QueryString = Request.QueryString["ID"];
                    string[] a = QueryString.Split(',');
                    string Fdate = "";
                    string Tdate = "";
                    int mMonth = 0;
                    if (a[3] == "1")
                    {
                        lblyear.Text = a[6];
                    }
                    else
                    {
                        lblyear.Text = a[5];
                    }
                    if (a[3] == "1")
                    {
                        string kdate = "";
                        string tkdate = "";
                        kdate = a[4].ToString();
                        tkdate = a[5].ToString();

                        CalendarExtenderTourdate.StartDate = Convert.ToDateTime(kdate).AddDays(0);
                        CalendarExtenderTourdate.EndDate = Convert.ToDateTime(tkdate).AddDays(0);

                    }
                    if (a[1] == "1")
                    {
                        mMonth = 12;
                    }
                    else
                    {
                        mMonth = Convert.ToInt32(a[1]) - 1;
                    }
                    if (a[1] == "2")
                    {
                        Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                        Tdate = DateTime.Now.Year + 1 + "-" + a[1] + "-" + "20";
                    }
                    else if (a[1] == "3")
                    {
                        Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                        Tdate = DateTime.Now.Year + 1 + "-" + a[1] + "-" + "31";
                    }
                    else if (a[1] == "4")
                    {
                        Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "01";
                        Tdate = DateTime.Now.Year + "-" + a[1] + "-" + "20";
                    }
                    else if (a[1] == "1")
                    {
                        Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                        Tdate = DateTime.Now.Year + 1 + "-" + a[1] + "-" + "20";
                    }
                    else
                    {
                        Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                        Tdate = DateTime.Now.Year + "-" + a[1] + "-" + "20";
                    }

                    Session["Tcluser"] = a[0];
                    Session["TMonth"] = a[1];
                    Session["FC"] = a[2];
                    Session["EndCreateDataVillage"] = null;
                    Session["StartCreateDataVillage"] = null;
                    Session["dtExpense"] = null;
                    Session["dtExpensevehicle"] = null;
                    FillCVillage(a[0]);
                    txtDate.Enabled = true;
                    clsMain.TraveGustHouseImageID = "";
                    clsMain.TravelImageID = "";
                    hndMaxamt.Value = "0";
                    lblUniqueCodeEx.Value = "";
                    lblImagePathEx.Text = "";

                    lblUniqueCodeVe.Value = "";
                    lblImagePathVe.Text = ""; ;
                    if (a[3] == "2")
                    {
                        string strQry2 = " Select * FROM [tblTravelMatrixDeatils2024] where [UniqueCode]='" + a[4] + "' ";
                        DataTable dtSer = objMain.LoadData(strQry2);
                        if (dtSer.Rows.Count > 0)
                        {
                            ddlType.SelectedValue = dtSer.Rows[0]["VisitType"].ToString();
                            ddlType_SelectedIndexChanged(ddlType, null);
                            txtSTime.Enabled = false;
                            ddlType.Enabled = false;
                            txtTTime.Enabled = false;
                            DateTime sDate = Convert.ToDateTime(dtSer.Rows[0]["TravelDate"].ToString());
                            txtDate.Text = sDate.ToString("dd/MM/yyy");
                            if (a[1] == "2")
                            {

                                Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                                Tdate = DateTime.Now.Year + 1 + "-" + a[1] + "-" + "20";
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Fdate))
                                {
                                    txtSTime.Enabled = true;
                                }
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Tdate))
                                {
                                    txtTTime.Enabled = true;
                                }
                            }

                            else if (a[1] == "3")
                            {

                                Fdate = DateTime.Now.Year + 1 + "-" + mMonth + "-" + "21";
                                Tdate = DateTime.Now.Year + 1 + "-" + a[1] + "-" + "31";
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Fdate))
                                {
                                    txtSTime.Enabled = true;
                                }
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Tdate))
                                {
                                    txtTTime.Enabled = true;
                                }
                            }
                            else if (a[1] == "4")
                            {
                                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "01";
                                Tdate = DateTime.Now.Year + "-" + a[1] + "-" + "20";
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Fdate))
                                {
                                    txtSTime.Enabled = true;
                                }
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Tdate))
                                {
                                    txtTTime.Enabled = true;
                                }
                            }
                            else if (a[1] == "1")
                            {
                                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                                Tdate = DateTime.Now.Year + 1 + "-" + a[1] + "-" + "20";
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Fdate))
                                {
                                    txtSTime.Enabled = true;
                                }
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Tdate))
                                {
                                    txtTTime.Enabled = true;
                                }
                            }
                            else
                            {
                                Fdate = DateTime.Now.Year + "-" + mMonth + "-" + "21";
                                Tdate = DateTime.Now.Year + "-" + a[1] + "-" + "20";
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Fdate))
                                {
                                    txtSTime.Enabled = true;
                                }
                                if (Convert.ToDateTime(sDate) == Convert.ToDateTime(Tdate))
                                {
                                    txtTTime.Enabled = true;
                                }
                            }
                            if (Convert.ToString(Session["user_level"]) == "19")
                            {
                                if (Convert.ToInt32(Session["Status"]) > 1)
                                {
                                    Button2.Visible = false;
                                    gvExpens.Columns[3].Visible = false;
                                    gvExpens.Columns[4].Visible = false;
                                    gvVehicle.Columns[4].Visible = false;
                                    gvVehicle.Columns[5].Visible = false;
                                }
                                else
                                {
                                    gvExpens.Columns[3].Visible = true;
                                    gvExpens.Columns[4].Visible = true;
                                    gvVehicle.Columns[4].Visible = true;
                                    gvVehicle.Columns[5].Visible = true;
                                    Button2.Visible = true;
                                }

                            }
                            if (Convert.ToString(Session["user_level"]) == "123" || Convert.ToString(Session["user_level"]) == "147")
                            {
                                if (Convert.ToInt32(Session["Status"]) > 2)
                                {
                                    gvExpens.Columns[3].Visible = false;
                                    gvExpens.Columns[4].Visible = false;
                                    gvVehicle.Columns[4].Visible = false;
                                    gvVehicle.Columns[5].Visible = false;
                                    Button2.Visible = false;
                                }
                                else
                                {
                                    gvVehicle.Columns[4].Visible = false;
                                    gvVehicle.Columns[5].Visible = false; gvExpens.Columns[3].Visible = true;
                                    gvExpens.Columns[4].Visible = true;
                                    gvVehicle.Columns[4].Visible = true;
                                    gvVehicle.Columns[5].Visible = true;
                                    Button2.Visible = true;
                                }

                            }


                            txtSTime.Enabled = true;
                            txtTTime.Enabled = true;
                            txtDate.Enabled = false;
                            ddlFromVillage.SelectedValue = dtSer.Rows[0]["FromVillageCode"].ToString();
                            ddlEndVillage.SelectedValue = dtSer.Rows[0]["ToVillageCode"].ToString();
                            txtSTime.Text = dtSer.Rows[0]["LoginTime"].ToString();
                            txtTTime.Text = dtSer.Rows[0]["LogoutTime"].ToString();
                            txtObjective.Text = dtSer.Rows[0]["Objective"].ToString();
                            txtKM.Text = dtSer.Rows[0]["KMAdmin"].ToString();
                            txtTotalFare.Text = dtSer.Rows[0]["TotalAmountAdmin"].ToString();
                            if (Convert.ToString(Session["user_level"]) == "19")
                            {
                                txtRemark.Text = dtSer.Rows[0]["Remarks"].ToString();
                            }
                            else
                            {
                                if (dtSer.Rows[0]["LockFlag"].ToString() == "1")
                                {
                                    txtRemark.Text = "";
                                }
                                else
                                {
                                    txtRemark.Text = dtSer.Rows[0]["RemarksAdmin"].ToString();
                                }
                            }
                            lblEditUUniqecode.Text = a[4];

                            if (Convert.ToInt32(ddlType.SelectedValue) == 2)
                            {
                                SqlParameter[] parm1 = new SqlParameter[]
                                  {

                                         new SqlParameter("@UniqueCode", a[4] ),


                                  };


                                DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatilAddvillage", parm1);
                                if (dt.Rows.Count > 0)
                                {
                                    lbllblVillageStart.Text = dt.Rows[0]["FromVillage"].ToString();
                                    lblVillageEnd.Text = dt.Rows[0]["ToVillage"].ToString();
                                }
                                ddlMode.SelectedValue = dtSer.Rows[0]["TravelMode"].ToString();
                                Mode_SelectedIndexChanged(ddlMode, null);
                                txtTotalFare.Text = dtSer.Rows[0]["TotalAmountAdmin"].ToString();
                                txtKM.Text = dtSer.Rows[0]["KMAdmin"].ToString();
                                rblDist.SelectedValue = dtSer.Rows[0]["Useofaccommodation"].ToString();
                                rblDist_SelectedIndexChanged(rblDist, null);
                                ddlgusttype.SelectedValue = dtSer.Rows[0]["GuestHouseType"].ToString();
                                ddlgusttype_SelectedIndexChanged(ddlgusttype, null);
                                ddlPayment.SelectedValue = dtSer.Rows[0]["PaymentType"].ToString();

                                if (Convert.ToInt32(ddlPayment.SelectedValue) == 1)
                                {
                                    ddlOccupancy.Enabled = true;
                                    txthoserent.Enabled = true;
                                    Fileupload1.Enabled = true;
                                }

                                if (Convert.ToInt32(ddlPayment.SelectedValue) == 2)
                                {
                                    ddlOccupancy.Enabled = false;
                                    txthoserent.Enabled = false;
                                    Fileupload1.Enabled = false;
                                }
                                ddlOccupancy.SelectedValue = dtSer.Rows[0]["Occupancy"].ToString();
                                txthoserent.Text = dtSer.Rows[0]["GuestHouseRentAdmin"].ToString();
                                rblDist1.SelectedValue = dtSer.Rows[0]["Useoflocalvehicle"].ToString();
                                rblDist1_SelectedIndexChanged(rblDist, null);

                                clsMain.TraveGustHouseImageID = dtSer.Rows[0]["GuestreceiptImage"].ToString();
                                clsMain.TravelImageID = dtSer.Rows[0]["ExpensereceiptImage"].ToString();
                                if (dtSer.Rows[0]["ExpensereceiptImage"].ToString().Length > 5)
                                {
                                    lnkMain.Visible = true;
                                }
                                else
                                {
                                    lnkMain.Visible = false;
                                }
                                if (dtSer.Rows[0]["GuestreceiptImage"].ToString().Length > 5)
                                {
                                    ImageButton1.Visible = true;
                                }
                                else
                                {
                                    ImageButton1.Visible = false;
                                }
                                if (dtSer.Rows[0]["isperdim"].ToString() == "1")
                                {
                                    chkENtry.Checked = true;
                                }
                                ddlcity.SelectedValue = dtSer.Rows[0]["CityType"].ToString();
                                ddlCite_SelectedIndexChanged(ddlcity, null);

                                ddlMealArrangement.SelectedValue = dtSer.Rows[0]["Arrangementby"].ToString();
                                txtPerDim.Text = dtSer.Rows[0]["PerdimAdmin"].ToString();
                                DataTable StartCreateDataVillage = StartCreateDataVillagedt();

                                if (dtSer.Rows[0]["isperdimApply"].ToString() == "1")
                                {
                                    divcityType.Visible = true;
                                }
                                else
                                {
                                    divcityType.Visible = false;
                                }
                                DataRow dr = null;
                                dr = StartCreateDataVillage.NewRow();
                                dr["TypeID"] = dtSer.Rows[0]["StartVillageFlag"].ToString();
                                dr["Dist"] = dtSer.Rows[0]["StartVillageDist"].ToString();
                                dr["Block"] = dtSer.Rows[0]["StartVillageBlock"].ToString();
                                dr["Cluster"] = dtSer.Rows[0]["StartVillageCluster"].ToString();
                                dr["Village"] = dtSer.Rows[0]["StartVillageOutside"].ToString();
                                dr["Other"] = dtSer.Rows[0]["SOtherplace"].ToString();

                                dr["Desc"] = dtSer.Rows[0]["SOtherDesc"].ToString();
                                dr["FromTierType"] = dtSer.Rows[0]["FromTierType"].ToString();
                                //    lbllblVillageStart.Text = ddl_V.SelectedItem.Text;
                                StartCreateDataVillage.Rows.Add(dr);
                                Session["StartCreateDataVillage"] = StartCreateDataVillage;

                                DataTable EndCreateDataVillage = EndCreateDataVillagedt();
                                string strQry7 = " Select * FROM [tblTravelMatrixPerDiem] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "'";
                                DataTable dtDim = objMain.LoadData(strQry7);
                                if (dtDim.Rows.Count > 0)
                                {
                                    lblPerDim.Text = dtDim.Rows[0]["TotalHours"].ToString();

                                }



                                DataRow dr1 = null;
                                dr1 = EndCreateDataVillage.NewRow();
                                dr1["TypeID"] = dtSer.Rows[0]["EndVillageFlag"].ToString();
                                dr1["Dist"] = dtSer.Rows[0]["EndVillageDist"].ToString();
                                dr1["Block"] = dtSer.Rows[0]["EndVillageBlock"].ToString();
                                dr1["Cluster"] = dtSer.Rows[0]["EndVillageCluster"].ToString();
                                dr1["Village"] = dtSer.Rows[0]["EndVillageOutside"].ToString();
                                dr1["Other"] = dtSer.Rows[0]["EOtherplace"].ToString();

                                dr1["Desc"] = dtSer.Rows[0]["EOtherDesc"].ToString();
                                dr1["ToTierType"] = dtSer.Rows[0]["ToTierType"].ToString();

                                EndCreateDataVillage.Rows.Add(dr1);

                                if (Convert.ToInt32(ddlcity.SelectedValue) == 2)
                                {
                                    if (dtSer.Rows[0]["EndVillageFlag"].ToString() == "7")
                                    {
                                        ddlcity.Enabled = true;
                                    }

                                }
                                //  lblVillageEnd.Text = ddl_V.SelectedItem.Text;

                                Session["EndCreateDataVillage"] = EndCreateDataVillage;

                            }

                        }
                        string strQry3 = " Select  UniqueCode	,UniqueChildRCode	,TotalAmountAdmin TotalAmount ,ImagePath, Expensedetails  FROM [tblTravelMatrixExpens] where [UniqueChildRCode]='" + a[4] + "' and Flag=1 and deleteFlag=1 ";
                        DataTable dtSerExpens = objMain.LoadData(strQry3);
                        if (dtSerExpens.Rows.Count > 0)
                        {
                            gvExpens.DataSource = dtSerExpens;
                            gvExpens.DataBind();
                            Session["dtExpense"] = dtSerExpens;
                        }
                        else
                        {
                            gvExpens.DataSource = null;
                            gvExpens.DataBind();
                            Session["dtExpense"] = null;
                        }
                        string strQry4 = " Select UniqueCode	,UniqueChildRCode	,TotalAmountAdmin  VehicleAmout,ImagePath, Expensedetails VehicleDescription, Description VehicletypeName,Vehicletype as VehicletypeID   FROM [tblTravelMatrixExpens] left join mstlookup on mstLookup.LookupCode=Vehicletype and mstLookup.Language=0 and mstLookup.LookupFlag='T8' where [UniqueChildRCode]='" + a[4] + "' and Flag=2 and deleteFlag=1 ";
                        DataTable dtSerVi = objMain.LoadData(strQry4);
                        if (dtSerVi.Rows.Count > 0)
                        {
                            gvVehicle.DataSource = dtSerVi;
                            gvVehicle.DataBind();
                            Session["dtExpensevehicle"] = dtSerVi;
                        }
                        else
                        {
                            gvVehicle.DataSource = null;
                            gvVehicle.DataBind();
                            Session["dtExpensevehicle"] = null;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }
        }

        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);

        //   ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction1();", true);

    }
    protected void ddlend_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry5 = " ";
        txtKM.Text = "";
        txtTotalFare.Text = "";
        if (ddlFromVillage.SelectedIndex >= 0 && ddlEndVillage.SelectedIndex >= 0)
        {
            strQry5 = "  select * from rptTravelFare  where aVillageCode='" + ddlFromVillage.SelectedValue + "' and bVillageCode='" + ddlEndVillage.SelectedValue + "' ";
            DataTable dtMax = objMain.LoadData(strQry5);
            if (dtMax.Rows.Count > 0 && Convert.ToInt32(dtMax.Rows[0]["KM"]) > 0)
            {
                txtKM.Text = dtMax.Rows[0]["KM"].ToString();
                txtTotalFare.Text = (Convert.ToInt32(dtMax.Rows[0]["KM"]) * 4).ToString();
            }

        }

    }
    protected void ddlStart_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry5 = " ";
        txtKM.Text = "";
        txtTotalFare.Text = "";
        if (ddlFromVillage.SelectedIndex >= 0 && ddlEndVillage.SelectedIndex >= 0)
        {
            strQry5 = "  select * from rptTravelFare  where aVillageCode='" + ddlFromVillage.SelectedValue + "' and bVillageCode='" + ddlEndVillage.SelectedValue + "' ";
            DataTable dtMax = objMain.LoadData(strQry5);
            if (dtMax.Rows.Count > 0 && Convert.ToInt32(dtMax.Rows[0]["KM"]) > 0)
            {
                txtKM.Text = dtMax.Rows[0]["KM"].ToString();
                txtTotalFare.Text = (Convert.ToInt32(dtMax.Rows[0]["KM"]) * 4).ToString();
            }
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {


        divcityType.Visible = false;
        //divcityMeal.Visible = false;
        //  divDim.Visible = false;
        //divExpense.Visible = false;
        divExpense.Attributes.Add("style", "display:none;");
        divMode.Visible = false;
        div1.Visible = false;
        div2.Visible = false;
        btnStart.Visible = false;
        btnend.Visible = false;
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {

            divcityType.Visible = true;
            //divcityMeal.Visible = true;
            //  divDim.Visible = true;
            //divExpense.Visible = true;

            divExpense.Attributes.Add("style", "display:block;");
            divMode.Visible = true;
            div1.Visible = true;
            div2.Visible = true;
            ddlFromVillage.Visible = false;
            ddlEndVillage.Visible = false;
            lblVillageEnd.Visible = true;
            lbllblVillageStart.Visible = true;
            txtTotalFare.Enabled = false;
            txtKM.Enabled = false;
            btnStart.Visible = true;
            btnend.Visible = true;

        }
        else
        {
            txtTotalFare.Enabled = false;
            ddlFromVillage.Visible = true;
            ddlEndVillage.Visible = true;
            lblVillageEnd.Visible = false;
            lbllblVillageStart.Visible = false;
            btnStart.Visible = false;
            btnend.Visible = false;
            txtKM.Enabled = false;
        }
        DataClear();

    }
    protected void Mode_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtKM.Text = "";
        txtTotalFare.Text = "";
        gvVehicle.DataSource = null;
        gvVehicle.DataBind();
        txtKM.Enabled = false;
        //  divExpense.Visible = false;
        divExpense.Attributes.Add("style", "display:none;");
        txtTotalFare.Enabled = false;
        Session["dtExpense"] = null;
        rblDist1.ClearSelection();
        if (lblEditUUniqecode.Text.Length > 4)
        {

        }
        else
        {
            if (lbllblVillageStart.Text == "" || lblVillageEnd.Text == "")
            {
                ddlMode.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Start Village and End Village')</script>", false);
                return;

            }
            else
            {
                if (lbllblVillageStart.Text.ToLower() == lblVillageEnd.Text.ToLower())
                {
                    if (Convert.ToInt32(ddlMode.SelectedValue) == 3 || Convert.ToInt32(ddlMode.SelectedValue) == 4)
                    {
                        ddlMode.SelectedIndex = 0;
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Start Village and End Village Same You Can Not Select Train and Flight')</script>", false);
                        return;
                    }
                }
            }
        }
        if (Convert.ToInt32(ddlMode.SelectedValue) == 1)
        {
            divExpense.Attributes.Add("style", "display:none;");
            //  divExpense.Visible = false;
            pnlvMain.Visible = false;
            txtKM.Enabled = true;
            txtTotalFare.Enabled = false;
        }
        if (Convert.ToInt32(ddlMode.SelectedValue) == 5)
        {
            pnlvMain.Visible = true;
            divExpense.Visible = true;
            txtTotalFare.Enabled = false;
            txtKM.Enabled = false;
        }
        if (Convert.ToInt32(ddlMode.SelectedValue) == 2 || Convert.ToInt32(ddlMode.SelectedValue) == 3 || Convert.ToInt32(ddlMode.SelectedValue) == 4)
        {
            pnlvMain.Visible = true;
            // divExpense.Visible = true;
            divExpense.Attributes.Add("style", "display:block;");
            txtTotalFare.Enabled = true;
            txtKM.Enabled = false;
        }

    }
    public void DataClear()
    {
        txtDate.Text = "";
        txtSTime.Text = "";
        txtTTime.Text = "";
        ddlFromVillage.SelectedIndex = 0;
        ddlEndVillage.SelectedIndex = 0;
        ddlMode.SelectedIndex = 0;

        txtObjective.Text = "";
        txtKM.Text = "";
        txtTotalFare.Text = "";
        txtRemark.Text = "";
        txtPerDim.Text = "";
        ddlcity.SelectedIndex = 0;
        ddlMealArrangement.SelectedIndex = 0;
        rblDist.ClearSelection();
        rblDist1.ClearSelection();
        ddlgusttype.SelectedIndex = 0;
        ddlPayment.SelectedIndex = 0;
        ddlOccupancy.SelectedIndex = 0;

        ddlvehicle.SelectedIndex = 0;
        txthoserent.Text = "";
        txtdes.Text = "";
        txtVIcRent.Text = "";
        txtTrotalAmout.Text = "";
        txtExpense.Text = "";
        gvVehicle.DataSource = null;
        gvVehicle.DataBind();
        Session["dtExpensevehicle"] = null;
        gvExpens.DataSource = null;
        gvExpens.DataBind();
        Session["dtExpense"] = null;
        Session["EndCreateDataVillage"] = null;
        Session["StartCreateDataVillage"] = null;
    }
    public void FillCVillage(string ClusterCode)
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


        conditions = "mst5Village.ClusterCOde ='" + ClusterCode + "'    ";

        DataTable dtVillage = null;

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village with(nolock) INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        dtVillage = objMain.LoadData(strQry);
        DataTable dtCopy = dtVillage.Copy();
        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlFromVillage, "VillageName", "VillageCode", "Select");


        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtCopy, conditions, "VillageName", "asc", ddlEndVillage, "VillageName", "VillageCode", "Select");

        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T1' and Language=0", "description", "asc", ddlType, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T3' ", "LookupCode", "asc", ddlMode, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T15' ", "LookupCode", "asc", ddlcity, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T15' ", "LookupCode", "asc", ddlCityTpyeID, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T9' ", "LookupCode", "asc", ddlMealArrangement, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T5' ", "LookupCode", "asc", ddlgusttype, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T6' ", "LookupCode", "asc", ddlPayment, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T7' ", "LookupCode", "asc", ddlOccupancy, "description", "LookupCode", "--Select--");
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T8' and Language=0", "LookupCode", "asc", ddlvehicle, "description", "LookupCode", "--Select--");

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

            Response.Redirect("~/frmTravelMatrix2024.aspx?ID=" + Session["Tcluser"].ToString() + "," + Session["TMonth"].ToString() + "");
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }

    }
    public DataTable CreateDataEntry()
    {

        DataTable dtEntryDoneBY = new DataTable();

        dtEntryDoneBY.Columns.Add(new DataColumn("UniqueCode", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("UniqueChildRCode", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("TotalAmount", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("Expensedetails", System.Type.GetType("System.String")));
        dtEntryDoneBY.Columns.Add(new DataColumn("ImagePath", System.Type.GetType("System.String")));
        Session["dtExpense"] = dtEntryDoneBY;
        return dtEntryDoneBY;
    }
    public DataTable CreateDataExpensevehicle()
    {

        DataTable dtExpensevehicle = new DataTable();

        dtExpensevehicle.Columns.Add(new DataColumn("UniqueCode", System.Type.GetType("System.String")));
        dtExpensevehicle.Columns.Add(new DataColumn("UniqueChildRCode", System.Type.GetType("System.String")));
        dtExpensevehicle.Columns.Add(new DataColumn("VehicletypeID", System.Type.GetType("System.String")));
        dtExpensevehicle.Columns.Add(new DataColumn("VehicletypeName", System.Type.GetType("System.String")));
        dtExpensevehicle.Columns.Add(new DataColumn("VehicleDescription", System.Type.GetType("System.String")));
        dtExpensevehicle.Columns.Add(new DataColumn("VehicleAmout", System.Type.GetType("System.String")));


        dtExpensevehicle.Columns.Add(new DataColumn("ImagePath", System.Type.GetType("System.String")));
        Session["dtExpensevehicle"] = dtExpensevehicle;
        return dtExpensevehicle;
    }

    protected void btnAdd_Vehicle(object sender, EventArgs e)
    {
        string strMainIDNo = objMain.Generate_RandomString(8);
        DataTable dtExpensevehicle = null;
        string Fullfilename = "";


        if (FileuploadExpensevehicle.PostedFile != null && FileuploadExpensevehicle.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadExpensevehicle.PostedFile.FileName).ToLower();
            if (FileuploadExpensevehicle.PostedFile.ContentLength < 1000000)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 1 MB')</script>", false);
                return;
            }

            if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                return;
            }

            string exten = Path.GetExtension(FileuploadExpensevehicle.PostedFile.FileName);
            string Img = "IMG_Vehicle" + "_" + Convert.ToString(Session["username"]);
            Fullfilename = "" + Img + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
        }
        else
        {
            //if (lblUniqueCodeVe.Value.Length > 2)
            //{ }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please upload image')</script>", false);
            //    return;
            //}
        }

        string sFileDir = Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
        ;

        if (FileuploadExpensevehicle.PostedFile != null && FileuploadExpensevehicle.PostedFile.FileName != "")
        {
            string exten = Path.GetExtension(FileuploadExpensevehicle.PostedFile.FileName);
            // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            //create directory

            if (Directory.Exists(sFileDir)) { }
            else { System.IO.Directory.CreateDirectory(sFileDir); }

            //======update the file =====\\

            if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
            {
                try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                catch
                {
                    //ShowMessage.Visible = true;
                    //ShowMessage.Style.Add("background-color", "#FFBABA");
                    //MessageLBL.Style.Add("Color", "#D8000C");
                    //MessageLBL.Text = ex.ToString();

                }
            }
            FileuploadExpensevehicle.PostedFile.SaveAs(sFileDir + Fullfilename);

        }

        if (Session["dtExpensevehicle"] != null)
        {
            dtExpensevehicle = ((DataTable)Session["dtExpensevehicle"]);
        }
        else
        {
            dtExpensevehicle = CreateDataExpensevehicle();
        }
        if (lblUniqueCodeVe.Value.Length > 2)
        {
            DataRow[] drmain = dtExpensevehicle.Select("UniqueCode='" + lblUniqueCodeVe.Value.Trim() + "'");
            if (drmain.Length > 0)
            {

                drmain[0]["UniqueChildRCode"] = "";
                drmain[0]["VehicletypeID"] = ddlvehicle.SelectedValue;
                drmain[0]["VehicletypeName"] = ddlvehicle.SelectedItem.Text;
                drmain[0]["VehicleDescription"] = txtdes.Text;
                drmain[0]["VehicleAmout"] = txtVIcRent.Text;

                if (Fullfilename.Length > 2)
                {
                    drmain[0]["ImagePath"] = Fullfilename;
                }
                else
                {
                    drmain[0]["ImagePath"] = lblImagePathVe.Text;
                }
            }
        }
        else
        {
            DataRow dr;
            dr = dtExpensevehicle.NewRow();
            dr["UniqueCode"] = strMainIDNo;
            dr["UniqueChildRCode"] = "";
            dr["VehicletypeID"] = ddlvehicle.SelectedValue;
            dr["VehicletypeName"] = ddlvehicle.SelectedItem.Text;
            dr["VehicleDescription"] = txtdes.Text;
            dr["VehicleAmout"] = txtVIcRent.Text;
            dr["ImagePath"] = Fullfilename;
            dtExpensevehicle.Rows.Add(dr);
        }


        Session["dtExpensevehicle"] = dtExpensevehicle;
        gvVehicle.DataSource = dtExpensevehicle;
        gvVehicle.DataBind();
        ddlvehicle.SelectedIndex = 0;
        txtdes.Text = "";
        txtVIcRent.Text = "";
        lblImagePathVe.Text = "";
        lblUniqueCodeVe.Value = "";
    }


    protected void BtnEntry_Click(object sender, EventArgs e)
    {

        string strMainIDNo = objMain.Generate_RandomString(8);
        DataTable dtExpense = null;
        string Fullfilename = "";

        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 1000000)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 1 MB')</script>", false);
                return;
            }
            if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                return;
            }
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            string Img = "IMG" + "_" + Convert.ToString(Session["username"]);
            Fullfilename = "" + Img + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;
        }
        else
        {
            if (lblUniqueCodeEx.Value.Length > 2)
            {
                if (lblImagePathEx.Text.Length > 5)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please upload image')</script>", false);
                    return;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Expense receipt')</script>", false);
                return;
            }
        }
        string sFileDir = Server.MapPath(Comman.GetImagePath("TravelPath") + "/");

        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

            //create directory

            if (Directory.Exists(sFileDir)) { }
            else { System.IO.Directory.CreateDirectory(sFileDir); }

            //======update the file =====\\

            if (System.IO.File.Exists(sFileDir + "\\" + Fullfilename))
            {
                try { System.IO.File.Delete(sFileDir + "\\" + Fullfilename); }
                catch
                {
                    //ShowMessage.Visible = true;
                    //ShowMessage.Style.Add("background-color", "#FFBABA");
                    //MessageLBL.Style.Add("Color", "#D8000C");
                    //MessageLBL.Text = ex.ToString();

                }
            }
            FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

        }

        if (Session["dtExpense"] != null)
        {
            dtExpense = ((DataTable)Session["dtExpense"]);
        }
        else
        {
            dtExpense = CreateDataEntry();
        }

        if (lblUniqueCodeEx.Value.Length > 2)
        {
            DataRow[] drmain = dtExpense.Select("UniqueCode='" + lblUniqueCodeEx.Value.Trim() + "'");
            if (drmain.Length > 0)
            {

                drmain[0]["UniqueChildRCode"] = "";
                drmain[0]["TotalAmount"] = txtTrotalAmout.Text;
                drmain[0]["Expensedetails"] = txtExpense.Text;
                if (Fullfilename.Length > 2)
                {
                    drmain[0]["ImagePath"] = Fullfilename;
                }
                else
                {
                    drmain[0]["ImagePath"] = lblImagePathEx.Text;
                }
            }
        }
        else
        {
            DataRow dr;
            dr = dtExpense.NewRow();
            dr["UniqueCode"] = strMainIDNo;
            dr["UniqueChildRCode"] = "";
            dr["TotalAmount"] = txtTrotalAmout.Text;
            dr["Expensedetails"] = txtExpense.Text;
            dr["ImagePath"] = Fullfilename;
            dtExpense.Rows.Add(dr);
        }
        Session["dtExpense"] = dtExpense;
        gvExpens.DataSource = dtExpense;
        gvExpens.DataBind();
        lblUniqueCodeEx.Value = "";
        lblImagePathEx.Text = "";
        txtTrotalAmout.Text = "";
        txtExpense.Text = "";
    }

    protected void ImgDownload_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string lblImagePath = (gvr.FindControl("lblImagePath") as Label).Text;

        string filename = "";
        string IDImage = lblImagePath;
        string sFileDir = Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
        filename = sFileDir + "Travel\\" + IDImage;
        filename = sFileDir + IDImage;

        if (lblImagePath.Length > 5)
        {
            if (System.IO.File.Exists(filename))
            {
                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();
            }
        }
    }
    protected void Delete_Question_Click2(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        string UniqueCode = (gvExpens.DataKeys[index].Values["UniqueCode"].ToString());
        string UniqueChildRCode = (gvExpens.DataKeys[index].Values["UniqueChildRCode"].ToString());
        DataTable dtParticiparticipate = null;


        //  int deleteTSD1 = DeleteExpense(UniqueCode);


        dtParticiparticipate = ((DataTable)Session["dtExpense"]);
        dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);

        int Totalvi = 0;

        for (int i = 0; i < dtParticiparticipate.Rows.Count; i++)
        {
            Totalvi += Convert.ToInt32(dtParticiparticipate.Rows[i]["TotalAmount"]);
        }
        int deleteTSD1 = DeleteExpense(UniqueCode, UniqueChildRCode, "1", Totalvi);



        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        Session["dtExpense"] = dtParticiparticipate;
        gvExpens.DataSource = dtParticiparticipate;
        gvExpens.DataBind();

    }
    public int DeleteExpense(string UniqueCode, string UniqueChildRCode, string Flag, int Totalvi)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),

              new SqlParameter("@DeleteBy", Convert.ToString(Session["username"])),
                new SqlParameter("@UniqueChildRCode",UniqueChildRCode),
                  new SqlParameter("@Totalvi", Totalvi),
                    new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                    new SqlParameter("@Flag", Flag),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteTravelExpens", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {
        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        int result;
        int Icount;
        string strMainIDNo = "";
        int TotalExp = 0;
        string Flag = "";
        DataTable dtExpense = null;
        Int32 dHours = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Hours;
        Int32 dMins = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Minutes;
        string retStr = dHours.ToString() + ":" + dMins.ToString();
        string retStr1 = dHours.ToString() + "." + dMins.ToString();

        DateTime startTime = DateTime.Parse(txtSTime.Text);
        DateTime endTime = DateTime.Parse(txtTTime.Text);

        // Calculate the difference
        TimeSpan duration = endTime - startTime;

        // Get total minutes
        double totalMinutes = duration.TotalMinutes;
        if (Convert.ToDecimal(dMins) >= 0)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid Time')</script>", false);
            return;
        }
        decimal Totalh = Convert.ToDecimal(retStr1);

        if (Totalh >= 0)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid Time')</script>", false);
            return;
        }
        if (Session["dtExpense"] != null)
        {
            dtExpense = Session["dtExpense"] as DataTable;

            for (int i = 0; i < dtExpense.Rows.Count; i++)
            {
                TotalExp += Convert.ToInt32(dtExpense.Rows[i]["TotalAmount"]);
            }
        }
        if (lblEditUUniqecode.Text.Length > 5)

        {
            string strQry7 = " Select * FROM [tblTravelMatrixDeatils2024] where UniqueCode<>'" + lblEditUUniqecode.Text + "' and [UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and deleteFlag=1 order by tblTravelMatrixDeatils2024.TravelDate, convert(time,LogoutTime) desc ";

            DataTable dtSer = objMain.LoadData(strQry7);
            if (dtSer.Rows.Count > 0)
            {
                string inputTime = Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss");
                string EinputTime = Convert.ToDateTime(txtTTime.Text).ToString("HH:mm:ss");
                DateTime newStart = Convert.ToDateTime(inputTime);
                DateTime newEnd = Convert.ToDateTime(EinputTime);


                if (dtSer.Rows.Count > 0)
                {

                    for (int i = 0; i < dtSer.Rows.Count; i++)
                    {

                        string inputTime1 = Convert.ToDateTime(dtSer.Rows[i]["LoginTime"]).ToString("HH:mm:ss");
                        string EinputTime1 = Convert.ToDateTime(dtSer.Rows[i]["LogoutTime"]).ToString("HH:mm:ss");

                        DateTime entryStart = Convert.ToDateTime(inputTime1);
                        DateTime entryENd = Convert.ToDateTime(EinputTime1);

                        bool isPartialOverlap =
                        (newStart >= entryStart && newStart < entryENd) ||
                        (newEnd > entryStart && newEnd <= entryENd);
                        bool isCompleteEnclosure = newStart <= entryStart && newEnd >= entryENd;
                        bool isDatabaseEnclosure = entryStart <= newStart && entryENd >= newEnd;

                        if (isPartialOverlap || isCompleteEnclosure || isDatabaseEnclosure)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The start time must be after the last end time of the day!')</script>", false);
                            return;
                        }
                    }
                }
                //for (int i = 0; i < dtSer.Rows.Count; i++)
                //{

                //    //---    && Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss") < Convert.ToDateTime(dtSer.Rows[i]["LogoutTime"]).ToString("HH:mm:ss"))) || (inputEnd > dbStart && inputEnd <= dbEnd);
                //    //bool isCompleteEnclosure = inputStart <= dbStart && inputEnd >= dbEnd;
                //    //    bool isDatabaseEnclosure = dbStart <= inputStart && dbEnd >= inputEnd;
                //}
                //for (int i = 0; i < dtSer.Rows.Count; i++)
                //{
                //    string strQry5 = " Select * FROM [tblTravelMatrixDeatils2024] where [UniqueCode] = '" + dtSer.Rows[i]["UniqueCode"].ToString() + "' and [UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + Convert.ToDateTime(txtDate.Text).Year + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and (('" + Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss") + "' >convert(datetime,LoginTime) and '" + Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss") + "' <convert(datetime,LogoutTime) )  or ( '" + Convert.ToDateTime(txtTTime.Text).ToString("HH:mm:ss") + "'>convert(datetime,LoginTime) and  '" + Convert.ToDateTime(txtTTime.Text).ToString("HH:mm:ss") + "' <= convert(datetime,LogoutTime) )) and deleteFlag=1  ";

                //    DataTable dtSer5 = objMain.LoadData(strQry5);
                //    if (dtSer5.Rows.Count > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The start time must be after the last end time of the day!')</script>", false);
                //        return;
                //    }

                //}
            }
        }
        else
        {
            string strQry7 = " Select * FROM [tblTravelMatrixDeatils2024] where  [UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and deleteFlag=1 order by tblTravelMatrixDeatils2024.TravelDate, convert(time,LogoutTime) desc ";

            DataTable dtSer = objMain.LoadData(strQry7);
            if (dtSer.Rows.Count > 0)
            {
                string inputTime = Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss");
                string EinputTime = Convert.ToDateTime(txtTTime.Text).ToString("HH:mm:ss");
                DateTime newStart = Convert.ToDateTime(inputTime);
                DateTime newEnd = Convert.ToDateTime(EinputTime);


                if (dtSer.Rows.Count > 0)
                {

                    for (int i = 0; i < dtSer.Rows.Count; i++)
                    {

                        string inputTime1 = Convert.ToDateTime(dtSer.Rows[i]["LoginTime"]).ToString("HH:mm:ss");
                        string EinputTime1 = Convert.ToDateTime(dtSer.Rows[i]["LogoutTime"]).ToString("HH:mm:ss");

                        DateTime entryStart = Convert.ToDateTime(inputTime1);
                        DateTime entryENd = Convert.ToDateTime(EinputTime1);

                        bool isPartialOverlap =
                        (newStart >= entryStart && newStart < entryENd) ||
                        (newEnd > entryStart && newEnd <= entryENd);
                        bool isCompleteEnclosure = newStart <= entryStart && newEnd >= entryENd;
                        bool isDatabaseEnclosure = entryStart <= newStart && entryENd >= newEnd;

                        if (isPartialOverlap || isCompleteEnclosure || isDatabaseEnclosure)
                        {

                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The start time must be after the last end time of the day!')</script>", false);
                            return;
                        }
                    }
                }
                //for (int i = 0; i < dtSer.Rows.Count; i++)
                //{

                //    //---    && Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss") < Convert.ToDateTime(dtSer.Rows[i]["LogoutTime"]).ToString("HH:mm:ss"))) || (inputEnd > dbStart && inputEnd <= dbEnd);
                //    //bool isCompleteEnclosure = inputStart <= dbStart && inputEnd >= dbEnd;
                //    //    bool isDatabaseEnclosure = dbStart <= inputStart && dbEnd >= inputEnd;
                //}
                //for (int i = 0; i < dtSer.Rows.Count; i++)
                //{
                //    string strQry5 = " Select * FROM [tblTravelMatrixDeatils2024] where [UniqueCode] = '" + dtSer.Rows[i]["UniqueCode"].ToString() + "' and [UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + Convert.ToDateTime(txtDate.Text).Year + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and (('" + Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss") + "' >convert(datetime,LoginTime) and '" + Convert.ToDateTime(txtSTime.Text).ToString("HH:mm:ss") + "' <convert(datetime,LogoutTime) )  or ( '" + Convert.ToDateTime(txtTTime.Text).ToString("HH:mm:ss") + "'>convert(datetime,LoginTime) and  '" + Convert.ToDateTime(txtTTime.Text).ToString("HH:mm:ss") + "' <= convert(datetime,LogoutTime) )) and deleteFlag=1  ";

                //    DataTable dtSer5 = objMain.LoadData(strQry5);
                //    if (dtSer5.Rows.Count > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The start time must be after the last end time of the day!')</script>", false);
                //        return;
                //    }

                //}
            }


        }
        //if (TotalExp > 0)
        //{
        //    if (Convert.ToInt32(txtTotalFare.Text) > TotalExp)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Total Expense Greater then Other Expense')</script>", false);
        //        return;
        //    }

        //}
        if (lblEditUUniqecode.Text.Length > 5)

        {
            strMainIDNo = lblEditUUniqecode.Text;
            Flag = "U";
        }
        else
        {
            Flag = "I";
            strMainIDNo = objMain.Generate_RandomString(8);
        }
        if (Convert.ToInt32(ddlType.SelectedValue) == 1)
        {
            SqlParameter[] parm2 = new SqlParameter[]
                             {

                                 new SqlParameter("@UniqueCode", ""+ strMainIDNo  +""),
                                new SqlParameter("@TravelDate", ""+ Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")+""),

                             new SqlParameter("@Villagename", ""+ ddlFromVillage.SelectedValue  +""),
                                 new SqlParameter("@TVillagename", ""+  ddlEndVillage.SelectedValue  +""),
                               new SqlParameter("@mYear", ""+ lblyear.Text  +""),
                           new SqlParameter("@mMonth", ""+ Convert.ToString(Session["TMonth"])+" "),


                             new SqlParameter("@computedFare", ""+ txtTotalFare.Text +" "),
                              new SqlParameter("@CBase", "0"),
                                new SqlParameter("@UserID", ""+ Convert.ToString(Session["FC"]) +" "),
                                 new SqlParameter("@Login", ""+ txtSTime.Text +" "),
                                  new SqlParameter("@Logout", ""+ txtTTime.Text+" "),
                                     new SqlParameter("@Objective", ""+ txtObjective.Text+" "),
                                        new SqlParameter("@KM", ""+ txtKM.Text+" "),

                                  new SqlParameter("@Remarks", txtRemark.Text),
                                    new SqlParameter("@BaseDA", "0"),
                                  new SqlParameter("@RevisedDA", "0"),
                                       new SqlParameter("@SystemLoginTime", ""),
                                                new SqlParameter("@SystemLouttime", ""),

                                                 new SqlParameter("@Distance", "0"),
                                                   new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                                   new SqlParameter("@VisitType", ddlType.SelectedValue),

                                                   new SqlParameter("@Flag",Flag),
                                                    new SqlParameter("@TotalExpensBO", TotalExp),
                                                      new SqlParameter("@TotalHours", totalMinutes.ToString()),
                                                       new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                                         new SqlParameter("@FromNo", Convert.ToString(Session["FromNo"])),
                                                                new SqlParameter("@FormSerialNo", Convert.ToString(Session["FormSerialNo"])),

                             };
            result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixDetails2024", parm2);
            if (result > 0)
            {

                if (Session["dtExpense"] != null)
                {
                    dtExpense = Session["dtExpense"] as DataTable;


                    if (dtExpense.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExpense.Rows.Count; i++)
                        {

                            SqlParameter[] cmdParameters = new SqlParameter[]
                            {
                        new SqlParameter("@UniqueCode", dtExpense.Rows[i]["UniqueCode"]),
                        new SqlParameter("@UniqueChildRCode", strMainIDNo),
                        new SqlParameter("@TotalAmount", dtExpense.Rows[i]["TotalAmount"]),
                        new SqlParameter("@ImagePath", dtExpense.Rows[i]["ImagePath"]),
                        new SqlParameter("@Expensedetails", dtExpense.Rows[i]["Expensedetails"]),
                          new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                           new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),

                            };
                            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixExpens", cmdParameters);

                        }

                    }
                }

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                lblEditUUniqecode.Text = strMainIDNo;
            }
        }

        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            string SType = "";
            string SDist = "";
            string Sblock = "";
            string SCluster = "";
            string SVillage = "";
            string SOtherPlace = "";
            string Sdec = "";
            string SCityType = "";

            string EType = "";
            string EDist = "";
            string Eblock = "";
            string ECluster = "";
            string EVillage = "";
            string EOtherPlace = "";
            string Edec = "";
            string ECityType = "";
            decimal Totalvi = 0;
            int Perdim = 0;
            DataTable dtex;
            if (Session["dtExpensevehicle"] != null)
            {
                dtex = Session["dtExpensevehicle"] as DataTable;

                for (int i = 0; i < dtex.Rows.Count; i++)
                {
                    Totalvi += Convert.ToInt32(dtex.Rows[i]["VehicleAmout"]);
                }
            }
            if (Convert.ToInt32(ddlMode.SelectedValue) == 1)
            {
                Totalvi = 0;
            }



            if (Session["StartCreateDataVillage"] != null)
            {


                DataTable StartCreateDataVillage = ((DataTable)Session["StartCreateDataVillage"]);
                SDist = StartCreateDataVillage.Rows[0]["Dist"].ToString(); ;
                Sblock = StartCreateDataVillage.Rows[0]["Block"].ToString();
                SCluster = StartCreateDataVillage.Rows[0]["Cluster"].ToString(); ;
                SVillage = StartCreateDataVillage.Rows[0]["Village"].ToString();
                SOtherPlace = StartCreateDataVillage.Rows[0]["Other"].ToString();
                Sdec = StartCreateDataVillage.Rows[0]["Desc"].ToString();
                SType = StartCreateDataVillage.Rows[0]["TypeID"].ToString();
                SCityType = StartCreateDataVillage.Rows[0]["FromTierType"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Add Travel Start Place')</script>", false);
                return;
            }
            if (Session["EndCreateDataVillage"] != null)
            {
                DataTable StartCreateDataVillage = ((DataTable)Session["EndCreateDataVillage"]);
                EDist = StartCreateDataVillage.Rows[0]["Dist"].ToString(); ;
                Eblock = StartCreateDataVillage.Rows[0]["Block"].ToString();
                ECluster = StartCreateDataVillage.Rows[0]["Cluster"].ToString(); ;
                EVillage = StartCreateDataVillage.Rows[0]["Village"].ToString();
                EOtherPlace = StartCreateDataVillage.Rows[0]["Other"].ToString();
                Edec = StartCreateDataVillage.Rows[0]["Desc"].ToString();
                EType = StartCreateDataVillage.Rows[0]["TypeID"].ToString();
                ECityType = StartCreateDataVillage.Rows[0]["ToTierType"].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Add Travel End Place')</script>", false);
                return;
            }
            if (chkENtry.Checked == true)
            {
                if (ddlcity.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select City Type')</script>", false);
                    return;
                }
                if (ddlMealArrangement.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Meal Arrangement by EG ')</script>", false);
                    return;
                }
                //if (Totalh < 8)
                //{
                //    if (ddlMealArrangement.SelectedValue == "4")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select other Meal Arrangement by EG')</script>", false);
                //        ddlMealArrangement.SelectedIndex = 0;
                //        return;
                //    }
                //}
                bool ttm = LoaadPerdim();
                if (ttm == false)
                {
                    if (ddlMealArrangement.SelectedValue == "4")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select other Meal Arrangement by EG')</script>", false);
                        ddlMealArrangement.SelectedIndex = 0;
                        return;
                    }
                }
            }

            if (lbllblVillageStart.Text.ToLower() == lblVillageEnd.Text.ToLower())
            {
                if (Convert.ToInt32(ddlMode.SelectedValue) == 3 || Convert.ToInt32(ddlMode.SelectedValue) == 4)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Start Village and End Village Same You Can Not Select Train and Flight')</script>", false);
                    return;
                }
            }
            if (Convert.ToInt32(ddlMode.SelectedValue) == 2 || Convert.ToInt32(ddlMode.SelectedValue) == 3 || Convert.ToInt32(ddlMode.SelectedValue) == 4)
            {
                if (Convert.ToInt32(txtTotalFare.Text) > 0)
                {
                    if (clsMain.TravelImageID.Length > 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Travel Fare Receipt')</script>", false);
                        return;
                    }
                }
                if (rblDist1.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Use of Local Conveyance')</script>", false);
                    return;
                }
            }

            if (rblDist.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Use of Accommodation')</script>", false);
                return;
            }
            if (Convert.ToInt32(rblDist.SelectedValue) == 1)
            {
                if (ddlgusttype.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Accommodation Type:')</script>", false);
                    return;
                }
                if (Convert.ToInt32(ddlgusttype.SelectedValue) == 2)
                {
                    if (ddlPayment.SelectedIndex <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Payment Type')</script>", false);
                        return;
                    }
                    if (Convert.ToInt32(ddlPayment.SelectedValue) == 1)
                    {
                        if (ddlOccupancy.SelectedIndex <= 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Occupancy')</script>", false);
                            return;
                        }
                        if (txthoserent.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Enter Accommodation Fare')</script>", false);
                            return;
                        }
                        if (clsMain.TraveGustHouseImageID.Length > 0)
                        {

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Fare Receipt')</script>", false);
                            return;

                        }
                    }
                }


            }

            if (txtPerDim.Text == "")
            {
                Perdim = 0;
            }
            if (txtPerDim.Text != "")
            {
                Perdim = Convert.ToInt32(txtPerDim.Text);
            }
            int isperdimApply = 0;
            if (divcityType.Visible == true)
            {
                isperdimApply = 1;
            }
            int isperdim = 0;
            if (chkENtry.Checked == true)
            {
                isperdim = 1;
            }

            SqlParameter[] parm2 = new SqlParameter[]
                             {

                                 new SqlParameter("@UniqueCode", ""+ strMainIDNo  +""),
                                new SqlParameter("@TravelDate", ""+ Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")+""),

                             new SqlParameter("@Villagename", ""+ ddlFromVillage.SelectedValue  +""),
                                 new SqlParameter("@TVillagename", ""+  ddlEndVillage.SelectedValue  +""),
                               new SqlParameter("@mYear", ""+ lblyear.Text +""),
                           new SqlParameter("@mMonth", ""+ Convert.ToString(Session["TMonth"])+" "),


                             new SqlParameter("@computedFare", ""+ txtTotalFare.Text +" "),
                              new SqlParameter("@CBase", "0"),
                                new SqlParameter("@UserID", ""+ Convert.ToString(Session["FC"]) +" "),
                                 new SqlParameter("@Login", ""+ txtSTime.Text +" "),
                                  new SqlParameter("@Logout", ""+ txtTTime.Text+" "),
                                     new SqlParameter("@Objective", ""+ txtObjective.Text+" "),
                                        new SqlParameter("@KM", ""+ txtKM.Text+" "),

                                  new SqlParameter("@Remarks", txtRemark.Text),
                                    new SqlParameter("@BaseDA", "0"),
                                  new SqlParameter("@RevisedDA", "0"),
                                       new SqlParameter("@SystemLoginTime", ""),
                                                new SqlParameter("@SystemLouttime", ""),

                                   new SqlParameter("@Distance", "0"),
                                     new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                     new SqlParameter("@VisitType", ddlType.SelectedValue),
                                new SqlParameter("@Flag",Flag),
                                new SqlParameter("@TotalExpensBO", TotalExp),
                                 new SqlParameter("@TravelMode", ddlMode.SelectedValue),
                                    new SqlParameter("@ExpensereceiptImage", clsMain.TravelImageID),
                                   new SqlParameter("@CityType", ddlcity.SelectedValue),
                                   new SqlParameter("@Arrangementby", ddlMealArrangement.SelectedValue),
                                   new SqlParameter("@Useofaccommodation",rblDist.SelectedValue),
                                   new SqlParameter("@GuestHouseType", ddlgusttype.SelectedValue),
                                     new SqlParameter("@PaymentType", ddlPayment.SelectedValue),
                                       new SqlParameter("@Occupancy", ddlOccupancy.SelectedValue),
                                         new SqlParameter("@GuestHouseRent", txthoserent.Text),
                                 new SqlParameter("@GuestreceiptImage",clsMain.TraveGustHouseImageID),
                                 new SqlParameter("@Useoflocalvehicle",rblDist1.SelectedValue),
                                 new SqlParameter("@StartVillageFlag", SType),
                                   new SqlParameter("@StartVillageDist", SDist),
                                     new SqlParameter("@StartVillageBlock", Sblock),
                                   new SqlParameter("@StartVillageCluster", SCluster),
                                     new SqlParameter("@StartVillageOutside", SVillage),
                                   new SqlParameter("@EndVillageFlag", EType),
                                     new SqlParameter("@EndVillageDist", EDist),
                                   new SqlParameter("@EndVillageBlock", Eblock),
                                    new SqlParameter("@EndVillageCluster", ECluster),
                                     new SqlParameter("@EndVillageOutside", EVillage),
                                      new SqlParameter("@SOtherPlace", SOtherPlace),
                                     new SqlParameter("@SOtherDesc", Sdec),
                                      new SqlParameter("@EOtherPlace", EOtherPlace),
                                     new SqlParameter("@EOtherDesc", Edec),
                                      new SqlParameter("@Totalvehicle", Totalvi),
                                       new SqlParameter("@TotalHours", totalMinutes.ToString()),
                                         new SqlParameter("@Perdim", Perdim),
                                             new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                                new SqlParameter("@FromNo", Convert.ToString(Session["FromNo"])),

                                                  new SqlParameter("@isperdim", isperdim),
                                                       new SqlParameter("@isperdimApply", isperdimApply),
                                                         new SqlParameter("@FormSerialNo", Convert.ToString(Session["FormSerialNo"])),
                                                          new SqlParameter("@FromTierType", SCityType),
                                                           new SqlParameter("@ToTierType", ECityType),





        };
            result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixDetails2024OOutside2026", parm2);
            if (result > 0)
            {

                if (Session["dtExpense"] != null)
                {
                    dtExpense = Session["dtExpense"] as DataTable;


                    if (dtExpense.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExpense.Rows.Count; i++)
                        {

                            SqlParameter[] cmdParameters = new SqlParameter[]
                            {
                        new SqlParameter("@UniqueCode", dtExpense.Rows[i]["UniqueCode"]),
                        new SqlParameter("@UniqueChildRCode", strMainIDNo),
                        new SqlParameter("@TotalAmount", dtExpense.Rows[i]["TotalAmount"]),
                        new SqlParameter("@ImagePath", dtExpense.Rows[i]["ImagePath"]),
                        new SqlParameter("@Expensedetails", dtExpense.Rows[i]["Expensedetails"]),
                          new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                 new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),

                            };
                            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixExpens", cmdParameters);

                        }

                    }
                }
                if (Session["dtExpensevehicle"] != null)
                {
                    dtExpense = Session["dtExpensevehicle"] as DataTable;


                    if (dtExpense.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtExpense.Rows.Count; i++)
                        {

                            SqlParameter[] cmdParameters = new SqlParameter[]
                            {
                        new SqlParameter("@UniqueCode", dtExpense.Rows[i]["UniqueCode"]),
                        new SqlParameter("@UniqueChildRCode", strMainIDNo),
                        new SqlParameter("@TotalAmount", dtExpense.Rows[i]["VehicleAmout"]),
                        new SqlParameter("@ImagePath", dtExpense.Rows[i]["ImagePath"]),
                        new SqlParameter("@Expensedetails", dtExpense.Rows[i]["VehicleDescription"]),
                          new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                            new SqlParameter("@VehicletypeID",dtExpense.Rows[i]["VehicletypeID"]),
                               new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),

                            };
                            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixVichExpens", cmdParameters);

                        }

                    }
                }
                if (chkENtry.Checked == true)
                {
                    string stperdim = objMain.Generate_RandomString(8);
                    SqlParameter[] cmdParameters1 = new SqlParameter[]
                               {
                        new SqlParameter("@UniqueCode", stperdim),
                        new SqlParameter("@TravelDate", ""+ Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd")+""),
                        new SqlParameter("@mYear",  ""+ lblyear.Text+""),
                        new SqlParameter("@mMonth",""+ Convert.ToString(Session["TMonth"])+" "),
                        new SqlParameter("@UserID", ""+ Convert.ToString(Session["FC"]) +" "),
                          new SqlParameter("@CityType", ddlcity.SelectedValue),
                            new SqlParameter("@MealArrangement",ddlMealArrangement.SelectedValue),
                               new SqlParameter("@TotalHours", lblPerDim.Text),
                                new SqlParameter("@TotalAmount", txtPerDim.Text),
                                  new SqlParameter("@user_level", Convert.ToString(Session["user_level"])),
                                    new SqlParameter("@CreateBy", Convert.ToString(Session["username"])),
                                     new SqlParameter("@UniqueChildRCode", strMainIDNo),
                               };
                    Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatetravelMatrixPerDeim", cmdParameters1);
                }
                else
                {
                    SqlParameter[] cmdParameters1 = new SqlParameter[]
                              {

                                     new SqlParameter("@UniqueChildRCode", strMainIDNo),
                              };
                    Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteMatrix2024PerDIm", cmdParameters1);

                }

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                lblEditUUniqecode.Text = strMainIDNo;
            }
        }
        txtDate.Enabled = false;
        ddlType.Enabled = false;
        //txtSTime.Enabled = false;
        //txtTTime.Enabled = false;
    }

    protected void ddl_D_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock_New();

        ModelVillageSelect.Show();
    }
    protected void ddl_B_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage_new();

        ModelVillageSelect.Show();
    }
    protected void ddl_C_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillVill();

        ModelVillageSelect.Show();
    }
    public void FillCBDist_New()
    {

        conditions = "";
        conditions = "DistrictCode ='" + Convert.ToString(Session["Dcode"]) + "'  ";
        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddl_D, "DistrictName", "DistrictCode", "Select");

    }
    public void FillCBBock_New()
    {
        conditions = "";
        if (Convert.ToInt32(ddl_S.SelectedValue) == 5 || Convert.ToInt32(ddl_S.SelectedValue) == 3)
        {
            conditions = "DistrictCode ='" + ddl_D.SelectedValue + "'   and BlockCode <>'" + Convert.ToString(Session["Bcode"]) + "'";

        }
        else
        {
            conditions = "DistrictCode ='" + ddl_D.SelectedValue + "'  ";
        }



        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddl_B, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCVillage_new()
    {
        conditions = "";
        if (Convert.ToInt32(ddl_S.SelectedValue) == 1)
        {
            conditions = "DistrictCode ='" + ddl_D.SelectedValue + "'  and BlockCode ='" + ddl_B.SelectedValue + "' and ClusterCode <>'" + Convert.ToString(Session["Ccode"]) + "'";

        }
        else if (Convert.ToInt32(ddl_S.SelectedValue) == 2)
        {
            conditions = "DistrictCode ='" + ddl_D.SelectedValue + "'  and BlockCode ='" + ddl_B.SelectedValue + "' and ClusterCode='" + Convert.ToString(Session["Ccode"]) + "'";

        }
        else
        {
            conditions = "DistrictCode ='" + ddl_D.SelectedValue + "'  and BlockCode ='" + ddl_B.SelectedValue + "' ";
        }



        objComman.BindDLL("mstcluster", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName", conditions, "ClusterName", "asc", ddl_C, "ClusterName", "ClusterCode", "--Select--");
    }
    public void FillVill()
    {
        conditions = "";

        conditions = "DistrictCode ='" + ddl_D.SelectedValue + "'  and BlockCode ='" + ddl_B.SelectedValue + "' and ClusterCode='" + ddl_C.SelectedValue + "'";
        objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddl_V, "VillageName", "VillageCode", "--Select--");
    }
    public void FillCBState(DropDownList ddl)
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddl, "StateName", "StateCode", "--Select--");
    }
    protected void btnStart_click(object sender, EventArgs e)
    {
        ddlCityTpyeID.SelectedIndex = 0;
        ddl_D.Enabled = false;
        ddl_B.Enabled = false;
        ddl_C.Enabled = false;
        ddl_C.Enabled = false;
        ddl_V.Enabled = false;

        ddl_B.Items.Clear();
        ddl_C.Items.Clear();
        ddl_C.Items.Clear();
        ddl_V.Items.Clear();
        divCitey.Visible = false;
        dOtherCitey.Visible = false;
        divother.Visible = false;
        divDetail.Visible = false;
        txtOtherPlace.Text = "";
        txtDeatils.Text = "";
        lblSflag.Text = "1";
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T10' and Language=0", "LookupCode", "asc", ddl_S, "description", "LookupCode", "--Select--");

        FillCBDist_New();

        if (Session["StartCreateDataVillage"] != null)
        {
            DataTable StartCreateDataVillage = ((DataTable)Session["StartCreateDataVillage"]);

            ddl_S.SelectedValue = StartCreateDataVillage.Rows[0]["TypeID"].ToString();

            if (ddl_S.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddl_S.SelectedValue) == 1)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.SelectedValue = StartCreateDataVillage.Rows[0]["Block"].ToString();
                    ddl_B_SelectedIndexChanged(ddl_B, null);
                    ddl_C.SelectedValue = StartCreateDataVillage.Rows[0]["Cluster"].ToString();
                    ddl_C_SelectedIndexChanged(ddl_C, null);
                    ddl_V.SelectedValue = StartCreateDataVillage.Rows[0]["Village"].ToString();
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 2)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.SelectedValue = StartCreateDataVillage.Rows[0]["Block"].ToString();
                    ddl_B_SelectedIndexChanged(ddl_B, null);
                    ddl_C.SelectedValue = StartCreateDataVillage.Rows[0]["Cluster"].ToString();
                    ddl_C_SelectedIndexChanged(ddl_C, null);
                    ddl_V.SelectedValue = StartCreateDataVillage.Rows[0]["Village"].ToString();
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 3)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_D_SelectedIndexChanged(ddl_D, null);
                    ddl_B.SelectedValue = StartCreateDataVillage.Rows[0]["Block"].ToString();
                    ddl_B_SelectedIndexChanged(ddl_B, null);
                    ddl_C.SelectedValue = StartCreateDataVillage.Rows[0]["Cluster"].ToString();
                    ddl_C_SelectedIndexChanged(ddl_C, null);
                    ddl_V.SelectedValue = StartCreateDataVillage.Rows[0]["Village"].ToString();
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                    ddl_B.Enabled = true;
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 4)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);

                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 5)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.Enabled = true;
                    ddl_B.SelectedValue = StartCreateDataVillage.Rows[0]["Block"].ToString();


                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 6)
                {
                    ddl_D.SelectedIndex = 1;


                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 7)
                {

                    divDetail.Visible = true;
                    divCitey.Visible = true;
                    ddlCityTpyeID.SelectedValue = StartCreateDataVillage.Rows[0]["FromTierType"].ToString();
                    divCitey_SelectedIndexChanged(ddlCityTpyeID, null);
                    if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 1 || Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 2)
                    {
                        string cityType = StartCreateDataVillage.Rows[0]["Other"].ToString();
                        ddlCityloction.Items.FindByText(cityType).Selected = true;
                        dOtherCitey.Visible = true;
                        divother.Visible = false;
                    }
                    else
                    {
                        divother.Visible = true;
                        dOtherCitey.Visible = false;
                        txtOtherPlace.Text = StartCreateDataVillage.Rows[0]["Other"].ToString();
                    }
                    txtDeatils.Text = StartCreateDataVillage.Rows[0]["Desc"].ToString();
                }


            }
        }
        ModelVillageSelect.Show();


    }
    protected void btnend_click(object sender, EventArgs e)
    {

        ddlCityTpyeID.SelectedIndex = 0;
        ddl_D.Enabled = false;
        ddl_B.Enabled = false;
        ddl_C.Enabled = false;
        ddl_C.Enabled = false;
        ddl_V.Enabled = false;
        ddl_B.Items.Clear();
        ddl_C.Items.Clear();
        ddl_C.Items.Clear();
        ddl_V.Items.Clear();
        divother.Visible = false;
        divDetail.Visible = false;
        divCitey.Visible = false;
        dOtherCitey.Visible = false;
        divother.Visible = false;
        txtOtherPlace.Text = "";
        txtDeatils.Text = "";
        lblSflag.Text = "2";
        FillCBDist_New();
        objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T10' and Language=0 ", "LookupCode", "asc", ddl_S, "description", "LookupCode", "--Select--");
        if (Session["EndCreateDataVillage"] != null)
        {
            DataTable StartCreateDataVillage = ((DataTable)Session["EndCreateDataVillage"]);

            ddl_S.SelectedValue = StartCreateDataVillage.Rows[0]["TypeID"].ToString();

            if (ddl_S.SelectedIndex > 0)
            {
                if (Convert.ToInt32(ddl_S.SelectedValue) == 1)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);
                    ddl_B_SelectedIndexChanged(ddl_B, null);
                    ddl_C.SelectedValue = StartCreateDataVillage.Rows[0]["Cluster"].ToString();
                    ddl_C_SelectedIndexChanged(ddl_C, null);
                    ddl_V.SelectedValue = StartCreateDataVillage.Rows[0]["Village"].ToString();
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 2)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);
                    ddl_B_SelectedIndexChanged(ddl_B, null);
                    ddl_C.SelectedValue = StartCreateDataVillage.Rows[0]["Cluster"].ToString();
                    ddl_C_SelectedIndexChanged(ddl_C, null);
                    ddl_V.SelectedValue = StartCreateDataVillage.Rows[0]["Village"].ToString();
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 3)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_D_SelectedIndexChanged(ddl_D, null);
                    ddl_B.SelectedValue = StartCreateDataVillage.Rows[0]["Block"].ToString();
                    ddl_B_SelectedIndexChanged(ddl_B, null);
                    ddl_C.SelectedValue = StartCreateDataVillage.Rows[0]["Cluster"].ToString();
                    ddl_C_SelectedIndexChanged(ddl_C, null);
                    ddl_V.SelectedValue = StartCreateDataVillage.Rows[0]["Village"].ToString();
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                    ddl_B.Enabled = true;
                    ddl_C.Enabled = true;
                    ddl_V.Enabled = true;
                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 4)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);

                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 5)
                {
                    ddl_D.SelectedIndex = 1;
                    FillCBBock_New();
                    ddl_B.Enabled = true;
                    ddl_B.SelectedValue = StartCreateDataVillage.Rows[0]["Block"].ToString();


                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 6)
                {
                    ddl_D.SelectedIndex = 1;


                }
                if (Convert.ToInt32(ddl_S.SelectedValue) == 7)
                {
                    divDetail.Visible = true;
                    divCitey.Visible = true;
                    ddlCityTpyeID.SelectedValue = StartCreateDataVillage.Rows[0]["TotierType"].ToString();
                    divCitey_SelectedIndexChanged(ddlCityTpyeID, null);
                    if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 1 || Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 2)
                    {
                        string cityType = StartCreateDataVillage.Rows[0]["Other"].ToString();
                        ddlCityloction.Items.FindByText(cityType).Selected = true;
                        dOtherCitey.Visible = true;
                        divother.Visible = false;
                    }
                    else
                    {
                        divother.Visible = true;
                        dOtherCitey.Visible = false;
                        txtOtherPlace.Text = StartCreateDataVillage.Rows[0]["Other"].ToString();
                    }
                    txtDeatils.Text = StartCreateDataVillage.Rows[0]["Desc"].ToString();
                }


            }
        }
        ModelVillageSelect.Show();
    }

    protected void divCitey_SelectedIndexChanged(object sender, EventArgs e)
    {
        divother.Visible = false;
        dOtherCitey.Visible = false;

        if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 1)
        {
            dOtherCitey.Visible = true;
            objComman.BindDLL("mstTravelMatrixOtherLocation", "ID, CityName", " TierType=" + ddlCityTpyeID.SelectedValue + "", "CityName", "asc", ddlCityloction, "CityName", "ID", "--Select--");

        }
        if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 2)
        {
            dOtherCitey.Visible = true;
            objComman.BindDLL("mstTravelMatrixOtherLocation", "ID, CityName", " TierType=" + ddlCityTpyeID.SelectedValue + "", "CityName", "asc", ddlCityloction, "CityName", "ID", "--Select--");

        }

        if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 3)
        {
            divother.Visible = true;
        }
        ModelVillageSelect.Show();
    }
    protected void ddl_S_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_D.Enabled = false;
        ddl_B.Enabled = false;
        ddl_C.Enabled = false;
        ddl_C.Enabled = false;
        ddl_V.Enabled = false;
        ddl_D.SelectedIndex = 0;
        ddl_B.Items.Clear();
        ddl_C.Items.Clear();
        ddlCityTpyeID.SelectedIndex = 0;

        ddl_V.Items.Clear();
        divother.Visible = false;
        divDetail.Visible = false;
        divCitey.Visible = false;
        txtOtherPlace.Text = "";
        txtDeatils.Text = "";

        if (ddl_S.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddl_S.SelectedValue) == 1)
            {
                ddl_D.SelectedIndex = 1;
                FillCBBock_New();
                ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);
                ddl_B_SelectedIndexChanged(ddl_B, null);
                ddl_C.Enabled = true;
                ddl_V.Enabled = true;
            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 2)
            {
                ddl_D.SelectedIndex = 1;
                FillCBBock_New();
                ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);
                ddl_B_SelectedIndexChanged(ddl_B, null);
                ddl_C.Enabled = true;
                ddl_V.Enabled = true;
            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 3)
            {
                ddl_D.SelectedIndex = 1;
                FillCBBock_New();

                ddl_B.Enabled = true;
                ddl_C.Enabled = true;
                ddl_V.Enabled = true;
            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 4)
            {
                ddl_D.SelectedIndex = 1;
                FillCBBock_New();
                ddl_B.SelectedValue = Convert.ToString(Session["Bcode"]);

            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 5)
            {
                ddl_D.SelectedIndex = 1;
                FillCBBock_New();
                ddl_B.Enabled = true;

            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 6)
            {
                ddl_D.SelectedIndex = 1;


            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 7)
            {
                //  divother.Visible = true;
                divCitey.Visible = true;
                divDetail.Visible = true;


            }


        }
        else
        {

        }

        ModelVillageSelect.Show();
    }

    protected void rblDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        txthoserent.Text = "";
        ddlOccupancy.SelectedIndex = 0;
        ddlPayment.SelectedIndex = 0;
        ddlgusttype.SelectedIndex = 0;
        pnlAcc1.Enabled = false;
        Fileupload1.Enabled = false;
        if (Convert.ToInt32(rblDist.SelectedValue) == 1)
        {
            // pnlAcc.Enabled = true;
            pnlAcc1.Enabled = true;
        }
        else
        {
            pnlAcc.Enabled = false;
        }
    }
    protected void ddlgusttype_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (Convert.ToInt32(ddlgusttype.SelectedValue) == 2)
        {
            if (rblDist.SelectedValue == "1")
            {
                pnlAcc.Enabled = true;
                pnlAcc1.Enabled = true;
                Fileupload1.Enabled = true;
            }
            else
            {
                clsMain.TraveGustHouseImageID = "";
                pnlAcc.Enabled = false;
                txthoserent.Text = "";
                ddlOccupancy.SelectedIndex = 0;
                ddlPayment.SelectedIndex = 0;
                Fileupload1.Enabled = false;
                // ddlgusttype.SelectedIndex = 0;
                pnlAcc.Enabled = false;
            }


        }
        else
        {
            clsMain.TraveGustHouseImageID = "";
            pnlAcc.Enabled = false;
            txthoserent.Text = "";
            ddlOccupancy.SelectedIndex = 0;
            ddlPayment.SelectedIndex = 0;
            Fileupload1.Enabled = false;
            // ddlgusttype.SelectedIndex = 0;
            pnlAcc.Enabled = false;

        }

    }

    protected void ddlccce_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Amm = 0;

        if (Convert.ToInt32(ddlPayment.SelectedValue) == 1)
        {
            ddlOccupancy.Enabled = true;
            txthoserent.Enabled = true;
            Fileupload1.Enabled = true;
            txthoserent.Text = "";
            ddlOccupancy.SelectedIndex = 0;
        }
        else if (Convert.ToInt32(ddlPayment.SelectedValue) == 2)
        {
            clsMain.TraveGustHouseImageID = "";
            ddlOccupancy.Enabled = false;
            txthoserent.Enabled = false;
            Fileupload1.Enabled = false;
            txthoserent.Text = "";
            ddlOccupancy.SelectedIndex = 0;
        }




    }

    protected void rblDist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtdes.Text = "";
        txtVIcRent.Text = "";
        ddlvehicle.SelectedIndex = 0;

        if (Convert.ToInt32(ddlMode.SelectedValue) == 2 || Convert.ToInt32(ddlMode.SelectedValue) == 3 || Convert.ToInt32(ddlMode.SelectedValue) == 4 || Convert.ToInt32(ddlMode.SelectedValue) == 5)
        {
            if (Convert.ToInt32(rblDist1.SelectedValue) == 1)
            {
                FileuploadExpensevehicle.Enabled = true;
                pndVic.Enabled = true;
            }
            else
            {
                FileuploadExpensevehicle.Enabled = false;
                pndVic.Enabled = false;
            }
        }
        Session["dtExpensevehicle"] = null;
        gvVehicle.DataSource = "";
        gvVehicle.DataBind();
    }

    protected void ImgDownloadV_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string lblImagePath = (gvr.FindControl("lblImagePath") as Label).Text;

        string filename = "";
        string IDImage = lblImagePath;
        string sFileDir = Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
        filename = sFileDir + "Travel\\" + IDImage;
        filename = sFileDir + IDImage;

        if (lblImagePath.Length > 5)
        {
            if (System.IO.File.Exists(filename))
            {
                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();
            }
        }
    }
    protected void Delete_Question_Click3(object sender, EventArgs e)
    {
        //MPEFormName.Show();

        LinkButton Edit_Question = sender as LinkButton;
        GridViewRow row = Edit_Question.NamingContainer as GridViewRow;
        int index = row.RowIndex;


        string UniqueCode = (gvVehicle.DataKeys[index].Values["UniqueCode"].ToString());
        string UniqueChildRCode = (gvVehicle.DataKeys[index].Values["UniqueChildRCode"].ToString());
        DataTable dtParticiparticipate = null;




        dtParticiparticipate = ((DataTable)Session["dtExpensevehicle"]);
        dtParticiparticipate.Rows.Remove(dtParticiparticipate.Rows[index]);





        int Totalvi = 0;

        for (int i = 0; i < dtParticiparticipate.Rows.Count; i++)
        {
            Totalvi += Convert.ToInt32(dtParticiparticipate.Rows[i]["VehicleAmout"]);
        }
        int deleteTSD1 = DeleteExpense(UniqueCode, UniqueChildRCode, "2", Totalvi);



        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Delete Sucessfully')</script>", false);

        Session["dtExpensevehicle"] = dtParticiparticipate;
        gvVehicle.DataSource = dtParticiparticipate;
        gvVehicle.DataBind();

    }

    public DataTable StartCreateDataVillagedt()
    {

        DataTable StartCreateDataVillage = new DataTable();

        StartCreateDataVillage.Columns.Add(new DataColumn("TypeID", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("Dist", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("Block", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("Cluster", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("Village", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("Other", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("Desc", System.Type.GetType("System.String")));
        StartCreateDataVillage.Columns.Add(new DataColumn("FromTierType", System.Type.GetType("System.String")));
        Session["StartCreateDataVillage"] = StartCreateDataVillage;
        return StartCreateDataVillage;
    }
    public DataTable EndCreateDataVillagedt()
    {

        DataTable EndCreateDataVillage = new DataTable();

        EndCreateDataVillage.Columns.Add(new DataColumn("TypeID", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("Dist", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("Block", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("Cluster", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("Village", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("Other", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("Desc", System.Type.GetType("System.String")));
        EndCreateDataVillage.Columns.Add(new DataColumn("ToTierType", System.Type.GetType("System.String")));
        Session["EndCreateDataVillage"] = EndCreateDataVillage;
        return EndCreateDataVillage;
    }
    protected void btnSave_village(object sender, EventArgs e)
    {
        divcityType.Visible = false;
        DataTable StartCreateDataVillage = null;
        DataTable EndCreateDataVillage = null;
        divcityType.Visible = true;
        ddlcity.Enabled = false;
        // ddlMealArrangement.Enabled = false;

        if (Convert.ToInt32(ddl_S.SelectedValue) > 0)
        {
            if (Convert.ToInt32(ddl_S.SelectedValue) == 1 || Convert.ToInt32(ddl_S.SelectedValue) == 2 || Convert.ToInt32(ddl_S.SelectedValue) == 3)
            {
                if (ddl_V.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
                    ModelVillageSelect.Show();
                    return;
                }
                if (lblSflag.Text == "1")
                {

                    StartCreateDataVillage = StartCreateDataVillagedt();


                    DataRow dr;
                    dr = StartCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = ddl_D.SelectedValue;
                    dr["Block"] = ddl_B.SelectedValue;
                    dr["Cluster"] = ddl_C.SelectedValue;
                    dr["Village"] = ddl_V.SelectedValue;
                    dr["Other"] = "";

                    dr["Desc"] = "";
                    lbllblVillageStart.Text = ddl_V.SelectedItem.Text;
                    StartCreateDataVillage.Rows.Add(dr);
                    Session["StartCreateDataVillage"] = StartCreateDataVillage;
                }
                if (lblSflag.Text == "2")
                {

                    EndCreateDataVillage = EndCreateDataVillagedt();


                    DataRow dr;
                    dr = EndCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = ddl_D.SelectedValue;
                    dr["Block"] = ddl_B.SelectedValue;
                    dr["Cluster"] = ddl_C.SelectedValue;
                    dr["Village"] = ddl_V.SelectedValue;
                    dr["Other"] = "";

                    dr["Desc"] = "";
                    lblVillageEnd.Text = ddl_V.SelectedItem.Text;
                    EndCreateDataVillage.Rows.Add(dr);
                    Session["EndCreateDataVillage"] = EndCreateDataVillage;
                    ddlcity.SelectedValue = "2";
                    ddlCite_SelectedIndexChanged(ddlcity, null);
                }

            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 4 || Convert.ToInt32(ddl_S.SelectedValue) == 5)
            {
                if (ddl_B.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);
                    ModelVillageSelect.Show();
                    return;
                }
                if (lblSflag.Text == "1")
                {
                    StartCreateDataVillage = StartCreateDataVillagedt();


                    DataRow dr;
                    dr = StartCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = ddl_D.SelectedValue;
                    dr["Block"] = ddl_B.SelectedValue;
                    dr["Cluster"] = "";
                    dr["Village"] = "";
                    dr["Other"] = "";

                    dr["Desc"] = "";

                    lbllblVillageStart.Text = ddl_B.SelectedItem.Text;
                    StartCreateDataVillage.Rows.Add(dr);
                    Session["StartCreateDataVillage"] = StartCreateDataVillage;
                }
                if (lblSflag.Text == "2")
                {

                    EndCreateDataVillage = EndCreateDataVillagedt();


                    DataRow dr;
                    dr = EndCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = ddl_D.SelectedValue;
                    dr["Block"] = ddl_B.SelectedValue;
                    dr["Cluster"] = "";
                    dr["Village"] = "";
                    dr["Other"] = "";

                    dr["Desc"] = "";
                    lblVillageEnd.Text = ddl_B.SelectedItem.Text;
                    EndCreateDataVillage.Rows.Add(dr);
                    Session["EndCreateDataVillage"] = EndCreateDataVillage;
                    ddlcity.SelectedValue = "2";
                    ddlCite_SelectedIndexChanged(ddlcity, null);
                }
            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 6)
            {
                if (ddl_D.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
                    ModelVillageSelect.Show();
                    return;
                }
                if (lblSflag.Text == "1")
                {

                    StartCreateDataVillage = StartCreateDataVillagedt();


                    DataRow dr;
                    dr = StartCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = ddl_D.SelectedValue;
                    dr["Block"] = "";
                    dr["Cluster"] = "";
                    dr["Village"] = "";
                    dr["Other"] = "";

                    dr["Desc"] = "";

                    lbllblVillageStart.Text = ddl_D.SelectedItem.Text;
                    StartCreateDataVillage.Rows.Add(dr);
                    Session["StartCreateDataVillage"] = StartCreateDataVillage;
                }
                if (lblSflag.Text == "2")
                {

                    EndCreateDataVillage = EndCreateDataVillagedt();


                    DataRow dr;
                    dr = EndCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = ddl_D.SelectedValue;
                    dr["Block"] = "";
                    dr["Cluster"] = "";
                    dr["Village"] = "";
                    dr["Other"] = "";

                    dr["Desc"] = "";
                    lblVillageEnd.Text = ddl_D.SelectedItem.Text;
                    EndCreateDataVillage.Rows.Add(dr);
                    Session["EndCreateDataVillage"] = EndCreateDataVillage;
                    ddlcity.SelectedValue = "2";
                    ddlCite_SelectedIndexChanged(ddlcity, null);

                }
            }
            if (Convert.ToInt32(ddl_S.SelectedValue) == 7)
            {
                if (ddlCityTpyeID.SelectedIndex <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select TierType')</script>", false);
                    ModelVillageSelect.Show();
                    return;
                }
                if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 1 || Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 2)
                {
                    if (ddlCityloction.SelectedIndex <= 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select City')</script>", false);
                        ModelVillageSelect.Show();
                        return;
                    }

                }

                if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 3)
                {
                    if (txtOtherPlace.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter other place')</script>", false);
                        ModelVillageSelect.Show();
                        return;
                    }
                }
                if (txtDeatils.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Detail of visited place')</script>", false);
                    ModelVillageSelect.Show();
                    return;
                }
                if (lblSflag.Text == "1")
                {

                    StartCreateDataVillage = StartCreateDataVillagedt();


                    DataRow dr;
                    dr = StartCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = "";
                    dr["Block"] = "";
                    dr["Cluster"] = "";
                    dr["Village"] = "";
                    if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 1 || Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 2)
                    {

                        dr["Other"] = ddlCityloction.SelectedItem.Text;
                        lbllblVillageStart.Text = ddlCityloction.SelectedItem.Text;
                    }
                    else
                    {
                        dr["Other"] = txtOtherPlace.Text;
                        lbllblVillageStart.Text = txtOtherPlace.Text;
                    }
                    dr["Desc"] = txtDeatils.Text;
                    dr["FromTierType"] = ddlCityTpyeID.SelectedValue;
                    ddlcity.SelectedValue = ddlCityTpyeID.SelectedValue;
                    StartCreateDataVillage.Rows.Add(dr);
                    Session["StartCreateDataVillage"] = StartCreateDataVillage;
                    ddlCite_SelectedIndexChanged(ddlcity, null);

                }
                if (lblSflag.Text == "2")
                {

                    EndCreateDataVillage = EndCreateDataVillagedt();


                    DataRow dr;
                    dr = EndCreateDataVillage.NewRow();
                    dr["TypeID"] = ddl_S.SelectedValue;
                    dr["Dist"] = "";
                    dr["Block"] = "";
                    dr["Cluster"] = "";
                    dr["Village"] = "";
                    if (Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 1 || Convert.ToInt32(ddlCityTpyeID.SelectedValue) == 2)
                    {
                        dr["Other"] = ddlCityloction.SelectedItem.Text;
                        lblVillageEnd.Text = ddlCityloction.SelectedItem.Text;
                    }
                    else
                    {
                        dr["Other"] = txtOtherPlace.Text;
                        lblVillageEnd.Text = txtOtherPlace.Text;
                    }

                    dr["Desc"] = txtDeatils.Text;
                    dr["ToTierType"] = ddlCityTpyeID.SelectedValue;

                    EndCreateDataVillage.Rows.Add(dr);
                    Session["EndCreateDataVillage"] = EndCreateDataVillage;
                    // ddlcity.SelectedValue = "2";
                    ddlCite_SelectedIndexChanged(ddlcity, null);
                    // ddlcity.Enabled = true;
                    //ddlcity.Enabled = true;
                }
            }

            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Save Sucessfully')</script>", false);

        }
        else
        {

        }

        if (Session["StartCreateDataVillage"] != null && Session["EndCreateDataVillage"] != null)
        {


            DataTable StartCreateDataVillage1 = ((DataTable)Session["StartCreateDataVillage"]);
            DataTable StartCreateDataVillage2 = ((DataTable)Session["EndCreateDataVillage"]);
            divcityType.Visible = true;

            string SType = StartCreateDataVillage1.Rows[0]["TypeID"].ToString();
            string EType = StartCreateDataVillage2.Rows[0]["TypeID"].ToString();
            if (SType == "1" || SType == "2" || SType == "4")
            {
                if (EType == "1" || EType == "2" || EType == "4")
                {
                    divcityType.Visible = false;
                    ddlcity.SelectedIndex = 0;
                    chkENtry.Checked = false;
                }
            }
            if (SType == "7" && SType == "7")
            {
                ddlcity.Enabled = false;
            }


        }

    }
    protected void ddlCite_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strQry5 = "  select * from tblTravelMatrixMaximumAmount  where EMPLevel='L8' ";
        DataTable dtMax = objMain.LoadData(strQry5);
        if (Convert.ToInt32(ddlcity.SelectedValue) == 2)
        {
            hndMaxamt.Value = dtMax.Rows[0]["Tier2"].ToString();
        }
        else if (Convert.ToInt32(ddlcity.SelectedValue) == 3)
        {
            hndMaxamt.Value = dtMax.Rows[0]["Tier3"].ToString();
        }
        else
        {
            hndMaxamt.Value = dtMax.Rows[0]["Tier1"].ToString();
        }

        ddlMealArrangement.SelectedIndex = 0;

    }

    protected void ddlMealArrangement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcity.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select City Type')</script>", false);
            txtPerDim.Text = "";
            return;
        }
        if (txtDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
            txtPerDim.Text = "";
            return;
        }
        if (txtSTime.Text == "" || txtTTime.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Valid Time')</script>", false);
            txtPerDim.Text = "";
            return;
        }
        string strQry6 = "  select * from TravelMartrixPerDim  where [EmployeeLevel]='L8' ";
        DataTable dt = objMain.LoadData(strQry6);
        DataTable dtDim = null;

        Int32 dHours = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Hours;
        Int32 dMins = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Minutes;
        if (Convert.ToDecimal(dMins) >= 0)
        {

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid Time')</script>", false);
            return;
        }
        string retStr = dHours.ToString() + "." + dMins.ToString();
        decimal Totalh = Convert.ToDecimal(retStr);
        DateTime startTime = DateTime.Parse(txtSTime.Text);
        DateTime endTime = DateTime.Parse(txtTTime.Text);

        // Calculate the difference
        TimeSpan duration = endTime - startTime;

        // Get total minutes
        double totalMinutes = duration.TotalMinutes;

        decimal TotalhEdit = 0;
        if (lblEditUUniqecode.Text.Length > 5)
        {
            string strQry7 = " Select isnull(sum(convert(decimal,totalhours)),0) TotalHours FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
            dtDim = objMain.LoadData(strQry7);
            if (dtDim.Rows.Count > 0)
            {
                TotalhEdit = Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);

            }
        }
        else
        {
            string strQry7 = " Select isnull(sum(convert(decimal,totalhours)),0) TotalHours  FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and VisitType=2  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and  DeleteFlag=1";
            dtDim = objMain.LoadData(strQry7);
            if (dtDim.Rows.Count > 0)
            {
                TotalhEdit += Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);
                //ddlMealArrangement.SelectedValue = dtDim.Rows[0]["MealArrangement"].ToString();
            }
        }
        Totalh = TotalhEdit + (decimal)totalMinutes;
        if (Totalh < 600)
        {
            if (ddlMealArrangement.SelectedValue == "4")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select other Meal Arrangement by EG')</script>", false);
                ddlMealArrangement.SelectedIndex = 0;
                return;
            }
        }
        //lblPerDim.Text = Totalh.ToString();
        //if (Totalh > 0)
        //{

        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid Time')</script>", false);
        //    return;
        //}

        //if (ddlMealArrangement.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
        //{
        //    if (Convert.ToInt32(ddlcity.SelectedValue) == 2)
        //    {
        //        if (Totalh > 8)
        //        {
        //            TotalCon = Convert.ToDecimal(dt.Rows[0]["Morethan8Hours100TierII"]);
        //        }
        //        else
        //        {
        //            TotalCon = Convert.ToDecimal(dt.Rows[0]["Lessthan8Hours50TierII"]);
        //        }
        //        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
        //        {
        //            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
        //            {
        //                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
        //            }
        //            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
        //            {
        //                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
        //            }
        //            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
        //            {
        //                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
        //            }
        //        }
        //    }
        //    if (Convert.ToInt32(ddlcity.SelectedValue) == 1)
        //    {
        //        if (Totalh > 8)
        //        {
        //            TotalCon = Convert.ToDecimal(dt.Rows[0]["Morethan8Hours100TierI"]);
        //        }
        //        else
        //        {
        //            TotalCon = Convert.ToDecimal(dt.Rows[0]["Lessthan8Hours50TierI"]);
        //        }
        //        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
        //        {
        //            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
        //            {
        //                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
        //            }
        //            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
        //            {
        //                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
        //            }
        //            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
        //            {
        //                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
        //            }
        //        }
        //    }
        //}
        //int FinalCon2 = Convert.ToInt32(Math.Round(TotalCon));
        //txtPerDim.Text = FinalCon2.ToString();

        LoaadPerdim();
    }

    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {


        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string lblUniqueCode = (gvr.FindControl("lblUniqueCode") as Label).Text;
        string lblTotalAmount = (gvr.FindControl("lblTotalAmount") as Label).Text;
        string lblImagePath = (gvr.FindControl("lblImagePath") as Label).Text;
        string lblExpensedetails = (gvr.FindControl("lblExpensedetails") as Label).Text;
        txtTrotalAmout.Text = lblTotalAmount.ToString();
        txtExpense.Text = lblExpensedetails.ToString();
        lblUniqueCodeEx.Value = lblUniqueCode.ToString();
        lblImagePathEx.Text = lblImagePath.ToString();

    }

    protected void LnkBtnBlock_OnClick1(object sender, EventArgs e)
    {


        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string lblUniqueCode = (gvr.FindControl("lblUniqueCode") as Label).Text;
        string lblTotalAmount = (gvr.FindControl("lblRent") as Label).Text;
        string lblImagePath = (gvr.FindControl("lblImagePath") as Label).Text;
        string lblExpensedetails = (gvr.FindControl("lblDescription") as Label).Text;
        string VehicletypeID = (gvr.FindControl("lblVehicletypeID") as Label).Text;


        txtVIcRent.Text = lblTotalAmount.ToString();
        txtdes.Text = lblExpensedetails.ToString();
        lblUniqueCodeVe.Value = lblUniqueCode.ToString();
        lblImagePathVe.Text = lblImagePath.ToString();
        ddlvehicle.SelectedValue = VehicletypeID.ToString();

    }
    protected void gvVehicle_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");

            ImageButton lnkd = (ImageButton)e.Row.FindControl("lnkd");
            if (lblImagePath.Text.Length > 5)
            {
                lnkd.Visible = true;
            }
            else
            {
                lnkd.Visible = false;
            }
        }
    }
    protected void gvExpens_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblImagePath = (Label)e.Row.FindControl("lblImagePath");

            ImageButton lnkd = (ImageButton)e.Row.FindControl("lnkd");
            if (lblImagePath.Text.Length > 5)
            {
                lnkd.Visible = true;
            }
            else
            {
                lnkd.Visible = false;
            }
        }
    }
    protected void ImgDownloadMani_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }

        string filename = "";
        string IDImage = clsMain.TravelImageID;
        string sFileDir = Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
        filename = sFileDir + "Travel\\" + IDImage;
        filename = sFileDir + IDImage;
        //clsMain.TraveGustHouseImageID = dtSer.Rows[0]["ExpensereceiptImage"].ToString();
        //clsMain.TravelImageID = dtSer.Rows[0]["GuestreceiptImage"].ToString();
        if (IDImage.Length > 5)
        {
            if (System.IO.File.Exists(filename))
            {
                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();
            }
        }
    }

    protected void ImgDownloadAcc_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["username"]) != "")
        {

        }
        else
        {
            Response.Redirect("Login.aspx", false);

        }

        string filename = "";
        string IDImage = clsMain.TraveGustHouseImageID;
        string sFileDir = Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
        filename = sFileDir + "Travel\\" + IDImage;
        filename = sFileDir + IDImage;
        //clsMain.TraveGustHouseImageID = dtSer.Rows[0]["ExpensereceiptImage"].ToString();
        //clsMain.TravelImageID = dtSer.Rows[0]["GuestreceiptImage"].ToString();
        if (IDImage.Length > 5)
        {
            if (System.IO.File.Exists(filename))
            {
                Response.ContentType = ".jpg";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

                Response.TransmitFile(filename);
                Response.End();
            }
        }
    }
    [WebMethod(EnableSession = true)]
    public static string SaveImage(string base64File, string fileName, FileUpload File4)
    {
        try
        {
            var extension = Path.GetExtension(fileName);
            string FormID = "IMG-GusestHouse";
            string Fullfilename = "" + FormID + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + extension;

            if (extension.ToUpper() != ".JPG" && extension.ToUpper() != ".PNG" && extension.ToUpper() != ".JPEG")
            {
                return "Only PNG and JPG file formats are allowed. Please upload a valid image file";
            }

            else
            {
                byte[] fileBytes = Convert.FromBase64String(base64File.Split(',')[1]);
                string uploadFolder = HttpContext.Current.Server.MapPath(Comman.GetImagePath("TravelPath") + "/");
                if (File4.PostedFile != null && File4.PostedFile.FileName != "")
                {
                    string exten = Path.GetExtension(File4.PostedFile.FileName);
                    // string Imagefile1 = "LeaveDoc" + "_" + Convert.ToString(Session["EMP_ID"]) + "_" + DateTime.Now.ToString("ddMMyyyy_hhmmss") + exten;

                    //create directory

                    if (Directory.Exists(uploadFolder)) { }
                    else { System.IO.Directory.CreateDirectory(uploadFolder); }

                    //======update the file =====\\

                    if (System.IO.File.Exists(uploadFolder + "\\" + Fullfilename))
                    {
                        try { System.IO.File.Delete(uploadFolder + "\\" + Fullfilename); }
                        catch
                        {
                            //ShowMessage.Visible = true;
                            //ShowMessage.Style.Add("background-color", "#FFBABA");
                            //MessageLBL.Style.Add("Color", "#D8000C");
                            //MessageLBL.Text = ex.ToString();

                        }
                    }
                    File4.PostedFile.SaveAs(uploadFolder + Fullfilename);

                }
                //if (!Directory.Exists(uploadFolder))
                //{
                //    Directory.CreateDirectory(uploadFolder);
                //}
                //string filePath = Path.Combine(uploadFolder, Fullfilename);
                //File.WriteAllBytes(filePath, fileBytes);
                //clsMain.TraveGustHouseImageID = Fullfilename;
                return "Image uploaded successfully";
            }


        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }


    protected void txtStartDate__Change(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlType.SelectedValue) == 2)
        {
            string strQry11 = " Select  * FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and IsPerdim=1 and  DeleteFlag=1";
            DataTable dtChheck = objMain.LoadData(strQry11);
            if (dtChheck.Rows.Count > 0)
            {
                txtDate.Text = "";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Last visit already add')</script>", false);
                return;
            }
        }

    }
    protected void txtEndDate__Change(object sender, EventArgs e)
    {
        if (txtSTime.Text == "" || txtTTime.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Valid Time')</script>", false);
            txtTTime.Text = "";
            return;
        }

    }
    public bool LoaadPerdim()
    {
        string strQry6 = "  select * from TravelMartrixPerDim  where [EmployeeLevel]='L8' ";
        DataTable dt = objMain.LoadData(strQry6);
        DataTable dtDim = null;

        Int32 dHours = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Hours;
        Int32 dMins = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Minutes;

        DateTime startTime = DateTime.Parse(txtSTime.Text);
        DateTime endTime = DateTime.Parse(txtTTime.Text);


        // Calculate the difference
        TimeSpan duration = endTime - startTime;

        // Get total minutes
        double totalMinutes = duration.TotalMinutes;


        int TotalKM = 0;
        int TotalMinKM = 0;
        if (txtKM.Text != "")
        {
            TotalKM = Convert.ToInt32(txtKM.Text);
        }
        // Calculate the difference


        string strQry8 = " Select  isnull(KMAdmin),0) KMAdmin FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
        DataTable dtKm = objMain.LoadData(strQry8);
        if (dtKm.Rows.Count > 0)
        {
            TotalMinKM = TotalKM + Convert.ToInt32(dtKm.Rows[0]["KMAdmin"]);
        }
        else
        {
            TotalMinKM = TotalKM;

        }

        if (Convert.ToInt32(ddlMode.SelectedValue) == 1)
        {
            if (TotalMinKM > 60)
            {
                if (ddlMealArrangement.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
                {
                    string retStr = dHours.ToString() + "." + dMins.ToString();
                    decimal Totalh = Convert.ToDecimal(retStr);

                    decimal TotalCon = 0;

                    decimal TotalhEdit = 0;


                    if (lblEditUUniqecode.Text.Length > 5)
                    {
                        string strQry7 = " Select  isnull(sum(convert(decimal,totalhours)),0) TotalHours FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
                        dtDim = objMain.LoadData(strQry7);
                        if (dtDim.Rows.Count > 0)
                        {
                            TotalhEdit = Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);

                        }
                    }
                    else
                    {
                        string strQry7 = " Select  isnull(sum(convert(decimal,totalhours)),0) TotalHours FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and  DeleteFlag=1";
                        dtDim = objMain.LoadData(strQry7);
                        if (dtDim.Rows.Count > 0)
                        {
                            TotalhEdit = Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);
                            //ddlMealArrangement.SelectedValue = dtDim.Rows[0]["MealArrangement"].ToString();
                        }
                    }

                    Totalh = TotalhEdit + (decimal)totalMinutes;

                    lblPerDim.Text = Totalh.ToString();

                    if (Totalh < 600)
                    {
                        if (ddlMealArrangement.SelectedValue == "4")
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select other Meal Arrangement by EG')</script>", false);
                            ddlMealArrangement.SelectedIndex = 0;
                            return false;
                        }
                    }

                    if (Convert.ToInt32(ddlcity.SelectedValue) == 2)
                    {
                        if (Totalh > 600)
                        {
                            TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIICityINR"]);
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                            {
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
                                }
                            }

                        }
                        else
                        {

                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 1)
                            {
                                TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIICityINR"]) / 2;
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                                {
                                    TotalCon = 0;
                                }
                            }

                        }
                    }
                    if (Convert.ToInt32(ddlcity.SelectedValue) == 1)
                    {
                        if (Totalh > 600)
                        {
                            TotalCon = Convert.ToDecimal(dt.Rows[0]["TierICityINR"]);
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                            {
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
                                }
                            }
                        }
                        else
                        {
                            // TotalCon = Convert.ToDecimal(dt.Rows[0]["Lessthan8Hours50TierI"]);
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 1)
                            {
                                TotalCon = Convert.ToDecimal(dt.Rows[0]["TierICityINR"]) / 2;
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                                {
                                    TotalCon = 0;
                                }

                            }
                        }

                    }


                    if (Convert.ToInt32(ddlcity.SelectedValue) == 3)
                    {
                        if (Totalh > 600)
                        {
                            TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIIICityINR"]);
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                            {
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
                                }
                            }
                        }
                        else
                        {
                            // TotalCon = Convert.ToDecimal(dt.Rows[0]["Lessthan8Hours50TierI"]);
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 1)
                            {
                                TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIIICityINR"]) / 2;
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                                {
                                    TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                                }
                                if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                                {
                                    TotalCon = 0;
                                }

                            }
                        }

                    }

                    int FinalCon2 = Convert.ToInt32(Math.Round(TotalCon));
                    txtPerDim.Text = FinalCon2.ToString();
                }
            }
        }
        else
        {
            if (ddlMealArrangement.SelectedIndex > 0 && ddlcity.SelectedIndex > 0)
            {
                string retStr = dHours.ToString() + "." + dMins.ToString();
                decimal Totalh = Convert.ToDecimal(retStr);

                decimal TotalCon = 0;

                decimal TotalhEdit = 0;


                if (lblEditUUniqecode.Text.Length > 5)
                {
                    string strQry7 = " Select  isnull(sum(convert(decimal,totalhours)),0) TotalHours FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
                    dtDim = objMain.LoadData(strQry7);
                    if (dtDim.Rows.Count > 0)
                    {
                        TotalhEdit = Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);

                    }
                }
                else
                {
                    string strQry7 = " Select  isnull(sum(convert(decimal,totalhours)),0) TotalHours FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and  DeleteFlag=1";
                    dtDim = objMain.LoadData(strQry7);
                    if (dtDim.Rows.Count > 0)
                    {
                        TotalhEdit = Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);
                        //ddlMealArrangement.SelectedValue = dtDim.Rows[0]["MealArrangement"].ToString();
                    }
                }

                Totalh = TotalhEdit + (decimal)totalMinutes;

                lblPerDim.Text = Totalh.ToString();

                if (Totalh < 600)
                {
                    if (ddlMealArrangement.SelectedValue == "4")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select other Meal Arrangement by EG')</script>", false);
                        ddlMealArrangement.SelectedIndex = 0;
                        return false;
                    }
                }

                if (Convert.ToInt32(ddlcity.SelectedValue) == 2)
                {
                    if (Totalh > 600)
                    {
                        TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIICityINR"]);
                        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                        {
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
                            }
                        }

                    }
                    else
                    {

                        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 1)
                        {
                            TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIICityINR"]) / 2;
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                            {
                                TotalCon = 0;
                            }
                        }

                    }
                }
                if (Convert.ToInt32(ddlcity.SelectedValue) == 1)
                {
                    if (Totalh > 600)
                    {
                        TotalCon = Convert.ToDecimal(dt.Rows[0]["TierICityINR"]);
                        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                        {
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
                            }
                        }
                    }
                    else
                    {
                        // TotalCon = Convert.ToDecimal(dt.Rows[0]["Lessthan8Hours50TierI"]);
                        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 1)
                        {
                            TotalCon = Convert.ToDecimal(dt.Rows[0]["TierICityINR"]) / 2;
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                            {
                                TotalCon = 0;
                            }

                        }
                    }

                }


                if (Convert.ToInt32(ddlcity.SelectedValue) == 3)
                {
                    if (Totalh > 600)
                    {
                        TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIIICityINR"]);
                        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                        {
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 75;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 4)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 25;
                            }
                        }
                    }
                    else
                    {
                        // TotalCon = Convert.ToDecimal(dt.Rows[0]["Lessthan8Hours50TierI"]);
                        if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3 || Convert.ToInt32(ddlMealArrangement.SelectedValue) == 1)
                        {
                            TotalCon = Convert.ToDecimal(dt.Rows[0]["TierIIICityINR"]) / 2;
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 2)
                            {
                                TotalCon = (Convert.ToDecimal(TotalCon) / 100) * 50;
                            }
                            if (Convert.ToInt32(ddlMealArrangement.SelectedValue) == 3)
                            {
                                TotalCon = 0;
                            }

                        }
                    }

                }

                int FinalCon2 = Convert.ToInt32(Math.Round(TotalCon));
                txtPerDim.Text = FinalCon2.ToString();
            }
        }


        return true;
    }

    protected void Submit(object sender, EventArgs e)
    {
        if (txtSTime.Text != "" && txtTTime.Text != "" && txtDate.Text != "")
        {
            Int32 dHours = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Hours;
            Int32 dMins = (Convert.ToDateTime(txtTTime.Text) - Convert.ToDateTime(txtSTime.Text)).Minutes;
            if (Convert.ToDecimal(dMins) >= 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Valid Time')</script>", false);
                return;
            }
            string retStr = dHours.ToString() + "." + dMins.ToString();
            decimal Totalh = Convert.ToDecimal(retStr);

            DataTable dtDim = null;
            decimal TotalhEdit = 0;
            if (lblEditUUniqecode.Text.Length > 5)
            {
                string strQry7 = " Select isnull(sum(convert(decimal,totalhours)),0) TotalHours FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
                dtDim = objMain.LoadData(strQry7);
                if (dtDim.Rows.Count > 0)
                {
                    TotalhEdit = Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);

                }
            }
            else
            {
                string strQry7 = " Select isnull(sum(convert(decimal,totalhours)),0) TotalHours  FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and VisitType=2  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and  DeleteFlag=1";
                dtDim = objMain.LoadData(strQry7);
                if (dtDim.Rows.Count > 0)
                {
                    TotalhEdit += Convert.ToDecimal(dtDim.Rows[0]["TotalHours"]);
                    //ddlMealArrangement.SelectedValue = dtDim.Rows[0]["MealArrangement"].ToString();
                }
            }
            Totalh = Totalh + TotalhEdit;
            lblPerDim.Text = Totalh.ToString();

            if (lblEditUUniqecode.Text.Length > 5)
            {

            }

            else
            {

                if (Totalh > 8)
                {
                    objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T9' ", "LookupCode", "asc", ddlMealArrangement, "description", "LookupCode", "--Select--");
                }
                else
                {
                    objComman.BindDLL("mstLookup", "LookupCode, description", " LookupFlag='T9' and LookupCode not in(4) ", "LookupCode", "asc", ddlMealArrangement, "description", "LookupCode", "--Select--");
                }
            }
        }
    }
    public void chkEnty_click(object sender, EventArgs e)
    {
        if (chkENtry.Checked == true)
        {
            if (txtSTime.Text == "" || txtTTime.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Valid Time')</script>", false);
                chkENtry.Checked = false;
                txtTTime.Text = "";
                return;
            }

            if (txtDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date')</script>", false);
                txtTTime.Text = "";
                chkENtry.Checked = false;
                return;
            }
            if (ddlcity.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select City Type')</script>", false);
                txtTTime.Text = "";
                chkENtry.Checked = false;
                return;
            }


            string strQry11 = " Select  * FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and IsPerdim=1 and  DeleteFlag=1";
            DataTable dtChheck = objMain.LoadData(strQry11);
            if (dtChheck.Rows.Count > 0)
            {
                ddlMealArrangement.SelectedIndex = 0;
                chkENtry.Checked = false;

                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Last visit already add')</script>", false);
                return;
            }

        }
        else
        {
            ddlMealArrangement.SelectedIndex = 0;

        }
    }

    public void txt_kmclick(object sender, EventArgs e)
    {
        if (txtDate.Text != "")
        {
            if (Convert.ToInt32(ddlType.SelectedValue) == 2 && Convert.ToInt32(ddlMode.SelectedValue) == 1)
            {
                if (txtKM.Text != "")
                {
                    if (Convert.ToInt32(txtKM.Text) > 100)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Plese Enter Less then 100 KM')</script>", false);
                        txtTotalFare.Text = "";
                        txtKM.Text = "";
                        return;

                    }
                    else
                    {

                        int Total = Convert.ToInt32(txtKM.Text) * 4;
                        txtTotalFare.Text = Total.ToString();

                    }
                }
            }

            DataTable StartCreateDataVillage = Session["StartCreateDataVillage"] as DataTable;
            if (Session["StartCreateDataVillage"] != null)
            {
                string fff = StartCreateDataVillage.Rows[0]["FromTierType"].ToString();
                if (fff != "")
                {
                    int TotalKM = 0;
                    int TotalMinKM = 0;
                    if (txtKM.Text != "")
                    {
                        TotalKM = Convert.ToInt32(txtKM.Text);
                    }
                    // Calculate the difference


                    string strQry8 = " Select  isnull(KMAdmin),0) KMAdmin FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
                    DataTable dtKm = objMain.LoadData(strQry8);
                    if (dtKm.Rows.Count > 0)
                    {
                        TotalMinKM = TotalKM + Convert.ToInt32(dtKm.Rows[0]["KMAdmin"]);
                    }
                    else
                    {
                        TotalMinKM = TotalKM;

                    }
                    if (TotalMinKM > 60)
                    {
                        ddlcity.SelectedValue = StartCreateDataVillage.Rows[0]["FromTierType"].ToString();
                    }
                    else
                    {
                        chkENtry.Checked = false;
                        ddlcity.SelectedIndex = 0;
                    }
                }
                else
                {
                    int TotalKM = 0;
                    int TotalMinKM = 0;
                    if (txtKM.Text != "")
                    {
                        TotalKM = Convert.ToInt32(txtKM.Text);
                    }
                    string strQry8 = " Select  isnull(KMAdmin),0) KMAdmin FROM [tblTravelMatrixDeatils2024] where[UserID] = '" + Convert.ToString(Session["FC"]) + "' and [mYear] = '" + lblyear.Text + "'and [mMonth] = '" + Convert.ToString(Session["TMonth"]) + "'  and[TravelDate] = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyy-MM-dd") + "' and VisitType=2 and UniqueCode<>'" + lblEditUUniqecode.Text + "' and  DeleteFlag=1";
                    DataTable dtKm = objMain.LoadData(strQry8);
                    if (dtKm.Rows.Count > 0)
                    {
                        TotalMinKM = TotalKM + Convert.ToInt32(dtKm.Rows[0]["KMAdmin"]);
                    }
                    else
                    {
                        TotalMinKM = TotalKM;

                    }
                    if (TotalMinKM > 60)
                    {


                        ddlcity.SelectedValue = "2";
                    }
                    else
                    {
                        chkENtry.Checked = false;
                        ddlcity.SelectedIndex = 0;
                    }

                }
            }
        }



    }
}