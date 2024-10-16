<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="LojaOnline.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h1>LOGIN</h1>
        <br />
        <table>
            <tr>
                <td style="text-align:right; width:200px">
                    Username/email:
                </td>
                <td style="width:20px">
                    
                </td>
                <td>
                    <asp:TextBox ID="tb_user" runat="server" Width="240px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; width:200px">
                    Password:                    
                </td>
                <td style="width:20px">
                    
                </td>
                <td>
                    <asp:TextBox ID="tb_pass" runat="server" Width="240px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </table>
         
        <br />
        <asp:Button ID="btn_login" runat="server" Text="Login" Width="140px" OnClick="btn_login_Click" Font-Size="Large" Height="52px" />
        <br />
        <br />
        <asp:LinkButton ID="lbtn_registar" runat="server" Font-Size="Small" OnClick="lbtn_registar_Click">Registar</asp:LinkButton>
        <br />
        <asp:LinkButton ID="lbtn_recuperar" runat="server" Font-Size="Small" OnClick="lbtn_recuperar_Click">Recuperar palavra-passe</asp:LinkButton>
        <br />
        <br />
        <asp:Label ID="lbl_mensagem" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
    </center>
    
</asp:Content>
