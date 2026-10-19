<%@ Page Title="Academic Calendar" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AcademicCalendar.aspx.cs" Inherits="AcademicCalendarPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>Academic Calendar</h2>
        <p>
            <span style="background:#f8c9c9;padding:2px 8px">Holiday</span>
            <span style="background:#ffe0a8;padding:2px 8px">Exam</span>
            <span style="background:#c9e4f8;padding:2px 8px">Event</span>
            <span style="background:#c9f0c9;padding:2px 8px">My Approved Leave</span>
        </p>
        <asp:Calendar ID="calAcademic" runat="server" Width="100%" Height="400px" BorderColor="#1f3a5f"
            NextPrevFormat="FullMonth" ShowGridLines="True" CellPadding="4"
            OnDayRender="calAcademic_DayRender" OnSelectionChanged="calAcademic_SelectionChanged">
            <TitleStyle BackColor="#1f3a5f" ForeColor="White" Font-Bold="True" Height="32px" />
            <DayHeaderStyle BackColor="#dde5f0" />
            <TodayDayStyle BorderColor="Red" BorderWidth="2px" />
            <SelectedDayStyle BackColor="#1f3a5f" ForeColor="White" />
            <OtherMonthDayStyle ForeColor="#aaa" />
        </asp:Calendar>
    </div>

    <div class="card">
        <h3>Details for: <asp:Label ID="lblDate" runat="server" Text="Select a date" /></h3>
        <asp:GridView ID="gvDay" runat="server" AutoGenerateColumns="False" Width="100%" EmptyDataText="No events on this date.">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Event" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
            </Columns>
        </asp:GridView>
    </div>

    <%-- Admin-only panel --%>
    <asp:Panel ID="pnlAdmin" runat="server" Visible="false" CssClass="card">
        <h3>Add Event (Admin) - uses the date selected above</h3>
        <asp:TextBox ID="txtTitle" runat="server" placeholder="Event title" Width="220px" />
        <asp:DropDownList ID="ddlType" runat="server">
            <asp:ListItem>Holiday</asp:ListItem>
            <asp:ListItem>Exam</asp:ListItem>
            <asp:ListItem>Event</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnAdd" runat="server" Text="Add Event" OnClick="btnAdd_Click" />
        <asp:Label ID="lblAdd" runat="server" CssClass="msg" />
    </asp:Panel>
</asp:Content>
