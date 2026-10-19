<%@ Page Title="Apply Leave" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ApplyLeave.aspx.cs" Inherits="ApplyLeavePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <h2>Apply for Leave</h2>
        <table width="100%"><tr>
            <td valign="top" width="50%">
                <b>From date</b>
                <asp:Calendar ID="calFrom" runat="server" OnSelectionChanged="cal_SelectionChanged" BorderColor="#1f3a5f">
                    <TitleStyle BackColor="#1f3a5f" ForeColor="White" />
                    <SelectedDayStyle BackColor="#1f3a5f" ForeColor="White" />
                </asp:Calendar>
                <asp:Label ID="lblFrom" runat="server" />
            </td>
            <td valign="top">
                <b>To date</b>
                <asp:Calendar ID="calTo" runat="server" OnSelectionChanged="cal_SelectionChanged" BorderColor="#1f3a5f">
                    <TitleStyle BackColor="#1f3a5f" ForeColor="White" />
                    <SelectedDayStyle BackColor="#1f3a5f" ForeColor="White" />
                </asp:Calendar>
                <asp:Label ID="lblTo" runat="server" />
            </td>
        </tr></table>
        <br />
        Leave type:
        <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True">Casual</asp:ListItem>
            <asp:ListItem>Medical</asp:ListItem>
            <asp:ListItem>Academic</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:RadioButtonList>
        Reason:<br />
        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="3" Width="100%" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtReason" ErrorMessage="Reason is required" ForeColor="Red" Display="Dynamic" />
        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtReason" ValidationExpression="^[\s\S]{10,300}$" ErrorMessage="Reason must be 10-300 characters" ForeColor="Red" Display="Dynamic" />
        <br /><br />
        <asp:Button ID="btnApply" runat="server" Text="Submit Application" OnClick="btnApply_Click" />
        <br /><asp:Label ID="lblMsg" runat="server" CssClass="msg" />
    </div>
</asp:Content>
