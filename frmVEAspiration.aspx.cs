using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using System.Drawing;


public partial class frmVEAspiration : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();
    public bool vADD = false;
    public bool vVerify = false;
    public bool vDelete = false;

    string conditions = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["username"]) != "")
            {

                //GVMainBind();
                LoadYear();
                LoadUserLeavel();

                //FillSocialCat();
                //FillDropResone();
                ViewState["Save"] = "Save";
               // FillFaimlyCat();
                //FillEdu();
                //FillSours();
               // FillReasone();
                btnDelete.Attributes.Add("onclick", "javascript:return " + "confirm('Are you sure you want to Delete? ')");
                ValdateUserLavel();
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

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
        DateTime GivenDate = DateTime.Now;
        int GivenYear = GivenDate.Year;
        int m = GivenDate.Month;

        DataTable dt = null;
        //ddlYear.Items.Add("--Select--","0");
        int y = GivenDate.Year;


        DateTime GivenDate1 = DateTime.Now;
        int GivenYear1 = GivenDate1.Year;
        DataTable dtYear = CreateDataTable();
        DataRow dr;
        if (ddlYear.SelectedIndex < 0)
        {

            string mYear1 = GivenYear1.ToString();
            for (int j = 0; j < 1; j++)
            {
                if (m > 3)
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear.ToString() + "-" + Convert.ToString((GivenYear + 1));
                    dr["ID"] = y;
                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 1 + "-" + Convert.ToString((GivenYear - 1 + 1));
                    dr["ID"] = y - 1;
                    dtYear.Rows.Add(dr);
                    //get last  two digits (eg: 10 from 2010);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);
                }
                else
                {
                    dr = dtYear.NewRow();
                    dr["Type"] = Convert.ToString((y - 1)) + "-" + y.ToString();
                    //y = y - 1;
                    dr["ID"] = y - 1;

                    dtYear.Rows.Add(dr);
                    dr = dtYear.NewRow();
                    dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                    dr["ID"] = y - 2;
                    dtYear.Rows.Add(dr);

                }

            }

        }
        objComman.BindDLLMasterTable("mstSchool", "Type,ID", dtYear, conditions, "Type", "asc", ddlYear, "Type", "ID", "Select");

        ddlYear.SelectedIndex = 1;
        //}


    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlState.SelectedIndex = 1;
            ddlState_SelectedIndexChanged(ddlDistrict, null);
            ddlDistrict.SelectedIndex = 1;
            ddlDistrict_SelectedIndexChanged(ddlDistrict, null);

            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
        else
        {
            ddlState.SelectedIndex = 0;
            ddlDistrict.Items.Clear();
            ddlBlock.Items.Clear();
            ddlPanchayat.Items.Clear();
            ddlVillage.Items.Clear();
        }
    }

    

    public void ValdateUserLavel()
    {

        string strQry = "";
        string Cond = "Module='TeamBalika' ";
        strQry = "Select * from MstUserRight  where " + Cond + " and Role_Id=" + Session["user_level"].ToString() + "   ";


        DataTable dtRole = objMain.LoadData(strQry);

        if (dtRole.Rows.Count > 0)
        {
            vADD = Convert.ToBoolean(dtRole.Rows[0]["AddStatus"].ToString());
            vVerify = Convert.ToBoolean(dtRole.Rows[0]["verify_Status"].ToString());
            vDelete = Convert.ToBoolean(dtRole.Rows[0]["Delete_status"].ToString());
            ViewState["vADD"] = vADD;
            ViewState["vVerify"] = vVerify;
            ViewState["vDelete"] = vDelete;
        }
        if (vDelete == true)
        {

            btnDelete.Visible = true;
        }
        else
        {

            btnDelete.Visible = false;
        }

        if (vADD == true)
        {
            btnAdd.Enabled = true;
            btnsave.Enabled = true;
            //lblMain.Text = "School Information Campaign";
        }
        else
        {
            btnAdd.Enabled = false;
            btnsave.Enabled = false;
        }
       
        if (vVerify == true)
        {

            btnsave.Enabled = true;

           
        }
        if (vVerify == true || vADD == true)
        {
            btnsave.Enabled = true;

        }
        else
        {
            btnsave.Enabled = false;

        }
    }
    public void LoadUserLeavel()
    {
        conditions = "";
        if (Session["user_level_Role"].ToString() == "1")
        {
            //conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");
            ddlState.Enabled = true;
            //ddlDistrict.Enabled = true;
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            ddlState.Enabled = true;
            //  ddlDistrict.Enabled = true;
        }
        else
        {
            conditions = "StateCode='" + Session["StateCode"].ToString() + "' ";
            objComman.BindDLL("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", conditions, "StateName", "Desc", ddlState, "StateName", "StateCode", "--Select--");

            ddlState.SelectedIndex = 1;
            //ddlState.Enabled = false;
            //  ddlDistrict.Enabled = false;
        }


        if (Session["user_level_Role"].ToString() == "1")
        {
        }
        else if (Session["user_level_Role"].ToString() == "2")
        {
            conditions = "";
            conditions = "StateCode ='" + ddlState.SelectedValue + "' ";
            objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "--Select--");

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


    public void FillCBState()
    {
        conditions = "";
        objComman.BindDLL("mst1State", "StateCode,dbo.TitleCase(upper(StateName)) as StateName ", conditions, "StateName", "asc", ddlState, "StateName", "StateCode", "--Select--");

        pnlMain.Enabled = false;

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
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode ='" + Session["DistrictCode"].ToString() + "'  and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "'";
        }
        else
        {
            conditions = "StateCode ='" + ddlState.SelectedValue + "' and DistrictCode in(" + Session["DistrictCode"].ToString() + ") and mst2District.FYear ='" + ddlYear.SelectedItem.Text + "' ";


        }


        objComman.BindDLL("mst2District", "DistrictCode,dbo.TitleCase(upper(DistrictName)) as DistrictName ", conditions, "DistrictName", "asc", ddlDistrict, "DistrictName", "DistrictCode", "Select");



    }

   
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBDist();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBBock();
        pnlMain.Enabled = false;
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCBCluster();
        pnlMain.Enabled = false;
    }
    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCVillage();
        pnlMain.Enabled = false;
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlMain.Enabled = false;
        //Unique();
        refreshonselect();
        
        
    }

    protected void ddlTbname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillLastEducation();
        fillAspiraton();
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
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 and BlockCode in( " + Session["BlockCode"].ToString() + " ) and FYear ='" + ddlYear.SelectedItem.Text + "' ";
        }
        else
        {
            conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "' and  DividedBlock=1 ";
        }
        objComman.BindDLL("mst3Block", "BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", conditions, "BlockName", "asc", ddlBlock, "BlockName", "BlockCode", "--Select--");



    }
    public void FillCBCluster()
    {
        conditions = "";
        conditions = "DistrictCode ='" + ddlDistrict.SelectedValue + "'  and BlockCode ='" + ddlBlock.SelectedValue + "'";
        objComman.BindDLLSelectAll("mstPanchayat", "PanchayatCode,dbo.TitleCase(upper(PanchayatName)) as PanchayatName", conditions, "PanchayatName", "asc", ddlPanchayat, "PanchayatName", "PanchayatCode", "Select");



    }

    public void fillTBName( string str)
    {
        objComman.BindDLLSelectAll("FROM [dbo].[mstTeamBalika] inner join mst5village on mst5village.villageCode = mstTeamBalika.VillageCode or mst5Village.OldUniqueCode=mstTeamBalika.VillageCode  Inner join tblVEAspiration on mstTeamBalika.UniqueCode != tblVEAspiration.TBCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode ", "mstTeamBalika.TBCODE as TBCODE, mstTeamBalika.UniqueCode as UniqueCode, TBNAme", str, "TBNAme", "asc", ddlPanchayat, "TBNAme", "UniqueCode", "Select");
    }

    public void fillLastEducation()
    {
        conditions = "";
        conditions = "LookupFlag ='Edu' and Active=1 ";
        objComman.BindDLL("mstLookup", "LookupCode,Description", conditions, "LookupCode", "asc", ddlEducation, "Description", "LookupCode", "Select");
    }

    public void fillAspiraton()
    {
        //objComman.BindDLL("MstAspirations", "LookupCode,Description", conditions, "LookupCode", "asc", ddl_aspiration, "Description", "LookupCode", "Select");
        DataTable dtasporation = objMain.LoadData("select ID, AspirationName FROM MstAspirations");
        //objComman.Bind_DDL_ZeroIndex_String(ddl_aspiration, dtasporation, "ID", "UID", "Select");
        ddl_aspiration.DataSource = dtasporation;
        ddl_aspiration.DataTextField = "AspirationName";
        ddl_aspiration.DataValueField = "ID";
        ddl_aspiration.DataBind();

        //ddlasp.DataSource = dtasporation;
        //ddlasp.DataTextField = "AspirationName";
        //ddlasp.DataValueField = "ID";
        //ddlasp.DataBind();
        
    }

   
    public void FillCVillage()
    {
        conditions = "";
     
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

    private void GVMainBind()
    {

        string str = "";

        if (ddlState.SelectedValue != null && ddlState.SelectedIndex > 0)
        {
            str = "where mst5Village.StateCode='" + ddlState.SelectedValue.ToString() + "'";
        }
        if (ddlDistrict.SelectedValue != null && ddlDistrict.SelectedIndex > 0)
        {
            str = str + "and mst5Village.DistrictCode='" + ddlDistrict.SelectedValue.ToString() + "'";
        }

        if (ddlBlock.SelectedValue != null && ddlBlock.SelectedIndex > 0)
        {
            str = str + "and mst5Village.BlockCode='" + ddlBlock.SelectedValue.ToString() + "'";
        }

        if (ddlPanchayat.SelectedValue != null && ddlPanchayat.SelectedIndex > 1)
        {
            str = str + "and mst5Village.PanchayatCode='" + ddlPanchayat.SelectedValue.ToString() + "'";
        }

        if (ddlVillage.SelectedValue != null && ddlVillage.SelectedIndex > 0)
        {
            str = str + "and mst5Village.VillageCode='" + ddlVillage.SelectedValue.ToString() + "'";
        }
        DataTable dtmstM = objMain.LoadData("select CCode as UniqueCode,mst5Village.TBCode,TBName from (SELECT mst5Village.VillageCode, mst5Village.DistrictCode,mst5Village.BlockCode,mst5Village.StateCode, mst5Village.PanchayatCode, mstTeamBalika.UniqueCode as CCode, mstTeamBalika.TBCode, TBName FROM [dbo].[mstTeamBalika] inner join mst5village on mst5village.villageCode = mstTeamBalika.VillageCode or mst5Village.OldUniqueCode=mstTeamBalika.VillageCode  left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode   left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode) as mst5Village inner join tblVEAspiration on mst5Village.CCode = tblVEAspiration.TBCode " + str + " ");
        DataTable dtmstMM = objMain.LoadData("select mstTeamBalika.TBCODE, mstTeamBalika.UniqueCode, TBNAme from mstTeamBalika inner join mst5Village on mst5Village.VillageCode = mstTeamBalika.VillageCode or mst5Village.OldUniqueCode=mstTeamBalika.VillageCode left join mst1State on mst1State.StateCode=mst5Village.StateCode left join mst2District on mst2District.DistrictCode=mst5Village.DistrictCode left join (select distinct blockcode,blockname from mst3Block) blk ON mst5Village.BlockCode = blk.BlockCode LEFT JOIN (select distinct PanchayatCode,PanchayatName from mstPanchayat) phy  ON mst5Village.PanchayatCode  = phy.PanchayatCode  LEFT join tblVEAspiration on mstTeamBalika.UniqueCode = tblVEAspiration.TBCode   " + str + " and tblVEAspiration.TBCode is null ");
       
        if (dtmstM.Rows.Count > 0)
        {
            GVMain.DataSource = dtmstM;
            GVMain.DataBind();
            ViewState["Serach"] = dtmstM;
            pnlMain.Enabled = false;
        }
        else
        {
            GVMain.DataSource = null;
            GVMain.DataBind();
            ViewState["Serach"] = "";
            pnlMain.Enabled = false;
        }
        if (dtmstMM.Rows.Count > 0)
        {

            
            ddlTbname.DataSource = dtmstMM;
            ddlTbname.DataTextField = "TBNAme";
            ddlTbname.DataValueField = "UniqueCode";
            ddlTbname.DataBind();
            ddlTbname.Items.Insert(0, new ListItem("--Select--", "0"));
            pnlMain.Enabled = true;
            ViewState["Save"] = "Save";
            txtMI.Enabled = false;

            ddlLHEType.Enabled = false;
        }
    }





   
    protected void btnsave_Click(object sender, EventArgs e)
    {


       Save_Update(0);
     
    }

    protected void btnSumbit_Click(object sender, EventArgs e)
    {
        //Save_Update(0);
    }

    private void Save_Update(int i)
    {
        if (Convert.ToInt32(ddlLHE.SelectedValue) == 1 && txtMI.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Monthly Income')</script>", false);


            this.txtMI.Focus();
            return;
        }
        if (Convert.ToInt32(ddlLHE.SelectedValue) == 1 && Convert.ToInt32(ddlLHEType.SelectedValue) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Livelihood Engagement Type')</script>", false);


            this.ddlLHEType.Focus();
            return;
        }
      
        if (Convert.ToInt32(ddlTbname.SelectedIndex) < 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Of team Balika')</script>", false);


            this.ddlTbname.Focus();
            return;
        }
        if (Convert.ToInt32(ddlEducation.SelectedValue) <= 0)
        {
           
                
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select last Education')</script>", false);


                    this.ddlEducation.Focus();
                    return;
              
        }

      

        if (Convert.ToInt32(ddlEducationStatus.SelectedValue) <= 0)
        {


            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Education Status')</script>", false);


            this.ddlEducationStatus.Focus();
            return;

        }

      
        string TBCDOE = ddlTbname.SelectedValue.ToString();
        string Vcode = ddlVillage.SelectedValue.ToString();
        int lastEdu = Convert.ToInt32(ddlEducation.SelectedValue);
        int edstatus = Convert.ToInt32(ddlEducationStatus.SelectedValue);
        int LHE = Convert.ToInt32(ddlLHE.SelectedValue);
        int LHEtype;
        if (LHE == 1)
        {
             LHEtype = Convert.ToInt32(ddlLHEType.SelectedValue);
        }
        else
        {
             LHEtype = 0;
        }
        Decimal monthlyincome;
        if (LHEtype != 0)
        {
            monthlyincome = Convert.ToDecimal(txtMI.Text);
        }
        else
        {
            monthlyincome = 0;
        }
        //int asp = Convert.ToInt32(ddlasp.SelectedValue);
       
        string flag = "";
        if (ViewState["Save"].ToString() == "Save")
        {
            flag = ViewState["Save"].ToString();
           
                 Unique();
                    string UID = objMain.Generate_RandomString(8);
                    //string TBCode = ViewState["TBCode"].ToString();
                    //string schoolod = ViewState["NumNo"].ToString();
                    int result = objMain.saveVEAsporation(UID, TBCDOE, Vcode, lastEdu, edstatus, LHE, LHEtype, monthlyincome,flag);
                    int sn = 1;
                    for (int k = 0; k < ddl_aspiration.Items.Count; k++)
                    {
                        if (ddl_aspiration.Items[k].Selected)
                        {
                            
                            Int32 selectedItem = Convert.ToInt32(ddl_aspiration.Items[k].Value);
                             SqlParameter[] p2 = new SqlParameter[]
                        {
                            new SqlParameter("@TBCODE",TBCDOE),
                            new SqlParameter("@ASpId",selectedItem),
                            new SqlParameter("@sn", sn),
                            new SqlParameter("@Flag", flag),
                 

                        };
                        int result1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_insertUpdateAspiration", p2);
                            sn++;
                        }
                    }

                        if (result > 0)
                        {


                            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved sucessfully')</script>", false);
                            GVMainBind();

                            RefreshControl();
                            pnlMain.Enabled = false;
                        }

                

            


        }
        else
        {
            flag = "update";
            //string TBCode = ViewState["TBCode"].ToString();
            int result = objMain.saveVEAsporation("", TBCDOE, Vcode, lastEdu, edstatus, LHE, LHEtype, monthlyincome, flag);

            int sn = 1;
            for (int k = 0; k < ddl_aspiration.Items.Count; k++)
            {
                
                if (ddl_aspiration.Items[k].Selected)
                {
                    
                    Int32 selectedItem = Convert.ToInt32(ddl_aspiration.Items[k].Value);
                    SqlParameter[] p2 = new SqlParameter[]
                        {
                            new SqlParameter("@TBCODE",TBCDOE),
                            new SqlParameter("@ASpId",selectedItem),
                            new SqlParameter("@sn", sn),
                            new SqlParameter("@Flag", flag),
                 

                        };
                    int result1 = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_insertUpdateAspiration", p2);
                    sn++;
                   
                }
               
            }
            if (result > 0)
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated sucessfully')</script>", false);
                GVMainBind();

                RefreshControl();
                pnlMain.Enabled = false;
            }

        }
    }



    protected void ddlLHE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlLHE.SelectedValue) == 1)
        {
           
            ddlLHEType.Enabled = true;
            txtMI.Enabled = true;
        }
        else
        {
            txtMI.Enabled = false;
            
            ddlLHEType.Enabled = false;
        }
    }

    public void refreshonselect()
    {
        ddl_aspiration.Items.Clear();
        ddlEducation.Items.Clear();

        //ddlEducation.SelectedIndex = 0;
        ddlEducationStatus.SelectedIndex = 0;
        ddlLHEType.SelectedIndex = 0;
        ddlLHE.SelectedIndex = 0;
        ddl_aspiration.Items.Clear();
        //ddlasp.Items.Clear();
        txtMI.Text = "";
    }
    private void RefreshControl()
    {
        #region RefreshControl
       
        ViewState["TMCode"] = null;
        ViewState["TBCode"] = null;
         ddlTbname.Items.Clear();
        ddl_aspiration.SelectedIndex = 0;
        ddlEducation.SelectedIndex = 0;
        
       ddlEducation.SelectedIndex = 0;
       ddlEducationStatus.SelectedIndex = 0;
       ddlLHEType.SelectedIndex = 0;
       ddlLHE.SelectedIndex = 0;
       ddl_aspiration.Items.Clear();
       //ddlasp.Items.Clear();
       txtMI.Text = "";



        ViewState["Save"] = "Save";

        ViewState["TMCode"] = null;
        #endregion
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlDistrict.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select District')</script>", false);
            return;
        }

        if (ddlBlock.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Block')</script>", false);
            return;
        }
        if (ddlPanchayat.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Panchayat')</script>", false);
            return;
        }
        if (ddlVillage.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Village')</script>", false);
            return;
        }
        pnlMain.Enabled = true;
        RefreshControl();

      

        ViewState["Save"] = "Save";
        
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {

        if (ViewState["TMCode"].ToString() != null)
        {
            objMain.DeleteTM(ViewState["TMCode"].ToString());
            GVMainBind();
        }
    }

    protected void GVMain_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        string str = "";

        if (e.CommandName == "GVUIO")
        {
            int iIndex = Convert.ToInt32(e.CommandArgument);
            string TBCode = GVMain.DataKeys[iIndex]["UniqueCode"].ToString();
            str = "where tblVEaspiration.TBCode = " + " '" + GVMain.DataKeys[iIndex]["UniqueCode"].ToString() + "'";
           
            ViewState["Save"] = "Edit";

            pnlMain.Enabled = true;

            for (int i = 0; i < GVMain.Rows.Count; i++)
            {
                GridViewRow RowD = GVMain.Rows[i];
                if (i % 2 == 0)
                {
                    RowD.BackColor = Color.White;
                }
                else
                {
                    RowD.BackColor = Color.FromArgb(245, 245, 245);
                }

            }
            GridViewRow row = GVMain.Rows[iIndex];
            row.BackColor = Color.LightYellow;
        }

        DataTable dtedit = objMain.LoadData("select tblVEaspiration.UniqueCode,tblVEaspiration.TBCode, TBName,LastEducation, EducationStatus, LivelihoodEngagement, LivelihoodEngagementType,LivelihoodEngagementMonthlyIncome,Aspirations,ID from tblVEaspiration left Join MstAspirations on MstAspirations.ID = Aspirations LEFT JOIN mstTeamBalika on mstTeamBalika.UniqueCode = tblVEaspiration.TBCode " + str + "  ");
        DataTable dtasp = objMain.LoadData("select ASpirationID from tblAspirationprebybalik left join tblVEaspiration on tblVEaspiration.Tbcode = tblAspirationprebybalik.Tbcode " + str + "  ");
        ddlTbname.DataSource = dtedit;
        ddlTbname.DataTextField = "TBName";
        ddlTbname.DataValueField = "TBCODE";
        ddlTbname.DataBind();

        fillLastEducation();
        fillAspiraton();
        if (dtedit.Rows.Count > 0)
        {
            DataRow row = dtedit.Rows[0];
            ddlTbname.SelectedValue = row["TBCODE"].ToString();
            ddlEducation.SelectedValue = row["LastEducation"].ToString();
            ddlEducationStatus.SelectedValue = row["EducationStatus"].ToString();
            
            ddlLHE.SelectedValue = row["LivelihoodEngagement"].ToString();
            if (ddlLHE.SelectedValue == "1")
            {
                ddlLHEType.Enabled = true;
                txtMI.Enabled = true;
                ddlLHEType.SelectedValue = row["LivelihoodEngagementType"].ToString();
                txtMI.Text = row["LivelihoodEngagementMonthlyIncome"].ToString();
            }
            else
            {
                ddlLHEType.Enabled = false;
                txtMI.Enabled = false;
                ddlLHEType.SelectedValue = "0";
                txtMI.Text = "0" ;
            }
            
            //ddlasp.SelectedValue = row["Aspirations"].ToString();
           
        }
        if (dtasp.Rows.Count > 0)
        {
           // DataRow srow = dtasp.Rows["ASpiration"];
            for (int n = 0; n < dtasp.Rows.Count; n++)
            {
                string va = dtasp.Rows[n]["ASpirationID"].ToString();
                int nn = Convert.ToInt32(va);
                ddl_aspiration.Items[nn-1].Selected = true;
            }
        }
       // cheboxselect();
        
    }


    public void cheboxselect()
    {
        List<ListItem> SelectedItems = new List<ListItem>();

        foreach (ListItem ItemsSelected in ddl_aspiration.Items)
        {
            if (ItemsSelected.Selected)
                SelectedItems.Add(ItemsSelected);
        }

        if (SelectedItems.Count() > 2)
        {
            // Display alert

            foreach (ListItem item in ddl_aspiration.Items)
            {
                
                if (!SelectedItems.Contains(item))
                {
                    //item.Selected = false;
                    item.Enabled = false;
                }
                //ScriptManager.RegisterStartupScript(Page, GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select only three preferences')</script>", false);
                //return;
            }
        }
        else
        {
            foreach (ListItem item in ddl_aspiration.Items)
            {
                item.Enabled = true;
            }
        }
    }


    protected void OnCheckBox_Changed(object sender, EventArgs e)
    {
        cheboxselect(); 
    }


   
    protected void txtSearchName_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["Serach"] as DataTable;
        string strFilter = "";

        string str = "TBName";
        DataTable dtfilter = dt.Copy();


        strFilter = str + " like '%" + txtSearchName.Text.Trim() + "%'   ";

        //dtSoSaleOrder.Select(txtSearch.SelectedValue.ToString() + " like '" + txtSearch.Text + "%'";


        dtfilter.DefaultView.RowFilter = strFilter;
        dtfilter.DefaultView.Sort = "TBName asc";
        GVMain.DataSource = dtfilter.DefaultView.ToTable();
        GVMain.DataBind();

    }
   
    protected void btnSerach_Click(object sender, EventArgs e)
    {

        GVMainBind();
        //pnlMain.Enabled = false;
    }




    protected void btnAdd_Click1(object sender, EventArgs e)
    {



        // ddllevel_selectindexchange(sender, e);
    }

    protected void GV_Project_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVMain.PageIndex = e.NewPageIndex;
        if (ViewState["Serach"] != null)
        {
            DataTable dt = ViewState["Serach"] as DataTable;
            GVMain.DataSource = dt;
            GVMain.DataBind();
        }

    }
    public void Unique()
    {
        if (ViewState["Save"].ToString() == "Save")
        {
            if (ddlVillage.SelectedIndex > 0)
            {
                Int32 mNewNo = 0;
                string strAlias;
                string strQry = " Select top 1 isnull(max(Serial),0) as Serial from mstTeamBalika inner join mst5Village on  mst5Village.VillageCode=mstTeamBalika.VillageCode or  mst5Village.OldUniqueCode=mstTeamBalika.VillageCode or  mst5Village.RefVillageCode=mstTeamBalika.VillageCode inner join mst3Block on  mst3Block.BlockCode=mst5Village.BlockCode where mst5Village.DistrictCode='" + ddlDistrict.SelectedValue + "'   ";
                //string strQry = " Select top 1 Serial from tblDTD   order by Serial desc ";
                DataTable dt = objMain.LoadData(strQry);

                string strQry1 = " Select EGVillageCode,VillageCode  from mst5Village where VillageCode='" + ddlVillage.SelectedValue + "' ";
                DataTable dtVillage = objMain.LoadData(strQry1);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Serial"].ToString() == "" || dt.Rows[0]["Serial"].ToString() == "-1")
                    {
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');
                        ViewState["TBCode"] = "TB" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;
                        ViewState["NumNo"] = strAlias;
                    }
                    else
                    {
                        mNewNo = Convert.ToInt32(dt.Rows[0]["Serial"].ToString());
                        mNewNo += 1;
                        strAlias = mNewNo.ToString().PadLeft(5, '0');

                        ViewState["NumNo"] = strAlias;
                        ViewState["TBCode"] = "TB" + "-" + dtVillage.Rows[0]["EGVillageCode"] + "-" + strAlias;

                    }

                }
                else
                {
                    mNewNo += 1;
                    strAlias = mNewNo.ToString().PadLeft(5, '0');
                    ViewState["TBCode"] = "TB" + "-" + strAlias;
                    ViewState["NumNo"] = strAlias;
                }
            }
        }

    }
}