using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SampleApp.Application.Common.Attributes;

namespace SampleApp.Application.Common.Resolvers
{
    public class SensitiveDataResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (!(member is PropertyInfo prop))
                return property;

            var isSensitiveData = Attribute.IsDefined(prop, typeof(MaskedFieldInLogAttribute));
            if (isSensitiveData)
                property.ValueProvider = new MaskedValueProvider();

            return property;
        }
    }
}
