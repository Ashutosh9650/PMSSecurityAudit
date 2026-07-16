using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
/// <summary>
/// Summary description for ExportdataWebservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ExportdataWebservice : System.Web.Services.WebService
{
    Comman objComman = new Comman();
    clsMain objMain = new clsMain();
    Password objPass = new Password();
    clsMain DBTask = new clsMain();
    public ExportdataWebservice()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    [WebMethod]
    public string GetReportStatus(string UserName)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Report_Status_Data", para);
        DataSet sqldata = new DataSet("MyData");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableNameTB(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;
    }
    private string GetTableNameTB(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tbl_Report";
                break;


            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }

    [WebMethod]
    public string GetMasterData(string UserName)
    {


        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_data", para);


        DataSet sqldata = new DataSet("MyData");
        int index = 0;

        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableNameTablate(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;
    }


    [WebMethod]
    public string GetMasterDataTablet(string UserName, string Password)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.CheckPassword(UserName, checkpass);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTablet", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNew(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GetMasterDataDownload(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.CheckPasswordNew(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNew", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateDownload(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GetMasterDataDownloadVillage(string UserName, string Password, string IMEINo, string VillageCode)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.CheckPasswordNew(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@VillageCode",VillageCode),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNewVillageBO", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateDownloadVillage(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    public string GetMasterDataDownloadVillage20190629(string UserName, string Password, string IMEINo, string VillageCode)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@VillageCode",VillageCode),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNewVillageBO2019", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateDownloadVillage2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GetMasterDataDownloadBO(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNewBO", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateDownload(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    public string GetMasterDataDownloadBO20190629(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNewBO20190629", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateDownload2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string GetTableNameTablateDownloadVillage2019(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mstSchool";
                break;

            case 1:
                tablename = "tblActivity_School";
                break;

            case 2:
                tablename = "tblActivity_Village";
                break;
            case 3:
                tablename = "TblActivityUpdate_Baseline_BO";
                break;

            case 4:
                tablename = "TblActivityUpdate_Office_BO";
                break;
            case 5:
                tablename = "tblTotal";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameTablateDownloadVillage(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mstSchool";
                break;

            case 1:
                tablename = "tblActivity_School";
                break;

            case 2:
                tablename = "tblActivity_Village";
                break;
            case 3:
                tablename = "TblActivityUpdate_Baseline_BO";
                break;

            case 4:
                tablename = "TblActivityUpdate_Office_BO";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameTablateDownloadBO(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "MstUser";

                break;

            case 6:
                tablename = "mstLookup";
                break;


            case 7:
                tablename = "MSTtopicDiscuss";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    private string GetTableNameTablateNew2019(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "MstUser";

                break;
            case 6:
                tablename = "Mstuserrole";
                break;
            case 7:
                tablename = "mstLookup";
                break;
            case 8:
                tablename = "mstSchool";
                break;
            case 9:
                tablename = "tblActivityUpdate_School";
                break;
            case 10:
                tablename = "tblActivityUpdate_Village";
                break;
            case 11:
                tablename = "tblDTD ";
                break;

            case 12:
                tablename = "tblCLT";
                break;
            case 13:
                tablename = "MSTtopicDiscuss";
                break;
            case 14:
                tablename = "TblActivityUpdate_Office";
                break;
            case 15:
                tablename = "TblActivityUpdate_Baseline";
                break;
            case 16:
                tablename = "mstGKPDeatils";
                break;

            case 17:
                tablename = "tblDTDMobileActivity";
                break;

            case 18:
                tablename = "tblTotal";
                break;
            case 19:
                tablename = "mstClassValdation";
                break;

            case 20:
                tablename = "tblOOSG";
                break;

            case 21:
                tablename = "tblChildOOSG";
                break;
            case 22:
                tablename = "mstSafetySecurity";
                break;
            case 23:
                tablename = "tblSafetySecurity";
                break;

            case 24:
                tablename = "mstTeamBalika";
                break;
            case 25:
                tablename = "tblChildRegistration";
                break;
            case 26:
                tablename = "tblChildAttendance";
                break;
            case 27:
                tablename = "tblVisitors";
                break;

            case 28:
                tablename = "MstCampNo";
                break;
            case 29:
                tablename = "MstLearningCampMaster";
                break;
            case 30:
                tablename = "TblCommunitySMC";
                break;
            case 31:
                tablename = "TblSMCAttendance";
                break;
            case 32:
                tablename = "tblLCGChildRegistration";
                break;
            case 33:
                tablename = "tblLSGChildAttendance";
                break;
            case 34:
                tablename = "mstInfluencerProfile";
                break;
            case 35:
                tablename = "tblChildandEnrolment";
                break;
            case 36:
                tablename = "tblChildRegistrationAGP";
                break;
            case 37:
                tablename = "tblChildAttendanceAGP";
                break;
            case 38:
                tablename = "tblVisitorsAGP";
                break;
            case 39:
                tablename = "MstLearningCampMasterAgp";
                break;
            case 40:
                tablename = "tblAttendanceImage";
                break;
            case 41:
                tablename = "MstSubCampNo";
                break;
            case 42:
                tablename = "MstSubLearningCampMaster";
                break;


            case 43:
                tablename = "tblChildRegistrationSchool";
                break;
            case 44:
                tablename = "tblChildAttendanceSchool";
                break;
            case 45:
                tablename = "tblVisitorsSchool";
                break;
            case 46:
                tablename = "tblAttendanceImageSchool";
                break;
            case 47:
                tablename = "tblChildAttendanceLifeskill";
                break;
            case 48:
                tablename = "tblChildRegistrationBalsabha";
                break;
            case 49:
                tablename = "tblAnualPlanDataDetail";
                break;
            case 50:
                tablename = "tblContactTarget";
                break;
            case 51:
                tablename = "tblSMCAttendanceNew";
                break;
            case 52:
                tablename = "masterGKP";
                break;
            case 53:
                tablename = "masterGkpDetails";
                break;
            case 54:
                tablename = "mstsacupdate";
                break;
            case 55:
                tablename = "tblClusterMeeting";
                break;
            case 56:
                tablename = "MstMobileUserRight";
                break;
            case 57:
                tablename = "tblOOSC";
                break;
            case 58:
                tablename = "mstGKPMasterReport";
                break;
            case 59:
                tablename = "masterGKPGyanodaya";
                break;
            case 60:
                tablename = "tblHoliday";
                break;
            case 61:
                tablename = "mstTBSchool";
                break;
            case 62:
                tablename = "MstUserBO";
                break;
            case 63:
                tablename = "mstOutcome";
                break;
            case 64:
                tablename = "mstOutcomeSpecific";
                break;
            case 65:
                tablename = "tblPlanActivity";
                break;
            case 66:
                tablename = "mstweek";
                break;
            case 67:
                tablename = "mstEnrollmentEntryvalidation";
                break;
            case 68:
                tablename = "tblTravelFare";
                break;
            case 69:
                tablename = "tblTravelMatrixMaximumAmount";
                break;
            case 70:
                tablename = "TravelMartrixPerDim";
                break;
            case 71:
                tablename = "Tbl_User_Login";
                break;
            case 72:
                tablename = "tblTravelMatrixDeatils2024";
                break;
            case 73:
                tablename = "tblTravelMatrixExpens";
                break;
            case 74:
                tablename = "tblTravelMatrixPerDiem";
                break;
            case 75:
                tablename = "tblRetention";
                break;

            case 76:
                tablename = "tblRound4Score";
                break;
            case 77:
                tablename = "tblPanchayatMeeting";
                break;
            case 78:
                tablename = "tblRatriChaupal";
                break;
            case 79:
                tablename = "tblEnrollmentRally";
                break;
            case 80:
                tablename = "tblChildAttendanceLifeskillKGBV";
                break;
            case 81:
                tablename = "tblChildRegistrationBalsabhaKGBV";
                break;
            case 82:
                tablename = "MstGKPSessionSequence";
                break;
            case 83:
                tablename = "tblSMCAttendanceChild";
                break;
            case 84:
                tablename = "mstTravelMatrixOtherLocation";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    private string GetTableNameTablateNew2019D2d(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblDTD ";
                break;

            case 1:
                tablename = "tblDTDMobileActivity";
                break;

            case 2:
                tablename = "tblTotal";
                break;
            case 3:
                tablename = "tblOOSC";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    public static DataSet GetDataSet(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(connString);
        SqlCommand cmd = new SqlCommand();

        try
        {
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParameters);
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

    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
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
    [WebMethod]
    public string GetMasterDataTabletNew20190626(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTablet20190626", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNew2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    public string GetMasterDataTabletNew20190725(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTablet20190725", para);
                int totalRowCount = dttabletdata.Tables.Cast<DataTable>()
                             .Sum(t => t.Rows.Count);

                DataSet sqldata = new DataSet("MyData");
                int index = 0;


                for (int i = 0; i < dttabletdata.Tables[18].Rows.Count; i++)
                {
                    dttabletdata.Tables[18].Rows[0]["TotalCount"] = totalRowCount;
                }


                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNew2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GetMasterDataTabletNew20190626D2d(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTablet20190626D2d2", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNew2019D2d(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    public string Get_Masters_TotalTarget(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_TotalTarget", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNametblContactTarget(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string GetTableNametblContactTarget(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblAnualPlanDataDetail ";
                break;

            case 1:
                tablename = "tblContactTarget";
                break;
            case 2:
                tablename = "tblDTD";
                break;

            case 3:
                tablename = "tblDTDMobileActivity";
                break;

            case 4:
                tablename = "tblEnrolment";
                break;

            case 5:
                tablename = "tblChildRegistration";
                break;

            case 6:
                tablename = "tblChildAttendance";
                break;
            case 7:
                tablename = "tblActivityUpdate_School";
                break;
            case 8:
                tablename = "tblActivityUpdate_Village";
                break;
            case 9:
                tablename = "tblOOSC";
                break;
            case 10:
                tablename = "tblOOSC";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    [WebMethod]
    public string GetMasterDataTabletNew(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTablet", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNew(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GettransactionTabletCount(string UserName, string Password, string IMEINo, int Flag)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.CheckPasswordNew(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
             new SqlParameter("@Flag",Flag),
            };

            try
            {
                DataTable dttabletdata = new DataTable();

                dttabletdata = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletAllCount", para);


                //DataSet sqldata = new DataSet("MyData");
                //int index = 0;

                //foreach (DataTable dt in dttabletdata.Tables)
                //{
                //    DataTable dtNew = new DataTable();
                //    dtNew = dt.Copy();
                //    dtNew.TableName = GetTableNameTablateNew(index);
                //    sqldata.Tables.Add(dtNew);
                //    index++;
                //}
                if (dttabletdata.Rows.Count > 0)
                {

                    sReturn = dttabletdata.Rows[0]["icount"].ToString();
                }
                else
                {
                    sReturn = "0";
                }

            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }



    [WebMethod]
    public string GetMasterDataTabletCount(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataTable dttabletdata = new DataTable();

                dttabletdata = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletCOunt", para);


                //DataSet sqldata = new DataSet("MyData");
                //int index = 0;

                //foreach (DataTable dt in dttabletdata.Tables)
                //{
                //    DataTable dtNew = new DataTable();
                //    dtNew = dt.Copy();
                //    dtNew.TableName = GetTableNameTablateNew(index);
                //    sqldata.Tables.Add(dtNew);
                //    index++;
                //}
                if (dttabletdata.Rows.Count > 0)
                {

                    sReturn = dttabletdata.Rows[0]["TotalCount"].ToString();
                }
                else
                {
                    sReturn = "0";
                }

            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }



    [WebMethod]
    public string GetMasterDataTabletBOCount(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataTable dttabletdata = new DataTable();

                dttabletdata = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNewBOCount", para);


                //DataSet sqldata = new DataSet("MyData");
                //int index = 0;

                //foreach (DataTable dt in dttabletdata.Tables)
                //{
                //    DataTable dtNew = new DataTable();
                //    dtNew = dt.Copy();
                //    dtNew.TableName = GetTableNameTablateNew(index);
                //    sqldata.Tables.Add(dtNew);
                //    index++;
                //}
                if (dttabletdata.Rows.Count > 0)
                {

                    sReturn = dttabletdata.Rows[0]["TotalCount"].ToString();
                }
                else
                {
                    sReturn = "0";
                }

            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GetMasterDataTabletVillageWise(string UserName, string Password, string Villagecode)
    {
        string sReturn = string.Empty;
        try
        {


            DataTable dtUser = objComman.GetUserAuthenticate(UserName, Password);

            if (dtUser.Rows.Count > 0)
            {
                // UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }





            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
              new SqlParameter("@Villagecode",Villagecode),

            };



            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletVillageWise", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNewVillagewise(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    [WebMethod]
    public string GettblEnrolment_Retention(string UserName, string Password, string Villagecode)
    {
        string sReturn = string.Empty;
        try
        {


            DataTable dtUser = objComman.GetUserAuthenticate(UserName, Password);

            if (dtUser.Rows.Count > 0)
            {
                // UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }





            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
              new SqlParameter("@Villagecode",Villagecode),

            };



            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Web_Get_tblEnrolment_Retention", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTable_r(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string GetTable_r(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblEnrolment_Retention ";
                break;

            case 1:
                tablename = "mstSchool_R";
                break;
            case 2:
                tablename = "TotalCount";
                break;
            case 3:
                tablename = "tblEnrolmentRetentionMain";
                break;
            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }
    [WebMethod]
    public string GetMasterDataTabletVillageWise20190626(string UserName, string Password, string Villagecode)
    {
        string sReturn = string.Empty;
        try
        {


            DataTable dtUser = objComman.GetUserAuthenticate(UserName, Password);

            if (dtUser.Rows.Count > 0)
            {
                // UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }





            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
              new SqlParameter("@Villagecode",Villagecode),

            };



            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletVillageWise20190626", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNewVillagewise2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    public string GetMasterDataTabletVillageWiseCOunt(string UserName, string Password, string Villagecode)
    {
        string sReturn = string.Empty;
        try
        {


            DataTable dtUser = objComman.GetUserAuthenticate(UserName, Password);

            if (dtUser.Rows.Count > 0)
            {
                // UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }





            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
              new SqlParameter("@Villagecode",Villagecode),

            };



            try
            {
                DataTable dttabletdata = new DataTable();

                dttabletdata = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletVillageWiseCount", para);


                if (dttabletdata.Rows.Count > 0)
                {

                    sReturn = dttabletdata.Rows[0]["TotalCount"].ToString();
                }
                else
                {
                    sReturn = "0";
                }
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string GetMasterDataTabletVillageWiseBOCOunt(string UserName, string Password, string Villagecode)
    {
        string sReturn = string.Empty;
        try
        {


            DataTable dtUser = objComman.GetUserAuthenticate(UserName, Password);

            if (dtUser.Rows.Count > 0)
            {
                // UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }





            SqlParameter[] para = new SqlParameter[] {


              new SqlParameter("@Villagecode",Villagecode),

            };



            try
            {
                DataTable dttabletdata = new DataTable();

                dttabletdata = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletNewVillageBOCount", para);


                if (dttabletdata.Rows.Count > 0)
                {

                    sReturn = dttabletdata.Rows[0]["TotalCount"].ToString();
                }
                else
                {
                    sReturn = "0";
                }
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    [WebMethod]
    public string GetMasterUser(string UserName, string Password)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",SqlDbType.NVarChar,50){Value =UserName },
             new SqlParameter("@Password",SqlDbType.NVarChar,200){Value= Password },

            };

        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();
        int flag;
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterUser", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableUser(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }

    [WebMethod]
    public string GetMasterState(string UserName)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterState", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableName(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    [WebMethod]
    public string GetMasterStateTest(string UserName)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterStateTest", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableNameTest(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    [WebMethod]
    public string GetMasterStateNew(string UserName)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterStateNew", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableName(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    [WebMethod]
    public string GetMasterStateTestNew(string UserName)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterStateTestNew", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableNameTest(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    [WebMethod]
    public string GetMasterVillage(string Condition, Int32 flag)
    {

        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Condition",Condition),
               new SqlParameter("@Flag",flag),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterVillage", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableName(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    private string GetTableTran(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblDTD ";
                break;

            case 1:
                tablename = "tblCLT";
                break;
            case 2:
                tablename = "Other";
                break;

        }

        return tablename;
    }

    private string GetTable(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblCLT";
                break;
            default:
                tablename = "Other";
                break;

        }

        return tablename;
    }
    [WebMethod]
    public string GeD2D(string Villagecode)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Villagecode",Villagecode),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetD2DAndEnroll", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableTran(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }

    [WebMethod]
    public string GetAnualData(string Villagecode)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Villagecode",Villagecode),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetAnualData", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableTran(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }

    [WebMethod]
    public string GetLearningLevel(string BlockCode)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@BlockCode",BlockCode),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetLearningLevel", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTable(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }

    [WebMethod]
    public string GeD2DDateWise(string Villagecode, Int32 DateNo)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Villagecode",Villagecode),
               new SqlParameter("@DateNo",DateNo),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetD2DAndEnrollDateWise", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableTran(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }
    [WebMethod]
    public string GeD2DNew(string Villagecode)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Villagecode",Villagecode),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetD2D", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableTran(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }



    [WebMethod]
    public string GeD2DTemp(string Villagecode)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Villagecode",Villagecode),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetD2DAndEnrollTemp", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableTran(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    [WebMethod]
    public string GeD2Dtablet(string Villagecode)
    {

        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Villagecode",Villagecode),

            };


        string sReturn = string.Empty;
        //try
        //{
        DataSet dttabletdata = new DataSet();

        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetD2DAndEnrolltablet", para);

        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableTran(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        //}
        //catch 
        //{
        //    sReturn = "9999";
        //}
        return sReturn;
    }


    [WebMethod]
    public string GetBatchData(string Con)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Condation",Con),


            };
        string sReturn = string.Empty;
        DataSet dttabletdata = new DataSet();
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Import_Batch", para);
        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = "tblImportView";
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;
    }

    [WebMethod]
    public string GetReprotData(string ReportFlag, string ReportDate, string UserName)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
            new SqlParameter("@ReportFlag",ReportFlag),
            new SqlParameter("@ReportDate",ReportDate),

            };
        string sReturn = string.Empty;
        DataSet dttabletdata = new DataSet();
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "EG_Tablet_Reports", para);
        DataSet sqldata = new DataSet("User");
        int index = 0;
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = "tbl_Reports";
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;
    }
    [WebMethod]
    public string Get_Master_LoadNew(string Flag, string StateCode, string DistCode, string BlockCode, string PCode)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@Flag",Flag),
            new SqlParameter("@StateCode",StateCode),
            new SqlParameter("@DistCode",DistCode),
             new SqlParameter("@BlockCode",BlockCode),
                new SqlParameter("@PCode",PCode),

            };
        string sReturn = string.Empty;
        DataSet dttabletdata = new DataSet();
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Tablet_NewMastReport", para);
        DataSet sqldata = new DataSet("User");
        int index = 0;
        string tableName = "";
        if (Flag == "S")
        {
            tableName = "mst1State";
        }
        if (Flag == "D")
        {
            tableName = "mst2District";
        }
        if (Flag == "B")
        {
            tableName = "mst3Block";
        }
        if (Flag == "P")
        {
            tableName = "mstPanchayat";
        }
        if (Flag == "V")
        {
            tableName = "mst5Village";
        }
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = tableName;
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;
    }


    [WebMethod]
    public string Get_Master_LoadNew2020(string UserName, string Flag, string StateCode, string DistCode, string BlockCode, string PCode)
    {
        string condition = "";
        SqlParameter[] para = new SqlParameter[] {
           new SqlParameter("@UserName",UserName),
            new SqlParameter("@Flag",Flag),
            new SqlParameter("@StateCode",StateCode),
            new SqlParameter("@DistCode",DistCode),
             new SqlParameter("@BlockCode",BlockCode),
                new SqlParameter("@PCode",PCode),

            };
        string sReturn = string.Empty;
        DataSet dttabletdata = new DataSet();
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Tablet_NewMastReportRetion", para);
        DataSet sqldata = new DataSet("User");
        int index = 0;
        string tableName = "";
        if (Flag == "S")
        {
            tableName = "mst1State_R";
        }
        if (Flag == "D")
        {
            tableName = "mst2District_R";
        }
        if (Flag == "B")
        {
            tableName = "mst3Block_R";
        }
        if (Flag == "P")
        {
            tableName = "mstPanchayat_R";
        }
        if (Flag == "V")
        {
            tableName = "mst5Village_R";
        }
        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = tableName;
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;
    }
    private string GetTableUser(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "MstUser ";
                break;

            case 1:
                tablename = "Mstuserrole";
                break;
            case 2:
                tablename = "tblemployeedetails";
                break;
            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }
    private string GetTableNameTest(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "mstLookup";
                break;
            case 6:
                tablename = "mstSchool";
                break;
            case 7:
                tablename = "tblemployeedetails";
                break;
            case 8:
                tablename = "MstUserRight";
                break;


            case 9:
                tablename = "tblActivityUpdate_School";
                break;
            case 10:
                tablename = "SchoolEnrolment";
                break;
            case 11:
                tablename = "tblcommunityMembers";
                break;
            case 12:
                tablename = "mstVillageTS";
                break;
            case 13:
                tablename = "mstCluster";
                break;
            case 14:
                tablename = "mstVillageDhani";
                break;
            case 15:
                tablename = "mstModuleLocking";
                break;
            case 16:
                tablename = "mstClassValdation";
                break;
            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }


    private string GetTableName(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "mstLookup";
                break;
            case 6:
                tablename = "mstSchool";
                break;
            case 7:
                tablename = "tblemployeedetails";
                break;
            case 8:
                tablename = "MstUserRight";
                break;
            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }

    private string GetTableNameTablateNewVillagewise(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {

            case 0:
                tablename = "MstUser";

                break;

            case 1:
                tablename = "mstSchool";
                break;
            case 2:
                tablename = "tblActivityUpdate_School";
                break;
            case 3:
                tablename = "tblActivityUpdate_Village";
                break;
            case 4:
                tablename = "tblDTD ";
                break;

            case 5:
                tablename = "tblCLT";
                break;
            case 6:
                tablename = "TblActivityUpdate_Office";
                break;
            case 7:
                tablename = "tblDTDMobileActivity";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    private string GetTableNameTablateNewVillagewise2019(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {

            case 0:
                tablename = "MstUser";

                break;

            case 1:
                tablename = "mstSchool";
                break;
            case 2:
                tablename = "tblActivityUpdate_School";
                break;
            case 3:
                tablename = "tblActivityUpdate_Village";
                break;
            case 4:
                tablename = "tblDTD ";
                break;

            case 5:
                tablename = "tblCLT";
                break;
            case 6:
                tablename = "TblActivityUpdate_Office";
                break;
            case 7:
                tablename = "tblDTDMobileActivity";
                break;
            case 8:
                tablename = "tblTotal";
                break;
            case 9:
                tablename = "tblEnrolment";
                break;
            case 10:
                tablename = "tblOOSG";
                break;
            case 11:
                tablename = "tblChildOOSG";
                break;
            case 12:
                tablename = "tblVerification";
                break;

            case 13:
                tablename = "mstTeamBalika";
                break;

            case 14:
                tablename = "tblChildRegistration";
                break;

            case 15:
                tablename = "tblChildAttendance";
                break;

            case 16:
                tablename = "tblVisitors";
                break;
            case 17:
                tablename = "tblCLLSG";
                break;
            case 18:
                tablename = "TblCommunitySMC";
                break;
            case 19:
                tablename = "TblSMCAttendance";
                break;
            case 20:
                tablename = "tblLCGChildRegistration";
                break;
            case 21:
                tablename = "tblLSGChildAttendance";
                break;
            case 22:
                tablename = "mstInfluencerProfile";
                break;
            case 23:
                tablename = "tblChildandEnrolment";
                break;
            case 24:
                tablename = "tblChildRegistrationAGP";
                break;
            case 25:
                tablename = "tblChildAttendanceAGP";
                break;
            case 26:
                tablename = "tblVisitorsAGP";
                break;
            case 27:
                tablename = "MstLearningCampMasterAgp";
                break;
            case 28:
                tablename = "tblAttendanceImage";
                break;
            case 29:
                tablename = "tblChildRegistrationSchool";
                break;
            case 30:
                tablename = "tblChildAttendanceSchool";
                break;
            case 31:
                tablename = "tblVisitorsSchool";
                break;
            case 32:
                tablename = "tblAttendanceImageSchool";
                break;
            case 33:
                tablename = "tblChildAttendanceLifeskill";
                break;
            case 34:
                tablename = "tblChildRegistrationBalsabha";
                break;
            case 35:
                tablename = "tblDTDMobileActivityVerification";
                break;
            case 36:
                tablename = "tblSMCAttendanceNew";
                break;
            case 37:
                tablename = "tblChildRegistrationGKP";
                break;
            case 38:
                tablename = "tblChildAttendanceGKP";
                break;
            case 39:
                tablename = "tblClusterMeeting";
                break;

            case 40:
                tablename = "tblOOSC";
                break;
            case 41:
                tablename = "tblOOSCNew";
                break;
            case 42:
                tablename = "tblHoushold";
                break;
            case 43:
                tablename = "tblSurvey";
                break;
            case 44:
                tablename = "tblEnrolmentGKP";
                break;

            case 45:
                tablename = "tblRandomSessionPhoto";
                break;

            case 46:
                tablename = "tblChildRegistrationGyanodaya";
                break;

            case 47:
                tablename = "tblChildAttendanceGyanodaya";
                break;
            case 48:
                tablename = "tblHousholdExpansion";
                break;
            case 49:
                tablename = "tblSurveyExpansion";
                break;
            case 50:
                tablename = "tblExpOtherVillageDetails";
                break;
            case 51:
                tablename = "tblBalikaAndInfluencer";
                break;
            case 52:
                tablename = "tblAudioRecording";
                break;
            case 53:
                tablename = "mstTBSchool";
                break;
            case 54:
                tablename = "tblEnrolmentCV";
                break;
            case 55:
                tablename = "tblChildRegistrationGKPPlus";
                break;
            case 56:
                tablename = "tblChildAttendanceGKPPlus";
                break;


            case 57:
                tablename = "tblHousholdExpansionNew";
                break;

            case 58:
                tablename = "tblSurveyExpansionNew";
                break;

            case 59:
                tablename = "tblExpOtherVillageDetailsNew";
                break;

            case 60:
                tablename = "tblBalikaAndInfluencerNew";
                break;
            case 61:
                tablename = "tblPanchayatMeeting";
                break;
            case 62:
                tablename = "tblRatriChaupal";
                break;
            case 63:
                tablename = "tblEnrollmentRally";
                break;
            case 64:
                tablename = "tblChildAttendanceLifeskillKGBV";
                break;
            case 65:
                tablename = "tblChildRegistrationBalsabhaKGBV";
                break;

            case 66:
                tablename = "tblLocationDetails";
                break;
            case 67:
                tablename = "tblSessionWiseDetails";
                break;
            case 68:
                tablename = "tblFemaleDetails";
                break;
            case 69:
                tablename = "tblSMCAttendanceChild";
                break;
            case 70:
                tablename = "tblChildGyanodayaAttendanceGKP";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameTablateDownload2019(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "MstUser";

                break;
            case 6:
                tablename = "Mstuserrole";
                break;
            case 7:
                tablename = "mstLookup";
                break;
            case 8:
                tablename = "mstSchool";
                break;

            case 9:
                tablename = "MSTtopicDiscuss";
                break;
            case 10:
                tablename = "tblActivity_School";
                break;
            case 11:
                tablename = "tblActivity_Village";
                break;

            case 12:
                tablename = "TblActivityUpdate_Baseline_BO";
                break;
            case 13:
                tablename = "TblActivityUpdate_Office_BO";
                break;
            case 14:
                tablename = "mstGKPDeatils";
                break;
            case 15:
                tablename = "mstOutcome";
                break;
            case 16:
                tablename = "mstOutcomeSpecific";
                break;
            case 17:
                tablename = "mstlearning";
                break;
            case 18:
                tablename = "tblTotal";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameTablateDownload(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "MstUser";

                break;
            case 6:
                tablename = "Mstuserrole";
                break;
            case 7:
                tablename = "mstLookup";
                break;
            case 8:
                tablename = "mstSchool";
                break;

            case 9:
                tablename = "MSTtopicDiscuss";
                break;
            case 10:
                tablename = "tblActivity_School";
                break;
            case 11:
                tablename = "tblActivity_Village";
                break;

            case 12:
                tablename = "TblActivityUpdate_Baseline_BO";
                break;
            case 13:
                tablename = "TblActivityUpdate_Office_BO";
                break;
            case 14:
                tablename = "mstGKPDeatils";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameTablateNew(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "MstUser";

                break;
            case 6:
                tablename = "Mstuserrole";
                break;
            case 7:
                tablename = "mstLookup";
                break;
            case 8:
                tablename = "mstSchool";
                break;
            case 9:
                tablename = "tblActivityUpdate_School";
                break;
            case 10:
                tablename = "tblActivityUpdate_Village";
                break;
            case 11:
                tablename = "tblDTD ";
                break;

            case 12:
                tablename = "tblCLT";
                break;
            case 13:
                tablename = "MSTtopicDiscuss";
                break;
            case 14:
                tablename = "TblActivityUpdate_Office";
                break;
            case 15:
                tablename = "TblActivityUpdate_Baseline";
                break;
            case 16:
                tablename = "mstGKPDeatils";
                break;

            case 17:
                tablename = "tblDTDMobileActivity";
                break;


            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    private string GetTableNameTablate(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mst1State ";
                break;

            case 1:
                tablename = "mst2District";
                break;

            case 2:
                tablename = "mst3Block";
                break;
            case 3:
                tablename = "mstPanchayat";
                break;

            case 4:
                tablename = "mst5Village";
                break;
            case 5:
                tablename = "MstUser";

                break;
            case 6:
                tablename = "Mstuserrole";
                break;
            case 7:
                tablename = "mstLookup";
                break;
            case 8:
                tablename = "mstSchool";
                break;
            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string PostLoginTables(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                //DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                //if (dtUser.Rows.Count > 0)
                //{
                //    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                //}
                UserID = 1;

            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_User_Login");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_User_Login"], dtTbl_User_Login);
                    DataTable DttblActivityUpdate_CLT = objComman.CreateDataTable("tblActivityUpdate_CLT");
                    DttblActivityUpdate_CLT = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_CLT"], DttblActivityUpdate_CLT);
                    DataTable DttblActivityUpdate_CTLImplementation = objComman.CreateDataTable("tblActivityUpdate_CTLImplementation");
                    DttblActivityUpdate_CTLImplementation = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_CTLImplementation"], DttblActivityUpdate_CTLImplementation);
                    DataTable DttblActivityUpdate_LifeskillGames = objComman.CreateDataTable("tblActivityUpdate_LifeskillGames");
                    DttblActivityUpdate_LifeskillGames = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_LifeskillGames"], DttblActivityUpdate_LifeskillGames);
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_School");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_Village");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataTable dttblDTD = objComman.CreateDataTable("tblDTD");
                    dttblDTD = SetColumnsOrdinal(dsMyData.Tables["tblDTD"], dttblDTD);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.IU_PostLogintables(dtTbl_User_Login, DttblActivityUpdate_CLT, DttblActivityUpdate_CTLImplementation, DttblActivityUpdate_LifeskillGames, DttblActivityUpdate_School, DttblActivityUpdate_Village, dttblDTD, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionUserLogin(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_User_LoginVersion");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_User_Login"], dtTbl_User_Login);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_User_Login(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionUserLoginNew(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                string checkpass = objPass.CreatePasswordHashSecurityAudit(Pass);

                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_User_LoginNewLogin");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_User_Login"], dtTbl_User_Login);

                    foreach (DataRow dr1 in dtTbl_User_Login.Rows)
                    {

                        string LoginTime = dr1["LoginTime"].ToString();

                        if (Convert.ToInt32(LoginTime.Length.ToString()) > 9)
                        {
                            string strnew = dr1["LoginTime"].ToString().Substring(6, 1);
                            if (strnew == "a")
                            {
                                string v = LoginTime.Replace("a.m.", "AM");
                                dr1["LoginTime"] = v;
                            }
                            else
                            {
                                string v = LoginTime.Replace("p.m.", "PM");
                                dr1["LoginTime"] = v;
                            }

                        }
                    }

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_User_LoginNew(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Tbl_GKP(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_GKP");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_GKP"], dtTbl_User_Login);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Tbl_GKP(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Tbl_GKPNew(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_GKPNew");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_GKP"], dtTbl_User_Login);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Tbl_GKPNew(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Tbl_GKPNew20190725(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_GKPNew2019");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_GKP"], dtTbl_User_Login);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Tbl_GKPNew20190725(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Tbl_GKPNewBO(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_GKPNewBO");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_GKP"], dtTbl_User_Login);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Tbl_GKPNewBO(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Tbl_GKPNewBO20190904(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_GKPNewBO2019");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_GKP_BO"], dtTbl_User_Login);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Tbl_GKPNewBO(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionLoginEntry(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_User_LoginNewLoginInt");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_User_Login"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_User_LoginNewDateAsInt(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionLoginEntryBO(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_User_LoginNewLoginInt");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_User_Login_BO"], dtTbl_User_Login);

                    foreach (DataRow dr1 in dtTbl_User_Login.Rows)
                    {

                        string LoginTime = dr1["LoginTime"].ToString();

                        if (Convert.ToInt32(LoginTime.Length.ToString()) > 9)
                        {
                            string strnew = dr1["LoginTime"].ToString().Substring(6, 1);
                            string strnew1 = dr1["LoginTime"].ToString().Substring(6, 5);
                            if (strnew1 == "अपराह")
                            {

                                string v = LoginTime.Replace("अपराह्न", "PM");
                                dr1["LoginTime"] = v;

                            }
                            if (strnew == "a")
                            {
                                string v = LoginTime.Replace("a.m.", "AM");
                                dr1["LoginTime"] = v;
                            }

                            if (strnew == "p")
                            {
                                string v = LoginTime.Replace("p.m.", "PM");
                                dr1["LoginTime"] = v;
                            }


                        }
                    }
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_User_LoginNewDateAsIntBO(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessiontrackRandomLatLong(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("TblTrackRandomLatLong");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_TrackRandomLatLong"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_TblTrackRandomLatLong(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateCLT(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_CLT = objComman.CreateDataTable("tblActivityUpdate_CLT");
                    DttblActivityUpdate_CLT = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_CLT"], DttblActivityUpdate_CLT);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_CLT(DttblActivityUpdate_CLT, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionCTLImplementation(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_CTLImplementation = objComman.CreateDataTable("tblActivityUpdate_CTLImplementation");
                    DttblActivityUpdate_CTLImplementation = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_CTLImplementation"], DttblActivityUpdate_CTLImplementation);

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_CTLImplementation(DttblActivityUpdate_CTLImplementation, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionLifeskillGames(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_LifeskillGames = objComman.CreateDataTable("tblActivityUpdate_LifeskillGames");
                    DttblActivityUpdate_LifeskillGames = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_LifeskillGames"], DttblActivityUpdate_LifeskillGames);

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_LifeskillGames(DttblActivityUpdate_LifeskillGames, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }





    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateSchool(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateSchooll20190719(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew20190719");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20190719(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateSchooll2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew2023");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School2023(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }




    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateSchooll202313(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew202313");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School202313(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool20230610(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew20230610");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20230610(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTblActivityUpdate_Baseline(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DtTblActivityUpdate_Baseline = objComman.CreateDataTable("TblActivityUpdate_Baseline");
                    DtTblActivityUpdate_Baseline = SetColumnsOrdinal(dsMyData.Tables["TblActivityUpdate_Baseline"], DtTblActivityUpdate_Baseline);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.TblActivityUpdate_Baseline(DtTblActivityUpdate_Baseline, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionDTD(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dttblDTD = objComman.CreateDataTable("tblDTD");
                    dttblDTD = SetColumnsOrdinal(dsMyData.Tables["tblDTD"], dttblDTD);

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_tblDTD(dttblDTD, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [ScriptMethod(UseHttpGet = true), WebMethod]
    public string TabletPostSessionDTDNew(string sData, string UserName, string Pass)
    {
        string result = string.Empty;
        try
        {
            new DataSet();
            int num = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable userAuthenticate = this.objComman.GetUserAuthenticate(UserName, Pass);
                if (userAuthenticate.Rows.Count > 0)
                {
                    num = Convert.ToInt32(userAuthenticate.Rows[0]["UserID"].ToString());
                }
            }
            if (num != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dataTable = objComman.CreateDataTable("tblDTDNew");
                    dataTable = SetColumnsOrdinal(dsMyData.Tables["tblDTD"], dataTable);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_tblDTDNew(dataTable, num, sData);
                    result = JsonConvert.SerializeObject(dsResult);
                }
                else
                {
                    result = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                result = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            result = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return result;
    }



    [ScriptMethod(UseHttpGet = true), WebMethod]
    public string TabletPostSessionDTDContact(string sData, string UserName, string Pass)
    {
        string result = string.Empty;
        try
        {
            new DataSet();
            int num = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable userAuthenticate = this.objComman.GetUserAuthenticate(UserName, Pass);
                if (userAuthenticate.Rows.Count > 0)
                {
                    num = Convert.ToInt32(userAuthenticate.Rows[0]["UserID"].ToString());
                }
            }
            if (num != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dataTable = objComman.CreateDataTable("tblDTDNewContact");
                    dataTable = SetColumnsOrdinal(dsMyData.Tables["tblDTD"], dataTable);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_tblDTDNewContact(dataTable, num, sData);
                    result = JsonConvert.SerializeObject(dsResult);
                    result = "{\"Table\":[{\"RetValue\":1}]}";
                }
                else
                {
                    result = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                result = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            result = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return result;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateOffice(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable TblActivityUpdate_Office = objComman.CreateDataTable("TblActivityUpdate_Office");
                    TblActivityUpdate_Office = SetColumnsOrdinal(dsMyData.Tables["TblActivityUpdate_Office"], TblActivityUpdate_Office);

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdateOffice(TblActivityUpdate_Office, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateOfficeNew(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable TblActivityUpdate_Office = objComman.CreateDataTable("TblActivityUpdate_OfficeNew");
                    TblActivityUpdate_Office = SetColumnsOrdinal(dsMyData.Tables["TblActivityUpdate_Office"], TblActivityUpdate_Office);

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdateOfficeNew(TblActivityUpdate_Office, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    public string UploadImageActivitySchool(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {

            // string stockImagesDir = ConfigurationManager.AppSettings["ImagesPath"].ToString();

            string sDirectory = Server.MapPath(Comman.GetImagePath("TabletImagePath"));

            //string sCNDirectory = sDirectory + "\\" + sUID + "\\";
            //string sFilename = sDirectory + "\\" + sUID + "\\" + fileName;
            byte[] sfilebytes = Convert.FromBase64String(filebytes);

            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            //if (!Directory.Exists(sCNDirectory))
            //    Directory.CreateDirectory(sCNDirectory);

            // instance a filestream pointing to the
            // storage folder, use the original file name
            // to name the resulting file
            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {
                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }


            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }


    [WebMethod]
    public string UploadImageSealSine(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {

            // string stockImagesDir = ConfigurationManager.AppSettings["ImagesPath"].ToString();

            string sDirectory = Server.MapPath(Comman.GetImagePath("TabletImagePath"));

            //string sCNDirectory = sDirectory + "\\" + sUID + "\\";
            //string sFilename = sDirectory + "\\" + sUID + "\\" + fileName;
            byte[] sfilebytes = Convert.FromBase64String(filebytes);

            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            //if (!Directory.Exists(sCNDirectory))
            //    Directory.CreateDirectory(sCNDirectory);

            // instance a filestream pointing to the
            // storage folder, use the original file name
            // to name the resulting file
            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {
                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }

            if (!File.Exists(sFilename))
            {
                return "FAIL";
            }
            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }

    [WebMethod]
    public string UploadImageVillageActivity(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {

            // string stockImagesDir = ConfigurationManager.AppSettings["ImagesPath"].ToString();

            string sDirectory = Server.MapPath(Comman.GetImagePath("TabletImagePath"));

            //string sCNDirectory = sDirectory + "\\" + sUID + "\\";
            //string sFilename = sDirectory + "\\" + sUID + "\\" + fileName;
            byte[] sfilebytes = Convert.FromBase64String(filebytes);

            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            //if (!Directory.Exists(sCNDirectory))
            //    Directory.CreateDirectory(sCNDirectory);

            // instance a filestream pointing to the
            // storage folder, use the original file name
            // to name the resulting file
            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {
                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }

            // string sReturnValue = string.Empty;
            // sReturnValue = DBTask.IUUpdateImageName(sImageFieldName, sFilename, sUID, sFlag);
            // return OK if we made it this far
            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }

    private DataTable SetColumnsOrdinal(DataTable dtData, DataTable dtCols)
    {
        try
        {
            if (dtData != null)
            {
                List<string> list = new List<string>();
                foreach (DataColumn colName in dtData.Columns)
                {
                    list.Add(colName.ToString());
                }
                for (int i = 0; i < list.Count; i++)
                {
                    if (!dtCols.Columns.Contains(list[i].ToString()))
                    {
                        dtData.Columns.Remove(list[i].ToString());
                    }
                }

                for (int i = 0; i < dtCols.Columns.Count; i++)
                {
                    dtData.Columns[dtCols.Columns[i].ToString()].SetOrdinal(i);
                }
            }
            else
            {
                dtData = dtCols.Copy();
            }

            return dtData;
        }
        catch
        {
            return dtData;
        }
    }





    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletInsertActivityUpdate(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivity_School");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityillage(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivity_Village");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivity_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.TabletActivityUpdateVillage(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }




    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_ActivityUpdate_Baseline_BO(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("TblActivityUpdate_Baseline_BO");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["TblActivityUpdate_Baseline_BO"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_ActivityUpdate_Baseline_BO(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_TblActivityUpdate_Office_BO(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("TblActivityUpdate_Office_BO");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["TblActivityUpdate_Office_BO"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_TblActivityUpdate_Office_BO(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2018(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2020(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2020");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2020(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2020New(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2020New");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2020New(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2020NewChange(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2020ChnageNew");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2020NewChange(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2021(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2021");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2021(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2022(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2022");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2022(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2023");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivity"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2023(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2022Verification(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2022Verification");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivityVerification"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2022Verification(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_D2dNew2022Verification2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblDTDMobile2023Verification");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblDTDMobileActivityVerification"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_DTDMobileActivity2023Verification(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string PasswordChangeNew(string UserName, string Password)
    {
        string sReturn = string.Empty;
        try
        {

            Int32 UserID = 0;



            try
            {
                if (UserName.Trim() != "")
                {
                    Password ojbP = new Password();
                    string NewPassWord = ojbP.CreatePasswordHashSecurityAudit(Password.Trim());
                    SqlParameter[] pr = new SqlParameter[]
                    {
                       new SqlParameter("@UserID",UserName.ToString()),

                       new SqlParameter("@NewPwd",NewPassWord),

                   };
                    int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp_Change_User_PasswordNew", pr);
                    if (result > 0)
                    {

                        sReturn = "{\"Table\":[{\"RetValue\":1}]}";

                    }
                    else
                    {
                        sReturn = "{\"Table\":[{\"RetValue\":9999}]}";

                    }
                }

            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string PasswordChange(string UserName, string Password)
    {
        string sReturn = string.Empty;
        try
        {

            Int32 UserID = 0;



            try
            {
                if (UserName.Trim() != "")
                {
                    Password ojbP = new Password();
                    string NewPassWord = ojbP.CreatePasswordHashSecurityAudit(Password.Trim());
                    SqlParameter[] pr = new SqlParameter[]
                    {
                       new SqlParameter("@UserID",UserName.ToString()),

                       new SqlParameter("@NewPwd",NewPassWord),

                   };
                    int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp_Change_User_PasswordNew", pr);
                    if (result > 0)
                    {
                        sReturn = "1";
                    }
                    else
                    {
                        sReturn = "9999";
                    }
                }

            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_New_Village(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("AdtionalAddVillage");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["mst5Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_AdtionalAddVillage(DttblActivityUpdate_Village, UserName, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    public string GeneraateMail()
    {
        DateTime _DateTime = DateTime.Now;
        string StartupPath = Server.MapPath("~/");
        Int32 mMonth = DateTime.Today.Month - 1;
        int pmonth = mMonth + 1;
        Int32 cYear = DateTime.Today.Year;
        Int32 oldYear = DateTime.Today.Year;
        if (Convert.ToInt32(DateTime.Now.Month) == 1 || Convert.ToInt32(DateTime.Now.Month) == 2 || Convert.ToInt32(DateTime.Now.Month) == 3)
        {
            cYear = cYear - 1;
            oldYear = cYear - 1;
        }
        if (Convert.ToInt32(DateTime.Now.Day) >= 1 && Convert.ToInt32(DateTime.Now.Day) <= 8)
        {
            mMonth = Convert.ToInt32(DateTime.Now.Month) - 2;
            pmonth = mMonth + 1;
        }
        string startdate = "";
        string enddate = "";


        startdate = "21" + "/" + mMonth + "/" + cYear + "";
        enddate = "20" + "/" + pmonth + "/" + cYear + "";



        if (Convert.ToInt32(DateTime.Now.Day) >= 21 && Convert.ToInt32(DateTime.Now.Day) <= 31 || Convert.ToInt32(DateTime.Now.Day) >= 01 && Convert.ToInt32(DateTime.Now.Day) <= 08)
        {

            string strQry31 = "Select * from mstTravelLockDate where mMonth=" + pmonth + " and TypeID=1 and Status=0   ";


            DataTable dtHoliday = objMain.LoadData(strQry31);
            if (dtHoliday.Rows.Count > 0)
            {
                if (Convert.ToDateTime(dtHoliday.Rows[0]["PDate"].ToString()).ToString("yyyy-MM-dd") == Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"))
                {
                    DataTable dtemployeeCheck = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,mst3Block.BlockCode,LockDay,mst2District.districtCode ", "mYear=" + cYear + " and mMonth=" + pmonth + " and  (SubmissionStatus is null or SubmissionStatus ='') ", "", "");
                    if (dtemployeeCheck.Rows.Count > 0)
                    {
                        for (int E = 0; E < dtemployeeCheck.Rows.Count; E++)
                        {
                            GenerateExcel("", dtemployeeCheck.Rows[E]["BlockCode"].ToString(), dtemployeeCheck.Rows[E]["BlockName"].ToString(), pmonth.ToString(), cYear.ToString(), startdate, enddate, dtemployeeCheck.Rows[E]["districtCode"].ToString());

                        }


                    }

                }

            }

        }


        if (Convert.ToInt32(DateTime.Now.Day) >= 21 && Convert.ToInt32(DateTime.Now.Day) <= 31 || Convert.ToInt32(DateTime.Now.Day) >= 01 && Convert.ToInt32(DateTime.Now.Day) <= 08)
        {

            string strQry31 = "Select * from mstTravelLockDate where mMonth=" + pmonth + " and TypeID=2 and Status=0   ";


            DataTable dtHoliday = objMain.LoadData(strQry31);
            if (dtHoliday.Rows.Count > 0)
            {
                if (Convert.ToDateTime(dtHoliday.Rows[0]["PDate"].ToString()).ToString("yyyy-MM-dd") == Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"))

                {

                    DataTable dtemployeeCheck = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,LockDay ,CONVERT(varchar,BOApprovalDate,103) as BOApprovalDate", "mYear=" + cYear + " and mMonth=" + pmonth + " and SubmissionStatus='P' ", "", "");
                    if (dtemployeeCheck.Rows.Count > 0)
                    {
                        for (int E = 0; E < dtemployeeCheck.Rows.Count; E++)
                        {
                            GenerateExcel("", dtemployeeCheck.Rows[E]["BlockCode"].ToString(), dtemployeeCheck.Rows[E]["BlockName"].ToString(), pmonth.ToString(), cYear.ToString(), startdate, enddate, dtemployeeCheck.Rows[E]["districtCode"].ToString());

                        }

                    }
                }

            }

        }
        if (Convert.ToInt32(DateTime.Now.Day) >= 21 && Convert.ToInt32(DateTime.Now.Day) <= 31 || Convert.ToInt32(DateTime.Now.Day) >= 01 && Convert.ToInt32(DateTime.Now.Day) <= 08)
        {

            string strQry31 = "Select * from mstTravelLockDate where mMonth=" + pmonth + " and TypeID=3 and Status=0   ";


            DataTable dtHoliday = objMain.LoadData(strQry31);
            if (dtHoliday.Rows.Count > 0)
            {
                if (Convert.ToDateTime(dtHoliday.Rows[0]["PDate"].ToString()).ToString("yyyy-MM-dd") == Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd"))

                {
                    DataTable dtemployeeCheck = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,LockDay ,CONVERT(varchar,AdminApprovalDate,103) as BOApprovalDate", "mYear=" + cYear + " and mMonth=" + pmonth + " and SubmissionStatus='A' ", "", "");

                    //   DataTable dtemployeeCheck = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,LockDay ,CONVERT(varchar,BOApprovalDate,103) as BOApprovalDate", "mYear=" + cYear + " and mMonth=" + pMonth + " and SubmissionStatus='P' ", "", "");
                    if (dtemployeeCheck.Rows.Count > 0)
                    {
                        for (int E = 0; E < dtemployeeCheck.Rows.Count; E++)
                        {
                            GenerateExcel("", dtemployeeCheck.Rows[E]["BlockCode"].ToString(), dtemployeeCheck.Rows[E]["BlockName"].ToString(), pmonth.ToString(), cYear.ToString(), startdate, enddate, dtemployeeCheck.Rows[E]["districtCode"].ToString());

                        }

                    }
                }

            }

        }





        return "";
    }
    //public string GeneraateMail()
    //{
    //    DateTime _DateTime = DateTime.Now;
    //    string StartupPath = Server.MapPath("~/");
    //    Int32 mMonth = 9;
    //    int pmonth = mMonth + 1;
    //    Int32 cYear = DateTime.Today.Year;
    //    Int32 oldYear = DateTime.Today.Year;
    //    if (Convert.ToInt32(DateTime.Now.Month) == 1 || Convert.ToInt32(DateTime.Now.Month) == 2 || Convert.ToInt32(DateTime.Now.Month) == 3)
    //    {
    //        cYear = cYear - 1;
    //        oldYear = cYear - 1;
    //    }
    //    if (Convert.ToInt32(DateTime.Now.Day) >= 1 && Convert.ToInt32(DateTime.Now.Day) <= 8)
    //    {
    //        mMonth = Convert.ToInt32(DateTime.Now.Month) - 1;
    //        pmonth = mMonth + 1;
    //    }
    //    string startdate = "";
    //    string enddate = "";
    //    if (mMonth == 1)
    //    {
    //        string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


    //        DataTable dtTravelRang = objMain.LoadData(strQry1);
    //        startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + 12 + "/" + oldYear + "";
    //        enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + 01 + "/" + cYear + "";


    //    }
    //    else if (mMonth == 2)
    //    {
    //        string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


    //        DataTable dtTravelRang = objMain.LoadData(strQry1);

    //        startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + mMonth + "/" + cYear + "";
    //        enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";



    //    }
    //    else if (mMonth == 3)
    //    {
    //        string strQry1 = "Select * from mstTravelDateRange  where mMonth=" + mMonth + "  ";


    //        DataTable dtTravelRang = objMain.LoadData(strQry1);

    //        startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + mMonth + "/" + cYear + "";
    //        enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";



    //    }
    //    else if (mMonth == 4)
    //    {
    //        string strQry1 = "Select * from mstTravelDateRange  where mMonth=" + mMonth + "  ";


    //        DataTable dtTravelRang = objMain.LoadData(strQry1);

    //        startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + 4 + "/" + cYear + "";
    //        enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";


    //    }
    //    else
    //    {
    //        string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


    //        DataTable dtTravelRang = objMain.LoadData(strQry1);

    //        startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + mMonth + "/" + cYear + "";
    //        enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";


    //    }
    //    string Fdate = "";
    //    string Tdate = "";
    //    if (mMonth == 2)
    //    {

    //        Fdate = "01" + "/" + mMonth + "/" + cYear + "";
    //        Tdate = "28" + "/" + mMonth + "/" + cYear + "";
    //    }
    //    else
    //    {
    //        Fdate = "01" + "/" + mMonth + "/" + cYear + "";
    //        Tdate = "30" + "/" + mMonth + "/" + cYear + "";
    //    }
    //    SqlParameter[] paramvT = new SqlParameter[]
    //                {                            
    //                        new SqlParameter("@FormDate",Convert.ToDateTime(Fdate).ToString("yyyy-MM-dd")),
    //                         new SqlParameter("@ToDate",Convert.ToDateTime(Tdate).ToString("yyyy-MM-dd")),
    //                };

    //    DataTable dtWeak = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadWeakDay", paramvT);

    // //   string date = DateTime.Today.ToString("yyyy-MM-dd");

    //    string strQry31 = "Select * from mstHoliday    ";


    //    DataTable dtHoliday= objMain.LoadData(strQry31);

    //    string date ="2018-09-27"; 
    //    string dateNew =Convert.ToDateTime(enddate).ToString("yyyy-MM-dd");
    //    DataRow[] drRow;
    //    DataRow[] drRowHoli;

    //    if (Convert.ToInt32(DateTime.Now.Day) >= 21 && Convert.ToInt32(DateTime.Now.Day) <= 31 || Convert.ToInt32(DateTime.Now.Day) >= 01 && Convert.ToInt32(DateTime.Now.Day) <= 08)
    //    {
    //        if ( Convert.ToDateTime(dateNew)>Convert.ToDateTime(date) )
    //        {
    //            DataTable dtemployeeCheck = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,mst3Block.BlockCode,LockDay,mst2District.districtCode ", "mYear=" + cYear + " and mMonth=" + mMonth + " and  (SubmissionStatus is null or SubmissionStatus ='') ", "", "");

    //             if (dtemployeeCheck.Rows.Count > 0)
    //             {
    //                 Int32 Days = 0;
    //                 Int32 DaysNew = 0;
    //                 Int32 iCount = 0;
    //                 DateTime FinalDate = Convert.ToDateTime(enddate);

    //                 DaysNew = Convert.ToInt32(dtemployeeCheck.Rows[0]["LockDay"]);
    //                 for (int E = 0; E < dtemployeeCheck.Rows.Count; E++)
    //                 {
    //                     Days = Convert.ToInt32(dtemployeeCheck.Rows[E]["LockDay"]);
    //                     DateTime FinalDateNew = Convert.ToDateTime(enddate).AddDays(1);

    //                     for (int i = 0; i < Days; i++)
    //                     {
    //                         FinalDateNew = Convert.ToDateTime(FinalDateNew).AddDays(1);
    //                         drRow = dtWeak.Select("DT='" + FinalDateNew.ToShortDateString() + "' and WD_No in(2,4)");


    //                         drRowHoli = dtHoliday.Select("HoliDayDate='" + FinalDateNew.ToShortDateString() + "'");
    //                         string str = FinalDateNew.ToString("ddd");
    //                         if (drRow.Length > 0 || drRowHoli.Length > 0 || str == "Sun")
    //                         {
    //                             Days = Days + 1;
    //                             iCount = iCount + 1;
    //                         }
    //                         //else
    //                         //{
    //                         //    FinalDateNew = Convert.ToDateTime(FinalDateNew).AddDays(1);
    //                         //}



    //                     }

    //                     DateTime d1 = Convert.ToDateTime(FinalDate);
    //                     DateTime d2 = Convert.ToDateTime(FinalDateNew).AddDays(Convert.ToInt32("-" + iCount));
    //                     TimeSpan t = d2 - d1;

    //                     double DaysCount = Convert.ToDouble(t.TotalDays);

    //                     if (DaysCount > Days)
    //                     {
    //                         GenerateExcel("", dtemployeeCheck.Rows[E]["BlockCode"].ToString(), dtemployeeCheck.Rows[E]["BlockName"].ToString(), mMonth.ToString(), cYear.ToString(), startdate, enddate, dtemployeeCheck.Rows[E]["districtCode"].ToString());


    //                     }


    //                 }


    //             }


    //             DataTable dtemployeeCheckAdmin = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,LockDay ,CONVERT(varchar,BOApprovalDate,103) as BOApprovalDate", "mYear=" + cYear + " and mMonth=" + mMonth + " and SubmissionStatus='P' ", "", "");

    //             if (dtemployeeCheckAdmin.Rows.Count > 0)
    //             {
    //                 Int32 Days = 0;
    //                 Int32 DaysNew = 0;
    //                 string FinalDateAdmin1 = "";
    //                 DateTime FinalDateAdmin;
    //                 for (int E = 0; E < dtemployeeCheckAdmin.Rows.Count; E++)
    //                 {
    //                      FinalDateAdmin1 = Convert.ToDateTime(dtemployeeCheckAdmin.Rows[E]["BOApprovalDate"].ToString()).ToString("yyyy-MM-dd");
    //                      FinalDateAdmin = Convert.ToDateTime(FinalDateAdmin1);
    //                     Days = Convert.ToInt32(dtemployeeCheckAdmin.Rows[E]["LockDay"]);
    //                     for (int i = 0; i < Days; i++)
    //                     {
    //                         FinalDateAdmin = Convert.ToDateTime(FinalDateAdmin).AddDays(i);
    //                         drRow = dtWeak.Select("DT='" + FinalDateAdmin.ToShortDateString() + "' and WD_No in(2,4)");
    //                         if (drRow.Length > 0)
    //                         {
    //                             Days = Days + 1;
    //                         }

    //                         string str = FinalDateAdmin.ToString("ddd");
    //                         if (str == "Sun")
    //                         {
    //                             Days = Days + 1;
    //                         }

    //                         FinalDateAdmin = Convert.ToDateTime(FinalDateAdmin).AddDays(i);
    //                     }
    //                     if (Convert.ToDateTime(date) > Convert.ToDateTime(FinalDateAdmin))
    //                     {
    //                         GenerateExcel("", dtemployeeCheck.Rows[E]["BlockCode"].ToString(), dtemployeeCheck.Rows[E]["BlockName"].ToString(), mMonth.ToString(), cYear.ToString(), startdate, enddate, dtemployeeCheck.Rows[E]["districtCode"].ToString());

    //                     }

    //                 }


    //             }

    //             DataTable dtemployeeCheckAcoo = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct  mst2District.districtname,BlockName,LockDay ,CONVERT(varchar,AdminApprovalDate,103) as BOApprovalDate", "mYear=" + cYear + " and mMonth=" + mMonth + " and SubmissionStatus='A' ", "", "");

    //             if (dtemployeeCheckAcoo.Rows.Count > 0)
    //             {
    //                 Int32 Days = 0;
    //                 Int32 DaysNew = 0;
    //                 string FinalDateAdmin1 = "";
    //                 DateTime FinalDateAdmin;
    //                 for (int E = 0; E < dtemployeeCheckAcoo.Rows.Count; E++)
    //                 {
    //                     FinalDateAdmin1 = Convert.ToDateTime(dtemployeeCheckAcoo.Rows[E]["BOApprovalDate"].ToString()).ToString("yyyy-MM-dd");
    //                     FinalDateAdmin = Convert.ToDateTime(FinalDateAdmin1);
    //                     Days = Convert.ToInt32(dtemployeeCheckAdmin.Rows[E]["LockDay"]);
    //                     for (int i = 0; i < Days; i++)
    //                     {
    //                         FinalDateAdmin = Convert.ToDateTime(FinalDateAdmin).AddDays(i);

    //                         drRow = dtWeak.Select("DT='" + FinalDateAdmin.ToShortDateString() + "' and WD_No in(2,4)");
    //                         if (drRow.Length > 0)
    //                         {
    //                             Days = Days + 1;
    //                         }
    //                         string str = FinalDateAdmin.ToString("ddd");
    //                         if (str == "Sun")
    //                         {
    //                             Days = Days + 1;
    //                         }
    //                     }
    //                     if (Convert.ToDateTime(FinalDateAdmin1) > Convert.ToDateTime(FinalDateAdmin))
    //                     {
    //                         GenerateExcel("", dtemployeeCheck.Rows[E]["BlockCode"].ToString(), dtemployeeCheck.Rows[E]["BlockName"].ToString(), mMonth.ToString(), cYear.ToString(), startdate, enddate, dtemployeeCheck.Rows[E]["districtCode"].ToString());

    //                     }

    //                 }


    //             }
    //        }
    //    }
    //    return "";
    //}
    protected string GeneraatePDF()
    {
        DateTime _DateTime = DateTime.Now;
        string StartupPath = Server.MapPath("~/");
        Int32 mMonth = DateTime.Now.Month - 1;
        int pmonth = mMonth + 1;
        Int32 cYear = DateTime.Today.Year;
        Int32 oldYear = DateTime.Today.Year;
        if (Convert.ToInt32(DateTime.Now.Month) == 1 || Convert.ToInt32(DateTime.Now.Month) == 2 || Convert.ToInt32(DateTime.Now.Month) == 3)
        {
            cYear = cYear - 1;
            oldYear = cYear - 1;
        }
        if (Convert.ToInt32(DateTime.Now.Day) >= 1 && Convert.ToInt32(DateTime.Now.Day) <= 8)
        {
            mMonth = Convert.ToInt32(DateTime.Now.Month) - 2;
            pmonth = mMonth + 1;
        }
        string str1 = Convert.ToString(_DateTime.Year);
        if (!Directory.Exists(StartupPath + "Travel vouchers\\" + str1))
        {
            Directory.CreateDirectory(StartupPath + "Travel vouchers\\" + str1);
        }
        string str2 = Convert.ToString(_DateTime.Month.ToString().PadLeft(2, '0'));
        if (!Directory.Exists(StartupPath + "Travel vouchers\\" + str1 + "\\" + str2))
        {
            Directory.CreateDirectory(StartupPath + "Travel vouchers\\" + str1 + "\\" + str2);
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        try
        {

            DataTable dtemployeeCheck = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode ", "distinct mstuser.UserName as code,districtName,BlockName, mstuser.UserID,mstuser.BlockCode as BlockCode,mMonth ", "mYear=" + cYear + " and mMonth=" + pmonth + " and SubmissionStatus='F' and GenerateFlag=0", "", "");
            for (int E = 0; E < dtemployeeCheck.Rows.Count; E++)
            {
                string StartupPath1 = AppDomain.CurrentDomain.BaseDirectory;

                String DIst = dtemployeeCheck.Rows[E]["districtName"].ToString();

                String BlockName = dtemployeeCheck.Rows[E]["BlockName"].ToString();
                if (!Directory.Exists(StartupPath1 + "Travel vouchers\\" + str1 + "\\" + str2 + "\\" + DIst))
                {
                    Directory.CreateDirectory(StartupPath1 + "Travel vouchers\\" + str1 + "\\" + str2 + "\\" + DIst);
                }
                if (!Directory.Exists(StartupPath1 + "Travel vouchers\\" + str1 + "\\" + str2 + "\\" + DIst + "\\" + BlockName))
                {
                    Directory.CreateDirectory(StartupPath1 + "Travel vouchers\\" + str1 + "\\" + str2 + "\\" + DIst + "\\" + BlockName);
                }
                #region PDF
                string empname = "", empcode = "", designation = "", district = "", Block = "", cluster = "", depatment = ""; ;
                DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils on  MstUser.UserID=tblTravelMatrixDeatils.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level inner join tblemployeedetails on tblemployeedetails.EmployeeID=MstUser.UserName inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode", "distinct FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,Department ,mst2District.districtname,BlockName,VillageName as cluster", "mYear=" + cYear + "  and tblTravelMatrixDeatils.UserID=" + dtemployeeCheck.Rows[E]["UserID"].ToString() + " and mMonth=" + pmonth + " and SubmissionStatus='F' and GenerateFlag=0 ", "", "");

                if (dtemployee.Rows.Count > 0)
                {

                    empname = dtemployee.Rows[0]["name"].ToString();
                    empcode = dtemployee.Rows[0]["code"].ToString();
                    designation = dtemployee.Rows[0]["desg"].ToString();
                    district = dtemployee.Rows[0]["districtname"].ToString();
                    Block = dtemployee.Rows[0]["BlockName"].ToString();
                    cluster = dtemployee.Rows[0]["cluster"].ToString();
                    depatment = dtemployee.Rows[0]["Department"].ToString();

                }
                string startdate = "";
                string enddate = "";
                if (mMonth == 1)
                {
                    string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


                    DataTable dtTravelRang = objMain.LoadData(strQry1);
                    startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + 12 + "/" + oldYear + "";
                    enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + 01 + "/" + cYear + "";


                }
                else if (mMonth == 2)
                {
                    string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


                    DataTable dtTravelRang = objMain.LoadData(strQry1);

                    startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + mMonth + "/" + cYear + "";
                    enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";



                }
                else if (mMonth == 3)
                {
                    string strQry1 = "Select * from mstTravelDateRange  where mMonth=" + mMonth + "  ";


                    DataTable dtTravelRang = objMain.LoadData(strQry1);

                    startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + mMonth + "/" + cYear + "";
                    enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";



                }
                else if (mMonth == 4)
                {
                    string strQry1 = "Select * from mstTravelDateRange  where mMonth=" + mMonth + "  ";


                    DataTable dtTravelRang = objMain.LoadData(strQry1);

                    startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + 4 + "/" + cYear + "";
                    enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";


                }
                else
                {
                    string strQry1 = "Select * from mstTravelDateRange  where mMonth=0  ";


                    DataTable dtTravelRang = objMain.LoadData(strQry1);

                    startdate = dtTravelRang.Rows[0]["FromDay"].ToString() + "/" + mMonth + "/" + cYear + "";
                    enddate = dtTravelRang.Rows[0]["ToDay"].ToString() + "/" + pmonth + "/" + cYear + "";


                }


                //  string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";

                string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";


                sb.Append("<table width='100%' cellspacing='0' cellpadding='2'>");



                DataTable dttravelmatrixdetails = objMain.Select_All_Data("tblTravelMatrixDeatils", "convert(varchar,TravelDate,103) as Fromdate,convert(varchar,TravelDate,103) as Todate,LoginTime as TimeIn,logouttime as Timeout, [FromVillagename] as [FromVillagename],[ToVillagename] ,isnull(RevisedFare,0) as LC,isnull(RevisedDAAdmin,0) as DA", "mYear=" + cYear + " and tblTravelMatrixDeatils.UserID=" + dtemployeeCheck.Rows[E]["UserID"].ToString() + "   and mMonth=" + pmonth + " and SubmissionStatus='F' and GenerateFlag=0 ", "TravelDate", "ASC");

                int tot = 0;
                int DA = 0;
                //if (pageindex <= 15)
                //{
                sb.Append("<tr style='font-size:20px;'>");
                sb.Append("<td style='font-size:20px;text-align:center'>");


                sb.Append("<table width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>Foundation to Educate Girls Globally <img  width='50%' height='50%' src='" + imageURLLogo + "' alt='Bird' /> </td>");

                sb.Append("</tr>");
                sb.Append("</table>");


                sb.Append("<table  width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>Travel Settlement form</td>");
                sb.Append("</tr>");

                sb.Append("</table>");



                sb.Append("<table bgColor='#BDD7EE' width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");
                sb.Append("<tr style='font-size:12px;font-weight:bold;background-color='Red''><td width='14%' >Name of Employee:</td><td width='14%'>Employee Code</td><td width='15%'>Designation</td><td width='15%'>Reporting Manager</td><td width='14%' valign='top'>District / Office Name</td><td width='14%'>Block Name</td><td width='14%'>Cluster Name</td></tr>");
                sb.Append("</table>");

                DataTable sqldtTourPlan = new DataTable();

                sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:10px'>");

                sb.Append("<td width='14%' valign='top'>" + empname + "</td>");
                sb.Append("<td width='14%' valign='top'>" + empcode + "</td>");
                sb.Append("<td width='15%' valign='top'>" + designation + "</td>");
                sb.Append("<td width='15%' valign='top'>" + empname + "</td>");
                sb.Append("<td width='14%' valign='top'>" + district + "</td>");
                sb.Append("<td width='14%' valign='top'>" + Block + "</td>");
                sb.Append("<td width='14%' valign='top'>" + cluster + "</td></tr>");
                sb.Append("</table>");
                sb.Append("<table   background-color='#F1F1F1' width='100%' cellspacing='0' cellpadding='2'>");

                sb.Append("</table>");

                sb.Append("<table bgColor='#BDD7EE' width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:12px;font-weight:bold'><td width='14%'>Department:</td><td width='14%'>Department Code</td><td width='15%'>Work Level</td><td width='15%' colspan='4'>Settlement Period</td></tr>");

                sb.Append("</table>");

                sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:10px'>");


                sb.Append("<td width='14%' valign='top'>" + depatment + "</td>");
                sb.Append("<td width='14%' valign='top'></td>");
                sb.Append("<td width='15%' valign='top'></td>");
                sb.Append("<td bgColor='#BDD7EE' width='15%' valign='top'>From:</td>");
                sb.Append("<td width='14%' valign='top'>" + startdate + "</td>");
                sb.Append("<td bgColor='#BDD7EE' width='14%' valign='top'>To:</td>");
                sb.Append("<td width='14%' valign='top'>" + enddate + "</td></tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table>");
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style='border-color:#dddddd'>");

                sb.Append("<tr  bgColor='#BDD7EE' style='font-size:12px;font-weight:bold'><td width='14%' colspan='10'></td><td width='14%'>Cost Centre 013</td><td width='14%'>Cost Centre 012</td><td width='14%'>Cost Centre 011</td><td width='14%'>Cost Centre 010</td><td width='14%' colspan='2'></td></tr>");



                // sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td  style='display:none' width='14%'>Date from:</td><td width='14%'>Time In:</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");
                sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='14%'>Date from</td><td width='14%'>Time In</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");

                sb.Append("</table>");





                if (dttravelmatrixdetails.Rows.Count > 0)
                {
                    //int rownum = 5;
                    //int p = 5;


                    for (int i = 0; i < dttravelmatrixdetails.Rows.Count; i++)
                    {

                        sb.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:10px; border-color:#dddddd;font-weight:normal'> ");
                        sb.Append("<tr>");
                        sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>");
                        sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>");
                        sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>");
                        sb.Append("<td width='15%' valign='top'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>");
                        sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>");
                        sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>");
                        sb.Append("<td width='14%' valign='top'></td>");
                        sb.Append("<td width='14%' valign='top'></td>");
                        sb.Append("<td width='15%' valign='top'></td>");
                        sb.Append("<td width='15%' valign='top'></td>");
                        sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["LC"] + "</td>");
                        sb.Append("<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>");
                        sb.Append("<td width='15%' valign='top'></td>");
                        sb.Append("<td width='14%' valign='top'></td>");
                        sb.Append("<td width='14%' valign='top'></td>");
                        Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["LC"]) + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"]);
                        sb.Append("<td width='14%' valign='top'>" + DATA + "</td>");

                        sb.Append("</table>");

                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["LC"]);

                        DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"]);
                    }


                }
                else
                {

                }



                sb.Append("</td>");
                sb.Append("</tr>");
                //    }

                //add table here
                sb.Append("<tr style='font-size:9px;'>");
                sb.Append("<td style='font-size:9px;'>");
                sb.Append("<table  width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:9px; border-color:#dddddd'>");
                sb.Append("<tr>");
                sb.Append("<td colspan='10'></td>");

                sb.Append("<td style='text-align:center'>" + tot + "</td>");

                sb.Append("<td style='text-align:center' > " + DA + "</td>");

                sb.Append("<td style='text-align:center' >  0.00</td>");

                sb.Append("<td style='text-align:center' >  0.00</td>");

                sb.Append("<td style='text-align:center'>  0.00</td>");
                int TOalDA = tot + DA;
                sb.Append("<td style='text-align:center'>  " + TOalDA + "</td>");


                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td colspan='10'>  Total number of pages :</td>");

                sb.Append("<td> </td>");

                sb.Append("<td>  </td>");

                sb.Append("<td> Advances:</td>");

                sb.Append("<td> </td>");

                sb.Append("<td>  </td>");

                sb.Append("<td style='text-align:center'>  0.00</td>");


                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td colspan='10'>  Total number of pages :</td>");

                sb.Append("<td> </td>");

                sb.Append("<td>  </td>");

                sb.Append("<td> TOTAL REIMBURSEMENT:</td>");

                sb.Append("<td> </td>");

                sb.Append("<td>  </td>");

                sb.Append("<td style='text-align:center'>  0.00</td>");


                sb.Append("</tr>");


                sb.Append("<tr>");
                sb.Append("<td colspan='4'>Submitted by Employee</td>");

                sb.Append("<td colspan='4'> Approved By Reporting Manager </td>");

                sb.Append("<td colspan='4'> Verified by Team Administration </td>");

                sb.Append("<td colspan='4'>Paid by Team Accounts</td>");


                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("</tr>");


                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");





                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>");
                sb.Append("<table width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr  bgColor='#FF6600' style='font-size:12px;font-weight:bold;background-color='Red''><td width='14%' >DATE</td><td width='14%'>DESCRIPTION</td><td width='15%'>Conveyance</td><td width='15%'>Others</td></tr>");

                sb.Append("<tr  style='font-size:12px;font-weight:bold;background-color='Red''><td width='14%' ></td><td width='14%'></td><td width='15%'></td><td width='15%'></td></tr>");

                sb.Append("</table>");

                sb.Append("</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");
                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>&nbsp;&nbsp;</td>");
                sb.Append("</tr>");

                sb.Append("<tr style='font-size:20px;font-weight:bold'>");
                sb.Append("<td style='font-size:20px;text-align:center'>");
                sb.Append("<table width='100%' cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:10px'>");

                sb.Append("<td width='14%' valign='top'>");

                sb.Append("<table border=1 width='100%' style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Lodging</td>");
                sb.Append("<td width='14%' valign='top'>This includes accommodation at Hotel/Guest House. In case of hotel, please indicate whether it is self paid or B2C</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>DA</td>");
                sb.Append("<td width='14%' valign='top'>This includes per diem payable as per work level & appropriate discounting for meals provided by organization</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Local Conveyance</td>");
                sb.Append("<td width='14%' valign='top'>This includes commute through auto, bus, cab for both local and outstation travellers in respective cities.</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Travel Expenses</td>");
                sb.Append("<td width='14%' valign='top'>This includes outstation travel expenses across districts. In addition, any exigency travel booked by self can be mentioned here.</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Others</td>");
                sb.Append("<td width='14%' valign='top'>This includes any miscellaneous expenses incurred during travel and approved by Reporting Manager.</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td colspan='2' width='14%' valign='top'></td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Administration Stamp</td>");
                sb.Append("<td width='14%' valign='top'>In case of multiple pages/Annexure attached as a part of the claim, the stamp will be done on the cover/last page of the claim form where the final total amount and breakup is mentioned. It is important to check the number of pages as attached match with the final count of pages as mentioned.</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Accounts Stamp</td>");
                sb.Append("<td width='14%' valign='top'>Team Accounts should mention the exact date when they receive the claim form for payment from Team Administration and the date of payment to the employee.</td>");

                sb.Append("</tr>");

                sb.Append("</table>");
                sb.Append("</td>");
                sb.Append("<td width='14%' valign='top'>");
                sb.Append("<table border=1 width='100%'  style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Department Code</td>");
                sb.Append("<td width='14%' valign='top'>Department Name</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130001</td>");
                sb.Append("<td width='14%' valign='top'>Communications</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130002</td>");
                sb.Append("<td width='14%' valign='top'>Development</td>");

                sb.Append("</tr>");


                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130003</td>");
                sb.Append("<td width='14%' valign='top'>Impact</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130004</td>");
                sb.Append("<td width='14%' valign='top'>Operations</td>");

                sb.Append("</tr>");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130005</td>");
                sb.Append("<td width='14%' valign='top'>Program</td>");

                sb.Append("</tr>");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130006</td>");
                sb.Append("<td width='14%' valign='top'>Learning & Development</td>");

                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("</td>");
                sb.Append("<td width='15%' valign='top'>");
                sb.Append("<table border=1 width='100%'  style=' font-size:17px; border-color:#dddddd'> ");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>Department Code</td>");
                sb.Append("<td width='14%' valign='top'>Department Name</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130007</td>");
                sb.Append("<td width='14%' valign='top'>Government Liasion</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130016</td>");
                sb.Append("<td width='14%' valign='top'>Volunteer Engagement</td>");

                sb.Append("</tr>");


                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130009</td>");
                sb.Append("<td width='14%' valign='top'>Finance & Accounts</td>");

                sb.Append("</tr>");

                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130010</td>");
                sb.Append("<td width='14%' valign='top'>HR & Administration</td>");

                sb.Append("</tr>");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130005</td>");
                sb.Append("<td width='14%' valign='top'>Program</td>");

                sb.Append("</tr>");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130011</td>");
                sb.Append("<td width='14%' valign='top'>IT</td>");

                sb.Append("</tr>");

                sb.Append("</tr>");
                sb.Append("<tr style='font-size:9px'>");

                sb.Append("<td width='14%' valign='top'>130012</td>");
                sb.Append("<td width='14%' valign='top'>ED Office</td>");

                sb.Append("</tr>");
                sb.Append("</table>");
                sb.Append("</td>");


                sb.Append("</table>");

                sb.Append("</td>");
                sb.Append("</tr>");



                sb.Append("</table>");




                string Fullfilename = dtemployeeCheck.Rows[E]["Code"].ToString() + ".pdf";


                string path = StartupPath1 + "Travel vouchers\\" + str1 + "\\" + str2 + "\\" + DIst + "\\" + BlockName + "\\" + Fullfilename;


                StringReader sr = new StringReader(sb.ToString());
                Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 70f, 70f, 20f, 10f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    pdfDoc.NewPage();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    File.WriteAllBytes(path, bytes);
                }


                #endregion
            }


        }
        catch (System.Exception ex)
        {

            //   Response.Clear();

            //string mmsg = ex.Message;
            //showEXPMessages("(crateZip)  " + mmsg); //showMessages(mmsg);
        }
        finally
        {

            //Response.Clear();

        }

        return sb.ToString();

    }

    [WebMethod]
    public string GetGenreatePD()
    {
        string sReturn = string.Empty;
        try
        {



            GeneraatePDF();
            GeneraateMail();
            try
            {

                sReturn = "0";


            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private void GenerateExcel(string FIleName, string blockCode, string blockName, string Mmonth, string mYear, string startdate, string enddate, string DistCode)
    {
        try
        {
            string conditions = " where mst3Block.BlockCode ='" + blockCode + "'";
            //  conditions = conditions + " and TravelDate between ( '" + Convert.ToDateTime(startdate).ToString("yyyy-MM-dd") + " ') and  ( '" + Convert.ToDateTime(enddate).ToString("yyyy-MM-dd") + "')";
            conditions = conditions + " and mMonth=" + Mmonth + " and mYear=" + mYear + "";

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@month", Mmonth),
            new SqlParameter("@Year",mYear),
            new SqlParameter("@flag", "4")
            };
            DataTable dataTable = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAStatusReport]", cmdParameters);
            System.Text.StringBuilder sw = new System.Text.StringBuilder();

            DataTable dt = dataTable.Copy();
            if (dt.Rows.Count > 0)
            {
                #region Excel Download
                //string Fullfilename1 = "" + blockName + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".xls";
                //string fileName = Server.MapPath(Comman.GetImagePath("DataBackupPath")+"/" + Fullfilename1 + "");
                //StreamWriter sw = new StreamWriter(fileName, false);

                //sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                //HttpContext.Current.Response.Charset = "utf-8";
                //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";


                //sw.Append("<table width='100%' cellspacing='0' cellpadding='2'>");







                sw.Append("<table width='100%' border=1 cellspacing='2' cellpadding='2' style=' font-size:17px; border-color:#dddddd;BACKGROUND-COLOR:Red'> ");

                sw.Append("<tr style='font-size:20px;font-weight:bold'>");
                sw.Append("<td style='font-size:20px;text-align:center'>Foundation to Educate Girls Globally <img  width='50%' height='50%' src='" + imageURLLogo + "' alt='Bird' /> </td>");

                sw.Append("</tr>");
                sw.Append("</table>");

                sw.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style='border-color:#dddddd'>");



                // sb.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td  style='display:none' width='14%'>Date from:</td><td width='14%'>Time In:</td><td width='15%'>Date To</td><td width='15%'>Time Out</td><td width='15%'>Travelling from</td><td width='15%'>Travelling to</td><td width='15%'>Purpose of Visit</td><td width='15%'>Lodging (Guest House/Hotel)</td><td width='15%'>Self Paid / BTC (applicable for hotel booking)</td><td width='15%'>Mode of Travel</td><td width='15%'>Local conveyance</td><td width='15%'>Lodging</td><td width='15%'>DA</td><td width='15%'>Travel Expenses</td><td width='15%'>Others</td><td width='15%'>Total</td></tr>");
                sw.Append("<tr  bgColor='#A9D08E' style='font-size:12px;font-weight:bold'><td width='14%'>Sr No.</td><td width='14%'>District Name</td></td><td width='14%'>Block Name</td><td width='15%'>Travel Period</td><td width='15%'>Total Amount</td><td width='15%'>	BO Approval Status</td><td width='15%'>BO Approval Date</td><td width='15%'>Admin Verification Status</td><td width='15%'>Admin Verification Date</td><td width='15%'>Processed for Payment</td><td width='15%'>Processed Date</td></tr>");

                sw.Append("</table>");




                //string HeaderStyle = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all; ";

                //sw.Append("<table>");

                //sw.Append("<tr style='font-width:bold;'>");

                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Sr No.	</th>");

                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Block Name	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Travel Period	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Total Amount	</th>");

                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BO Approval Status	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	BO Approval Date	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Admin Verification Status	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Admin Verification Date	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Processed for Payment	</th>");
                //sw.Append("<th class='header'  style='" + HeaderStyle + "  width:2%;'> 	Processing Date	</th>");

                //sw.Append("</tr>");

                //String RowStyle = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;";

                //String RowStyle1 = "border:.1pt solid windowtext; font-weight:100; font-size:9pt;rowspan=2;background:#FFFF00;";


                //String HeaderStyle1 = "border:.3pt solid windowtext; font-weight:700;   word-wrap: normal; word-break: break-all;text-align:center; ";

                string villagecode = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {



                    sw.Append("<table border=1 width='100%' cellspacing='2' cellpadding='2' style=' font-size:10px; border-color:#dddddd;font-weight:normal'> ");
                    sw.Append("<tr>");
                    sw.Append("<td width='14%' valign='top'>" + dt.Rows[i]["Sr No."] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["District/Office  Name"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Block Name"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Travel Period"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Total Amount"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["BO Approval Status"] + "</td>");
                    //sw.Append("<td width='14%' valign='top'></td>");
                    //sw.Append("<td width='14%' valign='top'></td>");
                    //sw.Append("<td width='15%' valign='top'></td>");
                    //sw.Append("<td width='15%' valign='top'></td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["BO Approval Date"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Admin Verification Status"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Admin Verification Date"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Processed for Payment"] + "</td>");
                    sw.Append("<td width='15%' valign='top'>" + dt.Rows[i]["Processing Date"] + "</td>");

                    //sw.Append("<td width='15%' valign='top'></td>");
                    //sw.Append("<td width='15%' valign='top'></td>");
                    //sw.Append("<td width='15%' valign='top'></td>");
                    sw.Append("</tr>");

                    sw.Append("</table>");






                }
                //Int32 TOtal = 0;
                //Int32 Local = 0;
                //Int32 DA = 0;
                //for (int j = 0; j < dt.Rows.Count; j++)
                //{


                //    if (dt.Rows[j]["Total Amount"].ToString() != "")
                //    {
                //        TOtal += Convert.ToInt32(dt.Rows[j]["Total Amount"]);
                //    }




                //}
                //sw.Append("<tr>");
                //sw.Append("<td colspan='10'  style='" + HeaderStyle1 + "'>Total</td>");
                //sw.Append("<td style='" + HeaderStyle + "'>" + TOtal + "</td>");

                //sw.Append("</tr>");
                //sw.Append("</table>");

                string Fullfilename = "TADAClaimStatusReport" + "_" + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss") + ".pdf";

                string StartupPath1 = AppDomain.CurrentDomain.BaseDirectory;

                string path = StartupPath1 + "Travel vouchers\\" + Fullfilename;


                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A2, 70f, 70f, 20f, 10f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);


                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();
                    pdfDoc.NewPage();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();

                    File.WriteAllBytes(path, bytes);
                }
                #region Mail
                //SqlParameter[] cmdParameter5 = new SqlParameter[]
                //{
                //    new SqlParameter("@Con", "where MstUser.UserID = '" + ddlFc.SelectedValue + "' "),
                //    new SqlParameter("@Con1", "where MstUser.BlockCode = '" + ddlBlock.SelectedValue + "'"),

                //};
                //DataSet dtEmail = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTADAMail]", cmdParameter5);
                //if (dtEmail.Tables[0].Rows.Count > 0)
                //{
                string email = "";
                //  string email = "mukta.arora@educategirls.ngo";
                //string email = "aksingh06mca@gmail.com";
                //email = dtEmail.Tables[0].Rows[0]["EmaillID"].ToString();
                //string ccemail = dtEmail.Tables[1].Rows[0]["EmaillID"].ToString();
                DataTable dtemployeeEmail = objMain.Select_All_Data("MstUser inner join tblemployeedetails on tblemployeedetails.EmployeeID=MstUser.UserName ", " EmaillID,MstUser.Username ", " UserLevel=91 and ActiveStatus=1 and DistrictCode=" + DistCode + " ", "", "");

                if (dtemployeeEmail.Rows.Count > 0)
                {
                    email = dtemployeeEmail.Rows[0]["EmaillID"].ToString();
                }
                if (email.Length > 0)
                {
                    string str = "TA/DA Claim Status";
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("PMS.Team@educategirls.ngo");
                    mail.To.Add("" + email + "");//
                                                 //if (ccemail.Length > 4)
                                                 //{
                                                 //    mail.CC.Add("" + ccemail + "");
                                                 //}
                    mail.Subject = str;
                    //ViewState["Body"] = "Dear  " + ViewState["EmployeeName"].ToString() + ",<br/><br/> Your Leave  for  " + ViewState["Noofdays"].ToString() + " days from " + HiddenField1.Value + " to " + HiddenField2.Value + " has been rejected,<br/> Reason:" + cmb_Reason.SelectedItem.Text + " <br/>Remarks:" + TxtRemark.InnerText + "<br/><br/> Regards,<br/> Name:" + ViewState["Supempname"].ToString() + "<br/> Post:" + ViewState["superdesg"].ToString() + "<br/> ";
                    string body = "Dear Sir/Madam,<br/><br/> Please find attached TA/DA Claim Status for the month " + Mmonth + "/" + mYear + " for your kind review.<br/><br/> Regards,<br/> PMS Team ";
                    mail.IsBodyHtml = true;
                    mail.Body = body;
                    System.Net.Mail.Attachment attachment;
                    if ((File.Exists(path)))
                    {
                        attachment = new System.Net.Mail.Attachment(path);
                        mail.Attachments.Add(attachment);
                    }
                    System.Net.Mail.Attachment attachment1;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("PMS.Team@educategirls.ngo", "PMSTeam2018");
                    SmtpServer.EnableSsl = true;



                    SmtpServer.Send(mail);


                }

                //}
                #endregion

                #endregion
            }

        }
        catch
        {
            throw;
        }

    }

    [WebMethod]
    public string Get_tblEnrolment_Temp(string UserName)
    {
        DataSet dttabletdata = new DataSet();
        SqlParameter[] para = new SqlParameter[] {
            new SqlParameter("@UserName",UserName),
            };
        string sReturn = string.Empty;
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Web_Get_tblEnrolment_Temp", para);
        // DataSet sqldata = new DataSet("User");


        DataSet sqldata = new DataSet("MyData");
        int index = 0;

        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableNameTablateEN(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;

        //foreach (DataTable dt in dttabletdata.Tables)
        //{
        //    DataTable dtNew = new DataTable();  
        //    dtNew = dt.Copy();            
        //    try
        //    {
        //        dtNew.Columns.RemoveAt(0);
        //    }
        //    catch 
        //    {

        //        throw;
        //    }
        //    dtNew.TableName = dt.Columns[0].ColumnName;        
        //    sqldata.Tables.Add(dtNew);
        //}
        //sReturn = JsonConvert.SerializeObject(sqldata);
        //return sReturn;
    }

    private string GetTableNameTablateEN(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblEnrolment_Temp";
                break;

            case 1:
                tablename = "tblEnrolmentCV";
                break;

            default:
                tablename = "NoName";
                break;
        }

        return tablename;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp2020(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2020");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2020(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    public string PostUpdateAndroidID(string UserName, string AndroidID)
    {
        string sReturn = string.Empty;

        try
        {
            string condition = "";
            SqlParameter[] para = new SqlParameter[] {

           new SqlParameter("@UserName",UserName),
           new SqlParameter("@AndroidID",AndroidID),

           };



            //try
            //{
            DataTable dttabletdata = new DataTable();
            dttabletdata = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Post_Update_AndroidID", para);
            if (dttabletdata.Rows.Count > 0)
            {

                sReturn = dttabletdata.Rows[0]["TotalCount"].ToString();
            }
            else
            {
                sReturn = "0";
            }


        }
        catch
        {

            sReturn = "9999";
        }

        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_tblQuestion(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dttblQuestion = objComman.CreateDataTable("tblQuestion");
                    dttblQuestion = SetColumnsOrdinal(dsMyData.Tables["tblQuestion"], dttblQuestion);



                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_tblQuestion(dttblQuestion, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblOOSG(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblOOSG");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblOOSG"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblOOSG(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateVerification(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblVerification");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblVerification"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdateVerification(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateVerificationNew(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblVerificationNew");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblVerification"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdateVerificationNew(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateVerificationNew2021(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblVerificationNew2021");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblVerification"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdateVerification2021(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildOOSG(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildOOSG");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildOOSG"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblChildOOSG(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetention(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionNew");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Retention"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrion(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetentionMain(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionMain");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolmentRetentionMain"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrionMmain(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblSafetySecurity(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSafetySecurity");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSafetySecurity"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_SafetySecurity(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildRegistration(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistration");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistration"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildRegistration(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildRegistrationNew(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationNew");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistration"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildRegistrationNew(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildRegistrationNew2021(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationNew2021");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistration"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildRegistrationNew2021(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    //---------------Last method

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildRegistrationNew2021New(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationNew20212022New");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistration"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationNew20212022New(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildRegistrationNew20212022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationNew20212022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistration"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildRegistrationNew20212022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildAttendance(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendance");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendance"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildAttendance(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildAttendance2020(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendance2020");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendance"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildAttendance2020(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildAttendance2021(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendance2021");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendance"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildAttendance2021(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildAttendance20212022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendance20212022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendance"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildAttendance20212022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildAttendance20212022New(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendance20212022New");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendance"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblChildAttendance20212022New(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblVisitors(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblVisitors");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblVisitors"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblVisitors(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblCLLSG(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblCLLSG");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblCLLSG"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InsertblCLLSG(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string IInsertUpdatetblOOSGNew(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblOOSGNew");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblOOSG"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblOOSGNew(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string IInsertUpdatetblOOSGNew2022(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblOOSG2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblOOSG"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblOOSGNew2022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    public string Get_ChildOOSG(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.CheckPasswordNew(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_ChildOOSG", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNametblOOSG(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string Get_SaftySafetySecurity(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.CheckPasswordNew(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }



            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadSafetySecurity", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = "mstSafetySecurity";
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private string GetTableNametblOOSG(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {

            case 0:
                tablename = "tblOOSG";

                break;

            case 1:
                tablename = "tblChildOOSG";
                break;
            case 2:
                tablename = "tblVerification";
                break;
            case 3:
                tablename = "TotalCount";
                break;
            case 4:
                tablename = "tblCLLSG";
                break;
            case 5:
                tablename = "tblDTDMobileActivityVerification";
                break;
            case 6:
                tablename = "tblOOSCNew";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionLoginEntry2020(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate2024(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("Tbl_User_LoginNewLoginInt2020");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["Tbl_User_Login"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_User_LoginNewDateAsInt2020(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Insert_TblCommunitySMC(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("TblCommunitySMC");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["TblCommunitySMC"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_TblCommunitySMC(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Insert_TblSMCAttendance(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("TblSMCAttendance");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["TblSMCAttendance"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_TblSMCAttendance(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Insert_Update_tblLSGChildAttendance(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("tblLCGChildRegistrationNew");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["tblLCGChildRegistration"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_ChildRegistration(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Insert_Update_ChildRegistrationLCGAttendance(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("tblLSGChildAttendance");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["tblLSGChildAttendance"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_ChildRegistrationLCGAttendance(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_Insert_Update_tblInfluencerProfile(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable dtTbl_User_Login = objComman.CreateDataTable("tblInfluencerProfile");
                    dtTbl_User_Login = SetColumnsOrdinal(dsMyData.Tables["mstInfluencerProfile"], dtTbl_User_Login);


                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_tblInfluencerProfile(dtTbl_User_Login, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage2021(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew2021");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village2021(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage2022(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew2022");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village2022(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew2023");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village2023(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationAgp(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationAGP");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateVisitorsAGP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblVisitorsAGP");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblVisitorsAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblVisitorsAGP(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblAttendanceImage(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblAttendanceImage");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblAttendanceImage"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblAttendanceImage(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceAGP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceAGP");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildRegistrationSchool(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationSchool");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationSchool"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationSchool(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblChildAttendanceSchool(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceSchool");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceSchool"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceSchool(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblVisitorsSchool(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblVisitorsSchool");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblVisitorsSchool"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttblVisitorsSchool(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblAttendanceImageSchool(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblAttendanceImageSchool");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblAttendanceImageSchool"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblAttendanceImageSchool(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatettblChildRegistrationBalsabha(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationBalsabha");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationBalsabha"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttbltblChildRegistrationBalsabha(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatettblChildAttendanceLifeskill(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceLifeskill");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceLifeskill"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttbltblChildAttendanceLifeskill(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceAGP2022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceAGP2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP2022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationAgp2022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationAGP2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp2022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationAgp2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationAGP2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceAGP2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceAGP2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceAGP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSMCAttendance(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSMCAttendance2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSMCAttendanceNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_SMCAttendance2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationGKP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationGKP");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationGKP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationGKP(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool20230707(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew20230707");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20230707(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp2022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    public string Get_ChildRegistrationGKP(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_ChildGKP", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameGKp(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private string GetTableNameGKp(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblChildRegistrationGKP";
                break;

            case 1:
                tablename = "tblChildAttendanceGKP";
                break;
            case 2:
                tablename = "tblEnrolmentGKP";
                break;
            case 3:
                tablename = "tblRandomSessionPhoto";
                break;
            case 4:
                tablename = "tblChildRegistrationGyanodaya";
                break;
            case 5:
                tablename = "tblChildAttendanceGyanodaya";
                break;
            case 6:
                tablename = "tblLocationDetails";
                break;
            case 7:
                tablename = "tblSessionWiseDetails";
                break;
            case 8:
                tablename = "tblChildGyanodayaAttendanceGKP";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp2022New(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2022New");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2023New(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetention2022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionNew2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Retention"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrion2022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetentionMain2022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionMain2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolmentRetentionMain"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrionMmain2022(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage202309(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew202309");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village202309(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_Session_tblClusterMeeting(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblClusterMeetingNew");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblClusterMeeting"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_tblClusterMeeting(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool20230908(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew20230908");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20230908(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP2022(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {

                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();

                    DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022(DttblEnrolment_Temp);

                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {

                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();

                    DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022(DttblEnrolment_Temp);

                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP2023StatewiseUP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            string UserID = "";
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticateNew(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToString(dtUser.Rows[0]["Statecode"].ToString());
                }
            }
            if (UserID.Length > 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();
                    if (UserID == "23")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022MP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022MP(DttblEnrolment_Temp);
                    }
                    else if (UserID == "9")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022UP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022UP(DttblEnrolment_Temp);
                    }
                    else
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022(DttblEnrolment_Temp);
                    }
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP2023StatewiseUPNew(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            string UserID = "";
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticateNew(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToString(dtUser.Rows[0]["Statecode"].ToString());
                }
            }
            if (UserID.Length > 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();
                    if (UserID == "23")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022MPNew");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022MPNew(DttblEnrolment_Temp);
                    }
                    else if (UserID == "9")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022UPNew");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022UPNew(DttblEnrolment_Temp);
                    }
                    else
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2022New");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022New(DttblEnrolment_Temp);
                    }
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool20231001(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew20231001");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20231001(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool20230112(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew20230112");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20230112(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSC(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSC");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSC"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSC(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSCOS(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSCNewCOIS");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSCNew"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSCNew(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage202302(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew20230221");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village20230221(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_Houshold(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblHousholdTemp");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblHoushold"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_HousholdTemp(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_Survey(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblSurveyTemp");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblSurvey"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyTemp(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_VlgHHImage(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblVlgHHImage");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblVlgHHImage"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblVlgHHImage(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    public string Get_D2dSurvey(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_D2dSurvey", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameSurvey(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private string GetTableNameSurvey(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblHoushold";
                break;

            case 1:
                tablename = "tblSurvey";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_Survey2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblSurveyTemp2023");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblSurvey"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyTemp2023(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSC2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;


            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSC2023");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSC"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSC2023(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSCOS2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {


            int UserID = 0;

            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSCNewCOIS2023");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSCNew"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSCNew2023(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp2023New(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2023New");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp20232024New(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationGKP2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationGKP2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationGKP"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationGKP2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp20232007New(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2023New2007");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp202320242007New(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP2024StatewiseUP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            string UserID = "";
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticateNew(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToString(dtUser.Rows[0]["Statecode"].ToString());
                }
            }
            if (UserID.Length > 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();
                    if (UserID == "23")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2023MP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2023MP(DttblEnrolment_Temp);
                    }
                    else if (UserID == "9A" || UserID == "9B" || UserID == "9C")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2023UP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2023UP(DttblEnrolment_Temp);
                    }
                    else
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2023");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP202387(DttblEnrolment_Temp);
                    }
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRandomSessionPhoto(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblRandomSessionPhoto");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblRandomSessionPhoto"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_tblRandomSessionPhoto(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationGKPGyanodaya(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationGyanodaya");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationGyanodaya"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationGKPGyanodaya(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKPGyanodaya(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGyanodaya");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGyanodaya"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGyanodayatemp(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateHousholdExpansion(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblHousholdExpansion");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblHousholdExpansion"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblHousholdExpansion(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpansion(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSurveyExpansion");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSurveyExpansion"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyExpansion(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpOtherVillageDetails(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblExpOtherVillageDetails");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblExpOtherVillageDetails"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetails(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpOtherBalikaAndInfluencer(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblBalikaAndInfluencer");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblBalikaAndInfluencer"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblBalikaAndInfluencer(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyAudioRecording(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblAudioRecording");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblAudioRecording"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblAudioRecording(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    public string UploadAudioFile(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {

            // string stockImagesDir = ConfigurationManager.AppSettings["ImagesPath"].ToString();

            string sDirectory = Server.MapPath(Comman.GetImagePath("AudioFilePath"));

            //string sCNDirectory = sDirectory + "\\" + sUID + "\\";
            //string sFilename = sDirectory + "\\" + sUID + "\\" + fileName;
            byte[] sfilebytes = Convert.FromBase64String(filebytes);

            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            //if (!Directory.Exists(sCNDirectory))
            //    Directory.CreateDirectory(sCNDirectory);

            // instance a filestream pointing to the
            // storage folder, use the original file name
            // to name the resulting file
            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {
                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }


            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }


    [WebMethod]
    public string Get_SurveyExpansion(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_SurveyExpansion", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameSurveyExpansion(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private string GetTableNameSurveyExpansion(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblHousholdExpansion";
                break;

            case 1:
                tablename = "tblSurveyExpansion";
                break;
            case 2:
                tablename = "tblExpOtherVillageDetails";
                break;
            case 3:
                tablename = "tblBalikaAndInfluencer";
                break;
            case 4:
                tablename = "tblAudioRecording";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpOtherVillageDetailsNew(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblExpOtherVillageDetailsNew");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblExpOtherVillageDetails"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetailsNew(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool20232024(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew202324");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School20232024(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpansion2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSurveyExpansion2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSurveyExpansion"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyExpansion2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatettblChildAttendanceLifeskill2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceLifeskill2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceLifeskill"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttbltblChildAttendanceLifeskill2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateHousholdExpansion2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblHousholdExpansion2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblHousholdExpansion"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblHousholdExpansion2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    public string Get_ReportTarget(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);


            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_ReportTarget", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = ReportTarget(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string ReportTarget(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mstMobileReportTarget";
                break;

            case 1:
                tablename = "tblEnrolmentCV";
                break;


            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameTL(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mstMobileReportTarget";
                break;

            case 1:
                tablename = "tblSurveyExpansion";
                break;


            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblEnrollSummary(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrollSummary");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrollSummary"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrollSummary(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblEnrollSummary2023(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrollSummary2023");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrollSummary"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrollSummary2023(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblEnrollSummaryBO(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;

            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrollSummaryBO");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrollSummaryBO"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrollSummaryBO(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetUserLoginAuthenticate(string UserName, string Password, string IMEINo)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            //if (UserName.Trim() != "" && Password.Trim() != "")
            //{
            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.GetUserLoginAuthenticateFC(UserName, checkpass, IMEINo);


            if (dtUser.Rows.Count > 0)
            {
                UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());

                sReturn = "{\"Table\":[{\"RetValue\":1}]}";
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }

            //}
            //else
            //{
            //    sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            //}

        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_Houshold2023(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblHousholdTemp2023");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblHoushold"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_HousholdTemp2023(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_Survey20232024(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblSurveyTemp20232024");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblSurvey"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyTemp20232024(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string Tablet_Post_TblActivityUpdate_Office_BO2024(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("TblActivityUpdate_Office_BO2024");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["TblActivityUpdate_Office_BO"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_TblActivityUpdate_Office_BO2024(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSC2024(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;


            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSC2024");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSC"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSC2024(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSCOS2024(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {


            int UserID = 0;

            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSCNewCOIS2024");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSCNew"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSCNew2024(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    public string Get_ChildRegistrationGKP2023(string UserName, string Password, string IMEINo, string Flag)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                string gg = Zip("0");
                return gg;
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
             new SqlParameter("@Flag",Flag),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_ChildGKP223", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameGKp(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
                sReturn = Zip(sReturn);
            }
            catch
            {
                sReturn = "9999";
                sReturn = Zip(sReturn);
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    public static string Zip(string value)
    {
        string sB = string.Empty;

        using (MemoryStream ms = new MemoryStream())
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(value);

            using (GZipStream sw = new GZipStream(ms, CompressionMode.Compress))
            {
                sw.Write(byteArray, 0, byteArray.Length);
            }

            byteArray = ms.ToArray();

            sB = Convert.ToBase64String(byteArray);
        }

        return sB;
    }


    [WebMethod]
    public string GetMasterDataTabletNew20240725(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                sReturn = "0";
                return sReturn = Zip(sReturn);
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTablet20190725", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNew2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                ///  sReturn = JsonConvert.SerializeObject(sqldata)  
                sReturn = JsonConvert.SerializeObject(sqldata);
                sReturn = Zip(sReturn);

            }
            catch
            {
                sReturn = "9999";
                sReturn = Zip(sReturn);
            }
        }

        catch
        {
            sReturn = "0";
            sReturn = Zip(sReturn);
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblPlanActivity(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;



            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblPlanActivity");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblPlanActivity"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblPlanActivity(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    public string Get_PlanActivity(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);


            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_tblPlanActivity", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = ReportPlanActivity(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string ReportPlanActivity(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblPlanActivity";
                break;
            case 1:
                tablename = "tblDTD";
                break;
            case 2:
                tablename = "tblOOSC";
                break;

            case 3:
                tablename = "tblRetention";
                break;

            case 4:
                tablename = "tblRound4Score";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetention2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionNew2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Retention"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrion2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetentionMain2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionMain2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolmentRetentionMain"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrionMmain2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrolmentModified(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolmentModified");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolmentModified"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_EnrolmentModified(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollment_temp2025(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_Temp2024New");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Temp"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2025(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    public string GetOMRSheet()
    {
        string sReturn = string.Empty;
        try
        {


            //string checkpass = objPass.CreatePasswordHashNew(Password);

            //DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);


            //if (dtUser.Rows.Count > 0)
            //{
            //    //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            //}
            //else
            //{
            //    return "0";
            //}


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",""),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptOCRLSE", para);


                DataSet sqldata = new DataSet("GetOCRSheet");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetOCRSheet(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string GetOCRSheet(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblLSEOMR";
                break;
            case 1:
                tablename = "tblDTD";
                break;
            case 2:
                tablename = "tblOOSC";
                break;



            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    [WebMethod]
    public string Get_tblEnrolment_Temp2024(string UserName)
    {
        DataSet dttabletdata = new DataSet();
        SqlParameter[] para = new SqlParameter[] {
            new SqlParameter("@UserName",UserName),
            };
        string sReturn = string.Empty;
        dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Web_Get_tblEnrolment_Temp2024", para);
        // DataSet sqldata = new DataSet("User");


        DataSet sqldata = new DataSet("MyData");
        int index = 0;

        foreach (DataTable dt in dttabletdata.Tables)
        {
            DataTable dtNew = new DataTable();
            dtNew = dt.Copy();
            dtNew.TableName = GetTableNameTablateEN(index);
            sqldata.Tables.Add(dtNew);
            index++;
        }
        sReturn = JsonConvert.SerializeObject(sqldata);
        return sReturn;

        //foreach (DataTable dt in dttabletdata.Tables)
        //{
        //    DataTable dtNew = new DataTable();  
        //    dtNew = dt.Copy();            
        //    try
        //    {
        //        dtNew.Columns.RemoveAt(0);
        //    }
        //    catch 
        //    {

        //        throw;
        //    }
        //    dtNew.TableName = dt.Columns[0].ColumnName;        
        //    sqldata.Tables.Add(dtNew);
        //}
        //sReturn = JsonConvert.SerializeObject(sqldata);
        //return sReturn;
    }

    [WebMethod]
    public string Get_Masters_TotalTarget2024(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_TotalTarget2024", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNametblContactTarget(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }


    [WebMethod]
    public string GetMasterDataTabletVillageWise201906262024(string UserName, string Password, string Villagecode)
    {
        string sReturn = string.Empty;
        try
        {


            DataTable dtUser = objComman.GetUserAuthenticate(UserName, Password);

            if (dtUser.Rows.Count > 0)
            {
                // UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }





            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),
              new SqlParameter("@Villagecode",Villagecode),

            };



            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTabletVillageWise201906262024", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameTablateNewVillagewise2019(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    [WebMethod]
    public string UploadImageBalsaba(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {

            // string stockImagesDir = ConfigurationManager.AppSettings["ImagesPath"].ToString();

            string sDirectory = Server.MapPath(Comman.GetImagePath("LSEPath"));

            //string sCNDirectory = sDirectory + "\\" + sUID + "\\";
            //string sFilename = sDirectory + "\\" + sUID + "\\" + fileName;
            byte[] sfilebytes = Convert.FromBase64String(filebytes);

            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            //if (!Directory.Exists(sCNDirectory))
            //    Directory.CreateDirectory(sCNDirectory);

            // instance a filestream pointing to the
            // storage folder, use the original file name
            // to name the resulting file
            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {
                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }

            if (!File.Exists(sFilename))
            {
                return "FAIL";
            }
            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatettblChildAttendanceLifeskill2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceLifeskill2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceLifeskill"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_InserttbltblChildAttendanceLifeskill2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblTravelMatrixDeatils(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DtttblTravelMatrixDeatils2024 = objComman.CreateDataTable("tblTravelMatrixDeatils2024");
                    DataTable DtttblTravelMatrixExpens = objComman.CreateDataTable("tblTravelMatrixExpens");
                    DataTable DtttblTravelMatrixPerDiem = objComman.CreateDataTable("tblTravelMatrixPerDiem");
                    DataTable dtTravelConsent = objComman.CreateDataTable("tblTravelConsent");

                    DtttblTravelMatrixDeatils2024 = SetColumnsOrdinal(dsMyData.Tables["tblTravelMatrixDeatils2024"], DtttblTravelMatrixDeatils2024);
                    DtttblTravelMatrixExpens = SetColumnsOrdinal(dsMyData.Tables["tblTravelMatrixExpens"], DtttblTravelMatrixExpens);
                    DtttblTravelMatrixPerDiem = SetColumnsOrdinal(dsMyData.Tables["tblTravelMatrixPerDiem"], DtttblTravelMatrixPerDiem);
                    dtTravelConsent = SetColumnsOrdinal(dsMyData.Tables["tblTravelConsent"], dtTravelConsent);
                    DataSet dsResult = new DataSet();
                    //if (DtttblTravelMatrixExpens.Rows.Count > 0 && DtttblTravelMatrixPerDiem != null && DtttblTravelMatrixPerDiem != null)
                    //{
                    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024(DtttblTravelMatrixDeatils2024, DtttblTravelMatrixExpens, DtttblTravelMatrixPerDiem, dtTravelConsent);
                    //}
                    //else  if (DtttblTravelMatrixExpens !=null  )
                    //{
                    //    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024withEX(DtttblTravelMatrixDeatils2024, DtttblTravelMatrixExpens);

                    //}
                    //else if (DtttblTravelMatrixPerDiem !=null)
                    //{
                    //    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024Pendim(DtttblTravelMatrixDeatils2024, DtttblTravelMatrixPerDiem);

                    //}
                    //else
                    //{
                    //    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024without(DtttblTravelMatrixDeatils2024);

                    //}
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblTravelMatrixDeatils2026(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DtttblTravelMatrixDeatils2024 = objComman.CreateDataTable("tblTravelMatrixDeatils2025");
                    DataTable DtttblTravelMatrixExpens = objComman.CreateDataTable("tblTravelMatrixExpens");
                    DataTable DtttblTravelMatrixPerDiem = objComman.CreateDataTable("tblTravelMatrixPerDiem");
                    DataTable dtTravelConsent = objComman.CreateDataTable("tblTravelConsent");

                    DtttblTravelMatrixDeatils2024 = SetColumnsOrdinal(dsMyData.Tables["tblTravelMatrixDeatils2024"], DtttblTravelMatrixDeatils2024);
                    DtttblTravelMatrixExpens = SetColumnsOrdinal(dsMyData.Tables["tblTravelMatrixExpens"], DtttblTravelMatrixExpens);
                    DtttblTravelMatrixPerDiem = SetColumnsOrdinal(dsMyData.Tables["tblTravelMatrixPerDiem"], DtttblTravelMatrixPerDiem);
                    dtTravelConsent = SetColumnsOrdinal(dsMyData.Tables["tblTravelConsent"], dtTravelConsent);
                    DataSet dsResult = new DataSet();
                    //if (DtttblTravelMatrixExpens.Rows.Count > 0 && DtttblTravelMatrixPerDiem != null && DtttblTravelMatrixPerDiem != null)
                    //{
                    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2026(DtttblTravelMatrixDeatils2024, DtttblTravelMatrixExpens, DtttblTravelMatrixPerDiem, dtTravelConsent);
                    //}
                    //else  if (DtttblTravelMatrixExpens !=null  )
                    //{
                    //    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024withEX(DtttblTravelMatrixDeatils2024, DtttblTravelMatrixExpens);

                    //}
                    //else if (DtttblTravelMatrixPerDiem !=null)
                    //{
                    //    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024Pendim(DtttblTravelMatrixDeatils2024, DtttblTravelMatrixPerDiem);

                    //}
                    //else
                    //{
                    //    dsResult = objComman.tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024without(DtttblTravelMatrixDeatils2024);

                    //}
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    public string UploadTravelMatrix(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {



            string sDirectory = Server.MapPath(Comman.GetImagePath("TravelPath"));


            byte[] sfilebytes = Convert.FromBase64String(filebytes);


            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {

                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }

            if (!File.Exists(sFilename))
            {
                return "FAIL";
            }
            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }


    [WebMethod]
    public string Get_TravelMatrixMaster(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_dataTravelMatrix", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameMatrixMaster(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    [WebMethod]
    public string Get_TravelMatrix(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_TravelMatrix", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameMatrix(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string GetTableNameMatrixMaster(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "mstBlockTravel";
                break;

            case 1:
                tablename = "mstClusterTravel";
                break;
            case 2:
                tablename = "mstVillageTravel";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }
    private string GetTableNameMatrix(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblTravelMatrixDeatils2024";
                break;

            case 1:
                tablename = "tblTravelMatrixExpens";
                break;
            case 2:
                tablename = "tblTravelMatrixPerDiem";
                break;

            case 3:
                tablename = "tblDTDMobileActivity";
                break;

            case 4:
                tablename = "tblEnrolment";
                break;

            case 5:
                tablename = "tblChildRegistration";
                break;

            case 6:
                tablename = "tblChildAttendance";
                break;
            case 7:
                tablename = "tblActivityUpdate_School";
                break;
            case 8:
                tablename = "tblActivityUpdate_Village";
                break;
            case 9:
                tablename = "tblOOSC";
                break;
            case 10:
                tablename = "tblOOSC";
                break;
            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildRegistrationGKPPlus(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationGKPPlus");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationGKPPlus"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationGKPLus(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChilddAttendanceGGKPPlus(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            string UserID = "";

            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticateNew(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToString(dtUser.Rows[0]["Statecode"].ToString());
                }
            }
            if (UserID.Length > 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();
                    if (UserID == "23")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKPPlusMP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKPPlus"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusMP(DttblEnrolment_Temp);
                    }
                    else if (UserID == "9A" || UserID == "9B" || UserID == "9C")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKPPlusUP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKPPlus"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusUP(DttblEnrolment_Temp);
                    }
                    else
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKPPlusRaj");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKPPlus"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusRaj(DttblEnrolment_Temp);
                    }
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    public string Get_ChildRegistrationGKPPlus(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                string gg = Zip("0");
                return gg;
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),


            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_ChildGKPPlus", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameGKpPlus(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
                sReturn = Zip(sReturn);
            }
            catch
            {
                sReturn = "9999";
                sReturn = Zip(sReturn);
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }
    private string GetTableNameGKpPlus(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblChildRegistrationGKPPlus";
                break;

            case 1:
                tablename = "tblChildAttendanceGKPPlus";
                break;


            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    [WebMethod]
    public string Get_TravelMatrixDowlond(Int32 mmoth, Int32 mYear, string FCName, string FromNo)
    {
        string sReturn = string.Empty;
        try
        {


            //string checkpass = objPass.CreatePasswordHashNew(Password);

            //DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            //if (dtUser.Rows.Count > 0)
            //{
            //    //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            //}
            //else
            //{
            //    return "0";
            //}


            //SqlParameter[] para = new SqlParameter[] {

            //new SqlParameter("@UserName",UserName),

            //};

            try
            {
                //DataSet dttabletdata = new DataSet();

                //dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Masters_TotalTarget2024", para);


                //DataSet sqldata = new DataSet("MyData");
                //int index = 0;

                //foreach (DataTable dt in dttabletdata.Tables)
                //{
                //    DataTable dtNew = new DataTable();
                //    dtNew = dt.Copy();
                //    dtNew.TableName = GetTableNametblContactTarget(index);
                //    sqldata.Tables.Add(dtNew);
                //    index++;
                //}
                string sdd = GeneraatePDFMainTest2(mmoth, mYear, FCName, FromNo);
                sReturn = JsonConvert.SerializeObject(sdd);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    protected string GeneraatePDFMainTest2(Int32 mmoth, Int32 mYear, string FCName, string FromNo)
    {
        string sb = "";
        string filename = "";
        try
        {

            int FYear = mYear;


            string empname = "", empcode = "", designation = "", district = "", Block = "", cluster = "", depatment = "", Reporting = "";
            //  DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils2024AuditTrail on  MstUser.UserName=tblTravelMatrixDeatils2024AuditTrail.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode    inner join MstUser u on u.blockcode=MstUser.blockcode and u.UserLevel=19 and U.ActiveStatus=1", "distinct mstuser.FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,'' Department ,mst2District.districtname,BlockName,VillageName as cluster,U.userName +'-'+ u.FristName  as [Reporting Manager]", "MstUser.UserName='" + ddlFC.SelectedValue + "' and mYear=" + mYear + " and mMonth=" + Convert.ToInt32(ddlMonth.SelectedValue) + "", "", "");
            //    DataTable dtemployee = objMain.Select_All_Data("MstUser inner join tblTravelMatrixDeatils2024 on  MstUser.UserName=tblTravelMatrixDeatils2024.UserID inner join  Mstuserrole on  MstUser.UserLevel= Mstuserrole.Role_Level inner join mst2District on mst2District.districtcode=MstUser.DistrictCode inner join mst3Block on mst3Block.blockcode=MstUser.blockcode inner join mst5Village on mst5Village.villagecode=MstUser.villagecode    left join MstUser u on u.blockcode=MstUser.blockcode and u.UserLevel=19 and U.ActiveStatus=1", "distinct mstuser.FristName as name,mstuser.UserName as code,Mstuserrole.Role as desg,'' Department ,mst2District.districtname,BlockName,VillageName as cluster,U.userName +'-'+ u.FristName  as [Reporting Manager]", "MstUser.UserName='" + FCName + "' and mYear=" + FYear + " and mMonth=" + mmoth + "", "", "");
            SqlParameter[] parm2 = new SqlParameter[]
             {

             new SqlParameter("@UserName", FCName),
             new SqlParameter("@mMonth", mmoth),
              new SqlParameter("@mYear",FYear),


             };


            DataTable dtemployee = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEMpDetailsTravel", parm2);

            if (dtemployee.Rows.Count > 0)
            {

                empname = dtemployee.Rows[0]["name"].ToString();
                empcode = dtemployee.Rows[0]["code"].ToString();
                designation = dtemployee.Rows[0]["desg"].ToString();
                district = dtemployee.Rows[0]["districtname"].ToString();
                Block = dtemployee.Rows[0]["BlockName"].ToString();
                cluster = dtemployee.Rows[0]["cluster"].ToString();
                depatment = dtemployee.Rows[0]["Department"].ToString();
                Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            }


            string imageURLLogo = Server.MapPath(".") + "/images/logo-new1.png";

            sb += @"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">";
            sb += "<html>";
            sb += "<body>";
            // sb += "<table width='100%' cellspacing='0' cellpadding='2'>";
            // Session["FromNo"] = "nov_1";
            SqlParameter[] parm1 = new SqlParameter[]
     {

             new SqlParameter("@UserName",FCName),
             new SqlParameter("@month", mmoth),
              new SqlParameter("@Myear",FYear),
                 new SqlParameter("@UserRole","1"),
         new SqlParameter("@FromNo",FromNo),



     };


            DataSet dstravle = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptTravelDeatil2024View", parm1);

            DataTable dttravelmatrixdetails = dstravle.Tables[0];
            DataTable dttraveDate = dstravle.Tables[4];
            DataTable dttravex = dstravle.Tables[1];
            DataTable dttravexIMg = dstravle.Tables[2];

            // DataTable dttravelmatrixdetails = objMain.Select_All_Data("tblTravelMatrixDeatils2024", "convert(varchar,TravelDate,103) as Fromdate,convert(varchar,TravelDate,103) as Todate,LoginTime as TimeIn,logouttime as Timeout, [FromVillagename] as [FromVillagename],[ToVillagename] ,isnull(RevisedFare,0) as LC,isnull(RevisedDAAdmin,0) as DA", "userid='" + ddlFC.SelectedValue + "' and mYear='" + ddlYear.SelectedValue + "' and deleteflag=1  ", "TravelDate", "ASC";
            int Acount = dttravelmatrixdetails.Rows.Count;
            int MainCount = 0;
            int icount = 0;
            if (Acount > 12)
            {
                MainCount = 12;

            }
            else
            {
                MainCount = Acount;

            }
            int tot = 0;
            int DA = 0;
            //if (pageindex <= 15)
            //{


            //sb += "<tr style='font-size:20px;'>";
            //sb += "<td style='font-size:20px;text-align:center'>";


            empname = dtemployee.Rows[0]["name"].ToString();
            empcode = dtemployee.Rows[0]["code"].ToString();
            designation = dtemployee.Rows[0]["desg"].ToString();
            district = dtemployee.Rows[0]["districtname"].ToString();
            Block = dtemployee.Rows[0]["BlockName"].ToString();
            cluster = dtemployee.Rows[0]["cluster"].ToString();
            depatment = dtemployee.Rows[0]["Department"].ToString();
            Reporting = dtemployee.Rows[0]["Reporting Manager"].ToString();

            DataTable sqldtTourPlan = new DataTable();
            if (Acount > 12)
            {
                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; font-family:Calibri (Body)' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px;font-family:Calibri (Body)'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code:<b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation:<b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004<b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                              "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                              " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                              " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                          " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                           "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                           "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                           " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                           "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                             " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                              " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                              " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                              "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                                "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                              " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                              "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                              "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                              "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";


                for (int i = 0; i < MainCount; i++)
                {

                    sb += "<tr  style='border: 0'>";
                    sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                    sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                    sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                    sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                    sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                    sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                    //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                    sb += "</tr>";

                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];
                }
                if (Acount > 12)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 12)
                {
                    int Ig = 0;
                    if (Acount > 24)
                    {
                        Ig = 24;
                    }
                    else
                    {
                        Ig = Acount - 12;
                        Ig = 12 + Ig;
                    }

                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                                  "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                                  " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                                  " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                              " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                               "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                               "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                               " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                               "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                                 " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                                  " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                                  " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                                  "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                                    "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                                  " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                                  "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                                  "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                                  "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";



                    for (int i = 12; i < Ig; i++)
                    {


                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);


                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }
                if (Acount > 24)
                {
                    sb += " </tbody> </table>";
                }
                if (Acount > 24)
                {
                    int Ig = 0;

                    if (Acount > 36)
                    {
                        Ig = 36;
                    }
                    else
                    {
                        Ig = Acount - 24;
                        Ig = 24 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 24; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 36)
                {
                    sb += " </tbody> </table>";
                }
                if (Acount > 36)
                {
                    int Ig = 0;

                    if (Acount > 48)
                    {
                        Ig = 48;
                    }
                    else
                    {
                        Ig = Acount - 48;
                        Ig = 48 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 36; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 48)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 48)
                {
                    int Ig = 0;

                    if (Acount > 60)
                    {
                        Ig = 60;
                    }
                    else
                    {
                        Ig = Acount - 60;
                        Ig = 60 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 48; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }



                if (Acount > 60)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 60)
                {
                    int Ig = 0;

                    if (Acount > 72)
                    {
                        Ig = 72;
                    }
                    else
                    {
                        Ig = Acount - 72;
                        Ig = 72 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 60; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 72)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 72)
                {
                    int Ig = 0;

                    if (Acount > 84)
                    {
                        Ig = 84;
                    }
                    else
                    {
                        Ig = Acount - 84;
                        Ig = 84 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 72; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 84)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 84)
                {
                    int Ig = 0;

                    if (Acount > 96)
                    {
                        Ig = 96;
                    }
                    else
                    {
                        Ig = Acount - 96;
                        Ig = 96 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 84; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                if (Acount > 96)
                {
                    sb += " </tbody> </table>";
                }

                if (Acount > 96)
                {
                    int Ig = 0;

                    if (Acount > 108)
                    {
                        Ig = 108;
                    }
                    else
                    {
                        Ig = Acount - 108;
                        Ig = 108 + Ig;
                    }


                    sb += "<div style='page-break-before : always;'> </div>";
                    sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                    sb += " <tr style='background: #fff2cc;'>";
                    sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                    sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Employee Code : <b>" + empcode + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                    sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager : <b>" + Reporting + "</b> </td>  ";
                    sb += " < td style='padding-bottom: 15px'> District : <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                    sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                    sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                    sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                    sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                    sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                    sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                          "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                          " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                          " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
                      " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                       "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                       "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                       " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                       "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                         " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                          " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                          "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                            "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                          " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                          "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                          "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";

                    for (int i = 96; i < Ig; i++)
                    {

                        sb += "<tr  style='border: 0'>";
                        sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                        sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                        sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                        sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                        sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                        sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                        sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                        sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                        //Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                        sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                        sb += "</tr>";
                        tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                        //DA = DA + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["DA"];



                    }

                }

                sb += "<tr style='border: 0'> <td colspan='18' style=' border-bottom: 1px solid #000000; text-align: right; padding-right: 15px; padding: 9px; font-weight: 900; font-size: 14px; border-left:1px solid #000; ' > TOTAL REIMBURSEMENT </td> <td style='border-bottom: 1px solid #000000; text-align:right ;border-right:1px solid #000;'>" + tot + "</td> </tr> </tbody> </table> ";

            }
            else
            {
                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table> </td> </tr>";
                sb += " < tr style='background: #ececec; font-size: 10px; border: 0'> " +
                   "<th style='width:6% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date From</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time In</th>" +
                   " <th style='width:6%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Date To</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Time Out</th>" +
                   " <th style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center; '>Travelling from</th>" +
               " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travelling to</th> " +
                "<th style='width:10%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Purpose of Visit</th> " +
                "<th style='width:4% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Within Cluster</th>" +
                " <th style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>KM Outside Cluster</th> " +
                "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:center;'>Place of Accomm odation</th>" +
                  " < th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Payment Type</th>" +
                   " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation Occupancy</th>" +
                   " <th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Mode of Travel</th> " +
                   "<th style='width:5%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Travel Expenses </th>" +
                     "<th  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Per Diem</th> " +
                   " <th style='width:5% ;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Accommodation</th>" +

                   "<th style='width:5% ;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Local Conveyance</th> " +
                   "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;'>Others</th> " +
                   "<th style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000; text-align:center;'>Total Travel Payable</th> </tr> ";


                for (int i = 0; i < dttravelmatrixdetails.Rows.Count; i++)
                {

                    sb += "<tr  style='border: 0'>";
                    sb += "<td style='width:6%;border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Fromdate"] + "</td>";
                    sb += "<td style='width:5%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TimeIn"] + "</td>";
                    sb += "<td style='width:6%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Todate"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Timeout"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["FromVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["ToVillagename"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Objective"] + "</td>";
                    sb += "<td style='width:4%; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right; '>" + dttravelmatrixdetails.Rows[i]["ClusterKM"] + "</td>";
                    sb += "<td style='width:4%;  border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:right;'>" + dttravelmatrixdetails.Rows[i]["ClusteroutKM"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseType"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["PaymentType"] + "</td>";
                    sb += "<td style='width:10% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Occupancy"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TravelMode"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalAmountAdmin"] + "</td>";
                    sb += "<td style='width:5%; text-align:right ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Perdim"] + "</td>";
                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["GuestHouseRent"] + "</td>";
                    sb += "<td style='width:5% ; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;text-align:right'>" + dttravelmatrixdetails.Rows[i]["Totalvehicle"] + "</td>";

                    Int32 DATA = Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["TotalExpensBO"] + "</td>";

                    sb += "<td style='width:5%; text-align:right; border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000;border-right:1px solid #000;'>" + dttravelmatrixdetails.Rows[i]["Total"] + "</td>";

                    sb += "</tr>";

                    tot = tot + Convert.ToInt32(dttravelmatrixdetails.Rows[i]["Total"]);


                }


                sb += "<tr style='border: 0'> <td colspan='18' style=' border-bottom: 1px solid #000000; text-align: right; padding-right: 15px; padding: 9px; font-weight: 900; font-size: 14px; border-left:1px solid #000; ' > TOTAL REIMBURSEMENT </td> <td style='border-bottom: 1px solid #000000; text-align:right ;border-right:1px solid #000;'>" + tot + "</td> </tr> </tbody> </table> ";

            }

            sb += "<div style='page-break-before : always;'> </div>";
            sb += "<table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody> <tr> <td colspan='1' style='border:1px solid #000; border-bottom:0; border-right:0; '></td> <td colspan='4' style=' font-size: 26px; text-align: center; font-weight: 900; padding: 15px; border:0; border-top:1px solid #000;' > Foundation to Educate Girls Globally </td> <td colspan='1' style=' text-align: right; border:1px solid #000; border-left: 0; border-bottom:0; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt='' /> </td> </tr>";

            sb += "<tr style='background: #fff2cc; border: 0'> <td colspan='6' style='padding: 15px; border:1px solid #000;'>";
            sb += " <table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' >";
            sb += " <tbody> <tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td> ";
            sb += "<td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table> ";
            sb += "<table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody> <tr style='font-size: 11px'> ";
            sb += "<td style='font-size: 11px'>Block: <b>" + Block + "</b></td> <td>Cluster: <b>" + cluster + " </b></td>";
            sb += " <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b></td> ";
            sb += "<td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
            sb += " <td>Form No: <b>" + FromNo + "</b></td> </tr> </tbody> </table> </td> </tr>";
            sb += " <tr> <th colspan='6' style=' font-weight: 900; font-size: 18px; padding: 9px; text-align: center; border:1px solid #000; ' > Other Expens </th> </tr>";
            sb += " <tr style='font-size: 10px; 'background: #ececec;'>  ";
            sb += "<th  width='9%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Date</th><th width='10%'  style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Local Travel in KM</th> <th  width='30%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Description</th>";
            sb += " <th  width='10%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Conveyance</th> <th  width='10%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;  padding-top:15px; padding-bottom:15px;'>Others</th> ";
            sb += "<th  width='15%' style='border-collapse: collapse; border-left:1px solid #000; border-bottom:1px solid #000; text-align:center;border-right:1px solid #000;  padding-top:15px; padding-bottom:15px;'>Remark</th> </tr> ";

            if (dttravex.Rows.Count > 0)
            {


                for (int i = 0; i < dttravex.Rows.Count; i++)
                {

                    sb += "<tr style='font-size:11px'>";

                    sb += "<td width='14%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravex.Rows[i]["Date"] + "</td>";

                    sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["KM"] + "</td>";
                    sb += "<td width='20%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'>" + dttravex.Rows[i]["Desc"] + "</td>";
                    sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["Conveyance"] + "</td>";
                    sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;text-align:right;'>" + dttravex.Rows[i]["Other"] + "</td>";
                    //sb+="<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>";
                    sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000; border-right:1px solid #000;'>" + dttravex.Rows[i]["Remark"] + "</td>";
                    //sb+="<td width='14%' valign='top'></td>";


                    sb += "</tr>";


                }


            }
            else
            {
                sb += "<tr style='font-size:11px'>";

                sb += "<td width='14%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";

                sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='20%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                sb += "<td width='10%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000;'></td>";
                //sb+="<td width='14%' valign='top'>" + dttravelmatrixdetails.Rows[i]["DA"] + "</td>";
                sb += "<td width='15%' valign='top' style='border-left:1px solid #000; border-bottom:1px solid #000; border-right:1px solid #000;'></td>";
                //sb+="<td width='14%' valign='top'></td>";


                sb += "</tr>";

            }
            DataTable dttravelApprove = dstravle.Tables[3];
            sb += "</tbody> </table>";
            sb += "< table style='width: 100%; border-spacing: 0; border-collapse: 0; margin-bottom: 15px; border: 0; font-size: 11px; ' border='1' > ";

            sb += "<tr>";
            //sb += "<tr> <td>01/11/2024</td> <td>01/11/2024</td> <td>01/11/2024</td> <td>01/11/2024</td> ";
            //sb += "<td>01/11/2024</td> <td>01/11/2024</td> </tr> <tr>";
            sb += " <td colspan='6' style=' text-align: center; background: #ececec; font-weight: 900; font-size: 18px; padding: 9px; border:0 ; border-bottom:1px solid #000;' > Approval Status </td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;' >Submission: " + dttravelApprove.Rows[0]["SubmittedStatus"].ToString() + " </td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Submitted By: " + dttravelApprove.Rows[0]["SubmittedBy"].ToString() + "  </td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Submitted Date: " + dttravelApprove.Rows[0]["SubmittedDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>BO Approval: " + dttravelApprove.Rows[0]["BOApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approved By: " + dttravelApprove.Rows[0]["BOApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approval Date: " + dttravelApprove.Rows[0]["BOApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Admin Verification: " + dttravelApprove.Rows[0]["AdminApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified By: " + dttravelApprove.Rows[0]["AdminApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified Date: " + dttravelApprove.Rows[0]["AdminApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>HR Verification: " + dttravelApprove.Rows[0]["HRApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified By: " + dttravelApprove.Rows[0]["HRApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Verified Date: " + dttravelApprove.Rows[0]["HRApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>DOL Approval: " + dttravelApprove.Rows[0]["DOLApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approved By: " + dttravelApprove.Rows[0]["DOLApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Approval Date: " + dttravelApprove.Rows[0]["DOLApprovalDate"].ToString() + "</td> </tr> ";
            sb += "<tr> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Status: " + dttravelApprove.Rows[0]["FinanceApprovalStatus"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Processed by: " + dttravelApprove.Rows[0]["FinanceApprovalBy"].ToString() + "</td> <td colspan='2'  style='vertical-align: bottom;border:solid black 0px;'>Payment Process Date:" + dttravelApprove.Rows[0]["FinanceApprovalDate"].ToString() + "</td> </tr> ";

            sb += " </table>";

            if (dttravexIMg.Rows.Count > 0)
            {
                sb += "<div style='page-break-before : always;'> </div>";

                sb += "  <table style='width: 100%; margin-bottom: 15px; border: 0; font-size: 11px; border-collapse: collapse;'; cellspacing='0'> <tbody style='border: 0'> <tr style='border: 0'> <td colspan='2' style='border-right: 0; border-left:1px solid #000; border-top:1px solid;'></td> <td colspan='15' style=' font-size: 26px; text-align: center; font-weight: 900; border-left: 0; border-top:1px solid #000; padding: 15px; border-right: 0; ' > Foundation to Educate Girls Globally </td> <td colspan='2' style=' text-align: right; border-left: 0; border-right:1px solid #000; border-top:1px solid #000; ' > <img width='50%' height='40%' src='" + imageURLLogo + "'  alt=''  /> </td> </tr> ";
                sb += " <tr style='background: #fff2cc;'>";
                sb += " < td colspan='19' style='padding: 15px; border:1px solid #000;'> <table border='1' style=' border-spacing: 0; border-collapse: 0; width: 100%;' > <tbody>";
                sb += "  < tr style='font-size: 11px'> <td style='padding-bottom: 15px'> Employee Name: <b>" + empname + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Employee Code: <b>" + empcode + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> Designation: <b>Field Coordinator</b>  ";
                sb += " </td> <td style='padding-bottom: 15px'> Reporting Manager: <b>" + Reporting + "</b> </td>  ";
                sb += " < td style='padding-bottom: 15px'> District: <b>" + district + "</b> </td> </tr> </tbody> </table>  ";
                sb += " < table border='0' style=' border-spacing: 0; border-collapse: 0; border: 0; width: 100%; ' > <tbody>  ";
                sb += " < tr style='font-size: 11px'> <td style='font-size: 11px'>Block: <b>" + Block + "</b></td>";
                sb += " < td>Cluster: <b>" + cluster + " </b></td> <td>Department: <b>Operations</b></td> <td>Department Code:130004 <b></b>";
                sb += " </td> <td>Work Level: <b>L8</b></td> <td>Settlement Period: <b>" + dttraveDate.Rows[0]["Fdate"].ToString() + " TO " + dttraveDate.Rows[0]["Tdate"].ToString() + "</b></td>";
                sb += " < td>Form No: <b>" + FromNo + " </b></td> </tr> </tbody> </table></td> </tr>";


                sb += "</tbody> </table>";

                sb += "<table border=1 width='100%' cellspacing='2' cellpadding='2' style='font-size:10px;page-break-after: always; border-color:#dddddd;font-weight:normal'> ";
                int kcount = 0;
                for (int i = 0; i < dttravexIMg.Rows.Count; i++)
                {
                    string Imh = dttravexIMg.Rows[i]["ImagePath"].ToString();
                    string imageURLLogo1 = Server.MapPath(".") + "/Travel/" + Imh;
                    if (System.IO.File.Exists(imageURLLogo1))
                    {
                        kcount = kcount + 1;
                        sb += "<tr>";

                        sb += "<td valign='top'><img      src='" + imageURLLogo1 + "'  height='600px' width='960px'  alt='Bird' /></td>";
                        //sb+="<td width='14%' valign='top'>dfgfdg</td>";

                        sb += "</tr>";
                    }
                }
                if (kcount == 0)
                {
                    sb += "<tr>";

                    sb += "<td valign='top'></td>";

                    sb += "</tr>";
                }
                sb += "</table>";
            }


            StringReader sr = new StringReader(sb.ToString());
            // Document pdfDoc = new Document(PageSize.A2, 70f, 70f, 20f, 10f);
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
            // Document pdfDoc = new Document(PageSize.A4, 36, 36, 36, 72;
            iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
            string FC = FCName;
            //var cssText = File.ReadAllText(MapPath("~/StyleSheet.css");

            string sDirectory = Server.MapPath(Comman.GetImagePath("TravelvouchersPath"));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memoryStream);

                pdfDoc.Open();
                pdfDoc.NewPage();

                using (TextReader reader = new StringReader(sb))
                {
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                }

                pdfDoc.Close();
                byte[] bytes = memoryStream.ToArray();


                memoryStream.Close();

                File.WriteAllBytes(sDirectory + "/TravelVoucher" + mmoth + "" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf", bytes);
            }
            filename = "Travelvoucher" + "" + mmoth + "" + FC.Substring(0, 7) + DateTime.Now.ToString("ddMMyyyhhmmss") + ".pdf";









        }
        catch (System.Exception ex)
        {

            //   Response.Clear(;

            //string mmsg = ex.Message;
            //showEXPMessages("(crateZip)  " + mmsg; //showMessages(mmsg;
        }
        finally
        {

            //Response.Clear(;

        }

        return filename;

    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpOtherVillageDetails2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblExpOtherVillageDetails2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblExpOtherVillageDetailsNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetails2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpOtherBalikaAndInfluencer2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblBalikaAndInfluencer2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblBalikaAndInfluencerNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblBalikaAndInfluencer2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateHousholdExpansion2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblHousholdExpansion2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblHousholdExpansionNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblHousholdExpansion2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpansion2024(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSurveyExpansion2024");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSurveyExpansionNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyExpansion2024(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyExpansion2025(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSurveyExpansion2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSurveyExpansionNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyExpansion2025(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    public string Get_SurveyExpansion2024(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewFC(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_SurveyExpansion2024", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameSurveyExpansion2024(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private string GetTableNameSurveyExpansion2024(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "tblHousholdExpansionNew";
                break;

            case 1:
                tablename = "tblSurveyExpansionNew";
                break;
            case 2:
                tablename = "tblExpOtherVillageDetailsNew";
                break;
            case 3:
                tablename = "tblBalikaAndInfluencerNew";
                break;
            case 4:
                tablename = "tblFemaleDetails";
                break;

            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_SurveyMaitri(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblSurveyTempMaitri");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblSurvey"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblSurveyTempMaitri(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatePanchayatMeeting(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblPanchayatMeetingTempType");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblPanchayatMeeting"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblPanchayatMeetingTem(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateRatriChaupal(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblRatriChaupalTemp");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblRatriChaupal"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblRatriChaupalTemp(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateEnrollmentRally(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrollmentRallytempType");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrollmentRally"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrollmentRallytempType(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdateVillage20252026(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_Village = objComman.CreateDataTable("tblActivityUpdate_VillageNew20252026");
                    DttblActivityUpdate_Village = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_Village"], DttblActivityUpdate_Village);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_Village20252026(DttblActivityUpdate_Village, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblPlanActivity2025(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;



            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblPlanActivity2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblPlanActivity"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblPlanActivity2025(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatettblChildAttendanceLifeskill2024KGBV(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceLifeskill2024KGBV");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceLifeskillKGBV"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceLifeskill2024KGBV(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatettblChildRegistrationBalsabhaKGBV(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblChildRegistrationBalsabhaKGBV");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildRegistrationBalsabhaKGBV"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildRegistrationBalsabhaKGBV(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildAttendanceGKP2025StatewiseUP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            string UserID = "";
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticateNew(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToString(dtUser.Rows[0]["Statecode"].ToString());
                }
            }
            if (UserID.Length > 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();
                    if (UserID == "23")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2025MP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025MP(DttblEnrolment_Temp);
                    }
                    else if (UserID == "9A" || UserID == "9B" || UserID == "9C")
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2025UP");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025UP(DttblEnrolment_Temp);
                    }
                    else
                    {
                        DttblEnrolment_Temp = objComman.CreateDataTable("tblChildAttendanceGKP2025");
                        DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildAttendanceGKP"], DttblEnrolment_Temp);
                        dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025(DttblEnrolment_Temp);
                    }
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblSessionWiseDetail(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                //if (dsMyData.Tables.Count >= 1)
                //{
                DataTable tblSessionWiseDetails = objComman.CreateDataTable("tblSessionWiseDetails");
                DataTable tblLocationDetails = objComman.CreateDataTable("tblLocationDetails");

                tblSessionWiseDetails = SetColumnsOrdinal(dsMyData.Tables["tblSessionWiseDetails"], tblSessionWiseDetails);
                tblLocationDetails = SetColumnsOrdinal(dsMyData.Tables["tblLocationDetails"], tblLocationDetails);

                DataSet dsResult = new DataSet();

                dsResult = objComman.Tablet_Post_Session_Insert_Update_SessionWiseDetails(tblSessionWiseDetails, tblLocationDetails);

                sReturn = JsonConvert.SerializeObject(dsResult);
                //}

                //else
                //{
                //    sReturn = "{\"Table\":[{\"RetValue\":10}]}";
                //}
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    public string UploadImageGKP(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {

            // string stockImagesDir = ConfigurationManager.AppSettings["ImagesPath"].ToString();

            string sDirectory = Server.MapPath(Comman.GetImagePath("GKPPath"));

            //string sCNDirectory = sDirectory + "\\" + sUID + "\\";
            //string sFilename = sDirectory + "\\" + sUID + "\\" + fileName;
            byte[] sfilebytes = Convert.FromBase64String(filebytes);

            // instance a memory stream and pass the
            // byte array to its constructor
            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            //if (!Directory.Exists(sCNDirectory))
            //    Directory.CreateDirectory(sCNDirectory);

            // instance a filestream pointing to the
            // storage folder, use the original file name
            // to name the resulting file
            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {
                // write the memory stream containing the original
                // file as a byte array to the filestream
                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }

            if (!File.Exists(sFilename))
            {
                return "FAIL";
            }
            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSurveyFemaleDetails(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblFemaleDetailstemp2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblFemaleDetails"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblFemaleDetails(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateHousholdExpansion2025(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblHousholdExpansion2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblHousholdExpansionNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblHousholdExpansion2025(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateLoginReason(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblLoginReason2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblLoginReason"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblLoginReason2025(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSMCAttendance2025(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSMCAttendance2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSMCAttendanceNew"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_SMCAttendance2025(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionActivityUpdatSchool2025(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblActivityUpdate_SchoolNew2025");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblActivityUpdate_School"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_ActivityUpdate_School2025(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateSMCAttendanceChiild(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblSMCAttendanceChild2025");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblSMCAttendanceChild"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_SMCAttendance2025Child(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblSessionWiseDetail2025(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                //if (dsMyData.Tables.Count >= 1)
                //{
                DataTable tblSessionWiseDetails = objComman.CreateDataTable("tblSessionWiseDetails2025");
                DataTable tblLocationDetails = objComman.CreateDataTable("tblLocationDetails");

                tblSessionWiseDetails = SetColumnsOrdinal(dsMyData.Tables["tblSessionWiseDetails"], tblSessionWiseDetails);
                tblLocationDetails = SetColumnsOrdinal(dsMyData.Tables["tblLocationDetails"], tblLocationDetails);

                DataSet dsResult = new DataSet();

                dsResult = objComman.Tablet_Post_Session_Insert_Update_SessionWiseDetails2025(tblSessionWiseDetails, tblLocationDetails);

                sReturn = JsonConvert.SerializeObject(dsResult);
                //}

                //else
                //{
                //    sReturn = "{\"Table\":[{\"RetValue\":10}]}";
                //}
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateChildGyanodayAttendanceGKP(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            string UserID = "";
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticateNew(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToString(dtUser.Rows[0]["Statecode"].ToString());
                }
            }
            if (UserID.Length > 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                DataTable DttblEnrolment_Temp = null;
                if (dsMyData.Tables.Count >= 1)
                {

                    DataSet dsResult = new DataSet();

                    DttblEnrolment_Temp = objComman.CreateDataTable("tblChildGyanodayaAttendanceGKPTemp");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblChildGyanodayaAttendanceGKP"], DttblEnrolment_Temp);
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblChildGyanodayaAttendanceGKP2025(DttblEnrolment_Temp);

                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSC2026(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {

            DataSet dtExportData = new DataSet();
            int UserID = 0;


            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSC2026");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSC"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSC2026(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string TabletPostSessionTablet_Post_Session_Insert_Update_tblOOSCOS2026(string sData, string UserName, string Pass)
    {

        string sReturn = string.Empty;
        try
        {


            int UserID = 0;

            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblActivityUpdate_School = objComman.CreateDataTable("tblOOSCNewCOIS2026");
                    DttblActivityUpdate_School = SetColumnsOrdinal(dsMyData.Tables["tblOOSCNew"], DttblActivityUpdate_School);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblOOSCNew2026(DttblActivityUpdate_School, UserID, sData);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblEnrollSummary2026(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrollSummary2026");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrollSummary"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblEnrollSummary2026(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblPlanActivity2026(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;



            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);

                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblPlanActivity2026");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblPlanActivity"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_tblPlanActivity2026(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdatetblRetention2026(string sData, string UserName, string Pass)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;
            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = objComman.GetUserAuthenticate(UserName, Pass);
                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }
            }
            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("tblEnrolment_RetentionNew2026");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["tblEnrolment_Retention"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_UpdatetblRetrion2026(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch (Exception)
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }

    [WebMethod]
    public string Get_MastersScheduler(string UserName, string Password, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {


            string checkpass = objPass.CreatePasswordHashSecurityAudit(Password);

            DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);

            if (dtUser.Rows.Count > 0)
            {
                //Int32  UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
            }
            else
            {
                return "0";
            }


            SqlParameter[] para = new SqlParameter[] {

            new SqlParameter("@UserName",UserName),

            };

            try
            {
                DataSet dttabletdata = new DataSet();

                dttabletdata = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_MastersScheduler", para);


                DataSet sqldata = new DataSet("MyData");
                int index = 0;

                foreach (DataTable dt in dttabletdata.Tables)
                {
                    DataTable dtNew = new DataTable();
                    dtNew = dt.Copy();
                    dtNew.TableName = GetTableNameStaff(index);
                    sqldata.Tables.Add(dtNew);
                    index++;
                }
                sReturn = JsonConvert.SerializeObject(sqldata);
            }
            catch
            {
                sReturn = "9999";
            }
        }

        catch
        {
            sReturn = "0";
        }
        return sReturn;
    }

    private string GetTableNameStaff(int index)
    {
        string tablename = string.Empty;

        switch (index)
        {
            case 0:
                tablename = "Tbl_Training_Scheduler";
                break;

            case 1:
                tablename = "Tbl_Scheduler_Participant";
                break;

            case 2:
                tablename = "Tbl_Photo_Attendance";
                break;

            case 3:
                tablename = "Tbl_Attendance_Audit";
                break;


            default:
                tablename = "NoName";
                break;


        }

        return tablename;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateTbl_Photo_Attendance(string sData, string UserName, string Pass, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;

            string checkpass = objPass.CreatePasswordHashSecurityAudit(Pass);


            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);


                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("Tbl_Photo_Attendance");
                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["Tbl_Photo_Attendance"], DttblEnrolment_Temp);

                    DataTable Dttbltraning = objComman.CreateDataTable("Tbl_Attendance_Audit");

                    Dttbltraning = SetColumnsOrdinal(dsMyData.Tables["Tbl_Attendance_Audit"], Dttbltraning);

                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_Tbl_Photo_Attendance(DttblEnrolment_Temp, Dttbltraning);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string InsertUpdateTbl_Attendance_Audit(string sData, string UserName, string Pass, string IMEINo)
    {
        string sReturn = string.Empty;
        try
        {
            DataSet dtExportData = new DataSet();
            int UserID = 0;

            string checkpass = objPass.CreatePasswordHashSecurityAudit(Pass);


            if (UserName.Trim() != "" && Pass.Trim() != "")
            {
                DataTable dtUser = DBTask.Get_Check_PasswordNewBO(UserName, checkpass, IMEINo);


                if (dtUser.Rows.Count > 0)
                {
                    UserID = Convert.ToInt32(dtUser.Rows[0]["UserID"].ToString());
                }


            }

            if (UserID != 0)
            {
                DataSet dsMyData = new DataSet();
                XmlDocument xdMyData = new XmlDocument();
                sData = "{ \"rootNode\": {" + sData.Trim().TrimStart('{').TrimEnd('}') + "} }";
                xdMyData = (XmlDocument)JsonConvert.DeserializeXmlNode(sData);
                dsMyData.ReadXml(new XmlNodeReader(xdMyData));
                if (dsMyData.Tables.Count >= 1)
                {
                    DataTable DttblEnrolment_Temp = objComman.CreateDataTable("Tbl_Attendance_Audit");

                    DttblEnrolment_Temp = SetColumnsOrdinal(dsMyData.Tables["Tbl_Attendance_Audit"], DttblEnrolment_Temp);
                    DataSet dsResult = new DataSet();
                    dsResult = objComman.Tablet_Post_Session_Insert_Update_Tbl_Attendance_Audit(DttblEnrolment_Temp);
                    sReturn = JsonConvert.SerializeObject(dsResult);
                }

                else
                {
                    sReturn = "{\"Table\":[{\"RetValue\":-10}]}";
                }
            }
            else
            {
                sReturn = "{\"Table\":[{\"RetValue\":-5}]}";
            }
        }
        catch
        {
            sReturn = "{\"Table\":[{\"RetValue\":-99}]}";
        }
        return sReturn;
    }



    [WebMethod]
    public string UploadTraning(string filebytes, string sFilename)
    {
        // the byte array argument contains the content of the file
        // the string argument contains the name and extension
        // of the file passed in the byte array
        try
        {



            string sDirectory = Server.MapPath(Comman.GetImagePath("TraningPath")); ;


            byte[] sfilebytes = Convert.FromBase64String(filebytes);


            MemoryStream ms = new MemoryStream(sfilebytes);

            if (!Directory.Exists(sDirectory))
                Directory.CreateDirectory(sDirectory);

            sFilename = sDirectory + "\\" + sFilename;
            using (FileStream fs = new FileStream(sFilename, FileMode.Create, FileAccess.ReadWrite))
            {

                ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();
            }

            if (!File.Exists(sFilename))
            {
                return "FAIL";
            }
            return "OK";
        }
        catch (Exception ex)
        {
            // return the error message if the operation fails
            //DBTask.InsertImageUploadError(sChassisNo, fileName, ex.Message.ToString());
            return "FAIL  " + ex.Message.ToString();

        }

    }

}
