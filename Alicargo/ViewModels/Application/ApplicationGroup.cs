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
				Count = new Holder<int>(count);
				Weight = new Holder<float>(weight);
            }

			public Holder<int> Count { get; private set; }
			public Holder<float> Weight { get; private set; }
        }

		public sealed class Holder<T>
		{
			public readonly T sum;

			public Holder(T value)
			{
				sum = value;
			}
		}

        // ReSharper restore InconsistentNaming
    }
}