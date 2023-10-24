using Magic_Villa_WebAPI.Domain.Models;
using System.Linq.Expressions;

namespace Magic_Villa_WebAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Create(T villa);
        Task Remove(T villa);
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T> GetById(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task Save();
    }
}
