using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace LojaOnline
{
    public partial class Carrinho : System.Web.UI.Page
    {
        int codigo_user;
        string perfil = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            string user = "";            
            decimal total = 0;

            if (Request.QueryString.AllKeys.Contains("user"))
            {
                user = DecryptString(Request.QueryString["user"]);
                perfil = DecryptString(Request.QueryString["perfil"]);

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

            SqlDataSource1.SelectCommand = $"SELECT * FROM produtos prod JOIN carrinho cart ON prod.cod_prod = cart.cod_prod where prod.cod_prod in (SELECT cod_prod FROM carrinho) and cod_user = {codigo_user}";

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

            if(perfil == "Revenda" && total > 0)
            {
                lbl_preco_total.Text = total.ToString() + " €";
                lbl_preco_total.Font.Strikeout = true;
                lbl_preco_total.ForeColor = System.Drawing.Color.Red;
                lbl_preco_total_revenda.Visible = true;
                lbl_preco_total_revenda.Text = Math.Round((total * 0.80m), 2).ToString() + " €";
            }
            else
            {
                lbl_preco_total.Text = total.ToString() + " €";
                lbl_preco_total.Font.Strikeout = false;
                lbl_preco_total.ForeColor = System.Drawing.Color.Black;
                lbl_preco_total_revenda.Visible = false;
            }                
            
            if(total == 0)
            {
                lbl_carrinho.Visible = true;
                Panel1.Visible = false;
            }
            else
            {
                lbl_carrinho.Visible = false;
                Panel1.Visible = true;
            }
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

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("ImgBtn_delete"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = $"DELETE FROM carrinho WHERE cod_user = {codigo_user} and cod_prod = " + ((ImageButton)e.Item.FindControl("ImgBtn_delete")).CommandArgument;

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect(Request.RawUrl);
            }            
        }

        protected void tb_quant_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            DataListItem item = (DataListItem)t.NamingContainer;
            string cod_prod = ((ImageButton)t.Parent.FindControl("ImgBtn_delete")).CommandArgument;
            int quant = int.Parse(((TextBox)item.FindControl("tb_quant")).Text);     
            
            if( quant > 0 )
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = $"UPDATE carrinho SET quant = {quant} where cod_user = {codigo_user} and cod_prod = {cod_prod}";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();                
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void btn_finalizar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.AllKeys.Contains("user"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = $"SELECT * FROM produtos prod JOIN carrinho cart ON prod.cod_prod = cart.cod_prod where prod.cod_prod in (SELECT cod_prod FROM carrinho) and cod_user = {codigo_user}";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                con.Close();
                con.Open();
                string[] nome = new string[count];
                string[] marca = new string[count];
                string[] cor = new string[count];
                decimal[] preco = new decimal[count];
                int[] quant = new int[count];
                int i = 0;
                decimal total = 0;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    nome[i] = reader.GetString(1);
                    marca[i] = reader.GetString(2);
                    cor[i] = reader.GetString(4);
                    if (perfil == "Revenda")
                    {
                        preco[i] = Math.Round((reader.GetDecimal(8) * 0.80m), 2);
                    }
                    else
                    {
                        preco[i] = reader.GetDecimal(8);
                    }
                    quant[i] = reader.GetInt32(13);
                    total += (preco[i] * Convert.ToDecimal((quant[i])));
                    i++;
                }
                con.Close();

                string caminhoPDF = "assets\\PDF_template\\";

                string pdfTemplate = caminhoPDF + "Template_fatura.pdf";


                string nomePDF = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") + ".pdf";

                string novoFicheiro = caminhoPDF + "Gerados\\" + nomePDF;

                query = $"SELECT * FROM utilizadores where cod_user = {codigo_user}";
                con.Open();
                cmd = new SqlCommand(query, con);
                reader = cmd.ExecuteReader();
                string nome_cliente = "";
                string morada_cliente = "";
                string nif_cliente = "";
                string email_cliente = "";
                while (reader.Read())
                {
                    nome_cliente = reader.GetString(1);
                    morada_cliente = reader.GetString(2);
                    nif_cliente = reader.GetString(3);
                    email_cliente = reader.GetString(6);
                }
                con.Close();

                // Criar PDF
                FileStream fs = new FileStream(Server.MapPath(caminhoPDF + "Gerados") + "\\" + nomePDF, FileMode.Create);

                Document document = new Document(PageSize.A4, 25, 25, 30, 30);

                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                document.Add(new Paragraph($"Nº Fatura: " + nomePDF.Remove(nomePDF.Length - 4)));
                document.Add(new Paragraph(nome_cliente));
                document.Add(new Paragraph(morada_cliente));
                document.Add(new Paragraph("NIF: " + nif_cliente));
                document.Add(new Paragraph("email: " + email_cliente));

                PdfPTable table = new PdfPTable(5);
                table.SpacingBefore = 50;
                table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                // Cria o Header
                table.AddCell("Nome");
                table.AddCell("Marca");
                table.AddCell("Cor");
                table.AddCell("Quant.");
                table.AddCell("Preço");
                // Conteudo/dados da tabela
                for (i = 0; i < nome.Length; i++)
                {
                    table.AddCell(nome[i]);
                    table.AddCell(marca[i]);
                    table.AddCell(cor[i]);
                    table.AddCell(quant[i].ToString());
                    table.AddCell(preco[i].ToString());
                }
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("Total:");
                table.AddCell(total.ToString());

                document.Add(table);

                document.Close();

                writer.Close();

                fs.Close();

                //////// ------------------------- COM TEMPLATE PDF ---------------------///////////
                //PdfReader preader = new PdfReader(pdfTemplate);
                //PdfStamper pstamper = new PdfStamper(preader, new FileStream(novoFicheiro, FileMode.Create));

                //AcroFields pdfFields = pstamper.AcroFields;
                //pdfFields.SetField("nome", lbl_nome.Text);
                //pdfFields.SetField("morada", lbl_morada.Text);
                //pdfFields.SetField("cp", lbl_cp.Text);
                //pdfFields.SetField("email", lbl_email.Text);
                //pdfFields.SetField("curso", lbl_curso.Text);

                //pstamper.Close();

                // Define o numero da encomenda
                query = "SELECT numero FROM encomendas order by 1 desc";

                con.Open();
                cmd = new SqlCommand(query, con);
                int encomenda = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
                encomenda++;

                // Envio fatura por email

                SmtpClient servidor = new SmtpClient();
                MailMessage email = new MailMessage();

                email.From = new MailAddress(ConfigurationManager.AppSettings["SMTP_USER"]);
                email.To.Add(new MailAddress(email_cliente));
                email.Subject = $"Fatura Nº " + nomePDF.Remove(nomePDF.Length - 4);

                email.IsBodyHtml = true;
                email.Body = $"Segue em anexo a fatura da  sua compra com nº encomenda: {encomenda}";

                Attachment anexo = new Attachment(Server.MapPath(caminhoPDF + "Gerados") + "\\" + nomePDF);
                email.Attachments.Add(anexo);

                servidor.Host = ConfigurationManager.AppSettings["SMTP_URL"];
                servidor.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_PORT"]);

                string utilizador = ConfigurationManager.AppSettings["SMTP_USER"];
                string password = ConfigurationManager.AppSettings["SMTP_PASSWORD"];

                servidor.Credentials = new NetworkCredential(utilizador, password);
                servidor.EnableSsl = true;
                servidor.Send(email);

                // Atualizar tabela encomendas                

                FileStream fstream = File.OpenRead(Server.MapPath(caminhoPDF + "Gerados") + "\\" + nomePDF);
                byte[] contentType = new byte[fstream.Length];
                fstream.Read(contentType, 0, (int)fstream.Length);
                fstream.Close();                             

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "inserir_encomenda";

                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@encomenda", encomenda);
                cmd.Parameters.AddWithValue("@fatura", nomePDF.Remove(nomePDF.Length - 4));
                cmd.Parameters.AddWithValue("@cod_user", codigo_user);
                cmd.Parameters.AddWithValue("@pdf", contentType);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                query = $"DELETE FROM carrinho where cod_user = {codigo_user}";

                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Redirect(Request.RawUrl);                
            }
            else
            {
                Response.Redirect("Login.aspx");
            }            
        }
    }
}