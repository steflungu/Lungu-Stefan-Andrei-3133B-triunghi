using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Platform;


namespace Lungu_Stefan_Andrei_triunghi // !!! Am modificat figura geometrica din triunghi in romb dar nu am mai schimbat numele proiectului!!!
{
    class SimpleWindow : GameWindow
    {
        List<List<float>> coord = new List<List<float>>(); //implementare lista de coordonate pentru citirea din fisier a coordonatelor
        List<Color> colors = new List<Color>(); //implementare lista de culori

        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            Console.WriteLine(" W - muta sus ");
            Console.WriteLine(" S - muta jos");
            Console.WriteLine(" A - muta dreapta");
            Console.WriteLine(" D - muta stanga");
            Console.WriteLine(" C - modificare culori");
            Console.WriteLine(" Click dreapta mouse - rotatie clockwise");
            Console.WriteLine(" Click stanga mouse - rotatie counter-clockwise");

            coord=GetCoordonate();
            setColors();
        }

        //functie ce genereaza culori , din interval RGB
        void setColors()
        {
            var rnd = new Random();
            colors.Clear();
            for(int i=0; i<4; i++)
            {
                colors.Add(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));
            }
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();
            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
            if (e.Key == Key.W)
            {
                GL.Translate(0.0,0.1,0);
            }
            if (e.Key == Key.S)
            {
                GL.Translate(0.0, -0.1, 0);
            }
            if (e.Key == Key.D)
            {
                GL.Translate(0.1, 0, 0);
            }
            if (e.Key == Key.A)
            {
                GL.Translate(-0.1, 0, 0);
            }

            if(e.Key==Key.C)
            {
                setColors();
            }


        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (mouse[OpenTK.Input.MouseButton.Right])
            {
                GL.Rotate(-3.0f,0.0f,0.0f,1.0f);
            }
            if (mouse[OpenTK.Input.MouseButton.Left])
            {
                GL.Rotate(3.0f, 0.0f, 0.0f, 1.0f);
            }


        }

        protected override void OnLoad(EventArgs e)
        {
            
            GL.ClearColor(Color.DarkBlue);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            DrawFigure();
            this.SwapBuffers();
        }
        public List<List<float>> GetCoordonate()
        {
            //O lista de float-uri pentru vertex
            //Coordonatele sunt cate 3 pe un rand, delimitate prin ","

            List<List<float>> list = new List<List<float>>();
            foreach (string line in System.IO.File.ReadLines("coordonate.txt"))
            {
                list.Add(line.Split(',')?.Select(float.Parse)?.ToList());
            }

          

            return list;
        }
        private void DrawFigure()
        {
            GL.Begin(PrimitiveType.Polygon);

            GL.Color3(colors[0]);
            GL.Vertex2(coord[0][0], coord[0][1]);
            GL.Color3(colors[1]);
            GL.Vertex2(coord[1][0], coord[1][1]);
            GL.Color3(colors[2]);
            GL.Vertex2(coord[2][0], coord[2][1]);
            GL.Color3(colors[3]);
            GL.Vertex2(coord[3][0], coord[3][1]);

            GL.End();
        }

        [STAThread]
        static void Main(string[] args)
        {
            using (SimpleWindow example = new SimpleWindow())
            {

                example.Run(30.0, 0.0);
            }
        }

    }
}
