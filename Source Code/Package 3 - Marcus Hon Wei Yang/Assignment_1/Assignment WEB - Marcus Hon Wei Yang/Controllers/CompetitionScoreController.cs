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
    public class CompetitionScoreController : Controller
    {
        private CompetitionScoreDAL competitionscoreContext = new CompetitionScoreDAL();

        // GET: CompetitionScoreController
        public ActionResult Index()
        {
            List<CompetitionScore> competitionscoreList = competitionscoreContext.GetAllCompetitionScore();
            return View(competitionscoreList);
        }

        // GET: CompetitionScoreController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

        // GET: CompetitionScoreController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionScoreController/Create
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

        // GET: CompetitionScoreController/Edit/5
        public ActionResult Edit(int? criteriaId, int? competitorId, int? competitionId)
        {

                     
            if (criteriaId == null || competitorId == null || competitionId == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            
            CompetitionScore competitionscore = competitionscoreContext.GetDetails(criteriaId.Value, competitorId.Value, competitionId.Value);
            if (competitionscore == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            
            return View(competitionscore);
        }


        // POST: CompetitionScoreController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompetitionScore competitionscore)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine("test") ;
                
                competitionscoreContext.Update(competitionscore);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(competitionscore);
            }

        }

        // GET: CompetitionScoreController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionScoreController/Delete/5
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
