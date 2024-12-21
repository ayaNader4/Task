using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
	public class Department
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public double budget { get; set; }

		public List<Employee>? Employees { get; set; }
	}
}
