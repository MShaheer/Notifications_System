using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.Configuration;
using System.Data;

namespace pral_work_1
{
    public partial class login : System.Web.UI.Page
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["notifications_db_cs"].ConnectionString;
        int lookupId;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            this.cmdLogin.ServerClick += new System.EventHandler(this.cmdLogin_ServerClick);
        } 

        private void cmdLogin_ServerClick(object sender, System.EventArgs e)
        {
            if (ValidateUser(txtUserName.Value, txtUserPass.Value))
            {
              HttpCookie myCookie = new HttpCookie("UserSettings");
               // myCookie["Font"] = "Arial";
               // myCookie["Color"] = "Blue";
                myCookie["Name"] = txtUserName.Value;
                myCookie["id"] = lookupId.ToString();
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
           
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Value,
                    chkPersistCookie.Checked);
            }

            else
                Response.Redirect("login.aspx", true);
        }

        private bool ValidateUser(string userName, string passWord)
        {
            SqlConnection conn;
            SqlCommand cmd,cmd2;
            string lookupPassword = null;
            

            // Check for invalid userName.
            // userName must not be null and must be between 1 and 15 characters.
            if ((null == userName) || (0 == userName.Length) || (userName.Length > 15))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of userName failed.");
                return false;
            }

            // Check for invalid passWord.
            // passWord must not be null and must be between 1 and 25 characters.
            if ((null == passWord) || (0 == passWord.Length) || (passWord.Length > 25))
            {
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Input validation of passWord failed.");
                return false;
            }

            try
            {
                // Consult with your SQL Server administrator for an appropriate connection
                // string to use to connect to your local SQL Server.
                conn = new SqlConnection(connectionString);
                conn.Open();

                // Create SqlCommand to select pwd field from users table given supplied userName.
                cmd = new SqlCommand("Select pwd from users where uname=@userName", conn);
                cmd.Parameters.Add("@userName", SqlDbType.VarChar, 25);
                cmd.Parameters["@userName"].Value = userName;

                // Execute command and fetch pwd field into lookupPassword string.
                lookupPassword = (string)cmd.ExecuteScalar();
                //lookupPassword = (string)cmd.

                // Retrieving Id of logged in user
                cmd2 = new SqlCommand("Select Id from users where uname=@userName AND pwd=@passWord", conn);
                cmd2.Parameters.Add("@userName", SqlDbType.VarChar, 25);
                cmd2.Parameters["@userName"].Value = userName;
                cmd2.Parameters.Add("@passWord", SqlDbType.VarChar, 25);
                cmd2.Parameters["@passWord"].Value = passWord;

                // Execute command and fetch pwd field into lookupPassword string.
                lookupId = (int)cmd2.ExecuteScalar();
                
                
                // Cleanup command and connection objects.
                cmd.Dispose();
                cmd2.Dispose();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                // Add error handling here for debugging.
                // This error message should not be sent back to the caller.
                System.Diagnostics.Trace.WriteLine("[ValidateUser] Exception " + ex.Message);
            }

            // If no password found, return false.
            if (null == lookupPassword || lookupId< 0)
            {
                // You could write failed login attempts here to event log for additional security.
                return false;
            }

            // Compare lookupPassword and input passWord, using a case-sensitive comparison.
            return (0 == string.Compare(lookupPassword, passWord, false));

        }
				



    }
}