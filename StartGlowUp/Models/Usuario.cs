using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StartGlowUp.Models
{
    public class Usuario
    {
        static string conexao = "Server=ESN509VMYSQL;Database=sgu;User id=aluno;Password=Senai1234";
        private string nome, endereco, email, desc, senha, tipo, doc, telefone;
        private byte[] img;

        public Usuario(string nome, string endereco, string email, string desc, string senha, string tipo, string doc, string telefone, byte[] img) {
            this.nome = nome;
            this.endereco = endereco;
            this.email = email;
            this.desc = desc;
            this.senha = senha;
            this.tipo = tipo;
            this.doc = doc;
            this.telefone = telefone;
            this.img = img;
        }
        public static string criptografar(string senha) {
            
            SHA256 sha256 = SHA256.Create();

            
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

           
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                builder.Append(bytes[i].ToString("x2"));
            }

            
            return builder.ToString();
        }
        public string CadastrarUsuario() {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "insert into usuario values(@endereco,@nome,@desc,@doc,@telefone,@email,@img,@tipo,@senha)";
                comando.Parameters.AddWithValue("@endereco", endereco);
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@desc", desc);
                comando.Parameters.AddWithValue("@doc", doc);
                comando.Parameters.AddWithValue("@telefone", telefone);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@img", img);
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.Parameters.AddWithValue("@senha", criptografar(senha));
                comando.ExecuteNonQuery();
                return "Cadastro Concluido!";

            } catch (Exception e) {
                return e.Message;

            } finally {
                con.Close();
            }

        }
        public bool LoginUsuario() {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select doc_user,senha_user from usuario where doc_user = @doc and senha_user = @senha";
                comando.Parameters.AddWithValue("@doc", doc);
                comando.Parameters.AddWithValue("@senha", criptografar(senha));
                MySqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                if (leitor.HasRows) {
                    return true;
                } else {
                    return false;
                }
            } catch {
                return false;
            } finally {
                con.Close();
            }

        }
        public static List<Usuario> PesquisaUsuarioStartUp(string pesquisa)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select nome_user,desc_user,img_user,doc_user from usuario where nome_user = @pesquisa and tipo_user = 'Startup' ";
                comando.Parameters.AddWithValue("@pesquisa", pesquisa);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Usuario u = new Usuario(leitor["nome_user"].ToString(), null, null, leitor["desc_user"].ToString(),
                        null, null, leitor["doc_user"].ToString(), null, (byte[])leitor["img_user"]);
                    lista.Add(u);
                    

                }
                con.Close();
                return lista;

            }
            catch
            {
                return null;
            }


        }
        public static List<Usuario> PesquisaUsuarioPessoaUnica(string pesquisa)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select nome_user,desc_user,img_user,doc_user from usuario where nome_user = @pesquisa and tipo_user = 'Pessoa Unica' ";
                comando.Parameters.AddWithValue("@pesquisa", pesquisa);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Usuario u = new Usuario(leitor["nome_user"].ToString(), null, null, leitor["desc_user"].ToString(),
                        null, null, leitor["doc_user"].ToString(), null, (byte[])leitor["img_user"]);
                    lista.Add(u);


                }
                con.Close();
                return lista;

            }
            catch
            {
                return null;
            }


        }
        public static List<Usuario> PesquisaUsuarioInvestidor(string pesquisa)
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Usuario> lista = new List<Usuario>();
            try
            {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select nome_user,desc_user,img_user,doc_user from usuario where nome_user = @pesquisa and tipo_user = 'investidor' ";
                comando.Parameters.AddWithValue("@pesquisa", pesquisa);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Usuario u = new Usuario(leitor["nome_user"].ToString(), null, null, leitor["desc_user"].ToString(),
                        null, null, leitor["doc_user"].ToString(), null, (byte[])leitor["img_user"]);
                    lista.Add(u);


                }
                con.Close();
                return lista;

            }
            catch
            {
                return null;
            }


        }
        public string EditarPerfil()
        {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try
            {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "update usuario set nome_user = @nome , desc_user = @desc ,img_user = @img , senha_user = @senha ,telefone_user = @telefone , endereco_user = @endereco , tipo_user = @tipo where doc_user = @doc ";
                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@desc", desc);
                comando.Parameters.AddWithValue("@img", img);
                comando.Parameters.AddWithValue("@senha", senha);
                comando.Parameters.AddWithValue("@doc", doc);
                comando.Parameters.AddWithValue("@telefone", telefone);
                comando.Parameters.AddWithValue("@endereco", endereco);
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.ExecuteNonQuery();
                con.Close();
                return "Alterado com Succeso";
            }catch(Exception e)
            {
                return e.Message;
            }
        }


        public string Nome { get => nome; set => nome = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Email { get => email; set => email = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Doc { get => doc; set => doc = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public byte[] Img { get => img; set => img = value; }
    }
}
