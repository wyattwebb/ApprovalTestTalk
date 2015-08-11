using System.Linq;
using ApprovalTests.Web.Models.BaconViewModels;
using ApprovalTests.Web.PersistanceModels.BaconModels;

namespace ApprovalTests.Web.Services
{
    /// <summary>
    /// This is a stub mapper.
    /// This is meant to emulate something like AutoMapper or Red Arrow's Ignition Projection
    /// </summary>
    public class MapperService : IMapperService
    {
        public PigsViewModel MapPigDomainToViewModel(Pig[] domainPigs)
        {
            return new PigsViewModel()
            {
                Pigs =
                    domainPigs.Select(parts =>
                        new PigsViewModel.PigViewModel
                        {
                            Back = parts.Back,
                            Belly = parts.Belly,
                            Cannon = parts.Cannon,
                            Cheek = parts.Cheek,
                            Coffin = parts.Coffin,
                            Crops = parts.Crops,
                            Dewclaw = parts.Dewclaw,
                            Ear = parts.Ear,
                            FetLock = parts.FetLock,
                            ForeFlank = parts.ForeFlank,
                            ForeLeg = parts.ForeLeg,
                            Ham = parts.Ham,
                            Hock = parts.Hock,
                            Jowls = parts.Jowls,
                            Knee = parts.Knee,
                            Loin = parts.Loin,
                            Neck = parts.Neck,
                            Nostrils = parts.Nostrils,
                            Pastern = parts.Pastern,
                            Poll = parts.Poll,
                            RearFlank = parts.RearFlank,
                            Rump = parts.Rump,
                            Sheath = parts.Sheath,
                            Shoulder = parts.Shoulder,
                            Snout = parts.Snout,
                            Stiffle = parts.Stiffle,
                            Tail = parts.Tail
                        }
                        ).ToArray()
            };
        }
    }
}