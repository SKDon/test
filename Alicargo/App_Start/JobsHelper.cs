using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using Alicargo.Jobs;
using Alicargo.Jobs.Calculation;
using Ninject;
using Ninject.Syntax;

namespace Alicargo.App_Start
{
	internal static class JobsHelper
	{
		private static readonly TimeSpan PausePeriod = TimeSpan.Parse(ConfigurationManager.AppSettings["JobPausePeriod"]);

		private const string JobRunnerAggregator = "JobRunnerAggregator";
		private const string CalculationMailerJob = "CalculationMailerJob";
		//private const string ClientExcelUpdaterJobRunner = "ClientExcelUpdaterJobRunner";
		//private const string ClientExcelUpdaterJob = "ClientExcelUpdaterJob";

		public static void RunJobs(IKernel kernel, CancellationTokenSource tokenSource)
		{
			Task.Factory.StartNew(() => kernel.Get<IJobRunner>(JobRunnerAggregator).Run(tokenSource), tokenSource.Token);
		}

		public static void BindJobs(IKernel kernel, string connectionString)
		{
			//kernel.Bind<IJob>().ToMethod(context =>
			//{
			//	return new CalculationMailerJob(new );
			//}).Named(CalculationMailerJob);

			BindStatelessJobRunner(kernel, CalculationMailerJob);

			kernel.Bind<IJobRunner>().ToMethod(context =>
			{
				var mailer = context.Kernel.Get<IJobRunner>(CalculationMailerJob);

				return new JobRunnerAggregator(mailer);
			}).InSingletonScope().Named(JobRunnerAggregator);
		}

		private static void BindStatelessJobRunner(IBindingRoot kernel, string jobName)
		{
			kernel.Bind<IJobRunner>().To<StatelessJobRunner>()
				  .InSingletonScope()
				  .Named(jobName)
				  .WithConstructorArgument("pausePeriod", PausePeriod)
				  .WithConstructorArgument("jobName", jobName);
		}
	}
}