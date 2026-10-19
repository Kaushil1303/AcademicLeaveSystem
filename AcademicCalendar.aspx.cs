using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class AcademicCalendarPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null) { Response.Redirect("Login.aspx"); return; }
        pnlAdmin.Visible = (string)Session["Role"] == "Admin";
    }

    // Colour each day cell according to events / approved leaves
    protected void calAcademic_DayRender(object sender, DayRenderEventArgs e)
    {
        DataTable ev = (DataTable)Application["Events"];
        DataTable lv = (DataTable)Application["Leaves"];
        string user = (string)Session["User"];
        DateTime d = e.Day.Date.Date;

        if (e.Day.IsWeekend && !e.Day.IsOtherMonth)
            e.Cell.BackColor = Color.FromArgb(245, 245, 245);

        lock (Application)
        {
            foreach (DataRow r in ev.Rows)
            {
                if (((DateTime)r["EventDate"]).Date != d) continue;
                string type = r["Type"].ToString();
                e.Cell.BackColor = type == "Holiday" ? Color.FromArgb(248, 201, 201)
                                 : type == "Exam"    ? Color.FromArgb(255, 224, 168)
                                                     : Color.FromArgb(201, 228, 248);
                e.Cell.Controls.Add(new LiteralControl("<br/><small>" + Server.HtmlEncode(r["Title"].ToString()) + "</small>"));
            }

            foreach (DataRow r in lv.Rows)
            {
                if (r["UserName"].ToString() == user && r["Status"].ToString() == "Approved"
                    && d >= ((DateTime)r["FromDate"]).Date && d <= ((DateTime)r["ToDate"]).Date)
                {
                    e.Cell.BackColor = Color.FromArgb(201, 240, 201);
                    e.Cell.Controls.Add(new LiteralControl("<br/><small>On Leave</small>"));
                }
            }
        }
    }

    protected void calAcademic_SelectionChanged(object sender, EventArgs e)
    {
        BindDay();
    }

    void BindDay()
    {
        DateTime d = calAcademic.SelectedDate.Date;
        lblDate.Text = d.ToString("dddd, dd MMMM yyyy");
        DataTable ev = (DataTable)Application["Events"];
        DataView dv;
        lock (Application)
        {
            dv = new DataView(ev.Copy());
        }
        dv.RowFilter = "EventDate >= #" + d.ToString("MM/dd/yyyy") + "# AND EventDate < #" + d.AddDays(1).ToString("MM/dd/yyyy") + "#";
        gvDay.DataSource = dv;
        gvDay.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (calAcademic.SelectedDate == DateTime.MinValue) { lblAdd.Text = "Select a date first."; lblAdd.ForeColor = Color.Red; return; }
        if (txtTitle.Text.Trim() == "") { lblAdd.Text = "Enter a title."; lblAdd.ForeColor = Color.Red; return; }

        lock (Application)
        {
            ((DataTable)Application["Events"]).Rows.Add(calAcademic.SelectedDate.Date, txtTitle.Text.Trim(), ddlType.SelectedValue);
        }
        txtTitle.Text = "";
        lblAdd.Text = "Event added.";
        lblAdd.ForeColor = Color.Green;
        BindDay();
    }
}
