using Magic_Villa_WebAPI.DB_Context;
using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Magic_Villa_WebAPI.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly VillaDBContext _context;

        public VillaRepository(VillaDBContext dBContext) {
            _context = dBContext;
        }    
        public async Task Create(VillaModelClass villa)
        {
           await _context.AddAsync(villa);
           await Save();
        }

        public async Task<List<VillaModelClass>> GetAll(Expression<Func<VillaModelClass,bool>> filter = null)
        {
            IQueryable<VillaModelClass> query = _context.Villas;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();  
        }

        public async Task<VillaModelClass> GetById(Expression<Func<VillaModelClass,bool>> filter = null, bool tracked = true)
        {
            IQueryable<VillaModelClass> query = _context.Villas;

            if(!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync(); //if no result in filter then return default value
        }

        public async Task Remove(VillaModelClass villa)
        {
            _context.Remove(villa);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(VillaModelClass villa)
        {
            _context.Update(villa);
            await Save();
        }
    }
}

