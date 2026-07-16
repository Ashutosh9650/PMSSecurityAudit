using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public class CommanData
{


    /// <summary>
    ///*************************************************** Common Select Procedure***************************
    /// create proc [dbo].[Get_Select_Table_Data_Common]
    ///@TableName varchar(max)
    ///,@Condition varchar(max)
    ///,@OrderbyvalueMem varchar(max)
    ///,@sortbycondi varchar(max)
    ///,@FieldName varchar(max)
    ///as
    ///begin
    ///DECLARE @SQLString NVARCHAR(MAX)
    ///SET @SQLString='select '+@FieldName+' from '+@TableName + ''+@Condition+''+@OrderbyvalueMem+''+@sortbycondi+''
    ///EXEC (@SQLString)
    ///end
    /// </summary>


    public CommanData()
    {
    }



    public string Set_GridColor(string sPerVal)
    {
        string colr = "";
        int PerVal = Convert.ToInt32(sPerVal == "" ? "0" : sPerVal);
        if (PerVal == 0)
        {
            colr = "#93c8f0";
        }
        else if (PerVal >= 0 && PerVal <= 25)
        {
            colr = "#72bbf3";
        }
        else if (PerVal > 25 && PerVal <= 50)
        {
            colr = "#43a7f3";
        }
        else if (PerVal > 50 && PerVal <= 75)
        {
            colr = "#4398d7";
        }
        else if (PerVal >= 75 && PerVal <= 95)
        {
            colr = "#238fe0";
        }
        else if (PerVal >= 95 && PerVal <= 100)
        {
            colr = "#327ab1";
        }
        return colr;
    }
    public static string Set_GridColors(string sPerVal)
    {
        string colr = "";
        int PerVal = Convert.ToInt32(sPerVal == "" ? "0" : sPerVal);
        if (PerVal == 0)
        {
            colr = "#93c8f0";
        }
        else if (PerVal >= 0 && PerVal <= 25)
        {
            colr = "#72bbf3";
        }
        else if (PerVal > 25 && PerVal <= 50)
        {
            colr = "#43a7f3";
        }
        else if (PerVal > 50 && PerVal <= 75)
        {
            colr = "#4398d7";
        }
        else if (PerVal >= 75 && PerVal <= 95)
        {
            colr = "#238fe0";
        }
        else if (PerVal >= 95 && PerVal <= 100)
        {
            colr = "#327ab1";
        }
        return colr;
    }


    public DataSet GetDataFormat_DataSet(string ProcName)
    {
        string lng = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        string str = (lng == "" ? "1" : lng);
        SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("LanguageID", str),
        };
        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcName, pr);

        return ds;
    }

    public string GetDataFormat_DeptsNames(DataSet ds)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("");
        sb.Append("<div class='bg-grey' style='line-height: 28px;font-size: 14px !important;color: #333;'>");
        string lng = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        string str = "LanguageID=" + (lng == "" ? "1" : lng);
        if (str == "1")
        {
            sb.Append("<div class='col-lg-2 col-md-2 col-sm-2 col-xs-12' style='border-right: solid 1px #ddd;'><b>Data Sources </b></div>");
        }
        else if (str == "2")
        {
            sb.Append("<div class='col-lg-2 col-md-2 col-sm-2 col-xs-12' style='border-right: solid 1px #ddd;'><b>ಡೇಟಾ ಮೂಲಗಳು </b></div>");
        }
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            if (ds.Tables[i].Rows.Count > 0)
            {
                sb.Append("<div class='col-lg-5 col-md-6 col-sm-12 col-xs-12 form-group-1'><i class='fa fa-hand-o-right m-r-5'></i>");
                sb.Append("<span class=' f-13'>" + Convert.ToString(ds.Tables[i].Rows[0][1]) + " : " + Convert.ToString(ds.Tables[i].Rows[0][2]) + "</span></div>");
            }
        }
        sb.Append("</div>");
        return sb.ToString();

    }

    public string Graph_Dynaic_Colors(string PID, out string ChartColors, out string DataAvilable, out string KarifZaid, out string RFColor)
    {
        string Condition = "";
        ChartColors = "";
        DataAvilable = "";
        KarifZaid = "";
        RFColor = "";
        SqlParameter[] pc = new SqlParameter[] {
            new SqlParameter("PID", PID)
            };

        DataTable dtc = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_DashboardMain_Details_ChartColors", pc);
        if (dtc.Rows.Count > 0)
        {
            ChartColors = Convert.ToString(dtc.Rows[0][0]);
            DataAvilable = Convert.ToString(dtc.Rows[0][1]);
            KarifZaid = Convert.ToString(dtc.Rows[0][2]);
            RFColor = Convert.ToString(dtc.Rows[0][3]);
        }
        return Condition;
    }


    public void Download_Status_report(string UserID, string MenuID, string Downloads, string YearFrom, string YearTo, string Version)
    {
        try
        {
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("MenuID",MenuID),
                new SqlParameter("UserID",UserID),
                new SqlParameter("Downloads",Downloads),
                new SqlParameter("YearFrom",YearFrom),
                new SqlParameter("YearTo",YearTo),
                   new SqlParameter("Version",Version)

            };
            int re = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Download_Status_Report", pr);
        }
        catch //(Exception ex)
        {

            throw;
        }
    }
    public void Insertmstvisitorlog(string UserID, string MenuID, string IPAddress)
    {
        try
        {
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("MenuID",MenuID),
                new SqlParameter("UserID",UserID),
              new SqlParameter("IPAddress",IPAddress),

            };
            int re = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Insertmstvisitorlog", pr);
        }
        catch
        {
            throw;
        }
    }
    public void InsertmstvisitorlogNew(string UserID, string MenuID, string IPAddress, string SessionID)
    {
        try
        {
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("MenuID",MenuID),
                new SqlParameter("UserID",UserID),
              new SqlParameter("IPAddress",IPAddress),
                new SqlParameter("SessionID",SessionID),

            };
            int re = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertmstvisitorlogNew", pr);

        }
        catch
        {
            throw;
        }
    }


    public int Insertmstvisitorlog2023(string UserID, string MenuID, string IPAddress, string SessionID)
    {
        try
        {
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("MenuID",MenuID),
                new SqlParameter("UserID",UserID),
              new SqlParameter("IPAddress",IPAddress),
                new SqlParameter("SessionID",SessionID),

            };
            object ore = SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "InsertmstvisitorlogNew2023", pr);
            return Convert.ToInt32(ore);
        }

        catch
        {
            return 0;
            throw;
        }
    }

    public int InsertErrorLog(string MenuID, string Errror)
    {
        try
        {
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("MenuID",MenuID),
                new SqlParameter("Error",Errror),


            };
            object ore = SqlHelper.ExecuteScaler(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "insertUdateErrorLog", pr);
            return Convert.ToInt32(ore);
        }

        catch
        {
            return 0;
            throw;
        }
    }
    public DataTable UniCode_GetData(string LangID, string PageID)
    {
        DataTable dt = new DataTable();
        try
        {
            LangID = LangID == "" ? "1" : LangID;
            SqlParameter[] p = new SqlParameter[] {
        new SqlParameter("Menu_ID",PageID),
         new SqlParameter("LanguageID",LangID)
        };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Unicode_FieldName_Getdate", p);
        }
        catch
        {
            throw;
        }
        return dt;
    }
    public string Get_Disclaimer()
    {
        string disstr = "<b>Disclaimer:</b> Though all efforts have been made to ensure the accuracy and currency of the content, the same should not be construed as a statement of law or used for any legal purposes. In case of any ambiguity or doubts, users are advised to verify/check with the Department(s) and/or other source(s), and to obtain appropriate professional advice.";

        return disstr;
    }
    public string Get_Disclaimer(Label labelid)
    {
        string disstr = "";
        disstr = "<b>Disclaimer:</b> Though all efforts have been made to ensure the accuracy and currency of the content, the same should not be construed as a statement of law or used for any legal purposes. In case of any ambiguity or doubts, users are advised to verify/check with the Department(s) and/or other source(s), and to obtain appropriate professional advice.";
        return labelid.Text = disstr;
    }
    public string Get_Disclaimer(Label labelid, string lagID)
    {
        string disstr = "";
        if (lagID == "1")
        {
            disstr = "<b>Disclaimer:</b> Though all efforts have been made to ensure the accuracy and currency of the content, the same should not be construed as a statement of law or used for any legal purposes. In case of any ambiguity or doubts, users are advised to verify/check with the Department(s) and/or other source(s), and to obtain appropriate professional advice.";
        }
        if (lagID == "2")
        {
            disstr = "<b>Disclaimer:</b> Though all efforts have been made to ensure the accuracy and currency of the content, the same should not be construed as a statement of law or used for any legal purposes. In case of any ambiguity or doubts, users are advised to verify/check with the Department(s) and/or other source(s), and to obtain appropriate professional advice.";
        }
        //return disstr;
        return labelid.Text = disstr;
    }
}

