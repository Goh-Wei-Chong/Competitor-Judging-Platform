using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_1.DAL;
using Assignment_1.Models;

namespace Assignment_1.Controllers
{
    public class AreaInterestController : Controller
    {
        private AreaInterestDAL areaInterestContext = new AreaInterestDAL();

        // GET: AreaInterestController
        public ActionResult Index()
        {
            List<AreaInterest> areaInterestList = areaInterestContext.GetAllAreaInterest();
            return View(areaInterestList);
        }

        // GET: AreaInterestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AreaInterestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AreaInterestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAreaInterest(AreaInterest areaInterest)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                areaInterest.AreaInterestID = areaInterestContext.Add(areaInterest);
                //Redirect user to Staff/Index view
                return RedirectToAction("ViewAreaInterest");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(areaInterest);
            }
        }

        // GET: AreaInterestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AreaInterestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AreaInterestController/Delete/5
        public ActionResult Delete(int? id)
        {
            return View();
        }

        // POST: AreaInterestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
