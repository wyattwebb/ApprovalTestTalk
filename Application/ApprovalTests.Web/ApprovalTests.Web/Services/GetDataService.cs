using System.Collections.Generic;
using System.Linq;
using ApprovalTests.Web.PersistenceModels;
using Ploeh.AutoFixture;

namespace ApprovalTests.Web.Services
{
    /// <summary>
    /// This is simply a stub to represent a persistance layer returning data. 
    /// </summary>
    public class GetDataService : IGetDataService
    {
        public Team[] GetGeneratedTeams(int count)
        {
            var teams = new List<Team>();
            for (int i = 0; i < count; i++)
            {
                teams.Add(new Team());
            }

            return teams.ToArray();
        }
    }
}