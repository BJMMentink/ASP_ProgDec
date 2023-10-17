using Microsoft.AspNetCore.Mvc;

namespace BJM.ProgDec.UI.Controllers
{
    public class DeclarationController : Controller
    {
        public IActionResult Index()
        {
            return View(DeclarationManager.Load());
        }
        public IActionResult Details(int id)
        {
            return View(DeclarationManager.LoadById(id));
        }
        public IActionResult Create()
        {
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
            return View(DeclarationManager.LoadById(id));
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
            return View(DeclarationManager.LoadById(id));
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
