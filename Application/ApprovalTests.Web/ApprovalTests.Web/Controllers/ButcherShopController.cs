using System.Web.Http;
using ApprovalTests.Web.Models.BaconViewModels;
using ApprovalTests.Web.Services;

namespace ApprovalTests.Web.Controllers
{
    [RoutePrefix("Api/ButcherShop")]
    public class ButcherShopController : ApiController
    {
        private IBaconService _baconService;

        private IMapperService _mapperService;

        private IValidateInput _validationInput;

        // USED FOR TESTING
        public ButcherShopController(
            IBaconService baconService = null,
            IMapperService mapperService = null,
            IValidateInput validateInput = null)
        {
            // Poor Man's Dependency Injection
            _baconService = baconService ?? new BaconService();
            _mapperService = mapperService ?? new MapperService();
            _validationInput = validateInput ?? new ValidateInput();
        }

        public ButcherShopController()
        {
            // Poor Man's Dependency Injection
            _baconService = new BaconService();
            _mapperService = new MapperService();
            _validationInput = new ValidateInput();
        }

        // GET: api/ButcherShop/GetPigs
        [Route("GetPigs")]
        [HttpGet]
        public PigsViewModel GetPigs(int? id)
        {
            var validationMessage = _validationInput.ValidateGetPigs(id);
            if (validationMessage != null)
            {
                return new PigsViewModel { ErrorMessage = validationMessage };
            }

            var pigs = _baconService.GetGeneratedPigs(id.Value);

            return _mapperService.MapPigDomainToViewModel(pigs);
        }
    }
}
