using AeFinder.Sdk;
using AElf;
using EBridge.Contracts.Oracle;
using OracleIndexer.Entities;
using OracleIndexer.GraphQL;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;
using QueryInput = OracleIndexer.GraphQL.QueryInput;

namespace OracleIndexer.Processors.Oracle;

public class SufficientCommitmentsCollectedProcessorTests : OracleIndexerTestBase
{
    private readonly SufficientCommitmentsCollectedProcessor _sufficientCommitmentsCollectedProcessor;
    private readonly IReadOnlyRepository<OracleQueryInfoIndex> _repository;
    private readonly IObjectMapper _objectMapper;

    public SufficientCommitmentsCollectedProcessorTests()
    {
        _sufficientCommitmentsCollectedProcessor = GetRequiredService<SufficientCommitmentsCollectedProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<OracleQueryInfoIndex>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        var logEvent = new SufficientCommitmentsCollected
        {
            QueryId = HashHelper.ComputeFrom("queryid"),
        };
        var logEventContext = GenerateLogEventContext(logEvent);
        await _sufficientCommitmentsCollectedProcessor.ProcessAsync(logEvent, logEventContext);

        var entities = await Query.OracleQueryInfo(_repository, _objectMapper, new QueryInput
        {
            ChainId = ChainId,
            StartBlockHeight = 0,
            EndBlockHeight = 100
        });
        entities.Count.ShouldBe(1);
        entities[0].BlockHeight.ShouldBe(100);
        entities[0].QueryId.ShouldBe(logEvent.QueryId.ToHex());
        entities[0].Step.ShouldBe(OracleStep.SufficientCommitmentsCollected);
    }
}