using MarketAnalysisWebApi.DTOs.CompanyDTOs;
using MarketAnalysisWebApi.Repos.CompanyRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepo _companyRepo;

        public CompaniesController(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet("types/all")]
        public async Task<IActionResult> GetCompanyTypes()
        {
            var res = await _companyRepo.GetCompanyTypes();
            return Ok(res);
        }

        [HttpGet("company/inn")]
        public async Task<IActionResult> GetByInn(string inn)
        {
            try
            {
                var res = await _companyRepo.GetByInnAsync(inn);
                if(res != null)
                    return Ok(res);
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNewCompany(CreateCompanyDTO dto)
        {
            try
            {
                var res = await _companyRepo.CreateCompany(dto);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
