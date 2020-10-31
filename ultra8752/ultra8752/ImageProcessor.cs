using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Runtime.CompilerServices;
using System.Linq;

namespace ultra8752
{
    static class ImageProcessor
    {
        private static List<Circle> registered = new List<Circle>();

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
                double cannyThreshold = 60;
                double circleAccumulatorThreshold = 90;
                CircleF[] circles = CvInvoke.HoughCircles(gray, HoughModes.Gradient, 2.0, 30.0, cannyThreshold,
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

                #endregion




                #region compare to prev frame circles
                List<Circle> allDisplayed = new List<Circle>();
                foreach (CircleF displayed in circles)
                {
                    Color color = ColorDetector.DetectColor(img.ToBitmap(), displayed.Center);
                    if (ColorDetector.IsGray(color, 0.7f))
                    {
                        continue;
                    }

                    Circle circle = new Circle(displayed, color);
                    allDisplayed.Add(circle);

                    int index = registered.IndexOf(circle);
                    if (index != -1){
                        registered[index] = circle;
                    } else
                    {
                        registered.Add(circle);

                        if (ColorDetector.IsRed(color, 20))
                        {
                            circle.IsRed = true;
                            circle.IsInfected = true;
                        }
                    }

                    CvInvoke.Circle(circleImage, Point.Round(displayed.Center), (int)displayed.Radius,
                        new Bgr(Color.Brown).MCvScalar, 2);

                    Point point = Point.Round(displayed.Center);
                    point.Offset(-20, 20);
                    CvInvoke.PutText(circleImage, "" + index, point, FontFace.HersheyDuplex, 2,
                        new MCvScalar(255, 255, 255));
                }
                #endregion



                #region detect collisions and infected balls
                var dict = CollisionDetection.CollisionBetweenCircles(allDisplayed, 2);
                foreach(var pair in dict)
                {
                    if (pair.Key.IsInfected)
                    {
                        pair.Value.IsInfected = true;
                    } else if (pair.Value.IsInfected)
                    {
                        pair.Key.IsInfected = true;
                    }
                }
                #endregion


                Mat result = new Mat();
                CvInvoke.HConcat(new Mat[] { img, circleImage }, result);

                return result;
            }
        }

        internal static string[] getInfectedBalls()
        {
            List<string> result = new List<string>();
            for(int i = 0; i < registered.Count; i++)
            {
                if (registered[i].IsInfected)
                {
                    result.Add("" + i);
                }
            }

            return result.ToArray();
        }

        internal static int GetRedBallsCount()
        {
            return registered.Count(ball => ball.IsRed);
        }

        internal static int GetBallsCount()
        {
            return registered.Count;
        }
    }
}
