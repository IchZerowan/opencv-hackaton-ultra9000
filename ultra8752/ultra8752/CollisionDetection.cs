using System;
using System.Collections.Generic;

namespace ultra8752
{
    class CollisionDetection
    {
        public static Dictionary<Circle, Circle> CollisionBetweenCircles(List<Circle> circles, int threshhold)
        {
            Dictionary<Circle, Circle> collidedCircles = new Dictionary<Circle, Circle>();
            foreach (Circle circle in circles)
            {
                foreach (Circle currentCircle in circles)
                {
                    if (circle.Geometry.Center != currentCircle.Geometry.Center)
                    {
                        double distantion = 0;
                        double xVec = 0;
                        double yVec = 0;
                        xVec = currentCircle.Geometry.Center.X - circle.Geometry.Center.X;
                        yVec = currentCircle.Geometry.Center.Y - circle.Geometry.Center.Y;
                        distantion = Math.Sqrt(Math.Pow(xVec, 2) + Math.Pow(yVec, 2));
                        if (distantion <= Math.Abs(circle.Geometry.Radius + currentCircle.Geometry.Radius) + threshhold)
                        {
                            if ((!collidedCircles.ContainsKey(circle) && !collidedCircles.ContainsValue(currentCircle)) || (!collidedCircles.ContainsKey(currentCircle) && !collidedCircles.ContainsValue(circle)))
                            {
                                collidedCircles.Add(circle, currentCircle);
                            }
                            
                        }                    
                    }
                    continue;
                }
            }
            return collidedCircles;
        }
    }
}
