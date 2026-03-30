using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.BaseRepo;

namespace MarketAnalysisWebApi.Repos.InnerHelperRepo
{
    public interface IInnerHelperRepo
    {
        Task CreateNewRequestConfig(string name);
        Task CreateKnsEquipment(string name);
        Task InnerCreateCompanyType(string name);
        Task InnerCreateBusinessAcc(Guid userId);
        Task<Guid> RequestStatusChangeAsync(RequestStasusChangeDTo dto);
        Task<Guid> RequestArchive(Guid requestId);
        Task<ICollection<DbRegion>> GetAllRegions();
        Task<Guid> CreateRegion(string name);
        Task<Guid> AttachRegion(Guid requestId, Guid regionId);
        Task<Guid> CreatePumpType(string name);       
    }
}
