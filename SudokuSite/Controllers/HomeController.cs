using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuSite.Game;

namespace SudokuSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Table t = new Table();
            t.GenerateField();

            ViewBag.Points = t.DictionaryPoint;

            return View();
        }


        public ActionResult About()
        {
           
            return View();
        }

        [HttpPost]
        public string Array(List<string> points, int idfield)
        {
            string fin = "";
            for (int i = 0; i < points.Count; i++)
            {
                fin += points[i] + ";  ";
            }

            return fin;
        }

    }
}