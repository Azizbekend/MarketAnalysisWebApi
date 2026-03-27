using MarketAnalysisWebApi.DbEntities.FileStorages;
using MarketAnalysisWebApi.DTOs.FileStorageDTOS;

namespace MarketAnalysisWebApi.Repos.FileStorageRepo
{
    public interface IFileStorageRepo
    {
        Task<Guid> SaveBusinesOfferFileAsync(OfferFileCreateDTO dto, CancellationToken token = default);
        Task<DbBusinessOfferFileModel> GetDbBusinessOfferFileModel(Guid fileId, CancellationToken token = default);
        Task<Guid> SaveEquipmentCertificateAsync(EquipmentCertificateCreateDTO dto, CancellationToken token = default);
        Task<DbEquipmentCertificateFileModel> GetEquipmentCertificateAsync(Guid certId, CancellationToken token = default);
        Task<Guid> SavePlanFileAsync(PlanFileCreateDTO dto, CancellationToken token = default);
        Task<DbPlanFile> GetPlanFileAsync(Guid planFileId, CancellationToken token = default);
        Task<Guid> SaveEquipmentPassportFileAsync(EquipmentPassportFileCreateDTO dto, CancellationToken token = default);
        Task<DbEquipmentPassportFile> GetEquipmentPassportFileAsync(Guid passportFileId, CancellationToken token = default);
        Task<Guid> SaveRequestFileAsync(RequestSchemeFileDTO dto, CancellationToken token = default);
        Task<DbRequestFileModel> GetRequestFileAsync(Guid requestSchemeFileId, CancellationToken token = default);
    }
}
