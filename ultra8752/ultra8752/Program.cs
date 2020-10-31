using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace ultra8752
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                if(frame == null)
                {
                    return;
                }

                Mat processed = ImageProcessor.ProcessImage(frame);

                // TODO: display processed
            }
        }
    }
}
