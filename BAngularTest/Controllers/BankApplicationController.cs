using BAngularTest.Authorization;
using System.Web.Mvc;

namespace BAngularTest.Controllers
{
    [CustomAuthorize]
    public class BankApplicationController : Controller
    {

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Bank()
        {
            ViewBag.Title = "Bank Page";

            return View();
        }

        public ActionResult Customer()
        {
            ViewBag.Title = "Customer Page";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Title = "Register Customer";

            return View();
        }
    }
}
