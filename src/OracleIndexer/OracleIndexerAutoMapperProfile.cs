using AeFinder.Sdk.Processor;
using EBridge.Contracts.Oracle;
using EBridge.Contracts.Report;
using AElf.Types;
using OracleIndexer.Entities;
using OracleIndexer.GraphQL;
using AutoMapper;

namespace OracleIndexer;

public class OracleIndexerAutoMapperProfile : Profile
{
    public OracleIndexerAutoMapperProfile()
    {
        // Common
        CreateMap<Hash, string>().ConvertUsing(s => s == null ? null : s.ToHex());
        CreateMap<Address, string>().ConvertUsing(s => s.ToBase58());
        
        // Query
        CreateMap<OracleQueryInfoIndex, OracleQueryInfoDto>()
            .ForMember(d=>d.BlockHash, opt=>opt.MapFrom(o=>o.Metadata.Block.BlockHash))
            .ForMember(d=>d.BlockHeight, opt=>opt.MapFrom(o=>o.Metadata.Block.BlockHeight))
            .ForMember(d=>d.BlockTime, opt=>opt.MapFrom(o=>o.Metadata.Block.BlockTime))
            .ForMember(d=>d.ChainId, opt=>opt.MapFrom(o=>o.Metadata.ChainId));

        CreateMap<Entities.QueryInfo, QueryInfoDto>();
        
        CreateMap<QueryCreated, OracleQueryInfoIndex>()
            .ForMember(d => d.DesignatedNodeList, opt => opt.MapFrom(o => o.DesignatedNodeList.Value.Select(o => o.ToBase58()).ToList()));
        CreateMap<CommitmentRevealed, OracleQueryInfoIndex>();
        CreateMap<Committed, OracleQueryInfoIndex>();
        CreateMap<QueryCompletedWithAggregation, OracleQueryInfoIndex>();
        CreateMap<QueryCompletedWithoutAggregation, OracleQueryInfoIndex>();
        CreateMap<SufficientCommitmentsCollected, OracleQueryInfoIndex>();
        
        CreateMap<EBridge.Contracts.Oracle.QueryInfo, Entities.QueryInfo>()
            .ForMember(d => d.Options, opt => opt.MapFrom(o => o.Options.ToList()));
        
        // Report
        CreateMap<ReportInfoIndex, ReportInfoDto>()
            .ForMember(d=>d.BlockHash, opt=>opt.MapFrom(o=>o.Metadata.Block.BlockHash))
            .ForMember(d=>d.BlockHeight, opt=>opt.MapFrom(o=>o.Metadata.Block.BlockHeight))
            .ForMember(d=>d.BlockTime, opt=>opt.MapFrom(o=>o.Metadata.Block.BlockTime))
            .ForMember(d=>d.ChainId, opt=>opt.MapFrom(o=>o.Metadata.ChainId));

        CreateMap<ReportConfirmed, ReportInfoIndex>();
        CreateMap<ReportProposed, ReportInfoIndex>();
        CreateMap<EBridge.Contracts.Report.OffChainQueryInfo, Entities.OffChainQueryInfo>()
            .ForMember(d => d.Options, opt => opt.MapFrom(o => o.Options.ToList()));
        CreateMap<Entities.OffChainQueryInfo, OffChainQueryInfoDto>();
    }
}