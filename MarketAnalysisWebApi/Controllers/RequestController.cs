using MarketAnalysisWebApi.DTOs.FileStorageDTOS;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.DTOs.RequestDTOs.Supplier;
using MarketAnalysisWebApi.Repos.FileStorageRepo;
using MarketAnalysisWebApi.Repos.ProjectRequestRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IProjectRequestRepo _projectRequestRepo;
        private readonly IFileStorageRepo _fileStorageRepo;

        public RequestController(IProjectRequestRepo projectRequestRepo, IFileStorageRepo fileStorageRepo)
        {
            _projectRequestRepo = projectRequestRepo;
            _fileStorageRepo = fileStorageRepo;
        }
        [HttpGet("sherry/half")]
        public async Task<IActionResult> GEtSherreyRRequest(Guid id)
        {
            var res = await _projectRequestRepo.GetSherryRequest(id);
            return Ok(res);
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetRequets(Guid id)
        {
            var res = await _projectRequestRepo.GetRequestWithRegion(id);
            return Ok(res);
        }
        [HttpGet("all/regions")]
        public async Task<IActionResult> GetRequestWithRegins()
        {
            var res = await _projectRequestRepo.GetRequestsWithRegions();
            return Ok(res);
        }
        [HttpPost("supplier/single")]
        public async Task<IActionResult> GetSuppliersReqWithStatus(SupplierCheckRequestDTo dTo)
        {
            try
            {
                var res = await _projectRequestRepo.GetRequestWithStatusById(dTo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("supplier/single/halfrequest")]
        public async Task<IActionResult> GetHalfSingle(SupplierCheckRequestDTo dTo)
        {
            try
            {
                var res = await _projectRequestRepo.GetRequestHalfRequestForSupplier(dTo);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("user/requests/all")]
        public async Task<IActionResult> GetUsersRequests(Guid userId)
        {
            try
            {
                var res = await _projectRequestRepo.GetUsersPublishRequests(userId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(userId);
            }
        }
        [HttpGet("actual/published/all")]
        public async Task<IActionResult> GetActualRequests()
        {
            try
            {
                var res = await _projectRequestRepo.GetPublishedRequests();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("request/payStatus/check")]
        public async Task<IActionResult> CheckPayStatus(SupplierCheckRequestDTo dTo)
        {
            var res = await _projectRequestRepo.CheckPayStatus(dTo);
            return Ok(res);
        }

        [HttpPost("supplier/view/request")]
        public async Task<IActionResult> CheckRequest(SupplierCheckRequestDTo dto)
        {
            try
            {
                var res = await _projectRequestRepo.CheckRequestBySupplier(dto);
                return Ok(res);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("supplier/click/request")]
        public async Task<IActionResult> BayRequest(SupplierCheckRequestDTo dto)
        {
            try
            {
                await _projectRequestRepo.CreateClickForCoins(dto);
                return Ok();
            }
            catch
            {
                return BadRequest("что-то пошло не так...");
            }
        }
        [HttpGet("user/favourites/table/base")]
        public async Task<IActionResult> GetListWithFavourites(Guid id)
        {
            var res = await _projectRequestRepo.GetFavoriteProjectRequestsByUserIdAsync(id);
            return Ok(res);
        }
        //public async Task<IActionResult> GetUsersFavouritesRequests(Guid userId)
        //{
        //    var res = await _projectRequestRepo.GetFavourites(userId);
        //    return Ok(res);
        //}
        //[HttpGet("user/favourites")]
        //public async Task<IActionResult> GetUsersFavouritesRequests(Guid userId)
        //{
        //    var res = await _projectRequestRepo.GetFavourites(userId);
        //    return Ok(res); 
        //}
        [HttpPost("favourites/add")]
        public async Task<IActionResult> AddToFavourites(RequestStandartDTO dto)
        {
            try
            {
                var res = await _projectRequestRepo.AddToFavourites(dto);
                return Ok(res);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("favourites/remove")]
        public async Task<IActionResult> RemoveFromFavourites(RequestStandartDTO dto)
        {
            try
            {
                await _projectRequestRepo.RemoveFromFavourites(dto);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishRequest(RequestStandartDTO dto)
        {
            try
            {
                var res = await _projectRequestRepo.PublishRequest(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("archive")]
        public async Task<IActionResult> ArchiveRequest(RequestStandartDTO dto)
        {
            try
            {
                var res = await _projectRequestRepo.ArchiveRequest(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("schemeFile/download")]
        public async Task<IActionResult> UploadRequestSchemeFile(Guid fileId, CancellationToken token)
        {
            try
            {
                var res = await _fileStorageRepo.GetRequestFileAsync(fileId, token);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("schemeFile/upload")]
        public async Task<IActionResult> UploadRequestSchemeFile(RequestSchemeFileDTO dto, CancellationToken token)
        {
            try
            {
                var res = await _fileStorageRepo.SaveRequestFileAsync(dto, token);
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
