using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.services
{
	public interface IEmployeeServices:IRepository<Employee>
	{
	}
}
