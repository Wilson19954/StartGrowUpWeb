using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartGlowUp.Models
{
    public class Comentario
    {
        static string conexao = "Server=ESN509VMYSQL;Database=sgu;User id=aluno;Password=Senai1234";
        private string  doc_user,texto;
        private int cod_pub, id;

        public Comentario(string doc_user, string texto, int cod_pub, int id) {
            this.doc_user = doc_user;
            this.texto = texto;
            this.cod_pub = cod_pub;
            this.id = id;
            
        }
        public string Comentar() {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "insert into comentario values(@texto,@doc_user,@cod_pub)";
                
                comando.Parameters.AddWithValue("@texto", texto);
                comando.Parameters.AddWithValue("@doc_user", doc_user);
                comando.Parameters.AddWithValue("@cod_pub", cod_pub);
                comando.ExecuteNonQuery();
                con.Close();
                return "Comentado Com Sucesso";

            }catch(Exception e) {
                return e.Message;
            }
            

        }
        public static List<Comentario> ListarComentario(int cod_pub) {
            MySqlConnection con = new MySqlConnection(conexao);
            MySqlCommand comando = new MySqlCommand();
            List<Comentario> lista = new List<Comentario>();
            
            try {
                con.Open();
                comando.Connection = con;
                comando.CommandText = "select id_comentario,comentarios,doc_user,cod_pub from comentario where cod_pub = @cod";
                comando.Parameters.AddWithValue("@cod", cod_pub);
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read()) {
                    Comentario c = new Comentario(leitor["doc_user"].ToString(), leitor["comentarios"].ToString(), (int)leitor["cod_pub"], (int)leitor["id_comentario"]);
                    lista.Add(c);
                }
                con.Close();
                return lista;
            } catch {
                return null;
            }


        }



        public string Doc_user { get => doc_user; set => doc_user = value; }
        public string Texto { get => texto; set => texto = value; }
        public int Cod_pub { get => cod_pub; set => cod_pub = value; }
        public int Id { get => id; set => id = value; }

    }
}
