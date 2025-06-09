using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Attributes
{
    public class RoleRequiredAttribute : ActionFilterAttribute
    {
        private readonly UserRole[] _roles;

        public RoleRequiredAttribute(params UserRole[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userRoleString = session.GetString("UserRole");

            if (string.IsNullOrEmpty(userRoleString))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (Enum.TryParse<UserRole>(userRoleString, out UserRole userRole))
            {
                if (!_roles.Contains(userRole))
                {
                    context.Result = new RedirectToActionResult("AccessDenied", "Auth", null);
                    return;
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
} 