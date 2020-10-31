using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Drawing;

namespace ultra8752
{
    class VideoLoader
    {
        public VideoCapture capture;

        private const bool RESIZE = true;

        public VideoLoader(String path)
        {
            capture = new VideoCapture(path);
        }

        public Mat nextFrame()
        {
            Mat ret = new Mat();
            if (!capture.Read(ret) || ret.IsEmpty)
            {
                return null;
            }

            if (RESIZE)
            {
                CvInvoke.Resize(ret, ret, new Size(480, 853), 0, 0, Inter.Linear);
            }

            

            return ret;
        }

        public int Width { get { return 480; } }
        public int Height { get { return 853; } }
    }
}
