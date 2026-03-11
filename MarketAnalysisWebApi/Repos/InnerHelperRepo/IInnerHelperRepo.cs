namespace MarketAnalysisWebApi.Repos.InnerHelperRepo
{
    public interface IInnerHelperRepo
    {
        Task CreateNewRequestConfig(string name);
        Task CreateKnsEquipment(string name);
    }
}
