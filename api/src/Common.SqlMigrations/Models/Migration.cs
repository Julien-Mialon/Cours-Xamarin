using System;
using ServiceStack.DataAnnotations;

namespace Common.SqlMigrations.Models
{
	public class Migration
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Id { get; set; }
		
		[Index]
		public int Number { get; set; }
		
		[Index]
		[StringLength(200)]
		public string Module { get; set; }
		
		public DateTime AppliedDate { get; set; }
	}
}