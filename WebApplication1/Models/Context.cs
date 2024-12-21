using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
	public class Context: IdentityDbContext<IdentityUser>
	{
		public Context(DbContextOptions options) : base(options) { }

		DbSet<Department> Departments { get; set; }
		DbSet<Employee> Employees { get; set; }

	}
}
