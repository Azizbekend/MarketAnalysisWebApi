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
        
    }
}
