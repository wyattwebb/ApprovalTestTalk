using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApprovalTests.Web.Tests.Setup
{
    namespace ApiTesting.Infrastructure
    {
        public class JsonApprovalOutputContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(
            Type type,
            MemberSerialization memberSerialization)
            {
                var allProperties = base.CreateProperties(
                    type,
                    memberSerialization);

                return allProperties
                    .OrderBy(prop => prop.PropertyName)
                    .ToList();
            }
        }
    }
}
