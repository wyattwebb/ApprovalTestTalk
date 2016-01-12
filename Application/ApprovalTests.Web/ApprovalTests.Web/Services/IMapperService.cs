using ApprovalTests.Web.Models;
using ApprovalTests.Web.PersistenceModels;

namespace ApprovalTests.Web.Services
{
    public interface IMapperService
    {
        TeamsViewModel MapPigDomainToViewModel(Team[] teams);
    }
}