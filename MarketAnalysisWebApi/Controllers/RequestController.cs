using MarketAnalysisWebApi.DTOs.RequestDTOs;
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

        public RequestController(IProjectRequestRepo projectRequestRepo)
        {
            _projectRequestRepo = projectRequestRepo;
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetRequets(Guid id)
        {
            var res = await _projectRequestRepo.GetRequestById(id);
            return Ok(res);
        }
        [HttpGet("user/requests/all")]
        public async Task<IActionResult> GetUsersRequests(Guid userId)
        {
            //Console.WriteLine($" VOT TUT ==========================>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> { userId}");
            var res = await _projectRequestRepo.GetUsersRequests(userId);
            return Ok(res);
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
    }
}
