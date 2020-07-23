﻿using System;
using Kledex.Domain;
using Newtonsoft.Json;

namespace Kledex.Store.Cosmos.Mongo.Documents.Factories
{
    public class EventDocumentFactory : IEventDocumentFactory
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public EventDocumentFactory(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        public EventDocument CreateEvent(IDomainEvent @event, long version)
        {
            return new EventDocument
            {
                Id = Guid.NewGuid().ToString(),
                AggregateId = @event.AggregateRootId.ToString(),
                CommandId = @event.CommandId.ToString(),
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