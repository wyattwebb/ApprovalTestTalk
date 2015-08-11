using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace ApprovalTests.Web.Tests.Setup
{
    public class ApprovalTestCustomization :
        ICustomization
    {
        protected static readonly char DefaultChar = 'X';
        protected static readonly string DefaultString = "...";
        protected static readonly DateTime DefaultDateTime = new DateTime(1111, 1, 1, 1, 1, 1, 111);

        public virtual void Customize(IFixture fixture)
        {
            var specimenBuilders = CreateSpecimenBuilders();

            foreach (var specimenBuilder in specimenBuilders)
            {
                fixture.Customizations.Add(specimenBuilder);
            }
        }

        protected virtual IEnumerable<ISpecimenBuilder> CreateSpecimenBuilders()
        {
            yield return new ConsistentSpecimenBuilder();
        }

        protected class ConsistentSpecimenBuilder :
            ISpecimenBuilder
        {
            public virtual object Create(
                object request,
                ISpecimenContext context)
            {
                var seededRequest = request as SeededRequest;

                if (seededRequest != null
                    && seededRequest.Request is Type)
                {
                    return HandleSeededRequest(
                        seededRequest,
                        seededRequest.Request as Type,
                        context);
                }

                return TryCreateSpecimen(request, context)
                    ?? new NoSpecimen(request);
            }

            protected virtual object HandleSeededRequest(
                SeededRequest seededRequest,
                Type seededRequestType,
                ISpecimenContext context)
            {
                if (seededRequestType == typeof(string)
                    && seededRequest.Seed is string)
                {
                    return seededRequest.Seed;
                }

                return TryCreateSpecimen(seededRequestType, context)
                    ?? new NoSpecimen(seededRequest);
            }

            protected virtual object TryCreateSpecimen(
                object request,
                ISpecimenContext context)
            {
                var type = request as Type;

                if (type == null)
                {
                    return null;
                }

                if (type == typeof(string))
                {
                    return DefaultString;
                }

                if (type.IsEnum)
                {
                    return Enum.GetValues(type)
                        .Cast<object>()
                        .LastOrDefault();
                }

                if (type == typeof(char))
                {
                    return DefaultChar;
                }

                if (type == typeof(DateTime))
                {
                    return DefaultDateTime;
                }

                // Set all numeric types to the same (non-default) value
                if (type.IsPrimitive || type == typeof(decimal))
                {
                    return Convert.ChangeType(1, type);
                }

                return null;
            }
        }
    }
}
