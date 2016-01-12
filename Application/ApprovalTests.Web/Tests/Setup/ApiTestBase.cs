using System;
using System.Collections.Generic;
using ApprovalTests.Reporters;
using ApprovalTests.Web.Tests.ControllerTests;
using ApprovalTests.Web.Tests.Setup.ApiTesting.Infrastructure;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using RestSharp;

namespace ApprovalTests.Web.Tests.Setup
{
    public abstract class ApiTestBase
    {
        private const string JsonContentType = "application/json";

        protected abstract string BaseUrl
        {
            get;
        }

        [UseReporter(typeof(DiffReporter))]
        protected void VerifyApprovedJsonResult(object result)
        {
            Approvals.Verify(
                JsonConvert.SerializeObject(
                    result,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new JsonApprovalOutputContractResolver()
                    }));
        }

        [UseReporter(typeof(FileLauncherReporter), typeof(DiffReporter))]
        protected void VerifyApprovedHtmlResult(string result)
        {
            Approvals.VerifyHtml(result);
        }

        protected dynamic ExecuteGetRequest(
           string apiPath)
        {
            return ExecuteGetRequest<dynamic>(
                apiPath);
        }

        protected T ExecuteGetRequest<T>(
            string apiPath)
        {
            return ExecuteRequest<T>(
                new RestRequest(apiPath));
        }

        protected dynamic ExecutePostRequest(
            string apiPath,
            object data)
        {
            return ExecutePostRequest<dynamic>(
                apiPath,
                data);
        }

        protected T ExecutePostRequest<T>(
            string apiPath,
            object data)
        {
            return ExecuteRequest<T>(
                apiPath,
                data,
                Method.POST);
        }

        protected dynamic ExecutePatchRequest(
            string apiPath,
            object data)
        {
            return ExecutePatchRequest<dynamic>(
                apiPath,
                data);
        }

        protected T ExecutePatchRequest<T>(
            string apiPath,
            object data)
        {
            return ExecuteRequest<T>(
                apiPath,
                data,
                Method.PATCH);
        }

        private T ExecuteRequest<T>(
            string apiPath,
            object data,
            Method method)
        {
            var postData = JsonConvert.SerializeObject(data);
            var request = new RestRequest(
                apiPath,
                method)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddParameter("application/json; charset=utf-8", postData, ParameterType.RequestBody);
            request.AddHeader("Content-Type", JsonContentType);
            request.AddHeader("Accepts", JsonContentType);

            return ExecuteRequest<T>(request);
        }

        private T ExecuteRequest<T>(
            RestRequest request)
        {
            var client = CreateClient();
            var response = client.Execute(request);

            if (response.ResponseStatus != ResponseStatus.Completed
                || (int)response.StatusCode >= 300
                || String.IsNullOrWhiteSpace(response.Content)
                || response.ErrorException != null
                || !String.IsNullOrWhiteSpace(response.ErrorMessage))
            {
                throw response.ErrorException
                    ?? new Exception(response.ErrorMessage ?? response.Content);
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        private RestClient CreateClient()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(BaseUrl),
                Timeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds
            };

            client.AddDefaultHeader("api-version", "1");
            client.AddDefaultHeader("Accept", JsonContentType);

            return client;
        }
    }
}
