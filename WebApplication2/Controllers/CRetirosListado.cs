using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    public class CRetirosListado : Controller
    {
        RRetirosListado _repoListado = new RRetirosListado();
        public IActionResult GetRetiros()
        {
            return View(_repoListado.listarTodos());
        }
    }
}
