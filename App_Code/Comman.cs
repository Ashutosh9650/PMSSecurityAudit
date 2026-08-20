using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
public class Comman
{
    public const int PasswordSaltSize = 16;
    Password objPass = new Password();
    public DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch
        {
            //string mmsg = ex.Message; showMessages(mmsg);
            //showMessages("(SelectAllData)  " + mmsg);
            throw;
        }
        return dtcombo;
    }
    public static void Bind_DDL_ZeroIndex_String(DropDownList dll, DataTable dtall, string fname, string fvalue, string ZeroIndex)
    {
        try
        {
            DataTable dt = dtall.Copy();
            if (dll.Items.Count > 0)
            {
                dll.Items.Clear();
            }
            dll.DataSource = dt;
            dll.DataTextField = fname;
            dll.DataValueField = fvalue;
            dll.DataBind();
            if (ZeroIndex != "")
            {
                dll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--" + ZeroIndex + "--", "0"));
            }

        }
        catch (Exception rv1)
        {
            string msg = rv1.Message;
        }
    }
    public static DataTable Select_All_Data(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition, string OtherConnection)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            dtcombo = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_Table_Data_Common", paramvT);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: {0}", ex);
        }
        //catch 
        //{
        //    string msg = ex.Message;
        //    //string mmsg = ex.Message; showMessages(mmsg);
        //    //showMessages("(SelectAllData)  " + mmsg);
        //}
        return dtcombo;
    }
    public static void Bind_DDL_ZeroIndex_String_List(ListBox dll, DataTable dtall, string fname, string fvalue, string ZeroIndex)
    {
        try
        {
            DataTable dt = dtall.Copy();
            if (dll.Items.Count > 0)
            {
                dll.Items.Clear();
            }
            dll.DataSource = dt;
            dll.DataTextField = fname;
            dll.DataValueField = fvalue;
            dll.DataBind();
            if (ZeroIndex != "")
            {
                dll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--" + ZeroIndex + "--", "0"));
            }

        }
        catch (Exception rv1)
        {
            string msg = rv1.Message;
        }
    }

    public static void Bind_DDL_ZeroIndex_String(DropDownList dll, string ZeroIndex)
    {
        try
        {
            if (dll.Items.Count > 0)
            {
                dll.Items.Clear();
            }
            dll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("  " + ZeroIndex + "  ", "0"));

        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: {0}", ex);
        }
        //catch 
        //{
        //    string msg = ex.Message;
        //}
    }
    public DataTable Select_All_DataNew(string TableName, string TFieldName, string Condition, string OrderbyCondition, string Sortcondition, string Sortcofndition)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            string WConditions = Condition.Length > 0 ? " where " + Condition : "";
            string OrderbyvalueMem = OrderbyCondition.Length > 0 ? " order by " + OrderbyCondition + "  " : "";
            string sortbycondi = Sortcondition.Length > 0 ? "" + Sortcondition : "";
            string FieldName = TFieldName.Length > 0 ? TFieldName : "";
            SqlParameter[] paramvT = new SqlParameter[]
                    {
                            new SqlParameter("@TableName",TableName),
                            new SqlParameter("@Condition",WConditions),
                            new SqlParameter("@OrderbyvalueMem",OrderbyvalueMem),
                            new SqlParameter("@sortbycondi",sortbycondi),
                            new SqlParameter("@FieldName",FieldName),

                    };

            DataSet ds = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "Get_Select_AllTableData", paramvT);
            dtcombo = ds.Tables[0] as DataTable;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: {0}", ex);
        }
        //catch 
        //{
        //    //string mmsg = ex.Message; showMessages(mmsg);
        //    //showMessages("(SelectAllData)  " + mmsg);
        //}
        return dtcombo;
    }

    public bool BindDLLMasterTableVillage(string dtname, string fieldname, DataTable dt, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        //string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        //DataTable dt = dbt.VGridFill(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }
    public bool BindDLLNew(string dtname, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        DataTable dt = LoadData(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "--" + ZeroIndex + "--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    public bool BindDLL(string dtname, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        DataTable dt = LoadData(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }
    public bool BindDLLSelectAll(string dtname, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        DataTable dt = LoadData(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);

            if (dt.Rows.Count > 0)
            {
                dr = dt.NewRow();
                dr[textData] = "--" + "All" + "--";
                dr[valData] = "1";
                dt.Rows.InsertAt(dr, 1);
                dt.AcceptChanges();
            }
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }


    public bool BindDLLDatatable(string dtname, DataTable dt, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;



        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    public DataTable LoadData(string Query)
    {
        DataTable dtcombo = new DataTable();
        try
        {
            dtcombo = SqlHelper.GetDataTable(SqlHelper.mainConnectionString, CommandType.Text, Query);


        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.TraceError("Error: {0}", ex);
        }
        //catch
        //{
        //    //string mmsg = ex.Message; showMessages(mmsg);
        //    //showMessages("(SelectAllData)  " + mmsg);
        //}
        return dtcombo;
    }
    public string Generate_RandomString(int NoChar)
    {
        string UNICode = "";
        System.Threading.Thread.Sleep(1000);
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, NoChar).Select(s => s[random.Next(s.Length)]).ToArray());
        //var result = new string(Enumerable.Repeat(chars, NoChar).Select(s => s[random.Next(s.Length)]).ToArray()) + DateTime.Now.ToString("yyyyMMddhhmmssfff");
        UNICode = result.ToString();
        return UNICode;
    }
    public string Generate_RandomStringAnu(int NoChar)
    {
        string UNICode = "";
        System.Threading.Thread.Sleep(100);
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        //  var result = new string(Enumerable.Repeat(chars, NoChar).Select(s => s[random.Next(s.Length)]).ToArray());
        var result = new string(Enumerable.Repeat(chars, NoChar).Select(s => s[random.Next(s.Length)]).ToArray()) + DateTime.Now.ToString("yyyyMMddhhmmssfff");
        UNICode = result.ToString();
        return UNICode;
    }
    public DataTable VGridFill(string select, string con)
    {
        OleDbConnection dbOleconnection = new OleDbConnection(con);

        try
        {
            if (dbOleconnection.State == ConnectionState.Closed)
            {
                dbOleconnection.Open();
            }
            DataTable dbOleDataTable = new DataTable();
            OleDbCommand dbOleCommand = new OleDbCommand();
            dbOleCommand.Connection = dbOleconnection;
            dbOleCommand.Parameters.Clear();
            dbOleCommand.CommandType = CommandType.Text;
            dbOleCommand.CommandText = select;
            OleDbDataAdapter dbOleDataAdapter = new OleDbDataAdapter();
            dbOleDataAdapter.SelectCommand = dbOleCommand;
            dbOleDataAdapter.Fill(dbOleDataTable);
            return dbOleDataTable;

        }
        catch (OleDbException sqlEx)
        {
            if (dbOleconnection.State == ConnectionState.Open)
            {
                dbOleconnection.Close();
            }
            throw sqlEx;
        }
        catch (Exception ex1)
        {
            if (dbOleconnection.State == ConnectionState.Open)
            {
                dbOleconnection.Close();
            }
            throw;
        }
        finally
        {
            if (dbOleconnection.State == ConnectionState.Open)
            {
                dbOleconnection.Close();
            }

        }
    }
    public bool AddUpdate(string query, string con)
    {

        using (OleDbCommand cmd = new OleDbCommand())
        {
            OleDbConnection mycon = new OleDbConnection(con);

            try
            {
                DataTable dtCode = new DataTable();
                if (mycon.State == ConnectionState.Closed)
                {
                    mycon.Open();
                }
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = mycon;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                return (true);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                mycon.Close();
            }
        }

    }


    public DataTable GetUserAuthenticate(string pUsername, string pPassword)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            string checkpass = objPass.CreatePasswordHashNew(pPassword);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataTable dbSqlDataSet = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_AuthenticateUser";
            sqlcmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 100).Value = pUsername;
            sqlcmd.Parameters.Add("@Password", SqlDbType.NVarChar, 256).Value = pPassword;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);

            sqlDataAdapter.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            //IUErrorDetail(e.ToString());
            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataTable GetUserAuthenticate2024(string pUsername, string pPassword)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            string checkpass = objPass.CreatePasswordHashNew(pPassword);
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataTable dbSqlDataSet = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_AuthenticateUser2024";
            sqlcmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 100).Value = pUsername;
            sqlcmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100).Value = pPassword;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);

            sqlDataAdapter.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            //IUErrorDetail(e.ToString());
            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataTable GetUserAuthenticateNew(string pUsername, string pPassword)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataTable dbSqlDataSet = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_AuthenticateUser2023";
            sqlcmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 100).Value = pUsername;
            sqlcmd.Parameters.Add("@Password", SqlDbType.NVarChar, 256).Value = pPassword;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);

            sqlDataAdapter.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            //IUErrorDetail(e.ToString());
            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataTable CreateDataTable(string sTableTypeName)
    {
        DataTable dt = new DataTable();
        DataTable dtDB = GetTableTypeColumns(sTableTypeName);

        foreach (DataRow dr in dtDB.Rows)
        {
            dt.Columns.Add(new DataColumn(dr["ColumnName"].ToString(), Type.GetType(dr["TypeName"].ToString())));
        }

        return dt;
    }
    private DataTable GetTableTypeColumns(string sTableTypeName)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataTable dbSqlDataSet = new DataTable();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_GetTableTypeColumns";
            sqlcmd.Parameters.Add("@TableType", SqlDbType.VarChar, 50).Value = sTableTypeName;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlcmd);

            sqlDataAdapter.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            //IUErrorDetail(e.ToString());
            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet IU_PostLogintables(DataTable dtTbl_User_Login, DataTable DttblActivityUpdate_CLT, DataTable DttblActivityUpdate_CTLImplementation, DataTable DttblActivityUpdate_LifeskillGames, DataTable DttblActivityUpdate_School, DataTable DttblActivityUpdate_Village, DataTable dttblDTD, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Data]";
            sqlcmd.Parameters.Add(new SqlParameter("@Tbl_User_Login", SqlDbType.Structured) { TypeName = "dtTbl_User_Login", Value = dtTbl_User_Login });
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_CLT", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_CLT", Value = DttblActivityUpdate_CLT });
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_CTLImplementation", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_CTLImplementation", Value = DttblActivityUpdate_CTLImplementation });
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_LifeskillGames", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_LifeskillGames", Value = DttblActivityUpdate_LifeskillGames });
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_School", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_School", Value = DttblActivityUpdate_School });
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_Village", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_Village", Value = DttblActivityUpdate_Village });
            sqlcmd.Parameters.Add(new SqlParameter("@tblDTD", SqlDbType.Structured) { TypeName = "dttblDTD", Value = dttblDTD });
            sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
            sqlcmd.Parameters.Add("@JSON", SqlDbType.VarChar, -1).Value = sJason;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_User_Login(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_User_Login]";
            sqlcmd.Parameters.Add(new SqlParameter("@Tbl_User_Login", SqlDbType.Structured)
            {
                TypeName = "dbo.Tbl_User_LoginVersion",
                Value = dtTbl_User_Login
            });
            sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
            sqlcmd.Parameters.Add("@JSON", SqlDbType.VarChar, -1).Value = sJason;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_CLT(DataTable DttblActivityUpdate_CLT, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_ActivityUpdate_CLT]";
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_CLT", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_CLT", Value = DttblActivityUpdate_CLT });
            sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
            sqlcmd.Parameters.Add("@JSON", SqlDbType.VarChar, -1).Value = sJason;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_CTLImplementation(DataTable DttblActivityUpdate_CTLImplementation, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_CTLImplementation";
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_CTLImplementation", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_CTLImplementation", Value = DttblActivityUpdate_CTLImplementation });
            sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
            sqlcmd.Parameters.Add("@JSON", SqlDbType.VarChar, -1).Value = sJason;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_LifeskillGames(DataTable DttblActivityUpdate_LifeskillGames, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_LifeskillGames";
            sqlcmd.Parameters.Add(new SqlParameter("@tblActivityUpdate_LifeskillGames", SqlDbType.Structured) { TypeName = "DttblActivityUpdate_LifeskillGames", Value = DttblActivityUpdate_LifeskillGames });
            sqlcmd.Parameters.Add("@UserID", SqlDbType.Int).Value = iUserID;
            sqlcmd.Parameters.Add("@JSON", SqlDbType.VarChar, -1).Value = sJason;
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_School(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_School20190719(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20190719";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_School2023(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School2023";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_School202313(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School202313";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_School20230610(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20230610";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_ActivityUpdate_School20230707(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20230707";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_School20230908(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20230908";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_School20231001(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20231001";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_School20230112(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20230112";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_School20232024(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School20232024";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_School2025(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_School2025";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblOOSC(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSC";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblOOSC2023(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSC2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblOOSCNew(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSCNew";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblOOSCNew2023(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSCNew2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_HousholdTemp(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_HousholdTemp";
            sqlcmd.Parameters.AddWithValue("@tblHousholdTemp", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_HousholdTemp2023(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_HousholdTemp2023";
            sqlcmd.Parameters.AddWithValue("@tblHousholdTemp", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyTemp(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyTemp";
            sqlcmd.Parameters.AddWithValue("@tblSurveyTemp", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyTemp2023(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyTemp2023";
            sqlcmd.Parameters.AddWithValue("@tblSurveyTemp", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyTemp20232024(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyTemp20232024";
            sqlcmd.Parameters.AddWithValue("@tblSurveyTemp", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyTempMaitri(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyTempMaitri";
            sqlcmd.Parameters.AddWithValue("@tblSurveyTemp", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblVlgHHImage(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblVlgHHImage";
            sqlcmd.Parameters.AddWithValue("@tblVlgHHImage", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet TblActivityUpdate_Baseline(DataTable dtTblActivityUpdate_Baseline, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Baseline";
            sqlcmd.Parameters.AddWithValue("@dtTblActivityUpdate_Baseline", dtTblActivityUpdate_Baseline);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_ActivityUpdate_Village(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_Village2021(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village2021";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_Village2022(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village2022";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_Village2023(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village2023";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_ActivityUpdate_Village202309(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village202309";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_Village20230221(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village20230221";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate_Village20252026(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdate_Village20252026";
            sqlcmd.Parameters.AddWithValue("@tblActivityUpdate_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_tblClusterMeeting(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_tblClusterMeeting";
            sqlcmd.Parameters.AddWithValue("@tblClusterMeeting", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_tblDTD(DataTable dttblDTD, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_tblDTD";
            sqlcmd.Parameters.AddWithValue("@tblDTD", dttblDTD);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_ActivityUpdateOffice(DataTable TblActivityUpdate_Office, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdateOffice";
            sqlcmd.Parameters.AddWithValue("@TblActivityUpdate_Office", TblActivityUpdate_Office);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_ActivityUpdateOfficeNew(DataTable TblActivityUpdate_Office, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_ActivityUpdateOfficeNew";
            sqlcmd.Parameters.AddWithValue("@TblActivityUpdate_Office", TblActivityUpdate_Office);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public string INSERT_ImportDataSingleSP(DataTable dt, string strSP_Name, string strParentTable_Name, string strtemptblmstGroupChk, string strtemptblmstGroup, string Flag, SqlConnection ConStr)
    {
        string RowAffect = string.Empty;



        try
        {


            ConStr.Open();

            using (SqlCommand dbSqlCommand = new SqlCommand(strtemptblmstGroupChk, ConStr))
            { dbSqlCommand.ExecuteNonQuery(); }
            using (SqlCommand dbSqlCommand = new SqlCommand(strtemptblmstGroup, ConStr))
            { dbSqlCommand.ExecuteNonQuery(); }



            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConStr))
            {
                bulkCopy.DestinationTableName = "#temp_" + strParentTable_Name + "";
                bulkCopy.BulkCopyTimeout = 3000000;
                bulkCopy.WriteToServer(dt);
            }



            using (SqlCommand dbSqlCommand = new SqlCommand())
            {
                dbSqlCommand.Connection = ConStr;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = strSP_Name;
                dbSqlCommand.Parameters.Add("@Flag", Flag);
                System.Data.SqlClient.SqlParameter pRowsAffected1 = new SqlParameter("@RowAfected", System.Data.SqlDbType.Int);
                pRowsAffected1.Direction = System.Data.ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(pRowsAffected1);
                dbSqlCommand.CommandTimeout = 3000000;
                int _returnRow = dbSqlCommand.ExecuteNonQuery();
                //SqlDataAdapter ad = new SqlDataAdapter(dbSqlCommand);
                //DataTable dttemp = new DataTable();
                //ad.Fill(dttemp);
                RowAffect = Convert.ToString(_returnRow).Trim();
            }
            ConStr.Close();
        }
        catch (Exception _ex)
        {
            return "5000";
            ConStr.Close();
        }
        finally
        {
        }
        return RowAffect;
    }

    public Boolean BulkCopyTempDistProfile(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("DistrictCode", "DistrictCode");
            // SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("OldDistrictCode", "OldDistrictCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("EGBlockCode", "EGBlockCode");
            //SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("OldBlockCode", "OldBlockCode");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("GP_CODE", "GP_CODE");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("OldPanchayatCode", "OldPanchayatCode");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            //  SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("OldUniqueCode", "OldUniqueCode");
            //  SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("OldVillageUniqueCode", "OldVillageUniqueCode");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("DISECODE", "DISECODE");
            // SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("OldSchoolUniqueCode", "OldSchoolUniqueCode");

            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            // bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            //bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            // bulkCopy.ColumnMappings.Add(mapping08);
            // bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            //  bulkCopy.ColumnMappings.Add(mapping11);

            bulkCopy.DestinationTableName = "TempDistProfile";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean BulkCopyTbTrainingDeatils(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("TBUniqueCode", "TBUniqueCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("TBID", "TBID");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("TotalDay", "TotalDay");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("Adate1", "Adate1");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("Adate2", "Adate2");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("Adate3", "Adate3");

            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("Adate4", "Adate4");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("Adate5", "Adate5");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("Adate6", "Adate6");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("Adate7", "Adate7");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.DestinationTableName = "tblTrainingDetail";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Boolean BulkCopyTbTranAtt(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("AttUniqueCode", "AttUniqueCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("TBId", "TBId");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("AttDate", "AttDate");


            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);


            bulkCopy.DestinationTableName = "tblAttendance";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public Boolean BulkCopySchool(DataTable dt)
    {
        try
        {

            SqlBulkCopyColumnMapping mapping01 = new SqlBulkCopyColumnMapping("VillageCode", "VillageCode");
            SqlBulkCopyColumnMapping mapping02 = new SqlBulkCopyColumnMapping("DISECODE", "SchoolCode");
            SqlBulkCopyColumnMapping mapping03 = new SqlBulkCopyColumnMapping("DISECODE", "SchoolCodeID");
            SqlBulkCopyColumnMapping mapping04 = new SqlBulkCopyColumnMapping("DISECODE", "DISECode");
            SqlBulkCopyColumnMapping mapping05 = new SqlBulkCopyColumnMapping("DISECODE", "DISECode1");
            SqlBulkCopyColumnMapping mapping06 = new SqlBulkCopyColumnMapping("DISECODE", "DISECode2");
            SqlBulkCopyColumnMapping mapping07 = new SqlBulkCopyColumnMapping("SchoolName", "Name");
            SqlBulkCopyColumnMapping mapping08 = new SqlBulkCopyColumnMapping("SchoolName", "Name1");
            SqlBulkCopyColumnMapping mapping09 = new SqlBulkCopyColumnMapping("SchoolName", "Name2");
            SqlBulkCopyColumnMapping mapping10 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel");
            SqlBulkCopyColumnMapping mapping11 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel1");
            SqlBulkCopyColumnMapping mapping12 = new SqlBulkCopyColumnMapping("SchoolType", "SchoolLevel2");
            SqlBulkCopyColumnMapping mapping13 = new SqlBulkCopyColumnMapping("GOVTDISECODE", "SchoolCodeTemp");
            SqlBulkCopyColumnMapping mapping14 = new SqlBulkCopyColumnMapping("Management", "ManagementType");
            SqlBulkCopyColumnMapping mapping15 = new SqlBulkCopyColumnMapping("OPERATIONAL", "WorkingStatus");
            SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlHelper.mainConnectionString);
            bulkCopy.BatchSize = 100;
            bulkCopy.BulkCopyTimeout = 5;
            bulkCopy.ColumnMappings.Add(mapping01);
            bulkCopy.ColumnMappings.Add(mapping02);
            bulkCopy.ColumnMappings.Add(mapping03);
            bulkCopy.ColumnMappings.Add(mapping04);
            bulkCopy.ColumnMappings.Add(mapping05);
            bulkCopy.ColumnMappings.Add(mapping06);
            bulkCopy.ColumnMappings.Add(mapping07);
            bulkCopy.ColumnMappings.Add(mapping08);
            bulkCopy.ColumnMappings.Add(mapping09);
            bulkCopy.ColumnMappings.Add(mapping10);
            bulkCopy.ColumnMappings.Add(mapping11);
            bulkCopy.ColumnMappings.Add(mapping12);
            bulkCopy.ColumnMappings.Add(mapping13);
            bulkCopy.ColumnMappings.Add(mapping14);
            bulkCopy.ColumnMappings.Add(mapping15);
            bulkCopy.DestinationTableName = "T_mstSchool";
            bulkCopy.NotifyAfter = 200;
            bulkCopy.WriteToServer(dt);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string INSERT_CLTData(DataTable dt, string strSP_Name, string strParentTable_Name, string strtemptblmstGroupChk, string strtemptblmstGroup, string Flag, SqlConnection ConStr)
    {
        string RowAffect = string.Empty;

        try
        {
            ConStr.Open();

            using (SqlCommand dbSqlCommand = new SqlCommand(strtemptblmstGroupChk, ConStr))
            { dbSqlCommand.ExecuteNonQuery(); }
            using (SqlCommand dbSqlCommand = new SqlCommand(strtemptblmstGroup, ConStr))
            { dbSqlCommand.ExecuteNonQuery(); }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(ConStr))
            {
                bulkCopy.DestinationTableName = "temp_" + strParentTable_Name + "";
                bulkCopy.BulkCopyTimeout = 3000000;
                bulkCopy.ColumnMappings.Add("Villagecode", 0);
                bulkCopy.ColumnMappings.Add("SchoolCode", 1);
                bulkCopy.ColumnMappings.Add("Year", 2);
                bulkCopy.ColumnMappings.Add("UniqueChildCode", 3);
                bulkCopy.ColumnMappings.Add("ChildCode", 4);
                bulkCopy.ColumnMappings.Add("Class", 5);
                bulkCopy.ColumnMappings.Add("ChildName", 6);
                bulkCopy.ColumnMappings.Add("Gender", 7);
                bulkCopy.ColumnMappings.Add("SocialCategory", 8);
                bulkCopy.ColumnMappings.Add("Serial", 9);
                bulkCopy.ColumnMappings.Add("Term", 10);
                bulkCopy.ColumnMappings.Add("Hindi", 11);
                bulkCopy.ColumnMappings.Add("English", 12);
                bulkCopy.ColumnMappings.Add("Math", 13);
                bulkCopy.ColumnMappings.Add("EvaluationDate", 14);
                bulkCopy.ColumnMappings.Add("CreatedBy", 15);
                bulkCopy.ColumnMappings.Add("CreatedDate", 16);

                bulkCopy.WriteToServer(dt);
            }



            using (SqlCommand dbSqlCommand = new SqlCommand())
            {
                dbSqlCommand.Connection = ConStr;
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = strSP_Name;
                dbSqlCommand.Parameters.Add("@Flag", Flag);
                System.Data.SqlClient.SqlParameter pRowsAffected1 = new SqlParameter("@RowAfected", System.Data.SqlDbType.Int);
                pRowsAffected1.Direction = System.Data.ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(pRowsAffected1);
                dbSqlCommand.CommandTimeout = 3000000;
                int _returnRow = dbSqlCommand.ExecuteNonQuery();
                //SqlDataAdapter ad = new SqlDataAdapter(dbSqlCommand);
                //DataTable dttemp = new DataTable();
                //ad.Fill(dttemp);
                RowAffect = Convert.ToString(_returnRow).Trim();
            }
            ConStr.Close();
        }
        catch (Exception _ex)
        {
            return "5000";
            ConStr.Close();
        }
        finally
        {
        }
        return RowAffect;
    }

    public static object Setnullvalue(string p)
    {
        throw new NotImplementedException();
    }
    public int Insert_TaskForce_Add_Update(string GRMtg_UID, string GR_UID, string Date, string Minutes, string MtgType, bool MinutesUpload, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "Insert_TaskForce_Add_Update";
                dbSqlCommand.Parameters.AddWithValue("@GRMtg_UID", GRMtg_UID);
                dbSqlCommand.Parameters.AddWithValue("@GR_UID", GR_UID);
                dbSqlCommand.Parameters.AddWithValue("@Date", Date);
                dbSqlCommand.Parameters.AddWithValue("@Minutes", Minutes);
                dbSqlCommand.Parameters.AddWithValue("@MtgType", MtgType);
                dbSqlCommand.Parameters.AddWithValue("@MinutesUpload", MinutesUpload);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }
    public int Insert_Meeting_Add_Update(string GRMtgAction_UID, string GRMtg_UID, string ActionPoint, string Status, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "Insert_Meeting_Add_Update";
                dbSqlCommand.Parameters.AddWithValue("@GRMtgAction_UID", GRMtgAction_UID);
                dbSqlCommand.Parameters.AddWithValue("@GRMtg_UID", GRMtg_UID);
                dbSqlCommand.Parameters.AddWithValue("@ActionPoint", ActionPoint);
                dbSqlCommand.Parameters.AddWithValue("@Status", Status);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                int _returnRow = dbSqlCommand.ExecuteNonQuery();
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Insert_Update_MOU(string GR_UID, string StateCode, string DistrictCode, string StartDate, string EndDate, bool MOU, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "Insert_Update_MOU";
                dbSqlCommand.Parameters.AddWithValue("@StateCode", StateCode);
                dbSqlCommand.Parameters.AddWithValue("@DistrictCode", DistrictCode);
                dbSqlCommand.Parameters.AddWithValue("@StartDate", StartDate);
                dbSqlCommand.Parameters.AddWithValue("@EndDate", EndDate);
                dbSqlCommand.Parameters.AddWithValue("@MOU", MOU);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                dbSqlCommand.Parameters.AddWithValue("@GR_UID", GR_UID);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Insert_GovtRep_Add_Update(string GRRep_UID, string GR_UID, string Level, string Desig, string Name1, string Phone, string E_mail, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Insert_GovtRep_Add_Update]";
                dbSqlCommand.Parameters.AddWithValue("@GRRep_UID", GRRep_UID);
                dbSqlCommand.Parameters.AddWithValue("@GR_UID", GR_UID);
                dbSqlCommand.Parameters.AddWithValue("@Level", Level);
                dbSqlCommand.Parameters.AddWithValue("@Desig", Desig);
                dbSqlCommand.Parameters.AddWithValue("@Name1", Name1);
                dbSqlCommand.Parameters.AddWithValue("@Phone", Phone);
                // dbSqlCommand.Parameters.AddWithValue("@Name1", Name1);
                dbSqlCommand.Parameters.AddWithValue("@E_mail", E_mail);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Update_AnnualExamStatus(string str, string UID, string Flag)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Annual_Exam_Status]";
                dbSqlCommand.Parameters.AddWithValue("@str", str);
                dbSqlCommand.Parameters.AddWithValue("@UID", UID);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Update_AnnualExamStatusNew(string str, string UID, string Flag, string ReasonforAbsent, string ReasonOther)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Annual_Exam_Status]";
                dbSqlCommand.Parameters.AddWithValue("@str", str);
                dbSqlCommand.Parameters.AddWithValue("@UID", UID);
                dbSqlCommand.Parameters.AddWithValue("@Flag", Flag);
                dbSqlCommand.Parameters.AddWithValue("@ReasonforAbsent", ReasonforAbsent);
                dbSqlCommand.Parameters.AddWithValue("@ReasonOther", ReasonOther);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Update_SchoolWorkingStatus(string SchoolCode, int WorkingStatus, int MangmentType, int GKP, int GKPLevel, int SchoolType)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_School_WorkingStatus]";
                dbSqlCommand.Parameters.AddWithValue("@SchoolCode", SchoolCode);
                dbSqlCommand.Parameters.AddWithValue("@WorkingStatus", WorkingStatus);
                dbSqlCommand.Parameters.AddWithValue("@MangmentType", MangmentType);
                dbSqlCommand.Parameters.AddWithValue("@GKP", GKP);
                dbSqlCommand.Parameters.AddWithValue("@GKPLevel", GKPLevel);
                dbSqlCommand.Parameters.AddWithValue("@SchoolType", SchoolType);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Update_ModuelStatus(string FromName, string DistCode, string Fyear, int Pmonth)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_ModualLock]";
                dbSqlCommand.Parameters.AddWithValue("@FromName", FromName);
                dbSqlCommand.Parameters.AddWithValue("@DistCode", DistCode);
                dbSqlCommand.Parameters.AddWithValue("@Fyear", Fyear);
                dbSqlCommand.Parameters.AddWithValue("@Pmonth", Pmonth);
                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public int Update_VillageCluster(string VillageCode, string ClusterCode, string VillageGeography, string VillageOperational)
    {
        SqlConnection dbSqlconnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            dbSqlconnection.Open();
            using (SqlCommand dbSqlCommand = (SqlCommand)dbSqlconnection.CreateCommand())
            {
                dbSqlCommand.CommandType = CommandType.StoredProcedure;
                dbSqlCommand.CommandText = "[Update_Village_Cluster]";
                dbSqlCommand.Parameters.AddWithValue("@VillageCode", VillageCode);
                dbSqlCommand.Parameters.AddWithValue("@ClusterCode", ClusterCode);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeography", VillageGeography);
                dbSqlCommand.Parameters.AddWithValue("@VillageGeographyOperational", VillageOperational);

                SqlParameter ReturnAffectedRows = new SqlParameter("@RowAffected", System.Data.SqlDbType.Int);
                ReturnAffectedRows.Direction = ParameterDirection.Output;
                dbSqlCommand.Parameters.Add(ReturnAffectedRows);
                dbSqlCommand.ExecuteNonQuery();
                int _returnRow = Convert.ToInt32(ReturnAffectedRows.Value);
                return _returnRow;
            }
        }
        catch (SqlException exp)
        {
            throw;
        }
        finally
        {
            dbSqlconnection.Dispose();
        }
    }

    public DataSet SP_Check_District_Excel_Import()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "GovUploadDate";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet SP_Check_District_Excel_Import_IN_Maintable()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "SP_Check_District_Excel_Import_MainTable";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet rptUpdateUniqueCode()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "rptUpdateUniqueCode";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet rptUinqueGenerate()
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandTimeout = 0;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "rptUinqueGenerate";
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet InsertRole(string RoleName, string RoleLevel)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Sp__Add_UserRole]";
            sqlcmd.Parameters.AddWithValue("@RoleName", RoleName);
            sqlcmd.Parameters.AddWithValue("@RoleLevel", RoleLevel);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_ActivityUpdate_Baseline_BO(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_ActivityUpdate_Baseline_BO";
            sqlcmd.Parameters.AddWithValue("@Baseline_BO", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_TblActivityUpdate_Office_BO(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_TblActivityUpdate_Office_BO";
            sqlcmd.Parameters.AddWithValue("@Office_BO", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_DTDMobileActivity2018(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2018";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_DTDMobileActivity2020(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2020";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_DTDMobileActivity2020New(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2020New";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }



    public DataSet Tablet_Post_Session_DTDMobileActivity2020NewChange(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2020ChangeNew";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }



    public DataSet Tablet_Post_Session_DTDMobileActivity2021(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2021";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_DTDMobileActivity2022(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2022";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_DTDMobileActivity2023(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2023";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_DTDMobileActivity2022Verification(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2022Verification";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_DTDMobileActivity2023Verification(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_DTDMobileActivity2023Verification";
            sqlcmd.Parameters.AddWithValue("@tblDTDjj", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet TabletActivityUpdateVillage(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_ActivityVillage";
            sqlcmd.Parameters.AddWithValue("@tblActivity_Village", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_AdtionalAddVillage(DataTable DttblActivityUpdate_Village, string iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_AdtionalAddVillage";
            sqlcmd.Parameters.AddWithValue("@AddVillage", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_User_LoginNew(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_User_LoginNew]";
            sqlcmd.Parameters.AddWithValue("@Tbl_User_Login", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Tbl_GKPNew(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Tbl_GKPNew]";
            sqlcmd.Parameters.AddWithValue("@Tbl_GKP", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Tbl_GKPNew20190725(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Tbl_GKPNew20190725]";
            sqlcmd.Parameters.AddWithValue("@Tbl_GKP", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Tbl_GKPNewBO(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Tbl_GKPNewBO20190904]";
            sqlcmd.Parameters.AddWithValue("@Tbl_GKP", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Tbl_GKPNewBO20190904(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Tbl_GKPNewBO20190904]";
            sqlcmd.Parameters.AddWithValue("@Tbl_GKP", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Tbl_GKP(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Tbl_GKP]";
            sqlcmd.Parameters.AddWithValue("@Tbl_GKP", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_User_LoginNewDateAsInt(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_User_LoginNewDateAsInt]";
            sqlcmd.Parameters.AddWithValue("@Tbl_User_Login", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_User_LoginNewDateAsInt2020(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_User_LoginNewDateAsInt2020]";
            sqlcmd.Parameters.AddWithValue("@Tbl_User_Login", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_TblCommunitySMC(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Insert_TblCommunitySMC]";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", dtTbl_User_Login);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_TblSMCAttendance(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Insert_TblSMCAttendance]";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", dtTbl_User_Login);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_ChildRegistration(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Insert_Update_ChildRegistration]";
            sqlcmd.Parameters.AddWithValue("@tblLSGChildAttendance", dtTbl_User_Login);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_ChildRegistrationLCGAttendance(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Insert_Update_ChildRegistrationLCGAttendance]";
            sqlcmd.Parameters.AddWithValue("@tblLSGChildAttendance", dtTbl_User_Login);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_tblInfluencerProfile(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_Insert_tblInfluencerProfile]";
            sqlcmd.Parameters.AddWithValue("@tblInfluencerProfile", dtTbl_User_Login);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_User_LoginNewDateAsIntBO(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Session_User_LoginNewDateAsIntBO]";
            sqlcmd.Parameters.AddWithValue("@Tbl_User_Login", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_TblTrackRandomLatLong(DataTable dtTbl_User_Login, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_TblTrackRandomLatLong]";
            sqlcmd.Parameters.AddWithValue("@Tbl_User_Login", dtTbl_User_Login);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_ActivityUpdate(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_ActivityUpdate";
            sqlcmd.Parameters.AddWithValue("@tblActivity_School", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public bool BindDLLMasterTable(string dtname, string fieldname, DataTable dt, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        //string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        //DataTable dt = dbt.VGridFill(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    public DataTable CreateDataTable()
    {

        DataTable dtYear = new DataTable();
        dtYear.Columns.Add("Type", System.Type.GetType("System.String"));

        dtYear.Columns.Add("ID", System.Type.GetType("System.Int32"));
        return dtYear;
    }
    public bool BindDLLYear(string dtname, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
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

                dr = dtYear.NewRow();
                dr["Type"] = GivenYear - 2 + "-" + Convert.ToString((GivenYear - 2 + 1));
                dr["ID"] = y - 2;
                dtYear.Rows.Add(dr);
                //get last  two digits (eg: 10 from 2010);

            }
            else
            {
                //Int32 m7 = y + 1;
                //dr = dtYear.NewRow();
                //dr["Type"] = Convert.ToString((y)) + "-" + m7.ToString();
                ////y = y - 1;
                //dr["ID"] = y;
                //dtYear.Rows.Add(dr);

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


        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        //string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        //DataTable dt = dbt.VGridFill(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr1;
            dr1 = dtYear.NewRow();
            dr1[textData] = "--" + ZeroIndex + "--";
            dr1[valData] = "0";
            dtYear.Rows.InsertAt(dr1, 0);
            dtYear.AcceptChanges();
        }
        if (dtYear.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dtYear;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    public DataSet Tablet_Post_Session_tblDTDNew(DataTable dttblDTD, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        DataSet result;
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            DataSet dataSet = new DataSet();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "Tablet_Post_Session_tblDTDNew";
            sqlCommand.Parameters.AddWithValue("@tblDTD", dttblDTD);
            sqlCommand.Parameters.AddWithValue("@UserID", iUserID);
            sqlCommand.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
            result = dataSet;
        }
        catch (SqlException ex)
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            throw;
        }
        finally
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        return result;
    }

    public DataSet Tablet_Post_Session_tblDTDNewContact(DataTable dttblDTD, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        DataSet result;
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            DataSet dataSet = new DataSet();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "Tablet_Post_Session_tblDTDContact";
            sqlCommand.Parameters.AddWithValue("@tblDTD", dttblDTD);
            sqlCommand.Parameters.AddWithValue("@UserID", iUserID);
            sqlCommand.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(dataSet);
            result = dataSet;
        }
        catch (SqlException ex)
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            throw;
        }
        finally
        {
            if (sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
        return result;
    }
    public bool BindDLLSchool(string dtname, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;


        string strQry = "Select  distinct " + fieldname + " from " + dtname + " " + conditions + " " + orberbyfields + " " + orderbys + "";
        DataTable dt = LoadData(strQry);
        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "0";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            if (ZeroIndex != "")
            {
                DataRow dr;
                dr = dt.NewRow();
                dr[textData] = "--" + "Other" + "--";
                dr[valData] = "99";
                dt.Rows.InsertAt(dr, dt.Rows.Count + 1);
                dt.AcceptChanges();
            }
        }
        else
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + "Other" + "--";
            dr[valData] = "99";
            dt.Rows.InsertAt(dr, 1);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

    }

    public bool BindDLLDatatableV(string dtname, DataTable dt, string fieldname, string Condition, string orberbyfield, string orderby, DropDownList ddl, string textData, string valData, string ZeroIndex)
    {
        bool status = false;
        string conditions = Condition == "" ? "" : " where " + Condition;
        string orberbyfields = orberbyfield == "" ? "" : " order by " + orberbyfield;
        string orderbys = orderby == "" ? "" : orderby;



        if (ZeroIndex != "")
        {
            DataRow dr;
            dr = dt.NewRow();
            dr[textData] = "--" + ZeroIndex + "--";
            dr[valData] = "--" + ZeroIndex + "--";
            dt.Rows.InsertAt(dr, 0);
            dt.AcceptChanges();
        }
        if (dt.Rows.Count > 0)
        {
            ddl.DataTextField = textData;
            ddl.DataValueField = valData;

            ddl.DataSource = dt;
            ddl.DataBind();
            status = true;
        }
        return status;

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
    public static DataTable Generate_Financial_Yearsd()
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
    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2020(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2020";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2023";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2023New(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2023New";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp20232024New(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp20232024New";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp202320242007New(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp202320242007New";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdatetblOOSG(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdatetblOOSG";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_UpdatetblOOSGNew(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdatetblOOSGNew";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_UpdatetblOOSGNew2022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdatetblOOSG2022";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdatetblChildOOSG(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdatetblChildOOSG";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrion(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentRetion";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrionMmain(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentMain";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrion2022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentRetion2022";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrionMmain2022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentMain2022";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_SafetySecurity(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_SafetySecurity";
            sqlcmd.Parameters.AddWithValue("@tblSafetySecurity_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttblChildRegistration(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistration";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistration", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_InserttblChildRegistrationNew(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationNew";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistration", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_InserttblChildRegistrationNew2021(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationNew2021";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistration", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationNew20212022New(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationNew20212022New";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistration", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationSchool(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationSchool";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationSchool", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_InserttblChildRegistrationNew20212022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationNew20212022";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistration", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }



    public DataSet Tablet_Post_Session_InserttblVisitors(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblVisitors";
            sqlcmd.Parameters.AddWithValue("@tblVisitors", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttblVisitorsSchool(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblVisitorsSchool";
            sqlcmd.Parameters.AddWithValue("@tblVisitorsSchool", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_InserttbltblChildRegistrationBalsabha(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationBalsabha";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationBalsabha", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationBalsabhaKGBV(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationBalsabhaKGBV";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationBalsabha", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_InserttbltblChildAttendanceLifeskill(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceLifeskill";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceLifeskill", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttbltblChildAttendanceLifeskill2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceLifeskill2023";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceLifeskill", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_InserttbltblChildAttendanceLifeskill2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceLifeskill2024";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceLifeskill", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceLifeskill2024KGBV(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceLifeskill2024KGBV";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceLifeskill", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_InsertblCLLSG(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblCLLSG";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendance", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }



    public DataSet Tablet_Post_Session_InserttblChildAttendance(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendance";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendance", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttblChildAttendance2020(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendance2020";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendance", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttblChildAttendance2021(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendance2021";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendance", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttblChildAttendance20212022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendance20212022";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendance", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_InserttblChildAttendance20212022New(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendance20212022New";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendance", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceSchool(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceSchool";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceSchool", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdateVerification(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Verification";
            sqlcmd.Parameters.AddWithValue("@tblVerification_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationAgp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp2022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp2022";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationAgp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationAgp2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationAgp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblVisitorsAGP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblVisitorsAGP";
            sqlcmd.Parameters.AddWithValue("@tblVisitorsAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblAttendanceImage(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblAttendanceImage";
            sqlcmd.Parameters.AddWithValue("@tblVisitorsAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblAttendanceImageSchool(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblAttendanceImageSchool";
            sqlcmd.Parameters.AddWithValue("@tblAttendanceImageSchool", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP2022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP2022";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceAGP2023";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_SMCAttendance2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_SMCAttendance2025";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_SMCAttendance2025Child(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_SMCAttendance2025Child";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_SMCAttendance2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_SMCAttendance2023";
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceAGP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }



    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationGKP2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationGKP2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationGKPGyanodaya(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationGKPGyanodaya";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationGKP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationGKP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildRegistrationGKPLus(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildRegistrationGKPPlus";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlus(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlus";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusMP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusMP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusUP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusUP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusRaj(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKPPlusRaj";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP202387(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP20222023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_tblRandomSessionPhoto(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_tblRandomSessionPhoto";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022New(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022New";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022UP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022UP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2023UP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2023UP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025UP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025UP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022UPNew(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022UPNew";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022MP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022MP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGyanodayatemp(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGyanodayatemp";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblHousholdExpansion(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblHousholdExpansion";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblHousholdExpansion2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblHousholdExpansion2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblHousholdExpansion2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblHousholdExpansion2024";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblHousholdExpansion2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblHousholdExpansion2025";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblLoginReason2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblLoginReason2025";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrollSummary(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrollSummary";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrollSummary2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrollSummary2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblEnrollSummary2026(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrollSummary2026";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblEnrollSummaryBO(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrollSummaryBO";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyExpansion(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyExpansion";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyExpansion2023(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyExpansion2023";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyExpansion2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyExpansion2024";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblSurveyExpansion2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblSurveyExpansion2025";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetails(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetails";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetails2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetails2024";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetailsNew(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblExpOtherVillageDetailsNew";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblBalikaAndInfluencer(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblBalikaAndInfluencer";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblBalikaAndInfluencer2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblBalikaAndInfluencer2024";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblAudioRecording(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblAudioRecording";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2023MP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2023MP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025MP(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2025MP";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblChildGyanodayaAttendanceGKP2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGyanodaya";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022MPNew(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblChildAttendanceGKP2022MPNew";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdateVerification2021(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Verification2021";
            sqlcmd.Parameters.AddWithValue("@tblVerification_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdateVerificationNew(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_VerificationNew";
            sqlcmd.Parameters.AddWithValue("@tblVerification_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public static DataTable Generate_Post_Financial_Years()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Year + 1;
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
    public DataSet Tablet_Post_tblQuestion(DataTable dttblQuestion, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "[Tablet_Post_Insert_Update_tblQuestion]";
            sqlcmd.Parameters.AddWithValue("@dttblQuestion", dttblQuestion);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }



    }


    public DataSet Tablet_Post_Session_Insert_Update_tblOOSC2024(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSC2024";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblOOSC2026(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSC2026";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblOOSCNew2024(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSCNew2024";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblOOSCNew2026(DataTable DttblActivityUpdate_School, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblOOSCNew2026";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_TblActivityUpdate_Office_BO2024(DataTable DttblActivityUpdate_Village, int iUserID, string sJason)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_TblActivityUpdate_Office_BO2024";
            sqlcmd.Parameters.AddWithValue("@Office_BO", DttblActivityUpdate_Village);
            sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblPlanActivity(DataTable DttblActivityUpdate_School)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblPlanActivity";
            sqlcmd.Parameters.AddWithValue("@tblPlanActivity", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tblPlanActivity2025(DataTable DttblActivityUpdate_School)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblPlanActivity2025";
            sqlcmd.Parameters.AddWithValue("@tblPlanActivity", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblPlanActivity2026(DataTable DttblActivityUpdate_School)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblPlanActivity2026";
            sqlcmd.Parameters.AddWithValue("@tblPlanActivity", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_Tbl_Photo_Attendance(DataTable DttblActivityUpdate_School, DataTable Dttbltraning)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_Tbl_Photo_Attendance";
            sqlcmd.Parameters.AddWithValue("@tblPlanActivity", DttblActivityUpdate_School);
            sqlcmd.Parameters.AddWithValue("@@tblTrainaudit", Dttbltraning);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_Tbl_Attendance_Audit(DataTable DttblActivityUpdate_School)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_Tbl_Attendance_Audit";
            sqlcmd.Parameters.AddWithValue("@tblPlanActivity", DttblActivityUpdate_School);

            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrionMmain2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentMain2024";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrion2024(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentRetion2024";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_UpdatetblRetrion2026(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_UpdateEnlolmentRetion2026";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_EnrolmentModified(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_EnrolmentModified";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2025(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2025";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public static DataTable Generate_Financial_Years()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Type");
        DataRow dr;
        int stYr = DateTime.Today.Month < 4 ? DateTime.Today.Year : DateTime.Today.Year + 2;
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

    public DataSet tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024(DataTable DtttblTravelMatrixDeatils2024, DataTable DtttblTravelMatrixExpens, DataTable DtttblTravelMatrixPerDiem, DataTable dtTravelConsent)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024";
            sqlcmd.Parameters.AddWithValue("@MatrixDeatils2024", DtttblTravelMatrixDeatils2024);
            sqlcmd.Parameters.AddWithValue("@MatrixExpens", DtttblTravelMatrixExpens);
            sqlcmd.Parameters.AddWithValue("@MatrixPerDiem", DtttblTravelMatrixPerDiem);
            sqlcmd.Parameters.AddWithValue("@TravelConsentTemp", dtTravelConsent);

            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }


    public DataSet tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2026(DataTable DtttblTravelMatrixDeatils2024, DataTable DtttblTravelMatrixExpens, DataTable DtttblTravelMatrixPerDiem, DataTable dtTravelConsent)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2026";
            sqlcmd.Parameters.AddWithValue("@MatrixDeatils2024", DtttblTravelMatrixDeatils2024);
            sqlcmd.Parameters.AddWithValue("@MatrixExpens", DtttblTravelMatrixExpens);
            sqlcmd.Parameters.AddWithValue("@MatrixPerDiem", DtttblTravelMatrixPerDiem);
            sqlcmd.Parameters.AddWithValue("@TravelConsentTemp", dtTravelConsent);

            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }



    public DataSet Tablet_Post_Session_Insert_Update_SessionWiseDetails(DataTable DtttblTravelMatrixDeatils2024, DataTable DtttblTravelMatrixExpens)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_SessionWiseDetails";
            sqlcmd.Parameters.AddWithValue("@SessionWiseDetails", DtttblTravelMatrixDeatils2024);
            sqlcmd.Parameters.AddWithValue("@LocationDetails", DtttblTravelMatrixExpens);


            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_SessionWiseDetails2025(DataTable DtttblTravelMatrixDeatils2024, DataTable DtttblTravelMatrixExpens)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_SessionWiseDetails2025";
            sqlcmd.Parameters.AddWithValue("@SessionWiseDetails", DtttblTravelMatrixDeatils2024);
            sqlcmd.Parameters.AddWithValue("@LocationDetails", DtttblTravelMatrixExpens);


            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024without(DataTable DtttblTravelMatrixDeatils2024)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024WithoutExpen";
            sqlcmd.Parameters.AddWithValue("@MatrixDeatils2024", DtttblTravelMatrixDeatils2024);
            // sqlcmd.Parameters.AddWithValue("@MatrixExpens", DtttblTravelMatrixExpens);
            //sqlcmd.Parameters.AddWithValue("@MatrixPerDiem", DtttblTravelMatrixPerDiem);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024withEX(DataTable DtttblTravelMatrixDeatils2024, DataTable DtttblTravelMatrixExpens)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024WithExprn";
            sqlcmd.Parameters.AddWithValue("@MatrixDeatils2024", DtttblTravelMatrixDeatils2024);
            sqlcmd.Parameters.AddWithValue("@MatrixExpens", DtttblTravelMatrixExpens);
            // sqlcmd.Parameters.AddWithValue("@MatrixPerDiem", DtttblTravelMatrixPerDiem);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024Pendim(DataTable DtttblTravelMatrixDeatils2024, DataTable DtttblTravelMatrixPerDiem)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblTravelMatrixDeatils2024Withped";
            sqlcmd.Parameters.AddWithValue("@MatrixDeatils2024", DtttblTravelMatrixDeatils2024);
            // sqlcmd.Parameters.AddWithValue("@MatrixExpens", DtttblTravelMatrixExpens);
            sqlcmd.Parameters.AddWithValue("@MatrixPerDiem", DtttblTravelMatrixPerDiem);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblPanchayatMeetingTem(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblPanchayatMeetingTem";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblRatriChaupalTemp(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblRatriChaupalTemp";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblEnrollmentRallytempType(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrollmentRallytempType";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
    public DataSet Tablet_Post_Session_Insert_Update_tblFemaleDetails(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblFemaleDetails";
            sqlcmd.Parameters.AddWithValue("@tblChildRegFKP", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public static string GetImagePath(string key)
    {
        switch (key)
        {
            case "GKPPath":
                return "~/GKP";

            case "TravelPath":
                return "~/Travel";

            case "TravelvouchersFCPath":
                return "~/TravelvouchersFC";

            case "TravelvouchersPath":
                return "~/Travel vouchers";

            case "TraningPath":
                return "~/Traning";

            case "TabletImagePath":
                return "~/TabletImage";

            case "AudioFilePath":
                return "~/AudioFile";

            case "LSEPath":
                return "~/LSE";

            case "TrainingPath":
                return "~/Traning";

            case "ExportPath":
                return "~/Export";

            case "DataBackupPath":
                return "~/DataBackup/";

            case "MouPath":
                return "~/Mou//";

            case "MouSinglePath":
                return "~/Mou";

            case "EmpImg":
                return "~/EmpImg/";

            case "ImgPage":
                return "~/images";

            case "EnrolmentDetailsPath":
                return "~/EnrolmentDetails";

            case "QRCodePath":
                return "~/QRCode";

            case "ImportExcelPath":
                return "~/ImportExcel";

            case "SurveyFilesPath":
                return "~/SurveyFiles";

            case "SurveyPath":
                return "~/Survey";

            case "TBTriningPath":
                return "~/TBTrining";

            case "GeoPublishPath":
                return "~/GeoPublish";

            case "SurveyAnsPath":
                return "~/SurveyAns";

            case "GeoTempPath":
                return "~/GeoTemp";


            default:
                return string.Empty;
        }
    }


    public DataSet Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2026(DataTable DttblEnrolment_Temp)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblEnrolment_Temp2026";
            sqlcmd.Parameters.AddWithValue("@tblEnrolment_Temp", DttblEnrolment_Temp);
            //sqlcmd.Parameters.AddWithValue("@UserID", iUserID);
            //sqlcmd.Parameters.AddWithValue("@JSON", sJason);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw e;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_InsertUpdateVidhyaSabhaGKP(DataTable tblVidhyaSabhaGKP, DataTable tblUtsavGKP, DataTable tblChildPreparationGKP)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tblVidhyaSabhaGKP";
            sqlcmd.Parameters.AddWithValue("@tblVidhyaSabhaGKP", tblVidhyaSabhaGKP);
            sqlcmd.Parameters.AddWithValue("@tblUtsavGKP", tblUtsavGKP);
            sqlcmd.Parameters.AddWithValue("@tblChildPreparationGKP", tblChildPreparationGKP);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw e;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }

    public DataSet Tablet_Post_Session_Insert_Update_tbldAttendanceGKPBO(DataTable tblVidhyaSabhaGKP, DataTable tblUtsavGKP, DataTable tblChildPreparationGKP)
    {
        SqlConnection sqlConnection = new SqlConnection(SqlHelper.mainConnectionString);
        try
        {
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }

            DataSet dbSqlDataSet = new DataSet();
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.Connection = sqlConnection;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = "Tablet_Post_Session_Insert_Update_tbldAttendanceGKPBO";
            sqlcmd.Parameters.AddWithValue("@tblChildRegistrationGKPBO", tblVidhyaSabhaGKP);
            sqlcmd.Parameters.AddWithValue("@tblChildAttendanceGKPBO", tblUtsavGKP);
            sqlcmd.Parameters.AddWithValue("@tblClassAttendanceGKPBO", tblChildPreparationGKP);
            SqlDataAdapter da = new SqlDataAdapter(sqlcmd);
            da.Fill(dbSqlDataSet);
            return dbSqlDataSet;
        }
        catch (SqlException e)
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            throw e;
        }
        finally
        {
            if (!(sqlConnection.State == ConnectionState.Closed))
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

        }
    }
}

public class GoogleCaptchaResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }
}



