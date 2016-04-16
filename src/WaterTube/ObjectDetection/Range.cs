namespace WaterTube.ObjectDetection
{
    public class Range<T>
    {
        private T min;
        private T max;

        public Range() : this(default(T), default(T))
        {
        }

        public Range(T min, T max)
        {
            this.min = min;
            this.max = max;
        }

        public T Min
        {
            get { lock (this) return this.min; }
            set { lock (this) this.min = value; }
        }

        public T Max
        {
            get { lock (this) return this.max; }
            set { lock (this) this.max = value; }
        }
    }
}
