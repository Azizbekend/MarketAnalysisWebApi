using MarketAnalysisWebApi.DTOs.OffersDTO;
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

        public OffersController(IOfferRepo offerRepo)
        {
            _offerRepo = offerRepo;
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
        
    }
}
