using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity.InterceptionExtension;
using WpfExperiments.Events;

namespace WpfExperiments.Interceptors
{
    public class ShitHappenedHandler : ICallHandler
    {
        private readonly IEventAggregator _eventAggregator;

        public ShitHappenedHandler(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            var result = getNext().Invoke(input, getNext);
            if (result.Exception != null)
            {
                _eventAggregator.GetEvent<ShitHappenedEvent>().Publish(result.Exception);
                result.Exception = null;
            }

            return result;
        }

        public int Order { get; set; }
    }
}
