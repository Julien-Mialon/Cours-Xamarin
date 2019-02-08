using System;

namespace Common.Api.Attributes
{
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public sealed class ImplementationNotesAttribute : Attribute
	{
		public string Description { get; }
		
		public ImplementationNotesAttribute(string description)
		{
			Description = description;
		}
	}
}