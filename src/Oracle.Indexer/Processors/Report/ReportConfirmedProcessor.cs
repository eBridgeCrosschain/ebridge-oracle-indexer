using AElf.Contracts.Report;
using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.Indexer.Entities;
using Volo.Abp.ObjectMapping;

namespace Oracle.Indexer.Processors.Report;

public class ReportConfirmedProcessor: ReportProcessorBase<ReportConfirmed>
{
    private readonly ILogger<ReportConfirmedProcessor> _logger;
    
    public ReportConfirmedProcessor(ILogger<ReportConfirmedProcessor> logger, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<ReportInfoIndex, LogEventInfo> repository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions)
        : base(logger, objectMapper, repository, contractInfoOptions)
    {
        _logger = logger;
    }

    protected override async Task HandleEventAsync(ReportConfirmed eventValue, LogEventContext context)
    {
        var id = GetReportInfoId(context);

        _logger.LogInformation("ReportConfirmedProcessor Signature: {Signature}", eventValue.Signature);
        var reportInfo = new ReportInfoIndex
        {
            Id = id,
            Step = ReportStep.Confirmed
        };
        ObjectMapper.Map(context, reportInfo);
        ObjectMapper.Map(eventValue, reportInfo);

        await Repository.AddOrUpdateAsync(reportInfo);
    }
}