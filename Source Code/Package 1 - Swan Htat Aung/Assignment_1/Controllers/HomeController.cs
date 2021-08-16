using Assignment_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Assignment_1.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private CompetitionDAL competitionContext = new CompetitionDAL();

        private AreaInterestDAL areaInterestContext = new AreaInterestDAL();

        private CompetitionSubmissionDAL competitionSubmissionContext = new CompetitionSubmissionDAL();

        private JudgeDAL judgeContext = new JudgeDAL();

        private CompetitionJudgeDAL competitionJudgeContext = new CompetitionJudgeDAL();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["txtLoginID"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();
            if (loginID == "test@gmail.com" && password == "pass1234")
            {
                
                return RedirectToAction("AdministratorMain");
            }
            else
            {
                // Store an error message in TempData for display at the index view
                TempData["Message"] = "Invalid Login Credentials!";
                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }
        }

        private List<SelectListItem> GetAreaInterestList()
        {
            List<SelectListItem> areaInterest = new List<SelectListItem>();
            foreach (AreaInterest item in areaInterestContext.GetAllAreaInterest())
            {
                areaInterest.Add(new SelectListItem
                {
                    Value = item.AreaInterestID.ToString(),
                    Text = item.Name
                });
            }

            return areaInterest;
        }

        private bool IsCompetitionEditable(Competition competition)
        {
            List<CompetitionSubmission> competitionSubmissionList = competitionSubmissionContext.GetAllCompetitionSubmission();
            foreach (CompetitionSubmission item in competitionSubmissionList)
            {
                if (item.CompetitionID == competition.CompetitionID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCompetitionOver(int competitionID)
        {
            foreach (Competition competition in competitionContext.GetAllCompetitions())
            {
                if (competition.CompetitionID == competitionID)
                {
                    if (competition.EndDate < DateTime.Now)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsJudgeFree(int? judgeID)
        {
            foreach (CompetitionJudge competitionJudge in competitionJudgeContext.GetAllCompetitionJudge())
            {
                if (competitionJudge.JudgeID == judgeID)
                {
                    if (IsCompetitionOver(competitionJudge.CompetitionID) == false)
                    {
                        return false;
                    } 
                }
            }

            return true;
        }

        private bool IsAreaInterestDeletable(AreaInterest areaInterest)
        {
            foreach (Competition item in competitionContext.GetAllCompetitions())
            {
                if (item.AreaInterestID == areaInterest.AreaInterestID)
                {
                    return false;
                }
            }

            foreach (Judge item in judgeContext.GetAllJudge())
            {
                if (item.AreaInterestID == areaInterest.AreaInterestID)
                {
                    return false;
                }
            }

            return true;
        }

        private List<SelectListItem> GetInterestJudges(int competitionID)
        {
            int areaInterestID = new int();
            foreach (Competition competition in competitionContext.GetAllCompetitions())
            {
                if (competitionID == competition.CompetitionID)
                {
                    areaInterestID = competition.AreaInterestID;
                }
            }
            List<SelectListItem> judges = new List<SelectListItem>();
            foreach (Judge item in judgeContext.GetAllJudge())
            {
                if (item.AreaInterestID == areaInterestID)
                {
                    judges.Add(new SelectListItem
                    {
                        Value = item.JudgeID.ToString(),
                        Text = item.JudgeName
                    });
                }
            }

            return judges;
        }

        public ActionResult CreateCompetition()
        {
            ViewData["AreaInterestList"] = GetAreaInterestList();
            return View();
        }

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
                ViewData["AreaInterestList"] = GetAreaInterestList();
                return View(competition);
            }
        }

        public ActionResult EditCompetition(int? id)
        {
            Competition competition = competitionContext.GetDetails(id.Value);
            if (IsCompetitionEditable(competition))
            {
                TempData["Message"] = "Selected competition cannot be edited as there are existing competitors.";
                return RedirectToAction("ViewCompetition");
            }
            ViewData["AreaInterestList"] = GetAreaInterestList();
            return View(competition);
 
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompetition(Competition competition)
        {
            //Get branch list for drop-down list
            //in case of the need to return to Edit.cshtml view

            if (ModelState.IsValid)
            {
                //Update staff record to database
                competitionContext.Update(competition);
                return RedirectToAction("ViewCompetition");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                ViewData["AreaInterestList"] = GetAreaInterestList();
                return View(competition);
            }

        }

        public ActionResult CreateAreaInterest()
        {
            return View();
        }

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

        // GET: AreaInterestController/Delete/5
        public ActionResult DeleteAreaInterest(int? id)
        {
            AreaInterest areaInterest = areaInterestContext.GetDetails(id.Value);
            if (IsAreaInterestDeletable(areaInterest))
            {
                return View(areaInterest);
            }
            else
            {
                TempData["Message"] = "Selected area of interest cannot be deleted as there are existing competitions or judges using the area of interest.";
                return RedirectToAction("ViewAreaInterest");
            }
        }

        // POST: AreaInterestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAreaInterest(AreaInterest areaInterest)
        {
            areaInterestContext.Delete(areaInterest.AreaInterestID);
            return RedirectToAction("ViewAreaInterest");
        }

        public ActionResult AdministratorMain()
        {
            return View();
        }

        public ActionResult ViewCompetition()
        {
            List<Competition> competitionList = competitionContext.GetAllCompetitions();

            return View(competitionList);
        }

        public ActionResult ViewAreaInterest()
        {
            List<AreaInterest> areaInterestList = areaInterestContext.GetAllAreaInterest();
            return View(areaInterestList);
        }

        public ActionResult AssignJudge(int id)
        {
            if (IsCompetitionOver(id))
            {
                TempData["Message"] = "Judges cannot be assigned as competition is over.";
                return RedirectToAction("ViewCompetition");
            }
            CompetitionJudge competitionJudge = competitionJudgeContext.CreateTemp(id);
            ViewData["JudgesList"] = GetInterestJudges(id);
            return View(competitionJudge);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AssignJudge(CompetitionJudge competitionJudge)
        {
            if (ModelState.IsValid)
            {
                if (IsJudgeFree(competitionJudge.JudgeID))
                {
                    //Add staff record to database
                    competitionJudge.CompetitionID = competitionJudgeContext.Add(competitionJudge);
                    //Redirect user to Staff/Index view
                    return RedirectToAction("ViewCompetition");
                }
                TempData["Message"] = "Judge cannot be assigned as he/she is already judging a competition.";
                return RedirectToAction("AssignJudge");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(competitionJudge);
            }
        }

        public ActionResult ViewAssignedJudge(int id)
        {
            List<int?> judgeIDList = new List<int?>();
            foreach (CompetitionJudge competitionJudge in competitionJudgeContext.GetAllCompetitionJudge())
            {
                if (id == competitionJudge.CompetitionID)
                {
                    int? selectedID = competitionJudge.JudgeID;
                    judgeIDList.Add(selectedID);
                }
            }


            List<Judge> judges = new List<Judge>();
            foreach (int judgeID in judgeIDList)
            {
                foreach (Judge j in judgeContext.GetAllJudge())
                {
                    if (j.JudgeID == judgeID)
                    {
                        judges.Add(j);
                    }
                }
            }

            return View(judges);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
