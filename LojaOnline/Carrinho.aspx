<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Carrinho.aspx.cs" Inherits="LojaOnline.Carrinho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tabela
        {
            width:1200px;
            margin-left:120px;
            border-bottom-style:solid;
            padding-left:25px;
        }
        td
        {
            padding-left:15px;
        }
    </style>
    <asp:Panel ID="Panel1" runat="server">
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="DataList1_ItemCommand">
        <HeaderTemplate>
            <table class="tabela" border="1">
                <tr>
                    <td style="width:900px">
                        <asp:Label ID="lbl_produto" runat="server" Text="Nome"></asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:Label ID="lbl_unidade" runat="server" Text=Unidades></asp:Label>
                    </td>
                    <td style="width:200px">
                        <asp:Label ID="lbl_preco" runat="server" Text=Preço></asp:Label>
                    </td>
                </tr>
            </table>
        </HeaderTemplate>
        <ItemTemplate>
            <table class="tabela">
                <tr>
                    <td style="width:1100px">
                        <asp:Label ID="lbl_produto" runat="server" Text='<%#Eval("nome") %>'></asp:Label>
                    </td>
                    <td style="width:100px">
                        <asp:TextBox ID="tb_quant" runat="server" AutoPostBack="True" TextMode="Number" Text='<%#Eval("quant")%>' Width="50" OnTextChanged="tb_quant_TextChanged"></asp:TextBox>      
                    </td>
                    <td style="width:200px">
                        <asp:Label ID="lbl_preco" runat="server" Text='<%#string.Concat(Eval("preco"), " €")%>'></asp:Label>
                    </td>
                    <td style="text-align:center; padding-left:0px">
                        <asp:ImageButton ID="ImgBtn_delete" runat="server" ImageUrl="~/assets/images/delete_carrinho.png" CommandArgument='<%# Eval("cod_prod")%>' CommandName="ImgBtn_delete" />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>"></asp:SqlDataSource>

            <table style="width:1270px; margin-left:120px">
                <tr>
                    <td style="width:3000px; text-align:right">
                        <asp:Label ID="lbl_total" runat="server" Text="Total:" Font-Size="X-Large" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width:350px; padding-left:50px; padding-top:10px">
                        <asp:Label ID="lbl_preco_total" runat="server" Font-Size="X-Large"></asp:Label>
                        <asp:Label ID="lbl_preco_total_revenda" runat="server" Font-Size="X-Large" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width:900px; text-align:right">
                        
                    </td>
                    <td style="width:450px; padding-top:15px; padding-left:110px">
                        <asp:Button ID="btn_finalizar" runat="server" Text="Finalizar compra" Height="57px" OnClick="btn_finalizar_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    <center>
    <h2>
        <asp:Label ID="lbl_carrinho" runat="server" Text="Carrinho Vazio" Visible="False"></asp:Label>
    </h2>
    </center>
</asp:Content>
