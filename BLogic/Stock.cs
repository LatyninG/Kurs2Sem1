using System.Threading;
using System;

namespace BLogic
{
    delegate void DrawingLogic();

    public class Stock : ILoader
    {
        public delegate void CounterChanger(int count);
        public delegate void MechanicCall(bool repair);
        public delegate void CounterChangerUnload(int count, int carstate);
        public delegate void EndLoad();
        public delegate void EndUnload();

        public event CounterChanger ChangeLoad;
        public event CounterChangerUnload ChangeUnload;
        public event MechanicCall NeedRepair;
        public event EndLoad EndL;
        public event EndUnload EndU;

        //  public bool Checked { get; set; }
        private int _counter = 0;
        private object lul = 1;

     //   object lockOn = new object();

        private int _counterMax = 9;
        private int _counterOfStateCome = 3;
        Random rnd = new Random();

        public void Unload(object state)
        {
          //  state = 1;
            Monitor.Enter(lul);
            int q;
            for(q = 0;  q <= _counterOfStateCome; q++)
            {
                ChangeUnload(_counter,q);
                Thread.Sleep(500);
            }
            while (_counter != 0)
            {
                _counter--;
                ChangeUnload(_counter,q);
                Thread.Sleep(500);
            }
            for (q = _counterOfStateCome; q >= 0; q--)
            {
                ChangeUnload(_counter,q);
                Thread.Sleep(500);
            }
          /*  Monitor.Pulse(state);
            Monitor.Wait(state); */
            Monitor.Exit(lul);
            EndU();
        }

        public void Load(object state)
        {
    //        state = 1;
            Monitor.Enter(lul);
            int rand;
            while (_counter <= _counterMax)
            {
                rand = rnd.Next(0, 10);
                if (rand == 1)
                {
                    //      NeedRepair(true);
                    /*   Monitor.Wait(state);
                       Monitor.Pulse(state); */
                  //  NeedRepair(true);
                    Monitor.Exit(lul);
                    NeedRepair(true);
                }
                _counter++;
                ChangeLoad(_counter);
                Thread.Sleep(500);
            }
            /*   Monitor.Pulse(state);
               Monitor.Wait(state); */
        //    EndL();
            Monitor.Exit(lul);
            EndL();
        }
    }
}
