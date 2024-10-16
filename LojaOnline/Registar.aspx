<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registar.aspx.cs" Inherits="LojaOnline.Registar" %>

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
        <div style="text-align: center">
            <h1>Regista-te aqui</h1>

        </div>
        <div>
            <asp:Panel ID="Panel1" style="margin-left: 460px" runat="server"> 
                    <table>
                        <tr>
                            <td style="width:200px">
                                Nome:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_nome" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_nome" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Morada:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_morada" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_morada" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                NIF:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_nif" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_nif" runat="server" Width="400px" MaxLength="9"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Telemóvel:
                            </td>
                            <td>
                                <asp:TextBox ID="tb_tlmvl" runat="server" Width="400px" TextMode="Phone"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Username:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tb_username" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_username" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Email:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tb_email" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_email" runat="server" Width="400px" TextMode="Email"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Password:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tb_pass" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_pass" runat="server" Width="400px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Repetir Password:
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tb_pass2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_pass2" runat="server" Width="400px" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                    </table>                
            </asp:Panel>
            <div style="text-align: center">
                <br />
                <p>
                    <asp:Button ID="btn_registar" runat="server" Text="Registar" Height="56px" Width="205px" OnClick="btn_registar_Click" Font-Size="Large" />
                </p>
            </div>
            <div style="text-align: center">
                <p>
                    &nbsp;</p>
            </div>
            <div style="text-align: center">
                <p>
                    <asp:Label ID="lbl_mensagem" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
