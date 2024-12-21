using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.services
{
	public class EmployeeServices: Repository<Employee>  , IEmployeeServices
	{
        private readonly Context _context;

        public EmployeeServices(Context context):base(context)
        {
            this._context = context;
        }
        public override async Task<IEnumerable<Employee>> GetAll() => await _context.Set<Employee>().Include(d=>d.Department).ToListAsync();
        public override async Task<Employee> GetById(int id) => await _context.Set<Employee>().Include(d => d.Department).FirstOrDefaultAsync(e => e.Id == id);


    }
}
