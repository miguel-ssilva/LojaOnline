<%@ Page Title="" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="LojaOnline.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Page Content -->
    <!-- Banner Starts Here -->
    <div class="banner" style="background-image: url(assets/images/banner-bg_2.jpg)">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="caption">
              <h2>Bem-Vindo ao Sports!</h2>
              <div class="line-dec"></div>
              <p>Loja venda de desporto Nº1 no Mundo!</p>
                <p>Vem conhecer-nos... Regista-te e aproveita as imensas ofertas que temos para ti.</p>
              <div class="main-button">
                  <asp:Button ID="btn_registar" OnClick="btn_registar_Click" runat="server" class="main-button" Text="Faz o teu registo aqui!" BackColor="#3a8bcd" ForeColor="White" Font-Size="Medium" Height="50" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Banner Ends Here -->

    <%--<!-- Featured Starts Here -->
    <div class="featured-items">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="section-heading">
              <div class="line-dec"></div>
              <h1>Featured Items</h1>
            </div>
          </div>
          <div class="col-md-12">
            <div class="owl-carousel owl-theme">
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-01.jpg" alt="Item 1">
                  <h4>Proin vel ligula</h4>
                  <h6>$15.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-02.jpg" alt="Item 2">
                  <h4>Erat odio rhoncus</h4>
                  <h6>$25.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-03.jpg" alt="Item 3">
                  <h4>Integer vel turpis</h4>
                  <h6>$35.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-04.jpg" alt="Item 4">
                  <h4>Sed purus quam</h4>
                  <h6>$45.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-05.jpg" alt="Item 5">
                  <h4>Morbi aliquet</h4>
                  <h6>$55.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-06.jpg" alt="Item 6">
                  <h4>Urna ac diam</h4>
                  <h6>$65.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-04.jpg" alt="Item 7">
                  <h4>Proin eget imperdiet</h4>
                  <h6>$75.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-05.jpg" alt="Item 8">
                  <h4>Nullam risus nisl</h4>
                  <h6>$85.00</h6>
                </div>
              </a>
              <a href="single-product.html">
                <div class="featured-item">
                  <img src="assets/images/item-06.jpg" alt="Item 9">
                  <h4>Cras tempus</h4>
                  <h6>$95.00</h6>
                </div>
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Featred Ends Here -->--%>


    <!-- Subscribe Form Starts Here -->
    <div class="subscribe-form">
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <div class="section-heading">
              <div class="line-dec"></div>
              <h1>Subscreve já SPORTS!</h1>
            </div>
          </div>
          <div class="col-md-8 offset-md-2">
            <div class="main-content">              
              <div class="container">
                <div id="subscribe" action="" method="get">
                  <div class="row">
                    <div class="col-md-7">
                      <fieldset>
                          <asp:TextBox ID="tb_email" runat="server" placeholder="Your Email..." Width="420px"></asp:TextBox>
                      </fieldset>
                    </div>
                    <div class="col-md-5">
                      <fieldset>
                          <asp:Button ID="btn_subscrever" class="button" runat="server" Text="Subscreve Agora!" OnClick="btn_subscrever_Click" />
                      </fieldset>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Subscribe Form Ends Here -->   
</asp:Content>
