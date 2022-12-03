using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartGlowUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartGlowUp.Controllers
{
    public class ComentarioController : Controller
    {
        public IActionResult Index() {
            return View();
        }
        public IActionResult Comentar() {
            if (HttpContext.Session.GetString("user") != null)
            {


                return View();
            }
            else
            {
                return RedirectToAction("ListarPublicacoes", "Publicacoes");
            }
        }
        [HttpPost]
        public IActionResult Comentar(string texto,string doc_user,int cod_pub) {
            Comentario c = new Comentario(doc_user, texto, cod_pub, 0);
            c.Comentar();
            return RedirectToAction("Comentar");

        }
        public IActionResult ListarComentario(int cod_pub) {
            return View(Comentario.ListarComentario(cod_pub));
        }

    }
}
