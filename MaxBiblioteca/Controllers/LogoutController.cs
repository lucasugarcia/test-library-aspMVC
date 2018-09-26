using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxBiblioteca.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            return RedirectToAction("Logout");
        }

        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session.Clear();
            Session.Remove("usuarioLogado");
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index", "Login");
        }
    }
}