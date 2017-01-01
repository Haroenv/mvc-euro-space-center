using EuroSpaceCenter.Models;
using System.Linq;
using System.Web.Mvc;

namespace EuroSpaceCenter.Controllers {
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string cat)
        {
            if (cat != null) {
                // zoek categorien
                try {
                    ViewBag.Items = item.Get(cat).OrderBy(i => i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN);
                } catch {
                    ViewBag.Items = item.GetAll().OrderBy(i => i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN);
                }
            } else {
                ViewBag.Items = item.GetAll().OrderBy(i => i.ratings.Any() ? i.ratings.Average(r => r.rating1) : double.NaN);
            }
            ViewBag.Cat = cat;
            return View();
        }
    }
}