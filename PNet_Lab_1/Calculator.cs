using System;

namespace PNet_Lab_1
{
    public static class Calculator
    {
        public const double G = 9.8;

        public static double CalculateLengthOfFlight(double speed, double angle)
        {
            var length = Math.Pow(speed, 2) * Math.Sin(2 * angle) / G;

            return length;
        }
    }
}
