using System;
using System.Web;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session check: not logged in -> go to login
        if (Session["User"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        lblUser.Text = "Welcome, " + Session["User"] + " (" + Session["Role"] + ") | Leave balance: " + Session["Balance"] + " days";
        lnkApprove.Visible = (string)Session["Role"] == "Admin";

        // Cookie: last visit time
        HttpCookie c = Request.Cookies["LastVisit"];
        if (c != null)
            lblLastVisit.Text = "Your last visit: " + Server.UrlDecode(c.Value);

        if (!IsPostBack)
        {
            HttpCookie nc = new HttpCookie("LastVisit", Server.UrlEncode(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt")));
            nc.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(nc);
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
}
