using AElf.Contracts.Report;
using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.Indexer.Entities;
using Volo.Abp.ObjectMapping;

namespace Oracle.Indexer.Processors.Report;

public class ReportProposedProcessor: ReportProcessorBase<ReportProposed>
{
    public ReportProposedProcessor(ILogger<ReportProposedProcessor> logger, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<ReportInfoIndex, LogEventInfo> repository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions)
        : base(logger, objectMapper, repository, contractInfoOptions)
    {
    }
    
    protected override async Task HandleEventAsync(ReportProposed eventValue, LogEventContext context)
    {
        var id = GetReportInfoId(context);
        var reportInfo = new ReportInfoIndex()
        {
            Id = id,
            Step = ReportStep.Proposed,
            ReceiptId = eventValue.QueryInfo.Title.Split("_")[2]
        };
        ObjectMapper.Map(context, reportInfo);
        ObjectMapper.Map(eventValue, reportInfo);
        await Repository.AddOrUpdateAsync(reportInfo);
    }
}