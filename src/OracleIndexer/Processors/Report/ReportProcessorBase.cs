using AeFinder.Sdk.Processor;
using AElf.CSharp.Core;
using EbridgeServerIndexer;
using Volo.Abp.ObjectMapping;

namespace OracleIndexer.Processors.Report;

public abstract class ReportProcessorBase<TEvent> : LogEventProcessorBase<TEvent>
    where TEvent : IEvent<TEvent>, new()
{
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetRequiredService<IObjectMapper>();

    public override string GetContractAddress(string chainId)
    {
        return chainId switch
        {
            OracleConst.AELF => OracleConst.ReportContractAddress,
            OracleConst.tDVV => OracleConst.ReportContractAddressTDVV,
            OracleConst.tDVW => OracleConst.ReportContractAddressTDVW,
            _ => string.Empty
        };
    }
}