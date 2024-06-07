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
            
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult(); 
                return;
            }

            // Verificar si el usuario tiene el rol requerido
            var requiredRole = "administrador"; 
            if (!context.HttpContext.User.IsInRole(requiredRole))
            {
                context.Result = new ForbidResult(); 
            }
        }
    }
}
