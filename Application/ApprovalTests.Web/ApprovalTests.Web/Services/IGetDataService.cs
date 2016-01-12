using ApprovalTests.Web.PersistenceModels;

namespace ApprovalTests.Web.Services
{
    public interface IGetDataService
    {
        Team[] GetGeneratedTeams(int count);
    }
}