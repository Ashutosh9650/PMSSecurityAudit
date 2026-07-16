using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class frmLSE : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;
    public bool edit_status = false;
    string conditions = "";
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {
                Div4.Visible = false;
                Div5.Visible = false;
                Div6.Visible = false;
                LoadYear();
                LoadUserLeavel();
                //UserLevelFilter();
                clsMain.LSEFormID = Convert.ToString(Session["username"]);
                //FillEduStauts();
                ViewState["1"] = "ss";
                //  LoadUploadImage();

            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);

        // ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction();", true);
        // ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "loadJSFunction1();", true);
    }
    public void LoadYear()
    {

        DataTable dtYear = objComman.Generate_Financial_Year();
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    public void LoadUploadImage()
    {

        string url = "http://89.116.20.47:8080/upload";
        string sFileDir = Server.MapPath(Comman.GetImagePath("LSEPath") + "/");
        string filePath = sFileDir + "IMGWA0160.jpg"; // Change this to the path of your image file

        try
        {
            // Create the HttpWebRequest for the URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=---------------------------boundary";

            // Create the multipart form-data body
            string boundary = "---------------------------boundary";
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] trailerBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            // Header for the file part
            StringBuilder header = new StringBuilder();
            header.Append("Content-Disposition: form-data; name=\"files\"; filename=\"");
            header.Append(Path.GetFileName(filePath));
            header.Append("\"\r\n");
            header.Append("Content-Type: image/jpeg\r\n\r\n");  // Change Content-Type based on the image format
            byte[] headerBytes = Encoding.ASCII.GetBytes(header.ToString());

            // Read the file into a byte array
            byte[] fileBytes;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fileBytes = new byte[fileStream.Length];
                fileStream.Read(fileBytes, 0, fileBytes.Length);
            }

            // Calculate the content length
            request.ContentLength = boundaryBytes.Length + headerBytes.Length + fileBytes.Length + trailerBytes.Length;

            // Write the request body to the request stream
            using (Stream requestStream = request.GetRequestStream())
            {
                // Write boundary
                requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);

                // Write header
                requestStream.Write(headerBytes, 0, headerBytes.Length);

                // Write file content
                requestStream.Write(fileBytes, 0, fileBytes.Length);

                // Write trailer (end boundary)
                requestStream.Write(trailerBytes, 0, trailerBytes.Length);
            }

            // Get the response from the server
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    Console.WriteLine("Response: " + responseText);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        string appId = "<App Id>";
        string json = "";
        string url5 = string.Format("http://89.116.20.47:8080/run-script", appId);
        using (WebClient client = new WebClient())
        {
            json = client.DownloadString(url5);

        }
        string json1 = "";
        string url51 = string.Format("http://89.116.20.47:8080/result", appId);
        using (WebClient client = new WebClient())
        {
            json1 = client.DownloadString(url51);

        }


    }
    public void Jasoncode()
    {
        string appId = "<App Id>";
        string json = "";
        string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", "", appId);
        using (WebClient client = new WebClient())
        {
            json = client.DownloadString(url);

        }


        DataSet dsMyData = new DataSet();
        XmlDocument xdMyData = new XmlDocument();
        json = "{ \"rootNode\": {" + json.Trim().TrimStart('{').TrimEnd('}') + "} }";
        xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(json);
        dsMyData.ReadXml(new XmlNodeReader(xdMyData));
        if (dsMyData.Tables.Count >= 1)
        {
            //DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2024New");
            //DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
            //DataSet dsResult = new DataSet();
            //dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2025(DttblEnrolment_Temp);
            //sReturn = JsonConvert.SerializeObject(dsResult);
        }



    }
    public void LoadOutComeSpicify()
    {
        string conditions = " ";

        //  objComman.BindDLL("mstOutcomeSpecific", "sOutcomeID,sOutcomeName ", "OutcomeID=" + ddlOutcomde.SelectedValue + " and ActiveStatus=1", "sOutcomeID", "asc", ddlSpecific, "sOutcomeName", "sOutcomeID", "--Select--");



    }
    public void FillCBDist()
    {

        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "StateCode ='" + ddlState.SelectedValue + "' and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }
        if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = " mst2District.StateCode ='" + ddlState.SelectedValue + "' and UserName='" + Session["username"].ToString() + "' ";
            string strQry1 = "       sELECT distinct mst2District.DistrictCode as DistrictCode, dbo.TitleCase(upper(DistrictName))  as DistrictName FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + conditions + "  and  Fyear='" + ddlYear.SelectedItem.Text + "' order by DistrictName  ";
            DataTable dtDistrict = objMain.LoadData(strQry1);

            objComman.BindDLLDatatable("mst2District", dtDistrict, "DistrictCode, dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "Desc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

        }
        else
        {

            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");

        }

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

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
            objComman.BindDLLDatatable("mst1State", dtAllState, "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");


        }

    }
    public void LoadUserLeavel()
    {
        AlllStateCode();
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 0;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
        }
        else
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            //objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = false;
            ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            //conditions = "StateCode ='" + ddlState.SelectedValue + "'  and Fyear= '" + ddlYear.SelectedItem.Text + "'  ";
            //objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

            ddlDistrict.SelectedIndex = 0;

            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }

        else
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and Fyear= '" + ddlYear.SelectedItem.Text + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");
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
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            //ddlDistrict.SelectedIndex = 1;
            //conditions = "";
            //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and BlockCode ='" + Session["BlockCode"].ToString() + "' ";
            //objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName  ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");

        }





    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();

    }
    protected void ddlFC_SelectedIndexChanged(object sender, EventArgs e)
    {

        gvWeallyDatewise.DataSource = null;
        gvWeallyDatewise.DataBind();
        gvWeeklly.DataSource = null;
        gvWeeklly.DataBind();
        ddlMonth.SelectedIndex = 0;
        ddlMonth_SelectedIndexChanged(ddlMonth, null);

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {

        FillCBBock();
    }
    public void FillCBBock()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {

            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        else if (Session["user_level_Role"].ToString() == "4")
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( '" + Session["NewBlockCode"].ToString() + "' ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");


        if (Session["user_level_Role"].ToString() == "4")
        {
            ddlBlock.SelectedIndex = 1;
            ddlBlock.Enabled = false;
            ddlBlock_SelectedIndexChanged(ddlDistrict, null);
        }
        else
        {

        }
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSchool();
    }
    public void FillSchool()
    {
        string strQry = "Select SchoolCode,Name from mstSchool  where VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "' and balval=1 and LSG=1 ";

        DataTable dtSchool = objMain.LoadData(strQry);

        objComman.BindDLLMasterTable("mstSchool", "SchoolCode,Name", dtSchool, conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");




        //conditions = "";
        //conditions = "VillageCode ='" + ddlVillage.SelectedValue + "'  and FYear ='" + ddlYear.SelectedItem.Text + "'";
        //objComman.BindDLL("mstSchool", "SchoolCode,Name", conditions, "Name", "asc", ddlSchool, "Name", "SchoolCode", "Select");


    }
    public void FillCVillage()
    {
        conditions = "";
        //conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "' and  PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";
        //objComman.BindDLL("mst5Village", "VillageCode,dbo.TitleCase(upper(VillageName)) as VillageName", conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");

        if (ddlPanchayat.SelectedValue.ToString() == "1")
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "'  ";

        }
        else
        {
            conditions = "mst5Village.DistrictCode ='" + ddlDistrict.SelectedValue + "'  and mst5Village.BlockCode ='" + ddlBlock.SelectedValue + "' and  mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue + "'  ";

        }

        string strQry = "  SELECT mst5Village.VillageCode, dbo.TitleCase(upper((mst5Village.VillageName))) + ' (' + dbo.TitleCase(upper(mstPanchayat.PanchayatName)) +')'   as VillageName FROM mst5Village INNER JOIN mstPanchayat ON mst5Village.PanchayatCode = mstPanchayat.PanchayatCode where " + conditions + "  order by VillageName   ";
        DataTable dtVillage = objMain.LoadData(strQry);

        objComman.BindDLLMasterTableVillage("mst5Village", "VillageName,VillageCode", dtVillage, conditions, "VillageName", "asc", ddlVillage, "VillageName", "VillageCode", "Select");


    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


    protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
        string con = "";


        if (ddlBlock.SelectedIndex > 0)
        {
            con = " and BlockCOde='" + ddlBlock.SelectedValue + "'";
        }
        if (ddlUser.SelectedIndex > 0)
        {
            con += " and Username='" + ddlUser.SelectedValue + "'";
        }
        SqlParameter[] parm1 = new SqlParameter[]
          {

               new SqlParameter("@Con",  con),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                      new SqlParameter("@Week", ddlWeeklly.SelectedValue),
          };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactWeelllyReport", parm1);
        int Icount;
        if (Convert.ToInt32(ddlMonth.SelectedValue) == 1 || Convert.ToInt32(ddlMonth.SelectedValue) == 2 || Convert.ToInt32(ddlMonth.SelectedValue) == 3)
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        }
        else
        {
            Icount = Convert.ToInt32(ddlYear.SelectedValue);
        }
        SqlParameter[] parm2 = new SqlParameter[]
               {
              new SqlParameter("@Year",Icount),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                  new SqlParameter("@Flag", ddlWeeklly.SelectedValue),

            };


        DataTable dtmin = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadWeekDropdown", parm2);


        gvWeallyDatewise.DataSource = null;
        gvWeallyDatewise.DataBind();
    }
    protected void Lnkdelete_OnClick(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        int res1 = DeleteEnrollMentData(UniqueChildCode);

        if (res1 > 0)
        {
            LoadDate(lblEditUserName.Text);
            ScriptManager.RegisterStartupScript(this, GetType(), "importingdone", "alert('Record Deleted');", true);

        }


    }
    public int DeleteEnrollMentData(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),

            new SqlParameter("@UserName",  Session["username"].ToString() )
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteWeekPlan", cmdParameters);
    }
    protected void LnkBtnBlock_OnClick(object sender, EventArgs e)
    {
        ImageButton bt = (ImageButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string UniqueChildCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        string lblVillagecode = (gvr.FindControl("Lvillagecode") as Label).Text;
        objComman.BindDLL("mst5Village", "Villagecode, VillageName ", "Villagecode='" + lblVillagecode + "' ", "VillageName", "asc", ddlVillage, "VillageName", "Villagecode", "--Select--");

        ddlVillage.SelectedIndex = 1;



        lblEditUniquePlanCode.Text = UniqueChildCode;
        string strQry2 = " Select * FROM [tblPlanActivity] where [UniquePlanCode]='" + UniqueChildCode + "' ";
        DataTable dtSer = objMain.LoadData(strQry2);

        DataTable dtmstM = objMain.LoadData(" SELECT TBCode, TBName FROM [dbo].[mstTeamBalika] inner join mst5Village on mst5Village.VillageCode=mstTeamBalika.VillageCode 	or  mst5Village.refVillage16=mstTeamBalika.VillageCode	or  mst5Village.refVillage17=mstTeamBalika.VillageCode	or  mst5Village.refVillage18=mstTeamBalika.VillageCode	or  mst5Village.refVillage19=mstTeamBalika.VillageCode	or  mst5Village.refVillage20=mstTeamBalika.VillageCode	 	or  mst5Village.refVillage21=mstTeamBalika.VillageCode or  mst5Village.refVillage22=mstTeamBalika.VillageCode	or  mst5Village.refVillage23=mstTeamBalika.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode where mst5Village.villagecode= '" + lblVillagecode + "' ");


    }
    protected void CBContacts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CBContacts1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlOutcomde_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    public void LoadDate(string username)
    {
        string con = "";

        SqlParameter[] parm1 = new SqlParameter[]
          {

               new SqlParameter("@CreateBy",  username),
                 new SqlParameter("@month", ddlMonth.SelectedValue),
                      new SqlParameter("@Week", ddlWeeklly.SelectedValue),
          };


        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptContactWeellyDatewiseReport", parm1);



        if (dt.Rows.Count > 0)
        {
            gvWeallyDatewise.DataSource = dt;
            gvWeallyDatewise.DataBind();
        }
        else
        {
            gvWeallyDatewise.DataSource = null;
            gvWeallyDatewise.DataBind();
        }
    }
    protected void gvnroll_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Label lblUniqueChildCode = (Label)e.Row.FindControl("lblUniqueChildCode");

            //ImageButton lbtn = (ImageButton)e.Row.FindControl("ImgAcc");
            //lbtn.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
            Label lblStatus1 = (Label)e.Row.FindControl("lblStatus1");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");



            //e.Row.Cells[3].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
            if (lblStatus1.Text == "0")
            {
                lblStatus.Text = "Pending";
                lblStatus.ForeColor = System.Drawing.Color.Red;

            }
            else if (lblStatus1.Text == "1")
            {
                lblStatus.Text = "Approved";
                lblStatus.ForeColor = System.Drawing.Color.Green;

            }
            else
            {
                lblStatus.Text = "";

            }
        }
    }

    protected void gvnroll1_OnRowCommand3(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            RadioButtonList LinkButton1 = (RadioButtonList)e.Row.FindControl("rblScore");
            Label lblVillagename = (Label)e.Row.FindControl("lblVillagename");
            if (lblVillagename.Text == "" || lblVillagename.Text == "0")
            {

            }
            else
            {
                LinkButton1.SelectedValue = lblVillagename.Text;
            }

            //ImageButton LinkBut51 = (ImageButton)e.Row.FindControl("LinkBut51");
            //LinkBut51.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");

        }
    }

    protected void gvnroll1_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton LinkButton1 = (ImageButton)e.Row.FindControl("ImgShow");
            Label lblAnswerSheetPhoto = (Label)e.Row.FindControl("lblAnswerSheetPhoto");
            Label lblAttendanceStatus = (Label)e.Row.FindControl("lblAttendanceStatus");

            DropDownList ddlAttStatus = (DropDownList)e.Row.FindControl("ddlAttStatus");
            Label lblPresent = (Label)e.Row.FindControl("lblPresent");
            ImageButton lnkd = (ImageButton)e.Row.FindControl("lnkd");
            LinkButton Button1 = (LinkButton)e.Row.FindControl("Button1");




            //ImageButton LinkBut51 = (ImageButton)e.Row.FindControl("LinkBut51");
            //LinkBut51.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");

            if (lblAnswerSheetPhoto.Text.Length > 5)
            {
                LinkButton1.Visible = true;
                Button1.Visible = true;
                lnkd.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
                Button1.Visible = false;
                lnkd.Visible = false;

            }
            ddlAttStatus.SelectedValue = lblPresent.Text;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {



    }
    protected void btnsaveScore_Click(object sender, EventArgs e)
    {
        string Score = "";
        int icount = 0;
        if (Convert.ToString(Session["Session"]) == "B")
        {
            if (FileuploadAttach.Visible == true)
            {
                if (clsMain.LSEImageID.Length > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Image')</script>", false);
                    MpexdrDistrict1.Show();
                    return;
                }

            }
            for (int i = 0; i < gvTopvillage.Rows.Count; i++)
            {
                RadioButtonList rblScore = (RadioButtonList)gvTopvillage.Rows[i].FindControl("rblScore");
                if (rblScore.SelectedValue != "")
                {
                    Score += rblScore.SelectedValue + ",";
                    icount = icount + 1;
                }
                else
                {
                    Score += "0,";
                }
            }
            if (Score.Length > 0)
            {
                Score = Score.Substring(0, Score.LastIndexOf(","));
            }
            if (icount < 30)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select atleast 30 Question')</script>", false);
                MpexdrDistrict1.Show();
                return;
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                Label lblUniquePlanCode = (Label)GridView1.Rows[i].FindControl("lblUniquePlanCode");

                Label lblAnswerSheetPhoto = (Label)GridView1.Rows[i].FindControl("lblAnswerSheetPhoto");
                Label lblOMRAnswersEdit = (Label)GridView1.Rows[i].FindControl("lblOMRAnswersEdit");
                if (lblUniquePlanCode.Text == hdnMKID4.Value)
                {
                    if (FileuploadAttach.Visible == true)
                    {
                        lblAnswerSheetPhoto.Text = clsMain.LSEImageID;
                    }

                    lblOMRAnswersEdit.Text = Score;
                }
            }
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

        }
        else
        {
            if (FileuploadAttach.Visible == true)
            {
                if (clsMain.LSEImageID.Length > 0)
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Image')</script>", false);
                    MpexdrDistrict1.Show();
                    return;
                }

            }
            for (int i = 0; i < gvTopvillage.Rows.Count; i++)
            {
                RadioButtonList rblScore = (RadioButtonList)gvTopvillage.Rows[i].FindControl("rblScore");
                if (rblScore.SelectedValue != "")
                {
                    Score += rblScore.SelectedValue + ",";
                    icount = icount + 1;
                }
                else
                {
                    Score += "0,";
                }
            }
            if (Score.Length > 0)
            {
                Score = Score.Substring(0, Score.LastIndexOf(","));
            }
            if (Convert.ToInt32(hdnMKID6.Value) == 1)
            { }
            else
            {
                if (icount < 30)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select atleast 30 Question')</script>", false);
                    MpexdrDistrict1.Show();
                    return;
                }
            }
            string img1 = "";
            if (FileuploadAttach.Visible == true)
            {
                img1 = clsMain.LSEImageID;
            }

            int icoun = InsertUpdateLSE(hdnMKID4.Value, clsMain.LSEImageID, Score, hdnMKID5.Value, Convert.ToInt32(hdnMKID6.Value));
            if (icoun > 0)
            {
                for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
                {

                    Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");

                    Label lblAnswerSheetPhoto = (Label)gvWeallyDatewise.Rows[i].FindControl("lblAnswerSheetPhoto");
                    Label lblOMRAnswersEdit = (Label)gvWeallyDatewise.Rows[i].FindControl("lblOMRAnswersEdit");
                    if (lblUniquePlanCode.Text == hdnMKID4.Value)
                    {
                        if (FileuploadAttach.Visible == true)
                        {
                            lblAnswerSheetPhoto.Text = clsMain.LSEImageID;
                        }

                        lblOMRAnswersEdit.Text = Score;
                    }
                }
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);

            }
        }
    }
    protected void btnFinalSave_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Session"]) == "B")
        {
            if (txtFromDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Date.')</script>", false);
                return;
            }

            DateTime dt = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (txtFromDate.Text != "" && dt > DateTime.Today)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Should not be future date.')</script>", false);
                return;
            }
            if (rblTBFC.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Activity.')</script>", false);
                return;
            }
            if (ddlTbFC.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select TB/FC Name')</script>", false);
                return;
            }
            if (ddlWeeklly.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Session')</script>", false);
                return;
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddlAttStatus = (DropDownList)GridView1.Rows[i].FindControl("ddlAttStatus");
                Label lblAnswerSheetPhoto = (Label)GridView1.Rows[i].FindControl("lblAnswerSheetPhoto");
                string lblOMRAnswersEdit = (GridView1.Rows[i].FindControl("lblOMRAnswersEdit") as Label).Text;

                if (ddlAttStatus.SelectedValue == "1")
                {

                    if (lblAnswerSheetPhoto.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Image.')</script>", false);
                        return;
                    }
                    if (lblOMRAnswersEdit.Length > 2)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please entry question.')</script>", false);
                        return;
                    }
                }
                if (ddlAttStatus.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Attendence Status.')</script>", false);
                    return;
                }
            }
            int iScount = 0;
            string FC = "";
            string TB = "";
            if (rblTBFC.SelectedValue == "1")
            {
                TB = ddlTbFC.SelectedValue;
            }
            if (rblTBFC.SelectedValue == "2")
            {
                FC = ddlTbFC.SelectedValue;
            }
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddlAttStatus = (DropDownList)GridView1.Rows[i].FindControl("ddlAttStatus");
                Label lblOMRAnswersEdit = (Label)GridView1.Rows[i].FindControl("lblOMRAnswersEdit");
                Label lblUniquePlanCode = (Label)GridView1.Rows[i].FindControl("lblUniquePlanCode");
                Label lblUniqueChildRCode = (Label)GridView1.Rows[i].FindControl("lblUniqueChildRCode");
                Label lblAnswerSheetPhoto = (Label)GridView1.Rows[i].FindControl("lblAnswerSheetPhoto");
                string img = "";
                string OMRAnswersEdit = "";
                if (ddlAttStatus.SelectedValue == "1")
                {
                    OMRAnswersEdit = lblOMRAnswersEdit.Text;
                    img = lblAnswerSheetPhoto.Text;
                }
                iScount = InsertUpdateFinal(lblUniquePlanCode.Text, lblUniqueChildRCode.Text, ddlVillage.SelectedValue, rblTBFC.SelectedValue, FC, TB, Convert.ToDateTime(txtFromDate.Text), ddlWeeklly.SelectedValue, Session["username"].ToString(), ddlSchool.SelectedValue, ddlAttStatus.SelectedValue, OMRAnswersEdit, img);
            }
            if (iScount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnApprove_Click(LinkButton1, null);

            }
        }
        else
        {
            for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
            {
                DropDownList ddlAttStatus = (DropDownList)gvWeallyDatewise.Rows[i].FindControl("ddlAttStatus");
                Label lblAnswerSheetPhoto = (Label)gvWeallyDatewise.Rows[i].FindControl("lblAnswerSheetPhoto");
                string lblOMRAnswersEdit = (gvWeallyDatewise.Rows[i].FindControl("lblOMRAnswersEdit") as Label).Text;
                if (ddlAttStatus.SelectedValue == "1")
                {
                    if (lblAnswerSheetPhoto.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Image.')</script>", false);
                        return;
                    }
                    if (lblOMRAnswersEdit.Length > 2)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please entry question.')</script>", false);
                        return;
                    }
                }
                if (ddlAttStatus.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select Attendence Status.')</script>", false);
                    return;
                }
            }
            int icount = 0;
            for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
            {
                DropDownList ddlAttStatus = (DropDownList)gvWeallyDatewise.Rows[i].FindControl("ddlAttStatus");
                Label lblOMRAnswersEdit = (Label)gvWeallyDatewise.Rows[i].FindControl("lblOMRAnswersEdit");
                Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");
                icount = InsertUpdateLSEFinal(lblUniquePlanCode.Text, ddlAttStatus.SelectedValue);
            }
            if (icount > 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                btnApprove_Click(LinkButton1, null);
            }
        }
    }
    public int InsertUpdateLSEFinal(string Tarining_ID, string IMG)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "InsertandUpdateLSEFinal";
        dbSqlCommand.Parameters.Add("@Uniqecode", SqlDbType.VarChar).Value = Tarining_ID;
        dbSqlCommand.Parameters.Add("@Present", SqlDbType.VarChar).Value = IMG;


        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }
    public int InsertUpdateFinal(string Uniqecode, string UniqueChildRCode, string VillageCode, string ActivityDoneBy, string FCName, string TBCode, DateTime AttDate, string sseion, string CreateBy, string SchoolCode, string Present, string Assessment, string AnswerSheetPhoto)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "InsertandLSEFinal";
        dbSqlCommand.Parameters.Add("@Uniqecode", SqlDbType.VarChar).Value = Uniqecode;
        dbSqlCommand.Parameters.Add("@UniqueChildRCode", SqlDbType.VarChar).Value = UniqueChildRCode;
        dbSqlCommand.Parameters.Add("@VillageCode", SqlDbType.VarChar).Value = VillageCode;
        dbSqlCommand.Parameters.Add("@ActivityDoneBy", SqlDbType.VarChar).Value = ActivityDoneBy;
        dbSqlCommand.Parameters.Add("@FCName", SqlDbType.VarChar).Value = FCName;
        dbSqlCommand.Parameters.Add("@TBCode", SqlDbType.VarChar).Value = TBCode;
        dbSqlCommand.Parameters.Add("@AttDate", SqlDbType.Date).Value = AttDate.ToString("yyyy-MM-dd");
        dbSqlCommand.Parameters.Add("@Session", SqlDbType.VarChar).Value = sseion;
        dbSqlCommand.Parameters.Add("@CreateBy", SqlDbType.VarChar).Value = CreateBy;
        dbSqlCommand.Parameters.Add("@SchoolCode", SqlDbType.VarChar).Value = SchoolCode;
        dbSqlCommand.Parameters.Add("@Present", SqlDbType.VarChar).Value = Present;

        dbSqlCommand.Parameters.Add("@Assessment", SqlDbType.VarChar).Value = Assessment;
        dbSqlCommand.Parameters.Add("@AnswerSheetPhoto", SqlDbType.VarChar).Value = AnswerSheetPhoto;



        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {

        //for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
        //{
        //    Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");
        //    CheckBox chkdel = (CheckBox)gvWeallyDatewise.Rows[i].FindControl("chkdel");
        //    if (chkdel.Checked==true)
        //    {
        //        icount = icount + 1;
        //    }
        //}
        //if (gvWeallyDatewise.Rows.Count==icount)
        //{

        rblTBFC.ClearSelection();

        string Con = " and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";

        if (ddlVillage.SelectedIndex > 0)
        {
            Con += " and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ";
        }

        if (ddlSchool.SelectedIndex > 0)
        {
            Con += " and tblChildAttendanceLifeskill.Schoolcode='" + ddlSchool.SelectedValue + "' ";
        }

        if (ddlWeeklly.SelectedIndex > 0)
        {
            Con += " and tblChildAttendanceLifeskill.Session='" + ddlWeeklly.SelectedValue + "' ";
        }
        if (ddlSchool.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select School.')</script>", false);
            return;
        }
        if (ddlWeeklly.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Session.')</script>", false);
            return;
        }


        if (ddlWeeklly.SelectedValue == "243")
        {

            DataTable dtmstMos = objMain.LoadData(" select * from tblChildAttendanceLifeskill where  schoolcode='" + ddlSchool.SelectedValue + "' and session='" + ddlWeeklly.SelectedValue + "' ");
            if (dtmstMos.Rows.Count > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter previous session')</script>", false);
                return;
            }
        }

        SqlParameter[] parm1 = new SqlParameter[]
        {
            new SqlParameter("@Con",Con),

        };

        txtFromDate.Text = "";
        ddlTbFC.Items.Clear();
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadLSEImage", parm1);

        if (dt.Rows.Count > 0)
        {
            gvWeallyDatewise.Visible = true;
            gvWeallyDatewise.DataSource = dt;
            gvWeallyDatewise.DataBind();
            LinkButton2.Visible = true;
            GridView1.Visible = false;
            Div4.Visible = false;
            Div5.Visible = false;
            Div6.Visible = false;
            Session["Session"] = "A";
        }
        else
        {
            gvWeallyDatewise.Visible = false;
            gvWeallyDatewise.DataSource = null;
            gvWeallyDatewise.DataBind();


            string Con1 = " and mst5Village.Fyear='" + ddlYear.SelectedItem.Text + "' ";

            if (ddlVillage.SelectedIndex > 0)
            {
                Con1 += " and mst5Village.VillageCode='" + ddlVillage.SelectedValue + "' ";
            }

            if (ddlSchool.SelectedIndex > 0)
            {
                Con1 += " and tblChildRegistrationBalsabha.Schoolcode='" + ddlSchool.SelectedValue + "' ";
            }




            SqlParameter[] parm11 = new SqlParameter[]
              {
                    new SqlParameter("@Con",Con1),
                    new SqlParameter("@ScoolCode",ddlSchool.SelectedValue),
                    new SqlParameter("@Session",ddlWeeklly.SelectedValue),

              };


            DataTable dt1 = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptAddNewBalsaba", parm11);
            if (dt1.Rows.Count > 6)
            {
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                Div4.Visible = true;
                Div5.Visible = true;
                Div6.Visible = true;
                Session["Session"] = "B";
                LinkButton2.Visible = true;
            }
            GridView1.Visible = true;
        }
        Locking();
        //if (dt.Tables[1].Rows.Count>0)
        //{
        //    if (Convert.ToInt32( dt.Tables[1].Rows[0]["IsHours"]) != Convert.ToInt32(dt.Tables[0].Rows[0]["TotalHH"]))
        //    {
        //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please add plans for each day of the week to approve the plan.')</script>", false);
        //        return;
        //    }
        //}

        //for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
        //{
        //    Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");
        //    CheckBox chkdel = (CheckBox)gvWeallyDatewise.Rows[i].FindControl("chkdel");
        //    icountr = SaveDataApprove(lblUniquePlanCode.Text, Session["username"].ToString());
        //}
        //if (icountr > 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Approve Sucessfully')</script>", false);
        //    gvWeallyDatewise.DataSource = null;
        //    gvWeallyDatewise.DataBind();
        //    ddlWeek_SelectedIndexChanged(ddlActivity, null);
        //}
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select All')</script>", false);

        //}
    }
    public void Locking()
    {
        if (ddlYear.SelectedIndex > 0)
        {


            string strQry;

            strQry = "Select * from mstModuleLocking  where [FromName]='LSE' and DistrictCode='" + ddlDistrict.SelectedValue + "' and Fyear='" + ddlYear.SelectedItem.Text + "' ";


            string Year = ddlYear.SelectedItem.Text;
            string[] Year1 = Year.Split('-');



            DateTime date1;
            DateTime date2;
            DataTable dtModel = objMain.LoadData(strQry);
            if (dtModel.Rows.Count > 0)
            {


                date1 = Convert.ToDateTime(dtModel.Rows[0]["lockdate"].ToString());
                date2 = DateTime.Now.Date;





                if (date2 > date1)
                {




                    LinkButton2.Enabled = false;



                }
            }


        }
    }
    public int SaveDataApprove(string UniqueCode, string CreateBy)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),

                 new SqlParameter("@Createby", CreateBy),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateContactWeeklyApprove", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    public int SaveDataInsertUpdate(string UniqueCode, string Plandate, string ActivityID, string SupportBy, string BO, string TB, string OOSC, string Remark, string CreateBy, string Meeting, string Travel, string Holiday, string Leave, string Outcome, string SpecificOutcome, string IsHours)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@Villagecode", ddlVillage.SelectedValue),
            new SqlParameter("@PlanDate", Plandate),
            new SqlParameter("@ActivityID", ActivityID),
            new SqlParameter("@SupportBy", SupportBy),
            new SqlParameter("@BOCode", BO),
            new SqlParameter("@TBCode", TB),
            new SqlParameter("@OOSG", OOSC),
            new SqlParameter("@Remark", Remark),
                 new SqlParameter("@Createby", lblEditUserName.Text),
              new SqlParameter("@Meeting", Meeting),
               new SqlParameter("@Travel", Travel),
                new SqlParameter("@Holiday ", Holiday ),
                 new SqlParameter("@Leave", Leave),
                  new SqlParameter("@Outcome", Outcome),
                   new SqlParameter("@SpecificOutcome ", SpecificOutcome ),
                    new SqlParameter("@IsHalfDay ", "" ),
                       new SqlParameter("@IsHours ",IsHours),
                     new SqlParameter("@Week ", ddlWeeklly.SelectedValue ),
                       new SqlParameter("@Month ", ddlMonth.SelectedValue ),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateContactWeekly", cmdParameters);
        }
        catch
        {

        }
        return Icount;
    }
    protected void ImgView_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string lblUniquePlanCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        string lblAnswerSheetPhoto = (gvr.FindControl("lblOMRAnswersEdit") as Label).Text;
        DropDownList ddlAttStatus = (gvr.FindControl("ddlAttStatus") as DropDownList);
        string lblIsweb = (gvr.FindControl("lblIsweb") as Label).Text;

        hdnMKID4.Value = lblUniquePlanCode;
        hdnMKID5.Value = ddlAttStatus.SelectedValue;
        hdnMKID6.Value = lblIsweb;
        clsMain.LSEImageID = "";
        DataTable dtmstMos = objMain.LoadData(" select 0 srNo, 0 Flag, LTRIM(dataTable) as Score   from [dbo].[SplitStringLSE]((select OMRAnswersEdit from tblChildAttendanceLifeskill where Present=1 and UniqueCode='" + lblUniquePlanCode + "' and len(OMRAnswersEdit)>4))");
        DataTable dtmstMos1 = objMain.LoadData(" select 0 srNo,0  Flag, LTRIM(dataTable) as Score   from [dbo].[SplitStringLSE]((select Assessment from tblChildAttendanceLifeskill where Present=1 and UniqueCode='" + lblUniquePlanCode + "' and len(OMRAnswersEdit)>4))");

        if (dtmstMos.Rows.Count > 0)
        {
            int icount = 0;
            foreach (DataRow dr in dtmstMos.Rows)
            {
                dr["srNo"] = icount + 1;
                icount = icount + 1;
            }


            if (lblIsweb == "1")
            {
                int icount1 = 0;
                foreach (DataRow dr in dtmstMos1.Rows)
                {
                    dr["srNo"] = icount1 + 1;
                    icount1 = icount1 + 1;
                }
                foreach (DataRow dr in dtmstMos1.Rows)
                {
                    DataRow[] dr2 = dtmstMos.Select("srNo='" + dr["srNo"] + "' ");
                    if (dr2.Length > 0 && dr["Score"].ToString() == "5")
                    {
                        dr2[0]["Flag"] = "1";
                    }
                }
            }

            gvTopvillage.DataSource = dtmstMos;
            gvTopvillage.DataBind();
        }
        else
        {
            DataRow dr;
            for (int i = 0; i < 40; i++)
            {
                dr = dtmstMos.NewRow();
                dr[0] = (i + 1).ToString();
                dtmstMos.Rows.Add(dr);
            }
            dtmstMos.AcceptChanges();

            gvTopvillage.DataSource = dtmstMos;
            gvTopvillage.DataBind();
        }
        if (lblIsweb == "1")
        {
            for (int i = 0; i < gvTopvillage.Rows.Count; i++)
            {
                RadioButtonList rblScore = (RadioButtonList)gvTopvillage.Rows[i].FindControl("rblScore");
                Label lblFlag = (Label)gvTopvillage.Rows[i].FindControl("lblFlag");
                if (lblFlag.Text == "1")
                {
                    rblScore.Enabled = true;
                    // rblScore.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    rblScore.Enabled = false;
                }
            }
        }
        if (lblAnswerSheetPhoto.Length > 5)
        {
            FileuploadAttach.Visible = false;
            lblbImg.Visible = false;
        }
        else
        {
            FileuploadAttach.Visible = true;
            lblbImg.Visible = true;
        }
        clsMain.LSEFormID = Convert.ToString(Session["username"]);
        MpexdrDistrict1.Show();
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
        string lblAnswerSheetPhoto = (gvr.FindControl("lblAnswerSheetPhoto") as Label).Text;

        string filename = "";
        string IDImage = lblAnswerSheetPhoto;
        string sFileDir = Server.MapPath(Comman.GetImagePath("LSEPath") + "/");
        filename = sFileDir + "LSE\\" + IDImage;
        filename = sFileDir + IDImage;

        if (lblAnswerSheetPhoto.Length > 5)
        {
            Response.ContentType = ".jpg";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

            Response.TransmitFile(filename);
            Response.End();
        }
    }

    protected void ImgShow_Click(object sender, EventArgs e)
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
        string lblAnswerSheetPhoto = (gvr.FindControl("lblAnswerSheetPhoto") as Label).Text;

        string lblUniquePlanCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        hdnMKID4.Value = lblUniquePlanCode;
        EduImg.ImageUrl = ResolveUrl("~/LSE/" + lblAnswerSheetPhoto);
        Modalimages.Show();
    }

    protected void btnOmg_Click(object sender, EventArgs e)
    {

        string Fullfilename = "";

        if (FileuploadAttach.PostedFile != null && FileuploadAttach.PostedFile.FileName != "")
        {
            string ext = System.IO.Path.GetExtension(FileuploadAttach.PostedFile.FileName).ToLower();
            if (FileuploadAttach.PostedFile.ContentLength < 202400)
            {
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image size must be less than 100kb')</script>", false);
                Modalimages.Show();
                return;
            }
            if (ext != ".jpeg" && ext != ".jpg" && ext != ".png" && ext != ".gif")
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Images')</script>", false);
                Modalimages.Show();
                return;
            }
            string exten = Path.GetExtension(FileuploadAttach.PostedFile.FileName);
            Fullfilename = "" + hdnMKID4.Value + "_" + Session["username"].ToString() + exten;
        }
        #region Attach image
        //System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FileuploadAttach.PostedFile.InputStream);
        //System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 81);


        string sFileDir = Server.MapPath(Comman.GetImagePath("LSEPath") + "/");

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
                catch// (Exception ex)
                {
                    //ShowMessage.Visible = true;
                    //ShowMessage.Style.Add("background-color", "#FFBABA");
                    //MessageLBL.Style.Add("Color", "#D8000C");
                    //MessageLBL.Text = ex.ToString();

                }
            }
            FileuploadAttach.PostedFile.SaveAs(sFileDir + Fullfilename);

            //int icoun = InsertUpdateLSE(hdnMKID4.Value, Fullfilename);
            //if (icoun>0)
            //{
            //    for (int i = 0; i < gvWeallyDatewise.Rows.Count; i++)
            //    {

            //        Label lblUniquePlanCode = (Label)gvWeallyDatewise.Rows[i].FindControl("lblUniquePlanCode");                  

            //        Label lblAnswerSheetPhoto = (Label)gvWeallyDatewise.Rows[i].FindControl("lblAnswerSheetPhoto");
            //       if (lblUniquePlanCode.Text== hdnMKID4.Value)
            //        {
            //            lblAnswerSheetPhoto.Text = Fullfilename;
            //        }
            //    }
            //        ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
            //}
        }

        #endregion

    }
    protected void ddlWorkingStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlAttStatus = (DropDownList)row1.FindControl("ddlAttStatus");
        ImageButton ImgShow = (ImageButton)row1.FindControl("ImgShow");
        ImageButton lnkd = (ImageButton)row1.FindControl("lnkd");
        LinkButton Button1 = (LinkButton)row1.FindControl("Button1");
        if (ddlAttStatus.SelectedValue == "1")
        {
            ImgShow.Visible = true;
            lnkd.Visible = true;
            Button1.Visible = true;
        }
        else
        {
            ImgShow.Visible = false;
            lnkd.Visible = false;
            Button1.Visible = false;
        }
    }
    public int InsertUpdateLSE(string Tarining_ID, string IMG, string Scroe, string p, int Flag)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "InsertandUpdateLSE";
        dbSqlCommand.Parameters.Add("@Uniqecode", SqlDbType.VarChar).Value = Tarining_ID;
        dbSqlCommand.Parameters.Add("@Img", SqlDbType.VarChar).Value = IMG;
        dbSqlCommand.Parameters.Add("@Scroe", SqlDbType.VarChar).Value = Scroe;
        dbSqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = Session["username"].ToString();
        dbSqlCommand.Parameters.Add("@P", SqlDbType.VarChar).Value = p;
        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.Int).Value = Flag;



        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }


    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

        }
        catch
        {

        }

    }
    protected void rvltbc(object sender, EventArgs e)
    {

        if (rblTBFC.SelectedValue == "1")
        {
            string strQry = "      select TBCode,TBname from mstTeamBalika mst  with(nolock) left join mst5Village V on V.VillageCode=mst.VillageCode   	or  V.refVillage16=mst.VillageCode	or V.refVillage17=mst.VillageCode	or  V.refVillage18=mst.VillageCode or  V.refVillage19=mst.VillageCode or  V.refVillage20=mst.VillageCode or  V.refVillage21=mst.VillageCode  or  V.refVillage22=mst.VillageCode  or  V.refVillage23=mst.VillageCode  where WorkingStatus=1 and V.VillageCode='" + ddlVillage.SelectedValue + "'  ";
            DataTable dtVillageActivtiy = objMain.LoadData(strQry);
            objComman.BindDLLDatatable("mstSchool", dtVillageActivtiy, "TBCode,TBname", conditions, "TBname", "asc", ddlTbFC, "TBname", "TBCode", "Select");

        }
        else
        {
            string strQry = "      select UserName,UserName TBname from mstUser   with(nolock)  where  ActiveStatus=1 and VillageCode in (select distinct clustercode from mst5village where Villagecode ='" + ddlVillage.SelectedValue + "')  ";
            DataTable dtVillageActivtiy = objMain.LoadData(strQry);
            objComman.BindDLLDatatable("mstSchool", dtVillageActivtiy, "UserName,TBname", conditions, "TBname", "asc", ddlTbFC, "TBname", "UserName", "Select");

        }


    }
    protected void ImgShow1_Click(object sender, EventArgs e)
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
        string lblAnswerSheetPhoto = (gvr.FindControl("lblAnswerSheetPhoto") as Label).Text;

        string lblUniquePlanCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        hdnMKID4.Value = lblUniquePlanCode;
        EduImg.ImageUrl = ResolveUrl("~/LSE/" + lblAnswerSheetPhoto);
        Modalimages.Show();
    }
    protected void ddlWorkingStatus1_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddlLabTest1 = (DropDownList)sender;
        GridViewRow row1 = (GridViewRow)ddlLabTest1.NamingContainer;

        DropDownList ddlAttStatus = (DropDownList)row1.FindControl("ddlAttStatus");
        ImageButton ImgShow = (ImageButton)row1.FindControl("ImgShow1");
        ImageButton lnkd = (ImageButton)row1.FindControl("lnkd1");
        LinkButton Button1 = (LinkButton)row1.FindControl("Button11");
        if (ddlAttStatus.SelectedValue == "1")
        {
            ImgShow.Visible = true;
            lnkd.Visible = true;
            Button1.Visible = true;
        }
        else
        {
            ImgShow.Visible = false;
            lnkd.Visible = false;
            Button1.Visible = false;
        }
    }
    protected void ImgDownload1_Click(object sender, EventArgs e)
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
        string lblAnswerSheetPhoto = (gvr.FindControl("lblAnswerSheetPhoto") as Label).Text;

        string filename = "";
        string IDImage = lblAnswerSheetPhoto;
        string sFileDir = Server.MapPath(Comman.GetImagePath("LSEPath") + "/");
        filename = sFileDir + "LSE\\" + IDImage;
        filename = sFileDir + IDImage;

        if (lblAnswerSheetPhoto.Length > 5)
        {
            Response.ContentType = ".jpg";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + IDImage + "");

            Response.TransmitFile(filename);
            Response.End();
        }
    }
    protected void ImgShow_Click1(object sender, EventArgs e)
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
        string lblAnswerSheetPhoto = (gvr.FindControl("lblAnswerSheetPhoto") as Label).Text;

        string lblUniquePlanCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        hdnMKID4.Value = lblUniquePlanCode;
        EduImg.ImageUrl = ResolveUrl("~/LSE/" + lblAnswerSheetPhoto);
        Modalimages.Show();
    }

    protected void ImgView1_Click(object sender, EventArgs e)
    {
        LinkButton bt = (LinkButton)sender;

        GridViewRow gvr = (GridViewRow)bt.NamingContainer;
        string lblUniquePlanCode = (gvr.FindControl("lblUniquePlanCode") as Label).Text;
        string lblAnswerSheetPhoto = (gvr.FindControl("lblOMRAnswersEdit") as Label).Text;
        DropDownList ddlAttStatus = (gvr.FindControl("ddlAttStatus") as DropDownList);
        string lblIsweb = (gvr.FindControl("lblIsweb") as Label).Text;

        hdnMKID4.Value = lblUniquePlanCode;
        hdnMKID5.Value = ddlAttStatus.SelectedValue;
        hdnMKID6.Value = lblIsweb;
        clsMain.LSEImageID = "";
        DataTable dtmstMos = objMain.LoadData(" select 0 srNo, 0 Flag, LTRIM(dataTable) as Score   from [dbo].[SplitStringLSE]((select OMRAnswersEdit from tblChildAttendanceLifeskill where Present=1 and UniqueCode='" + lblUniquePlanCode + "' and len(OMRAnswersEdit)>4))");
        DataTable dtmstMos1 = objMain.LoadData(" select 0 srNo,0  Flag, LTRIM(dataTable) as Score   from [dbo].[SplitStringLSE]((select Assessment from tblChildAttendanceLifeskill where Present=1 and UniqueCode='" + lblUniquePlanCode + "' and len(OMRAnswersEdit)>4))");

        if (dtmstMos.Rows.Count > 0)
        {
            int icount = 0;
            foreach (DataRow dr in dtmstMos.Rows)
            {
                dr["srNo"] = icount + 1;
                icount = icount + 1;
            }


            if (lblIsweb == "1")
            {
                int icount1 = 0;
                foreach (DataRow dr in dtmstMos1.Rows)
                {
                    dr["srNo"] = icount1 + 1;
                    icount1 = icount1 + 1;
                }
                foreach (DataRow dr in dtmstMos1.Rows)
                {
                    DataRow[] dr2 = dtmstMos.Select("srNo='" + dr["srNo"] + "' ");
                    if (dr2.Length > 0 && dr["Score"].ToString() == "5")
                    {
                        dr2[0]["Flag"] = "1";
                    }
                }
            }

            gvTopvillage.DataSource = dtmstMos;
            gvTopvillage.DataBind();
        }
        else
        {
            DataRow dr;

            for (int i = 0; i < 40; i++)
            {
                dr = dtmstMos.NewRow();
                dr[0] = (i + 1).ToString();
                dtmstMos.Rows.Add(dr);
            }

            dtmstMos.AcceptChanges();
            if (Convert.ToString(Session["Session"]) == "B")
            {
                if (lblAnswerSheetPhoto.Length > 0)
                {
                    string[] multiArray = lblAnswerSheetPhoto.Split(new Char[] { ',' });
                    int icoount = 0;
                    foreach (string author in multiArray)
                    {
                        dtmstMos.Rows[icoount]["Score"] = author.Trim();
                        icoount = icoount + 1;


                    }
                }
            }

            gvTopvillage.DataSource = dtmstMos;
            gvTopvillage.DataBind();
        }
        //if (lblIsweb == "1")
        //{
        //    for (int i = 0; i < gvTopvillage.Rows.Count; i++)
        //    {
        //        RadioButtonList rblScore = (RadioButtonList)gvTopvillage.Rows[i].FindControl("rblScore");
        //        Label lblFlag = (Label)gvTopvillage.Rows[i].FindControl("lblFlag");
        //        if (lblFlag.Text == "1")
        //        {
        //            rblScore.Enabled = true;
        //            // rblScore.ForeColor = System.Drawing.Color.Red;
        //        }
        //        else
        //        {
        //            rblScore.Enabled = false;
        //        }
        //    }
        //}
        if (lblAnswerSheetPhoto.Length > 5)
        {
            FileuploadAttach.Visible = false;
            lblbImg.Visible = false;
        }
        else
        {
            FileuploadAttach.Visible = true;
            lblbImg.Visible = true;
        }
        clsMain.LSEFormID = Convert.ToString(Session["username"]);
        MpexdrDistrict1.Show();
    }
    protected void gvnroll17_OnRowCommand(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton LinkButton1 = (ImageButton)e.Row.FindControl("ImgShow1");
            Label lblAnswerSheetPhoto = (Label)e.Row.FindControl("lblAnswerSheetPhoto");
            Label lblAttendanceStatus = (Label)e.Row.FindControl("lblAttendanceStatus");

            DropDownList ddlAttStatus = (DropDownList)e.Row.FindControl("ddlAttStatus");
            Label lblPresent = (Label)e.Row.FindControl("lblPresent");
            ImageButton lnkd = (ImageButton)e.Row.FindControl("lnkd1");
            LinkButton Button1 = (LinkButton)e.Row.FindControl("Button11");




            //ImageButton LinkBut51 = (ImageButton)e.Row.FindControl("LinkBut51");
            //LinkBut51.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");

            if (lblAnswerSheetPhoto.Text.Length > 5)
            {
                LinkButton1.Visible = true;
                Button1.Visible = true;
                lnkd.Visible = true;
            }
            else
            {
                LinkButton1.Visible = false;
                Button1.Visible = false;
                lnkd.Visible = false;

            }
            ddlAttStatus.SelectedValue = lblPresent.Text;
        }
    }

}