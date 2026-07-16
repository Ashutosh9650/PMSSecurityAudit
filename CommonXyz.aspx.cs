using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommonXyz : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_FYear(string ValidID)
    {

        DataTable dtYear = Comman.Generate_Financial_Years();
        DataView dv = new DataView(dtYear);
        dv.RowFilter = "ID in(2023,2024,2025,2026)";
        DataTable dtY = dv.ToTable();
        //DataTable dt = Comman.Select_All_Data("mstSchool", "ZoneID,  Zone ", "ZoneID not in(2,3,4,5)", "ZoneID", "Asc", "Y");
        //DataView dv = dt.DefaultView;
        DataTable dtf = dv.ToTable("Selected", false, "ID", "Type");
        return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "Type", "ID");
    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_FYear_NextFY(string ValidID)
    {

        DataTable dtYear = Comman.Generate_Post_Financial_Years();
        DataView dv = new DataView(dtYear);
        dv.RowFilter = "ID in(2023,2024,2025,2026)";
        DataTable dtY = dv.ToTable();
        //DataTable dt = Comman.Select_All_Data("mstSchool", "ZoneID,  Zone ", "ZoneID not in(2,3,4,5)", "ZoneID", "Asc", "Y");
        //DataView dv = dt.DefaultView;
        DataTable dtf = dv.ToTable("Selected", false, "ID", "Type");
        return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "Type", "ID");
    }

    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_FYear_NextFY2025(string ValidID)
    {

        DataTable dtYear = Comman.Generate_Post_Financial_Years();
        DataView dv = new DataView(dtYear);
        dv.RowFilter = "ID in(2026)";
        DataTable dtY = dv.ToTable();
        //DataTable dt = Comman.Select_All_Data("mstSchool", "ZoneID,  Zone ", "ZoneID not in(2,3,4,5)", "ZoneID", "Asc", "Y");
        //DataView dv = dt.DefaultView;
        DataTable dtf = dv.ToTable("Selected", false, "ID", "Type");
        return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "Type", "ID");
    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_State(string ValidID)
    {
        string conditions = "";
        if (ValidID=="2023")
        conditions = "StateCode=9 ";
        else
            conditions= "StateCode = 9A ";
        string userlevel= Convert.ToString(HttpContext.Current.Session["user_level_Role"]);
        string statecode = Convert.ToString(HttpContext.Current.Session["StateCode"]);
        string username = Convert.ToString(HttpContext.Current.Session["username"]);
        if (statecode.ToString() != "")
        {

            SqlParameter[] par1 = new SqlParameter[]
               {
                      new SqlParameter("@user_level_Role",  userlevel),
                      new SqlParameter("@UserName",username ),
                    new SqlParameter("@StateCode", statecode),
                       new SqlParameter("@Year",  ValidID),
               };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);
         

            conditions = "StateCode='" + statecode.ToString() + "' ";
           // DataTable dt = Comman.Select_All_Data("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", ""+ conditions + "", " StateCode", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "StateCode", "StateName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "StateName", "StateCode");
        }
        
        else
        {
            SqlParameter[] par1 = new SqlParameter[]
              {
                      new SqlParameter("@user_level_Role",  userlevel),
                      new SqlParameter("@UserName",username ),
                    new SqlParameter("@StateCode", statecode),
                       new SqlParameter("@Year",  ValidID),
              };
            DataTable dt = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "rptLoadAllState", par1);

            /// DataTable dt = Comman.Select_All_Data("mst1State", "StateCode, dbo.TitleCase(upper(StateName)) as StateName", " 1=1 ", "StateCode", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "StateCode", "StateName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "StateName", "StateCode");
        }
        
    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_District(string ValidID,string ValidID1)
    {
        clsMain objMain = new clsMain();
        string conditions = " ";
        string districtCode = "";
        string mul = "";

        string user_level_Role = Convert.ToString(HttpContext.Current.Session["user_level_Role"]);
        if (user_level_Role=="1")
        {

        }
       else if (user_level_Role == "2")
        {
            mul = " mst2District.StateCode in('" + ValidID1 + "') and UserName='" + Convert.ToString(HttpContext.Current.Session["username"])  + "' ";
        }
        else
        {
             districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        }
          if (mul.Length > 0)
        {
            string strQry1 = "  a.DistrictCode in (     sELECT distinct mst2District.DistrictCode FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + mul + "  and  Fyear='" + ValidID + "'  )  ";
            DataTable dtDistrict = Comman.Select_All_Data("[PMS].[dbo].[mst2District] a left join [PMS].[dbo].[GIS_District] b on a.EGDistrictCode=b.EG_DistrictCode", "Concat(DistrictCode,'#',b.lat,'#',b.long)DistrictCode,dbo.TitleCase(upper(a.DistrictName)) as DistrictName", " " + strQry1 + " ", " DistrictName", "Asc", "Y");

           
            DataView dv = dtDistrict.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "DistrictCode", "DistrictName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "DistrictName", "DistrictCode");
        }
      else  if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString()!="")
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst2District] a left join [PMS].[dbo].[GIS_District] b on a.EGDistrictCode=b.EG_DistrictCode", "Concat(DistrictCode,'#',b.lat,'#',b.long)DistrictCode,dbo.TitleCase(upper(a.DistrictName)) as DistrictName", " " + conditions + " ", " DistrictName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "DistrictCode", "DistrictName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "DistrictName", "DistrictCode");
        }
     
        else
        {
            conditions = "a.StateCode = '" + ValidID1 + "' and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst2District] a left join [PMS].[dbo].[GIS_District] b on a.EGDistrictCode=b.EG_DistrictCode", "Concat(DistrictCode,'#',b.lat,'#',b.long)DistrictCode,dbo.TitleCase(upper(a.DistrictName)) as DistrictName", " " + conditions + " ", " DistrictName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "DistrictCode", "DistrictName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "DistrictName", "DistrictCode");
        }

    }

    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_District2026(string ValidID, string ValidID1)
    {
        clsMain objMain = new clsMain();
        string conditions = " ";
        string districtCode = "";
        string mul = "";

        string user_level_Role = Convert.ToString(HttpContext.Current.Session["user_level_Role"]);
        if (user_level_Role == "1")
        {

        }
        else if (user_level_Role == "2")
        {
            mul = " mst2District.StateCode in('" + ValidID1 + "') and UserName='" + Convert.ToString(HttpContext.Current.Session["username"]) + "' ";
        }
        else
        {
            districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        }
        if (mul.Length > 0)
        {
            string strQry1 = "  a.DistrictCode in (     sELECT distinct mst2District.DistrictCode FROM MstusermultipleDist  ";
            strQry1 += " inner join mst2District on mst2District.OldDistrictCode=MstusermultipleDist.OldDistrictCode  where " + mul + "  and  Fyear='" + ValidID + "'  )  ";
            DataTable dtDistrict = Comman.Select_All_Data("[PMS].[dbo].[mst2District] a ", "Concat(a.DistrictCode,'#',a.D_lat,'#',a.D_long)DistrictCode,dbo.TitleCase(upper(a.DistrictName)) as DistrictName", " " + strQry1 + " ", " DistrictName", "Asc", "Y");


            DataView dv = dtDistrict.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "DistrictCode", "DistrictName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "DistrictName", "DistrictCode");
        }
        else if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst2District] a", "Concat(a.DistrictCode,'#',a.D_lat,'#',a.D_long)DistrictCode,dbo.TitleCase(upper(a.DistrictName)) as DistrictName", " " + conditions + " ", " DistrictName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "DistrictCode", "DistrictName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "DistrictName", "DistrictCode");
        }

        else
        {
            conditions = "a.StateCode = '" + ValidID1 + "' and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst2District] a ", "Concat(a.DistrictCode,'#',a.D_lat,'#',a.D_long)DistrictCode,dbo.TitleCase(upper(a.DistrictName)) as DistrictName", " " + conditions + " ", " DistrictName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "DistrictCode", "DistrictName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "DistrictName", "DistrictCode");
        }

    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Block(string ValidID, string ValidID1,string ValidID2)
    
    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        string nlockcode = "";
        if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "2")
        {
            districtCode = "";
        }
        if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "4")
        {
            nlockcode = " and BlockCode in(" + HttpContext.Current.Session["BlockCode"] + " )";
        }
        if (nlockcode.Length > 0)
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "' and b.F_year= '" + ValidID + "' and a.BlockCode in(" + HttpContext.Current.Session["BlockCode"] + " ) ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a inner join [PMS].[dbo].[GIS_Block] b on a.EGBlockCode=b.GISEGBlockCode ", "Concat(a.BlockCode,'#',b.lat,'#',b.long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }

     else   if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "' and b.F_year= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a inner join [PMS].[dbo].[GIS_Block] b on a.EGBlockCode=b.GISEGBlockCode ", "Concat(a.BlockCode,'#',b.lat,'#',b.long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }
   

        else
        {
            conditions = "a.StateCode = '" + ValidID1 + "'  and a.DistrictCode= '" + ValidID2 + "' and a.Fyear= '" + ValidID + "' and b.F_year= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a inner join [PMS].[dbo].[GIS_Block] b on a.EGBlockCode=b.GISEGBlockCode ", "Concat(a.BlockCode,'#',b.lat,'#',b.long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }

    }



    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Block2026(string ValidID, string ValidID1, string ValidID2)

    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        string nlockcode = "";
        if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "2")
        {
            districtCode = "";
        }
       else if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "1")
        {
            districtCode = "";
        }
        if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "4")
        {
            nlockcode = " and BlockCode in(" + HttpContext.Current.Session["BlockCode"] + " )";
        }
        if (nlockcode.Length > 0)
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "'  and a.BlockCode in(" + HttpContext.Current.Session["BlockCode"] + " ) ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a ", "Concat(a.BlockCode,'#',a.B_lat,'#',a.B_long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }

        else if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a  ", "Concat(a.BlockCode,'#',a.B_lat,'#',a.B_long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }


        else
        {
            conditions = "a.StateCode = '" + ValidID1 + "'  and a.DistrictCode= '" + ValidID2 + "' and a.Fyear= '" + ValidID + "'  ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a  ", "Concat(a.BlockCode,'#',a.B_lat,'#',a.B_long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }

    }

    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Block2025(string ValidID, string ValidID1, string ValidID2)

    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        string nlockcode = "";
        if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "2")
        {
            districtCode = "";
        }
        if (Convert.ToString(HttpContext.Current.Session["user_level_Role"]) == "4")
        {
            nlockcode = " and BlockCode in(" + HttpContext.Current.Session["BlockCode"] + " )";
        }
        if (nlockcode.Length > 0)
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.Fyear= '" + ValidID + "' and b.F_year= '" + ValidID + "' and a.BlockCode in(" + HttpContext.Current.Session["BlockCode"] + " ) ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a inner join [PMS].[dbo].[GIS_Block] b on a.EGBlockCode=b.GISEGBlockCode ", "Concat(a.BlockCode,'#',b.lat,'#',b.long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }

        else if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and b.F_year= '" + ValidID + "' and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a inner join [PMS].[dbo].[GIS_Block] b on a.EGBlockCode=b.GISEGBlockCode ", "Concat(a.BlockCode,'#',b.lat,'#',b.long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }


        else
        {
            conditions = "a.StateCode = '" + ValidID1 + "'  and a.DistrictCode= '" + ValidID2 + "' and b.F_year= '" + ValidID + "' and a.Fyear= '" + ValidID + "'";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mst3block] a inner join [PMS].[dbo].[GIS_Block] b on a.EGBlockCode=b.GISEGBlockCode ", "Concat(a.BlockCode,'#',b.lat,'#',b.long)BlockCode,dbo.TitleCase(upper(BlockName)) as BlockName ", " " + conditions + " ", " BlockName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "BlockCode", "BlockName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "BlockName", "BlockCode");
        }

    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "a.StateCode ='" + ValidID1 + "' and a.DistrictCode IN(" + districtCode + ") and a.BlockCode='"+ValidID3+"' and a.Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("(Select Concat(a.ClusterCode,'#',c.lat,'#',c.long)ClusterCode, dbo.TitleCase(upper(ClusterName)) as ClusterName, ROW_NUMBER() OVER(PARTITION by ClusterName ORDER BY ClusterName asc) as rn  from[PMS].[dbo].[mstCluster] a left join[PMS].[dbo].[mst5Village] b on a.ClusterCode = b.ClusterCode inner join[PMS].[dbo].[GIS_Village] c on b.EGVillageCode = c.EG_VillageCode where  " + conditions+")t", "clusterCode,ClusterName ", " rn=1 ", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

        else
        {
            conditions = "a.StateCode = '" + ValidID1 + "'  and a.DistrictCode= '" + ValidID2 + "' and a.Fyear= '" + ValidID + "' and a.BlockCode='" + ValidID3 + "' ";
            DataTable dt = Comman.Select_All_Data("(Select Concat(a.ClusterCode,'#',c.lat,'#',c.long)ClusterCode, dbo.TitleCase(upper(ClusterName)) as ClusterName, ROW_NUMBER() OVER(PARTITION by ClusterName ORDER BY ClusterName asc) as rn  from[PMS].[dbo].[mstCluster] a left join[PMS].[dbo].[mst5Village] b on a.ClusterCode = b.ClusterCode inner join[PMS].[dbo].[GIS_Village] c on b.EGVillageCode = c.EG_VillageCode where  " + conditions + ")t", "ClusterCode,ClusterName ", " rn=1 ", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

    }

    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Cluster_cluster2025(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {

        string conditions = " ";
        string districtCode = Convert.ToString(ValidID2);
        if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "StateCode ='" + ValidID1 + "' and DistrictCode IN('" + districtCode + "') and BlockCode='" + ValidID3 + "' and Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstCluster]", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", " " + conditions + " union ALL Select '99' as [ClusterCode],'Unassigned' as [ClusterName]", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

        else
        {
            conditions = "StateCode = '" + ValidID1 + "'  and DistrictCode= '" + ValidID2 + "' and Fyear= '" + ValidID + "' and BlockCode='" + ValidID3 + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstCluster]", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", " " + conditions + " union ALL Select '99' as [ClusterCode],'Unassigned' as [ClusterName]", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Cluster_cluster(string ValidID, string ValidID1, string ValidID2, string ValidID3)
    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "StateCode ='" + ValidID1 + "' and DistrictCode IN(" + districtCode + ") and BlockCode='" + ValidID3 + "' and Fyear= '" + ValidID + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstCluster]", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", " " + conditions + " union ALL Select '99' as [ClusterCode],'Unassigned' as [ClusterName]", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

        else
        {
            conditions = "StateCode = '" + ValidID1 + "'  and DistrictCode= '" + ValidID2 + "' and Fyear= '" + ValidID + "' and BlockCode='" + ValidID3 + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstCluster]", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", " " + conditions + " union ALL Select '99' as [ClusterCode],'Unassigned' as [ClusterName]", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_Cluster_Map(string ValidID, string ValidID1, string ValidID2, string ValidID3,string ValidID4)
    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        if (districtCode.ToString() != "'',''" && districtCode.ToString() != "''" && districtCode.ToString() != "")
        {
            conditions = "StateCode ='" + ValidID1 + "' and DistrictCode IN('" + ValidID2 + "') and BlockCode='" + ValidID3 + "' and Fyear= '" + ValidID + "' and ClusterCode<>'"+ ValidID4 + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstCluster]", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", " " + conditions + " ", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

        else
        {
            conditions = "StateCode = '" + ValidID1 + "'  and DistrictCode= '" + ValidID2 + "' and Fyear= '" + ValidID + "' and BlockCode='" + ValidID3 + "' and ClusterCode<>'" + ValidID4 + "' ";
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstCluster]", "ClusterCode,dbo.TitleCase(upper(ClusterName)) as ClusterName ", " " + conditions + " ", " ClusterName", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ClusterCode", "ClusterName");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "ClusterName", "ClusterCode");
        }

    }
    [WebMethod(EnableSession = true)]
    public static List<CommonsDBFn.CommonDdlStr> Fill_GroupBy(string ValidID, string ValidID1)
    {

        string conditions = " ";
        string districtCode = Convert.ToString(HttpContext.Current.Session["DistrictCode"]);
        //if (districtCode.ToString() != "''")
        //{
            if(ValidID=="")
            { conditions = "Level IN(1,2,3)"; }
            else if (ValidID1 == "")
            { conditions = "Level IN(2,3)"; }
            else 
            { conditions = "Level IN(3)"; }
            
            DataTable dt = Comman.Select_All_Data("[PMS].[dbo].[mstGroupBy]", "ID,dbo.TitleCase(upper(Name)) as Name ", " " + conditions + " ", " ID", "Asc", "Y");
            DataView dv = dt.DefaultView;
            DataTable dtf = dv.ToTable("Selected", false, "ID", "Name");
            return CommonsDBFn.Fill_DDL_ListStr(dtf, true, "Name", "ID");
        //}

        
    }
}