using System;
using System.Drawing;
using System.Windows.Forms;
using BLogic;
using DrawLogic;
using System.Threading;
using System.Collections.Generic;

namespace Task_4_27
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(panel1.Width, panel1.Height);
            g = Graphics.FromImage(bmp);
        }

        Graphics g;
        DrawL drawLogic;
        Stock stock;
        Mechanic mechanic;
  //      Stack<Thread> threads = new Stack<Thread>();
  /* Thread LoadThrd;
   Thread UnloadThrd;
   Thread MechanicThrd; */
        Bitmap bmp;
     //   Thread Last;

        public void Helper(int x0, int y0, int width, int height, int promejutok, Graphics graphics)
        {
            drawLogic = new DrawL(x0, y0, width, height, promejutok, g);
            stock = new Stock();
            mechanic = new Mechanic();
            stock.ChangeLoad += EventDrawLoad;
            stock.ChangeUnload += EventDrawUnload;
            stock.NeedRepair += EventMechanicCall;
            stock.EndL += AddUnL;
            stock.EndU += AddL;
            mechanic.RepairStock += Mechanics;
            mechanic.Continue += EventMechanicEnd;
          /*  LoadThrd = new Thread(new ThreadStart(stock.Load));
            UnloadThrd = new Thread(new ThreadStart(stock.Unload));
            MechanicThrd = new Thread(new ThreadStart(mechanic.Pochinka)); */
        }

        private void AddUnL()
        {
            ThreadPool.QueueUserWorkItem(stock.Unload);
        }

        private void AddL()
        {
            ThreadPool.QueueUserWorkItem(stock.Load);
        }

        private void EventMechanicEnd()
        {
            ThreadPool.QueueUserWorkItem(stock.Load);
        }

        private void EventMechanicCall(bool rep)
        {
            ThreadPool.QueueUserWorkItem(mechanic.Pochinka);
          /*  if (threads.Peek() == LoadThrd)
                threads.Push(UnloadThrd);
            else
                threads.Push(LoadThrd);
            threads.Push(MechanicThrd); */
        }

        private void EventDrawLoad(int counter)
        {
            drawLogic.DrawAll(counter);
            Invalidate();
        }

        private void EventDrawUnload(int counter, int carstate)
        {
            drawLogic.DrawAll(counter,carState:carstate);
            Invalidate();
        }

        private void Mechanics(int mechstate)
        {
            drawLogic.DrawAll(mechState: mechstate);
            Invalidate();
        }

        private void BTNStart_Click(object sender, EventArgs e)
        {
            Helper(800, 300, 200, 200, 10, g);
            drawLogic.DrawAll();
            ThreadPool.QueueUserWorkItem(stock.Load);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackgroundImage = bmp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           /* bmp = new Bitmap(panel1.Width, panel1.Height);
            g = Graphics.FromImage(bmp); */
        }
    }
}
