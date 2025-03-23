using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDBContext _context;

        public ReadRepository(ETicaretAPIDBContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
            => Table;

        public async Task<T> GetByIdAsync(string id)
            => await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));

        public async Task<T> GetSingleAsync(System.Linq.Expressions.Expression<Func<T, bool>> method)
            => await Table.FirstOrDefaultAsync(method);

        public IQueryable<T> GetWhere(System.Linq.Expressions.Expression<Func<T, bool>> method)
            => Table.Where(method);
    }
}
