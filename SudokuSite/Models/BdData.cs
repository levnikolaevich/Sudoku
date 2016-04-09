using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;


namespace SudokuSite.Models
{
    public class BdData
    {

        private db_sudokuEntities _context = new db_sudokuEntities();

        #region User
        public User GetUser(string email)
        {
            try
            {
                User user = _context.User.Where(x => x.Email == email).First();

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsAuthorize(string email, string password)
        {
            try
            {
                User user = _context.User.First(x => x.Email == email);

                //Включить для шифрования
                //return user != null && SimpleHash.VerifyHash(password, "md5", user.Password);

                //Убрать, при включении шифрования
                if (user != null && password == user.Password)
                {
                    return true;
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {

            }
            return false;
        }

        public void AddUser(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();            
        }

        #endregion

        public void AddGame(Game game, User user)
        {

            game.IdUser = user.Id;
            game.Status = false;
            game.CreationDate = DateTime.Now;
            game.IdLevel = 1;

            _context.Game.Add(game);
            _context.SaveChanges();

            foreach (Point p in game.ListPoint)
            {
                p.IdGame = game.Id;

                _context.Point.Add(p);
            }

            _context.SaveChanges();
        }

        public Game GetLastGame(User user)
        {
            try
            {
                Game game = _context.Game.Where(x => x.IdUser == user.Id && x.Status == false).OrderByDescending(y => y.CreationDate).First();

                game.FillDictionary();

                return game;
            }
            catch (Exception)
            {
                return null;
            }          
        }

    }
}