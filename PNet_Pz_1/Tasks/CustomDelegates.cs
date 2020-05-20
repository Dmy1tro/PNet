using System;

namespace PNet_Pz_1.Tasks
{
    public delegate bool TryCalculate(double a, double b, out double result);
    public class CustomDelegates
    {
        public void Example()
        {
            Func<double, double, double> divide = Divide;
            TryCalculate tryCalculate = TryDivide;

            try
            {
                divide(123, 0);
            }
            finally
            {
                Console.WriteLine("Error !");
            }

            double result = 0;
            var success = tryCalculate(123, 0, out result);
            
            Console.WriteLine("Complete without error");
        }
        public double Divide(double a, double b)
        {
            return a / b;
        }
        public bool TryDivide(double a, double b, out double result)
        {
            if (b == 0)
            {
                result = 0;

                return false;
            }

            result = a / b;

            return true;
        }
    }
}
