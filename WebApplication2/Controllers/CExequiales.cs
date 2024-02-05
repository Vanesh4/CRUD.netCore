using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    public class CExequiales : Controller
    {
        RExequiales _repoExequiales = new RExequiales();
        public IActionResult Exequiales()
        {
            return View();
        }
        public IActionResult ExequialesData(string cedula)
        {
            return View("Exequiales", _repoExequiales.TercerosMaestra(cedula));
        }
    }
}
