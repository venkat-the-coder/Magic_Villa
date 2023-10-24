using Magic_Villa_WebAPI.DB_Context;
using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Magic_Villa_WebAPI.Repository
{
    public class VillaRepository : Repository<VillaModelClass>, IVillaRepository
    {
        private readonly VillaDBContext _context;

        public VillaRepository(VillaDBContext dBContext): base(dBContext) {
            _context = dBContext;
        }    
        public async Task<VillaModelClass> Update(VillaModelClass villa)
        {
            villa.UpdatedDate = DateTime.Now;
            _context.Villas.Update(villa);
            await _context.SaveChangesAsync();
            return villa;
        }
    }
}

