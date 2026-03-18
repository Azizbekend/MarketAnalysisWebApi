using MarketAnalysisWebApi.Repos.CompanyRepo;
using MarketAnalysisWebApi.Repos.UserRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        private readonly ICompanyRepo _companyRepo;

        public AdminController(IUserRepo userRepo, ICompanyRepo companyRepo)
        {
            _userRepo = userRepo;
            _companyRepo = companyRepo;
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
    }
}
