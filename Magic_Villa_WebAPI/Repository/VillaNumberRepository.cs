using Magic_Villa_WebAPI.DB_Context;
using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Domain.Models.Dtos;
using Magic_Villa_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa_WebAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumberModel>, IVillaNumberRepository
    {
        private readonly VillaDBContext _context;

        public VillaNumberRepository(VillaDBContext context) : base(context) 
        {
            _context = context;
        }

        public async  Task<VillaNumberModel> UpdateAsync(VillaNumberModel model)    
        {
            _context.VillasNumber.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
