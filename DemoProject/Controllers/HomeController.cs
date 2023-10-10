using DemoProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        dataAccess data = new dataAccess();
        public ActionResult Index()
        {
            return View();
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
        public ActionResult ProductList(string SearchItem, int? i)
        {

            IEnumerable<MyAdvertiseModel> products = data.GetAllProductList();
            return View(products);
        }
        public ActionResult Details(int? advertiseId)
        {
            if (advertiseId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repo = new dataAccess();
            MyAdvertiseModel advertisement = repo.GetAdvertiseById(advertiseId);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            return View(advertisement);
        }
        [HttpGet]
        
        public ActionResult Edit(int id)
        {
            var repo = new dataAccess();
            // Retrieve the advertisement you want to edit by its ID
            MyAdvertiseModel advertisement = repo.GetAdvertiseById(id);

            if (advertisement == null)
            {
                return HttpNotFound();
            }

            return View(advertisement);
        }
        [HttpPost]

        public ActionResult Edit(MyAdvertiseModel advertisement)
        {
            if (ModelState.IsValid)
            {
                var repo = new dataAccess();
                bool updated = repo.UpdateAdvertisement(advertisement);

                if (updated)
                {
                    // Redirect to a success page or return a success message
                    return RedirectToAction("Success");
                }
                else
                {
                    // Handle update failure
                    ModelState.AddModelError("", "Failed to update the advertisement.");
                }
            }

            // If ModelState is not valid or update fails, return to the edit page with errors
            return View(advertisement);
        }
    }

    }
