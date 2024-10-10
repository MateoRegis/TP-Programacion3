using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EFRepository<T> : RepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public EFRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
