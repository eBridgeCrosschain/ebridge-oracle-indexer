using System;
using AElfIndexer.Client.GraphQL;

namespace Oracle.Indexer.GraphQL;

public class OracleIndexerSchema : AElfIndexerClientSchema<Query>
{
    public OracleIndexerSchema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}