using AeFinder.Sdk.Logging;
using AeFinder.Sdk.Processor;
using EBridge.Contracts.Oracle;
using EbridgeServerIndexer;
using OracleIndexer.Entities;

namespace OracleIndexer.Processors.Oracle;

public class QueryCreatedProcessor: OracleProcessorBase<QueryCreated>
{
    public override async Task ProcessAsync(QueryCreated logEvent, LogEventContext context)
    {
        Logger.LogInformation(
            "QueryCreatedProcessor start, blockHeight:{Height}, blockHash:{Hash}, txId:{txId}",
            context.Block.BlockHeight,
            context.Block.BlockHash,
            context.Transaction.TransactionId);
        var id = IdGenerateHelper.GetId(context.ChainId, context.Transaction.TransactionId, "QueryCreated");
        var info = new OracleQueryInfoIndex()
        {
            Id = id,
            Step = OracleStep.QueryCreated
        };
        ObjectMapper.Map(logEvent, info);
        await SaveEntityAsync(info);
    }
}