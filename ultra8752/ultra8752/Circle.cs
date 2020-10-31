using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ultra8752
{
    class Circle
    {
        public Color Color { get; }
        public CircleF Geometry { get; }

        public Circle(CircleF geometry, Color color)
        {
            Color = color;
            Geometry = geometry;
        }

        public override bool Equals(object obj)
        {
            return obj is Circle circle &&
                   ColorDetector.CompareColors(Color, ((Circle) obj).Color, 15);
        }
    }
}
