using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApprovalTests.Web.Services
{
    public class SampleService
    {
        private IGetDataService _dataService;

        private IMapperService _mapperService;

        private IValidateInput _validationInput;

        // USED FOR TESTING
        public SampleService(
            IGetDataService dataService = null,
            IMapperService mapperService = null,
            IValidateInput validateInput = null)
        {
            // Poor Man's Dependency Injection
            _dataService = dataService ?? new GetDataService();
            _mapperService = mapperService ?? new MapperService();
            _validationInput = validateInput ?? new ValidateInput();
        }

        public SampleService()
        {
            // Poor Man's Dependency Injection
            _dataService = new GetDataService();
            _mapperService = new MapperService();
            _validationInput = new ValidateInput();
        }

        public bool IsSample(bool value)
        {
            // TODO: use the services if needed
            return value;
        }
    }
}