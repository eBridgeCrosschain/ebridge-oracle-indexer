using AeFinder.Sdk.Entities;
using Nest;

namespace OracleIndexer.Entities;

public class ReportInfoIndex : AeFinderEntity, IAeFinderEntity
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
    public OffChainQueryInfo QueryInfo { get; set; }
}

public enum ReportStep
{
    Proposed = 0,
    Confirmed = 1
}

public class OffChainQueryInfo
{
    public string Title { get; set; }
    public List<string> Options { get; set; }
}