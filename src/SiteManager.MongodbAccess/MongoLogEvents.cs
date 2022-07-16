using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver.Core.Events;

namespace SiteManager.MongodbAccess
{
    internal class MongoLogEvents : IEventSubscriber
    {
        private readonly ILogger _logger;
        private readonly ReflectionEventSubscriber _subscriber;

        private readonly Guid _category = Guid.NewGuid();  

        private Stopwatch _watch = new();

        public MongoLogEvents(ILogger logger)
        {
            _logger = logger;
            _subscriber = new ReflectionEventSubscriber(this);
        }

        public bool TryGetEventHandler<TEvent>(out Action<TEvent> handler)
        {
            return _subscriber.TryGetEventHandler(out handler);
        }

        public void Handle(CommandStartedEvent e)
        {
            _watch = Stopwatch.StartNew();
            _logger.LogInformation("{Category} command:{Json}", _category, e.ToJson());
        }

        public void Handle(CommandSucceededEvent e)
        {
            _watch.Stop();
            _logger.LogInformation("{Category} execution time:{Times} ms", _category, _watch.ElapsedMilliseconds);
        }
    }
}