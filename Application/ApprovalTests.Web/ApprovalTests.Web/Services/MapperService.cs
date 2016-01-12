using System.Linq;
using ApprovalTests.Web.Models;
using ApprovalTests.Web.PersistenceModels;

namespace ApprovalTests.Web.Services
{
    /// <summary>
    /// This is a stub mapper.
    /// This is meant to emulate something like AutoMapper or Red Arrow's Ignition Projection
    /// </summary>
    public class MapperService : IMapperService
    {
        public TeamsViewModel MapPigDomainToViewModel(Team[] teams
            )
        {
            return new TeamsViewModel()
            {
                Teams =
                    teams.Select(members =>
                        new TeamsViewModel.TeamViewModel()
                        {
                            Center = members.Center,
                            CornerBack = members.CornerBack,
                            DefensiveEnd =  members.DefensiveEnd,
                            DefensiveTackle = members.DefensiveTackle,
                            LineBacker = members.LineBacker,
                            OffensiveGuard = members.OffensiveGuard,
                            OffensiveTackle = members.OffensiveTackle,
                            QuarterBack = members.QuarterBack,
                            RunningBack = members.RunningBack,
                            Safety = members.Safety,
                            TightEnd = members.TightEnd,
                            WideReciever = members.WideReciever
                        }
                        ).ToArray()
            };
        }
    }
}