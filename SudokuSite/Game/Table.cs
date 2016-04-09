using System;
using System.Collections.Generic;
using SudokuSite.Models;

namespace SudokuSite.Game
{
    public class Table
    {

        List<Point> listP = new List<Point>();
        Dictionary<string, Point> dictP = new Dictionary<string, Point>();

        int n = 3;

        public Table()
        {
            for (int y = 0; y < 9; y++)
            {

                for (int x = 0; x < 9; x++)
                {
                    int value = (x * n + x / n + y) % (n * n) + 1;

                    Point p = new Point();
                    p.HorizontalBlock = x;
                    p.VerticalBlock = y;
                    p.HorizontalArea = x / 3;
                    p.VerticalArea = y / 3;

                    listP.Add(p);
                    dictP.Add(x + "" + y, p);
                }
            }
        }

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

        public void GenerateField()
        {
            Random rand = new Random();

            for (int g = 0; g< 1000; g++) {                    
                ChangeRow(rand.Next(0,9), rand.Next(0, 9), true);
                ChangeСolumn(rand.Next(0, 9), rand.Next(0, 9), true);
                ChangeVerticalArea(rand.Next(0, 3), rand.Next(0, 3));
                ChangeHorizontalArea(rand.Next(0, 3), rand.Next(0, 3));
            }

            SetUnvisiblePoint(2);
        }

        public void ChangeСolumn(int y1, int y2, bool flag)
        {
            for (int x = 0; x < 9; x++)
            {
                if (flag)
                {
                    if (dictP[x + "" + y1].VerticalArea == dictP[x + "" + y2].VerticalArea)
                    {
                        Point p = dictP[x + "" + y1];
                        dictP[x + "" + y1] = dictP[x + "" + y2];
                        dictP[x + "" + y2] = p;
                    }
                }
                else
                {
                    Point p = dictP[x + "" + y1];
                    dictP[x + "" + y1] = dictP[x + "" + y2];
                    dictP[x + "" + y2] = p;
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
                        Point p = dictP[x1 + "" + y];
                        dictP[x1 + "" + y] = dictP[x2 + "" + y];
                        dictP[x2 + "" + y] = p;
                    }
                }
                else
                {
                    Point p = dictP[x1 + "" + y];
                    dictP[x1 + "" + y] = dictP[x2 + "" + y];
                    dictP[x2 + "" + y] = p;
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
                f = rand.Next(1, level+1);

                if (f == 1)
                {
                    p.Visibled = false;
                }

            }
        }

    }

}
