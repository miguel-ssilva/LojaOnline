using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Security.Cryptography;
using iTextSharp.text;

namespace LojaOnline
{
    public partial class Produtos : System.Web.UI.Page
    {
        int codigo_user;
        string query_where = "";
        string ordena = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void ChamaSelect(string tipo)
        {
            if (query_where != "")
            {
                SqlDataSource1.SelectCommand = $"SELECT * FROM [produtos] where {tipo} in ({query_where}){ordena}";
            }
            else
            {
                SqlDataSource1.SelectCommand = $"SELECT * FROM [produtos]{ordena}";
            }            
        }
        protected void cbl_genero_SelectedIndexChanged(object sender, EventArgs e)
        {
            query_where = "";

            if (cbl_genero.Items[0].Selected)
            {
                query_where += "'Homem'";
            }
            if (cbl_genero.Items[1].Selected)
            {
                if(query_where != "")
                {
                    query_where += ",'Mulher'";
                }
                else
                {
                    query_where += "'Mulher'";
                }                
            }
            if (cbl_genero.Items[2].Selected)
            {
                if (query_where != "")
                {
                    query_where += ",'Criança'";
                }
                else
                {
                    query_where += "'Criança'";
                }                
            }

            ChamaSelect("moda");            
        }

        protected void cbl_cor_SelectedIndexChanged(object sender, EventArgs e)
        {
            query_where = "";

            for (int i = 0; i < cbl_cor.Items.Count; i++)
            {
                if (cbl_cor.Items[i].Selected)
                {
                    if (query_where != "")
                    {
                        query_where += $",'{cbl_cor.Items[i].Text}'";
                    }
                    else
                    {
                        query_where += $"'{cbl_cor.Items[i].Text}'";
                    }

                }
            }

            ChamaSelect("cor");
        }
        protected void cbl_marca_SelectedIndexChanged(object sender, EventArgs e)
        {
            query_where = "";

            for (int i = 0; i < cbl_marca.Items.Count; i++)
            {
                if (cbl_marca.Items[i].Selected)
                {
                    if (query_where != "")
                    {
                        query_where += $",'{cbl_marca.Items[i].Text}'";
                    }
                    else
                    {
                        query_where += $"'{cbl_marca.Items[i].Text}'";
                    }

                }
            }

            ChamaSelect("marca");
        }

        protected void btn_pesquisar_Click(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = $"SELECT * FROM [produtos] where nome like '%{tb_pesquisa.Text}%'";
        }

        protected void btn_A_Z_Click(object sender, EventArgs e)
        {
            ordena = " order by nome asc";
            ChamaSelect("");
        }

        protected void btn_Z_A_Click(object sender, EventArgs e)
        {
            ordena = " order by nome desc";
            ChamaSelect("");
        }

        protected void btn_precoBaixo_Click(object sender, EventArgs e)
        {
            ordena = " order by preco asc";
            ChamaSelect("");
        }

        protected void btn_precoAlto_Click(object sender, EventArgs e)
        {
            ordena = " order by preco desc";
            ChamaSelect("");
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
        public void Select_User()
        {
            string user = "";

            if (Request.QueryString.AllKeys.Contains("user"))
            {
                user = DecryptString(Request.QueryString["user"]);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
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
        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("btn_comprar"))
            {
                Select_User();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "inserir_carrinho";

                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@cod_user", codigo_user);
                cmd.Parameters.AddWithValue("@cod_prod", ((Button)e.Item.FindControl("btn_comprar")).CommandArgument);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            string perfil = "";
            if (Request.QueryString.AllKeys.Contains("perfil"))
            {
                perfil = DecryptString(Request.QueryString["perfil"]);
            }
                
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddl_tamanho");

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT categoria FROM produtos where cod_prod = {((Button)ddl.Parent.FindControl("btn_comprar")).CommandArgument}";
            con.Open();
            string categoria = (string)cmd.ExecuteScalar();
            con.Close();

            if (categoria != "Calçado")
            {
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("S");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("M");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("L");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("XL");
            }
            else
            {
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("38");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("39");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("40");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("41");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("42");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("43");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("44");
                ((DropDownList)e.Item.FindControl("ddl_tamanho")).Items.Add("45");
            }
            
            if(perfil == "Revenda")
            {
                ((Label)e.Item.FindControl("lbl_preco_revenda")).Visible = true;
                ((Label)e.Item.FindControl("lbl_preco")).Font.Strikeout = true;
                ((Label)e.Item.FindControl("lbl_preco_revenda")).ForeColor = System.Drawing.Color.Red;
                ((Label)e.Item.FindControl("lbl_preco_revenda")).Font.Bold = true;
            }
        }
    }
}