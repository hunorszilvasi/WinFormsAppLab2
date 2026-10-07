using System.Threading;

namespace WinFormsAppLab2
{
    public class MyThread
    {
        private Thread thread;
        private bool running;

        public delegate void DrawDelegate();

        private DrawDelegate drawMethod;

        public MyThread(DrawDelegate drawMethod)
        {
            this.drawMethod = drawMethod;
        }

        public void Start()
        {
            if (running)
                return;

            running = true;

            thread = new Thread(Run);
            thread.Start();
        }

        public void Stop()
        {
            running = false;
        }

        private void Run()
        {
            while (running)
            {
                drawMethod();

                Thread.Sleep(100);
            }
        }
    }
}