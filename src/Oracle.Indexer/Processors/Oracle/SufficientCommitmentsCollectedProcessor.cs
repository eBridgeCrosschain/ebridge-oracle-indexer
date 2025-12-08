using AElf.Contracts.Oracle;
using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.Indexer.Entities;
using Volo.Abp.ObjectMapping;

namespace Oracle.Indexer.Processors.Oracle;

public class SufficientCommitmentsCollectedProcessor : OracleProcessorBase<SufficientCommitmentsCollected>
{
    public SufficientCommitmentsCollectedProcessor(ILogger<SufficientCommitmentsCollectedProcessor> logger,
        IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<OracleQueryInfoIndex, LogEventInfo> repository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions)
        : base(logger, objectMapper, repository, contractInfoOptions)
    {
    }

    protected override async Task HandleEventAsync(SufficientCommitmentsCollected eventValue, LogEventContext context)
    {
        var id = GetOracleInfoId(context);
        var info = new OracleQueryInfoIndex()
        {
            Id = id,
            Step = OracleStep.SufficientCommitmentsCollected
        };
        ObjectMapper.Map(context, info);
        ObjectMapper.Map(eventValue, info);

        await Repository.AddOrUpdateAsync(info);
    }
}