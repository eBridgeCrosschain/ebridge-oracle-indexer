using AElfIndexer.Client;

namespace Oracle.Indexer.Entities;

public class OracleIndexerEntity<T> : AElfIndexerClientEntity<T>
{
    public DateTime BlockTime { get; set; }
}