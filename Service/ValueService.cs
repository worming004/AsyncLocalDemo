using System.Collections.Generic;

namespace sandbox
{
    public interface IValueService
    {
        IEnumerable<int> GetValue();
    }
    public class ValueService : IValueService
    {
        private IValueProvider provider;

        public ValueService(IValueProvider provider)
        {
            this.provider = provider;
        }
        public IEnumerable<int> GetValue()
        {
            yield return this.provider.GetValue();
            yield return this.provider.GetValue();
            yield return this.provider.GetValue();
            yield return this.provider.GetValue();
            yield return this.provider.GetValue();
        }
    }
}