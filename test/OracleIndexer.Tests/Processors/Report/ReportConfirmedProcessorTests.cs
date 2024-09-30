using AeFinder.Sdk;
using AElf;
using EBridge.Contracts.Report;
using OracleIndexer.Entities;
using OracleIndexer.GraphQL;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace OracleIndexer.Processors.Report;

public class ReportConfirmedProcessorTests : OracleIndexerTestBase
{
    private readonly ReportConfirmedProcessor _reportConfirmedProcessor;
    private readonly IReadOnlyRepository<ReportInfoIndex> _repository;
    private readonly IObjectMapper _objectMapper;

    public ReportConfirmedProcessorTests()
    {
        _reportConfirmedProcessor = GetRequiredService<ReportConfirmedProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<ReportInfoIndex>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        var logEvent = new ReportConfirmed
        {
            RoundId = 1,
            Signature = "signature",
            RegimentId = HashHelper.ComputeFrom("regiment_id"),
            IsAllNodeConfirmed = true,
            TargetChainId = "target_chain_id",
            Token = "token"
        };
        var logEventContext = GenerateLogEventContext(logEvent);
        await _reportConfirmedProcessor.ProcessAsync(logEvent, logEventContext);
        
        var logEvent1 = new ReportConfirmed
        {
            RoundId = 1,
            Signature = "signature",
            RegimentId = HashHelper.ComputeFrom("regiment_id"),
            IsAllNodeConfirmed = false,
            TargetChainId = "target_chain_id",
            Token = "token"
        };
        var logEventContext1 = GenerateLogEventContext(logEvent1);
        logEventContext1.Transaction.TransactionId = "f76b5f03abc4c7392603caaf2181624b9d2c414161446af2922fdead77d3e9a7";
        await _reportConfirmedProcessor.ProcessAsync(logEvent1, logEventContext1);

        var entities = await Query.ReportInfo(_repository, _objectMapper, new QueryInput
        {
            ChainId = ChainId,
            StartBlockHeight = 0,
            EndBlockHeight = 100
        });
        entities.Count.ShouldBe(2);
        entities[0].BlockHeight.ShouldBe(100);
        entities[0].RoundId.ShouldBe(logEvent.RoundId);
        entities[0].IsAllNodeConfirmed.ShouldBe(false);
        entities[1].IsAllNodeConfirmed.ShouldBe(true);
    }
}