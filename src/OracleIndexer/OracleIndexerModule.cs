using AeFinder.Sdk.Processor;
using OracleIndexer.GraphQL;
using OracleIndexer.Processors;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using OracleIndexer.Processors.Oracle;
using OracleIndexer.Processors.Report;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace OracleIndexer;

public class OracleIndexerModule: AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<OracleIndexerModule>(); });
        context.Services.AddSingleton<ISchema, AeIndexerSchema>();
        
        // Add your LogEventProcessor implementation.
        context.Services.AddSingleton<ILogEventProcessor, CommitmentRevealedProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, CommittedProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, QueryCompletedWithAggregationProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, QueryCompletedWithoutAggregationProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, QueryCreatedProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, SufficientCommitmentsCollectedProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, ReportConfirmedProcessor>();
        context.Services.AddSingleton<ILogEventProcessor, ReportProposedProcessor>();
    }
}