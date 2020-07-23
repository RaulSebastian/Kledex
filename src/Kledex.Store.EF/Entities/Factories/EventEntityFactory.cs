using Kledex.Domain;
using Newtonsoft.Json;

namespace Kledex.Store.EF.Entities.Factories
{
    public class EventEntityFactory : IEventEntityFactory
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public EventEntityFactory(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        public EventEntity CreateEvent(IDomainEvent @event, int version)
        {
            return new EventEntity
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