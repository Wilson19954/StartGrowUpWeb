using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StartGlowUp.Models
{
    public class Projetos
    {
        static string conexao = "Server=ESN509VMYSQL;Database=sgu;User id=aluno;Password=Senai1234";
        private string desc, tag, nome, doc_user;
        private string custo;
        private List<byte[]> galeria;

        private string cod ;
        private string data = "";

        public Projetos(string cod, string desc, string tag, string nome, string doc_user, string data, string custo, List<byte[]> galeria) {
            this.cod = cod;
            this.desc = desc;
            this.tag = tag;
            this.nome = nome;
            this.doc_user = doc_user;
            this.data = data;
            this.custo = custo;
            this.galeria = galeria;

        }
        public string CadastrarProjeto() {
           
            data = DateTime.Now.Date.ToString();
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "insert into projetos values(@cod,@desc,@custo,@tag,@nome,@doc,@data)";
                comando.Parameters.AddWithValue("@cod", cod);
                comando.Parameters.AddWithValue("@desc", desc);
                comando.Parameters.AddWithValue("@custo", custo);
                comando.Parameters.AddWithValue("@tag", tag);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@doc", doc_user);
                comando.Parameters.AddWithValue("@data", data);
                comando.ExecuteNonQuery();
                return "Cadastro Concluido";

            } catch (Exception e) {
                return e.Message;

            } finally {
                con.Close();

            }

        }
        public static List<Projetos> ListarProjetosInicial() {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Projetos> lista = new List<Projetos>();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select cod_proj,desc_proj,custo_proj,tag_proj,nome_proj,doc_user,data_proj from projetos";
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    MySqlConnection tempCon = new MySqlConnection(conexao);
                    MySqlCommand comand = new MySqlCommand();
                    List<byte[]> galeria = new List<byte[]>();
                    tempCon.Open();
                    comand.Connection = tempCon;
                    comand.CommandText = "select img_proj from galeria where cod_proj = @cod";
                    comand.Parameters.AddWithValue("@cod", leitor["cod_proj"].ToString());
                    MySqlDataReader lei = comand.ExecuteReader();
                    while (lei.Read()) {
                        byte[] img = (byte[])lei["img_proj"];
                        galeria.Add(img);
                    }
                    tempCon.Close();



                    Projetos p = new Projetos(leitor["cod_proj"].ToString(), leitor["desc_proj"].ToString(), leitor["tag_proj"].ToString(), leitor["nome_proj"].ToString(),
                        leitor["doc_user"].ToString(), leitor["data_proj"].ToString(),leitor["custo_proj"].ToString(),galeria);

                    lista.Add(p);
                }
                con.Close();
                return lista;

            } catch {
                return null;

            }



        }
        public static List<Projetos> ListarProjetosPorTag(string tag_proj) {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Projetos> lista = new List<Projetos>();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select cod_proj,desc_proj,custo_proj,tag_proj,nome_proj,doc_user,data_proj from projetos where tag_proj = @tag";
                comando.Parameters.AddWithValue("@tag", tag_proj);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    MySqlConnection tempCon = new MySqlConnection(conexao);
                    MySqlCommand comand = new MySqlCommand();
                    List<byte[]> galeria = new List<byte[]>();
                    tempCon.Open();
                    comand.Connection = tempCon;
                    comand.CommandText = "select img_proj from galeria where cod_proj = @cod";
                    comand.Parameters.AddWithValue("@cod", leitor["cod_proj"]);
                    MySqlDataReader lei = comand.ExecuteReader();
                    while (lei.Read()) {
                        byte[] img = (byte[])lei["img_proj"];
                        galeria.Add(img);
                    }
                    Projetos p = new Projetos(leitor["cod_proj"].ToString(), leitor["desc_proj"].ToString(), leitor["tag_proj"].ToString(), leitor["nome_proj"].ToString(),
                        leitor["doc_user"].ToString(), leitor["data_proj"].ToString(),leitor["custo_proj"].ToString(),galeria);

                    lista.Add(p);
                    tempCon.Close();
                }
                con.Close();
                return lista;

            } catch {
                return null;

            }



        }
        public static List<Projetos> ListarProjetosDePerfil(string doc_user) {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Projetos> lista = new List<Projetos>();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select cod_proj,desc_proj,custo_proj,tag_proj,nome_proj,doc_user,data_proj from projetos where doc_user = @doc";
                comando.Parameters.AddWithValue("@doc", doc_user);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    MySqlConnection tempCon = new MySqlConnection(conexao);
                    MySqlCommand comand = new MySqlCommand();
                    List<byte[]> galeria = new List<byte[]>();
                    tempCon.Open();
                    comand.Connection = tempCon;
                    comand.CommandText = "select img_proj from galeria where cod_proj = @cod";
                    comand.Parameters.AddWithValue("@cod", leitor["cod_proj"].ToString());
                    MySqlDataReader lei = comand.ExecuteReader();
                    while (lei.Read()) {
                        byte[] img = (byte[])lei["img_proj"];
                        galeria.Add(img);
                    }
                    Projetos p = new Projetos(leitor["cod_proj"].ToString(), leitor["desc_proj"].ToString(), leitor["tag_proj"].ToString(), leitor["nome_proj"].ToString(),
                        leitor["doc_user"].ToString(), leitor["data_proj"].ToString(), leitor["custo_proj"].ToString(),galeria);

                    lista.Add(p);
                    tempCon.Close();
                }
                con.Close();
                return lista;

            } catch {
                return null;

            }



        }

        public string Cod { get => cod; set => cod = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Tag { get => tag; set => tag = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Doc_user { get => doc_user; set => doc_user = value; }
        public string Data { get => data; set => data = value; }
        public string Custo { get => custo; set => custo = value; }
        public List<byte[]> Galeria { get => galeria; set => galeria = value; }
    }
}
