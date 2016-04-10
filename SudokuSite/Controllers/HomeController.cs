using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SudokuSite.Models;
using System.Web.Security;

namespace SudokuSite.Controllers
{
    public class HomeController : Controller
    {
        BdData _data = new BdData();

        public ActionResult Index()
        {
            Random rand = new Random();
            Game game = new Game();
           

            if (User.Identity.IsAuthenticated)
            {
                User user = _data.GetUser(User.Identity.Name);

                if (_data.GetLastGame(user) != null)
                {
                    game = _data.GetLastGame(user);
                }
                else
                {
                    game.GenerateField();
                    _data.AddGame(game, user);
                }

                ViewBag.Points = game.DictionaryPoint;
                ViewBag.IdGame = game.Id;
            }
            else
            {
                game.GenerateField();
            }

            ViewBag.Points = game.DictionaryPoint;
            return View();
        }


        public ActionResult About()
        {

            return View();
        }




        #region Login
        //[AllowAnonymous]
        public ActionResult LogIn()
        {

            if (Session["LogonListError"] != null)
            {
                ViewBag.ListError = Session["LogonListError"];
                Session["LogonListError"] = null;
            }

            return View();
        }

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(string email, string password)
        {

            if (!_data.IsAuthorize(email, password))
            {
                Session["LogonListError"] = 0;
                return Redirect(Url.Action("LogIn", "Home"));
            }

            FormsAuthentication.SetAuthCookie(email, true);

            User user = _data.GetUser(email);
            Session["User"] = user;

            return Redirect(Url.Action("Index", "Home"));
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            return Redirect(Url.Action("Index", "Home"));
        }


        //[AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(string email, string name, string password)
        {
            User user = new User
            {
                CreationDate = DateTime.Now,
                Email = email.Trim(),
                Password = password.Trim()
            };

            if (name != null && name.Count() > 0)
            {
                user.Name = name.Trim();
            }
            else
            {
                user.Name = "Друг";
            }

            _data.AddUser(user);

            FormsAuthentication.SetAuthCookie(email, true);
            Session["User"] = user;

            return Redirect(Url.Action("index", "home"));
        }


        #endregion

        #region Game

        public ActionResult NewGame(int idgame)
        {
            Game game = _data.GetGame(idgame);
            game.Status = true;

            _data.UpdateGame(game);

            return Redirect(Url.Action("Index", "Home"));
        }


        /// <summary>
        /// Сохранить игру
        /// </summary>
        /// <param name="points"></param>
        /// <param name="idgame"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveGame(List<int?> points, int idgame)
        {

            Game game = _data.GetGame(idgame);
            Point p = new Point();

            List<Point> pl = new List<Point>();

            int i = 0;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    p = (Point)game.DictionaryPoint[x + "" + y];

                    if (points[i] != null)
                    {
                        if (p.Value == points[i].Value && !p.Visibled)
                        {
                            p.Value = points[i].Value;
                            p.Visibled = true;
                            p.Guessed = true;

                            pl.Add(p);
                            //_data.UpdatePoint(p);
                        }
                    }
                    i++;
                }
            }

            _data.UpdatePoint(pl);

            return Redirect(Url.Action("Index", "Home"));
        }

        /// <summary>
        /// Открыть поле
        /// </summary>
        /// <param name="idgame">ID игры</param>
        /// <returns></returns>
        public ActionResult OpenField(int idgame)
        {

            Game game = _data.GetGame(idgame);

            foreach (Point p in game.ListPoint)
            {
                if (!p.Visibled)
                {
                    p.Visibled = true;
                    p.Guessed = true;
                }
            }

            _data.UpdatePoint(game.ListPoint);

            return Redirect(Url.Action("Index", "Home"));
        }

       
        #endregion 
    }
}