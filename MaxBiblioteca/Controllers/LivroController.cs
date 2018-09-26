using MaxBiblioteca.DAO;
using MaxBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaxBiblioteca.Filtros;

namespace MaxBiblioteca.Controllers
{
    [AutorizacaoFilter]
    public class LivroController : Controller
    {
        // GET: Livro

        [Route("Livros", Name = "ListaLivros")]
        public ActionResult Index()
        {
            object usuarioLogado = Session["usuarioLogado"];
            if (usuarioLogado != null)
            {
                LivrosDAO dao = new LivrosDAO();
                IList<Livro> livros = dao.Lista();
                ViewBag.Livros = livros;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Form()
        {
            ViewBag.Livro = new Livro();
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Livro livro)
        {
            if (ModelState.IsValid)
            {
                LivrosDAO dao = new LivrosDAO();
                dao.Adiciona(livro);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Livro = livro;
                return View("Form");
            }
        }

        [Route("Livros/{id}", Name = "AlterarLivro")]
        public ActionResult FormUp(int Id)
        {
            LivrosDAO dao = new LivrosDAO();
            ViewBag.Livro = dao.BuscaPorId(Id);
            return View();
        }

        [HttpPost]
        public ActionResult Atualiza(Livro livro)
        {
            if (ModelState.IsValid)
            {
                LivrosDAO dao = new LivrosDAO();
                dao.Atualiza(livro);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Livro = livro;
                return View("Form");
            }
        }
    }
}