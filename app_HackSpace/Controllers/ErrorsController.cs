using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace app_HackSpace.Controllers
{
    [Authorize]
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult GenericError()
        {
            return View();
        }

        public ActionResult InternalError400()
        {
            return View();
        }

        public ActionResult InternalError401()
        {
            return View();
        }

        public ActionResult InternalError403()
        {
            return View();
        }

        public ActionResult InternalError404()
        {
            return View();
        }

        public ActionResult InternalError500()
        {
            return View();
        }

        public ActionResult InternalError503()
        {
            return View();
        }
    }
}