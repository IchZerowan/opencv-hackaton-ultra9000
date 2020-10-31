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
    class CollisionDetection
    {
        public Dictionary<CircleF, CircleF> collisionBetweenCircles(CircleF[] circles, int threshhold)
        {
            Dictionary<CircleF, CircleF> collidedCircles = new Dictionary<CircleF, CircleF>();
            foreach (CircleF circle in circles)
            {
                foreach (CircleF currentCircle in circles)
                {
                    if (circle.Center != currentCircle.Center)
                    {
                        double distantion = 0;
                        double xVec = 0;
                        double yVec = 0;
                        xVec = currentCircle.Center.X - circle.Center.X;
                        yVec = currentCircle.Center.Y - circle.Center.Y;
                        distantion = Math.Sqrt(Math.Pow(xVec, 2) + Math.Pow(yVec, 2));
                        if (distantion <= Math.Abs(circle.Radius + currentCircle.Radius) + threshhold)
                        {
                            collidedCircles.Add(circle, currentCircle);
                        }                    
                    }
                    continue;
                }
            }
            return collidedCircles;
        }
    }
}
