using PEX.HomeViewModel;
using PEX.Models;
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

        [HttpPost]
        public ActionResult EditVendor([System.Web.Http.FromBody] Vendor vendor)
        {
            _repository.UpdateVendor(vendor);
            return RedirectToAction("Home");
        }

        [HttpGet]
        public ActionResult SetAllVendorsCap(float cap)
        {
            var vensors = _repository.GetVendors();
            foreach (var vendor in vensors)
            {
                vendor.MonthlyPerUserCapDollars = cap;
                _repository.UpdateVendor(vendor);
            }
            return RedirectToAction("Home");
        }

    }
}