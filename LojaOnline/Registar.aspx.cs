using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace LojaOnline
{
    public partial class Registar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_registar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "inserir_utilizador";

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@nome", tb_nome.Text);
            cmd.Parameters.AddWithValue("@morada", tb_morada.Text);
            cmd.Parameters.AddWithValue("@nif", tb_nif.Text);
            cmd.Parameters.AddWithValue("@telemovel", tb_tlmvl.Text);
            cmd.Parameters.AddWithValue("@username", tb_username.Text);
            cmd.Parameters.AddWithValue("@email", tb_email.Text);
            cmd.Parameters.AddWithValue("@password", EncryptString(tb_pass.Text));
            //cmd.Parameters.AddWithValue("@avatar", null);
            //cmd.Parameters.AddWithValue("@perfil", null);

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
                lbl_mensagem.Text = "Verifique a sua conta de email para ativar a sua conta de registo!";

                SmtpClient servidor = new SmtpClient();
                MailMessage email = new MailMessage();

                // Vai buscar o email de envio ao webconfig
                email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                email.To.Add(new MailAddress(tb_email.Text));
                email.Subject = "Ativação de conta";

                email.IsBodyHtml = true;
                email.Body = $"Obrigado <b>{tb_nome.Text}</b> pelo registo na nossa aplicação.<br/>Para ativar a sua conta clique <a href='https://localhost:44300/ativacao.aspx?user={EncryptString(tb_username.Text)}'>aqui</a>";

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                servidor.Credentials = new NetworkCredential(utilizador, password);
                servidor.EnableSsl = true;
                servidor.Send(email);
            }
            else if (respostaSP == 2)
            {
                lbl_mensagem.Text = "Username já existe!";
            }
            else
            {
                lbl_mensagem.Text = "Email já existe!";
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