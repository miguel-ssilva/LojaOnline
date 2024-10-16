using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace LojaOnline
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtn_registar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registar.aspx");
        }
        protected void lbtn_recuperar_Click(object sender, EventArgs e)
        {
            Response.Redirect("recuperar.aspx");
        }
        protected void btn_login_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "autenticacao";

            cmd.Connection = con;

            if (tb_user.Text.Contains("@"))
            {
                cmd.Parameters.AddWithValue("@username", "");
                cmd.Parameters.AddWithValue("@email", tb_user.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@username", tb_user.Text);
                cmd.Parameters.AddWithValue("@email", "");
            }            
            cmd.Parameters.AddWithValue("@password", EncryptString(tb_pass.Text));

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;

            cmd.Parameters.Add(valor);

            con.Open();
            cmd.ExecuteNonQuery();
            int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            con.Close();

            if (respostaSP == 1)
            {                
                // Atualiza estado online ou offline
                cmd.Parameters.Clear();
                cmd.CommandText = "estado";
                if (tb_user.Text.Contains("@"))
                {
                    cmd.Parameters.AddWithValue("@username", "");
                    cmd.Parameters.AddWithValue("@email", tb_user.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@username", tb_user.Text);
                    cmd.Parameters.AddWithValue("@email", "");
                }                
                cmd.Parameters.AddWithValue("@estado", "ON");
                con.Open();
                cmd.ExecuteNonQuery();                
                con.Close();

                string user = "";
                string perfil = "";
                string resposta_perfil = "";
                string resposta_user = "";

                // Vai buscar username caso o login seja feito com o email
                if (tb_user.Text.Contains("@"))
                {
                    // Retorna tipo de cliente (Admin, Revenda, Normal)
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT perfil FROM utilizadores where email = '{tb_user.Text}'";
                    con.Open();
                    resposta_perfil = (string)cmd.ExecuteScalar();
                    con.Close();

                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT username FROM utilizadores where email = '{tb_user.Text}'";
                    con.Open();
                    resposta_user = (string)cmd.ExecuteScalar();
                    con.Close();                   

                    user = resposta_user;
                    perfil = resposta_perfil;
                }
                else
                {
                    // Retorna tipo de cliente (Admin, Revenda, Normal)
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT perfil FROM utilizadores where username = '{tb_user.Text}'";
                    con.Open();
                    resposta_perfil = (string)cmd.ExecuteScalar();
                    con.Close();                    

                    user = tb_user.Text;
                    perfil = resposta_perfil;
                }                 

                cmd.Connection = con;

                int codigo_user;                
                decimal total = 0;
                decimal total_1 = 0;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT cod_user FROM utilizadores where username = '{user}'";
                con.Open();
                codigo_user = (int)cmd.ExecuteScalar();
                con.Close();                

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT SUM(prod.preco * cart.quant) FROM produtos prod JOIN carrinho cart ON prod.cod_prod = cart.cod_prod where prod.cod_prod in (SELECT cod_prod FROM carrinho) and cart.cod_user = {codigo_user}";
                con.Open();
                try
                {
                    total = (decimal)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    total = 0;
                }
                con.Close();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT SUM(prod.preco * cart.quant) FROM produtos prod JOIN carrinho cart ON prod.cod_prod = cart.cod_prod where prod.cod_prod in (SELECT cod_prod FROM carrinho) and cart.cod_user = 1";
                con.Open();
                try
                {
                    total_1 = (decimal)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    total_1 = 0;
                }
                con.Close();

                if(total == 0 && total_1 > 0)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"UPDATE carrinho SET cod_user = {codigo_user} where cod_user = 1";

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else if(total > 0 && total_1 > 0)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM carrinho where cod_user = 1";

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                if (tb_user.Text.Contains("@"))
                {                   
                    Response.Redirect($"HomePage.aspx?user={EncryptString(resposta_user)}&perfil={EncryptString(resposta_perfil)}");
                }
                else
                { 
                    Response.Redirect($"HomePage.aspx?user={EncryptString(tb_user.Text)}&perfil={EncryptString(resposta_perfil)}");                    
                }
            }
            else if (respostaSP == 0)
            {
                lbl_mensagem.Text = "Utilizador e palavra-passe não existem!";
            }
            else if (respostaSP == 2)
            {
                lbl_mensagem.Text = "Utilizador inativo!";
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


    }
}