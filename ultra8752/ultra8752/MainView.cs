using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace ultra8752
{
    class MainView
    {
        public string window_name { get; set; }
        public string display_text { get; set; }
        public Mat display_image { get; set; }

        public MainView()
        {
            String win1 = window_name; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
        }
        public void show_image()
        {
            Mat img = display_image;
            if (display_text != null && String.IsNullOrWhiteSpace(display_text))
                CvInvoke.PutText(
                   img,
                   display_text,
                   new System.Drawing.Point(10, 80),
                   FontFace.HersheyComplex,
                   1.0,
                   new Bgr(0, 255, 0).MCvScalar);


            CvInvoke.Imshow(window_name, img); //Show the image
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window_name); //Destroy the window if key is pressed
        }

        public void show_image(Mat image)
        {
            Mat img = display_image;
            if (display_text != null && String.IsNullOrWhiteSpace(display_text))
                CvInvoke.PutText(
                   image,
                   display_text,
                   new System.Drawing.Point(10, 80),
                   FontFace.HersheyComplex,
                   1.0,
                   new Bgr(0, 255, 0).MCvScalar);


            CvInvoke.Imshow(window_name, image); //Show the image
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window_name); //Destroy the window if key is pressed
        }
    }
}
