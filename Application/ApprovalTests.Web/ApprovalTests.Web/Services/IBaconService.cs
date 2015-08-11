using ApprovalTests.Web.PersistanceModels.BaconModels;

namespace ApprovalTests.Web.Services
{
    public interface IBaconService
    {
        Pig[] GetGeneratedPigs(int count);
    }
}