using System.Web.Http;
using ApprovalTests.Web.Models;
using ApprovalTests.Web.Services;

namespace ApprovalTests.Web.Controllers
{
    [RoutePrefix("Api/Data")]
    public class ApiController : System.Web.Http.ApiController
    {
        private IGetDataService _dataService;

        private IMapperService _mapperService;

        private IValidateInput _validationInput;

        // USED FOR TESTING
        public ApiController(
            IGetDataService dataService = null,
            IMapperService mapperService = null,
            IValidateInput validateInput = null)
        {
            // Poor Man's Dependency Injection
            _dataService = dataService ?? new GetDataService();
            _mapperService = mapperService ?? new MapperService();
            _validationInput = validateInput ?? new ValidateInput();
        }

        public ApiController()
        {
            // Poor Man's Dependency Injection
            _dataService = new GetDataService();
            _mapperService = new MapperService();
            _validationInput = new ValidateInput();
        }

        // GET: api/Data
        [Route("{id}")]
        [HttpGet]
        public TeamsViewModel Get(int? id)
        {
            var validationMessage = _validationInput.ValidateGet(id);
            if (validationMessage != null)
            {
                return new TeamsViewModel { ErrorMessage = validationMessage };
            }

            var pigs = _dataService.GetGeneratedTeams(id.Value);

            return _mapperService.MapPigDomainToViewModel(pigs);
        }
    }
}
