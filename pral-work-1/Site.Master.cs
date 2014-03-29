using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
namespace pral_work_1
{
    public partial class Site : System.Web.UI.MasterPage
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == true)
            {
                logout.Visible = true;
                login.Visible = false;
            }
            else {
                login.Visible = true;
                logout.Visible = false;
            }
        }

        protected void logout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("login.aspx", true);
        }

        protected void login_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx", true);
        }
    }
}