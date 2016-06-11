using BootstrapTable.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapTable.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() { return View(); }
        public ActionResult Basic() { return View(); }
        public ActionResult Paged() { return View(); }
        public ActionResult Menus() { return View(); }
        public ActionResult Links() { return View(); }
        public ActionResult Sorting() { return View(); }
        public ActionResult TableStyles() { return View(); }
        public ActionResult ColumnStyles() { return View(); }
        public ActionResult Toolbar() { return View(); }
        public ActionResult Search() { return View(); }
        public ActionResult Extensions() { return View(); }
        public ActionResult Checkboxes() { return View(); }

        private List<Person> PeopleSource()
        {
            return new List<Person>
            {
                new Person { Id = 1, FirstName = "Odysseus", LastName = "Kirkland", Email = "fermentum@Proinvelnisl.net", BirthDate = new DateTime(1982,9,5), Location = "Eritrea", },
                new Person { Id = 2, FirstName = "Jocelyn", LastName = "Mccall", Email = "Nullam.lobortis@Fuscefermentum.ca", BirthDate = new DateTime(1982,9,5), Location = "Bolivia", },
                new Person { Id = 3, FirstName = "Lael", LastName = "Trujillo", Email = "enim.Suspendisse.aliquet@nec.com", BirthDate = new DateTime(1982,9,5), Location = "Sri Lanka", },
                new Person { Id = 4, FirstName = "Chelsea", LastName = "Mcgee", Email = "magna.et@dolornonummyac.co.uk", BirthDate = new DateTime(1982,9,5), Location = "Hungary", },
                new Person { Id = 5, FirstName = "Connor", LastName = "Pope", Email = "In.tincidunt@eu.com", BirthDate = new DateTime(1982,9,5), Location = "Albania", },
                new Person { Id = 6, FirstName = "Dustin", LastName = "Arnold", Email = "ante.Nunc@Pellentesquetincidunttempus.com", BirthDate = new DateTime(1982,9,5), Location = "Lithuania", },
                new Person { Id = 7, FirstName = "Tatum", LastName = "Dale", Email = "turpis.egestas.Aliquam@atauctor.edu", BirthDate = new DateTime(1982,9,5), Location = "South Africa", },
                new Person { Id = 8, FirstName = "Priscilla", LastName = "Roach", Email = "at.fringilla@risus.com", BirthDate = new DateTime(1982,9,5), Location = "Lebanon", },
                new Person { Id = 9, FirstName = "Cade", LastName = "Smith", Email = "auctor.velit.eget@egetvolutpat.edu", BirthDate = new DateTime(1982,9,5), Location = "New Zealand", },
                new Person { Id = 10, FirstName = "James", LastName = "Frank", Email = "purus.Nullam@iderat.co.uk", BirthDate = new DateTime(1982,9,5), Location = "Norfolk Island", },
            };
        }

        public JsonResult GetPeopleData()
        {
            System.Threading.Thread.Sleep(1000);
            return Json(PeopleSource(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeoplePaged(int offset, int limit, string search, string sort, string order)
        {
            var people = PeopleSource();
            var model = new
            {
                total = people.Count(),
                rows = people.Skip((offset / limit) * limit).Take(limit),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPeopleSearch(int offset, int limit, string search, string sort, string order)
        {
            var people = PeopleSource().AsQueryable()
                .WhereIf(!string.IsNullOrEmpty(search), o =>
                    o.Email.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                    o.FirstName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                    o.LastName.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                    o.Location.Contains(search, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(sort ?? "LastName", order)
                .ToList();

            var model = new
            {
                total = people.Count(),
                rows = people.Skip((offset / limit) * limit).Take(limit),
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MenuAction(int id)
        {
            if (Request.IsAjaxRequest())
                return JavaScript("alert('MenuAction id = " + id + "')");
            else
                return View("MenuAction", id);
        }

        public ActionResult TableAction(int[] ids)
        {
            if (ids != null)
                return Json(new { result = string.Join(", ", ids) });
            else
                return Json(new { result = "Nothing selected!" });
        }
    }
}