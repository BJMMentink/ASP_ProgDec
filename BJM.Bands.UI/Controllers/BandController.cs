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
                new Band{Id = 1, Image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.deezer.com%2Fus%2Fartist%2F324922&psig=AOvVaw2mCznjJLK5XAwV-oWj6oie&ust=1697049087265000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCKiert6O7IEDFQAAAAAdAAAAABAK", Name = "Grandson", Genre = "Rock", DateFounded = new DateTime(2018, 6, 15)},
                new Band{Id = 2, Image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FTransmissions_%2528Starset_album%2529&psig=AOvVaw0rpT9QjeHTK3ElPrQ9v05F&ust=1697049092707000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCIiskOOO7IEDFQAAAAAdAAAAABAE", Name = "STARSET", Genre = "Rock", DateFounded = new DateTime(2013, 3, 10)},
                new Band{Id = 3, Image="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.discogs.com%2Fmaster%2F2203873-Killstation-The-Two-Of-Us-Are-Dying&psig=AOvVaw1awhnbIRU0_HSLCK5qSB8A&ust=1697049734052000&source=images&cd=vfe&opi=89978449&ved=0CA4QjRxqFwoTCNjg2YaR7IEDFQAAAAAdAAAAABAD", Name = "Killstation", Genre = "Emo Rap", DateFounded = new DateTime(1997, 7, 22)}
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
            return View("IndexCard",bands);
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
