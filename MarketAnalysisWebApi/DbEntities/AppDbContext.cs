using MarketAnalysisWebApi.DbEntities.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.DbEntities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt) { }

        public DbSet<DbUser> UsersTable { get; set; }
        public DbSet<DbBusinessOffer> OffersTable { get; set; }
        public DbSet<DbCompany> CompaniesTable { get; set; }
        public DbSet<DbCompanyType> CompanyTypesTable { get; set; }
        public DbSet<DbEquipment> EquipmentTable { get; set; }
        public DbSet <DbEquipRequest> EquipRequestTable { get; set; }
        public DbSet<DbFavoriteRequest> FavoritesTable { get; set; }
        public DbSet<DbProjectRequest> ProjectRequestsTable { get; set; }
        public DbSet<DbRequestConfigType> ConfigurationTypesTable { get; set; }
        public DbSet<DbSupplierUserCompany> SupplierUsersCompaniesTable { get; set; }
        public DbSet<DbUserRole> UsersRolesTable { get; set; }




    }
}
