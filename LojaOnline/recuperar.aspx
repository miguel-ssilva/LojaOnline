<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recuperar.aspx.cs" Inherits="LojaOnline.recuperar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <center>
                <a class="navbar-brand" href="https://localhost:44300/HomePage.aspx"><img src="assets/images/Sports_header_new.png" alt=""/></a>
            </center>            
        </div>
        <div>
            <br />
            <br />
            Email: 
            <asp:TextBox ID="tb_email" runat="server" TextMode="Email" Width="401px"></asp:TextBox>
&nbsp;
            <asp:Button ID="btn_recuperar" runat="server" OnClick="btn_recuperar_Click" Text="Recuperar palavra-passe" Width="260px" />
            <br />
            <br />
            <asp:Label ID="lbl_mensagem" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
