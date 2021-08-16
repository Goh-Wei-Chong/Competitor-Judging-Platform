using Assignment_WEB___Marcus_Hon_Wei_Yang.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Assignment_WEB___Marcus_Hon_Wei_Yang.Models;
using System.Diagnostics;
using System.IO;

namespace Assignment_WEB___Marcus_Hon_Wei_Yang.Controllers
{
    public class JudgeController : Controller
    {
        private JudgeDAL judgeContext = new JudgeDAL();
        private AreaInterestDAL areainterestContext = new AreaInterestDAL();

  
        // GET: StaffController
        public ActionResult Index()
        {
                    
            List<Judge> judgeList = judgeContext.GetAllJudge();
            return View(judgeList);
            
        }

        // GET: StaffController/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        

        // GET: StaffController/Create
        public ActionResult Create()
        {
            
            ViewData["AreaInterest"] = GetAreaInterest();
            return View(); 
        }
        


        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Judge judge)
        {
            

            if (ModelState.IsValid)
            {
                
                judge.JudgeId = judgeContext.Add(judge);
                
                return RedirectToAction("Index");
            }
            else
            {
                
                return View(judge);
            }

        }
        

       
        //select dropdownlist for area interest 
        private List<AreaInterest> GetAreaInterest()
        {
           
            List<AreaInterest> areainterestList = areainterestContext.GetAllAreaInterest();
            
            areainterestList.Insert(0, new AreaInterest
            {
                AreaInterestId = 0,
                AreaInterestName = "--Select--"
            });
            return areainterestList;

            
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StaffController/Edit/5
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

        // GET: StaffController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        // POST: StaffController/Delete/5
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
