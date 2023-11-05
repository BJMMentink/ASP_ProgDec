﻿using BJM.ProgDec.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BJM.ProgDec.UI.Controllers
{
    public class ProgramController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of all programs";
            return View(ProgramManager.Load());
        }
        public IActionResult Details(int id)
        {
            var item = ProgramManager.LoadById(id);
            ViewBag.Title = "Details for " + item.Description;
            return View(item);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Create a Program";
            if (Authenticate.IsAuthenticated(HttpContext)) 
                return View();
            else 
                return RedirectToAction("Login", "User", new {returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request)});
        }
        [HttpPost]
        public IActionResult Create(BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Insert(program, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult Edit(int id) 
        {
            var item = ProgramManager.LoadById(id);
            ViewBag.Title = "Edit " + item.Description;
            return View(ProgramManager.LoadById(id));
        }
        [HttpPost]
        public IActionResult Edit(int id, BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Update(program, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(program);
            }
        }
        public IActionResult Delete(int id)
        {
            var item = ProgramManager.LoadById(id);
            ViewBag.Title = "Delete " + item.Description;
            return View(ProgramManager.LoadById(id));
        }
        [HttpPost]
        public IActionResult Delete(int id, BL.Models.Program program, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(program);
            }
        }
    }
    
}
