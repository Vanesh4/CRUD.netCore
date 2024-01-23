using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication2.Models;
using WebApplication2.Repo;
using System.Data.SqlClient;
using System.Numerics;

namespace WebApplication2.Controllers
{
    public class CLogin : Controller
    {
        private conexion _contexto = new conexion();
        private RUsuario _rusuario = new RUsuario();
        public IActionResult Login()
        {
            //Usuario usuario = new Usuario("sistemas Corpen", "sistemas", "correo@example.com", "sistemas123");

            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null) //evitar errores de un usuaro no autenticado
            {
                if (c.Identity.IsAuthenticated) //se valida en el POST
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //crear la sesion de usuario
        [HttpPost]
        public async Task<IActionResult> Login(Usuario u)
        {
            try
            {
                using (SqlConnection con = new(_contexto.getCadenaConAPP()))
                {
                    using (SqlCommand cmd = new("SELECT * FROM Usuarios WHERE UserName=@UserName and Clave=@Clave;", con))
                    {
                        string hashedPassword = _rusuario.HashPassword(u.clave);
                        cmd.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar).Value = u.userName;
                        cmd.Parameters.Add("@Clave", System.Data.SqlDbType.VarChar).Value = hashedPassword;
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["UserName"] != null && u.userName != null)
                            {
                                List<Claim> c = new List<Claim>()
                                {
                                    new Claim(ClaimTypes.NameIdentifier, u.userName)
                                };
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();

                                p.AllowRefresh = true; //actualizar la sesion
                                p.IsPersistent = u.MantenerActivo;


                                if (!u.MantenerActivo)
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                else
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Credenciales incorrectas o cuenta no registrada.";
                            }
                        }
                        con.Close();
                    }
                    return View();
                }
            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
