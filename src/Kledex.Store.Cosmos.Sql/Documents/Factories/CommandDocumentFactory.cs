using Kledex.Configuration;
using Kledex.Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kledex.Store.Cosmos.Sql.Documents.Factories
{
    public class CommandDocumentFactory : ICommandDocumentFactory
    {
        private readonly MainOptions _mainOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        private bool SaveCommandData(IDomainCommand command) => command.SaveCommandData ?? _mainOptions.SaveCommandData;

        public CommandDocumentFactory(IOptions<MainOptions> mainOptions, JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings;
            _mainOptions = mainOptions.Value;
        }

        public CommandDocument CreateCommand(IDomainCommand command)
        {
            return new CommandDocument
            {
                Id = command.Id,
                AggregateId = command.AggregateRootId,
                Type = command.GetType().AssemblyQualifiedName,
                Data = SaveCommandData(command) ? JsonConvert.SerializeObject(command, _serializerSettings) : null,
                TimeStamp = command.TimeStamp,
                UserId = command.UserId,
                Source = command.Source
            };
        }
    }
}