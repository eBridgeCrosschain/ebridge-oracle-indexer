using AElfIndexer.Client;
using AElfIndexer.Client.Handlers;
using AElfIndexer.Grains.State.Client;
using Microsoft.Extensions.DependencyInjection;
using Oracle.Indexer.GraphQL;
using Oracle.Indexer.Processors.Oracle;
using Oracle.Indexer.Processors.Report;
using Volo.Abp.Modularity;

namespace Oracle.Indexer;

[DependsOn(typeof(AElfIndexerClientModule))]
public class OracleIndexerModule:AElfIndexerClientPluginBaseModule<OracleIndexerModule, OracleIndexerSchema, Query>
{
    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
        var configuration = serviceCollection.GetConfiguration();

        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, CommitmentRevealedProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, CommittedProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, QueryCompletedWithAggregationProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, QueryCompletedWithoutAggregationProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, QueryCreatedProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, SufficientCommitmentsCollectedProcessor>();
        
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, ReportConfirmedProcessor>();
        serviceCollection.AddSingleton<IAElfLogEventProcessor<LogEventInfo>, ReportProposedProcessor>();

        Configure<ContractInfoOptions>(configuration.GetSection("ContractInfo"));
    }

    protected override string ClientId => "AElfIndexer_Oracle";
    protected override string Version => "65d229925bcf426d8c38c501d886853f";
}