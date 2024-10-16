using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Drawing;

namespace LojaOnline
{
    public partial class AreaCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.QueryString.AllKeys.Contains("user"))
            {
                Response.Redirect("HomePage.aspx");
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string user = DecryptString(Request.QueryString["user"]);            

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT cod_user FROM utilizadores where username = '{user}'";
            con.Open();
            int codigo_user = (int)cmd.ExecuteScalar();
            con.Close();

            SqlDataSource1.SelectCommand = $"SELECT * FROM [encomendas] where cod_user = {codigo_user} order by 1 desc";
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
        public void PanelVisible()
        {
            pnl_perfil.Visible = false;
            pnl_encomendas.Visible = false;
            pnl_favoritos.Visible = false;
            pnl_definicoes.Visible = false;
            pnl_alterar.Visible = false;

            if(lbtn_perfil.Font.Bold == true)
            {
                pnl_perfil.Visible = true;
            }
            else if (lbtn_encomendas.Font.Bold == true)
            {
                pnl_encomendas.Visible = true;
            }
            else if (lbtn_favoritos.Font.Bold == true)
            {
                pnl_favoritos.Visible = true;
            }
            else if (lbtn_definicoes.Font.Bold == true)
            {
                pnl_definicoes.Visible = true;
            }
            else if (lbtn_alterar.Font.Bold == true)
            {
                pnl_alterar.Visible = true;
            }
        }
        public void lbtnBold()
        {
            lbtn_perfil.Font.Bold = false;
            lbtn_encomendas.Font.Bold = false;
            lbtn_favoritos.Font.Bold = false;
            lbtn_definicoes.Font.Bold = false;
            lbtn_alterar.Font.Bold = false;
            lbtn_sair.Font.Bold = false;
        }

        protected void lbtn_perfil_Click(object sender, EventArgs e)
        {            
            lbtnBold();
            lbtn_perfil.Font.Bold = true;
            PanelVisible();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);            

            string user = DecryptString(Request.QueryString["user"]);
            string query = $"SELECT * FROM utilizadores where username = '{user}'";

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();            

            while (reader.Read())
            {
                tb_nome.Text = reader.GetString(1);
                tb_morada.Text = reader.GetString(2);
                tb_tlmvl.Text = reader.GetString(4);
                tb_email.Text = reader.GetString(6);
            }
            con.Close();
        }
        protected void lbtn_encomendas_Click(object sender, EventArgs e)
        {
            lbtnBold();
            lbtn_encomendas.Font.Bold = true;
            PanelVisible();
        }

        protected void lbtn_favoritos_Click(object sender, EventArgs e)
        {
            lbtnBold();
            lbtn_favoritos.Font.Bold = true;
            PanelVisible();
        }

        protected void lbtn_definicoes_Click(object sender, EventArgs e)
        {
            lbtnBold();
            lbtn_definicoes.Font.Bold = true;
            PanelVisible();
        }

        protected void lbtn_alterar_Click(object sender, EventArgs e)
        {
            lbtnBold();
            lbtn_alterar.Font.Bold = true;
            PanelVisible();
        }
        protected void lbtn_sair_Click(object sender, EventArgs e)
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
                
        protected void btn_alterar_pass_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "autenticacao";
            cmd.Connection = con;

            string user = DecryptString(Request.QueryString["user"]);

            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@email", "");
            cmd.Parameters.AddWithValue("@password", EncryptString(tb_pass_atual.Text));

            SqlParameter valor = new SqlParameter();
            valor.ParameterName = "@retorno";
            valor.Direction = ParameterDirection.Output;
            valor.SqlDbType = SqlDbType.Int;

            cmd.Parameters.Add(valor);

            con.Open();
            cmd.ExecuteNonQuery();
            int respostaSP = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            con.Close();

            if(respostaSP == 1)
            {
                if(tb_pass_nova.Text == tb_pass_repete.Text)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = "alterar_password";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@username", user);                   
                    cmd.Parameters.AddWithValue("@password", EncryptString(tb_pass_nova.Text));

                    con.Open();
                    cmd.ExecuteNonQuery();                    
                    con.Close();

                    lbl_mensagem.Text = "Palavra-passe alterada com sucesso!";
                }
                else
                {
                    lbl_mensagem.Text = "A repetição da palavra-passe deve ser igual à nova";
                }
            }
            else
            {
                lbl_mensagem.Text = "Palavra-passe atual não está correta";
            }            
        }

        protected void btn_alterar_Click(object sender, EventArgs e)
        {
            pnl_perfil.Visible = true;

            if (btn_alterar.Text == "Gravar")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string user = DecryptString(Request.QueryString["user"]);
                string query = $"UPDATE utilizadores SET nome = '{tb_nome.Text}', morada = '{tb_morada.Text}', telemovel = '{tb_tlmvl.Text}', email = '{tb_email.Text}' where username = '{user}'";
                
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                btn_alterar.Text = "Alterar dados";
                btn_cancelar.Visible = false;

                tb_nome.Enabled = false;
                tb_morada.Enabled = false;
                tb_tlmvl.Enabled = false;
                tb_email.Enabled = false;
            }
            else
            {
                btn_alterar.Text = "Gravar";
                btn_cancelar.Visible = true;

                tb_nome.Enabled = true;
                tb_morada.Enabled = true;
                tb_tlmvl.Enabled = true;
                tb_email.Enabled = true;
            }            
        }
        protected void btn_cancelar_Click(object sender, EventArgs e)
        {
            pnl_perfil.Visible = true;

            if (btn_alterar.Text == "Gravar")
            {
                btn_alterar.Text = "Alterar dados"; 
                btn_cancelar.Visible = false;
            }

            tb_nome.Enabled = false;
            tb_morada.Enabled = false;
            tb_tlmvl.Enabled = false;
            tb_email.Enabled = false;
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("lbtn_download"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = $"SELECT pdf_fatura FROM encomendas where cod_encomenda = {((LinkButton)e.Item.FindControl("lbtn_download")).CommandArgument}";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=fatura.pdf");
                Response.BinaryWrite((byte[])cmd.ExecuteScalar());
                Response.End();                
                con.Close();
            }
        }
    }
}