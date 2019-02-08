namespace Common.Core.Parameters
{
	public class PaginationParameter
	{
		public int? Page { get; set; }
		
		public int? Count { get; set; }

		public bool IsValid()
		{
			return (Page == null || Page >= 0) &&
			       (Count == null || Count > 0);
		}

		public void UseDefaultIfNeeded(int defaultCount = 25)
		{
			if (Page == null)
			{
				Page = 0;
			}

			if (Count == null)
			{
				Count = defaultCount;
			}
		}
	}
}