using MarketAnalysisWebApi.DTOs.KnsCongigDTOs;
using MarketAnalysisWebApi.Providers;
using MarketAnalysisWebApi.Providers.EmailProvider;
using MarketAnalysisWebApi.Repos.InnerHelperRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarketAnalysisWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InnerControllerForDevelopers : ControllerBase
    {
        private readonly IInnerHelperRepo _innerHelperRepo;
        private readonly IMailServiceProvider _mailRepo;

        public InnerControllerForDevelopers(IInnerHelperRepo innerHelperRepo, IMailServiceProvider mailRepo)
        {
            _innerHelperRepo = innerHelperRepo;
            _mailRepo = mailRepo;
        }

        [HttpPost("request/config/add")]
        public async Task<IActionResult> CreateNewConfigType(string name)
        {
            try
            {
                await _innerHelperRepo.CreateNewRequestConfig(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("kns/equipment/add")]
        public async Task<IActionResult> CreateNewEquipment(string name)
        {
            try
            {
                await _innerHelperRepo.CreateKnsEquipment(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("companyType/create/new")]
        public async Task<IActionResult> CreateCompanyType(string name)
        {
            await _innerHelperRepo.InnerCreateCompanyType(name);  
            return Ok();
        }
        [HttpPost("businessAccount/noUser/create")]
        public async Task<IActionResult> CreateBusinessAcc(Guid userId)
        {
            await _innerHelperRepo.InnerCreateBusinessAcc(userId);
            return Ok();
        }

        [HttpPost("email/send")]
        public async Task<IActionResult> EmailSendingTest()
        {
            var reciever = new EmailReceiver
            {
                Address = "r3dinc@yandex.ru",
                Name = "Вадим Начаров"
            };
            await _mailRepo.Send(reciever, "Testing", "TestMessage");
            return Ok();
        }

        [HttpGet("request/region/create")]
        public async Task<IActionResult> CreateRegion(string name)
        {
            var res = await _innerHelperRepo.CreateRegion(name);
            return Ok(res);
        }

        [HttpGet("request/region/attach")]
        public async Task<IActionResult> AttachRegion(Guid requestId, Guid regionId)
        {
            var res = await _innerHelperRepo.AttachRegion(requestId, regionId);
            return Ok(res);
        }

        [HttpGet("regions/all")]
        public async Task<IActionResult> GetRegions()
        {
            var res = await _innerHelperRepo.GetAllRegions();
            return Ok(res);
        }

        //[HttpGet("sql/join/test")]
        //public async Task<IActionResult> GetJoinTestResuilt(Guid userId)
        //{
        //    var res = await _innerHelperRepo.InnerUserRequestJoin(userId);
        //    return Ok(res);
        //}
    }
}
