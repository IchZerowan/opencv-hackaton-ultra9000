using Emgu.CV;
using Emgu.CV.UI;

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
