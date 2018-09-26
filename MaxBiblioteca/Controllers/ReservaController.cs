using MaxBiblioteca.DAO;
using MaxBiblioteca.Filtros;
using MaxBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxBiblioteca.Controllers
{
    [AutorizacaoFilter]
    public class ReservaController : Controller
    {
        // GET: Reserva
        [Route("Reservas", Name = "ListaReservas")]
        public ActionResult Index()
        {
            object usuarioLogado = Session["usuarioLogado"];
            if (usuarioLogado != null)
            {
                UsuariosDAO daoU = new UsuariosDAO();
                LivrosDAO daoL = new LivrosDAO();
                ReservasDAO daoR = new ReservasDAO();

                var reservas = daoR.Lista();
                ViewBag.Reservas = reservas;

                List<Livro> livrosReservados = new List<Livro>();
                List<Usuario> usuariosQueReservaram = new List<Usuario>();

                foreach (Reserva r in reservas)
                {
                    Usuario u = daoU.BuscaPorId(r.IdUsuario);
                    Livro l = daoL.BuscaPorId(r.IdLivro);

                    usuariosQueReservaram.Add(u);
                    livrosReservados.Add(l);
                }

                ViewBag.LivrosReservados = livrosReservados;
                ViewBag.UsuariosQueReservaram = usuariosQueReservaram;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Form()
        {
            ViewBag.Reserva = new Reserva();

            LivrosDAO dao = new LivrosDAO();
            IList<Livro> livros = dao.Lista().Where(l => l.QuantidadeDeExemplares > 0).ToList();
            ViewBag.Livros = livros;

            UsuariosDAO daoUsr = new UsuariosDAO();
            IList<Usuario> usuarios = daoUsr.Lista();
            ViewBag.Usuarios = usuarios;

            return View();
        }

        [ValidateAntiForgeryToken, HttpPost]
        public ActionResult Adiciona(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                ReservasDAO dao = new ReservasDAO();
                LivrosDAO daoL = new LivrosDAO();
                dao.Adiciona(reserva);
                Livro l = daoL.BuscaPorId(reserva.IdLivro);
                l.QuantidadeDeExemplares--;
                daoL.Atualiza(l);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Reserva = reserva;

                LivrosDAO dao = new LivrosDAO();
                IList<Livro> livros = dao.Lista().Where(l => l.QuantidadeDeExemplares > 0).ToList();
                ViewBag.Livros = livros;

                UsuariosDAO daoUsr = new UsuariosDAO();
                IList<Usuario> usuarios = daoUsr.Lista();
                ViewBag.Usuarios = usuarios;

                return View("Form");
            }
        }

        [Route("Reservas/{Id}", Name = "AlterarReserva")]
        public ActionResult FormUp(int Id)
        {
            LivrosDAO daoL = new LivrosDAO();
            IList<Livro> livros = daoL.Lista().Where(l => l.QuantidadeDeExemplares > 0).ToList();
            ViewBag.Livros = livros;

            UsuariosDAO daoUsr = new UsuariosDAO();
            IList<Usuario> usuarios = daoUsr.Lista();
            ViewBag.Usuarios = usuarios;

            ReservasDAO dao = new ReservasDAO();
            ViewBag.Reserva = dao.BuscaPorId(Id);
            return View();
        }

        [HttpPost]
        public ActionResult Atualiza(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                ReservasDAO dao = new ReservasDAO();
                LivrosDAO daoL = new LivrosDAO();

                Reserva r = dao.BuscaPorId(reserva.Id);
                Livro l = daoL.BuscaPorId(r.IdLivro);
                l.QuantidadeDeExemplares++;

                daoL.Atualiza(l);

                l = daoL.BuscaPorId(reserva.IdLivro);
                l.QuantidadeDeExemplares--;

                daoL.Atualiza(l);

                dao.Atualiza(reserva);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Reserva = reserva;

                LivrosDAO dao = new LivrosDAO();
                IList<Livro> livros = dao.Lista().Where(l => l.QuantidadeDeExemplares > 0).ToList();
                ViewBag.Livros = livros;

                UsuariosDAO daoUsr = new UsuariosDAO();
                IList<Usuario> usuarios = daoUsr.Lista();
                ViewBag.Usuarios = usuarios;

                return View("Form");
            }
        }
    }
}