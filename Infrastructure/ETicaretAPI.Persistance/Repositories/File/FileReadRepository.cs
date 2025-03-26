using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistance.Contexts;

namespace ETicaretAPI.Persistance.Repositories
{
    public class FileReadRepository : ReadRepository<ETicaretAPI.Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDBContext context) : base(context)
        {
        }
    }
}
