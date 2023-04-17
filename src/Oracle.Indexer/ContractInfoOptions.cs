namespace Oracle.Indexer;

public class ContractInfoOptions
{
    public Dictionary<string,ContractInfo> ContractInfos { get; set; }
}

public class ContractInfo
{
    public string OracleContractAddress { get; set; }
    public string ReportContractAddress { get; set; }
}