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

public class QueryCompletedWithAggregationProcessorTests : OracleIndexerTestBase
{
    private readonly QueryCompletedWithAggregationProcessor _queryCompletedWithAggregation;
    private readonly IReadOnlyRepository<OracleQueryInfoIndex> _repository;
    private readonly IObjectMapper _objectMapper;

    public QueryCompletedWithAggregationProcessorTests()
    {
        _queryCompletedWithAggregation = GetRequiredService<QueryCompletedWithAggregationProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<OracleQueryInfoIndex>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        var logEvent = new QueryCompletedWithAggregation
        {
            QueryId = HashHelper.ComputeFrom("queryid"),
            Result = "result"
        };
        var logEventContext = GenerateLogEventContext(logEvent);
        await _queryCompletedWithAggregation.ProcessAsync(logEvent, logEventContext);

        var entities = await Query.OracleQueryInfo(_repository, _objectMapper, new QueryInput
        {
            ChainId = ChainId,
            StartBlockHeight = 0,
            EndBlockHeight = 100
        });
        entities.Count.ShouldBe(1);
        entities[0].BlockHeight.ShouldBe(100);
        entities[0].QueryId.ShouldBe(logEvent.QueryId.ToHex());
    }
}