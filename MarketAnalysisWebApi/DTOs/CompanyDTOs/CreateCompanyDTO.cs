using MarketAnalysisWebApi.DbEntities.FileStorages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DTOs.CompanyDTOs
{
    public class CreateCompanyDTO
    {
        public string? FullCompanyName { get; set; }
        public string? ShortCompanyName { get; set; }
        public string? INN { get; set; }
        public string? KPP { get; set; }
        public string? JurAdress { get; set; }
        public Guid CompanyTypeId { get; set; }
    }
}
