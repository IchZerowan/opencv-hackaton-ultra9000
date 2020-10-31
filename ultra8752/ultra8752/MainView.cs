using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System.Windows.Forms;

namespace ultra8752
{
    class MainView
    {
        ImageViewer viewer;

        public MainView()
        {
            viewer = new ImageViewer();
            viewer.ShowDialog();
        }

        public void DisplayImage(Mat image)
        {
            viewer.Image = image;
        }
    }
}
