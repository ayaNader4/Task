
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly Context _context;

		public Repository(Context context)
		{
			_context = context;
		}

		public async Task Add(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public async Task Delete(int id)
		{
			var entity =await GetById(id);
			_context.Set<T>().Remove(entity);
		}

		public virtual async Task<IEnumerable<T>> GetAll()=>await _context.Set<T>().ToListAsync();
		

		public virtual async Task<T> GetById(int id) => await _context.Set<T>().FindAsync(id);

		public async Task Save()
		{
			await _context.SaveChangesAsync();
		}

		public void Update(T updatedEntity)
		{
			_context.Entry<T>(updatedEntity).State = EntityState.Modified;
		}
	}
}
