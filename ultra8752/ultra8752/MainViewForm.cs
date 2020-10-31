using Emgu.CV;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ultra8752
{
    public partial class MainViewForm : Form
    {
        string[] args;
        bool Stopped = false;
        public MainViewForm(string[] args_)
        {
            InitializeComponent();
            args = args_;
            Task.Run(run);
            this.WindowState = FormWindowState.Maximized;
        }

        public async Task run()
        {
            if (args.Length < 1)
            {
                Console.WriteLine("No input file provided. Usage: ultra8752 <file>");
                System.Environment.Exit(-1);
            }

            if (!System.IO.File.Exists(args[0]))
            {
                Console.WriteLine("Invalid input file path");
                System.Environment.Exit(-1);
            }

            VideoLoader videoLoader = new VideoLoader(args[0]);
            int fourcc = VideoWriter.Fourcc('H', '2', '6', '4');

            Backend[] backends = CvInvoke.WriterBackends;
            int backend_idx = 0; 
            foreach (Backend be in backends)
            {
                if (be.Name.Equals("MSMF"))
                {
                    backend_idx = be.ID;
                    break;
                }
            }

            VideoWriter videoWriter = new VideoWriter("result.mp4", backend_idx, fourcc, 30, new Size(videoLoader.Width, videoLoader.Height), true);
            Mat frame;
            for (; ; )
            {
                if (!Stopped)
                {
                    frame = videoLoader.nextFrame();
                    if (frame == null)
                    {
                        break;
                    }

                    Mat processed = ImageProcessor.ProcessImage(frame);

                    videoWriter.Write(processed);

                    pictureBox1.Image = processed.ToBitmap();
                }
                else
                {
                    continue;
                }
            }

            videoWriter.Dispose();

            File.WriteAllLines("result.txt", new string[]{
                ImageProcessor.GetBallsCount() + "",
                ImageProcessor.GetRedBallsCount() + "",
                string.Join(" ", ImageProcessor.getInfectedBalls())
            });
        }

        private void pauseBtn_Click(object sender, EventArgs e)
        {
            if (Stopped)
            {
                Stopped = false;
                pauseBtn.Text = "II";
            }
            else
            {
                Stopped = true;
                pauseBtn.Text = ">";
            }
        }
    }
}
