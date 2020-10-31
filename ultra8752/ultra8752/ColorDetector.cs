using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ultra8752
{
    static class ColorDetector
    {
        public static Color DetectColor(Bitmap bitmap, PointF coords)
        {
            int r = 0, g = 0, b = 0;
            int count = 10;
            for(int i = 0; i < count; i++)
            {
                Color color = bitmap.GetPixel((int) coords.X + i - count / 2, (int) coords.Y + i - count / 2);
                r += color.R;
                g += color.G;
                b += color.B;
            }

            r /= count;
            g /= count;
            b /= count;

            return Color.FromArgb(0, r, g, b);
        }

        public static bool CompareColors(Color c1, Color c2, int threshold)
        {
            int r = Math.Abs(c1.R - c2.R);
            int g = Math.Abs(c1.G - c2.G);
            int b = Math.Abs(c1.B - c2.B);

            return r <= threshold && g <= threshold && b <= threshold;
        }

        public static bool IsGray(Color c, float threshold)
        {
            return c.GetSaturation() <= threshold;
        }

        public static bool IsRed(Color c, float threshold)
        {
            float hue = c.GetHue();
            return hue < threshold / 2 || hue > 360 - threshold / 2;
        }
    }
}
