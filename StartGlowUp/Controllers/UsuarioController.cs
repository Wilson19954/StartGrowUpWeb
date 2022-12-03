using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using StartGlowUp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StartGlowUp.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
        public ActionResult CadastrarUsuario() 
        {

            if (HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }

        }
        [HttpPost]
        public ActionResult CadastrarUsuario(string nome, string endereco, string email, string desc, string senha, string tipo, string doc, string telefone) 
        {
            IFormFile arquivo = Request.Form.Files[0];
            string tipoArquivo = arquivo.ContentType;
            if(tipoArquivo.Contains("png") || tipoArquivo.Contains("jpeg")) 
            {
                MemoryStream s = new MemoryStream();
                arquivo.CopyToAsync(s);
                byte[] img = s.ToArray();
                Usuario u = new Usuario(nome, endereco, email, desc, senha, tipo, doc, telefone, img);
                u.CadastrarUsuario();

            }

            return RedirectToAction ("LogarUsuario");

        }
        public ActionResult LogarUsuario() 
        {
            if(HttpContext.Session.GetString("user") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }
            

        }
        [HttpPost]
        public ActionResult LogarUsuario(string doc,string senha) 
        {
            MySqlConnection con = new MySqlConnection("Server=ESN509VMYSQL;Database=sgu;User id=aluno;Password=Senai1234");
            MySqlCommand comando = new MySqlCommand();
            string nome = "";
            string endereco = "";
            string email = "";
            string desc = "";
            string tipo = "";
            string telefone = "";
            byte[] img = null;

            con.Open();
            comando.Connection = con;
            comando.CommandText = "select nome_user,endereco_user,email_user,desc_user,tipo_user,telefone_user,img_user from usuario where doc_user = @doc and senha_user = @senha ";
            comando.Parameters.AddWithValue("@doc", doc);
            comando.Parameters.AddWithValue("@senha",Usuario.criptografar(senha));
            MySqlDataReader leitor = comando.ExecuteReader();
            while (leitor.Read()) 
            {
                nome = leitor["nome_user"].ToString();
                endereco = leitor["endereco_user"].ToString();
                email = leitor["email_user"].ToString();
                desc = leitor["desc_user"].ToString();
                tipo = leitor["tipo_user"].ToString();
                telefone = leitor["telefone_user"].ToString();
                img = (byte[])leitor["img_user"];

            }
            con.Close();
            Usuario u = new Usuario(nome, endereco, email, desc, senha, tipo, doc, telefone, img);
            if (u.LoginUsuario()) 
            {
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));
                return RedirectToAction("LogarUsuario");

            } 
            else 
            {
                TempData["erro"] = "erro";
                return RedirectToAction("LogarUsuario");

            }

        }
        public ActionResult Sair() 
        {
            if (HttpContext.Session.GetString("user") != null)
            {


                HttpContext.Session.Remove("user");
                return RedirectToAction("LogarUsuario");
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }

        }
        public ActionResult PesquisaUsuarioStartUp(string pesquisa)
        {
            return View(Usuario.PesquisaUsuarioStartUp(pesquisa));

        }
        public ActionResult PesquisaUsuarioInvestidor(string pesquisa)
        {
            return View(Usuario.PesquisaUsuarioInvestidor(pesquisa));

        }
        public ActionResult PesquisaUsuarioPessoaUnica(string pesquisa)
        {
            return View(Usuario.PesquisaUsuarioPessoaUnica(pesquisa));

        }
        public IActionResult EditarPerfil()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditarPerfil(string nome ,string email, string senha,string telefone,string doc,string tipo,string desc,string endereco)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                Usuario session = JsonConvert.DeserializeObject<Usuario>(HttpContext.Session.GetString("user").ToString());
                doc = session.Doc;
                IFormFile arquivo = Request.Form.Files[0];
                string tipoArquivo = arquivo.ContentType;

                if (tipoArquivo.Contains("png") || tipoArquivo.Contains("jpeg"))
                {
                    MemoryStream s = new MemoryStream();
                    arquivo.CopyToAsync(s);
                    byte[] img = s.ToArray();
                    Usuario u = new Usuario(nome, endereco, email, desc, senha, tipo, doc, telefone, img);
                    u.EditarPerfil();
                    HttpContext.Session.Remove("user");
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(u));


                }
                return RedirectToAction("ListarPublicacoesPerfil", "Publicacoes");
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }

        }
    }
}
