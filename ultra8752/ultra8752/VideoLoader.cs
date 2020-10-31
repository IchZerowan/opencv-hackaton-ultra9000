using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Drawing;

namespace ultra8752
{
    class VideoLoader
    {
        public VideoCapture capture;

        private const bool RESIZE = false;

        public VideoLoader(String path)
        {
            capture = new VideoCapture(path);
        }

        public Mat nextFrame()
        {
            Mat ret = new Mat();
            if (!capture.Read(ret))
            {
                return null;
            }

            if (RESIZE)
            {
                CvInvoke.Resize(ret, ret, new Size(720, 480), 0, 0, Inter.Linear);
            }

            return ret;
        }
    }
}
