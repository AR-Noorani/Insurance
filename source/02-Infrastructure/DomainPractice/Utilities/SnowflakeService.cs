using DomainServices.Utilities;
using IdGen;

namespace DomainPractice.Utilities
{
    internal sealed class SnowflakeService : ISnowflakeService
    {
        private readonly IdGenerator _snowflake;

        internal SnowflakeService(SnowflakeOptions options)
        {
            var (timestampBits, generatorIdBits, sequenceBits) = options.IdStructure;
            var structure = new IdStructure(timestampBits, generatorIdBits, sequenceBits);
            var idGenOptions = new IdGeneratorOptions(structure, new DefaultTimeSource(options.Epoch));

            _snowflake = new IdGenerator(options.GeneratorId, idGenOptions);
        }

        long ISnowflakeService.CreateId()
        {
            return _snowflake.CreateId();
        }
    }
}
