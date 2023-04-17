using AElf.Contracts.Oracle;
using AElf.Contracts.Report;
using AElf.Types;
using AElfIndexer.Client.Handlers;
using AutoMapper;
using Oracle.Indexer.Entities;
using Oracle.Indexer.GraphQL;

namespace Oracle.Indexer;

public class OracleIndexerMapperProfile:Profile
{
    public OracleIndexerMapperProfile()
    {
        // Common
        CreateMap<Hash, string>().ConvertUsing(s => s == null ? null : s.ToHex());
        CreateMap<Address, string>().ConvertUsing(s => s.ToBase58());
        
        // Query
        CreateMap<LogEventContext, OracleQueryInfoIndex>();
        
        CreateMap<OracleQueryInfoIndex, OracleQueryInfoDto>();
        CreateMap<Entities.QueryInfo, QueryInfoDto>();
        
        CreateMap<QueryCreated, OracleQueryInfoIndex>()
            .ForMember(d => d.DesignatedNodeList, opt => opt.MapFrom(o => o.DesignatedNodeList.Value.Select(o => o.ToBase58()).ToList()));
        CreateMap<CommitmentRevealed, OracleQueryInfoIndex>();
        CreateMap<Committed, OracleQueryInfoIndex>();
        CreateMap<QueryCompletedWithAggregation, OracleQueryInfoIndex>();
        CreateMap<QueryCompletedWithoutAggregation, OracleQueryInfoIndex>();
        CreateMap<SufficientCommitmentsCollected, OracleQueryInfoIndex>();
        
        CreateMap<AElf.Contracts.Oracle.QueryInfo, Entities.QueryInfo>()
            .ForMember(d => d.Options, opt => opt.MapFrom(o => o.Options.ToList()));
        
        // Report
        CreateMap<LogEventContext, ReportInfoIndex>();
        CreateMap<ReportInfoIndex, ReportInfoDto>();
        CreateMap<ReportConfirmed, ReportInfoIndex>();
        CreateMap<ReportProposed, ReportInfoIndex>();
        
    }
}