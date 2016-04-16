using System;
using System.Drawing;

namespace WaterTube.ObjectDetection
{
    public class ObjectDetectedEventArgs : EventArgs
    {
        public Point ObjectCenter { get; private set; }
        public int ObjectRadius { get; private set; }

        internal ObjectDetectedEventArgs(Point objectCenter, int objectRadius)
        {
            this.ObjectCenter = objectCenter;
            this.ObjectRadius = objectRadius;
        }
    }
}
