using BJM.ProgDec.UI.Models;
using BJM.ProgDec.UI.ViewModels;
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
            ProgramVM programVM = new ProgramVM();
            programVM.Program = new BL.Models.Program();
            programVM.DegreeTypes = DegreeTypeManager.Load();
            ViewBag.Title = "Create a Program";
            if (Authenticate.IsAuthenticated(HttpContext)) 
                return View(programVM);
            else 
                return RedirectToAction("Login", "User", new {returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request)});
        }
        [HttpPost]
        public IActionResult Create(ProgramVM programVM)
        {
            try
            {
                int result = ProgramManager.Insert(programVM.Program);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IActionResult Edit(int id)
        {

            ProgramVM programVM = new ProgramVM();

            programVM.Program = ProgramManager.LoadById(id);
            programVM.DegreeTypes = DegreeTypeManager.Load();

            ViewBag.Title = "Edit " + programVM.Program.Description;

            if (Authenticate.IsAuthenticated(HttpContext))
                return View(programVM);
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }
        [HttpPost]
        public IActionResult Edit(int id, ProgramVM programVM, bool rollback = false)
        {
            try
            {
                int result = ProgramManager.Update(programVM.Program, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(programVM);
            }
        }

        public IActionResult Delete(int id)
        {
            var item = StudentManager.LoadById(id);
            ViewBag.Title = "Delete " + item.FullName;
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id, Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete " + student.FullName;
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }
    }
}