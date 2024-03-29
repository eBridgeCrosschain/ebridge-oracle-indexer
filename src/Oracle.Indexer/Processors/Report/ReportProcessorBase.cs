using AElf.CSharp.Core;
using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oracle.Indexer.Entities;
using Volo.Abp.ObjectMapping;

namespace Oracle.Indexer.Processors.Report;

public abstract class ReportProcessorBase<TEvent>: AElfLogEventProcessorBase<TEvent,LogEventInfo>
    where TEvent : IEvent<TEvent>, new()
{
    protected readonly IObjectMapper ObjectMapper;
    protected readonly IAElfIndexerClientEntityRepository<ReportInfoIndex, LogEventInfo> Repository;
    protected readonly ContractInfoOptions ContractInfoOptions;

    protected ReportProcessorBase(ILogger<ReportProcessorBase<TEvent>> logger, IObjectMapper objectMapper,
        IAElfIndexerClientEntityRepository<ReportInfoIndex, LogEventInfo> repository,
        IOptionsSnapshot<ContractInfoOptions> contractInfoOptions)
        : base(logger)
    {
        ObjectMapper = objectMapper;
        Repository = repository;
        ContractInfoOptions = contractInfoOptions.Value;
    }

    public override string GetContractAddress(string chainId)
    {
        return ContractInfoOptions.ContractInfos[chainId].ReportContractAddress;
    }
    
    protected string GetReportInfoId(LogEventContext context)
    {
        return IdGenerateHelper.GetId(context.ChainId, context.TransactionId, typeof(TEvent).Name);
    }
}