using System;
using System.Collections.Generic;
using System.Drawing;

namespace ultra8752
{
    class CollisionDetection
    {
        public static Dictionary<Circle, Circle> CollisionBetweenCircles(List<Circle> circles, int threshhold)
        {
            Dictionary<Circle, Circle> collidedCircles = new Dictionary<Circle, Circle>();
            for(int i = 0; i < circles.Count; i++)
            {
                for(int j = i + 1; j < circles.Count; j++)
                {
                    PointF c1 = circles[i].Geometry.Center;
                    PointF c2 = circles[j].Geometry.Center;
                    float r1 = circles[i].Geometry.Radius;
                    float r2 = circles[j].Geometry.Radius;

                    double sqDist = Math.Pow(c1.X - c2.X, 2) + Math.Pow(c1.Y - c2.Y, 2);
                    if (sqDist + threshhold <= Math.Pow(r1 + r2, 2))
                    {
                        collidedCircles.Add(circles[i], circles[j]);
                    }
                }
            }
            return collidedCircles;
        }
    }
}
