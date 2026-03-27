using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations.LOS
{
    public class DbLosConfiguration : DbBaseEntity
    {
        public double TotalCathmentArea { get; set; }
        public double WaterproofSurfaceArea { get; set; }
        public double PavementArea { get; set; }
        public double CobblestonePavementArea { get; set; }


    }

}
