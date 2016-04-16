using Emgu.CV;
using Emgu.CV.Util;

internal static class VectorOfVectorOfPointExtensions
{
    public static int? GetIndexOfContourWithLargestArea(this VectorOfVectorOfPoint contours)
    {
        int? index = null;
        double largestArea = 0;

        for (var i = 0; i < contours.Size; i++)
        {
            var a = CvInvoke.ContourArea(contours[i]);
            if (a > largestArea)
            {
                largestArea = a;
                index = i;
            }
        }

        return index;
    }
}
