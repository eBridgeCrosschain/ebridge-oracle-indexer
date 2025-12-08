using Oracle.Indexer.Entities;

namespace Oracle.Indexer.GraphQL;

public class ReportInfoDto : GraphQLDto
{
    public long RoundId { get; set; }
    public string Token { get; set; }
    public string TargetChainId { get; set; }
    public ReportStep Step { get; set; }
    public string RawReport { get; set; }
    public string Signature { get; set; }
    public bool IsAllNodeConfirmed { get; set; }
    public OffChainQueryInfoDto QueryInfo { get; set; }
}

public class OffChainQueryInfoDto
{
    public string Title { get; set; }
    public List<string> Options { get; set; }
}