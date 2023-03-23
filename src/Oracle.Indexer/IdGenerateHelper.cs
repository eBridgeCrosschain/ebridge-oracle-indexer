using System.Collections.Generic;

namespace Oracle.Indexer;

public static class IdGenerateHelper
{
    public static string GetId(params object[] inputs)
    {
        return inputs.JoinAsString("-");
    }
}