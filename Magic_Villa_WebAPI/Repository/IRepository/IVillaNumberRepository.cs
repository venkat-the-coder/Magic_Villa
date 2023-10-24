using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Domain.Models.Dtos;
using System.Linq.Expressions;

namespace Magic_Villa_WebAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumberModel>
    {
        Task<VillaNumberModel> UpdateAsync(VillaNumberModel model);
    }
}
