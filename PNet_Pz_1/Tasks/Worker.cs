using System;

namespace PNet_Pz_1.Tasks
{
    public delegate bool TryToMakeOrder(string order, out object result);

    public class Worker
    {
        public event Action<string> Work;

        public event TryToMakeOrder TryToMakeOrder;

        public object DoJob(string order)
        {
            Work?.Invoke(order);

            object result = null;

            TryToMakeOrder?.Invoke(order, out result);

            return result;
        }
    }
}
