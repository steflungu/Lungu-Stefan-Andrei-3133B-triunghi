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


namespace Lungu_Stefan_Andrei_triunghi
{
    class SimpleWindow : GameWindow
    {
        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
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
                GL.Scale(1.1, 1.1, 1.1);
            }
            if (e.Key == Key.S)
            {
                GL.Scale(0.9, 0.9, 0.9);
            }
            if (e.Key == Key.D)
            {
                GL.Translate(0.1, 0, 0);
            }
            if (e.Key == Key.A)
            {
                GL.Translate(-0.1, 0, 0);
            }

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (mouse[OpenTK.Input.MouseButton.Right])
            {
                GL.Rotate(3, 0, 1, 0);
            }
            if (mouse[OpenTK.Input.MouseButton.Left])
            {
                GL.Rotate(-3, 0, 1, 0);
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.SteelBlue);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(Color.Red);
            GL.Vertex2(0.0f, 1.0f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex2(-1.0f, -1.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex2(1.0f, -1.0f);

            GL.End();


            this.SwapBuffers();
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
