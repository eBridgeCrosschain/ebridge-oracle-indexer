using AElf.Indexing.Elasticsearch;
using Nest;

namespace Oracle.Indexer.Entities;

public class OracleQueryInfoIndex : OracleIndexerEntity<string>, IIndexBuild
{
    [Keyword]
    public string QueryId { get; set; }
    public List<string> DesignatedNodeList { get; set; }
    public QueryInfo QueryInfo { get; set; }
    public OracleStep Step { get; set; }
}

public class QueryInfo
{
    [Keyword]
    public string Title { get; set; }
    [Keyword]
    public List<string> Options { get; set; }
}

public enum OracleStep
{
    QueryCreated,
    Committed,
    SufficientCommitmentsCollected,
    CommitmentRevealed,
    QueryCompleted
}