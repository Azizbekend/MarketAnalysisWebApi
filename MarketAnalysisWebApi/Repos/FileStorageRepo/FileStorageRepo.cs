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
            offer.OfferFileId = fileModel.Id;
            _appDbContext.OffersTable.Attach(offer);
            await _appDbContext.SaveChangesAsync(token);
            return fileModel.Id;

        }

        public async Task<Guid> SaveEquipmentCertificateAsync(EquipmentCertificateCreateDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.CertificateFile);
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferId);
            ArgumentNullException.ThrowIfNull(offer);
            var fileModel = new DbEquipmentCertificateFileModel
            {
                FileName = dto.CertificateFile.FileName,
                ContentType = dto.CertificateFile.ContentType,
                FileSize = dto.CertificateFile.Length,
                FileData = await ReadFileDataAsync(dto.CertificateFile, token)
            };
            await _appDbContext.CertificatesFilesTable.AddAsync(fileModel, token);
            offer.CertificateFileId = fileModel.Id;
            _appDbContext.OffersTable.Attach(offer);
            await _appDbContext.SaveChangesAsync(token);
            return fileModel.Id;
        }

        public async Task<Guid> SaveEquipmentPassportFileAsync(EquipmentPassportFileCreateDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.PassportFile);
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferId);
            ArgumentNullException.ThrowIfNull(offer);
            var fileModel = new DbEquipmentPassportFile
            {
                FileName = dto.PassportFile.FileName,
                ContentType = dto.PassportFile.ContentType,
                FileSize = dto.PassportFile.Length,
                FileData = await ReadFileDataAsync(dto.PassportFile, token)
            };
            await _appDbContext.PassportsFilesTable.AddAsync(fileModel, token);
            offer.PassportFileId = fileModel.Id;
            _appDbContext.OffersTable.Attach(offer);
            await _appDbContext.SaveChangesAsync(token);
            return fileModel.Id;
        }

        public async  Task<Guid> SavePlanFileAsync(PlanFileCreateDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.PlanFile);
            var offer = await _appDbContext.OffersTable.FirstOrDefaultAsync(x => x.Id == dto.OfferId);
            ArgumentNullException.ThrowIfNull(offer);
            var fileModel = new DbPlanFile
            {
                FileName = dto.PlanFile.FileName,
                ContentType = dto.PlanFile.ContentType,
                FileSize = dto.PlanFile.Length,
                FileData = await ReadFileDataAsync(dto.PlanFile, token)
            };
            await _appDbContext.PlanFilesTable.AddAsync(fileModel, token);
            offer.PlanFileId = fileModel.Id;
            _appDbContext.OffersTable.Attach(offer);
            await _appDbContext.SaveChangesAsync(token);
            return fileModel.Id;
        }

        private static async Task<byte[]> ReadFileDataAsync(
       IFormFile file,
       CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }

        public async Task<Guid> SaveRequestFileAsync(RequestSchemeFileDTO dto, CancellationToken token = default)
        {
            ArgumentNullException.ThrowIfNull(dto.File);
            var request = await _appDbContext.ProjectRequestsTable.FirstOrDefaultAsync(x => x.Id == dto.RequestId);
            ArgumentNullException.ThrowIfNull(request);
            var fileModel = new DbRequestFileModel
            {
                FileName = dto.File.FileName,
                ContentType = dto.File.ContentType,
                FileSize = dto.File.Length,
                FileData = await ReadFileDataAsync(dto.File, token)
            };
            await _appDbContext.RequestFiles.AddAsync(fileModel, token);
            request.FileId = fileModel.Id;
            _appDbContext.ProjectRequestsTable.Attach(request);
            await _appDbContext.SaveChangesAsync(token);
            return fileModel.Id;
        }

        public async Task<DbRequestFileModel> GetRequestFileAsync(Guid requestSchemeFileId, CancellationToken token = default)
        {
            return await _appDbContext.RequestFiles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == requestSchemeFileId, token);
        }
    }
}
