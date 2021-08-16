using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WEB___Marcus_Hon_Wei_Yang.DAL;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;
using System.Diagnostics;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Controllers
{
    public class CompetitionController : Controller      
    {
        private CompetitionDAL competitionContext = new CompetitionDAL();

        // GET: CompetitionController
        public ActionResult Index(int? id)
        {
           
            CompetitionViewModel competitionVM = new CompetitionViewModel();
            competitionVM.CompetitionList = competitionContext.GetAllCompetition();
            
            if (id != null)
            {
                ViewData["selectCompetitionID"] = id.Value;               
                competitionVM.CompetitionScoreList = competitionContext.GetCompetitionCompetitionScore(id.Value);
            }
            else
            {
                ViewData["selectCompetitionID"] = "";
            }
            return View(competitionVM);


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

        // GET: CompetitionController/Edit/5
        public ActionResult Edit(int id)
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
