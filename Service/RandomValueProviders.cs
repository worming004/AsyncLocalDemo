using System;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace sandbox {
    public interface IValueProvider {
        int GetValue ();
    }

    public class ValueProviderCacheDecorator : IValueProvider
    { 
        private IValueProvider next;
        private AsyncLocal<int?> cache = new AsyncLocal<int?>();
        public ValueProviderCacheDecorator(IValueProvider next) {
            // logger.LogInformation("Build ValueProviderCacheDecorator");
            this.next = next;
        }

        public int GetValue()
        {
            if (cache.Value == null) {
                cache.Value = next.GetValue();
            }
            return (int)cache.Value;
        }
    }

    public class RandomValueProvider : IValueProvider
    {
        private static Random rnd = new Random();
        public RandomValueProvider(ILogger<RandomValueProvider> logger) {
            logger.LogInformation("Build RandomValueProvider");
        }
        public int GetValue()
        {
            return rnd.Next();
        }
    }
}