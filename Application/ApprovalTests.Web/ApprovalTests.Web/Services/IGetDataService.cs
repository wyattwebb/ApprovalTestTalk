using ApprovalTests.Web.PersistanceModels.BaconModels;

namespace ApprovalTests.Web.Services
{
    public interface IGetDataService
    {
        Pig[] GetGeneratedPigs(int count);
    }
}