using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pral_work_1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            //status.Text = val1.ToString();
            if (Request.Cookies["UserSettings"] != null)
            {
                string userSettings,userid;
                if (Request.Cookies["UserSettings"]["Name"] != null)
                { 
                    userSettings = Request.Cookies["UserSettings"]["Name"];
                    userid = Request.Cookies["UserSettings"]["Id"];
                    status.Text = "Logged in user:"+userid+"-"+userSettings;
                }
                
            }
            if (!val1) {

                status.Text = "You are not logged in."; 
                guest.Text = "Use id=guest & password=guest";
            }
        }
    }
}