using Magic_Villa_WebAPI.Domain.Models;
using System.Linq.Expressions;

namespace Magic_Villa_WebAPI.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task Create(VillaModelClass villa);

        Task Update(VillaModelClass villa);
        Task Remove(VillaModelClass villa);
        Task<List<VillaModelClass>> GetAll(Expression<Func<VillaModelClass,bool>> filter = null);
        Task<VillaModelClass> GetById(Expression<Func<VillaModelClass,bool>> filter = null ,bool tracked = true);
        Task Save();
    }
}
