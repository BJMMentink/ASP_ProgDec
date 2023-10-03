using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BJM.Bands.UI.Models;

namespace BJM.Bands.UI.Controllers
{
    public class BandController : Controller
    {
        Band[] bands;
        public void LoadBands()
        {
            bands = new Band[]
            {
                new Band{Id = 1, Name = "Grandson", Genre = "Rock", DateFounded = new DateTime(2018, 6, 15)},
                new Band{Id = 2, Name = "STARSET", Genre = "Rock", DateFounded = new DateTime(2013, 3, 10)},
                new Band{Id = 3, Name = "Killstation", Genre = "Emo Rap", DateFounded = new DateTime(1997, 7, 22)}
            };
        }
        // GET: Band
        public ActionResult Index()
        {
            LoadBands();
            return View(bands);
        }

        // GET: Band/Details/5
        public ActionResult Details(int id)
        {
            LoadBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // GET: Band/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Band/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Band/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Band/Edit/5
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

        // GET: Band/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Band/Delete/5
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
