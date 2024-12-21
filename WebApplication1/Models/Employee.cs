using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
	public class Employee
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		 
		public double salary { get; set; }

		[ForeignKey("Department")]
		public int? DepartmentId { get; set; }
		public Department? Department { get; set; }
	}
}
