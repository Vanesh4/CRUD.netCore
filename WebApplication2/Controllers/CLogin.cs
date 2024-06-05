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
            //Usuario usuario = new Usuario("Kelnherth Daniel Hernandez", "HERNANDEZK", "creditos03@corpentunida.org.co", "Kk123456");
            //_rusuario.AgregarUsuario(usuario);
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null) //evitar errores de un usuario no autenticado
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
                            //if (dr["UserName"] != null && u.userName != null)
                            //{
                            //    List<Claim> c = new List<Claim>()
                            //    {
                            //        new Claim(ClaimTypes.NameIdentifier, u.userName)
                            //    };
                            //    ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                            //    AuthenticationProperties p = new();

                            //    var role = dr["rol"].ToString();
                            //    c.Add(new Claim(ClaimTypes.Role, role));

                            //    p.AllowRefresh = true; //actualizar la sesion
                            //    p.IsPersistent = u.MantenerActivo;


                            //    if (!u.MantenerActivo)
                            //        p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10);
                            //    else
                            //        p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

                            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                            //    return RedirectToAction("Index", "Home");
                            //}

                            if (dr.HasRows)
                            {
                                // Usuario autenticado correctamente
                                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, u.userName) // Agregar el nombre de usuario como reclamación
                                };
                                // Agregar más reclamaciones según sea necesario

                                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                                // Crear propiedades de autenticación
                                var authProperties = new AuthenticationProperties
                                {
                                    AllowRefresh = true,
                                    IsPersistent = u.MantenerActivo
                                };

                                if (!u.MantenerActivo)
                                {
                                    authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10);
                                }
                                else
                                {
                                    authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
                                }

                                // Iniciar sesión del usuario
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);

                                // Redirigir al usuario a la página de inicio
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
