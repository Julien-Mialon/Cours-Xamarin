namespace Common.Api
{
	public static class EnvironmentHelper
	{
		public enum EnvironmentSlot
		{
			Local,
			Dev,
			Test,
			Beta,
			Prod,
		}

		public static EnvironmentSlot Slot { get; private set; }

		public static void Set(EnvironmentSlot slot)
		{
			Slot = slot;
		}

		public static bool IsAvailableClient => Slot == EnvironmentSlot.Beta || Slot == EnvironmentSlot.Prod;

		public static bool IsLocal => Slot == EnvironmentSlot.Local;

		public static bool IsSwaggerAvailable => true; //!IsAvailableClient;

		public static bool IsHttpsRequired =>
#if DEBUG
			false;
#else
			!IsLocal;
#endif
	}
}