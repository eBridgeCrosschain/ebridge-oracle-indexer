using AeFinder.Sdk.Processor;
using AElf.CSharp.Core;
using EbridgeServerIndexer;
using Volo.Abp.ObjectMapping;

namespace OracleIndexer.Processors.Oracle;

public abstract class OracleProcessorBase<TEvent> : LogEventProcessorBase<TEvent>
    where TEvent : IEvent<TEvent>, new()
{
    protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetRequiredService<IObjectMapper>();

    public override string GetContractAddress(string chainId)
    {
        return chainId switch
        {
            OracleConst.AELF => OracleConst.OracleContractAddress,
            OracleConst.tDVV => OracleConst.OracleContractAddressTDVV,
            OracleConst.tDVW => OracleConst.OracleContractAddressTDVW,
            _ => string.Empty
        };
    }

}