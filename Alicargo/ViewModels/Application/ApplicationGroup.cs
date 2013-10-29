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
            public Aggregates(int count, float weight)
            {
                Count = new CountHolder(count);
                Weight = new WeightHolder(weight);
            }

            public CountHolder Count { get; private set; }
            public WeightHolder Weight { get; private set; }
        }

        public sealed class CountHolder
        {
            public readonly int sum;

            public CountHolder(int value)
            {
                sum = value;
            }
        }

        public sealed class WeightHolder
        {
            public readonly float sum;

            public WeightHolder(float value)
            {
                sum = value;
            }
        }

        // ReSharper restore InconsistentNaming
    }
}