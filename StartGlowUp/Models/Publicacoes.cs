using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace StartGlowUp.Models
{
    public class Publicacoes
    {
        static string conexao = "Server=ESN509VMYSQL;Database=sgu;User id=aluno;Password=Senai1234";
        private string desc, cod, doc_user, tag;
        private int like;
        private byte[] img;
        private string data = "";

        public Publicacoes(string desc, string cod, string doc_user, string tag, int like, byte[] img) {
            this.desc = desc;
            this.cod = cod;
            this.doc_user = doc_user;
            this.tag = tag;
            this.like = like;
            this.img = img;
        }
        public string Publicar() {
            like = 0;
            data = DateTime.Now.Date.ToString();
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();

            try {

                con.Open();
                comando.Connection = con;
                comando.CommandText = "insert into publicacoes(img_pub,desc_pub,like_pub,tag_pub,doc_user,data_pub) values(@img,@desc,@like,@tag,@doc,@data)";
                comando.Parameters.AddWithValue("@img", img);
                comando.Parameters.AddWithValue("@desc", desc);
                comando.Parameters.AddWithValue("@tag", tag);
                comando.Parameters.AddWithValue("@like", like);
                comando.Parameters.AddWithValue("@doc", doc_user);
                comando.Parameters.AddWithValue("@data", data);
                comando.ExecuteNonQuery();
                return "Publicado Com Sucesso";
            } catch (Exception e) {
                return e.Message;
            } finally {
                con.Close();
            }



        }
        public static string AdicionarLike(string cod_pub) {
            int like = 0;
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select like_pub from publicacoes where cod_pub = @cod";
                comando.Parameters.AddWithValue("@cod", cod_pub);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    like = (int)leitor["like_pub"] + 1;
                }
                con.Close();
                con.Open();
                comando.CommandText = "update publicacoes set like_pub = @like where cod_pub = @cod ";
                comando.Parameters.AddWithValue("@like", like);
                
                comando.ExecuteNonQuery();
                con.Close();
                return ";)";
            } catch {
                return "Algo Deu Errado :(";
            }

        }
        public static List<Publicacoes> ListaPublicacoes() {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Publicacoes> lista = new List<Publicacoes>();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select img_pub,desc_pub,like_pub,tag_pub,cod_pub,doc_user,data_pub from publicacoes";
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    Publicacoes p = new Publicacoes(leitor["desc_pub"].ToString(), leitor["cod_pub"].ToString(), leitor["doc_user"].ToString(), leitor["tag_pub"].ToString(), (int)leitor["like_pub"], (byte[])leitor["img_pub"]);
                    lista.Add(p);
                }
                con.Close();
                return lista;


            } catch {
                return null;
            }

        }
        public static List<Publicacoes> ListaPublicacoesComTag(string tag_pub) {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Publicacoes> lista = new List<Publicacoes>();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select img_pub,desc_pub,like_pub,tag_pub,cod_pub,doc_user,data_pub from publicacoes where tag_pub = @tag";
                comando.Parameters.AddWithValue("@tag", tag_pub);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    Publicacoes p = new Publicacoes(leitor["desc_pub"].ToString(), leitor["cod_pub"].ToString(), leitor["doc_user"].ToString(), leitor["tag_pub"].ToString(), (int)leitor["like_pub"], (byte[])leitor["img_pub"]);
                    lista.Add(p);
                }
                con.Close();
                return lista;


            } catch {
                return null;
            }

        }
        public static List<Publicacoes> ListaPublicacoesDePerfil(string doc_user) {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Publicacoes> lista = new List<Publicacoes>();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select img_pub,desc_pub,like_pub,tag_pub,cod_pub,doc_user,data_pub from publicacoes where doc_user = @doc";
                comando.Parameters.AddWithValue("@doc", doc_user);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    Publicacoes p = new Publicacoes(leitor["desc_pub"].ToString(), leitor["cod_pub"].ToString(), leitor["doc_user"].ToString(), leitor["tag_pub"].ToString(), (int)leitor["like_pub"], (byte[])leitor["img_pub"]);
                    lista.Add(p);
                }
                con.Close();
                return lista;


            } catch {
                return null;
            }

        }
        public static string RetirarLike(string cod_pub) {
            int like = 0;
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select like_pub from publicacoes where cod_pub = @cod";
                comando.Parameters.AddWithValue("@cod", cod_pub);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    like = (int)leitor["like_pub"] - 1;
                }
                con.Close();
                con.Open();
                comando.CommandText = "update publicacoes set like_pub = @like where cod_pub = @cod ";
                comando.Parameters.AddWithValue("@like", like);
                comando.Parameters.AddWithValue("@cod", cod_pub);
                comando.ExecuteNonQuery();
                con.Close();
                return ";)";
            } catch {
                return "Algo Deu Errado :(";
            }

        }



        public string Desc { get => desc; set => desc = value; }
        public string Cod { get => cod; set => cod = value; }
        public string Doc_user { get => doc_user; set => doc_user = value; }
        public string Tag { get => tag; set => tag = value; }
        public int Like { get => like; set => like = value; }
        public byte[] Img { get => img; set => img = value; }
    }
}
