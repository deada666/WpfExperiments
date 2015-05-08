using Microsoft.Practices.Prism.PubSubEvents;
using System;

namespace WpfExperiments.Events
{
    public class ShitHappenedEvent : PubSubEvent<Exception>
    {
    }
}
