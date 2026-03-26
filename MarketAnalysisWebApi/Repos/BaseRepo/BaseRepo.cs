using MarketAnalysisWebApi.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.BaseRepo
{
    public abstract class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        internal readonly AppDbContext _appDbContext;

        protected BaseRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual async Task Create(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();

        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            var list = await _appDbContext.Set<T>().ToListAsync();
            return list;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var res = await _appDbContext.Set<T>().FindAsync(id);
            return res;
        }

        public async Task Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            await _appDbContext.SaveChangesAsync();

        }

        protected async Task<string> GenerateRequestNumber(Guid userId)
        {
            var user = await _appDbContext.UsersTable.ToListAsync();
            var requests = await _appDbContext.ProjectRequestsTable.ToListAsync();
            int usersIndex = user.FindIndex(x => x.Id == userId);
            var uscomp = await _appDbContext.SupplierUsersCompaniesTable.FirstOrDefaultAsync(x => x.SupplierUserId == userId);
            if (uscomp == null)
            {
                string number = $"{requests.Count}-{usersIndex}-П";
                return number;
            }
            else
            {
                var company = await _appDbContext.CompaniesTable.FirstOrDefaultAsync(x => x.Id == uscomp.CompanyId);
                var comps = await _appDbContext.CompaniesTable.ToListAsync();
                int index = comps.FindIndex(x => x.Id == company.Id);
                string number = $"{requests.Count}-{usersIndex}-{index}";
                return number;
            }

        }
    }
}
