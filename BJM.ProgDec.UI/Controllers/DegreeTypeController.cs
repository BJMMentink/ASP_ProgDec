﻿using Microsoft.AspNetCore.Mvc;

namespace BJM.ProgDec.UI.Controllers
{
    public class DegreeTypeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of Degree Types";
            return View(DegreeTypeManager.Load());
        }
        public IActionResult Details(int id)
        {
            var item = DegreeTypeManager.LoadById(id);
            ViewBag.Title = "Details for " + item.Description;
            return View(item);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Create a Degree Type";
            return View();
        }
        [HttpPost]
        public IActionResult Create(DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int result = DegreeTypeManager.Insert(degreeType, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create a Degree Type";
                ViewBag.Error = ex.Message;
                throw;
            }
        }
        public IActionResult Edit(int id) 
        {
            var item = DegreeTypeManager.LoadById(id);
            ViewBag.Title = "Edit " + item.Description;
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int id, DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int result = DegreeTypeManager.Update(degreeType, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit " + degreeType.Description;
                ViewBag.Error = ex.Message;
                return View(degreeType);
            }
        }
        public IActionResult Delete(int id)
        {
            var item = DegreeTypeManager.LoadById(id);
            ViewBag.Title = "Delete " + item.Description;
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(int id, DegreeType degreeType, bool rollback = false)
        {
            try
            {
                int result = DegreeTypeManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete " + degreeType.Description;
                ViewBag.Error = ex.Message;
                return View(degreeType);
            }
        }
    }
    
}
