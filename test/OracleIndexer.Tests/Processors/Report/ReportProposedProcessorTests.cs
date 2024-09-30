using AeFinder.Sdk;
using AElf;
using EBridge.Contracts.Report;
using OracleIndexer.Entities;
using OracleIndexer.GraphQL;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace OracleIndexer.Processors.Report;

public class ReportProposedProcessorTests : OracleIndexerTestBase
{
    private readonly ReportProposedProcessor _reportProposedProcessor;
    private readonly IReadOnlyRepository<ReportInfoIndex> _repository;
    private readonly IObjectMapper _objectMapper;

    public ReportProposedProcessorTests()
    {
        _reportProposedProcessor = GetRequiredService<ReportProposedProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<ReportInfoIndex>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        var logEvent = new ReportProposed
        {
            RoundId = 1,
            RegimentId = HashHelper.ComputeFrom("regiment_id"),
            TargetChainId = "tDVW",
            Token = "token",
            RawReport = "raw_report",
            QueryInfo = new EBridge.Contracts.Report.OffChainQueryInfo
            {
                Title = "lock_token_receiptId",
                Options = { "option1","option2" }
            }
            
        };
        var logEventContext = GenerateLogEventContext(logEvent);
        await _reportProposedProcessor.ProcessAsync(logEvent, logEventContext);
        var entities = await Query.ReportInfo(_repository, _objectMapper, new QueryInput
        {
            ChainId = ChainId,
            StartBlockHeight = 0,
            EndBlockHeight = 100
        });
        entities.Count.ShouldBe(1);
        entities[0].BlockHeight.ShouldBe(100);
        entities[0].RoundId.ShouldBe(logEvent.RoundId);
        entities[0].Token.ShouldBe(logEvent.Token);
        entities[0].Step.ShouldBe(ReportStep.Proposed);
        entities[0].TargetChainId.ShouldBe("tDVW");
    }
}