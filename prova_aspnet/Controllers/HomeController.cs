using Controllers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prova_aspnet.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel usuarioModel = new UsuarioModel();
        UsuarioController usuarioController = new UsuarioController();

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(string usuario, string senha)
        {
            usuarioModel.Nome = usuario;
            usuarioModel.Senha = senha;

            bool verifica = usuarioController.VerificaUsuario(usuarioModel);

            if (verifica)
                return RedirectToAction("Index");
            else
            {
                TempData["message"] = "Usuário ou Senha inválido";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
