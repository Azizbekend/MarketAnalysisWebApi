using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS;
using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using MarketAnalysisWebApi.DTOs.RequestDTOs;

namespace MarketAnalysisWebApi.Repos.KnsConfiGRepo
{
    public interface IKnsConfigRepo
    {
        Task <DbKnsConfiguration> GetKnsConfig(Guid knsId);
        Task <ICollection<DbEquipment>> GetCurrentKnsEquipment(Guid request);
        Task<Guid> CreateKnsRequest(CreateInnerRequestDTO dto);
        Task<Guid> CreateKnsConfig(CreateKnsInnerConfigDTO dto);
        Task CreateKnsEquipment(Guid requestId, List<Guid> equipments);
        Task<ICollection<DbEquipment>> GetKnsConfigEquipment(Guid typeId);
    }
}
