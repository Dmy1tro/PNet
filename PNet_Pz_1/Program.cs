using PNet_Pz_1.Tasks;
using System;

namespace PNet_Pz_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DelegateExample();

            ChainingExample();

            CustomDelegateExample();

            EventsExample();

            Console.ReadKey();
        }

        public static void DelegateExample()
        {
            var task = new Delegates();

            task.Example();
        }

        public static void ChainingExample()
        {
            var chaining = new Chaining();
            
            chaining.Example();
        }

        public static void CustomDelegateExample()
        {
            var customDelegates = new CustomDelegates();

            customDelegates.Example();
        }

        public static void EventsExample()
        {
            var worker = new Worker();

            worker.Work += (orderName) =>
            {
                Console.WriteLine("Work in proccess...");
            };

            worker.TryToMakeOrder += (string orderName, out object result) =>
            {
                if (orderName.Length % 2 == 0)
                {
                    result = null;
                    return false;
                }

                result = "Completed order";
                return true;
            };

            worker.DoJob("my order");
        }
    }
}
