using System.Drawing;

internal static class PointFExtensions
{
    public static Point ToPoint(this PointF p)
    {
        return new Point((int)p.X, (int)p.Y);
    }
}
