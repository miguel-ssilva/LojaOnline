<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="AreaCliente.aspx.cs" Inherits="LojaOnline.AreaCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <h1 style="text-align:center">Área Cliente</h1>
    <div style="display:inline-block; align-content:center; vertical-align:top">
    <table>
        <tr>
            <td style="width:50px">

            </td>
            <td style="width:250px; vertical-align:top">

                    <asp:LinkButton ID="lbtn_perfil" runat="server" OnClick="lbtn_perfil_Click">Perfil</asp:LinkButton>
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtn_encomendas" runat="server" OnClick="lbtn_encomendas_Click">Encomendas</asp:LinkButton>
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtn_favoritos" runat="server" OnClick="lbtn_favoritos_Click">Favoritos</asp:LinkButton>
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtn_definicoes" runat="server" OnClick="lbtn_definicoes_Click">Definições</asp:LinkButton>
                    <br />
                    <br />
                    <asp:LinkButton ID="lbtn_alterar" runat="server" OnClick="lbtn_alterar_Click">Alterar Palavra-passe</asp:LinkButton>
                    <br />
                    <br />                    
                    <asp:LinkButton ID="lbtn_sair" runat="server" OnClick="lbtn_sair_Click">Sair da Conta</asp:LinkButton>
            </td>
            </tr>
        </table>
        </div>
   
    <%--Separação Menu e Paineis de trabalho--%>
 
        <div style="display:inline-block; align-content:center; max-width:700px; margin-left:100px">
                <asp:Panel ID="pnl_perfil" runat="server" Visible="False">                    
                    <br />
                    <asp:Image ID="Image1" runat="server" />
                    <br />
                    <table>
                        <tr>
                            <td style="text-align:right; width:200px">
                                Nome:
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_nome" runat="server" Width="350px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td style="text-align:right; width:200px">
                                Morada:                    
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_morada" runat="server" Width="350px" TextMode="SingleLine" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td style="text-align:right; width:200px">
                                Telemóvel:                    
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_tlmvl" runat="server" Width="350px" TextMode="SingleLine" Enabled="False" MaxLength="9"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td style="text-align:right; width:200px">
                                Email:                    
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_email" runat="server" Width="350px" TextMode="SingleLine" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <center>
                        <asp:Button ID="btn_alterar" runat="server" Text="Alterar dados" OnClick="btn_alterar_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btn_cancelar" runat="server" Text="Cancelar" Visible="False" OnClick="btn_cancelar_Click" />
                    </center>
                </asp:Panel>                
                <asp:Panel ID="pnl_encomendas" runat="server" Visible="False">  
                    <br />
                    <br />
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="Repeater1_ItemCommand">
                        <HeaderTemplate>
                            <table style="margin-left:50px">
                                <tr>
                                    <td style="padding:10px 15px">
                                        <b>Nº encomenda</b>
                                    </td>
                                    <td style="padding:10px 15px">
                                        <b>Nº fatura</b>
                                    </td>
                                    <td style="padding:10px 15px">
                                        <b>Data</b>
                                    </td>
                                    <td style="padding:10px 15px">
                                        <b>PDF</b>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                                <tr>
                                    <td style="padding:10px 15px">
                                        <asp:Label ID="lbl_num" runat="server" Text='<%# Eval("numero") %>' />
                                    </td>
                                    <td style="padding:10px 15px">
                                        <asp:Label ID="lbl_fatura" runat="server" Text='<%# Eval("fatura") %>' />
                                    </td>
                                    <td style="padding:10px 15px">
                                        <asp:Label ID="lbl_data" runat="server" Text='<%# Eval("data", "{0:dd/M/yyyy}") %>' /> 
                                    </td>
                                    <td style="padding:10px 15px">
                                        <asp:LinkButton ID="lbtn_download" runat="server" CommandArgument ='<%# Eval("cod_encomenda") %>' CommandName="lbtn_download">Download PDF</asp:LinkButton>
                                    </td>
                                </tr>                            
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT * FROM [encomendas] order by 1 desc"></asp:SqlDataSource>
                    <asp:Label ID="Label1" runat="server" Font-Size="Larger"></asp:Label>
                </asp:Panel>                

                <asp:Panel ID="pnl_favoritos" runat="server" Visible="False">
                    <br />
                    <br />
                    <asp:Label ID="Label2" runat="server" Font-Size="Larger"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnl_definicoes" runat="server" Visible="False">
                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" Font-Size="Larger"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnl_alterar" runat="server" Visible="False">
                    <br />
                    <br />
                    <center>
                        <asp:Label ID="lbl_alterar" runat="server" Font-Size="Larger" Text="Alterar palavra-passe"></asp:Label>
                    </center>
                    <br />
                    <br />
                    <table>
                        <tr>
                            <td style="text-align:right; width:250px">
                                Palavra-passe atual:                    
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_pass_atual" runat="server" Width="350px" TextMode="Password" Enabled="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td style="text-align:right; width:250px">
                                Nova Palavra-passe:                    
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_pass_nova" runat="server" Width="350px" TextMode="Password" Enabled="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                        <tr>
                            <td style="text-align:right; width:250px">
                                Repetir Palavra-passe:                    
                            </td>
                            <td style="width:20px">
            
                            </td>
                            <td>
                                <asp:TextBox ID="tb_pass_repete" runat="server" Width="350px" TextMode="Password" Enabled="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr><td>&nbsp;</td></tr>
                    </table>
                    <center>
                        <asp:Button ID="btn_alterar_pass" runat="server" Text="Alterar" OnClick="btn_alterar_pass_Click" />
                        <br />
                        <br />
                        <asp:Label ID="lbl_mensagem" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </center>
                </asp:Panel>
            </div>
</asp:Content>
