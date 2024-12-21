namespace WebApplication1.Repository
{
	public interface IRepository<T>where T : class
	{
		Task<IEnumerable<T>> GetAll();
		Task Delete(int id);
		Task<T> GetById(int id);
		Task Add(T entity);
		void Update(T updatedEntity);

		Task Save();
	}
}
