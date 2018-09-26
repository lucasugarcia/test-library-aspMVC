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
    public class UsuarioController : Controller
    {
        // GET: Usuario

        [Route("Usuarios", Name = "ListaUsuarios")]
        public ActionResult Index()
        {
            object usuarioLogado = Session["usuarioLogado"];
            if (usuarioLogado != null)
            {
                UsuariosDAO dao = new UsuariosDAO();
                IList<Usuario> usuarios = dao.Lista();
                ViewBag.Usuarios = usuarios;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Form()
        {
            ViewBag.Usuario = new Usuario();
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                UsuariosDAO dao = new UsuariosDAO();
                dao.Adiciona(usuario);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Usuario = usuario;
                return View("Form");
            }
        }

        [Route("Usuarios/{Id}", Name = "AlterarUsuario")]
        public ActionResult FormUp(int Id)
        {
            UsuariosDAO dao = new UsuariosDAO();
            ViewBag.Usuario = dao.BuscaPorId(Id);
            return View();
        }

        [HttpPost]
        public ActionResult Atualiza(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                UsuariosDAO dao = new UsuariosDAO();
                dao.Atualiza(usuario);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Usuario = usuario;
                return View("Form");
            }
        }
    }
}