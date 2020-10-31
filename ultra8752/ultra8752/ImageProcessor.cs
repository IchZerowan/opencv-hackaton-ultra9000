using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;

namespace ultra8752
{
    static class ImageProcessor
    {
        public static Mat ProcessImage(Mat img)
        {
            using (UMat gray = new UMat())
            using (UMat cannyEdges = new UMat())
            using (Mat circleImage = new Mat(img.Size, DepthType.Cv8U, 3))
            {
                //Convert the image to grayscale and filter out the noise
                CvInvoke.CvtColor(img, gray, ColorConversion.Bgr2Gray);

                //Remove noise
                CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 1);

                #region circle detection
                double cannyThreshold = 80;
                double circleAccumulatorThreshold = 80;
                CircleF[] circles = CvInvoke.HoughCircles(gray, HoughModes.Gradient, 2.0, 20.0, cannyThreshold,
                    circleAccumulatorThreshold, 5);
                #endregion

                #region draw circles
                circleImage.SetTo(new MCvScalar(0));
                foreach (CircleF circle in circles)
                    CvInvoke.Circle(circleImage, Point.Round(circle.Center), (int)circle.Radius,
                        new Bgr(Color.Brown).MCvScalar, 2);

                //Drawing a light gray frame around the image
                CvInvoke.Rectangle(circleImage,
                    new Rectangle(Point.Empty, new Size(circleImage.Width - 1, circleImage.Height - 1)),
                    new MCvScalar(120, 120, 120));

                for(int i = 0; i < circles.Length; i++)
                {
                    CvInvoke.Circle(circleImage, Point.Round(circles[i].Center), (int)circles[i].Radius,
                        new Bgr(Color.Brown).MCvScalar, 2);

                    Point point = Point.Round(circles[i].Center);
                    point.Offset(-20, 20);
                    CvInvoke.PutText(circleImage, "" + i, point, FontFace.HersheyDuplex, 2,
                        new MCvScalar(255, 255, 255));
                }

                #endregion

                Mat result = new Mat();
                CvInvoke.HConcat(new Mat[] { img, circleImage }, result);
                return result;
            }
        }
    }
}
