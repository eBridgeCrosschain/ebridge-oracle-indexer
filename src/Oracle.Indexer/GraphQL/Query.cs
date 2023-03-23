using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AElfIndexer.Client;
using AElfIndexer.Client.Providers;
using AElfIndexer.Grains;
using AElfIndexer.Grains.Grain.Client;
using AElfIndexer.Grains.State.Client;
using GraphQL;
using Nest;
using Oracle.Indexer.Entities;
using Orleans;
using IObjectMapper = Volo.Abp.ObjectMapping.IObjectMapper;

namespace Oracle.Indexer.GraphQL;

public class Query
{
    public static async Task<List<ReportInfoDto>> ReportInfo(
        [FromServices] IAElfIndexerClientEntityRepository<ReportInfoIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, QueryDto dto)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<ReportInfoIndex>, QueryContainer>>();

        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(dto.ChainId)));
        if (dto.StartBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).GreaterThanOrEquals(dto.StartBlockHeight)));
        }
        if (dto.EndBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).LessThanOrEquals(dto.EndBlockHeight)));
        }

        QueryContainer Filter(QueryContainerDescriptor<ReportInfoIndex> f) => f.Bool(b => b.Must(mustQuery));

        var result = await repository.GetListAsync(Filter, sortExp: k => k.BlockHeight,
            sortType: SortOrder.Ascending);
        return objectMapper.Map<List<ReportInfoIndex>, List<ReportInfoDto>>(result.Item2);
    }

    public static async Task<List<OracleQueryInfoDto>> OracleQueryInfo(
        [FromServices] IAElfIndexerClientEntityRepository<OracleQueryInfoIndex, LogEventInfo> repository,
        [FromServices] IObjectMapper objectMapper, QueryDto dto)
    {
        var mustQuery = new List<Func<QueryContainerDescriptor<OracleQueryInfoIndex>, QueryContainer>>();

        mustQuery.Add(q => q.Term(i => i.Field(f => f.ChainId).Value(dto.ChainId)));
        if (dto.StartBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).GreaterThanOrEquals(dto.StartBlockHeight)));
        }
        if (dto.EndBlockHeight > 0)
        {
            mustQuery.Add(q => q.Range(i => i.Field(f => f.BlockHeight).LessThanOrEquals(dto.EndBlockHeight)));
        }

        QueryContainer Filter(QueryContainerDescriptor<OracleQueryInfoIndex> f) => f.Bool(b => b.Must(mustQuery));

        var result = await repository.GetListAsync(Filter, sortExp: k => k.BlockHeight,
            sortType: SortOrder.Ascending);
        return objectMapper.Map<List<OracleQueryInfoIndex>, List<OracleQueryInfoDto>>(result.Item2);
    }
    
    public static async Task<SyncStateDto> SyncState(
        [FromServices] IClusterClient clusterClient, [FromServices] IAElfIndexerClientInfoProvider clientInfoProvider,
        [FromServices] IObjectMapper objectMapper, GetSyncStateDto dto)
    {
        var version = clientInfoProvider.GetVersion();
        var clientId = clientInfoProvider.GetClientId();
        var blockStateSetInfoGrain =
            clusterClient.GetGrain<IBlockStateSetInfoGrain>(
                GrainIdHelper.GenerateGrainId("BlockStateSetInfo", clientId, dto.ChainId, version));
        var confirmedHeight = await blockStateSetInfoGrain.GetConfirmedBlockHeight(dto.FilterType);
        return new SyncStateDto
        {
            ConfirmedBlockHeight = confirmedHeight
        };
    }
}