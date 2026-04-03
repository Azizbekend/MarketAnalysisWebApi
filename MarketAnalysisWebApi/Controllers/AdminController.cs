using MarketAnalysisWebApi.DTOs.FileStorageDTOS;
using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.CompanyRepo;
using MarketAnalysisWebApi.Repos.FileStorageRepo;
using MarketAnalysisWebApi.Repos.InnerHelperRepo;
using MarketAnalysisWebApi.Repos.ProjectRequestRepo;
using MarketAnalysisWebApi.Repos.UserRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ICompanyRepo _companyRepo;
        private readonly IInnerHelperRepo _innerHelperRepo;
        private readonly IProjectRequestRepo _requestRepo;
        private readonly IFileStorageRepo _fileStorageRepo;

        public AdminController(IUserRepo userRepo, ICompanyRepo companyRepo, IInnerHelperRepo innerHelperRepo, IProjectRequestRepo requestRepo, IFileStorageRepo fileStorageRepo)
        {
            _userRepo = userRepo;
            _companyRepo = companyRepo;
            _innerHelperRepo = innerHelperRepo;
            _requestRepo = requestRepo;
            _fileStorageRepo = fileStorageRepo;
        }

        [HttpGet("users/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var res = await _userRepo.GetAllAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("companies/all")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                var res = await _companyRepo.GetAllAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("requests/all")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var res = await _requestRepo.GetPublishedRequests();
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("request/archive/change")]
        public async Task<IActionResult> ArchiveRequest(Guid id)
        {
            try
            {
                var res = await _innerHelperRepo.RequestArchive(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("request/statusChange")]
        public async Task<IActionResult> RequestStatusChange(RequestStasusChangeDTo dto)
        {
            try
            {
                var res = await _innerHelperRepo.RequestStatusChangeAsync(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("request/schemeFile/replace")]
        public async Task<IActionResult> ReplaceSchemeFile(RequestFileSchemeUpdateDTO dto)
        {
            var res = await _fileStorageRepo.ReplaceRequestFileAsync(dto);
            return Ok(res);
        }

        [HttpDelete("requset/schemeFile")]
        public async Task<IActionResult> DeleteSchemeFile(Guid id)
        { 
            await _fileStorageRepo.DeleteRequestFileAsync(id);
            return Ok("Succesfull deleted!");
        }
        [HttpDelete("request/cascade")]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            await _requestRepo.DeleteCascade(id);
            return Ok();
        }
    }
}
