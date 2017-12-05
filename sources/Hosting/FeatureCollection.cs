using System;
using System.Collections.Concurrent;

namespace Hosting
{
    public class FeatureCollection : IFeatureCollection
    {
        private readonly ConcurrentDictionary<Type, object> _features = new ConcurrentDictionary<Type, object>();

        public TFeature Get<TFeature>()
        {
            object feature;
            return _features.TryGetValue(typeof (TFeature), out feature) ? (TFeature) feature : default(TFeature);
        }

        public void Set<TFeature>(TFeature instance)
        {
            _features[typeof (TFeature)] = instance;
        }
    }
}