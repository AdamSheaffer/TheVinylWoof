using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheVinylWoof.Data;

namespace TheVinylWoof.Controllers
{
    public class HomeController : Controller
    {
        private IVinylWoofRepository _repo;

        public HomeController(IVinylWoofRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var users = _repo.GetUsers().ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult MyProfile()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}