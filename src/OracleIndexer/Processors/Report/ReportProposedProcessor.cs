using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using EBridge.Contracts.Report;
using EbridgeServerIndexer;
using OracleIndexer.Entities;

namespace OracleIndexer.Processors.Report;

public class ReportProposedProcessor: ReportProcessorBase<ReportProposed>
{
    public override async Task ProcessAsync(ReportProposed logEvent, LogEventContext context)
    {
        Logger.LogInformation(
            "ReportProposedProcessor start, blockHeight:{Height}, blockHash:{Hash}, txId:{txId}",
            context.Block.BlockHeight,
            context.Block.BlockHash,
            context.Transaction.TransactionId);
        var id= IdGenerateHelper.GetId(context.ChainId, context.Transaction.TransactionId, "ReportProposed");
        var reportInfo = new ReportInfoIndex()
        {
            Id = id,
            Step = ReportStep.Proposed
        };
        ObjectMapper.Map(logEvent, reportInfo);
        await SaveEntityAsync(reportInfo);
    }
}