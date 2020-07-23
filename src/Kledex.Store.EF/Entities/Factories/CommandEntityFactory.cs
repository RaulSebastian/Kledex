using Kledex.Configuration;
using Kledex.Domain;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kledex.Store.EF.Entities.Factories
{
    public class CommandEntityFactory : ICommandEntityFactory
    {
        private readonly MainOptions _mainOptions;
        private readonly JsonSerializerSettings _serializerSettings;

        private bool SaveCommandData(IDomainCommand command) => command.SaveCommandData ?? _mainOptions.SaveCommandData;

        public CommandEntityFactory(IOptions<MainOptions> mainOptions, JsonSerializerSettings serializerSettings)
        {
            _mainOptions = mainOptions.Value;
            _serializerSettings = serializerSettings;
        }

        public CommandEntity CreateCommand(IDomainCommand command)
        {
            return new CommandEntity
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