namespace Infrastructure.Data
{
    public class EFRepository<T> : RepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public EFRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
