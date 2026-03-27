using MarketAnalysisWebApi.DbEntities.DbEntities;
using MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.PUMP;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.PumpDTO
{
    public class CreateInnerSubPump
    {
        public Guid PumpTypeId { get; set; }
        public Guid RequestId { get; set; }
        public double PotentialDepth { get; set; }
        public InstalationType InstalationType { get; set; }
    }
}
