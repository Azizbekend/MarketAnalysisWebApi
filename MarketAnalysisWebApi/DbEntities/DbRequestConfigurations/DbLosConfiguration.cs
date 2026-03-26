using MarketAnalysisWebApi.DbEntities.Base;

namespace MarketAnalysisWebApi.DbEntities.DbRequestConfigurations
{
    public class DbLosConfiguration : DbBase
    {
        public double TotalCathmentArea { get; set; }
        public double WaterproofSurfaceArea { get; set; }
        public double PavementArea { get; set; }
        public double CobblestonePavementArea { get; set; }


    }

}
