using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Data.OleDb;
using System.IO;
using Ionic.Zip;
using System.IO.Compression;


public partial class frmDownLoad : System.Web.UI.Page
{
    clsMain objMain = new clsMain();
    Comman objComman = new Comman();

    string conditions = "";
    string flag = "";
    Password objPass = new Password();
    public DataTable dtUserDeatils;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
            

        }
    }
    public void BackUpInformation()
    {
       
           
    }


    protected void btnsave_Click(object sender, EventArgs e)
    {
     
     
    }
  

   

   
}