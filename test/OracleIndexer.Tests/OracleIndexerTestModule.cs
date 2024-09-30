using AeFinder.App.TestBase;
using Microsoft.Extensions.DependencyInjection;
using OracleIndexer.Processors.Oracle;
using OracleIndexer.Processors.Report;
using Volo.Abp.Modularity;

namespace OracleIndexer;

[DependsOn(
    typeof(AeFinderAppTestBaseModule),
    typeof(OracleIndexerModule))]
public class OracleIndexerTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AeFinderAppEntityOptions>(options => { options.AddTypes<OracleIndexerModule>(); });
        
        // Add your Processors.
        //CommitmentRevealedProcessor
        context.Services.AddSingleton<CommitmentRevealedProcessor>();
        //CommittedProcessor
        context.Services.AddSingleton<CommittedProcessor>();
        //QueryCompletedWithAggregationProcessor
        context.Services.AddSingleton<QueryCompletedWithAggregationProcessor>();
        //QueryCompletedWithoutAggregationProcessor
        context.Services.AddSingleton<QueryCompletedWithoutAggregationProcessor>();
        //QueryCreatedProcessor
        context.Services.AddSingleton<QueryCreatedProcessor>();
        //SufficientCommitmentsCollectedProcessor
        context.Services.AddSingleton<SufficientCommitmentsCollectedProcessor>();
        //ReportConfirmedProcessor
        context.Services.AddSingleton<ReportConfirmedProcessor>();
        //ReportProposedProcessor
        context.Services.AddSingleton<ReportProposedProcessor>();
    }
}