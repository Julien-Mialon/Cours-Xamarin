namespace Common.Api.Swaggers
{
	public class SwaggerVersionModule
	{
		public SwaggerVersionModule(string moduleName, params string[] matchExpressions)
		{
			MatchExpressions = matchExpressions;
			ModuleName = moduleName;
		}

		public string[] MatchExpressions { get; }
			
		public string ModuleName { get; }
	}
}