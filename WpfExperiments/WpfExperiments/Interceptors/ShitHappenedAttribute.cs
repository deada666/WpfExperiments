using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace WpfExperiments.Interceptors
{
    public class ShitHappenedAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();
            return new ShitHappenedHandler(eventAggregator);
        }
    }
}
