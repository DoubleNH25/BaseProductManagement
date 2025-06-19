using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductManagementMVC.Controllers
{
    public class BaseController : Controller
    {
        protected AccountMember? CurrentUser
        {
            get
            {
                return HttpContext.Session.GetObject<AccountMember>("CurrentUser");
            }
            set
            {
                HttpContext.Session.SetObject("CurrentUser", value);
            }
        }

        protected bool IsAuthenticated => CurrentUser != null;

        protected bool IsAdmin => CurrentUser?.MemberRole == 1;

        protected bool IsUser => CurrentUser?.MemberRole == 2;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.CurrentUser = CurrentUser;
            ViewBag.IsAuthenticated = IsAuthenticated;
            ViewBag.IsAdmin = IsAdmin;
            ViewBag.IsUser = IsUser;
        }

        protected IActionResult RedirectToLogin()
        {
            return RedirectToAction("Login", "Account");
        }

        protected IActionResult RedirectToAccessDenied()
        {
            return RedirectToAction("AccessDenied", "Account");
        }
    }

    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : System.Text.Json.JsonSerializer.Deserialize<T>(value);
        }
    }
} 