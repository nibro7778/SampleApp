using Newtonsoft.Json.Serialization;

namespace SampleApp.Application.Common.Resolvers
{
    public class MaskedValueProvider : IValueProvider
    {
        private const string MaskedValue = "****";
        public void SetValue(object target, object? value) => throw new InvalidOperationException();
        public object? GetValue(object target) => MaskedValue;
    }
}
