using System;
namespace Console_Crossing
{
    public class FruitTrees
    {
        private string treeType;
        private int year;
        private int month;
        private int day;
        private bool exists;

        public string GetTreeType()
        {
            return treeType;
        }
        public void SetTreeType(string temp)
        {
            treeType = temp;
        }
        public int GetYear()
        {
            return year;
        }
        public void SetYear(int temp)
        {
            year = temp;
        }
        public int GetMonth()
        {
            return month;
        }
        public void SetMonth(int temp)
        {
            month = temp;
        }
        public int GetDay()
        {
            return day;
        }
        public void SetDay(int temp)
        {
            day = temp;
        }
        public bool GetExists()
        {
            return exists;
        }
        public void SetExists(bool temp)
        {
            exists = temp;
        }
        public FruitTrees(string treeType, int year, int month, int day)
        {
            this.treeType = treeType;
            this.year = year;
            this.month = month;
            this.day = day;
        }
        public FruitTrees()
        {

        }
        public bool CheckFruitTrees()
        {
            bool verify = false;
            if (this.exists != false)
            {
                DateTime treeDate = new DateTime(year, month, day, 0, 0, 0);
                treeDate.AddDays(3);
                DateTime currentDate = DateTime.Now;
                if (treeDate >= currentDate)
                {
                    verify = true;
                }
            }
            return verify;
        }
    }
}