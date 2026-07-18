using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
public class clsMain
{
    public static string ImageID;
    public static string TestID;
    public static string TBImageID;
    public static string TBTestID;
    public static string Adim;

    public static string LSEImageID;
    public static string LSEFormID;
    public static string TravelImageID;
    public static string TraveAccID = ConfigurationManager.AppSettings["DataBackupPath"];
    public static string TraveGustHouseImageID;

    public static string TraveUserID;
    public DataTable LoadData(string Query)
    {
        DataTable result = new DataTable();
        try
        {
            result = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.Text, Query, new SqlParameter[0]);
        }
        catch (Exception)
        {
        }
        return result;
    }
    public DataTable Get_DataFor3Filter(string ProcedureName, string Filter1, string Filter2, string Filter3)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                            new SqlParameter("@Filter3",Filter3),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {
            throw;
        }
        return dtcombo;
    }

    public DataTable Get_DataFor4Filter(string ProcedureName, string Filter1, string Filter2, string Filter3, string Filter4)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
{
new SqlParameter("@Filter1",Filter1),
new SqlParameter("@Filter2",Filter2),
new SqlParameter("@Filter3",Filter3),
                           new SqlParameter("@Filter4",Filter4),

};
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {
            throw;
        }
        return dtcombo;
    }
    public DataTable Get_DataFor1Filter(string ProcedureName, string Filter1)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {
            throw;
        }
        return dtcombo;
    }
    public DataTable Get_DataFor5Filter(string ProcedureName, string Filter1, string Filter2, string Filter3, string Filter4, string Filter5)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@Filter1",Filter1),
                            new SqlParameter("@Filter2",Filter2),
                            new SqlParameter("@Filter3",Filter3),
                            new SqlParameter("@Filter4",Filter4),
                            new SqlParameter("@Filter5",Filter5),


                    };
            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception)
        {

        }
        return dtcombo;
    }
    public DataTable GetUserWise(string condition, string condition1, string condition2, string condition3, string StateCode, string DistrictCode, string BlockCode, string EmployeeCode, int Flag)
    {

        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@Condition",  condition),
         new SqlParameter("@Condition1",  condition1),
           new SqlParameter("@Condition2",  condition2),
               new SqlParameter("@Condition3",  condition3),
               new SqlParameter("@StateCode",  StateCode),
               new SqlParameter("@DistrictCode",  DistrictCode),
               new SqlParameter("@EmployeeCode",EmployeeCode),
               new SqlParameter("@BlockCode",  BlockCode),
       new SqlParameter("@Flag",  Flag),


                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_ModuleWise_DataReport", parm);
        return dt;
    }
    public DataTable GetGridDataEntryStatusReprt(string condition, string condition1, string condition2, string condition3, int Flag)
    {

        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@Condition",  condition),
         new SqlParameter("@Condition1",  condition1),
           new SqlParameter("@Condition2",  condition2),
               new SqlParameter("@Condition3",  condition3),
       new SqlParameter("@Flag",  Flag),


                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp_GetDataEntryStatusReport", parm);
        return dt;
    }
    public DataTable ExcelErrorData()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[EG_Get_ExcelImport_Error_data]");
        return dt;
    }

    public int SavecommunityMembers(string Schoolcode, int year, string Name, string Address, string Phone)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@SchoolCode", Schoolcode),
            new SqlParameter("@Year", year),
            new SqlParameter("@Name", Name),
            new SqlParameter("@Address", Address),
            new SqlParameter("@Phone", Phone)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SavecommunityMembers", cmdParameters);
    }

    public int DeleteSchool(string Schoolcode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", Schoolcode)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DeleteSchool", cmdParameters);
    }

    public int DeleteTM(string Schoolcode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", Schoolcode)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_DeleteTM", cmdParameters);
    }

    public DataSet GetMasterVillage(string Condition, int flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", Condition),
            new SqlParameter("@Flag", flag)
        };
        string arg_2F_0 = string.Empty;
        DataSet dataSet = new DataSet();
        return SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadMasterVillage", cmdParameters);
    }

    //public bool AddUpdate(string query)
    //{
    //    bool result;
    //    using (SqlCommand sqlCommand = new SqlCommand())
    //    {
    //        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
    //        try
    //        {
    //            new DataTable();
    //            if (sqlConnection.State == ConnectionState.Closed)
    //            {
    //                sqlConnection.Open();
    //            }
    //            sqlCommand.CommandType = CommandType.Text;
    //            sqlCommand.CommandText = query;
    //            sqlCommand.Connection = sqlConnection;
    //            sqlCommand.ExecuteNonQuery();
    //            sqlCommand.Dispose();
    //            result = true;
    //        }
    //         catch 
    //        {
    //            throw;
    //        }
    ////        finally
    //        {
    //            sqlConnection.Close();
    //        }
    //    }
    //    return result;
    //}

    public int DeleteSchool(string Schoolcode, int year)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@StateCode", Schoolcode),
            new SqlParameter("@Year", year)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteEnroll", cmdParameters);
    }

    public int DeleteBhamasa(string Schoolcode, int year)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@SchoolCode", Schoolcode),
            new SqlParameter("@Year", year)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletecommunityMembers", cmdParameters);
    }

    public DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable result = new DataTable();
        try
        {
            string value = (Condition.Length > 0) ? (" where " + Condition) : "";
            string value2 = (OrderbyCondition.Length > 0) ? (" order by " + OrderbyCondition + "  ") : "";
            string value3 = (Sortcondition.Length > 0) ? (Sortcondition ?? "") : "";
            string value4 = (TFieldName.Length > 0) ? TFieldName : "";
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@TableName", TableName),
                new SqlParameter("@Condition", value),
                new SqlParameter("@OrderbyvalueMem", value2),
                new SqlParameter("@sortbycondi", value3),
                new SqlParameter("@FieldName", value4)
            };
            DataSet dataSet = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "jslps_Get_Select_AllTableData", cmdParameters);
            result = dataSet.Tables[0];
        }
        catch (Exception)
        {
            throw;
        }
        return result;
    }

    public string Generate_RandomString(int NoChar)
    {
        Thread.Sleep(300);
        string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        string text = new string((from s in Enumerable.Repeat<string>(element, NoChar)
                                  select s[random.Next(s.Length)]).ToArray<char>()) + DateTime.Now.ToString("yyyyMMddhhmmssfff");
        return text.ToString();
    }

    public string Generate_RandomStringTemp(int NoChar)
    {
        Thread.Sleep(1000);
        string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        string text = new string((from s in Enumerable.Repeat<string>(element, NoChar)
                                  select s[random.Next(s.Length)]).ToArray<char>());
        return text.ToString();
    }

    public int SaveDataSchool(string strMainIDNo, string Disccode, string VillageCode, int MainSchoolLevel, string strMainTeacherName, string strMainSchoolName, string strMainNamePrincipal, string strMainTeacherContactNo, string strMainContactPrincipal, DateTime strMainDate, string type, string schoolod, int UserId, DateTime SurveyDate)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@pMainIDNo", strMainIDNo),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Disecode", Disccode),
            new SqlParameter("@pMainTeacherName", strMainTeacherName),
            new SqlParameter("@pMainSchoolName", strMainSchoolName),
            new SqlParameter("@pMainNamePrincipal", strMainNamePrincipal),
            new SqlParameter("@pMainTeacherContactNo", strMainTeacherContactNo),
            new SqlParameter("@pMainDate", strMainDate),
            new SqlParameter("@pMainContactPrincipal", strMainContactPrincipal),
            new SqlParameter("@pMainSchoolLevel", MainSchoolLevel),
            new SqlParameter("@pOperation", type),
            new SqlParameter("@Schoolid", schoolod),
            new SqlParameter("@UserId", UserId),
            new SqlParameter("@SurveyDate", SurveyDate)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSchool", cmdParameters);
    }

    public int SaveDataSchoolTS(string strMainIDNo, string type, string strGirlsNeverenrolled, string strSMCMeeting, int BoysEnrollment, int GrilsEnrollment, int BoysAppeared, int GrilsAppeared, int BoysRetenion, int GirlsRetenion, int BoysDropout, int GirlsDropout, int BoysNeverenrolled, int GirlsNeverenrolled, int TeacherMale, int TeacherFeMale, int TeacherTotal, int Classroom, int year, int Drinkingwater, int Separatetoilet, int Electricity, int Playground, int SwingsSlides, int Boundarywall, int Kitchen, int Teachinglearningmaterial)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@pBoysEnrollment ", BoysEnrollment),
            new SqlParameter("@pGrilsEnrollment", GrilsEnrollment),
            new SqlParameter("@pBoysAppeared  ", BoysAppeared),
            new SqlParameter("@pGrilsAppeared", GrilsAppeared),
            new SqlParameter("@pBoysRetenion", BoysRetenion),
            new SqlParameter("@pGirlsRetenion", GirlsRetenion),
            new SqlParameter("@pBoysDropout", BoysDropout),
            new SqlParameter("@pGirlsDropout", GirlsDropout),
            new SqlParameter("@pBoysNeverenrolled", BoysNeverenrolled),
            new SqlParameter("@pGirlsNeverenrolled", GirlsNeverenrolled),
            new SqlParameter("@pSMCMeeting", strSMCMeeting),
            new SqlParameter("@pTeacherMale", TeacherMale),
            new SqlParameter("@pTeacherFeMale", TeacherFeMale),
            new SqlParameter("@pTeacherTotal", TeacherTotal),
            new SqlParameter("@pClassroom", Classroom),
            new SqlParameter("@pYear", year),
            new SqlParameter("@pDrinkingwater ", Drinkingwater),
            new SqlParameter("@pSeparatetoilet", Separatetoilet),
            new SqlParameter("@pElectricitye", Electricity),
            new SqlParameter("@pPlayground", Playground),
            new SqlParameter("@pSwingsSlides", SwingsSlides),
            new SqlParameter("@pBoundarywall", Boundarywall),
            new SqlParameter("@pKitchen", Kitchen),
            new SqlParameter("@pTeachinglearningmaterial", Teachinglearningmaterial),
            new SqlParameter("@pOperation", type),
            new SqlParameter("@pSSchoolCode", strMainIDNo)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSchoolTs", cmdParameters);
    }

    public int insert_Enrol(string SchoolCode, string Year, int Class, int p1, int p2, int p3, int p4, int p5, int p6, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@StateCode", SchoolCode),
            new SqlParameter("@Year ", Year),
            new SqlParameter("@Class", Class),
            new SqlParameter("@B1 ", p1),
            new SqlParameter("@G1", p2),
            new SqlParameter("@EB1  ", p3),
            new SqlParameter("@EG1", p4),
            new SqlParameter("@PB1  ", p5),
            new SqlParameter("@PG1", p6),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[EducateGirl_Insert_UpdateNewTest]", cmdParameters);
    }

    public int CTLImplementation(string GUID_School, string VillageCode, string SchoolCode, DateTime ActivityDate, int Subject, string CLTGroup)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@GUID_School", GUID_School),
            new SqlParameter("@VillageCode ", VillageCode),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@ActivityDate ", ActivityDate),
            new SqlParameter("@Subject", Subject),
            new SqlParameter("@CLTGroup  ", CLTGroup)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Insert_CTLImplementation]", cmdParameters);
    }

    public int ActivitySchoolStatusUpdate(string Status, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Status", Status),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[UpdateSchoolActivtiyApprove]", cmdParameters);
    }
    public int ActivitySchoolStatusUpdateNew(string Status, string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Status", Status),
            new SqlParameter("@WhereQuery", WhereQuery),
                    new SqlParameter("@flag", Flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[UpdateSchoolActivtiyApproveBulk]", cmdParameters);
    }

    public int ActivityVillageStatusUpdate(string Status, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Status", Status),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ActivityVillageStatusUpdate]", cmdParameters);
    }

    public int ActivityOfficeStatusUpdate(string Status, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Status", Status),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[UpdateOfficeActivtiyApprove]", cmdParameters);
    }
    public int ActivityeApproveStatus(string BlockCode, DateTime FromDate, DateTime TODate, Int32 UserEntry)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@FromDate", FromDate),
            new SqlParameter("@ToDate", TODate),
            new SqlParameter("@UserEntry", UserEntry)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertUpdateApproveStaus]", cmdParameters);
    }

    public int InsertUpdateActivitySchool(string GUID_School, string VillageCode, string UserID, string SchoolCode, DateTime ActivityDate, string TB_Handholding, string SMC, string SMC_TB, string SMC_FC, string SMC_Mtg, string SMC_OtherSIP, string SMC_OtherDiscussions, string SMCOr, string SMCOr_TB, string SMCOr_FC, string SMC_TotTrained, string SMC_FemaleTrained, string CLT, string CLTTB, string CLTFC, string CLTHindi, string CLTEnglish, string CLTMath, string CLT_Pretest_FC, string CLT_Pretest_TB, string CTL_Midtest_FC, string CTL_Midtest_TB, string CLT_Posttest_FC, string CLT_Posttest_TB, string Clt_Pre_PC, string Clt_Mid_PC, string Clt_Post_PC, string BalSabha, string BalSabha_TB, string BalSabha_FC, string BalSabha_Formation, string BalSabha_Orientation, string BalSabha_Chart, string BalSabha_Kit, string Lifeskill_Games, string Lifeskill_Games_TB, string Lifeskill_Games_FC, string SACUpdate_TB, string SACUpdate_FC, string SACUpdate, string SAC_Periodic_Checkup, string SAC_Listing_Name_Of_Girls, string SAC_Listing_Name_Of_Boys, string SAC_Girls_Left, string SAC_Boys_Left, string SAC_Girs_Not_Joined_School, string SAC_Boys_Not_Joined_School, string SAC_No_Of_Attended, int Classrooms, int DrinkingWater, int GirlsToilet, int Electricity, int Playground, int Slide, int BoundaryWall, int Kitchen, int Teachers_Male, int Teachers_Female, int CLT_Kit, int bookAvl, int Infrastructure, int Infrastructure_FC, int Infrastructure_TB, int SIP_Annual_FC, int SIP_Annual_TB, int Retention_Annual_FC, int Retention_Annual_TB, int AnnualData, int SIP_Annual, int Retention_Annual, string SIP_PC, string Retention_PC, string Others_Description, string LifeSkillGameEntry, string UserEntry, string Flag, string ApproveBy, int CLT_Pretest, int CLT_Midtest, int CLT_Posttes, string Remark, string CreateBy)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@GUID_School", GUID_School),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@ActivityDate", ActivityDate),
            new SqlParameter("@TB_Handholding", TB_Handholding),
            new SqlParameter("@SMC", SMC),
            new SqlParameter("@SMC_TB", SMC_TB),
            new SqlParameter("@SMC_FC", SMC_FC),
            new SqlParameter("@SMC_OtherSIPPrepaired", SMC_Mtg),
            new SqlParameter("@SMC_OtherSIPComp", SMC_OtherSIP),
            new SqlParameter("@SMC_OtherDiscussions", SMC_OtherDiscussions),
            new SqlParameter("@SMCOr", SMCOr),
            new SqlParameter("@SMCOr_TB", SMCOr_TB),
            new SqlParameter("@SMCOr_FC", SMCOr_FC),
            new SqlParameter("@SMC_TotTrained", SMC_TotTrained),
            new SqlParameter("@SMC_FemaleTrained", SMC_FemaleTrained),
            new SqlParameter("@CLT", CLT),
            new SqlParameter("@CLTTB", CLTTB),
            new SqlParameter("@CLTFC", CLTFC),
            new SqlParameter("@CLTHindi", CLTHindi),
            new SqlParameter("@CLTEnglish", CLTEnglish),
            new SqlParameter("@CLTMath", CLTMath),
            new SqlParameter("@CLT_Pretest_FC", CLT_Pretest_FC),
            new SqlParameter("@CLT_Pretest_TB", CLT_Pretest_TB),
            new SqlParameter("@CTL_Midtest_FC", CTL_Midtest_FC),
            new SqlParameter("@CTL_Midtest_TB", CTL_Midtest_TB),
            new SqlParameter("@CLT_Posttest_FC", CLT_Posttest_FC),
            new SqlParameter("@CLT_Posttest_TB", CLT_Posttest_TB),
            new SqlParameter("@Clt_Pre_PC", Clt_Pre_PC),
            new SqlParameter("@Clt_Mid_PC", Clt_Mid_PC),
            new SqlParameter("@Clt_Post_PC", Clt_Post_PC),
            new SqlParameter("@BalSabha", BalSabha),
            new SqlParameter("@BalSabha_TB", BalSabha_TB),
            new SqlParameter("@BalSabha_FC", BalSabha_FC),
            new SqlParameter("@BalSabha_Formation", BalSabha_Formation),
            new SqlParameter("@BalSabha_Orientation", BalSabha_Orientation),
            new SqlParameter("@BalSabha_Chart", BalSabha_Chart),
            new SqlParameter("@BalSabha_Kit", BalSabha_Kit),
            new SqlParameter("@Lifeskill_Games", Lifeskill_Games),
            new SqlParameter("@Lifeskill_Games_TB", Lifeskill_Games_TB),
            new SqlParameter("@Lifeskill_Games_FC", Lifeskill_Games_FC),
            new SqlParameter("@SACUpdate_TB", SACUpdate_TB),
            new SqlParameter("@SACUpdate_FC", SACUpdate_FC),
            new SqlParameter("@SACUpdate", SACUpdate),
            new SqlParameter("@SAC_Periodic_Checkup", SAC_Periodic_Checkup),
            new SqlParameter("@SAC_Listing_Name_Of_Girls", SAC_Listing_Name_Of_Girls),
            new SqlParameter("@SAC_Listing_Name_Of_Boys", SAC_Listing_Name_Of_Boys),
            new SqlParameter("@SAC_Girls_Left", SAC_Girls_Left),
            new SqlParameter("@SAC_Boys_Left", SAC_Boys_Left),
            new SqlParameter("@SAC_Girs_Not_Joined_School", SAC_Girs_Not_Joined_School),
            new SqlParameter("@SAC_Boys_Not_Joined_School", SAC_Boys_Not_Joined_School),
            new SqlParameter("@SAC_No_Of_Attended", SAC_No_Of_Attended),
            new SqlParameter("@Classrooms", Classrooms),
            new SqlParameter("@DrinkingWater", DrinkingWater),
            new SqlParameter("@GirlsToilet", GirlsToilet),
            new SqlParameter("@Electricity", Electricity),
            new SqlParameter("@Playground", Playground),
            new SqlParameter("@Slide", Slide),
            new SqlParameter("@BoundaryWall", BoundaryWall),
            new SqlParameter("@Kitchen", Kitchen),
            new SqlParameter("@Teachers_Male", Teachers_Male),
            new SqlParameter("@Teachers_Female", Teachers_Female),
            new SqlParameter("@CLT_Kit", CLT_Kit),
            new SqlParameter("@bookAvl", bookAvl),
            new SqlParameter("@Infrastructure_TB", Infrastructure_TB),
            new SqlParameter("@Infrastructure", Infrastructure),
            new SqlParameter("@Infrastructure_FC", Infrastructure_FC),
            new SqlParameter("@SIP_Annual_FC", SIP_Annual_FC),
            new SqlParameter("@SIP_Annual_TB", SIP_Annual_TB),
            new SqlParameter("@Retention_Annual_FC", Retention_Annual_FC),
            new SqlParameter("@Retention_Annual_TB", Retention_Annual_TB),
            new SqlParameter("@AnnualData", AnnualData),
            new SqlParameter("@SIP_Annual", SIP_Annual),
            new SqlParameter("@Retention_Annual", Retention_Annual),
            new SqlParameter("@SIP_PC", SIP_PC),
            new SqlParameter("@Retention_PC", Retention_PC),
            new SqlParameter("@LifeSkillGameEntry", LifeSkillGameEntry),
            new SqlParameter("@Others_Description", Others_Description),
            new SqlParameter("@UserEntry", UserEntry),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@ApproveBy", ApproveBy),
            new SqlParameter("@CLT_Pretest", CLT_Pretest),
            new SqlParameter("@CLT_Midtest", CLT_Midtest),
            new SqlParameter("@CLT_Posttes", CLT_Posttes),
            new SqlParameter("@Remark", Remark),
            new SqlParameter("@CreateBy", CreateBy)

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertUpdateActivity_School]", cmdParameters);
    }

    public int ActivitySchool(string GUID_School, string VillageCode, string UserID, string SchoolCode, DateTime ActivityDate, string TB_Handholding, string SMC, string SMC_TB, string SMC_FC, string SMC_TotTrained, string SMC_FemaleTrained, string SMC_Mtg, string SMC_OtherSIP, string SMC_OtherDiscussions, string CLT, string CLT_TB, string CLT_FC, string BalSabha, string BalSabha_TB, string BalSabha_FC, string BalSabha_Formation, string BalSabha_Orientation, string BalSabha_Chart, string BalSabha_Kit, string Flag, string Lifeskill_Games, string Lifeskill_Games_TB, string Lifeskill_Games_FC, string SACUpdate_TB, string SACUpdate_FC, string SACUpdate, string SAC_Periodic_Checkup, string SAC_Listing_Name_Of_Girls, string SAC_Listing_Name_Of_Boys, string SAC_Girls_Left, string SAC_Boys_Left, string SAC_Girs_Not_Joined_School, string SAC_Boys_Not_Joined_School, string SAC_No_Of_Attended, string UserEntry, string CLTHindi, string CLTEnglish, string CLTMath, string LifeSkillGameEntry, int UserRole, int Classrooms, int DrinkingWater, int GirlsToilet, int Electricity, int Playground, int Slide, int BoundaryWall, int Kitchen, int Teachers_Male, int Teachers_Female, int CLT_Kit, int bookAvl, int SIP_Annual_FC, int SIP_Annual_TB, int Retention_Annual_FC, int Retention_Annual_TB, int AnnualData, int SIP_Annual, int Retention_Annual, int Infrastructure_FC, int Infrastructure_TB, string Other_TB, int Other_FC, string CLT_Pretest_FC, string CLT_Pretest_TB, string CTL_Midtest_FC, string CTL_Midtest_TB, string CLT_Posttest_FC, string CLT_Posttest_TB, string Clt_Pre_PC, int Infrastructure, string Clt_Mid_PC, string Clt_Post_PC, string SIP_PC, string Retention_PC)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@GUID_School", GUID_School),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@UserID", UserID),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@ActivityDate", ActivityDate),
            new SqlParameter("@TB_Handholding", TB_Handholding),
            new SqlParameter("@SMC", SMC),
            new SqlParameter("@SMC_TB", SMC_TB),
            new SqlParameter("@SMC_FC", SMC_FC),
            new SqlParameter("@SMC_TotTrained", SMC_TotTrained),
            new SqlParameter("@SMC_FemaleTrained", SMC_FemaleTrained),
            new SqlParameter("@SMC_Mtg", SMC_Mtg),
            new SqlParameter("@SMC_OtherSIP", SMC_OtherSIP),
            new SqlParameter("@SMC_OtherDiscussions", SMC_OtherDiscussions),
            new SqlParameter("@CLT", CLT),
            new SqlParameter("@CLT_TB", CLT_TB),
            new SqlParameter("@CLT_FC", CLT_FC),
            new SqlParameter("@BalSabha", BalSabha),
            new SqlParameter("@BalSabha_TB", BalSabha_TB),
            new SqlParameter("@BalSabha_FC", BalSabha_FC),
            new SqlParameter("@BalSabha_Formation", BalSabha_Formation),
            new SqlParameter("@BalSabha_Orientation", BalSabha_Orientation),
            new SqlParameter("@BalSabha_Chart", BalSabha_Chart),
            new SqlParameter("@BalSabha_Kit", BalSabha_Kit),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@Lifeskill_Games", Lifeskill_Games),
            new SqlParameter("@Lifeskill_Games_TB", Lifeskill_Games_TB),
            new SqlParameter("@Lifeskill_Games_FC", Lifeskill_Games_FC),
            new SqlParameter("@SACUpdate_TB", SACUpdate_TB),
            new SqlParameter("@SACUpdate_FC", SACUpdate_FC),
            new SqlParameter("@SACUpdate", SACUpdate),
            new SqlParameter("@SAC_Periodic_Checkup", SAC_Periodic_Checkup),
            new SqlParameter("@SAC_Listing_Name_Of_Girls", SAC_Listing_Name_Of_Girls),
            new SqlParameter("@SAC_Listing_Name_Of_Boys", SAC_Listing_Name_Of_Boys),
            new SqlParameter("@SAC_Girls_Left", SAC_Girls_Left),
            new SqlParameter("@SAC_Boys_Left", SAC_Boys_Left),
            new SqlParameter("@SAC_Girs_Not_Joined_School", SAC_Girs_Not_Joined_School),
            new SqlParameter("@SAC_Boys_Not_Joined_School", SAC_Boys_Not_Joined_School),
            new SqlParameter("@SAC_No_Of_Attended", SAC_No_Of_Attended),
            new SqlParameter("@UserEntry", UserEntry),
            new SqlParameter("@CLTHindi", CLTHindi),
            new SqlParameter("@CLTEnglish", CLTEnglish),
            new SqlParameter("@CLTMath", CLTMath),
            new SqlParameter("@LifeSkillGameEntry", LifeSkillGameEntry),
            new SqlParameter("@UserRole", UserRole),
            new SqlParameter("@Classrooms", Classrooms),
            new SqlParameter("@DrinkingWater", DrinkingWater),
            new SqlParameter("@GirlsToilet", GirlsToilet),
            new SqlParameter("@Electricity", Electricity),
            new SqlParameter("@Playground", Playground),
            new SqlParameter("@Slide", Slide),
            new SqlParameter("@BoundaryWall", BoundaryWall),
            new SqlParameter("@Kitchen", Kitchen),
            new SqlParameter("@Teachers_Male", Teachers_Male),
            new SqlParameter("@Teachers_Female", Teachers_Female),
            new SqlParameter("@CLT_Kit", CLT_Kit),
            new SqlParameter("@bookAvl", bookAvl),
            new SqlParameter("@SIP_Annual_FC", SIP_Annual_FC),
            new SqlParameter("@SIP_Annual_TB", SIP_Annual_TB),
            new SqlParameter("@Retention_Annual_FC", Retention_Annual_FC),
            new SqlParameter("@Retention_Annual_TB", Retention_Annual_TB),
            new SqlParameter("@AnnualData", AnnualData),
            new SqlParameter("@SIP_Annual", SIP_Annual),
            new SqlParameter("@Retention_Annual", Retention_Annual),
            new SqlParameter("@Infrastructure_FC", Infrastructure_FC),
            new SqlParameter("@Infrastructure_TB", Infrastructure_TB),
            new SqlParameter("@Other_TB", Other_TB),
            new SqlParameter("@Other_FC", Other_FC),
            new SqlParameter("@CLT_Pretest_FC", CLT_Pretest_FC),
            new SqlParameter("@CLT_Pretest_TB", CLT_Pretest_TB),
            new SqlParameter("@CTL_Midtest_FC", CTL_Midtest_FC),
            new SqlParameter("@CTL_Midtest_TB", CTL_Midtest_TB),
            new SqlParameter("@CLT_Posttest_FC", CLT_Posttest_FC),
            new SqlParameter("@CLT_Posttest_TB", CLT_Posttest_TB),
            new SqlParameter("@Clt_Pre_PC", Clt_Pre_PC),
            new SqlParameter("@Infrastructure", Infrastructure),
            new SqlParameter("@Clt_Mid_PC", Clt_Mid_PC),
            new SqlParameter("@Clt_Post_PC", Clt_Post_PC),
            new SqlParameter("@SIP_PC", SIP_PC),
            new SqlParameter("@Retention_PC", Retention_PC)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertWebActivityUpdate_School]", cmdParameters);
    }

    public int LifeskillGames(string GUID_School, string VillageCode, string SchoolCode, DateTime ActivityDate, int Subject)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@GUID_School", GUID_School),
            new SqlParameter("@VillageCode ", VillageCode),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@ActivityDate ", ActivityDate),
            new SqlParameter("@GameNo", Subject)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Insert_LifeskillGames]", cmdParameters);
    }

    public int insert_EnrolVerify(string SchoolCode, string Year, int Class, int p1, int p2, int p3, int p4, int p5, int p6, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@StateCode", SchoolCode),
            new SqlParameter("@Year ", Year),
            new SqlParameter("@Class", Class),
            new SqlParameter("@B1 ", p1),
            new SqlParameter("@G1", p2),
            new SqlParameter("@EB1  ", p3),
            new SqlParameter("@EG1", p4),
            new SqlParameter("@PB1  ", p5),
            new SqlParameter("@PG1", p6),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[EducateGirl_Insert_UpdateVerify]", cmdParameters);
    }

    public int SaveDataSchoolVerify(string strMainIDNo, string Disccode, string VillageCode, int MainSchoolLevel, string strMainTeacherName, string strMainSchoolName, string strMainNamePrincipal, string strMainTeacherContactNo, string strMainContactPrincipal, DateTime strMainDate, string type, string schoolod, int UserId)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@pMainIDNo", strMainIDNo),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Disecode", Disccode),
            new SqlParameter("@pMainTeacherName", strMainTeacherName),
            new SqlParameter("@pMainSchoolName", strMainSchoolName),
            new SqlParameter("@pMainNamePrincipal", strMainNamePrincipal),
            new SqlParameter("@pMainTeacherContactNo", strMainTeacherContactNo),
            new SqlParameter("@pMainDate", strMainDate),
            new SqlParameter("@pMainContactPrincipal", strMainContactPrincipal),
            new SqlParameter("@pMainSchoolLevel", MainSchoolLevel),
            new SqlParameter("@pOperation", type),
            new SqlParameter("@Schoolid", schoolod),
            new SqlParameter("@UserId", UserId)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSchoolVerify", cmdParameters);
    }

    public int SaveDataSchoolTSVerify(string strMainIDNo, string type, string strGirlsNeverenrolled, string strSMCMeeting, int BoysEnrollment, int GrilsEnrollment, int BoysAppeared, int GrilsAppeared, int BoysRetenion, int GirlsRetenion, int BoysDropout, int GirlsDropout, int BoysNeverenrolled, int GirlsNeverenrolled, int TeacherMale, int TeacherFeMale, int TeacherTotal, int Classroom, int year, int Drinkingwater, int Separatetoilet, int Electricity, int Playground, int SwingsSlides, int Boundarywall, int Kitchen, int Teachinglearningmaterial)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@pBoysEnrollment ", BoysEnrollment),
            new SqlParameter("@pGrilsEnrollment", GrilsEnrollment),
            new SqlParameter("@pBoysAppeared  ", BoysAppeared),
            new SqlParameter("@pGrilsAppeared", GrilsAppeared),
            new SqlParameter("@pBoysRetenion", BoysRetenion),
            new SqlParameter("@pGirlsRetenion", GirlsRetenion),
            new SqlParameter("@pBoysDropout", BoysDropout),
            new SqlParameter("@pGirlsDropout", GirlsDropout),
            new SqlParameter("@pBoysNeverenrolled", BoysNeverenrolled),
            new SqlParameter("@pGirlsNeverenrolled", GirlsNeverenrolled),
            new SqlParameter("@pSMCMeeting", strSMCMeeting),
            new SqlParameter("@pTeacherMale", TeacherMale),
            new SqlParameter("@pTeacherFeMale", TeacherFeMale),
            new SqlParameter("@pTeacherTotal", TeacherTotal),
            new SqlParameter("@pClassroom", Classroom),
            new SqlParameter("@pYear", year),
            new SqlParameter("@pDrinkingwater ", Drinkingwater),
            new SqlParameter("@pSeparatetoilet", Separatetoilet),
            new SqlParameter("@pElectricitye", Electricity),
            new SqlParameter("@pPlayground", Playground),
            new SqlParameter("@pSwingsSlides", SwingsSlides),
            new SqlParameter("@pBoundarywall", Boundarywall),
            new SqlParameter("@pKitchen", Kitchen),
            new SqlParameter("@pTeachinglearningmaterial", Teachinglearningmaterial),
            new SqlParameter("@pOperation", type),
            new SqlParameter("@pSSchoolCode", strMainIDNo)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSchoolTsVerify", cmdParameters);
    }

    public int RemoveAndSaveDataD2d(string strMainIDNo)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueCode", strMainIDNo)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "RemoveDataDTD", cmdParameters);
    }

    public int RemoveAndSaveDataOutOfD2d(string strMainIDNo)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueCode", strMainIDNo)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "RemoveAndUpdateDataOutDTD", cmdParameters);
    }

    public int RemoveAndUpdateDataDTDandEnrollment(string D2dUniqueCode, string EnrollUniqueCode, string ChildName, string fatherName, string HHNo, string serial)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@D2dUniqueCode", D2dUniqueCode),
            new SqlParameter("@EnrollUniqueCode", EnrollUniqueCode),
            new SqlParameter("@ChildName", ChildName),
            new SqlParameter("@fatherName", fatherName),
            new SqlParameter("@HHNo", HHNo),
            new SqlParameter("@serial", serial)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "RemoveAndUpdateDataDTDandEnrollment", cmdParameters);
    }

    public int SaveDataTeamBalika(string strMainIDNo, string TcodeSerial, string Tcode, string VillageCode, string TBName, int Gender, string strFatherName, int SocialCategory, int EducationLevel, int FamilyOccupation, int DOBAvailable, DateTime DOB, int AgeAson, DateTime AsOnDate, int ReasonForTBChoice, int RecruitmentReferalInfo, bool PriorWorkExperience, int TotalPriorWorkExperience, int PriorWorkYearMonth, string Contact, string flag, string Expectation, string Abvision, string MotherName, string ImagePath, DateTime DateofJoining, int dropOutStatus, int DroupOutRe, DateTime DropoutResone, string createby)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueCode", strMainIDNo),
            new SqlParameter("@TBCode", Tcode),
            new SqlParameter("@TBName", TBName),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Gender", Gender),
            new SqlParameter("@FatherMotherName", strFatherName),
            new SqlParameter("@SocialCategory", SocialCategory),
            new SqlParameter("@EducationLevel", EducationLevel),
            new SqlParameter("@FamilyOccupation", FamilyOccupation),
            new SqlParameter("@DOBAvailable", DOBAvailable),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@AgeAson", AgeAson),
            new SqlParameter("@AsOnDate", AsOnDate),
            new SqlParameter("@ReasonForTBChoice", ReasonForTBChoice),
            new SqlParameter("@RecruitmentReferalInfo", RecruitmentReferalInfo),
            new SqlParameter("@PriorWorkExperience", PriorWorkExperience),
            new SqlParameter("@TotalPriorWorkExperience", TotalPriorWorkExperience),
            new SqlParameter("@PriorWorkYearMonth", PriorWorkYearMonth),
            new SqlParameter("@Contact", Contact),
            new SqlParameter("@flag", flag),
            new SqlParameter("@Expectation", Expectation),
            new SqlParameter("@Abvision", Abvision),
            new SqlParameter("@MotherName", MotherName),
            new SqlParameter("@TcodeSerial", TcodeSerial),
            new SqlParameter("@ImagePath", ImagePath),
            new SqlParameter("@DateofJoining", DateofJoining),
            new SqlParameter("@dropOutStatus", dropOutStatus),
            new SqlParameter("@DroupOutRe", DroupOutRe),
            new SqlParameter("@DropoutResone", DropoutResone),
            new SqlParameter("@createby", createby)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalika", cmdParameters);
    }

    public DataTable ReportD2d(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportDTDVerfiy]", cmdParameters);
    }
    public DataTable ReportD2dAllReport(string Frist, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportAllTypeDTD]", cmdParameters);
    }

    public DataTable ReportD2dENrollmentStatus(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportDTDVerfiyAndEnrollmentStatus]", cmdParameters);
    }
    public DataTable ReportD2dENrollmentStatusNew(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportDTDVerfiyAndEnrollmentStatusNew]", cmdParameters);
    }

    public DataTable ReportD2dEnrollment(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2dandEnrollmentVerfiy]", cmdParameters);
    }

    public DataTable ReportEnrollDeatils(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatils]", cmdParameters);
    }
    public DataTable ReportEnrollDeatilsNew(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollDeatilsNew]", cmdParameters);
    }

    public DataTable AgeWiseSocialCategory(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeWiseSocialCategory]", cmdParameters);
    }

    public DataTable AgeWisFeamilyOccupation(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeWisFeamilyOccupation]", cmdParameters);
    }

    public DataTable Baseline(string Frist, string subject, int SubjectId)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@subject", subject),
            new SqlParameter("@subjectId", SubjectId)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptBaseline]", cmdParameters);
    }
    public DataTable rptRetention(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptRetention]", cmdParameters);
    }

    public DataTable AgeWiseEducationstatus(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeWiseEducationstatus]", cmdParameters);
    }

    public DataTable AgeWiseGrade(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeGradewise]", cmdParameters);
    }

    public DataTable AgeWiseEnrollPlan(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeWiseEducationsPlan]", cmdParameters);
    }

    public DataTable AgeWiseEducationsReason(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeWiseEducationsReason]", cmdParameters);
    }

    public DataTable rptEnrollmentAnalayis(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@cond", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptEnrollmentAnalayis]", cmdParameters);
    }

    public DataTable AgeWise(string Frist, int Age)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist),
            new SqlParameter("@AgeGrup", Age)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAgeWise]", cmdParameters);
    }

    public DataTable ReportUserEntery(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportUserWiseEntry]", cmdParameters);
    }

    public DataTable OutD2d(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[OutDTDVerfiy]", cmdParameters);
    }

    public DataTable OutD2dEnrollment(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptOutOfD2dandEnrollmentVerfiy]", cmdParameters);
    }

    public DataTable ReportEnrollUserWiseEntry(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollUserWiseEntry]", cmdParameters);
    }

    public DataTable Report(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtionCr", Frist),
            new SqlParameter("@condtionMo ", Second),
            new SqlParameter("@condtionDe", Third),
            new SqlParameter("@condtionAll", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportDTD]", cmdParameters);
    }

    public DataTable tblReport(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLogin]", cmdParameters);
    }
    public DataTable tblReportGraph(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginGraph]", cmdParameters);
    }
    public DataTable rptTblUserLoginGraphCount(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginGraphCount]", cmdParameters);
    }
    public DataTable rptTblUserLoginMapLong(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginMapLong]", cmdParameters);
    }
    public DataTable tblReportBO(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginBO]", cmdParameters);
    }
    public DataTable tblReportVersion(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptTblUserLoginVersion]", cmdParameters);
    }
    public DataTable ReportEnrollment(string Frist, string Second, string Third, string Fourth)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtionCr", Frist),
            new SqlParameter("@condtionMo ", Second),
            new SqlParameter("@condtionDe", Third),
            new SqlParameter("@condtionAll", Fourth)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportEnrollment]", cmdParameters);
    }
    public DataTable LoadMasterImport(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", Frist),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptInportExcel]", cmdParameters);
    }
    public DataTable LoadVillageActivtiy(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiy]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyOfffice(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyOffice]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyCluseter(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyClusterWise]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyCluseterNew(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyClusterWiseNew]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyCluseterNewIO(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyClusterWiseNewIO]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyCluseterNewReport(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fyear", fdate),
            new SqlParameter("@toMonth ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyClusterWiseNewReport]", cmdParameters);
    }

    public DataTable LoadActivtiyOfficeClusterWiseReport(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fyear", fdate),
            new SqlParameter("@toMonth ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyOfficeClusterWiseReport]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyClusterWiseNewReport(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fyear", fdate),
            new SqlParameter("@toMonth ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyClusterWiseNewReport]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyOfficeCluseter(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyOfficeClusterWise]", cmdParameters);
    }
    public DataTable GetActivityDateWiseBlankRecord(string fdate, string toDate, string BlockCode, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@fDate ", fdate),
            new SqlParameter("@toDate", toDate),
            new SqlParameter("@Flag", Flag),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityDateWiseBlankRecord]", cmdParameters);
    }
    public DataTable GetActivityDistinctAllVillage(string fdate, string toDate, string BlockCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@fDate ", fdate),
            new SqlParameter("@toDate", toDate),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityDistinctAllVillage]", cmdParameters);
    }
    public DataTable LoadVillageActivtiyOfficeCluseterNew(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyOfficeClusterWiseNew]", cmdParameters);
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
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyAllClusterWiseNew]", cmdParameters);
    }

    public DataTable LoadActivtiyAllBlockClusterWise(string fdate, string toDate, string userName, string WhereQuery, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyAllBlockClusterWiseNew]", cmdParameters);
    }
    public DataTable LoadActivtiyAllDateNewWise(string fdate, string toDate, string userName, string WhereQuery, string WhereQuery1, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
                new SqlParameter("@WhereQueryD2d", WhereQuery1),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyAllDateNewWiseNew]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyBlockWise(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyBlockWiseNew]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyCluseterIO(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyClusterWiseIO]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiy(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiy]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyNew(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyNew]", cmdParameters);
    }

    public DataTable LoadVIllageActivtiyNew(string fdate, string toDate, string userName, string WhereQuery, string WhereQuery1)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@WhereQueryD2d", WhereQuery1)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyDateWiseNew]", cmdParameters);
    }

    public DataTable LoadOfficeActivtiyNew(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyOfficeClusterDateWise]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyCluseter(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyClusterWise]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyCluseterNew(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyClusterWiseNew]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyCluseterReport(string year, string month, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fyear", year),
            new SqlParameter("@toMonth ", month),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyClusterWiseReport]", cmdParameters);
    }

    public DataTable LoadAllActivtiyDateWiseAll(string year, string month, string userName, string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", year),
            new SqlParameter("@toDate ", month),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadDatewisectivtiyReport]", cmdParameters);
    }

    public DataTable LoadActivtiyBlockWiseReport(string year, string month, string userName, string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fyear", year),
            new SqlParameter("@toMonth ", month),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyBlockWiseReport]", cmdParameters);
    }

    public DataTable LoadAllActivtiyDatewise(string WhereQuery, int flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetAllActivityUpdateDateWise]", cmdParameters);
    }
    public DataTable GetActivityUserWiseMaxDate(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityUserWiseMaxDate]", cmdParameters);
    }
    public DataTable GetActivityUserWiseMaxDateNew(string WhereQuery, string CluserterCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", WhereQuery)   ,
                new SqlParameter("@CluserterCode", CluserterCode)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityUserWiseMaxDateNew]", cmdParameters);
    }

    public DataTable LoadGKPDeatils(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", WhereQuery)    ,

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadGKPDeatilsNew]", cmdParameters);
    }
    public DataTable LoadCheckGkp(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@con", WhereQuery)    ,

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadCheckGkp]", cmdParameters);
    }
    public DataTable GetActivityUserWiseMaxDateNewIO(string WhereQuery, string CluserterCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", WhereQuery)   ,
                new SqlParameter("@CluserterCode", CluserterCode)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityUserWiseMaxDateNewIO]", cmdParameters);
    }
    public DataTable GetActivityUpdateDateWiseBlockWise(string BlockCode, string UserEntry, string ApproveStatus)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@UserEntry ", UserEntry),
            new SqlParameter("@ApproveStatus", ApproveStatus)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityUpdateDateWiseBlockWiseNew]", cmdParameters);
    }
    public DataTable GetDistrictGridData(string Year)
    {
        SqlParameter[] Param = new SqlParameter[]
        {
            new SqlParameter("@Year", Year)

        };

        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetDistrictGridData]", Param);
    }
    public DataTable GetActivityUpdateDateWiseBlockWiseNew(string BlockCode, string UserEntry, string ApproveStatus)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@UserEntry ", UserEntry),
            new SqlParameter("@ApproveStatus", ApproveStatus)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityUpdateDateWiseBlockWiseNewApprrove]", cmdParameters);
    }

    public DataTable GetActivityUpdateDateWiseDistWise(string BlockCode, string UserEntry, string ApproveStatus)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@DistrictCode", BlockCode),
            new SqlParameter("@UserEntry ", UserEntry),
            new SqlParameter("@ApproveStatus", ApproveStatus)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityUpdateDateWiseDistWise]", cmdParameters);
    }

    public DataTable GetSchoolActivtiy(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetSchoolWiseActivity]", cmdParameters);
    }
    public DataTable GetGKPWiseActivity(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetGKPWiseActivity]", cmdParameters);
    }

    public DataTable GeVillageActivtiy(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetVillageWiseActivity]", cmdParameters);
    }


    public DataTable GetOfficeWiseActivity(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetOfficeWiseActivity]", cmdParameters);
    }

    public DataTable GetActivityClusterLoad(string Blcode, string fdate, string toDate)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@BlockCode", Blcode),
            new SqlParameter("@fDate", fdate),
            new SqlParameter("@toDate ", toDate),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetActivityClusterLoad]", cmdParameters);
    }
    public DataTable LoadSchoolActivtiyBlockWise(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyBlockWiseNew]", cmdParameters);
    }
    public DataTable LoadSchoolActivtiyForAllTypeReport(string fdate, string toDate, string userName, string WhereQuery, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyForAllTypeReport]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyOfficeBlockWise(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadActivtiyOfficeBlockWise]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyCluseterIO(string fdate, string toDate, string userName, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@userName", userName),
            new SqlParameter("@WhereQuery", WhereQuery)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyClusterWiseIO]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyApprove(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyApprove]", cmdParameters);
    }

    public DataTable LoadActivtiyApproveAllReport(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadAllActivtiyReportApprove]", cmdParameters);
    }

    public DataTable LoadSchoolActivtiyApproveNew(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadSchoolActivtiyApproveNew]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyApprove(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyApprove]", cmdParameters);
    }

    public DataTable LoadVillageActivtiyApproveNew(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadVillageActivtiyApproveNew]", cmdParameters);
    }

    public DataTable LoadOfficeActivtiyApprove(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadOfficeActivtiyApprove]", cmdParameters);
    }
    public DataTable LoadGKPActivtiyApprove(string WhereQuery, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadGKPActivtiyApprove]", cmdParameters);
    }

    public DataTable LoadTB(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@uID", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadTBTRanining]", cmdParameters);
    }

    public DataTable LoadTeamBalikTraining(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", Frist)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadTeamBalikTraining]", cmdParameters);
    }

    public int insert_Attendeace(string AttUniqueCode, string TBId, DateTime AttDate, int Status)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@AttUniqueCode", AttUniqueCode),
            new SqlParameter("@TBId ", TBId),
            new SqlParameter("@AttDate", AttDate),
            new SqlParameter("@Status ", Status)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertUpdateAttendance]", cmdParameters);
    }

    public DataTable LoadMasterData(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Report_Master_Data]", cmdParameters);
    }
    public DataTable LoadMasterDataNew(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Report_Master_DataNew]", cmdParameters);
    }
    public DataTable LoadMasterDataNew2025(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Report_Master_DataNewMaster]", cmdParameters);
    }
    public DataTable LoadAnnaulPlanRowData(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[LoadAnnaulPlanRowData]", cmdParameters);
    }

    public DataTable rptAnnualSummaryCluserWise(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@DistCode", condition),
                    new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptAnnualSummaryCluserWise]", cmdParameters);
    }
    public DataTable LoadSIPData(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptSIPDeatials]", cmdParameters);
    }
    public DataTable LoadDTDInEligible(string condition)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", condition)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportDTDInEligible]", cmdParameters);
    }
    public DataTable rptRetentionIndividual(string condition)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", condition)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptRetentionIndividual]", cmdParameters);
    }

    public DataTable ReportMobileActivityStatus(string condition, string Year)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@condtion", condition),

            new SqlParameter("@Year", Year)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportMobileActivityStatus]", cmdParameters);
    }
    public int DeleteCLTData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteCLTData", cmdParameters);
    }
    public int DeleteCLTDataIO(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteCLTDataIO", cmdParameters);
    }

    public int DeleteEnrollMentData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteEnrollMentData", cmdParameters);
    }
    public int DeleteReEnrollMentData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteReEnrollMentData", cmdParameters);
    }
    public int DeleteD2dData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteD2Dservey", cmdParameters);
    }

    public int DeleteTBTraing(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteTBTraing", cmdParameters);
    }
    public int DeleteD2dDataAcctivtiyAchool(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteActivitySchool", cmdParameters);
    }
    public int DeleteD2dDataAcctivtiyVillage(string UniqueChildCode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteAcctivtiyVillage", cmdParameters);
    }
    public int DeleteInddtData(string UniqueChildCode, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueChildCode ", UniqueChildCode),
            new SqlParameter("@flag", flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteInelgiable", cmdParameters);
    }

    public DataTable GetCLTData(string condition, int Flag, int Term)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag),
            new SqlParameter("@Term",Term)

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_Data]", cmdParameters);
    }
    public DataTable GetCLTDataIO(string condition, int Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Condition", condition),
            new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_CLT_DataIO]", cmdParameters);
    }

    public DataTable CheckPassword(string UserName, string Password)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", SqlDbType.NVarChar,100){Value=UserName },
            new SqlParameter("@Password", SqlDbType.NVarChar,250){Value= Password }
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_Check_Password]", cmdParameters);
    }

    public DataTable CheckPasswordNew(string UserName, string Password, string IMi)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", SqlDbType.NVarChar,100){Value=UserName },
            new SqlParameter("@Password", SqlDbType.NVarChar,250){Value= Password },
            new SqlParameter("@IMEINo", SqlDbType.NVarChar,250){Value= IMi }
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_Check_PasswordNew]", cmdParameters);
    }

    public DataTable Get_Check_PasswordNewFC(string UserName, string Password, string IMi)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", SqlDbType.NVarChar,100){Value=UserName },
            new SqlParameter("@Password", SqlDbType.NVarChar,250){Value= Password },
            new SqlParameter("@IMEINo", SqlDbType.NVarChar,250){Value= IMi }
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_Check_PasswordNewFC]", cmdParameters);
    }

    public DataTable GetUserLoginAuthenticateFC(string UserName, string Password, string IMi)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
           new SqlParameter("@UserName", SqlDbType.NVarChar,100){Value=UserName },
            new SqlParameter("@Password", SqlDbType.NVarChar,250){Value= Password },
            new SqlParameter("@IMEINo", SqlDbType.NVarChar,250){Value= IMi }
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetUserLoginAuthenticateFC]", cmdParameters);
    }
    public DataTable Get_Check_PasswordNewBO(string UserName, string Password, string IMi)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserName", SqlDbType.NVarChar,100){Value=UserName },
            new SqlParameter("@Password", SqlDbType.NVarChar,250){Value= Password },
            new SqlParameter("@IMEINo", SqlDbType.NVarChar,250){Value= IMi }
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[Get_Check_PasswordNewBO]", cmdParameters);
    }

    public DataTable mstActivityVillageCheck(string UserID, string Villagecode, Int32 Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserID", UserID),
    new SqlParameter("@Villagecode", Villagecode),
                new SqlParameter("@Flag", Flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[mstActivityVillageCheck]", cmdParameters);
    }

    public int mstActivityVillageMaster(string UserID, string Villagecode, string VillageName)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserId", UserID),
            new SqlParameter("@villagecode", Villagecode),
                new SqlParameter("@villageName", VillageName)

        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "mstActivityVillageMaster", cmdParameters);
    }
    public int DeleteActivityVillage(string UserId, string villagecode)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UserId ", UserId),
            new SqlParameter("@villagecode", villagecode)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "mstDeleteVillageMaster", cmdParameters);
    }
    public int DeleteUserRole(string UniqueChildCode)
    {
        Int32 Icoutn = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@level ", UniqueChildCode)

        };
        Icoutn = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp__GetUseRrightDelete", cmdParameters);
        return Icoutn;
    }
    public int DeleteUserActivity(string UniqueChildCode, Int32 ActiveStatus)
    {
        Int32 Icoutn = 0;
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@level ", UniqueChildCode),
            new SqlParameter("@ActiveStatus ", ActiveStatus)

        };
        Icoutn = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Sp__GetUseMaterDelete", cmdParameters);
        return Icoutn;
    }

    public DataTable rptActivityUpdateReports(string fdate, string toDate, string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@fdate", fdate),
            new SqlParameter("@toDate ", toDate),
            new SqlParameter("@WhereQuery", WhereQuery),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityUpdateReports]", cmdParameters);
    }
    public DataTable rptActivityUpdateReportsMonthly(string WhereQuery)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityUpdateReportsMonthly]", cmdParameters);
    }

    public DataTable rptActivityWeaklyReport(string WhereQuery, string Q1, string Q2, string Q3, string Q4, string Q5, Int32 flag, Int32 @Month)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Q1", Q1),
            new SqlParameter("@Q2", Q2),
          new SqlParameter("@Q3", Q3),
          new SqlParameter("@Q4", Q4),
          new SqlParameter("@Q5", Q5),
          new SqlParameter("@flag", flag),
           new SqlParameter("@Month", Month),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityWeaklyReportNew]", cmdParameters);
    }
    public DataTable rptActivityMonthReport(string WhereQuery, string Q1, string Q2, string Q3, string Q4, Int32 flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Q1", Q1),
            new SqlParameter("@Q2", Q2),
          new SqlParameter("@Q3", Q3),
          new SqlParameter("@Q4", Q4),
          new SqlParameter("@flag", flag),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityMonthReport]", cmdParameters);
    }
    public DataTable rptActivityMonthReportNew(string WhereQuery, string Q1, string G)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@SelectQuery", Q1),
            new SqlParameter("@Groupby", G),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityMontlyNew]", cmdParameters);
    }

    public DataTable rptActivitySIPSummaryReport(string WhereQuery, string conditions1)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
        new SqlParameter("@Con", conditions1),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivitySIPSummaryReport]", cmdParameters);
    }


    public DataTable rptContactSummary(string WhereQuery, string conditions1, string Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
            new SqlParameter("@Con", conditions1),
            new SqlParameter("@Flag", Flag),


        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummary]", cmdParameters);
    }
    public DataTable rptContactSummaryOutReach(string WhereQuery, string conditions1, string Flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
            new SqlParameter("@Flag", Flag),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptD2d2ContactBlockWiseSummaryOutReach]", cmdParameters);
    }

    public DataTable rptActivityLifeSkillReport(string WhereQuery, string conditions1)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@schoolCode", WhereQuery),
           new SqlParameter("@Con", conditions1),

        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityLifeSkillReport]", cmdParameters);
    }
    public DataTable rptActivityQuerlty(string WhereQuery, string Q1, string Q2, string Q3, string Q4, Int32 flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),
            new SqlParameter("@Q1", Q1),
            new SqlParameter("@Q2", Q2),
          new SqlParameter("@Q3", Q3),
          new SqlParameter("@Q4", Q4),
          new SqlParameter("@flag", flag),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityQuerlty]", cmdParameters);

    }


    public DataTable rptActivityQuerltyNew(string WhereQuery, Int32 flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {

            new SqlParameter("@WhereQuery", WhereQuery),

          new SqlParameter("@flag", flag),
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptActivityQuerltyNew]", cmdParameters);
    }

    public DataTable ReenrollmentData(string conditions, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@Con", conditions),
            new SqlParameter("@Flag", flag)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[ReportReEnrollDeatils]", cmdParameters);

    }

    public int saveVEAsporation(string UID, string TBCode, string villagecode, int lastedu, int eduStatus, int lhe, int lhetype, decimal income, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UniqueCode", UID),
            new SqlParameter("@TBCode", TBCode),
            new SqlParameter("@VillageCode", villagecode),
            new SqlParameter("@lastedu", lastedu),
            new SqlParameter("@eduStatus", eduStatus),
            new SqlParameter("@lhe", lhe),
            new SqlParameter("@lhetype", lhetype),
            new SqlParameter("@income", income),

            new SqlParameter("@flag", flag)
        };

        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateVEaspiration", cmdParameters); ;
    }


    public int saveEnrolledStatus(string UID, string TBCode, string Vcode, string preference, string enrollment, string organization, DateTime startdate, DateTime enddate, int duration, int palaement, DateTime Pdate, string porg, string designation, decimal salary, string remarks, string flag)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("@UID", UID) ,
            new SqlParameter("@TBCode", TBCode),
            new SqlParameter("@villagecode",Vcode),
            new SqlParameter("@preference",preference),
            new SqlParameter("@enrollment",enrollment),
            new SqlParameter("@organization", organization),
            new SqlParameter("@startdate",startdate),
            new SqlParameter("@enddate",enddate),
            new SqlParameter("@duration",duration),
            new SqlParameter("@palaement",palaement),
            new SqlParameter("@PDate",Pdate),
            new SqlParameter("@porg",porg),
            new SqlParameter("@designation",designation),
            new SqlParameter("@salary",salary),
            new SqlParameter("@remarks",remarks),
            new SqlParameter("@flag",flag)
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdaateEnrolledStatus", cmdParameters); ;
    }


    public int InsertUpdateverification(string UniqueCode, string VillageCode, string SchoolCode, int CV_UID, string value, string comboID, int fromID, string flag)
    {

        SqlParameter[] p = new SqlParameter[]
       {
           new SqlParameter("@UniqueCode", UniqueCode),
           new SqlParameter("@VillageCode", VillageCode),
           new SqlParameter("@SchoolCode", SchoolCode),
           new SqlParameter("@CV_UID", CV_UID),
           new SqlParameter("@value", value),
           new SqlParameter("@comboID",comboID),
           new SqlParameter("@fromID", fromID),
           new SqlParameter("@flag", flag)

       };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateverification", p);
        return result;
    }
    public DataTable SaveUpdate(string latlong, string VillageCode)
    {

        SqlParameter[] parm = new SqlParameter[]
            {
       new SqlParameter("@latlong",  latlong),
         new SqlParameter("@VillageCode",  VillageCode),

                 };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_SaveUpdate_GeoLocation", parm);
        return dt;
    }


    public DataTable ReportCVverifivcation(string con, String FormID)
    {
        SqlParameter[] p = new SqlParameter[]
        {
            new SqlParameter("@Str", con),
            new SqlParameter("@Form", FormID)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_VerficationReport", p);
    }

    public int DeleteRecord(string uid, Int32 Flag)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@UID", uid),
             new SqlParameter("@FormID", Flag),
              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_DeleteRecord", parm);
        return result;

    }


    public int ApproveTracker(string uid, Int32 Flag, Int32 Staus, string username)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
             new SqlParameter("@UID", uid),
             new SqlParameter("@FormID", Flag),
                   new SqlParameter("@Status", Staus),
               new SqlParameter("@Username", username),
              };
        int result = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_ApproveTracker", parm);
        return result;

    }

    public DataTable ReportTracker(string con, String FormID)
    {
        SqlParameter[] p = new SqlParameter[]
        {
            new SqlParameter("@Str", con),
            new SqlParameter("@Form", FormID)
        };
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "sp_ReportTracker", p);
    }
    public DataTable OutD2dEnrollmentRemoveLeftGrid(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@condtion", Frist)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptOutOfD2dandSealSignSpecificationRemoveDupliateLeft]", cmdParameters);
    }
    public DataTable EnrollmentRemoveRightGrid(string Frist)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
{
new SqlParameter("@condtion", Frist)
};
        return SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[rptPtentialMatchesSealSignSpecificationRemoveDupliateRight]", cmdParameters);
    }
    public int UpdateFilingSystemSend(string SchoolCode, string sendFile, string RecieveFiles)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
       {
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@SendFlag", sendFile),
            new SqlParameter("@RecieveFlag", RecieveFiles)
       };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_FilingSendUpdate", cmdParameters);
    }


    public int UpdateFilingSystemRecieved(string schoolCode, string recieveFiles)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
      {
            new SqlParameter("@SchoolCode", schoolCode),
            //new SqlParameter("@SendFlag", recieveFiles),
            new SqlParameter("@RecieveFlag", recieveFiles)
      };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "usp_FilingRecieveUpdate", cmdParameters);
    }

    public int InsertUpdateLearningCampMaster(int CampId, int campNo, string campDurationInWeek, string sessioninCamp, string sessioninWeek, string hindiBaselineSessionNo, string hindiEndlineSessionNo, string mathBaselineSessionNo, string mathEndlineSessionNo, string hindiBaselineHeading1, string hindiBaselineHeading2, string mathBaselineHeading1, string mathBaselineHeading2, string hindiBaselineEndlineMaxScore, string mathBaselineEndlineMaxScore, string hindiBaselineEndlineHeading2Active, string mathBaselineEndlineHeading2Active, string userName, string operation)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
                    new SqlParameter("@CampId", CampId),
                    new SqlParameter("@campNo", campNo),
                    new SqlParameter("@campDurationInWeek", campDurationInWeek),
                    new SqlParameter("@sessioninCamp", sessioninCamp),
                    new SqlParameter("@sessioninWeek", sessioninWeek),
                    new SqlParameter("@hindiBaselineSessionNo", hindiBaselineSessionNo),
                    new SqlParameter("@hindiEndlineSessionNo", hindiEndlineSessionNo),
                    new SqlParameter("@mathBaselineSessionNo", mathBaselineSessionNo),
                    new SqlParameter("@mathEndlineSessionNo", mathEndlineSessionNo),
                    new SqlParameter("@hindiBaselineHeading1", hindiBaselineHeading1),
                    new SqlParameter("@hindiBaselineHeading2", hindiBaselineHeading2),
                    new SqlParameter("@mathBaselineHeading1", mathBaselineHeading1),
                    new SqlParameter("@mathBaselineHeading2", mathBaselineHeading2),
                    new SqlParameter("@hindiBaselineEndlineMaxScore", hindiBaselineEndlineMaxScore),
                    new SqlParameter("@mathBaselineEndlineMaxScore", mathBaselineEndlineMaxScore),
                    new SqlParameter("@hindiBaselineEndlineHeading2Active", hindiBaselineEndlineHeading2Active),
                    new SqlParameter("@mathBaselineEndlineHeading2Active", mathBaselineEndlineHeading2Active),
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@operation", operation)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateLearningCampMaster", cmdParameters);
    }
    public DataTable GetCampNo()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[GetCampNo]");
        return dt;
    }

    public DataTable GetCampExit(int campNo)
    {
        SqlParameter[] parm = new SqlParameter[]
            {
              new SqlParameter("@CampNo",  campNo),
            };
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetCampExit", parm);
        return dt;
    }
    public DataTable GridBindLearningMasterCamp()
    {
        DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "GetBindLearningMasterCamp");
        return dt;
    }
    public int InsertReportAudittrail(string MenuName, string ReportName, string UserID)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
        {
            new SqlParameter("MenuName", @MenuName),
                new SqlParameter("@ReportName", @ReportName),
                                new SqlParameter("@UserID", @UserID),
        };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertReportAudittrail", cmdParameters);
    }
    public DataTable Get_DataFor8Filter(string ProcedureName, string Filter1, string Filter2)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@FormID",Filter1),
                          new SqlParameter("@Pid",Filter2),



                    };
            dtcombo = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcedureName, paramvT);

        }
        catch (Exception)
        {
            throw;
        }
        return dtcombo;
    }
    public int CopyFormQuestion(int Assessment, int QuestionCategory, int QuestionID, int FormID, int Sequence, DataTable tblQuestionbank)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@Assessment", Assessment),
            new SqlParameter("@QuestionCategory ", QuestionCategory),
            new SqlParameter("@QuestionID", QuestionID),
            new SqlParameter("@FormID ", FormID),
            new SqlParameter("@Sequence",Sequence),
            new SqlParameter("@Tbl_Training_Ques", tblQuestionbank)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_InsertQuestion]", cmdParameters);

    }

    public int DeleteFormQuestion(int Assessment, int QuestionCategory, int QuestionID, int FormID)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@Assessment", Assessment),
            new SqlParameter("@QuestionCategory ", QuestionCategory),
            new SqlParameter("@QuestionID", QuestionID),
            new SqlParameter("@FormID ", FormID)

    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_DeleteQuestion]", cmdParameters);

    }
    public int Insert_participate(int FormID, DataTable tbl_Tarining_Participarticipate)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@FormID", FormID),
            new SqlParameter("@tbl_Tarining_Participarticipate", tbl_Tarining_Participarticipate)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_Participarticipate]", cmdParameters);

    }
    public int Insert_participateStatff(int SchedulerID, DataTable tbl_Tarining_Participarticipate)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@SchedulerID", SchedulerID),
            new SqlParameter("@tbl_Tarining_Participarticipate", tbl_Tarining_Participarticipate)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_StaffParticiparticipateinsert]", cmdParameters);

    }
    public int Insert_participateSh(int FormID, DataTable tbl_Tarining_Participarticipate)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@FormID", FormID),
            new SqlParameter("@tbl_Tarining_Participarticipate", tbl_Tarining_Participarticipate)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_Participarticipate2026]", cmdParameters);

    }


    public int Insert_EntryDone(int FormID, DataTable tbl_EntryDone)
    {
        DataTable dtcombo = new DataTable();

        SqlParameter[] cmdParameters = new SqlParameter[]
    {
            new SqlParameter("@FormID", FormID),
            new SqlParameter("@Tbl_EntryDoneBy", tbl_EntryDone)
    };
        return SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[USP_EntryDoneBy]", cmdParameters);

    }
    public int SaveDataSchool(string VillageCode, string SchoolCode, string SchoolCodeID, string Name, string Status, string Createdate, string CreateBy, string sysFlag, string DISECode, string SchoolLevel, string Govt_DiseCode)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@SchoolCodeID", SchoolCodeID),
            new SqlParameter("@Name", Name),
            new SqlParameter("@Status", Status),
            new SqlParameter("@Createdate", Createdate),
            new SqlParameter("@CreateBy", CreateBy),
            new SqlParameter("@sysFlag", sysFlag),
            new SqlParameter("@DISECode", DISECode),
            new SqlParameter("@SchoolLevel", SchoolLevel),
                  new SqlParameter("@Govt_DiseCode", Govt_DiseCode),
            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateSchoolData", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public int SaveDataD2d(string UniqueChildCode, string UniqueCode, string VillageCode, string Serial, string SocialCategory, string ChildName, string FathersName, string Gender, string DOBAvailable, string DOB, string AgeAson, string School, string EnrolmentCategory, string HHNo, string DoChild, string SWType, string Status, string AsOnDate, string Createdate, string CreateBy, string SurvayDate, string EnrollCode, string EnrollStatus, string DeleteFlag)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueChildCode", UniqueChildCode),
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Serial", Serial),
            new SqlParameter("@SocialCategory", SocialCategory),
            new SqlParameter("@ChildName", ChildName),
            new SqlParameter("@FathersName", FathersName),
            new SqlParameter("@Gender", Gender),
            new SqlParameter("@DOBAvailable", DOBAvailable),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@AgeAson", AgeAson),
            new SqlParameter("@School", School),
            new SqlParameter("@EnrolmentCategory", EnrolmentCategory),
            new SqlParameter("@HHNo", HHNo),
            new SqlParameter("@DoChild", DoChild),
            new SqlParameter("@SWType", SWType),
            new SqlParameter("@Status", Status),
           new SqlParameter("@AsOnDate", AsOnDate),
            new SqlParameter("@Createdate", Createdate),
             new SqlParameter("@CreateBy", CreateBy),
            new SqlParameter("@SurvayDate", SurvayDate),
            new SqlParameter("@EnrollCode", EnrollCode),
            new SqlParameter("@EnrollStatus", EnrollStatus),
            new SqlParameter("@DeleteFlag", DeleteFlag),




            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateD2dData", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public int SaveDataEnrolment(string UniqueChildCode, string EnTBcode, string TBFC, string MotherName, string ChildCode, string VillageCode, string Serial, string Category, string Class, string Session, string ChildName, string FatherName, string ChildNameH, string FatherNameH, string Gender, string SchoolCode, string EnrolmentDateTime, string DOBAvailable, string DOB, string AgeAson, string AsOnDateTime, string Status, string CreateDateTime, string CreateBy, string HouseNo, string DeleteFlag, string VillagenameOther, string SamgraID, string IsDoBoFlag, string IsComplete, string ActivityDateTime, string EnrolmentMatching, string SysDateTime, string remark, string Flag)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueChildCode", UniqueChildCode),
            new SqlParameter("@EnTBcode", EnTBcode),
            new SqlParameter("@TBFC", TBFC),
            new SqlParameter("@MotherName", MotherName),
            new SqlParameter("@ChildCode", ChildCode),
            new SqlParameter("@VillageCode", VillageCode),
            new SqlParameter("@Serial", Serial),
            new SqlParameter("@Category", Category),
            new SqlParameter("@Class", Class),
            new SqlParameter("@Session", Session),
                  new SqlParameter("@ChildName", ChildName),
            new SqlParameter("@FatherName", FatherName),
            new SqlParameter("@ChildNameH", ChildNameH),
            new SqlParameter("@FatherNameH", FatherNameH),
            new SqlParameter("@Gender", Gender),
            new SqlParameter("@SchoolCode", SchoolCode),
            new SqlParameter("@EnrolmentDate", EnrolmentDateTime),
            new SqlParameter("@DOBAvailable", DOBAvailable),
            new SqlParameter("@DOB", DOB),
            new SqlParameter("@AgeAson", AgeAson),
            new SqlParameter("@AsOnDate", AsOnDateTime),
            new SqlParameter("@Status", Status),
            new SqlParameter("@CreateDate", CreateDateTime),
            new SqlParameter("@CreateBy", CreateBy),
            new SqlParameter("@HouseNo", HouseNo),
            new SqlParameter("@DeleteFlag", DeleteFlag),
            new SqlParameter("@VillagenameOther", VillagenameOther),
            new SqlParameter("@SamgraID", SamgraID),
                new SqlParameter("@IsDoBoFlag", IsDoBoFlag),

                new SqlParameter("@IsComplete", IsComplete),
                new SqlParameter("@ActivityDate", ActivityDateTime),
                  new SqlParameter("@EnrolmentMatching", EnrolmentMatching),
                 new SqlParameter("@SysDate", SysDateTime),
 new SqlParameter("@remark", remark),
 new SqlParameter("@Flag",  Flag),




            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateENrolmentData", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public int SaveDataInsertUpdateTeamBalikTraing(string UniqueCode, string Learningtype, string TrainingMode, string TrainingType, string DistCode, string BlockCode, string FromDate, string ToDate, string Status, string Description, string Createby, string Flag)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@Learningtype", Learningtype),
            new SqlParameter("@TrainingMode", TrainingMode),
            new SqlParameter("@TrainingType", TrainingType),
            new SqlParameter("@DistCode", DistCode),
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@FromDate", FromDate),
            new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@Status ", Status ),
            new SqlParameter("@Description ", Description ),
            new SqlParameter("@Createby", @Createby),
               new SqlParameter("@Flag", @Flag),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikTraing", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int SaveDataInsertUpdateStaffUniqueCode(string StaffUniqueCode, string StaffId)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@StaffUniqueCode", StaffUniqueCode),
            new SqlParameter("@StaffId", StaffId),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffUniqueCode", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int SaveDataInsertUpdateStaffTraing2023(string StaffUniqueCode, string ToDate, string TrainingDay)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@ScheduleID", StaffUniqueCode),
            new SqlParameter("@ToDate", ToDate),
              new SqlParameter("@TrainingDay", TrainingDay),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraing2023", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }


    public int SaveDataInsertUpdateTraning2023(string StaffUniqueCode, string StaffId)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@StaffUniqueCode", StaffUniqueCode),
            new SqlParameter("@StaffId", StaffId),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTraning2023", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int InsertUpdateStaffTraningMain(string UniqueCode, string Learningtype, string TrainingMode, string TrainingType, string DistCode, string BlockCode, string FromDate, string ToDate, string Status, string Description, string Createby, string Type, string SchedueID, string TrainerName, string Email, string Contact, string InternalTrainername)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@Learningtype", Learningtype),
                 new SqlParameter("@TrainingMode", TrainingMode),
                              new SqlParameter("@TrainingType", TrainingType),
            new SqlParameter("@DistCode", DistCode),
             new SqlParameter("@BlockCode", BlockCode),
             new SqlParameter("@FromDate", FromDate),
             new SqlParameter("@ToDate", ToDate),
             new SqlParameter("@Status", Status),
                            new SqlParameter("@Description", Description),
                new SqlParameter("@Createby", Createby),
                   new SqlParameter("@Type", Type),
                      new SqlParameter("@SchedueID", SchedueID),
                         new SqlParameter("@TrainerName", TrainerName),
               new SqlParameter("@Email", Email),
                new SqlParameter("@Contact", Contact),

                 new SqlParameter("@InternalTrainername", InternalTrainername),


            };


            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningMain", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int InsertUpdateStaffTraningMainDetails(string TBUniqueCode, string TBID, string TotalDay, string Adate1, string Adate2, string Adate3, string Adate4, string Adate5, string Adate6, string Adate7, string Name, string UserType)
    {
        int Icount = 0;
        try
        {
            SqlParameter[] cmdParameters = new SqlParameter[]
  {
            new SqlParameter("@TBUniqueCode", TBUniqueCode),
            new SqlParameter("@TBID", TBID),
            new SqlParameter("@TotalDay", TotalDay),
                 new SqlParameter("@Adate1", Adate1),
            new SqlParameter("@Adate2", Adate2),
             new SqlParameter("@Adate3", Adate3),
             new SqlParameter("@Adate4", Adate4),
             new SqlParameter("@Adate5", Adate5),
             new SqlParameter("@Adate6", Adate6),
                new SqlParameter("@Adate7", Adate7),
                   new SqlParameter("@Name", Name),
                      new SqlParameter("@UserType", UserType),



  };


            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningDetails", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public int InsertUpdateStaffTraningDelete(string TBCode, string TrainUserdID, string ModifyBy, string txtDesc)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@TBCode", TBCode),
            new SqlParameter("@TrainUserdID", TrainUserdID),
            new SqlParameter("@ModifyBy", ModifyBy),
            new SqlParameter("@txtDesc", txtDesc),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateStaffTraningDelete", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int DeleteAssment(int Tarining_ID)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Tarining_ID", Tarining_ID),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteAssment", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int InsertUpdateAssment(string Assessment, string QuestionID, string FormID, string Sequence)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@Assessment", Assessment),
            new SqlParameter("@QuestionID", QuestionID),
            new SqlParameter("@FormID", FormID),
            new SqlParameter("@Sequence", Sequence),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatAssment", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int DeleteAssmentQuestion(string FormID, string ParticiparticipateName)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@FormID", FormID),

              new SqlParameter("@ParticiparticipateName", ParticiparticipateName),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeleteAssmentQuestion", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int InsertUpdatedonormaster2023(string DID, string BlockCode)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@DID", DID),

              new SqlParameter("@BlockCode", BlockCode),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatedonormaster2023", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int InsertUpdatedonormasterDistrict2023(string DID, string DistrictCode)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@DID", DID),

              new SqlParameter("@DistrictCode", DistrictCode),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatedonormasterDistrict2023", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int InsertUpdatedonormasterDeatils2023(string OID, string OSID, string OSubID)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@OID", OID),

              new SqlParameter("@OSID", OSID),
                 new SqlParameter("@OSubID", OSubID),


            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdatedonormasterDeatils2023", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public int DeletedonormasterDistrict2023(string OID)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@OID", OID),




            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "DeletedonormasterDistrict2023", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }

    public int insertUpdateSealSen(string SamgraID, string Category, string Class, string Serial, string ChildName, string FatherName, string Gender, string EnrolmentDate, string DOBAvailable, string DOB, string AgeAson, string AsOnDate, string ModifyBy, string HouseNo, string SealSenReject, string UniqueChildCode)
    {
        int Icount = 0;
        try
        {


            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@SamgraID", SamgraID),

              new SqlParameter("@Category", Category),
                 new SqlParameter("@Class", Class),
                   new SqlParameter("@Serial", Serial),
                     new SqlParameter("@ChildName", ChildName),
                       new SqlParameter("@FatherName", FatherName),
                         new SqlParameter("@Gender", Gender),
                           new SqlParameter("@EnrolmentDate", EnrolmentDate),
                           new SqlParameter("@DOBAvailable", DOBAvailable),
                             new SqlParameter("@DOB", DOB),
                               new SqlParameter("@AgeAson", AgeAson),
                                   new SqlParameter("@AsOnDate", AsOnDate),
                                       new SqlParameter("@ModifyBy", ModifyBy),
                                           new SqlParameter("@HouseNo", HouseNo),
                                               new SqlParameter("@SealSenReject", SealSenReject),
                                                new SqlParameter("@UniqueChildCode", UniqueChildCode),

            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "insertUpdateSealSen", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public DataTable StaffEntryQuery(string Fliter, string Fliter1, string Fliter2, string Flag)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@Fliter",  Fliter),
               new SqlParameter("@Fliter1",  Fliter1),
                new SqlParameter("@Fliter2",  Fliter2),
                 new SqlParameter("@Flag",  Flag),
               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEntryQuery", parm);

        }
        catch
        {
            throw;
        }
        return dt;
    }
    public DataTable LoadEmployee(string StateCode, string StateName, string DistCode, string DistName)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@StateCode",  StateCode),
               new SqlParameter("@StateName",  StateName),
                new SqlParameter("@DistCode",  DistCode),
                 new SqlParameter("@DistName",  DistName),
               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "LoadEmployee", parm);

        }
        catch
        {
            throw;
        }
        return dt;
    }
    public DataTable rptEntryTraingDeltailQuery(string Fliter)
    {
        DataTable dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@Fliter",  Fliter),

               };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptEntryTraingDeltailQuery", parm);

        }
        catch
        {
            throw;
        }
        return dt;
    }
    public void ReportDownload(string Rname, string ModuleName, string UserName)
    {
        SqlParameter[] cmdParameters = new SqlParameter[]
         {
        new SqlParameter("@fname", Rname),
            new SqlParameter("@Username", UserName),
            new SqlParameter("@ModuleName", ModuleName),


       };
        int icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[InsertDownloadReport2023]", cmdParameters);
    }
    public int SaveDataInsertUpdateTeamBalikTraing(string UniqueCode, string Learningtype, string TrainingMode, string TrainingType, string DistCode, string BlockCode, string FromDate, string ToDate, string Status, string Description, string Createby, string Flag, string Email, string Contact, string Location, string TypeID, string Name)
    {
        int Icount = 0;
        try
        {

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
            new SqlParameter("@UniqueCode", UniqueCode),
            new SqlParameter("@Learningtype", Learningtype),
            new SqlParameter("@TrainingMode", TrainingMode),
            new SqlParameter("@TrainingType", TrainingType),
            new SqlParameter("@DistCode", DistCode),
            new SqlParameter("@BlockCode", BlockCode),
            new SqlParameter("@FromDate", FromDate),
            new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@Status ", Status ),
            new SqlParameter("@Description ", Description ),
            new SqlParameter("@Createby", @Createby),
               new SqlParameter("@Flag", @Flag),
                 new SqlParameter("@Email", @Email),
                   new SqlParameter("@Contact", @Contact),
                       new SqlParameter("@Location", @Location),
                        new SqlParameter("@TypeID", TypeID),
                         new SqlParameter("@Name", Name),



            };
            Icount = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertUpdateTeamBalikTraing2023New", cmdParameters);
        }
        catch
        {
            throw;
        }
        return Icount;
    }
    public DataSet ArchiveCheck(string Moduel, int Myear, string UserName)
    {
        DataSet dt = null;
        try
        {

            SqlParameter[] parm = new SqlParameter[]
               {
              new SqlParameter("@Moduel",  Moduel),
               new SqlParameter("@Year",  Myear),
                 new SqlParameter("@UserName",  UserName),

               };
            dt = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptArchiveCheck", parm);

        }
        catch
        {
            throw;
        }
        return dt;
    }

}


