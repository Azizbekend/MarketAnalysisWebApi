using MarketAnalysisWebApi.DTOs.RequestDTOs;
using MarketAnalysisWebApi.Repos.CompanyRepo;
using MarketAnalysisWebApi.Repos.InnerHelperRepo;
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

        public AdminController(IUserRepo userRepo, ICompanyRepo companyRepo, IInnerHelperRepo innerHelperRepo)
        {
            _userRepo = userRepo;
            _companyRepo = companyRepo;
            _innerHelperRepo = innerHelperRepo;
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
        
    }
}
