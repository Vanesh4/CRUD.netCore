using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Repo
{
    public class RolesdeUsuario : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Verificar si el usuario está autenticado
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult(); // No permitir acceso si el usuario no está autenticado
                return;
            }

            // Verificar si el usuario tiene el rol requerido
            var requiredRole = "administrador"; 
            if (!context.HttpContext.User.IsInRole(requiredRole))
            {
                context.Result = new ForbidResult(); // No permitir acceso si el usuario no tiene el rol requerido
            }
        }
    }
}
