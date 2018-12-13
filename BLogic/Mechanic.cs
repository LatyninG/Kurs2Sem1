using System.Threading;

namespace BLogic
{
    public class Mechanic
    {
        public delegate void StateOfMechanic(int state);
        public delegate void ContinueWorking();

        public event StateOfMechanic RepairStock;
        public event ContinueWorking Continue;

        //   object lockOn = new object();
        private static object lul;
        private int _maxStateofMech = 3;

        public void Pochinka(object state)
        {
            //state = 1;
            Monitor.Enter(lul);
            for (int i = 0; i < _maxStateofMech; i++)
            {
                RepairStock(i);
                Thread.Sleep(500);
            }
          /*  Monitor.Pulse(state);
            Monitor.Wait(state); */
            Monitor.Exit(lul);
            Continue();
        }
    }
}
