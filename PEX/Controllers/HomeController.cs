using PEX.HomeViewModel;
using PEX.Models;
using PEX.Repositories;
using System;
using System.Collections.Generic;
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditVendors([System.Web.Http.FromBody] List<Vendor> vendors)
        {
            foreach (var vendor in vendors)
            {
                _repository.UpdateVendor(vendor);
            }

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ValidateTransaction(ValidateTransactionRequest request)
        {
            if (string.IsNullOrEmpty(request.vendorid))
                return Json(new ValidateTransactionResponse { approved = false }, JsonRequestBehavior.AllowGet);

            var vendor = _repository.GetVendorById(request.vendorid);

            if(!vendor.Enabled)
                return Json(new ValidateTransactionResponse { approved = false }, JsonRequestBehavior.AllowGet);

            var sum = _repository.GetTransactionSum(request.vendorid, request.userid, DateTime.Now);

            if (sum > vendor.MonthlyPerUserCap)
                return Json(new ValidateTransactionResponse { approved = false }, JsonRequestBehavior.AllowGet);

            var insertedTransaction = _repository.InsertTranzaction(request);
            if (insertedTransaction == null)
                return Json(new ValidateTransactionResponse { approved = false }, JsonRequestBehavior.AllowGet);

            insertedTransaction.approved = true;
            insertedTransaction.currentMonthUserSpend = sum;

            return Json(insertedTransaction, JsonRequestBehavior.AllowGet);
        }

    }
}