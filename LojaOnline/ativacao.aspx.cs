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
    public partial class ativacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = DecryptString(Request.QueryString["user"]);
            // Só para validar se está a desencriptar o utilizador
            //Response.Write(utilizador);

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "ativacao_user";

            cmd.Connection = con;

            cmd.Parameters.AddWithValue("@username", username);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
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

        protected void lbtn_home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Homepage.aspx");
        }
    }
}