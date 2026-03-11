using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbEntities;

namespace MarketAnalysisWebApi.Repos.InnerHelperRepo
{
    public class InnerHelperRepo : IInnerHelperRepo
    {
        private readonly AppDbContext _appDbContext;

        public InnerHelperRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task CreateKnsEquipment(string name)
        {
            var equipment = new DbEquipment
            {
                Name = name,
                ConfigTypeId = Guid.Parse("019cdcd9-1892-7f3a-955c-3503ede15a6d")
            };
            _appDbContext.EquipmentTable.Add(equipment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task CreateNewRequestConfig(string name)
        {
            var config = new DbRequestConfigType { ConfigTypeName = name };
            _appDbContext.ConfigurationTypesTable.Add(config);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task InnerCreateCompanyType(string name)
        {
            var type = new DbCompanyType
            {
                TypeName = name
            };
            await _appDbContext.CompanyTypesTable.AddAsync(type);
            await _appDbContext.SaveChangesAsync();

        }
    }
}
