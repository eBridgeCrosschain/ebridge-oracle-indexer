using AeFinder.Sdk.Entities;
using Nest;

namespace OracleIndexer.Entities;

public class OracleQueryInfoIndex : AeFinderEntity, IAeFinderEntity
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