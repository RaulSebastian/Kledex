using Kledex.Domain;
using Newtonsoft.Json;

namespace Kledex.Store.Cosmos.Sql.Documents.Factories
{
    public class EventDocumentFactory : IEventDocumentFactory
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public EventDocumentFactory(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        public EventDocument CreateEvent(IDomainEvent @event, int version)
        {
            return new EventDocument
            {
                Id = @event.Id,
                AggregateId = @event.AggregateRootId,
                CommandId = @event.CommandId,
                Sequence = version,
                Type = @event.GetType().AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(@event, _serializerSettings),
                TimeStamp = @event.TimeStamp,
                UserId = @event.UserId,
                Source = @event.Source
            };
        }
    }
}