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
            var res = await _projectRequestRepo.GetRequestByUserId(id);
            return Ok(res);
        }
        [HttpGet("user/requests/all")]
        public async Task<IActionResult> GetUsersRequests(Guid userId)
        {
            var res = await _projectRequestRepo.GetRequestByUserId(userId);
            return Ok(res);
        }
    }
}
