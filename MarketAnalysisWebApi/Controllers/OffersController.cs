using MarketAnalysisWebApi.DbEntities.FileStorages;
using MarketAnalysisWebApi.DTOs.FileStorageDTOS;
using MarketAnalysisWebApi.DTOs.OffersDTO;
using MarketAnalysisWebApi.Repos.FileStorageRepo;
using MarketAnalysisWebApi.Repos.OffersRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOfferRepo _offerRepo;
        private readonly IFileStorageRepo _fileStorageRepo;

        public OffersController(IOfferRepo offerRepo, IFileStorageRepo fileStorageRepo)
        {
            _offerRepo = offerRepo;
            _fileStorageRepo = fileStorageRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateOffer(OfferCreateDTO dto)
        {
            try
            {
                var res = await _offerRepo.CreateBusinesOffer(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users/offers")]
        public async Task<IActionResult> GetUsersOffers(Guid userId)
        {
            try
            {
                var res = await _offerRepo.GetEmpoyesOffersByUser(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("businessacc/offers")]
        public async Task<IActionResult> GetAccOffers(Guid acc)
        {
            try
            {
                var res = await _offerRepo.GetEmpoyesOffersByBusinessAccId(acc);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("requests/offers")]
        public async Task<IActionResult> GetRequestOffers(Guid requestId)
        {
            try
            {
                var res = await _offerRepo.GetEmpoyesOffersByRequestId(requestId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("single/offer/fullinfo")]
        public async Task<IActionResult> GetFullOfferById(Guid id)
        {
            try
            {
                var res = await _offerRepo.GetFullOfferForCustomer(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("offerFile/upload")]
        public async Task<IActionResult> UploadOfferFile(OfferFileCreateDTO dto, CancellationToken token)
        {
            try
            {
                if (dto.OfferFile == null || dto.OfferFile.Length == 0)
                {
                    return BadRequest("Файл не выбран или пуст");
                }
                if (dto.OfferFile.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("Размер файла не должен превышать 10 MB");
                }
                var savedFileId = await _fileStorageRepo.SaveBusinesOfferFileAsync(dto, token);
                return Ok(savedFileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("offerFile/download/")]
        public async Task<IActionResult> GetOffer(Guid offerId, CancellationToken token = default)
        {
            try
            {
                var file = await _fileStorageRepo.GetDbBusinessOfferFileModel(offerId, token);
                if (file == null)
                {
                    return BadRequest("Файл перемещен или удален!");
                }
                else
                {
                    return File(file.FileData, file.ContentType, file.FileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }



        [HttpPost("passportFile/upload")]
        public async Task<IActionResult> UploadPassportFile(EquipmentPassportFileCreateDTO dto, CancellationToken token)
        {
            try
            {
                if (dto.PassportFile == null || dto.PassportFile.Length == 0)
                {
                    return BadRequest("Файл не выбран или пуст");
                }
                if (dto.PassportFile.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("Размер файла не должен превышать 10 MB");
                }
                var savedFileId = await _fileStorageRepo.SaveEquipmentPassportFileAsync(dto, token);
                return Ok(savedFileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("equipPassport/download/")]
        public async Task<IActionResult> GetPassport(Guid passportId, CancellationToken token = default)
        {
            try
            {
                var file = await _fileStorageRepo.GetEquipmentPassportFileAsync(passportId, token);
                if (file == null)
                {
                    return BadRequest("Файл перемещен или удален!");
                }
                else
                {
                    return File(file.FileData, file.ContentType, file.FileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost("certificateFile/upload")]
        public async Task<IActionResult> UploadCertificateFile(EquipmentCertificateCreateDTO dto, CancellationToken token)
        {
            try
            {
                if (dto.CertificateFile == null || dto.CertificateFile.Length == 0)
                {
                    return BadRequest("Файл не выбран или пуст");
                }
                if (dto.CertificateFile.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("Размер файла не должен превышать 10 MB");
                }
                var savedFileId = await _fileStorageRepo.SaveEquipmentCertificateAsync(dto, token);
                return Ok(savedFileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("equipCertificate/download/")]
        public async Task<IActionResult> GetCertificate(Guid certifecateId, CancellationToken token = default)
        {
            try
            {
                var file = await _fileStorageRepo.GetEquipmentCertificateAsync(certifecateId, token);
                if (file == null)
                {
                    return BadRequest("Файл перемещен или удален!");
                }
                else
                {
                    return File(file.FileData, file.ContentType, file.FileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPost("schemeFile/upload")]
        public async Task<IActionResult> UploadCPlanFile(PlanFileCreateDTO dto, CancellationToken token)
        {
            try
            {
                if (dto.PlanFile == null || dto.PlanFile.Length == 0)
                {
                    return BadRequest("Файл не выбран или пуст");
                }
                if (dto.PlanFile.Length > 10 * 1024 * 1024)
                {
                    return BadRequest("Размер файла не должен превышать 10 MB");
                }
                var savedFileId = await _fileStorageRepo.SavePlanFileAsync(dto, token);
                return Ok(savedFileId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("scemeFile/download/")]
        public async Task<IActionResult> GetSchemee(Guid shemeFileId, CancellationToken token = default)
        {
            try
            {
                var file = await _fileStorageRepo.GetPlanFileAsync(shemeFileId, token);
                if (file == null)
                {
                    return BadRequest("Файл перемещен или удален!");
                }
                else
                {
                    return File(file.FileData, file.ContentType, file.FileName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }



    }



    }
