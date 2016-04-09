using System;
using SudokuSite.Models;

namespace SudokuSite.Game1
{
    public partial class Point : ICloneable
    {

        //------------------members----------------------------------------------------------------------------
        private int mX;
        private int mY;
        private int mValue;
        private int mHorizontalArea;
        private int mVerticalArea;
        private int mHorizontalBlock;
        private int mVerticalBlock;
        private bool mIsVisibled;
        private Table mTable;

        //------------------public-----------------------------------------------------------------------------

        public Point(int x, int y, int value, bool isVisibled, Table table)
        {
            mX = x;
            mY = y;
            mValue = value;
            mIsVisibled = isVisibled;
            mTable = table;
        }

        public int X
        {
            get { return mX; }
        }

        public int Y
        {
            get { return mY; }
        }

        public int Value
        {
            get { return mValue; }
        }

        public Table Table
        {
            get { return mTable; }
        }

        public bool Visibled
        {
            set { mIsVisibled = value; }
            get { return mIsVisibled; }
        }

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



        public void SetVisibled(bool isVisibled)
        {
            mIsVisibled = isVisibled;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
