<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="StormWeb.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server" Width="100%">
                <asp:Image ID="Image1" runat="server" ImageUrl ="~/images/TopBarCPF.PNG" Width="100%"/>
            </asp:Panel>
        </div>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left" BackColor="#F9F9F9" Height="100%" style="margin-right: 0px" Width="100%">
            <asp:Image ID="Image2" runat="server" ImageUrl ="~/images/SideBarCPF.PNG" Height="50%" Width="14%" />
        </asp:Panel>
    </form>
</body>
</html>
