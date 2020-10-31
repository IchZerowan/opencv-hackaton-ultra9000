using Emgu.CV.Structure;
using System.Drawing;

namespace ultra8752
{
    class Circle
    {
        public Color Color { get; }
        public CircleF Geometry { get; }

        public bool IsRed { get; set; }

        public bool IsInfected { get; set; }

        public Circle(CircleF geometry, Color color)
        {
            Color = color;
            Geometry = geometry;
            IsRed = false;
            IsInfected = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Circle circle &&
                   ColorDetector.CompareColors(Color, ((Circle) obj).Color, 15);
        }
    }
}
