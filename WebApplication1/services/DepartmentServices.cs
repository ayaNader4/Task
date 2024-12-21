using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.services
{
	public class DepartmentServices :Repository<Department>, IDepartmentServices
	{
		public DepartmentServices(Context context) :base(context){ }
	}
}
