<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimplexWebForm.aspx.cs" Inherits="SimplexWebForm.SimplexWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox runat="server" ID="x" />
        <asp:Label runat="server"> + </asp:Label>
        <asp:TextBox runat="server" ID="y" />
        <asp:Button runat="server" ID="sum" OnClick="Sum_Click" Text=" = " />
        <asp:TextBox runat="server" ID="result" ReadOnly="true" TabIndex="-1" />
    </form>
</body>
</html>
