using MarketAnalysisWebApi.DbEntities.FileStorages;
using MarketAnalysisWebApi.DTOs.FileStorageDTOS;

namespace MarketAnalysisWebApi.Repos.FileStorageRepo
{
    public interface IFileStorageRepo
    {
        Task<DbBusinessOfferFileModel> SaveBusinesOfferFileAsync(OfferFileCreateDTO dto, CancellationToken token = default);
        
    }
}
