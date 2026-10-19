using System;
using System.Collections.Generic;
using System.Web;

public partial class LoginPage : System.Web.UI.Page
{
    // username -> { password, role, leave balance }
    static readonly Dictionary<string, string[]> Users = new Dictionary<string, string[]>
    {
        { "admin",    new[] { "admin123", "Admin",   "20" } },
        { "student1", new[] { "pass123",  "Student", "10" } },
        { "faculty1", new[] { "pass123",  "Faculty", "15" } }
    };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Cookie: pre-fill remembered username
            HttpCookie c = Request.Cookies["RememberUser"];
            if (c != null)
            {
                txtUser.Text = Server.UrlDecode(c.Value);
                chkRemember.Checked = true;
            }
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string u = txtUser.Text.Trim().ToLower();
        string[] info;

        if (Users.TryGetValue(u, out info) && info[0] == txtPass.Text)
        {
            // Session: store logged-in details
            Session["User"] = u;
            Session["Role"] = info[1];
            Session["Balance"] = int.Parse(info[2]);

            // Cookie: remember username for 7 days
            if (chkRemember.Checked)
            {
                HttpCookie c = new HttpCookie("RememberUser", Server.UrlEncode(u));
                c.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(c);
            }
            else if (Request.Cookies["RememberUser"] != null)
            {
                HttpCookie c = new HttpCookie("RememberUser");
                c.Expires = DateTime.Now.AddDays(-1);   // delete cookie
                Response.Cookies.Add(c);
            }
            Response.Redirect("AcademicCalendar.aspx");
        }
        else
        {
            lblMsg.Text = "Invalid username or password.";
        }
    }
}
