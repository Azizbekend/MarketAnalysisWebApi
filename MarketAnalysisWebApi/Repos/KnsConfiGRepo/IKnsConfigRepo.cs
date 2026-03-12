using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;

namespace MarketAnalysisWebApi.Repos.KnsConfiGRepo
{
    public interface IKnsConfigRepo
    {
        Task <DbKnsConfig> GetKnsConfig(Guid knsId);
        Task <ICollection<DbEquipment>> GetCurrentKnsEquipment(Guid request);
        Task<Guid> CreateKnsRequest(CreateKnsInnerRequestDTO dto);
        Task<Guid> CreateKnsConfig(CreateKnsInnerConfigDTO dto);
        Task CreateKnsEquipment(Guid requestId, List<Guid> equipments);
        Task<ICollection<DbEquipment>> GetKnsConfigEquipment(Guid typeId);
    }
}