public class CommanDB
{


    /// <summary>
    ///*************************************************** Common Select Procedure***************************
    /// create proc [dbo].[Get_Select_Table_Data_Common]
    ///@TableName varchar(max)
    ///,@Condition varchar(max)
    ///,@OrderbyvalueMem varchar(max)
    ///,@sortbycondi varchar(max)
    ///,@FieldName varchar(max)
    ///as
    ///begin
    ///DECLARE @SQLString NVARCHAR(MAX)
    ///SET @SQLString='select '+@FieldName+' from '+@TableName + ''+@Condition+''+@OrderbyvalueMem+''+@sortbycondi+''
    ///EXEC (@SQLString)
    ///end
    /// </summary>





    public static string Set_GridColor(string sPerVal)
    {
        string colr = "";
        int PerVal = Convert.ToInt32(sPerVal == "" ? "0" : sPerVal);
        if (PerVal == 0)
        {
            colr = "#93c8f0";
        }
        else if (PerVal >= 0 && PerVal <= 25)
        {
            colr = "#72bbf3";
        }
        else if (PerVal > 25 && PerVal <= 50)
        {
            colr = "#43a7f3";
        }
        else if (PerVal > 50 && PerVal <= 75)
        {
            colr = "#4398d7";
        }
        else if (PerVal >= 75 && PerVal <= 95)
        {
            colr = "#238fe0";
        }
        else if (PerVal >= 95 && PerVal <= 100)
        {
            colr = "#327ab1";
        }
        return colr;
    }
    public static DataSet GetDataFormat_DataSet(string ProcName)
    {
        string lng = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        string str = "LanguageID=" + (lng == "" ? "1" : lng);
        SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("LanguageID", str),
        };
        DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcName, pr);

        return ds;
    }
    public static string GetDataFormat_DeptsNames(DataSet ds)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("");
        sb.Append("<div class='bg-grey' style='line-height: 28px;font-size: 14px !important;color: #333;'>");
        string lng = Convert.ToString(HttpContext.Current.Session["SessLangID"]);
        string str = "LanguageID=" + (lng == "" ? "1" : lng);
        if (str == "1")
        {
            sb.Append("<div class='col-lg-2 col-md-2 col-sm-2 col-xs-12' style='border-right: solid 1px #ddd;'><b>Data Sources </b></div>");
        }
        else if (str == "2")
        {
            sb.Append("<div class='col-lg-2 col-md-2 col-sm-2 col-xs-12' style='border-right: solid 1px #ddd;'><b>ಡೇಟಾ ಮೂಲಗಳು </b></div>");
        }
        for (int i = 0; i < ds.Tables.Count; i++)
        {
            sb.Append("<div class='col-lg-5 col-md-6 col-sm-12 col-xs-12 form-group-1'><i class='fa fa-hand-o-right m-r-5'></i>");
            sb.Append("<span class=' f-13'>" + Convert.ToString(ds.Tables[i].Rows[0][1]) + " : " + Convert.ToString(ds.Tables[i].Rows[0][2]) + "</span></div>");
        }
        sb.Append("</div>");
        return sb.ToString();

    }
    public static string Graph_Dynaic_Colors(string PID, out string ChartColors, out string DataAvilable, out string KarifZaid, out string RFColor)
    {
        string Condition = "";
        ChartColors = "";
        DataAvilable = "";
        KarifZaid = "";
        RFColor = "";
        SqlParameter[] pc = new SqlParameter[] {
            new SqlParameter("PID", PID)
            };

        DataTable dtc = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_DashboardMain_Details_ChartColors", pc);
        if (dtc.Rows.Count > 0)
        {
            ChartColors = Convert.ToString(dtc.Rows[0][0]);
            DataAvilable = Convert.ToString(dtc.Rows[0][1]);
            KarifZaid = Convert.ToString(dtc.Rows[0][2]);
            RFColor = Convert.ToString(dtc.Rows[0][3]);
        }
        return Condition;
    }

    public static void Download_Status_report(string UserID, string MenuID, string Downloads, string YearFrom, string YearTo, string Version)
    {
        try
        {
            SqlParameter[] pr = new SqlParameter[] {
                new SqlParameter("MenuID",MenuID),
                new SqlParameter("UserID",UserID),
                new SqlParameter("Downloads",Downloads),
                new SqlParameter("YearFrom",YearFrom),
                new SqlParameter("YearTo",YearTo),
                   new SqlParameter("Version",Version)

            };
            int re = SqlHelper.ExecuteNonQuery(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Download_Status_Report", pr);
        }
        catch
        {
            throw;
        }
    }
    public static DataTable UniCode_GetData(string LangID, string PageID)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] p = new SqlParameter[] {
        new SqlParameter("Menu_ID",PageID),
         new SqlParameter("LanguageID",LangID)
        };

            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "SP_Unicode_FieldName_Getdate", p);
        }
        catch
        {
            throw;
        }
        return dt;
    }


}

