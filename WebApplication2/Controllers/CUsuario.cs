using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    [Authorize]
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

        public IActionResult MonitoriaFiltros(string fecha, string user, int opc)
        {
            if (opc==0)
            {
                return Json(_repoReg.RegistrosAuditoria());
            }
            else
            {
                return Json(_repoReg.MonitoriaFiltros(fecha, user, opc));
            }
        }

        public IActionResult Usuarios() {
            List<Usuario> listaUser = _repoUsuario.ListaUsuarios();
            return Json(new { users = listaUser });
        }

    }
}
