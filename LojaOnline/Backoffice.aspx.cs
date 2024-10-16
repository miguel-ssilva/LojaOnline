using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LojaOnline
{
    public partial class Backoffice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request.QueryString.AllKeys.Contains("user"))
            {
                Response.Redirect("HomePage.aspx");
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbl_cod")).Text = dr["cod_user"].ToString();
                ((TextBox)e.Item.FindControl("tb_nome")).Text = dr["nome"].ToString();
                ((TextBox)e.Item.FindControl("tb_morada")).Text = dr["morada"].ToString();
                ((Label)e.Item.FindControl("lbl_nif")).Text = dr["nif"].ToString();
                ((TextBox)e.Item.FindControl("tb_telemovel")).Text = dr["telemovel"].ToString();
                ((TextBox)e.Item.FindControl("tb_username")).Text = dr["username"].ToString();
                ((TextBox)e.Item.FindControl("tb_email")).Text = dr["email"].ToString();
                ((DropDownList)e.Item.FindControl("ddl_perfil")).Text = dr["perfil"].ToString();
                if (dr["ativo"].ToString() == "True")
                {
                    ((Label)e.Item.FindControl("lbl_ativo")).Text = "Sim";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_ativo")).Text = "Não";
                }
                if (dr["online"].ToString() == "True")
                {
                    ((Label)e.Item.FindControl("lbl_online")).Text = "Sim";
                }
                else
                {
                    ((Label)e.Item.FindControl("lbl_online")).Text = "Não";
                }
                //((Label)e.Item.FindControl("lbl_ativo")).Text = dr["ativo"].ToString();
                //((Label)e.Item.FindControl("lbl_online")).Text = dr["online"].ToString();

                ((ImageButton)e.Item.FindControl("btn_gravar")).CommandArgument = dr["cod_user"].ToString();
            }
        }

        protected void btn_utilizadores_Click(object sender, EventArgs e)
        {
            pnl_utilizadores.Visible = true;
            pnl_produtos.Visible = false;
            pnl_inserirProd.Visible = false;
            btn_inserir.Visible = false;
            
        }
        protected void btn_produtos_Click(object sender, EventArgs e)
        {
            pnl_utilizadores.Visible = false;
            pnl_produtos.Visible = true;
            pnl_inserirProd.Visible = false;
            btn_inserir.Visible = true;
            
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("btn_gravar"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = "UPDATE utilizadores SET ";
                query += "nome='" + ((TextBox)e.Item.FindControl("tb_nome")).Text + "',";
                query += "morada='" + ((TextBox)e.Item.FindControl("tb_morada")).Text + "',";
                query += "telemovel=" + ((TextBox)e.Item.FindControl("tb_telemovel")).Text + ",";
                query += "username='" + ((TextBox)e.Item.FindControl("tb_username")).Text + "',";
                query += "email='" + ((TextBox)e.Item.FindControl("tb_email")).Text + "',";
                query += "perfil='" + ((DropDownList)e.Item.FindControl("ddl_perfil")).Text + "' ";
                query += "where cod_user=" + ((ImageButton)e.Item.FindControl("btn_gravar")).CommandArgument;

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            if (e.CommandName.Equals("btn_apagar"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = "DELETE FROM utilizadores WHERE cod_user = " + ((ImageButton)e.Item.FindControl("btn_gravar")).CommandArgument;

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void btn_saveall_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            string query = "";

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                query += "UPDATE utilizadores SET ";
                query += "nome='" + ((TextBox)Repeater1.Items[i].FindControl("tb_nome")).Text + "',";
                query += "morada='" + ((TextBox)Repeater1.Items[i].FindControl("tb_morada")).Text + "',";
                query += "telemovel=" + ((TextBox)Repeater1.Items[i].FindControl("tb_telemovel")).Text + ",";
                query += "username='" + ((TextBox)Repeater1.Items[i].FindControl("tb_username")).Text + "',";
                query += "email='" + ((TextBox)Repeater1.Items[i].FindControl("tb_email")).Text + "',";
                query += "perfil='" + ((DropDownList)Repeater1.Items[i].FindControl("ddl_perfil")).Text + "' ";                
                query += "where cod_user=" + ((Label)Repeater1.Items[i].FindControl("lbl_cod")).Text + ";";
            }

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Redirect(Request.RawUrl);
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                ((Label)e.Item.FindControl("lbl_cod")).Text = dr["cod_prod"].ToString();
                ((TextBox)e.Item.FindControl("tb_nome")).Text = dr["nome"].ToString();
                ((TextBox)e.Item.FindControl("tb_marca")).Text = dr["marca"].ToString();
                ((TextBox)e.Item.FindControl("tb_cor")).Text = dr["cor"].ToString();
                ((TextBox)e.Item.FindControl("tb_categoria")).Text = dr["categoria"].ToString();
                ((TextBox)e.Item.FindControl("tb_moda")).Text = dr["moda"].ToString();
                ((TextBox)e.Item.FindControl("tb_stock")).Text = dr["stock"].ToString();
                ((TextBox)e.Item.FindControl("tb_preco")).Text = dr["preco"].ToString();     
                
                ((ImageButton)e.Item.FindControl("btn_gravar")).CommandArgument = dr["cod_prod"].ToString();
            }
        }

        protected void btn_inserir_Click(object sender, EventArgs e)
        {
            pnl_utilizadores.Visible = false;
            pnl_produtos.Visible = false;
            pnl_inserirProd.Visible = true;
            btn_inserir.Visible = false;            
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("btn_gravar"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = "UPDATE produtos SET ";
                query += "nome='" + ((TextBox)e.Item.FindControl("tb_nome")).Text + "',";
                query += "marca='" + ((TextBox)e.Item.FindControl("tb_marca")).Text + "',";
                query += "cor='" + ((TextBox)e.Item.FindControl("tb_cor")).Text + "',";
                query += "categoria='" + ((TextBox)e.Item.FindControl("tb_categoria")).Text + "',";
                query += "moda='" + ((TextBox)e.Item.FindControl("tb_moda")).Text + "',";
                query += "stock= " + ((TextBox)e.Item.FindControl("tb_stock")).Text + ",";
                query += "preco= " + ((TextBox)e.Item.FindControl("tb_preco")).Text.Replace(",", ".") + " ";
                query += "where cod_prod=" + ((ImageButton)e.Item.FindControl("btn_gravar")).CommandArgument;

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            if (e.CommandName.Equals("btn_apagar"))
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                string query = "DELETE FROM produtos WHERE cod_prod = " + ((ImageButton)e.Item.FindControl("btn_gravar")).CommandArgument;

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void btn_saveall_prod_Click(object sender, ImageClickEventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

            string query = "";

            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                query += "UPDATE produtos SET ";
                query += "nome='" + ((TextBox)Repeater2.Items[i].FindControl("tb_nome")).Text + "',";
                query += "marca='" + ((TextBox)Repeater2.Items[i].FindControl("tb_marca")).Text + "',";
                query += "cor='" + ((TextBox)Repeater2.Items[i].FindControl("tb_cor")).Text + "',";
                query += "categoria='" + ((TextBox)Repeater2.Items[i].FindControl("tb_categoria")).Text + "',";
                query += "moda='" + ((TextBox)Repeater2.Items[i].FindControl("tb_moda")).Text + "',";
                query += "stock= " + ((TextBox)Repeater2.Items[i].FindControl("tb_stock")).Text + ",";                
                query += "preco= " + ((TextBox)Repeater2.Items[i].FindControl("tb_preco")).Text.Replace(",",".") + " ";                
                query += "where cod_prod=" + ((Label)Repeater2.Items[i].FindControl("lbl_cod")).Text + ";";
            }

            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);            
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Redirect(Request.RawUrl);
        }

        protected void btn_inserirProd_Click(object sender, EventArgs e)
        {
            if(tb_inserirNome.Text != "" && tb_inserirPreco.Text != "")
            {
                Stream imgStream = FileUpload1.PostedFile.InputStream;
                int tamanhoFich = FileUpload1.PostedFile.ContentLength;
                string contentType = FileUpload1.PostedFile.ContentType;

                byte[] imgBinaryData = new byte[tamanhoFich];
                imgStream.Read(imgBinaryData, 0, tamanhoFich);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LojaOnlineConnectionString"].ConnectionString);

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "inserir_produto";

                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@nome", tb_inserirNome.Text);
                cmd.Parameters.AddWithValue("@marca", tb_inserirMarca.Text);
                cmd.Parameters.AddWithValue("@tamanho", ddl_inserirTamanho.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@cor", tb_inserirCor.Text);
                cmd.Parameters.AddWithValue("@categoria", ddl_inserirCategoria.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@moda", ddl_inserirModa.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@stock", tb_inserirStock.Text);
                string preco = tb_inserirPreco.Text;
                if (preco.Contains("."))
                {
                    cmd.Parameters.AddWithValue("@preco", Convert.ToDecimal(preco.Replace(".", ",")));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@preco", Convert.ToDecimal(preco));
                }

                cmd.Parameters.AddWithValue("@imagem", imgBinaryData);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                tb_inserirNome.Text = "";
                tb_inserirMarca.Text = "";
                tb_inserirCor.Text = "";                
            }            
        }
    }    
}