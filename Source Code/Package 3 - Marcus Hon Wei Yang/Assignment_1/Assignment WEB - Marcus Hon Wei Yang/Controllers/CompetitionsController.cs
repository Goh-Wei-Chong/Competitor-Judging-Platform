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
    public class CompetitionsController : Controller
    {
        private CompetitionsDAL competitionsContext = new CompetitionsDAL();

        // GET: CompetitionsControllercs
        public ActionResult Index(int? id)
        {
            CompetitionsViewModel competitionsVM = new CompetitionsViewModel();
            competitionsVM.CompetitionsList = competitionsContext.GetAllCompetitions();

            
            if (id != null)
            {
                ViewData["selectCompetitionsID"] = id.Value;
                
                competitionsVM.CompetitionSubmissionList = competitionsContext.GetCompetitionsCompetitionSubmission(id.Value);
            }

            else
            {
                ViewData["selectCompetitionsID"] = "";
            }
            return View(competitionsVM);
        }


        // GET: CompetitionsControllercs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompetitionsControllercs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionsControllercs/Create
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

        // GET: CompetitionsControllercs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompetitionsControllercs/Edit/5
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

        // GET: CompetitionsControllercs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionsControllercs/Delete/5
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
