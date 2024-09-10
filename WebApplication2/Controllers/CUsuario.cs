using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    public class CUsuario : Controller
    {
        RAuditoriaRegistros _repoReg = new RAuditoriaRegistros();
        RUsuario _repoUsuario = new RUsuario();
        public IActionResult AdministracionUser()
        {
            return View("AdministracionUser",_repoReg.RegistrosAuditoria());
        }

        public IActionResult AddUsuario(Usuario user)
        {
            var res = _repoUsuario.AgregarUsuario(user);
            if (res)
                return Json(new { success = true, message = "Se añadió el registro." });
            else
            {
                return Json(new { success = false, message = "Error al crear el registro." });
            }
        }

    }
}
