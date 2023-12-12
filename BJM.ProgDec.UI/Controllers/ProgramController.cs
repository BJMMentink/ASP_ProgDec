using BJM.ProgDec.UI.Models;
using BJM.ProgDec.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BJM.ProgDec.UI.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IWebHostEnvironment _host;

        public ProgramController(IWebHostEnvironment host)
        {
            _host = host;
        }

        #region "Pre-WebAPI"
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
            
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                ProgramVM programVM = new ProgramVM();

                programVM.Program = ProgramManager.LoadById(id);
                programVM.DegreeTypes = DegreeTypeManager.Load();

                ViewBag.Title = "Edit " + programVM.Program.Description;

                return View(programVM);
            }
            else
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
        }
        [HttpPost]
        public IActionResult Edit(int id, ProgramVM programVM, bool rollback = false)
        {
            try
            {
                ProcessImage(programVM);
                int result = ProgramManager.Update(programVM.Program, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(programVM);
            }
        }

        private void ProcessImage(ProgramVM programVM)
        {
            if (programVM.File != null)
            {
                programVM.Program.ImagePath = programVM.File.FileName;
                string path = _host.WebRootPath + "\\images\\";
                using (var stream = System.IO.File.Create(path + programVM.File.FileName))
                {
                    programVM.File.CopyTo(stream);
                    ViewBag.Message = "File Upliaded Successfully...";
                }
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

        #endregion
        #region "Web-API"

        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7112/api/");
            return client;
        }

        public IActionResult Get()
        {
            ViewBag.Title = "List of all programs";
            HttpClient client = InitializeClient();
            // Call API
            HttpResponseMessage response = client.GetAsync("Program").Result;

            // parse the result
            string result = response.Content.ReadAsStringAsync().Result;

            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<BL.Models.Program> programs = items.ToObject<List<BL.Models.Program>>();
            return View(nameof(Index), programs);
        }

        public IActionResult GetOne(int id)
        {
            ViewBag.Title = "Program Details";
            HttpClient client = InitializeClient();

            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;
            // parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            BL.Models.Program program= item.ToObject<BL.Models.Program>();

            return View(nameof(Details), program);
        }
        public IActionResult Insert()
        {
            ViewBag.Title = "Create a Program";
            HttpClient client = InitializeClient();

            // Call API
            HttpResponseMessage response = client.GetAsync("DegreeType").Result;

            // parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic items = JsonConvert.DeserializeObject(result);
            List<DegreeType> degreeTypes = items.ToObject<List<DegreeType>>();
            

            ProgramVM programVM = new ProgramVM();
            programVM.DegreeTypes = degreeTypes;
            programVM.Program = new BL.Models.Program();

            return View(nameof(Create), programVM);
        }
        [HttpPost]
        public IActionResult Insert(ProgramVM programVM)
        {
            try
            {
                HttpClient client = InitializeClient();
                ProcessImage(programVM);

                string serializedObject = JsonConvert.SerializeObject(programVM.Program);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                HttpResponseMessage response = client.PostAsync("Program", content).Result;
                return RedirectToAction(nameof(Get));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Create), programVM);

                
            }
        }
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Update";
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            BL.Models.Program program = item.ToObject<BL.Models.Program>();

            // Call the API
            response = client.GetAsync("DegreeType").Result;

            // Parse the result
            result = response.Content.ReadAsStringAsync().Result;
            dynamic items = (JArray)JsonConvert.DeserializeObject(result);
            List<DegreeType> degreeTypes = items.ToObject<List<DegreeType>>();

            ProgramVM programVM = new ProgramVM();
            programVM.DegreeTypes = degreeTypes;
            programVM.Program = program;

            return View(nameof(Edit), programVM);

        }
        [HttpPost]
        public IActionResult Insert(int id, ProgramVM programVM)
        {
            try
            {
                HttpClient client = InitializeClient();
                ProcessImage(programVM);

                string serializedObject = JsonConvert.SerializeObject(programVM.Program);
                var content = new StringContent(serializedObject);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // Call the API
                HttpResponseMessage response = client.PutAsync("Program/" + id, content).Result;
                return RedirectToAction(nameof(Get));

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Edit), programVM);


            }
        }
        public IActionResult Remove(int id)
        {
            HttpClient client = InitializeClient();

            // Call the API
            HttpResponseMessage response = client.GetAsync("Program/" + id).Result;

            // Parse the result
            string result = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject(result);
            BL.Models.Program program = item.ToObject<BL.Models.Program>();

            return View(nameof(Delete), program);
        }

        [HttpPost]
        public IActionResult Remove(int id, BL.Models.Program program)
        {
            try
            {
                HttpClient client = InitializeClient();
                HttpResponseMessage response = client.DeleteAsync("Program/" + id).Result;
                return RedirectToAction(nameof(Get));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nameof(Delete), program);
            }
        }

        #endregion
    }
    

}