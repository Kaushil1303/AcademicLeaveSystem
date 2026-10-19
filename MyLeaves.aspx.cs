using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class MyLeavesPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null) { Response.Redirect("Login.aspx"); return; }
        if (!IsPostBack) BindGrid();
    }

    void BindGrid()
    {
        DataView dv;
        lock (Application) { dv = new DataView(((DataTable)Application["Leaves"]).Copy()); }
        dv.RowFilter = "UserName = '" + ((string)Session["User"]).Replace("'", "''") + "'";
        dv.Sort = "Id DESC";
        gv.DataSource = dv;
        gv.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
        TableCell c = e.Row.Cells[6];
        c.ForeColor = status == "Approved" ? Color.Green : status == "Rejected" ? Color.Red
                    : status == "Cancelled" ? Color.Gray : Color.DarkOrange;
        c.Font.Bold = true;
        // Only pending leaves can be cancelled
        if (status != "Pending") e.Row.Cells[7].Controls.Clear();
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "CancelLeave") return;
        int idx = Convert.ToInt32(e.CommandArgument);
        int id = (int)gv.DataKeys[idx].Value;

        lock (Application)
        {
            foreach (DataRow r in ((DataTable)Application["Leaves"]).Rows)
            {
                if ((int)r["Id"] == id && r["Status"].ToString() == "Pending"
                    && r["UserName"].ToString() == (string)Session["User"])
                {
                    r["Status"] = "Cancelled";
                    Session["Balance"] = (int)Session["Balance"] + (int)r["Days"];   // refund
                    lblMsg.Text = "Leave #" + id + " cancelled. Balance refunded.";
                    lblMsg.ForeColor = Color.Green;
                    break;
                }
            }
        }
        BindGrid();
    }
}
