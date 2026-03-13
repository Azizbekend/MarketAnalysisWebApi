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

        public async Task<DbBusinessOfferFileModel> GetDbBusinessOfferFileModel(Guid fileId, CancellationToken token = default)
        {
            return await _appDbContext.OfferFilesTable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == fileId, token);
        }

        public async Task<DbEquipmentCertificateFileModel> GetEquipmentCertificateAsync(Guid certId, CancellationToken token = default)
        {
            return await _appDbContext.CertificatesFilesTable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == certId, token);
        }

        public async Task<DbEquipmentPassportFile> GetEquipmentPassportFileAsync(Guid passportFileId, CancellationToken token = default)
        {
            return await _appDbContext.PassportsFilesTable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == passportFileId, token);
        }

        public async Task<DbPlanFile> GetPlanFileAsync(Guid planFileId, CancellationToken token = default)
        {
            return await _appDbContext.PlanFilesTable.AsNoTracking().FirstOrDefaultAsync(x => x.Id == planFileId, token);
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
