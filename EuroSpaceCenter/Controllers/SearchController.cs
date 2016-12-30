using EuroSpaceCenter.Models;
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
                    ViewBag.Items = item.Get(cat);
                } catch {
                    ViewBag.Items = item.GetAll();
                }
            } else {
                ViewBag.Items = item.GetAll();
            }
            return View();
        }
    }
}