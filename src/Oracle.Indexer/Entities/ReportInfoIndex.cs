using AElf.Indexing.Elasticsearch;
using Nest;

namespace Oracle.Indexer.Entities;

public class ReportInfoIndex : OracleIndexerEntity<string>, IIndexBuild
{
    public long RoundId { get; set; }
    [Keyword]
    public string Token { get; set; }
    [Keyword]
    public string TargetChainId { get; set; }
    public ReportStep Step { get; set; }
    [Keyword]
    public string RawReport { get; set; }
    [Keyword]
    public string Signature { get; set; }
    public bool IsAllNodeConfirmed { get; set; }
    
}

public enum ReportStep
{
    Proposed = 0,
    Confirmed = 1
}