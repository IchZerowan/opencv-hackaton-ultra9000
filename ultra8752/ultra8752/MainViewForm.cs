using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ultra8752
{
    public partial class MainViewForm : Form
    {
        string[] args;
        public MainViewForm(string[] args_)
        {
            InitializeComponent();
            args = args_;
            Task.Run(run);
        }

        public async Task run()
        {
            if (args.Length < 1)
            {
                Console.Out.WriteLine("No input file provided. Usage: ultra8752 <file>");
                System.Environment.Exit(-1);
            }

            if (!System.IO.File.Exists(args[0]))
            {
                Console.Out.WriteLine("Invalid input file path");
                System.Environment.Exit(-1);
            }

            VideoLoader videoLoader = new VideoLoader(args[0]);
            Mat frame;
            for (; ; )
            {
                frame = videoLoader.nextFrame();
                if (frame == null)
                {
                    return;
                }

                Mat processed = ImageProcessor.ProcessImage(frame);

                pictureBox1.Image = processed.ToBitmap();
            }
        }
    }
}
