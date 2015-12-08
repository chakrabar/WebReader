<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebReader.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h4>This is a WebForm : Home</h4>
        <div>
            <asp:label ID="lblMessage" runat="server" text="Welcome"></asp:label>
            <br /><br />
            Enter your name here<br />
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:Button ID="btnName" runat="server" Text="Button" OnClick="btnName_Click" />
            <br />
            <asp:Label ID="lblName" runat="server" Text="msg" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
