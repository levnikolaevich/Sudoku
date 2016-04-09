using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SudokuSite.Models
{
    public partial class Point : ICloneable
    {
        //------------------members----------------------------------------------------------------------------
        private int mHorizontalArea;
        private int mVerticalArea;
        private int mHorizontalBlock;
        private int mVerticalBlock;   
   
        //------------------public-----------------------------------------------------------------------------    
  
        public int HorizontalArea
        {
            set { mHorizontalArea = value; }
            get { return mHorizontalArea; }
        }

        public int VerticalArea
        {
            set { mVerticalArea = value; }
            get { return mVerticalArea; }
        }

        public int HorizontalBlock
        {
            set { mHorizontalBlock = value; }
            get { return mHorizontalBlock; }
        }
        
        public int VerticalBlock
        {
            set { mVerticalBlock = value; }
            get { return mVerticalBlock; }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public partial class Game
    {
        //-----------------------------------members-------------------------------------------------------------------
        private const int n = 3;
        

        private List<Point> listP = new List<Point>();
        private Dictionary<string, Point> dictP = new Dictionary<string, Point>();

        //-----------------------------------public method-------------------------------------------------------------------
        
        public Dictionary<string, Point> DictionaryPoint
        {
            get { return dictP; }
        }

        public List<Point> ListPoint
        {
            get { return listP; }
        }

        public Dictionary<string, Point> Dict
        {
            get { return dictP; }
        }

        /// <summary>
        /// Создание игрового поля
        /// </summary>
        /// <param name="level"></param>
        public void GenerateField(int level)
        {
            Random rand = new Random();

            for (int y = 0; y < 9; y++)
            {

                for (int x = 0; x < 9; x++)
                {
                    int value = (x * n + x / n + y) % (n * n) + 1;

                    Point p = new Point();
                    p.X = x;
                    p.Y = y;
                    p.Value = value;

                    p.Guessed = false;
                    p.Checked = false;
                    p.Visibled = true;

                    p.HorizontalBlock = x;
                    p.VerticalBlock = y;
                    p.HorizontalArea = x / n;
                    p.VerticalArea = y / n;

                    listP.Add(p);
                    dictP.Add(x + "" + y, p);
                }
            }                     

            for (int g = 0; g < 100; g++)
            {
                ChangeRow(rand.Next(0, 9), rand.Next(0, 9), true);
                ChangeСolumn(rand.Next(0, 9), rand.Next(0, 9), true);
                ChangeVerticalArea(rand.Next(0, 3), rand.Next(0, 3));
                ChangeHorizontalArea(rand.Next(0, 3), rand.Next(0, 3));
            }

            SetUnvisiblePoint(level);
        }

        public void FillDictionary()
        {
            listP.Clear();
            dictP.Clear();

            foreach (Point p in Point.ToList())
            {
                p.HorizontalBlock = p.X;
                p.VerticalBlock = p.Y;
                p.HorizontalArea = p.X / n;
                p.VerticalArea = p.Y / n;

                listP.Add(p);
                dictP.Add(p.X + "" + p.Y, p);
            }

        }

        public void ChangeСolumn(int y1, int y2, bool flag)
        {
            for (int x = 0; x < 9; x++)
            {
                if (flag)
                {
                    if (dictP[x + "" + y1].VerticalArea == dictP[x + "" + y2].VerticalArea)
                    {
                        Point p = (Point)dictP[x + "" + y1].Clone();
                        dictP[x + "" + y1].Value = dictP[x + "" + y2].Value;
                        dictP[x + "" + y2].Value = p.Value;
                    }
                }
                else
                {
                    Point p = (Point)dictP[x + "" + y1].Clone();
                    dictP[x + "" + y1].Value = dictP[x + "" + y2].Value;
                    dictP[x + "" + y2].Value = p.Value;
                }
            }
        }

        public void ChangeRow(int x1, int x2, bool flag)
        {

            for (int y = 0; y < 9; y++)
            {
                if (flag)
                {
                    if (dictP[x1 + "" + y].HorizontalArea == dictP[x2 + "" + y].HorizontalArea)
                    {
                        Point p = (Point)dictP[x1 + "" + y].Clone();
                        dictP[x1 + "" + y].Value = dictP[x2 + "" + y].Value;
                        dictP[x2 + "" + y].Value = p.Value;
                    }
                }
                else
                {
                    Point p = (Point)dictP[x1 + "" + y].Clone();
                    dictP[x1 + "" + y].Value = dictP[x2 + "" + y].Value;
                    dictP[x2 + "" + y].Value = p.Value;
                }
            }
        }

        public void ChangeVerticalArea(int a1, int a2)
        {
            for (int i = 0; i < 3; i++)
            {
                int r1 = a1 * 3 + i;
                int r2 = a2 * 3 + i;

                ChangeСolumn(r1, r2, false);
            }
        }

        public void ChangeHorizontalArea(int a1, int a2)
        {
            for (int i = 0; i < 3; i++)
            {
                int r1 = a1 * 3 + i;
                int r2 = a2 * 3 + i;

                ChangeRow(r1, r2, false);
            }
        }

        public void SetUnvisiblePoint(int level)
        {
            int f;
            Random rand = new Random();

            foreach (Point p in listP)
            {
                f = rand.Next(1, level + 1);

                if (f == 1)
                {
                    p.Visibled = false;
                }

            }
        }

    }
}