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

        public ActionResult Index(string id, int? level, int? idgame)
        {
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
                    game.GenerateField(2);
                    _data.AddGame(game, user);
                }              
                 
                ViewBag.Points = game.DictionaryPoint;
                ViewBag.IdGame = game.Id;
            }
            else
            {                
                game.GenerateField(2);               
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

        public void SaveGame(List<string> points, int idgame)
        {
            //_data.AddGame(t, user);
        }

        public void DeleteGame(List<string> points, int idfield)
        {
            //_data.AddGame(t, user);
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

        #endregion 
    }
}