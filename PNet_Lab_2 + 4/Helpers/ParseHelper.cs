
using System;

namespace PNet_Lab_2.Helpers
{
    public static class ParseHelper
    {
        public static int GetInt(this object obj)
        {
            if(int.TryParse(obj.ToString(), out var number))
            {
                return number;
            }

            return -1;
        }

        public static decimal GetDecimal(this object obj)
        {
            if (decimal.TryParse(obj.ToString(), out var decNumber))
            {
                return decNumber;
            }

            return -1;
        }

        public static DateTime GetDate(this object obj)
        {
            if (DateTime.TryParse(obj.ToString(), out var date))
            {
                return date;
            }

            return DateTime.MinValue;
        }

        public static string GetString(this object obj)
        {
            return obj.ToString().Trim();
        }
    }
}
