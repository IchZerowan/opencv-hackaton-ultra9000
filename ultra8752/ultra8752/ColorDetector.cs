using System;
using System.Drawing;

namespace ultra8752
{
    static class ColorDetector
    {
        public static Color DetectColor(Bitmap bitmap, PointF coords)
        {
            int r = 0, g = 0, b = 0;
            int count = 16;
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
            float diff = Math.Abs(c1.GetHue() - c2.GetHue());

            return diff <= threshold;
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