public static class CommonsDBFn
{
    #region ************** Amit
    public partial class GW_General
    {
        public string GUID { get; set; }
        public string WellNo { get; set; }
        public string Well_Type { get; set; }
        public string Agency { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Block_Taluk { get; set; }
        public string GP_Mandal { get; set; }
        public string Village { get; set; }
        public string Hamlet { get; set; }
        public string Owner { get; set; }
        public string Additional_Inf { get; set; }
        public string Basin { get; set; }
        public string Sub_Basin { get; set; }
        public string Minor_Basin { get; set; }
        public string Geology { get; set; }
        public string Geomorphology { get; set; }
        public string Toposheet_No { get; set; }
        public Nullable<byte> LatD { get; set; }
        public Nullable<byte> LatM { get; set; }
        public Nullable<float> LatS { get; set; }
        public Nullable<byte> LonD { get; set; }
        public Nullable<byte> LonM { get; set; }
        public Nullable<float> LonS { get; set; }
        public Nullable<float> Easting { get; set; }
        public Nullable<float> Northing { get; set; }
        public string Method { get; set; }
        public string Well_use { get; set; }
        public string SW_Influence { get; set; }
        public string Local_Morphology { get; set; }
        public bool Command_Area { get; set; }
        public string Command_name { get; set; }
        public string Well_Location { get; set; }
        public Nullable<float> MP { get; set; }
        public Nullable<double> RL { get; set; }
        public bool DWLR_installed { get; set; }
        public string DWLR_No { get; set; }
        public string DWLR_type { get; set; }
        public string stratigraphy { get; set; }
        public string RF_Stn_Name { get; set; }
        public Nullable<System.DateTime> MonitorFrom { get; set; }
        public Nullable<System.DateTime> MonitorTo { get; set; }
        public string Land_Use { get; set; }
        public string WQ_Mon_Type { get; set; }
        public string WQ_Issue { get; set; }
        public Nullable<float> Abstraction { get; set; }
        public string Lifting_Device { get; set; }
        public string PlatformType { get; set; }
        public Nullable<System.DateTime> PlatformDt { get; set; }
        public string Category { get; set; }
        public Nullable<float> Well_Depth { get; set; }
        public Nullable<float> IntakeDepth { get; set; }
        public bool Select { get; set; }
        public bool Filter { get; set; }
        public Nullable<byte> Mark { get; set; }
        public bool Repl { get; set; }
        public string OriWell { get; set; }
        public string AqTap { get; set; }
        public bool Urban { get; set; }
        public Nullable<bool> IsUploaded { get; set; }
        public Nullable<bool> IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
        public string GeoLoc { get; set; }
        public string DataLoggerID { get; set; }
    }
    public partial class Well_Lithology
    {
        public string GUID { get; set; }
        public string WellNo { get; set; }
        public Nullable<double> DepthTo { get; set; }
        public string LyrId { get; set; }
        public string Lithology { get; set; }
        public string Colour { get; set; }
        public string Texture { get; set; }
        public string Shape { get; set; }
        public Nullable<float> Drill_Time { get; set; }
        public string Sub_Lithology { get; set; }
        public string Sub_Colour { get; set; }
        public string Sub_Texture { get; set; }
        public string Sub_Shape { get; set; }
        public Nullable<short> Sub_Percent { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Log_Detail
    {
        public string GUID { get; set; }
        public string wellno { get; set; }
        public Nullable<float> depth { get; set; }
        public Nullable<float> SP { get; set; }
        public Nullable<float> SNRes { get; set; }
        public Nullable<float> LNRes { get; set; }
        public Nullable<float> SPR { get; set; }
        public Nullable<float> Gamma { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Water_Bearing_Zones
    {

        public string Wellno { get; set; }
        public Nullable<float> wbzone_from { get; set; }
        public Nullable<float> wbzone_to { get; set; }
        public string Description { get; set; }
        public Nullable<float> Discharge { get; set; }

    }
    public partial class Log_General
    {
        public string GUID { get; set; }
        public string Wellno { get; set; }
        public Nullable<float> Dia { get; set; }
        public string Logger { get; set; }
        public Nullable<System.DateTime> LogDate { get; set; }
        public Nullable<float> Mudres { get; set; }
        public Nullable<float> Mudtemp { get; set; }
        public string ResUnit { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Well_Assembly
    {
        public string GUID { get; set; }
        public string Wellno { get; set; }
        public Nullable<float> AssemblyFrom { get; set; }
        public Nullable<float> AssemblyTo { get; set; }
        public string Casing_Screen { get; set; }
        public Nullable<float> AssemblyDia { get; set; }
        public string AssemblyMaterial { get; set; }
        public string AssemblyType { get; set; }
        public Nullable<float> OpenArea { get; set; }
        public Nullable<float> SlotSize { get; set; }
        public Nullable<float> Schedule { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Well_Details
    {
        public string GUID { get; set; }
        public string WellNo { get; set; }
        public Nullable<float> Well_Length { get; set; }
        public Nullable<float> Well_Breadth { get; set; }
        public Nullable<float> Well_Diameter { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Seal_and_Grout
    {
        public string GUID { get; set; }
        public string Wellno { get; set; }
        public string Seal_or_grout { get; set; }
        public string Material { get; set; }
        public Nullable<float> SGFrom { get; set; }
        public Nullable<float> SGTo { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Well_pilot_hole
    {
        public string GUID { get; set; }
        public string wellno { get; set; }
        public Nullable<float> pilot_from { get; set; }
        public Nullable<float> pilot_to { get; set; }
        public Nullable<float> pilot_Dia { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Well_reaming
    {
        public string GUID { get; set; }
        public string wellno { get; set; }
        public Nullable<float> ream_from { get; set; }
        public Nullable<float> ream_to { get; set; }
        public Nullable<float> ream_Dia { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    public partial class Gravel_Packing
    {
        public string GUID { get; set; }
        public string Wellno { get; set; }
        public Nullable<float> Grav_from { get; set; }
        public Nullable<float> Grav_to { get; set; }
        public Nullable<float> Size_from { get; set; }
        public Nullable<float> Size_to { get; set; }
        public bool IsUploaded { get; set; }
        public bool IsEdited { get; set; }
        public Nullable<System.DateTime> UploadedOn { get; set; }
    }
    #endregion
    public static object Date_HifanToHifan(string ddmmyyTOmmddyy)
    {
        object dtstr = "";
        if (ddmmyyTOmmddyy.Trim() != "")
        {
            object[] dtyp;
            if (ddmmyyTOmmddyy.Contains("/"))
                dtyp = ddmmyyTOmmddyy.Split('/');
            else
                dtyp = ddmmyyTOmmddyy.Split('-');

            if (dtyp.Length > 2)
            {
                dtstr = dtyp[1] + "/" + dtyp[0] + "/" + dtyp[2];
            }
        }
        else
        {
            dtstr = DBNull.Value;
        }
        return dtstr;
    }

    public static DataTable HydroGraphTable(DateTime datefrom, DateTime dateto, string month1, string month2, string WellNo, string Flags, string WQCont, string ProcName)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlParameter[] prms = new SqlParameter[]
                  {
                    new SqlParameter("datestart", CommonsDBFn.Date_SlaceToHifan(datefrom)),
                    new SqlParameter("dateend",CommonsDBFn.Date_SlaceToHifan(dateto)),
                    new SqlParameter("month1", CommonsDBFn.Null_Dbnull(month1)),
                    new SqlParameter("month2", CommonsDBFn.Null_Dbnull(month2)),
                    new SqlParameter("wellno", CommonsDBFn.Null_Dbnull(WellNo)),
                    new SqlParameter("flag",CommonsDBFn.Null_Dbnull(Flags)),
                    new SqlParameter("WQCont",CommonsDBFn.Null_Dbnull(WQCont))
                  };
            dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, ProcName, prms);
        }
        catch
        {
            // string msg = ex.Message;
            throw;
        }

        return dt;
    }
    public static string Date_SlaceToHifan(DateTime dtD)
    {
        string dtstr = "", dd = dtD.Day > 9 ? dtD.Day.ToString() : "0" + dtD.Day.ToString()
          , mm = dtD.Month > 9 ? dtD.Month.ToString() : "0" + dtD.Month.ToString()
          , yy = dtD.Year > 9 ? dtD.Year.ToString() : "0" + dtD.Year.ToString();
        dtstr = yy + "-" + mm + "-" + dd;
        return dtstr;
    }
    public static object Null_Dbnull(object objval)
    {
        //return (objval == null ? DBNull.Value : objval);
        if (Convert.ToString(objval) == "" || Convert.ToString(objval) == "0" || objval == null)
            return DBNull.Value;
        else
            return objval;
    }
    public static List<TSource> DataTable_ToList<TSource>(this DataTable dataTable) where TSource : new()
    {
        var dataList = new List<TSource>();
        if (dataTable != null)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                                 select new
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
                         aProp.PropertyType
                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
                                     select new
                                     {
                                         Name = aHeader.ColumnName,
                                         Type = aHeader.DataType
                                     }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var aTSource = new TSource();
                foreach (var aField in commonFields)
                {
                    PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                    var value = (dataRow[aField.Name] == DBNull.Value) ?
                    null : dataRow[aField.Name]; //if database field is nullable
                    propertyInfos.SetValue(aTSource, value, null);
                }
                dataList.Add(aTSource);
            }
        }
        return dataList;
    }


    public static List<CommonDdlStr> Fill_DDL_ListStr(DataTable dt, bool isColumnChange, string TextField, string ValueField)
    {
        List<CommonDdlStr> lst = new List<CommonDdlStr>();
        DataTable dtf = new DataTable();
        if (isColumnChange)
        {
            dt.Columns[TextField].ColumnName = "Text";
            dt.Columns[ValueField].ColumnName = "Value";
            dtf = dt.Clone();
            dtf.Columns["Value"].DataType = typeof(System.String);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtf.ImportRow(dt.Rows[i]);
            }
        }
        else
        {
            dtf = dt.Clone();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtf.ImportRow(dt.Rows[i]);
            }
        }
        return lst = CommonsDBFn.DataTable_ToList<CommonDdlStr>(dtf);
    }
    public static List<CommonDdlInt> Fill_DDL_ListInt(DataTable dt, bool isColumnChange, string TextField, string ValueField)
    {
        List<CommonDdlInt> lst = new List<CommonDdlInt>();
        if (isColumnChange)
        {
            dt.Columns[TextField].ColumnName.Replace(TextField, "Text");
            dt.Columns[ValueField].ColumnName.Replace(ValueField, "Value");
        }
        return lst = CommonsDBFn.DataTable_ToList<CommonDdlInt>(dt);
    }

    public class WatetlevelChart
    {
        public DateTime date { get; set; }
        public decimal Water_Level { get; set; }
    }

    public class CommonDdlStr
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class CommonDdlInt
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public static DataTable Delete_Extra_Columns(DataTable dt, int ColCnt)
    {
        DataTable dtN = dt.Copy();
        for (int i = 0; i < ColCnt; i++)
        {
            dtN.Columns.RemoveAt(0);
        }
        return dtN;
    }
    public static DataTable AddRowsIfNull(DataTable dt)
    {
        DataTable dtn = dt.Copy();
        DataRow dr;
        dr = dtn.NewRow();
        for (int i = 0; i < dtn.Columns.Count; i++)
        {
            dr[i] = "0";
        }
        dtn.Rows.Add(dr);
        dtn.AcceptChanges();
        return dtn;
    }
    public static List<T> ConvertDataTable<T>(DataTable dt)
    {
        List<T> data = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T item = GetItem<T>(row);
            data.Add(item);
        }
        return data;
    }
    public static T GetItem<T>(DataRow dr)
    {
        Type temp = typeof(T);
        T obj = Activator.CreateInstance<T>();

        foreach (DataColumn column in dr.Table.Columns)
        {
            foreach (PropertyInfo pro in temp.GetProperties())
            {
                if (pro.Name == column.ColumnName)
                    pro.SetValue(obj, dr[column.ColumnName], null);
                else
                    continue;
            }
        }
        return obj;
    }


    public static string Get_DataTable_XmlString(DataTable dt, string TextField, string ValueField)
    {
        string result = "";
        try
        {
            if (dt.Columns.Count >= 2)
            {
                dt.Columns[TextField].ColumnName.Replace(TextField, "Text");
                dt.Columns[ValueField].ColumnName.Replace(ValueField, "Value");
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw);
                    result = sw.ToString();
                }
            }
        }
        catch
        {
            //string msg = ex.Message;
            throw;
        }

        return result;
    }
    public static string Get_DataTable_JsonString(DataTable dt, string TextField, string ValueField)
    {
        string result = "";
        try
        {
            if (dt.Columns.Count >= 2)
            {
                dt.Columns[TextField].ColumnName.Replace(TextField, "Text");
                dt.Columns[ValueField].ColumnName.Replace(ValueField, "Value");
                result = JsonConvert.SerializeObject(dt);
            }
        }
        catch //(Exception ex)
        {
            // string msg = ex.Message;
            throw;
        }

        return result;
    }


}
