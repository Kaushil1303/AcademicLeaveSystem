using System;
using System.Data;
using System.Drawing;

public partial class ApplyLeavePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null) { Response.Redirect("Login.aspx"); return; }

        if (!IsPostBack)
        {
            // Session: restore draft if user navigated away earlier
            if (Session["DraftReason"] != null) txtReason.Text = (string)Session["DraftReason"];
            if (Session["DraftFrom"] != null) { calFrom.SelectedDate = (DateTime)Session["DraftFrom"]; calFrom.VisibleDate = calFrom.SelectedDate; }
            if (Session["DraftTo"] != null)   { calTo.SelectedDate = (DateTime)Session["DraftTo"]; calTo.VisibleDate = calTo.SelectedDate; }
            ShowDates();
        }
    }

    protected void cal_SelectionChanged(object sender, EventArgs e)
    {
        Session["DraftFrom"] = calFrom.SelectedDate == DateTime.MinValue ? (object)null : calFrom.SelectedDate;
        Session["DraftTo"] = calTo.SelectedDate == DateTime.MinValue ? (object)null : calTo.SelectedDate;
        Session["DraftReason"] = txtReason.Text;
        ShowDates();
    }

    void ShowDates()
    {
        lblFrom.Text = calFrom.SelectedDate == DateTime.MinValue ? "" : "Selected: " + calFrom.SelectedDate.ToString("dd-MMM-yyyy");
        lblTo.Text = calTo.SelectedDate == DateTime.MinValue ? "" : "Selected: " + calTo.SelectedDate.ToString("dd-MMM-yyyy");
    }

    void Err(string m) { lblMsg.Text = m; lblMsg.ForeColor = Color.Red; }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid) return;

        DateTime from = calFrom.SelectedDate.Date, to = calTo.SelectedDate.Date;
        if (calFrom.SelectedDate == DateTime.MinValue || calTo.SelectedDate == DateTime.MinValue) { Err("Select both From and To dates."); return; }
        if (from < DateTime.Today) { Err("From date cannot be in the past."); return; }
        if (to < from) { Err("To date cannot be before From date."); return; }

        int days = (to - from).Days + 1;
        int balance = (int)Session["Balance"];
        if (days > balance) { Err("Insufficient balance. You have " + balance + " day(s) left."); return; }

        string user = (string)Session["User"];
        lock (Application)
        {
            DataTable lv = (DataTable)Application["Leaves"];

            // overlap check against own pending/approved leaves
            foreach (DataRow r in lv.Rows)
            {
                string st = r["Status"].ToString();
                if (r["UserName"].ToString() == user && (st == "Pending" || st == "Approved")
                    && from <= ((DateTime)r["ToDate"]).Date && to >= ((DateTime)r["FromDate"]).Date)
                { Err("These dates overlap with an existing leave application."); return; }
            }

            int id = (int)Application["NextId"];
            lv.Rows.Add(id, user, rblType.SelectedValue, from, to, days, txtReason.Text.Trim(), "Pending", DateTime.Now);
            Application["NextId"] = id + 1;
        }

        // Session: update balance (deducted on apply, refunded on reject/cancel) and clear draft
        Session["Balance"] = balance - days;
        Session.Remove("DraftFrom"); Session.Remove("DraftTo"); Session.Remove("DraftReason");

        Response.Redirect("MyLeaves.aspx");
    }
}
