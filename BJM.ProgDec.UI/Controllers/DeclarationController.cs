﻿using Microsoft.AspNetCore.Mvc;

namespace BJM.ProgDec.UI.Controllers
{
    public class DeclarationController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of All Programs";
            return View(DeclarationManager.Load());
        }
        //filter Declaration by Program ID
        public IActionResult Browse(int id)
        {
            var results = ProgramManager.LoadById(id);
            ViewBag.Title = "List of " + results.Description + " Declarations";
            return View(nameof(Index), DeclarationManager.Load(id));
        }
        public IActionResult Details(int id)
        {
            var item = DeclarationManager.LoadById(id);
            ViewBag.Title = "Details";
            return View(item);
        }
        public IActionResult Create()
        {
            ViewBag.title = "Create";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Insert(declaration, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IActionResult Edit(int id) 
        {
            var item = DeclarationManager.LoadById(id);
            ViewBag.Title = "Edit";
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int id, Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Update(declaration, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(declaration);
            }
        }
        public IActionResult Delete(int id)
        {
            var item = DeclarationManager.LoadById(id);
            ViewBag.Title = "Delete";
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(int id, Declaration declaration, bool rollback = false)
        {
            try
            {
                int result = DeclarationManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(declaration);
            }
        }
    }
    
}
