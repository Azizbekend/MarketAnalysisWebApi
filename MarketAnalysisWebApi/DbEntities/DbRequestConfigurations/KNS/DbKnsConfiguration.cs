using MarketAnalysisWebApi.DbEntities.Base;
using MarketAnalysisWebApi.DbEntities.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.KNS
{
    public enum PerfomanceMeasureUnit
    {
        LiterInSecond = 0,
        CubicMeter
    }

    public enum PumpsStartupMethod
    {
        Direct = 0,
        Smooth,
        FrequencyControl
    }
    public enum PumpEnvironment
    {
        Domestic = 0,
        Storm,
        Industrial
    }

    public enum PipelineMaterial 
    {
        Polyethylene = 0,
        Polypropylene,
        Steel
    }

    public enum ControllerInstalationPlace
    {
        UHL1 = 0,
        UHL2 = 1,
        UHL3 = 2,
        UHL4 = 3
    }


    public class DbKnsConfiguration : DbBaseEntity
    {
        public double Perfomance { get; set; }

        [Range(0, 1, ErrorMessage = "Значение может быть только 0 или 1")]
        public PerfomanceMeasureUnit Units { get; set; }
        public double RequiredPumpPressure { get; set; }
        public int ActivePumpsCount { get; set; }
        public int ReservePumpsCount { get; set; }
        public string ? InstalationPlace { get; set; }
        public int PumpsToWarehouseCount { get; set; }
        [Range(0, 2, ErrorMessage = "Значение может быть только от 0 до 2")]
        public PumpsStartupMethod startupMethod { get; set; }
        [Range(0, 2, ErrorMessage = "Значение может быть только от 0 до 2")]
        public PumpEnvironment PType { get; set; }
        public int EnvironmentTemperature{ get; set; }
        public bool ExplosionProtection { get; set; }
        public int SupplyPipelineDepth { get; set; }
        public int SupplyPipelineDiameter { get; set; }
        [Range(0, 2, ErrorMessage = "Значение может быть только от 0 до 2")]
        public PipelineMaterial SupplyPipelineMaterial { get; set; }
        public int SupplyPipelineDirectionInHours { get; set; }
        public int PressurePipelineDepth { get; set; }
        public int PressurePipelineDiameter { get; set; }
        [Range(0, 2, ErrorMessage = "Значение может быть только от 0 до 2")]
        public PipelineMaterial PressurePipelineMaterial { get; set; }
        public int PressurePipelineDirectionInHours { get; set; }
        public bool HasManyExitPressurePipelines { get; set; }
        public int ExpectedDiameterOfPumpStation { get; set; }
        public int ExpectedHeightOfPumpStation { get; set; }
        public int InsulatedHousingDepth { get; set; }
        public int PowerContactsToController { get; set; }
        [Range(0, 3, ErrorMessage = "Значение может быть только от 0 до 2")]
        public ControllerInstalationPlace Place { get; set; }

        public Guid RequestId { get; set; }
        [ForeignKey(nameof(RequestId))]
        public DbProjectRequest? Request { get; set; }


    }
}
