using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class ApprovePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null) { Response.Redirect("Login.aspx"); return; }
        if ((string)Session["Role"] != "Admin") { Response.Redirect("AcademicCalendar.aspx"); return; }  // role check
        if (!IsPostBack) BindGrid();
    }

    void BindGrid()
    {
        DataView dv;
        lock (Application) { dv = new DataView(((DataTable)Application["Leaves"]).Copy()); }
        dv.RowFilter = "Status = 'Pending'";
        gv.DataSource = dv;
        gv.DataBind();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Approve" && e.CommandName != "Reject") return;
        int id = (int)gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;

        lock (Application)
        {
            foreach (DataRow r in ((DataTable)Application["Leaves"]).Rows)
            {
                if ((int)r["Id"] != id || r["Status"].ToString() != "Pending") continue;
                r["Status"] = e.CommandName == "Approve" ? "Approved" : "Rejected";
                lblMsg.Text = "Leave #" + id + " " + r["Status"] + ".";
                // Note: with in-memory storage the applicant's Session balance isn't refundable
                // from here; with a database, refund Balance on rejection.
                break;
            }
        }
        BindGrid();
    }
}
