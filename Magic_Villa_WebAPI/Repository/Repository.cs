using Magic_Villa_WebAPI.DB_Context;
using Magic_Villa_WebAPI.Domain.Models;
using Magic_Villa_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa_WebAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VillaDBContext _context;
        internal DbSet<T> dbSet;
        public Repository(VillaDBContext dBContext)
        {
            _context = dBContext;
            this.dbSet = _context.Set<T>();
        }
        public async Task Create(T villa)
        {
            await dbSet.AddAsync(villa);
            await Save();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync(); //if no result in filter then return default value
        }

        public async Task Remove(T villa)
        {
            dbSet.Remove(villa);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
