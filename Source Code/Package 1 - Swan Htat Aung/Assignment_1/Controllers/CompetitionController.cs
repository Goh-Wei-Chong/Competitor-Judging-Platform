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
    public class CompetitionController : Controller
    {
        private CompetitionDAL competitionContext = new CompetitionDAL();

        // GET: CompetitionController
        public ActionResult Index()
        {
            List<Competition> competitionList = competitionContext.GetAllCompetitions();
            return View(competitionList);
        }

        // GET: CompetitionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompetitionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompetition(Competition competition)
        {
            if (ModelState.IsValid)
            {
                //Add staff record to database
                competition.CompetitionID = competitionContext.Add(competition);
                //Redirect user to Staff/Index view
                return RedirectToAction("ViewCompetition");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(competition);
            }
        }

        // GET: CompetitionController/Edit/5
        public ActionResult Edit(int? id)
        {
      
            return View();
        }

        // POST: CompetitionController/Edit/5
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

        // GET: CompetitionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionController/Delete/5
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
