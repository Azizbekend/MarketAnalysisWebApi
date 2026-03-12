using MarketAnalysisWebApi.DbEntities;
using MarketAnalysisWebApi.DbEntities.FileStorages;
using MarketAnalysisWebApi.DTOs.FileStorageDTOS;
using Microsoft.EntityFrameworkCore;

namespace MarketAnalysisWebApi.Repos.FileStorageRepo
{
    public class FileStorageRepo : IFileStorageRepo
    {
        private readonly AppDbContext _appDbContext;

        public FileStorageRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<DbBusinessOfferFileModel> GetDbBusinessOfferFileModel(Guid fileId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<DbEquipmentCertificateFileModel> GetEquipmentCertificateAsync(Guid certId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<DbEquipmentPassportFile> GetEquipmentPassportFileAsync(Guid passportFileId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<DbPlanFile> GetPlanFileAsync(Guid planFileId, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> SaveBusinesOfferFileAsync(OfferFileCreateDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.OfferFile);
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferId);
            ArgumentNullException.ThrowIfNull(offer);
            var fileModel = new DbBusinessOfferFileModel
            {
                FileName = dto.OfferFile.FileName,
                ContentType = dto.OfferFile.ContentType,
                FileSize = dto.OfferFile.Length,
                FileData = await ReadFileDataAsync(dto.OfferFile, token)
            };           
            await _appDbContext.OfferFilesTable.AddAsync(fileModel, token);
            await _appDbContext.SaveChangesAsync(token);
            offer.OfferFileId = fileModel.Id;
            return fileModel.Id;

        }

        public async Task<Guid> SaveEquipmentCertificateAsync(EquipmentCertificateCreateDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.CertificateFile);
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferId);
            ArgumentNullException.ThrowIfNull(offer);
            var fileModel = new DbBusinessOfferFileModel
            {
                FileName = dto.CertificateFile.FileName,
                ContentType = dto.CertificateFile.ContentType,
                FileSize = dto.CertificateFile.Length,
                FileData = await ReadFileDataAsync(dto.CertificateFile, token)
            };
            await _appDbContext.OfferFilesTable.AddAsync(fileModel, token);
            await _appDbContext.SaveChangesAsync(token);
            offer.OfferFileId = fileModel.Id;
            return fileModel.Id;
        }

        public async Task<Guid> SaveEquipmentPassportFileAsync(EquipmentPassportFileCreateDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.PassportFile);
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferId);
            ArgumentNullException.ThrowIfNull(offer);
            var fileModel = new DbBusinessOfferFileModel
            {
                FileName = dto.PassportFile.FileName,
                ContentType = dto.PassportFile.ContentType,
                FileSize = dto.PassportFile.Length,
                FileData = await ReadFileDataAsync(dto.PassportFile, token)
            };
            await _appDbContext.OfferFilesTable.AddAsync(fileModel, token);
            await _appDbContext.SaveChangesAsync(token);
            offer.OfferFileId = fileModel.Id;
            return fileModel.Id;
        }

        public Task<Guid> SavePlanFileAsync(PlanFileCreateDTO dto, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        private static async Task<byte[]> ReadFileDataAsync(
       IFormFile file,
       CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }
    }
}
