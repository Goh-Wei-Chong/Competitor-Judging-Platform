using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WEB___Marcus_Hon_Wei_Yang.DAL;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Controllers
{
    public class CompetitorController : Controller
    {
        private CompetitionSubmissionDAL competitorContext = new CompetitionSubmissionDAL();
        private CompetitionDAL competitionContext = new CompetitionDAL();

        // GET: CompetitorController
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: CompetitorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompetitorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CompetitorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompetitorController/Edit/5
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

        // GET: CompetitorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitorController/Delete/5
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
