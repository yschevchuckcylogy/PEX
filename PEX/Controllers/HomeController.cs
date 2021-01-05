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
            HttpContext.Session["cap"] = cap;
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
            if (string.IsNullOrEmpty(request.VendorId))
                return Json(new ValidateTransactionResponse { Approved = false }, JsonRequestBehavior.AllowGet);

            var vendor = _repository.GetVendorById(request.VendorId);

            if(vendor == null)
                return Json(new ValidateTransactionResponse { Approved = false, DenialReason = DenialReasons.VendorNotFound }, JsonRequestBehavior.AllowGet);

            if (!vendor.Enabled)
                return Json(new ValidateTransactionResponse { Approved = false, DenialReason = DenialReasons.VendorNotEnabled }, JsonRequestBehavior.AllowGet) ;

            var sum = _repository.GetTransactionSum(request.VendorId, request.UserId, DateTime.Now);

            if (sum > vendor.MonthlyPerUserCap)
                return Json(new ValidateTransactionResponse { Approved = false, DenialReason = DenialReasons.TransactionOverMonthlyLimit}, JsonRequestBehavior.AllowGet);

            var insertedTransaction = _repository.InsertTranzaction(request);
            if (insertedTransaction == null)
                return Json(new ValidateTransactionResponse { Approved = false }, JsonRequestBehavior.AllowGet);

            insertedTransaction.Approved = true;
            insertedTransaction.CurrentMonthUserSpend = _repository.GetTransactionSum(request.VendorId, request.UserId, DateTime.Now);

            return Json(insertedTransaction, JsonRequestBehavior.AllowGet);
        }

    }
}