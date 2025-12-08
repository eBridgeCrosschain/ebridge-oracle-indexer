using Oracle.Indexer.Entities;

namespace Oracle.Indexer.GraphQL;

public class OracleQueryInfoDto : GraphQLDto
{
    public string QueryId { get; set; }
    public List<string> DesignatedNodeList { get; set; }
    public QueryInfoDto QueryInfo { get; set; }
    public OracleStep Step { get; set; }
}

public class QueryInfoDto
{
    public string Title { get; set; }
    public List<string> Options { get; set; }
}