namespace Alicargo.ViewModels.Application
{
    public sealed class ApplicationGroup
    {
        // ReSharper disable InconsistentNaming

        public Aggregates aggregates { get; set; }
        public string field { get; set; }
        public string value { get; set; }
        public bool hasSubgroups { get; set; }
        public object[] items { get; set; }

        public sealed class Aggregates
        {
            public Aggregates(int count, float weigth)
            {
                Count = new CountHolder(count);
                Weigth = new WeigthHolder(weigth);
            }

            public CountHolder Count { get; private set; }
            public WeigthHolder Weigth { get; private set; }
        }

        public sealed class CountHolder
        {
            public readonly int sum;

            public CountHolder(int value)
            {
                sum = value;
            }
        }

        public sealed class WeigthHolder
        {
            public readonly float sum;

            public WeigthHolder(float value)
            {
                sum = value;
            }
        }

        // ReSharper restore InconsistentNaming
    }
}