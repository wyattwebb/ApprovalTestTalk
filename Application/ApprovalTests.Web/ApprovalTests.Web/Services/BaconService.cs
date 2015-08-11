using System.Linq;
using ApprovalTests.Web.PersistanceModels.BaconModels;
using Ploeh.AutoFixture;

namespace ApprovalTests.Web.Services
{
    /// <summary>
    /// This is simply a stub to represent a persistance layer returning data. 
    /// AutoFixture should never be used out side of something like this or in a Unit Test
    /// </summary>
    public class BaconService : IBaconService
    {
        private readonly IFixture _fixture;

        public BaconService(
            IFixture fixture = null)
        {
            // Poor Man's Dependency Injection
            _fixture = fixture ?? new Fixture();
        }

        public Pig[] GetGeneratedPigs(int count)
        {
            return _fixture.CreateMany<Pig>(count).ToArray();
        }
    }
}