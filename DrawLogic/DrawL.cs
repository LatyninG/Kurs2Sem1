using System.Drawing;

namespace DrawLogic
{
    public class DrawL
    {
        public int X0 { get; set; }
        public int Y0 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Promejutok { get; set; }

        private int _dx, _dy;
        private int nx, ny;

  //      private int _deltacarstX = 200;
        private int _carstStartX = 10, _carstStartY = 350;
        private int _carWidth = 150, _carHeight = 150;
        private int _carBlocksWidht = 35, _carBlocksHeight = 35;
        private int _kabinaWidth = 75, _kabinaHeight = 75;

        private Point[] _mechstCoords = new Point[] { new Point(875, 200), new Point(800, 240), new Point(950, 240) };
 //       private int _mechstHeight, _mechstWidth;

        private int _counter = 0, _counterMax = 9;
        private int _mechCounterMax = 3;
        private int _carStateLast = -1;

        Graphics g;

        public DrawL(int x0, int y0, int width, int height, int promejutok, Graphics graphics)
        {
            this.X0 = x0;
            this.Y0 = y0;
            this.Width = width;
            this.Height = height;
            this.Promejutok = promejutok;
            _dx = (Width - 4 * Promejutok) / 3;
            _dy = (Height - 4 * Promejutok) / 3;
            nx = X0 + Promejutok;
            ny = Y0 + Promejutok;
            this.g = graphics;
        }

        public void DrawBuildings()
        {
            g.DrawRectangle(Pens.Black, X0, Y0, Width, Height);
        }

        public void FillBuilding(int counter)
        {
            if (counter == 0)
                _counter = -1;
            else
                _counter = counter;
            for (int i = 0; i < _counter; i++)
            {
                if (i == 3 || i == 6)
                {
                    ny += _dy + Promejutok;
                    nx = X0 + Promejutok;
                }
                g.DrawRectangle(Pens.Black, nx, ny, _dx, _dy);
                nx += _dx + Promejutok;
            }
        }

        public void DrawMech(int mechState)
        {
            Point point = _mechstCoords[mechState];
            g.DrawEllipse(Pens.Black, point.X, point.Y, 40, 40);
        }

        public void DrawCar(int carState, int counter)
        {
            if (carState == 0)
                _carStateLast = -1;
            else
                _carStateLast = carState;
            _counter = counter;
            int perehod = carState * 170, qx = _carstStartX + perehod;
            g.DrawRectangle(Pens.Black, qx, _carstStartY, _carWidth, _carHeight);
            g.DrawRectangle(Pens.Black, qx + _carWidth, _carstStartY + 60, _kabinaWidth, _kabinaHeight);
            g.DrawEllipse(Pens.Black, qx, _carstStartY + _carHeight, 50, 50);
            g.DrawEllipse(Pens.Black, qx + 90, _carstStartY + _carHeight, 50, 50);
            int qqx = qx + Promejutok, qy = _carstStartY + Promejutok;
            for (int i = 0; i < _counterMax - _counter; i++)
            {
                if (i == 3 || i == 6)
                {
                    qy += _carBlocksHeight + Promejutok;
                    qqx = qx + Promejutok;
                }
                g.DrawRectangle(Pens.Black, qqx, qy, _carBlocksWidht, _carBlocksHeight);
                qqx += _carBlocksWidht + Promejutok;
            }
        }

        public void DrawAll(int counter = -1, bool mech = false, int carState = -1, int mechState = -1)
        {
            g.Clear(Color.White);
            DrawBuildings();
            if (counter >= 0)
            {
                FillBuilding(counter);
                if (carState != -1)
                    DrawCar(carState, counter);
                else if (_carStateLast >= 0)
                    DrawCar(_carStateLast, counter);
            }
            else if(_counter >= 0)
            {
                FillBuilding(_counter);
                if (carState != -1)
                    DrawCar(carState, _counter);
                else if (_carStateLast >= 0)
                    DrawCar(_carStateLast, _counter);
            }
            if (mech)
                DrawMech(mechState);
        }
    }
}
