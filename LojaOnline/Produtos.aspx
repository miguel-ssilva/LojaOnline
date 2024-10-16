<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="Produtos.aspx.cs" Inherits="LojaOnline.Produtos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        
     <style>
     .imagens
     {
         width: 250px;
         height: 300px;
     }
      .produtos
     {
         width: 250px;
         height: 430px;
         margin: 15px; 
         margin-top: 30px;
         /*padding-left: 35px;*/
         
         /*border-width: 1px;
         border-color: black;
         border-style:solid;*/ 
         /*serve para chegar tudo à direita*/
         /*margin-left: 100% !important;
         /*border-bottom: 5px solid;*/
     }
      .menu_filtros
      {
          display:inline-block;
          vertical-align:top;
          margin-top:30px;
          padding-left:10px;
          /*teste*/
          /*border-color: black;
          border-style:solid; */
      }
    </style>
    <center>
        <asp:Button ID="btn_A_Z" runat="server" Text="A-Z" OnClick="btn_A_Z_Click" />  <asp:Button ID="btn_Z_A" runat="server" Text="Z-A" OnClick="btn_Z_A_Click" />  <asp:Button ID="btn_precoBaixo" runat="server" Text="Preço + baixo" OnClick="btn_precoBaixo_Click" />  <asp:Button ID="btn_precoAlto" runat="server" Text="Preço + alto" OnClick="btn_precoAlto_Click" />  <asp:TextBox ID="tb_pesquisa" runat="server" Width="500px"></asp:TextBox>   <asp:Button ID="btn_pesquisar" runat="server" Text="Pesquisar" OnClick="btn_pesquisar_Click" /></center>
    <div class="menu_filtros">
     <asp:Panel ID="Panel1" runat="server" Width="250px">      
         <h4>Género:</h4>
         <asp:CheckBoxList ID="cbl_genero" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbl_genero_SelectedIndexChanged">
             <asp:ListItem Value="Homem"></asp:ListItem>
             <asp:ListItem Value="Mulher"></asp:ListItem>
             <asp:ListItem Value="Criança"></asp:ListItem>
         </asp:CheckBoxList>
         <br />
         <h4>Cor:</h4>
         <asp:CheckBoxList ID="cbl_cor" runat="server" DataSourceID="SqlDataSource2" DataTextField="cor" DataValueField="cor" OnSelectedIndexChanged="cbl_cor_SelectedIndexChanged" AutoPostBack="True"></asp:CheckBoxList>
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT DISTINCT([cor]) FROM [produtos]"></asp:SqlDataSource>
         <br />
         <h4>Marca:</h4>
         <asp:CheckBoxList ID="cbl_marca" runat="server" DataSourceID="SqlDataSource3" DataTextField="marca" DataValueField="marca" AutoPostBack="True" OnSelectedIndexChanged="cbl_marca_SelectedIndexChanged"></asp:CheckBoxList>         
         <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT DISTINCT([marca]) FROM [produtos]"></asp:SqlDataSource>
     </asp:Panel>
    </div>

    <div style="display:inline-block">
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" RepeatColumns="4" RepeatDirection="Horizontal" DataKeyField="cod_prod" Height="430px" Width="340px" OnItemCommand="DataList1_ItemCommand" OnItemDataBound="DataList1_ItemDataBound">
        <ItemTemplate> 
            <table class="produtos">
                <tr>
                    <td style="text-align:center">
                        <asp:ImageButton ID="imgBtn_imagem" runat="server" CssClass="imagens" ImageUrl='<%#"data:image/jpg;base64," + Convert.ToBase64String((byte[])Eval("imagem")) %>'/>
                    </td>
                </tr> 
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="lbl_titulo" runat="server" Text='<%#Eval("nome") %>'></asp:Label>
                    </td>                    
                </tr>
                <tr>
                    <td style="padding-left:5px">
                        <asp:Label ID="Label_preco" runat="server" Text="Preço: "></asp:Label>
                        <asp:Label ID="lbl_preco" runat="server" Text='<%#String.Format(string.Concat(Eval("preco"), " €"))%>'></asp:Label>
                        <asp:Label ID="lbl_preco_revenda" runat="server" Text='<%#String.Format(string.Concat(Convert.ToInt32(Eval("preco")) * 0.80, " €"))%>' Visible="False"></asp:Label>
                    </td>                    
                </tr>
                <tr>
                    <td style="padding-left:5px">
                        <asp:Label ID="lbl_tamanho" runat="server" Text="Tamanho: "></asp:Label>
                        <asp:DropDownList ID="ddl_tamanho" runat="server"></asp:DropDownList>      
                    </td>                    
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btn_comprar" runat="server" Text="Adicionar ao carrinho" CommandName="btn_comprar" CommandArgument='<%#Eval("cod_prod") %>'/>
                    </td>                    
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
        </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LojaOnlineConnectionString %>" SelectCommand="SELECT * FROM [produtos]"></asp:SqlDataSource>
     <br />
</asp:Content>
