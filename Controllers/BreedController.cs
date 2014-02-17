using EquiMarket.DAL;
using EquiMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquiMarket.Controllers
{
    public class BreedController : Controller
    {

        private EquiContext db = new EquiContext();

        //
        // GET: /Breed/
        public ActionResult Autocomplete(string term)
        {
            
            List<Breed> data = db.Breeds.Where(b => b.TitleSK.Contains(term)).Take(50).ToList<Breed>();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}