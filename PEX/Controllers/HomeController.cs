using PEX.HomeViewModel;
using PEX.Repositories;
using System.Web.Mvc;

namespace PEX.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;

        public HomeController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            var vendors = _repository.GetVendors();
            var model = new HomePageViewModel();
            model.Vendors = vendors;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}