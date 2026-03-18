using MarketAnalysisWebApi.DbEntities.DbEntities;

namespace MarketAnalysisWebApi.DTOs.RequestDTOs
{
    public class RequestStasusChangeDTo
    {
        public Guid RequestId { get; set; }
        public RequestStatus NewStatus { get; set; }
    }
}
