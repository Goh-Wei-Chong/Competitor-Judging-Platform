using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;
using Assignment_WEB___Marcus_Hon_Wei_Yang.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Controllers
{
    public class CriteriaController : Controller
    {
        private CriteriaDAL criteriaContext = new CriteriaDAL();
        private CompetitionDAL competitionContext = new CompetitionDAL();

        // GET: CriteriaController
        public ActionResult Index()
        {
            List<Criteria> criteriaList = criteriaContext.GetAllCriteria();
            return View(criteriaList);

        }

        // GET: CriteriaController/Details/5
        public ActionResult Details(int id)
        {
            Criteria criteria = criteriaContext.GetDetails(id);
            CriteriaViewModel criteriaVM = MapToCriteriaVM(criteria);
            return View(criteriaVM);
        }


        public CriteriaViewModel MapToCriteriaVM(Criteria criteria)
        {
            string competitionName = "";
            if (criteria.CompetitionId != null)
            {
                List<Competition> competitionList = competitionContext.GetAllCompetition();
                foreach (Competition competition in competitionList)
                {
                    if (competition.CompetitionId == criteria.CompetitionId)
                    {
                        competitionName = competition.CompetitionName;
                        break;
                    }
                }
            }


            CriteriaViewModel criteriaVM = new CriteriaViewModel
            {
                CriteriaId = criteria.CriteriaId,
                CompetitionName = criteria.CompetitionName,
                Weightage = criteria.Weightage,

            };
            return criteriaVM;
        }
        

        // GET: CriteriaController/Create
        public ActionResult Create()
        {
            ViewData["CompetitionList"] = GetCompetition();
            return View();
       
        }



        // POST: CriteriaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Criteria criteria)
        {
            if (ModelState.IsValid)
            {
                
                criteria.CriteriaId = criteriaContext.Add(criteria);
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(criteria);
            }

        }

        private List<Competition> GetCompetition()
        {
            
            List<Competition> competitionList = competitionContext.GetAllCompetition();
           
            competitionList.Insert(0, new Competition
            {
                CompetitionId = 0,
                CompetitionName = "--Select--"
            });
            return competitionList;
        }

        // GET: CriteriaController/Edit/5
        public ActionResult Edit(int? id)
        {
            
            Criteria criteria = criteriaContext.GetDetails(id.Value);
            if (criteria == null)
            {
                return RedirectToAction("Index");
            }
            return View(criteria);
        }

        // POST: CriteriaController/Edit/5
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

        // GET: CriteriaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CriteriaController/Delete/5
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
