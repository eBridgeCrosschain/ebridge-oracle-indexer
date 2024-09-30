using AeFinder.Sdk;
using AElf;
using EBridge.Contracts.Oracle;
using OracleIndexer.Entities;
using OracleIndexer.GraphQL;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;
using QueryInfo = EBridge.Contracts.Oracle.QueryInfo;
using QueryInput = OracleIndexer.GraphQL.QueryInput;

namespace OracleIndexer.Processors.Oracle;

public class QueryCreatedProcessorTests : OracleIndexerTestBase
{
    private readonly QueryCreatedProcessor _queryCreatedProcessor;
    private readonly IReadOnlyRepository<OracleQueryInfoIndex> _repository;
    private readonly IObjectMapper _objectMapper;

    public QueryCreatedProcessorTests()
    {
        _queryCreatedProcessor = GetRequiredService<QueryCreatedProcessor>();
        _repository = GetRequiredService<IReadOnlyRepository<OracleQueryInfoIndex>>();
        _objectMapper = GetRequiredService<IObjectMapper>();
    }

    [Fact]
    public async Task Test()
    {
        var logEvent = new QueryCreated
        {
            QueryId = HashHelper.ComputeFrom("queryid"),
            QueryInfo = new QueryInfo
            {
                Title = "querytitle",
                Options = {"option.1", "option.4"}
            }
        };
        var logEventContext = GenerateLogEventContext(logEvent);
        await _queryCreatedProcessor.ProcessAsync(logEvent, logEventContext);

        var entities = await Query.OracleQueryInfo(_repository, _objectMapper, new QueryInput
        {
            ChainId = ChainId,
            StartBlockHeight = 0,
            EndBlockHeight = 100
        });
        entities.Count.ShouldBe(1);
        entities[0].BlockHeight.ShouldBe(100);
        entities[0].QueryId.ShouldBe(logEvent.QueryId.ToHex());
        entities[0].Step.ShouldBe(OracleStep.QueryCreated);
    }
}