using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using StartGlowUp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StartGlowUp.Controllers
{
    public class ProjetosController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
        public IActionResult CadastrarProjeto() 
        {
            if(HttpContext.Session.GetString("user") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }
           

        }
        [HttpPost]
        public IActionResult CadastrarProjeto(string desc,string custo,string tag,string doc,string nome,string cod = "") 
        
            {
            Random r = new Random();
            cod = r.Next().ToString();
            Projetos p = new Projetos(cod, desc, tag, nome, doc, null, custo, null);
            p.CadastrarProjeto();
            foreach (IFormFile arquivo in Request.Form.Files) 
            {
                string tipoArquivo = arquivo.ContentType;
                if (tipoArquivo.Contains("png") ||
                    tipoArquivo.Contains("jpeg")) 
                {
                    MemoryStream s = new MemoryStream();
                    arquivo.CopyToAsync(s);
                    byte[] img = s.ToArray();
                    try 
                    {
                        int numero = 0;
                        
                        numero = r.Next(1,1000);
                        
                        MySqlConnection con = new MySqlConnection("Server=ESN509VMYSQL;Database=sgu;User id=aluno;Password=Senai1234");
                        MySqlCommand comando = new MySqlCommand();
                        con.Open();
                        comando.Connection = con;
                        comando.CommandText = "insert into galeria values(@img,@numero,@cod_proj)";
                        comando.Parameters.AddWithValue("@img", img);
                        comando.Parameters.AddWithValue("@numero", numero);
                        comando.Parameters.AddWithValue("@cod_proj", cod);
                        comando.ExecuteNonQuery();
                        con.Close();


                       

                    }catch 
                    {
                        return null;

                    }

                }

            }
           
            return RedirectToAction("CadastrarProjeto");

        }
        public IActionResult ListarProjetoInicial() 
        {
            return View(Projetos.ListarProjetosInicial());

        }
        public IActionResult ListarProjetoPorTag(string tag) 
        {
            return View(Projetos.ListarProjetosPorTag(tag));

        }
        public IActionResult ListarProjetoDePerfil(string doc_user) 
        {
            if (HttpContext.Session.GetString("user") != null)
            {


                Usuario u = JsonConvert.DeserializeObject<Usuario>
                (HttpContext.Session.GetString("user").ToString());
                doc_user = u.Doc;
                return View(Projetos.ListarProjetosDePerfil(doc_user));
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }

        }
    }
}
