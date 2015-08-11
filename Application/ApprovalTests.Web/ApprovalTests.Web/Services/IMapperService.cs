using ApprovalTests.Web.Models.BaconViewModels;
using ApprovalTests.Web.PersistanceModels.BaconModels;

namespace ApprovalTests.Web.Services
{
    public interface IMapperService
    {
        PigsViewModel MapPigDomainToViewModel(Pig[] domainPigs);
    }
}