<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">
    void Application_Start(object sender, EventArgs e)
    {
        // ---- Academic events table ----
        DataTable ev = new DataTable("Events");
        ev.Columns.Add("EventDate", typeof(DateTime));
        ev.Columns.Add("Title", typeof(string));
        ev.Columns.Add("Type", typeof(string));   // Holiday / Exam / Event
        ev.Rows.Add(new DateTime(2026, 10, 20), "Dussehra Holiday", "Holiday");
        ev.Rows.Add(new DateTime(2026, 11, 9),  "Mid-Sem Exam Begins", "Exam");
        ev.Rows.Add(new DateTime(2026, 11, 8),  "Diwali Vacation", "Holiday");
        ev.Rows.Add(new DateTime(2026, 11, 25), "Annual Tech Fest", "Event");
        ev.Rows.Add(new DateTime(2026, 12, 14), "End-Sem Exam Begins", "Exam");
        Application["Events"] = ev;

        // ---- Leave applications table ----
        DataTable lv = new DataTable("Leaves");
        lv.Columns.Add("Id", typeof(int));
        lv.Columns.Add("UserName", typeof(string));
        lv.Columns.Add("LeaveType", typeof(string));
        lv.Columns.Add("FromDate", typeof(DateTime));
        lv.Columns.Add("ToDate", typeof(DateTime));
        lv.Columns.Add("Days", typeof(int));
        lv.Columns.Add("Reason", typeof(string));
        lv.Columns.Add("Status", typeof(string)); // Pending / Approved / Rejected / Cancelled
        lv.Columns.Add("AppliedOn", typeof(DateTime));
        Application["Leaves"] = lv;
        Application["NextId"] = 1;
    }

    void Session_Start(object sender, EventArgs e) { }
</script>
