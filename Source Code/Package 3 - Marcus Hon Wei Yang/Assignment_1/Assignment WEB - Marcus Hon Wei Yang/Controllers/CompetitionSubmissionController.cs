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
    public class CompetitionSubmissionController : Controller
    {
        private CompetitionSubmissionDAL competitionsubmissionContext = new CompetitionSubmissionDAL();
        

        // GET: CompetitionSubmissionController
        public ActionResult Index()
        {
            
            List<CompetitionSubmission> competitionsubmissionsList = competitionsubmissionContext.GetAllCompetitionSubmission();
            return View(competitionsubmissionsList);
            
        }

        
        //get details (show total score) 
        // GET: CompetitionSubmissionController/Details/5
        public ActionResult Details(int? competitionId, int? competitorId)
        {
            CompetitionSubmission competitionsubmission = competitionsubmissionContext.GetDetails(competitionId.Value, competitorId.Value);
            CompetitionSubmissionViewModel competitionsubmissionVM = MapToCSVM(competitionsubmission);
            return View(competitionsubmissionVM);
            
        }

        public CompetitionSubmissionViewModel MapToCSVM(CompetitionSubmission competitionsubmission)
        {
            


            CompetitionSubmissionViewModel competitionsubmissionVM = new CompetitionSubmissionViewModel
            {
                CompetitorId = competitionsubmission.CompetitorId,
                TotalScore = competitionsubmission.TotalScore
            };
            return competitionsubmissionVM;
        }

        // GET: CompetitionSubmissionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionSubmissionController/Create
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

        //edit ranking 
        // GET: CompetitionSubmissionController/Edit/5
        public ActionResult Edit(int? competitionId, int? competitorId )
        {
            if (competitionId == null || competitorId == null )
            { 
                return RedirectToAction("Index");
            }

            CompetitionSubmission competitionsubmission = competitionsubmissionContext.GetDetails(competitionId.Value, competitorId.Value);
            if (competitionsubmission == null)
            {
                return RedirectToAction("Index");
            }

            return View(competitionsubmission);
        }

        // POST: CompetitionSubmissionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompetitionSubmission competitionsubmission)
        {
            if (ModelState.IsValid)
            {
                Debug.WriteLine("test");
                
                competitionsubmissionContext.Update(competitionsubmission);
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(competitionsubmission);
            }
        }

        // GET: CompetitionSubmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionSubmissionController/Delete/5
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
