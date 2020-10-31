using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ultra8752
{
    class VideoLoader
    {
        public VideoCapture capture;

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
            return ret;
        }
    }
}
