<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Backoffice.aspx.cs" Inherits="LojaOnline.Backoffice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
    <center>
        <asp:Button ID="btn_utilizadores" runat="server" Text="Gerir Utilizadores" OnClick="btn_utilizadores_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btn_produtos" runat="server" Text="Gerir Produtos" OnClick="btn_produtos_Click" />
        <br />
        <br />
        <asp:Panel ID="pnl_utilizadores" runat="server" Visible="False">
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="Repeater1_ItemDataBound" Visible="True" OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate>
                    <table width="900">
                        <tr style="background-color:#3a8bcd; color: white; text-align:center">
                            <td style="padding:4px">
                                <b>Código</b>
                            </td>
                            <td>
                                <b>Nome</b>
                            </td>
                            <td>
                                <b>Morada</b>
                            </td>
                            <td>
                                <b>NIF</b>
                            </td>
                            <td>
                                <b>Telemóvel</b>
                            </td>
                            <td>
                                <b>Username</b>
                            </td>
                            <td>
                                <b>Email</b>
                            </td>
                            <td>
                                <b>Perfil</b>
                            </td>
                            <td style="padding:4px">
                                <b>Ativo</b>
                            </td>
                            <td style="padding:4px">
                                <b>Online</b>
                            </td>
                            <td style="background-color:white">
                                <asp:ImageButton ID="btn_saveall" runat="server" ImageUrl="~/assets/images/saveall.png" />
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl_cod" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_nome" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_morada" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lbl_nif" runat="server"></asp:Label>
                        </td>
                        <td style="width:auto">
                            <asp:TextBox ID="tb_telemovel" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_username" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_perfil" runat="server">
                                <asp:ListItem Value="Admin"></asp:ListItem>
                                <asp:ListItem Value="Revenda"></asp:ListItem>
                                <asp:ListItem Value="Normal"></asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                        <td style="padding:4px">
                            <asp:Label ID="lbl_ativo" runat="server"></asp:Label>
                        </td>
                        <td style="padding:4px">
                            <asp:Label ID="lbl_online" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_gravar" runat="server" ImageUrl="~/assets/images/save.png" CommandName="btn_gravar"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_apagar" runat="server" ImageUrl="~/assets/images/delete.png" CommandName="btn_apagar"/>
                        </td>
                    </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl_cod" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_nome" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_morada" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lbl_nif" runat="server"></asp:Label>
                        </td>
                        <td style="width:auto">
                            <asp:TextBox ID="tb_telemovel" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_username" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_email" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_perfil" runat="server">
                                <asp:ListItem Value="Admin"></asp:ListItem>
                                <asp:ListItem Value="Revenda"></asp:ListItem>
                                <asp:ListItem Value="Normal"></asp:ListItem>
                            </asp:DropDownList>  
                        </td>
                        <td style="padding:4px">
                            <asp:Label ID="lbl_ativo" runat="server"></asp:Label>
                        </td>
                        <td style="padding:4px">
                            <asp:Label ID="lbl_online" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_gravar" runat="server" ImageUrl="~/assets/images/save.png" CommandName="btn_gravar"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_apagar" runat="server" ImageUrl="~/assets/images/delete.png" CommandName="btn_apagar"/>
                        </td>
                    </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        </asp:Panel>
        <asp:Panel ID="pnl_produtos" runat="server" Visible="False">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT * FROM [utilizadores]"></asp:SqlDataSource>

        <br />
        <asp:Button ID="btn_inserir" runat="server" OnClick="btn_inserir_Click" Text="Inserir Novo Produto" Visible="False" />
        <br />
        <br />

        <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2" OnItemDataBound="Repeater2_ItemDataBound" Visible="True" OnItemCommand="Repeater2_ItemCommand">
            <HeaderTemplate>
                    <table border="1" width="900">
                        <tr style="background-color:#3a8bcd; color: white; text-align:center">
                            <td>
                                <b>Código</b>
                            </td>
                            <td>
                                <b>Nome</b>
                            </td>
                            <td>
                                <b>Marca</b>
                            </td>
                            <td>
                                <b>Cor</b>
                            </td>
                            <td>
                                <b>Categoria</b>
                            </td>
                            <td>
                                <b>Moda</b>
                            </td>
                            <td>
                                <b>Stock</b>
                            </td>

                            <td>
                                <b>Preço</b>
                            </td>                            
                            <td style="background-color:white">
                                <asp:ImageButton ID="btn_saveall_prod" runat="server" ImageUrl="~/assets/images/saveall.png" OnClick="btn_saveall_prod_Click" />
                            </td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl_cod" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_nome" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_marca" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_cor" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_categoria" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_moda" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_stock" runat="server" Width="100px"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="tb_preco" runat="server" Width="100px"></asp:TextBox>
                        </td>
  
                        <td>
                            <asp:ImageButton ID="btn_gravar" runat="server" ImageUrl="~/assets/images/save.png" CommandName="btn_gravar"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_apagar" runat="server" ImageUrl="~/assets/images/delete.png" CommandName="btn_apagar"/>
                        </td>
                    </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <asp:Label ID="lbl_cod" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_nome" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_marca" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_cor" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_categoria" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_moda" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="tb_stock" runat="server" Width="100px"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="tb_preco" runat="server" Width="100px"></asp:TextBox>
                        </td>                        
                        <td>
                            <asp:ImageButton ID="btn_gravar" runat="server" ImageUrl="~/assets/images/save.png" CommandName="btn_gravar"/>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_apagar" runat="server" ImageUrl="~/assets/images/delete.png" CommandName="btn_apagar"/>
                        </td>
                    </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        </asp:Panel>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT * FROM [produtos]"></asp:SqlDataSource>
        <asp:Panel ID="pnl_inserirProd" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td style="width:200px">
                                Nome:                                
                            </td>
                            <td>
                                <asp:TextBox ID="tb_inserirNome" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Marca:                                
                            </td>
                            <td>
                                <asp:TextBox ID="tb_inserirMarca" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Tamanho:                                
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_inserirTamanho" runat="server" Width="400px">
                                    <asp:ListItem Value="S"></asp:ListItem>
                                    <asp:ListItem Value="M"></asp:ListItem>
                                    <asp:ListItem Value="L"></asp:ListItem>
                                    <asp:ListItem Value="XL"></asp:ListItem>
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Cor:
                            </td>
                            <td>
                                <asp:TextBox ID="tb_inserirCor" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Categoria:                                
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_inserirCategoria" runat="server" Width="400px">
                                    <asp:ListItem Value="T-shirt"></asp:ListItem>
                                    <asp:ListItem Value="Camisolas"></asp:ListItem>
                                    <asp:ListItem Value="Sweatshirt"></asp:ListItem>
                                    <asp:ListItem Value="Camisas"></asp:ListItem>
                                    <asp:ListItem Value="Casacos"></asp:ListItem>
                                    <asp:ListItem Value="Calças"></asp:ListItem>
                                    <asp:ListItem Value="Calções"></asp:ListItem>
                                    <asp:ListItem Value="Meias"></asp:ListItem>
                                    <asp:ListItem Value="Calçado">Calçado</asp:ListItem>
                                    <asp:ListItem Value="Acessórios"></asp:ListItem>
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Moda:                                
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_inserirModa" runat="server" Width="400px">
                                    <asp:ListItem Value="Homem"></asp:ListItem>
                                    <asp:ListItem Value="Mulher"></asp:ListItem>
                                    <asp:ListItem Value="Criança"></asp:ListItem>
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Stock:                                
                            </td>
                            <td>
                                <asp:TextBox ID="tb_inserirStock" runat="server" Width="400px" TextMode="Number"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Preço:                                
                            </td>
                            <td>
                                <asp:TextBox ID="tb_inserirPreco" runat="server" Width="400px" TextMode="SingleLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">
                                Imagem:                                
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                    </table>
                <br />
            <asp:Button ID="btn_inserirProd" runat="server" Text="Inserir" Height="50px" Width="113px" OnClick="btn_inserirProd_Click" />
        </asp:Panel>
    </center>
    
</asp:Content>

