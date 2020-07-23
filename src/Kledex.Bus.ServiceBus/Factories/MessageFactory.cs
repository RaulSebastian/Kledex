﻿using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace Kledex.Bus.ServiceBus.Factories
{
    public class MessageFactory : IMessageFactory
    {
        public static readonly string AssemblyQualifiedNamePropertyName = "AssemblyQualifiedName";
        private readonly JsonSerializerSettings _serializerSettings;

        public MessageFactory(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
        }

        /// <inheritdoc />
        public Message CreateMessage<TMessage>(TMessage message) where TMessage : IBusMessage
        {
            var json = JsonConvert.SerializeObject(message, _serializerSettings);
            var serviceBusMessage = new Message(Encoding.UTF8.GetBytes(json))
            {
                ContentType = "application/json"
            };

            if (message.Properties != null)
            {
                foreach (var prop in message.Properties)
                {
                    var serviceBusMessageProp = serviceBusMessage.GetType().GetProperty(prop.Key);

                    if (serviceBusMessageProp != null && serviceBusMessageProp.CanWrite)
                    {
                        serviceBusMessageProp.SetValue(serviceBusMessage, message.Properties[prop.Key], null);
                    }
                    else
                    {
                        serviceBusMessage.UserProperties.Add(prop.Key, prop.Value);
                    }
                }
            }

            serviceBusMessage.UserProperties.Add(new KeyValuePair<string, object>(AssemblyQualifiedNamePropertyName, message.GetType().AssemblyQualifiedName));

            return serviceBusMessage;
        }
    }
}