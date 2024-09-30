using AeFinder.Sdk;
using OracleIndexer.Entities;
using GraphQL;
using Volo.Abp.ObjectMapping;

namespace OracleIndexer.GraphQL;

public class Query
{
    public static async Task<List<ReportInfoDto>> ReportInfo(
        [FromServices] IReadOnlyRepository<ReportInfoIndex> repository,
        [FromServices] IObjectMapper objectMapper,
        QueryInput input)
    {
        var queryable = await repository.GetQueryableAsync();

        queryable = queryable.Where(a => a.Metadata.ChainId == input.ChainId);

        if (input.StartBlockHeight > 0)
        {
            queryable = queryable.Where(a => a.Metadata.Block.BlockHeight >= input.StartBlockHeight);
        }

        if (input.EndBlockHeight > 0)
        {
            queryable = queryable.Where(a => a.Metadata.Block.BlockHeight <= input.EndBlockHeight);
        }

        var result = queryable.OrderBy(o => o.Metadata.Block.BlockHeight).ThenBy(o => o.IsAllNodeConfirmed).Take(input.MaxMaxResultCount).ToList();
        return objectMapper.Map<List<ReportInfoIndex>, List<ReportInfoDto>>(result);
    }
    
    public static async Task<List<OracleQueryInfoDto>> OracleQueryInfo(
        [FromServices] IReadOnlyRepository<OracleQueryInfoIndex> repository,
        [FromServices] IObjectMapper objectMapper,
        QueryInput input)
    {
        input.Validate();
        var queryable = await repository.GetQueryableAsync();

        queryable = queryable.Where(a => a.Metadata.ChainId == input.ChainId);

        if (input.StartBlockHeight > 0)
        {
            queryable = queryable.Where(a => a.Metadata.Block.BlockHeight >= input.StartBlockHeight);
        }

        if (input.EndBlockHeight > 0)
        {
            queryable = queryable.Where(a => a.Metadata.Block.BlockHeight <= input.EndBlockHeight);
        }

        var result = queryable.OrderBy(o => o.Metadata.Block.BlockHeight).Take(input.MaxMaxResultCount).ToList();
        return objectMapper.Map<List<OracleQueryInfoIndex>, List<OracleQueryInfoDto>>(result);
    }
}