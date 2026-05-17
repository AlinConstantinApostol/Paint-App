using System.Drawing;
using System.Drawing.Drawing2D;

namespace PaintApp.Tests;

/// <summary>
/// Helper utilities shared by drawing and history tests.
/// </summary>
internal static class TestBitmapFactory
{
    public static Bitmap CreateBlankBitmap(int width = 60, int height = 60)
    {
        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        return bitmap;
    }

    public static Bitmap CreateSolidBitmap(Color color, int width = 10, int height = 10)
    {
        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(color);
        return bitmap;
    }

    public static Pen CreatePen()
    {
        return new Pen(Color.Black, 2f)
        {
            StartCap = LineCap.Round,
            EndCap = LineCap.Round,
            LineJoin = LineJoin.Round
        };
    }

    public static bool HasInk(Bitmap bitmap)
    {
        for (var x = 0; x < bitmap.Width; x++)
        {
            for (var y = 0; y < bitmap.Height; y++)
            {
                if (bitmap.GetPixel(x, y).ToArgb() != Color.White.ToArgb())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool RegionHasInk(Bitmap bitmap, Point center, int radius = 2)
    {
        for (var x = Math.Max(0, center.X - radius); x <= Math.Min(bitmap.Width - 1, center.X + radius); x++)
        {
            for (var y = Math.Max(0, center.Y - radius); y <= Math.Min(bitmap.Height - 1, center.Y + radius); y++)
            {
                if (bitmap.GetPixel(x, y).ToArgb() != Color.White.ToArgb())
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static Color ReadCenterPixel(Bitmap bitmap)
    {
        return bitmap.GetPixel(bitmap.Width / 2, bitmap.Height / 2);
    }
}
