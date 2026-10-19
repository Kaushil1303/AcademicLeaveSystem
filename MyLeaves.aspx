<%@ Page Title="My Leaves" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyLeaves.aspx.cs" Inherits="MyLeavesPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>My Leave Applications</h2>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" Width="100%"
            CellPadding="6" HeaderStyle-BackColor="#1f3a5f" HeaderStyle-ForeColor="White"
            EmptyDataText="You have not applied for any leave." AllowPaging="True" PageSize="8"
            OnPageIndexChanging="gv_PageIndexChanging" OnRowCommand="gv_RowCommand" OnRowDataBound="gv_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="LeaveType" HeaderText="Type" />
                <asp:BoundField DataField="FromDate" HeaderText="From" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="ToDate" HeaderText="To" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="Days" HeaderText="Days" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:ButtonField CommandName="CancelLeave" Text="Cancel" ButtonType="Button" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMsg" runat="server" CssClass="msg" />
    </div>
</asp:Content>
