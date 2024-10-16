using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace LojaOnline
{    
    public partial class template : System.Web.UI.MasterPage
    {
        public int active;        
        protected void Page_Load(object sender, EventArgs e)
        {            
            // Por defeito não deve aparecer
            backoffice.Visible = false;

            // Verifica se existe user no URL
            string user = "";
            string perfil = "";
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                user = Request.QueryString["user"];
                perfil = Request.QueryString["perfil"];
                lbtn_login.Text = DecryptString(user);
                lbtn_logout.Visible = true;

                logotipo.Attributes["href"] = $"https://localhost:44300/HomePage.aspx?user={user}&perfil={perfil}";

                if (DecryptString(perfil) == "Admin")
                {
                    // Se cliente for Admin visible = true
                    backoffice.Visible = true;
                }
                else
                {
                    backoffice.Visible = false;
                }
            }
            else
            {
                lbtn_logout.Visible = false;
                logotipo.Attributes["href"] = "https://localhost:44300/HomePage.aspx";
            }            

            ChangeActive();

            AtualizaCarrinhoQuant();
        } 
        public void AtualizaCarrinhoQuant()
        {
            int codigo_user;
            int quant;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            if (Request.QueryString.AllKeys.Contains("user"))
            {
                string user = DecryptString(Request.QueryString["user"]);

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT cod_user FROM utilizadores where username = '{user}'";
                con.Open();
                codigo_user = (int)cmd.ExecuteScalar();
                con.Close();
            }
            else
            {
                codigo_user = 1;
            }            

            string query = $"SELECT SUM(quant) FROM carrinho WHERE cod_user = {codigo_user}";

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            con.Open();
            try
            {
                quant = (int)cmd.ExecuteScalar();
            }
            catch(Exception e)
            {
                quant = 0;
            }            
            con.Close();

            lbl_quant.Text = " " + quant.ToString();
        }
        public void ChangeActive()
        {
            // Altera para cor azul o linkbutton seleccionado

            home.Attributes["class"] = "nav-item";
            produtos.Attributes["class"] = "nav-item";
            about.Attributes["class"] = "nav-item";
            contactos.Attributes["class"] = "nav-item";
            backoffice.Attributes["class"] = "nav-item";
            login.Attributes["class"] = "nav-item";

            if(active == 2)
            {
                produtos.Attributes["class"] = "nav-item active";
            }
        }
        public static string EncryptString(string Message)
        {
            string Passphrase = "cinel";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6
            string enc = Convert.ToBase64String(Results);
            enc = enc.Replace("+", "KLKLK");
            enc = enc.Replace("/", "JLJLJL");
            enc = enc.Replace("\\", "IOIOIO");
            return enc;
        }
        public static string DecryptString(string Message)
        {
            string Passphrase = "cinel";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Step 3
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4
            Message = Message.Replace("KLKLK", "+");
            Message = Message.Replace("JLJLJL", "/");
            Message = Message.Replace("IOIOIO", "\\");

            byte[] DataToDecrypt = Convert.FromBase64String(Message);

            // Step 5
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }

            // Step 6
            return UTF8.GetString(Results);
        }
        public string RetornaUser()
        {
            // Se existir "user" no URL guarda o username e mantem até terminar sessão
            string user = "";
            string perfil = "";
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                user = Request.QueryString["user"];
                perfil = Request.QueryString["perfil"];
                return $"?user={user}&perfil={perfil}";
            }
            else
            {
                return "";
            }
        }
        protected void lbtn_home_Click(object sender, EventArgs e)
        {
            Response.Redirect($"HomePage.aspx{RetornaUser()}");
        }
        protected void lbtn_prod_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Produtos.aspx{RetornaUser()}");
            active = 2;
        }
        protected void lbtn_about_Click(object sender, EventArgs e)
        {
            Response.Redirect($"About.aspx{RetornaUser()}");
        }
        protected void lbtn_contacto_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Contactos.aspx{RetornaUser()}");
        }
        protected void lbtn_backoffice_Click(object sender, EventArgs e)
        {
            Response.Redirect($"Backoffice.aspx{RetornaUser()}");            
        }
        protected void btn_img_carrinho_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect($"Carrinho.aspx{RetornaUser()}");
        }
        protected void lbtn_login_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                // Caso já esteja logado, enviar para o perfil do cliente (area cliente)
                Response.Redirect($"AreaCliente.aspx{RetornaUser()}");
            }
            else
            {
                Response.Redirect($"Login.aspx{RetornaUser()}");

                // Ao clicar em login verifica se já existe alguma sessão ativa (online)
                // E direcciona automaticamente para a HomePage
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);
                //SqlCommand cmd = new SqlCommand();
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = con;

                //string online_user = "";
                //string online_perfil = "";
                //con.Open();
                //cmd.CommandText = $"SELECT username FROM utilizadores where online = 1";
                //online_user = (string)cmd.ExecuteScalar();
                //cmd.CommandText = $"SELECT perfil FROM utilizadores where online = 1";
                //online_perfil = (string)cmd.ExecuteScalar();
                //con.Close();

                //if ((online_user != "" && online_user != null) && (online_perfil != "" && online_perfil != null))
                //{
                //    Response.Redirect($"HomePage.aspx?user={EncryptString(online_user)}&perfil={EncryptString(online_perfil)}");
                //}
                //else
                //{
                //    Response.Redirect($"Login.aspx{RetornaUser()}");
                //}
            }            
        }
        protected void lbtn_logout_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "estado";
            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@username", DecryptString(Request.QueryString["user"]));
            // É necessário no login, aqui segue a vazio para não dar erro
            cmd.Parameters.AddWithValue("@email", "");
            cmd.Parameters.AddWithValue("@estado", "OFF");

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect($"HomePage.aspx");
        }
    }
}