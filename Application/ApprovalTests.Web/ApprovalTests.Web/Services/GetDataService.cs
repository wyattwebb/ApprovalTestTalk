using System.Linq;
using ApprovalTests.Web.PersistanceModels.BaconModels;
using Ploeh.AutoFixture;

namespace ApprovalTests.Web.Services
{
    /// <summary>
    /// This is simply a stub to represent a persistance layer returning data. 
    /// </summary>
    public class GetDataService : IGetDataService
    {

        public GetDataService()
        {
        }

        public Pig[] GetGeneratedPigs(int count)
        {
            return new[]
            {
                new Pig {}
            };
        }
    }
}