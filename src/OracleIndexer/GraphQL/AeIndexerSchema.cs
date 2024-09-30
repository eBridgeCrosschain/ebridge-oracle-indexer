using AeFinder.Sdk;

namespace OracleIndexer.GraphQL;

public class AeIndexerSchema : AppSchema<Query>
{
    public AeIndexerSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}