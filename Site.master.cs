using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class SiteNewMaster : System.Web.UI.MasterPage
{
    SqlConnection mycon = new SqlConnection(SqlHelper.mainConnectionString);
    protected void Page_Init(object sender, EventArgs e)
    {
        //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetNoStore();
    }
    private DataSet MenuDS = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["ForcePassword"]) == 1)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);

            if (!currentPage.Equals("Frm_Change_Password.aspx",
                StringComparison.OrdinalIgnoreCase))
            {
                ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "DisableMenu",
                @"
                $(document).ready(function () {
                    ShowForcePasswordPopup();

                    setTimeout(function () {
                        DisableMenu();
                    }, 500);
                });
                ",
                true);
                //ScriptManager.RegisterStartupScript(
                //   this,
                //   GetType(),
                //   "DisableMenu",
                //   "$(document).ready(function(){ DisableMenu(); });",
                //   true);
                //    ScriptManager.RegisterStartupScript(
                //     this,
                //     this.GetType(),
                //     "ShowPopup",
                //     "$(document).ready(function(){ ShowForcePasswordPopup(); });",
                //     true);
                //ScriptManager.RegisterStartupScript(
                // this,
                // GetType(),
                // "ShowPopup",
                // "ShowForcePasswordPopup();",
                // true);
            }
        }
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetAllowResponseInBrowserHistory(false);
        //Request.Browser.Adapters.Clear();
        if (!IsPostBack)
        {

            //lblUserName.Text = "";

            //string userlevel = Session["user_level"].ToString();
            //menuMaster.Visible = true;
            //SqlParameter[] parm = new SqlParameter[1];
            //{
            //    parm[0] = new SqlParameter("@userlevelright", SqlDbType.VarChar);
            //    parm[0].Value = userlevel;
            //};
            //MenuDS = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_Load_Menu]", parm);



            if (Convert.ToString(Session["username"]) != "")
            {

                bulidmenu();

                if (Session["user_level"] != null)
                {
                    // user_check();
                    lblUser.Text = Session["username"].ToString();
                    lblRole.Text = Session["UserRole"].ToString();
                    lblEmpName.Text = Session["EmpName"].ToString();
                }
                string pageName = this.MainContent.Page.GetType().FullName;
                string pageValue = setpagevalue(pageName);
                if (pageValue != "")
                {



                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }

    protected void btn_logout_Click(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();

        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();
        if (Request.Cookies["ASP.NET_SessionId"] != null)
        {
            HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", "");
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);
        }
        Response.Redirect("Login.aspx");
    }
    //private void BuildMenu(MenuItemCollection nodes, Int32 IntParent)
    //{
    //    Int32 MenuID;
    //    String MenuName;
    //    string MenuImg;
    //    String NavUrl;
    //    DataRow[] children = MenuDS.Tables[0].Select("ParentID='" + IntParent + "'");
    //    //no child nodes, exit function
    //    if (children.Length == 0) return;

    //    foreach (DataRow child in children)
    //    {

    //        MenuID = Convert.ToInt32(child.ItemArray[0]);
    //        MenuName = Convert.ToString(child.ItemArray[1]);
    //        MenuImg = Convert.ToString(child.ItemArray[2]);
    //        NavUrl = Convert.ToString(child.ItemArray[3]);


    //        // step 3
    //        MenuItem NewNode = new MenuItem(MenuName, MenuID.ToString(), "", "~/" + NavUrl);

    //        // step 4
    //        nodes.Add(NewNode);

    //        // step 5
    //        BuildMenu(NewNode.ChildItems, MenuID);
    //    }
    //}
    protected void bulidmenu()
    {
        Int32 MenuID;
        String MenuName;

        String NavUrl;
        String StyleClass;
        StringBuilder menuhtml = new StringBuilder();

        if (Session["user_level"] != null)
        {
            string userlevel = Session["user_level"].ToString();
            // menuMaster.Visible = true;
            SqlParameter[] parm = new SqlParameter[1];
            {
                parm[0] = new SqlParameter("@userlevelright", SqlDbType.VarChar);
                parm[0].Value = userlevel;
            }
            ;
            MenuDS = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_Load_Menu_test]", parm);
            //MenuDS = SqlHelper.GetDataSet(SqlHelper.mainConnectionString, CommandType.StoredProcedure, "[SP_Load_Menu]", parm);
        }

        DataRow[] Parent = MenuDS.Tables[0].Select("ParentID='0'");
        if (Parent.Length == 0) return;
        menuhtml.Append(" <ul class='nav navbar-nav'> ");
        foreach (DataRow UL in Parent)
        {
            MenuID = Convert.ToInt32(UL.ItemArray[0]);
            MenuName = Convert.ToString(UL.ItemArray[1]);

            NavUrl = Convert.ToString(UL.ItemArray[3]);
            StyleClass = Convert.ToString(UL.ItemArray[2]);
            DataRow[] children = MenuDS.Tables[0].Select("ParentID=" + MenuID + "");

            if (children.Length != 0)
            {
                menuhtml.Append(" <li class='dropdown'> ");
                menuhtml.Append(" <a class='dropdown-toggle' role='button' aria-haspopup='true' aria-expanded='false'  data-toggle='dropdown' href='" + NavUrl + "'>");
                menuhtml.Append("" + MenuName + " <span class='caret'>  </span></a>");
            }
            else
            {
                menuhtml.Append("  <li> ");
                menuhtml.Append(" <a href=" + NavUrl + ">");
                menuhtml.Append("" + MenuName + " </a>");
            }
            if (children.Length != 0)
            {

                menuhtml.Append(" <ul class='dropdown-menu'> ");
                foreach (DataRow child in children)
                {
                    MenuID = Convert.ToInt32(child.ItemArray[0]);
                    MenuName = Convert.ToString(child.ItemArray[1]);

                    NavUrl = Convert.ToString(child.ItemArray[3]);
                    DataRow[] Subchild = MenuDS.Tables[0].Select("ParentID=" + MenuID + "");
                    if (Subchild.Length != 0)
                    {
                        menuhtml.Append(" <li> ");
                        menuhtml.Append(" <a class='trigger right-caret' data-toggle='dropdown' href='" + NavUrl + "'>");
                        menuhtml.Append("" + MenuName + " </a>");

                    }
                    else
                    {

                        menuhtml.Append(" <li> ");
                        menuhtml.Append(" <a href=" + NavUrl + ">");
                        menuhtml.Append(" " + MenuName + "</a>");


                    }
                    if (Subchild.Length != 0)
                    {
                        menuhtml.Append(" <ul class='dropdown-menu sub-menu'> ");
                        foreach (DataRow Subchildren in Subchild)
                        {
                            MenuID = Convert.ToInt32(Subchildren.ItemArray[0]);
                            MenuName = Convert.ToString(Subchildren.ItemArray[1]);
                            //StyleClass = Convert.ToString(Subchildren.ItemArray[2]);
                            NavUrl = Convert.ToString(Subchildren.ItemArray[3]);
                            menuhtml.Append(" <li> ");
                            menuhtml.Append(" <a href=" + NavUrl + ">");
                            menuhtml.Append(" " + MenuName + "</a>");


                        }
                        menuhtml.Append(" </li> ");
                        menuhtml.Append("</ul>");

                    }
                    else
                    {
                        menuhtml.Append(" </li> ");
                    }
                }
                menuhtml.Append(" </li> ");
                menuhtml.Append("</ul>");

            }
            else
            {
                menuhtml.Append(" </li> ");
            }

        }
        menuhtml.Append(" </ul> ");


        myNavbar.InnerHtml = menuhtml.ToString();
    }

    public int LoginLogoutInsertUpdate(string UserID, string IPAddess)
    {
        SqlCommand dbSqlCommand;
        using (dbSqlCommand = new SqlCommand())
            dbSqlCommand.Connection = mycon;
        if (mycon.State == ConnectionState.Closed)
            mycon.Open();
        dbSqlCommand.CommandType = CommandType.StoredProcedure;
        dbSqlCommand.CommandText = "rptUserLoginLogout";
        dbSqlCommand.Parameters.Add("@UserID", SqlDbType.VarChar).Value = "";
        dbSqlCommand.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = "";
        dbSqlCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "U";
        dbSqlCommand.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = Convert.ToString(Session["LOGID"]);

        System.Data.SqlClient.SqlParameter pRowsAffected = new SqlParameter("@output", System.Data.SqlDbType.Int);
        pRowsAffected.Direction = System.Data.ParameterDirection.Output;
        dbSqlCommand.Parameters.Add(pRowsAffected);
        try
        {
            dbSqlCommand.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return -1;
        }
        return Convert.ToInt32(pRowsAffected.Value);
    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {

        int kkk = LoginLogoutInsertUpdate("", "");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();

        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();
        //HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Session.RemoveAll();
        Session.Clear();
        Session.Abandon();



        Response.Redirect("~/Login.aspx");
    }
    public string setpagevalue(string pagename)
    {
        string valdata = pagename;
        switch (valdata)
        {
            case "ASP.default_aspx": return "Home Page";
            case "ASP.homepage_aspx": return "Home Page";

            case "ASP.userreg_aspx": return "New User Registration";

            case "ASP.tokengenrate_aspx": return "Token Generate";


            //


            //default
            default: return string.Empty;

        }
    }

    public void DisableAllMenus()
    {
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "DisableMenu",
            @"
        $(document).ready(function () {

            // Disable all menu links
            $('.clsallmenuslist').find('a').each(function () {

                $(this).css({
                    'pointer-events':'none',
                    'cursor':'not-allowed',
                    'opacity':'0.5'
                });

                $(this).removeAttr('href');

                $(this).click(function(e){
                    e.preventDefault();
                    return false;
                });
            });

            // Disable dropdowns
            $('.clsallmenuslist .dropdown-toggle')
                .removeAttr('data-toggle')
                .removeAttr('data-bs-toggle');

            // Disable logout button
            $('#" + btnlogout.ClientID + @"').css({
                'pointer-events':'none',
                'cursor':'not-allowed',
                'opacity':'0.5'
            });

        });
        ",
            true);
    }
}
