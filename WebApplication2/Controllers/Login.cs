using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class Login : Controller
    {
        public IActionResult IniciarSesion()
        {
            return View();
        }
    }
}
