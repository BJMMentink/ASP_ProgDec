using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BJM.Bands.UI.Models;
using BJM.Bands.UI.Extentions;
using NuGet.Versioning;
using System.Xml.Linq;

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
        private void GetBands()
        {
            if(HttpContext.Session.GetObject<Band[]>("bands") != null)
            {
                bands = HttpContext.Session.GetObject<Band[]>("bands");
            }
            else
            {
                LoadBands();
            }
        }
        private void SetBands()
        {
            HttpContext.Session.SetObject("bands", bands);
        }

        // GET: Band
        public ActionResult Index()
        {
            GetBands();
            return View(bands);
        }

        // GET: Band/Details/5
        public ActionResult Details(int id)
        {
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // GET: Band/Create
        public ActionResult Create()
        {
            Band band = new Band();
            return View(band);
        }

        // POST: Band/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Band band)
        {
            try
            {
                GetBands();
                // "resize" the array
                Array.Resize(ref bands, bands.Length + 1);
                // Calculate the New Id Value 
                band.Id = bands.Length;
                bands[bands.Length - 1] = band;
                SetBands();
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
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // POST: Band/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Band band)
        {
            try
            {
                GetBands();
                bands[id - 1] = band;
                SetBands();
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
            GetBands();
            Band band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        // POST: Band/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                GetBands();
                var newBands = bands.Where(b => b.Id != id);
                bands = newBands.ToArray();
                SetBands();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
