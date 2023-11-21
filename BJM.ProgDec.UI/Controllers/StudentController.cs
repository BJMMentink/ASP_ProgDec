using BJM.ProgDec.UI.Extentions;
using BJM.ProgDec.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BJM.ProgDec.UI.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of Advisors";
            return View(StudentManager.Load());
        }
        public IActionResult Details(int id)
        {
            var item = StudentManager.LoadById(id);
            ViewBag.Title = "Details for " + item.FullName;
            return View(StudentManager.LoadById(id));
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Create a Student";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student, bool rollback = false)
        {
            try
            {
                int result = StudentManager.Insert(student, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Create a Student";
                ViewBag.Error = ex.Message;
                throw;
            }
        }
        public IActionResult Edit(int id) 
        {
            StudentVM studentVM = new StudentVM(id);
            ViewBag.Title = "Edit " + studentVM.Student.FullName;
            HttpContext.Session.SetObject("advisorids", studentVM.AdvisorId);
            return View(studentVM);
        }
        [HttpPost]
        public IActionResult Edit(int id, StudentVM studentVM, bool rollback = false)
        {
            try
            {
                IEnumerable<int> newAdvisorIds = new List<int>();
                if (studentVM.AdvisorId != null)
                {
                    newAdvisorIds = studentVM.AdvisorId;
                }
                IEnumerable<int> oldAdvisorIds = new List<int>();
                oldAdvisorIds = GetObject();
                IEnumerable<int> deletes = oldAdvisorIds.Except(newAdvisorIds);
                IEnumerable<int> adds = newAdvisorIds.Except(oldAdvisorIds);

                deletes.ToList().ForEach(d => StudentAdvisorManager.Delete(id, d));
                adds.ToList().ForEach(a => StudentAdvisorManager.Insert(id, a));

                int result = StudentManager.Update(studentVM.Student, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Edit " + studentVM.Student.FullName;
                ViewBag.Error = ex.Message;
                return View(studentVM);
            }
        }

        private IEnumerable<int> GetObject()
        {
            if (HttpContext.Session.GetObject<IEnumerable<int>>("advisorids") != null)
                return HttpContext.Session.GetObject<IEnumerable<int>>("advisorids");
            else
                return null;
        }

        public IActionResult Delete(int id)
        {
            return View(StudentManager.LoadById(id));
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
                ViewBag.Error = ex.Message;
                return View(student);
            }
        }
    }
    
}
