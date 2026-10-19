<%@ Page Title="Approve Leaves" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Approve.aspx.cs" Inherits="ApprovePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>Pending Leave Requests (Admin)</h2>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%"
            CellPadding="6" HeaderStyle-BackColor="#1f3a5f" HeaderStyle-ForeColor="White"
            EmptyDataText="No pending requests." OnRowCommand="gv_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="UserName" HeaderText="Applicant" />
                <asp:BoundField DataField="LeaveType" HeaderText="Type" />
                <asp:BoundField DataField="FromDate" HeaderText="From" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="ToDate" HeaderText="To" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="Days" HeaderText="Days" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:ButtonField CommandName="Approve" Text="Approve" ButtonType="Button" />
                <asp:ButtonField CommandName="Reject" Text="Reject" ButtonType="Button" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMsg" runat="server" CssClass="msg" ForeColor="Green" />
    </div>
</asp:Content>
