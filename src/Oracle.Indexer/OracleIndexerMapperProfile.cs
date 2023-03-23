using AElf.Contracts.Bridge;
using AElf.Contracts.MultiToken;
using AElf.Contracts.Oracle;
using AElfIndexer.Client.Handlers;
using AutoMapper;
using Oracle.Indexer.Entities;
using Oracle.Indexer.GraphQL;
using QueryInfo = Oracle.Indexer.Entities.QueryInfo;

namespace Oracle.Indexer;

public class OracleIndexerMapperProfile:Profile
{
    public OracleIndexerMapperProfile()
    {
        CreateMap<LogEventContext, CommitmentRevealed>();
        CreateMap<LogEventContext, Committed>();
        CreateMap<LogEventContext, QueryCompletedWithAggregation>();
        CreateMap<LogEventContext, QueryCompletedWithoutAggregation>();
        CreateMap<LogEventContext, QueryCreated>();
        CreateMap<LogEventContext, SufficientCommitmentsCollected>();
        
        CreateMap<LogEventContext, ReportInfoIndex>();
        
        CreateMap<OracleQueryInfoIndex, OracleQueryInfoDto>();
        CreateMap<QueryInfo, QueryInfoDto>();
        CreateMap<ReportInfoIndex, ReportInfoDto>();
    }
}