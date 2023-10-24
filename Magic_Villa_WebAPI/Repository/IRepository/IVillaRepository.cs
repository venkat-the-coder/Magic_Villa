using Magic_Villa_WebAPI.Domain.Models;
using System.Linq.Expressions;

namespace Magic_Villa_WebAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<VillaModelClass>
    {
        Task<VillaModelClass> Update(VillaModelClass villa);
    }
}
