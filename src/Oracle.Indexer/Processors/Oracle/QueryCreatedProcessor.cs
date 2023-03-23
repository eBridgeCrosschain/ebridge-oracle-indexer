using AElf.Contracts.Oracle;
using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.Indexer.Entities;
using Volo.Abp.ObjectMapping;

namespace Oracle.Indexer.Processors.Oracle;

public class QueryCreatedProcessor: OracleProcessorBase<QueryCreated>
{
    public QueryCreatedProcessor(ILogger<QueryCreatedProcessor> logger, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<OracleQueryInfoIndex, LogEventInfo> repository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions)
        : base(logger, objectMapper, repository, contractInfoOptions)
    {
    }

    protected override async Task HandleEventAsync(QueryCreated eventValue, LogEventContext context)
    {
        var id = GetOracleInfoId(context);
        var info = new OracleQueryInfoIndex()
        {
            Id = id,
            Step = OracleStep.QueryCreated,
            QueryId = eventValue.QueryId.ToHex(),
            DesignatedNodeList = eventValue.DesignatedNodeList.Value.Select(o => o.ToBase58()).ToList(),
            QueryInfo = new Entities.QueryInfo
            {
                Title = eventValue.QueryInfo.Title,
                Options = eventValue.QueryInfo.Options.ToList()
            }
        };
        ObjectMapper.Map<LogEventContext, OracleQueryInfoIndex>(context, info);

        await Repository.AddOrUpdateAsync(info);
    }
}