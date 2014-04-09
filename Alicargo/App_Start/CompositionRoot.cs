using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Common;
using Alicargo.Core.Contracts.Email;
using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Contracts.State;
using Alicargo.Core.Contracts.Users;
using Alicargo.Core.Email;
using Alicargo.Core.Event;
using Alicargo.Core.Excel.Client;
using Alicargo.Core.State;
using Alicargo.Core.Users;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.Jobs;
using Alicargo.Utilities;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace Alicargo
{
	internal static class CompositionRoot
	{
		private const string AlicargoDataAccessDll = "Alicargo.DataAccess.dll";

		public static void BindServices(IKernel kernel, ILog mainLog)
		{
			kernel.Bind<ILog>().ToMethod(context => mainLog).InSingletonScope();

			kernel.Bind<IPasswordConverter>().To<PasswordConverter>().InThreadScope();

			kernel.Bind<IExcelClientCalculation>().To<ExcelClientCalculation>().InRequestScope();

			kernel.Bind<IAwbGtdHelper>().To<AwbGtdHelper>().InRequestScope();

			kernel.Bind<IAwbManager>().To<AwbManager>().InRequestScope();

			kernel.Bind<IApplicationStateManager>().To<ApplicationStateManager>().InRequestScope();

			kernel.Bind<IAwbStateManager>().To<AwbStateManager>().InRequestScope();

			kernel.Bind<IStateConfig>().To<StateConfig>().InRequestScope();

			kernel.Bind<IStateFilter>().To<StateFilter>().InRequestScope();

			kernel.Bind<IForwarderService>().To<ForwarderService>().InRequestScope();

			kernel.Bind<IEventFacade>().To<EventFacade>().InSingletonScope();

			kernel.Bind<IPartitionConverter>().To<PartitionConverter>()
				.InSingletonScope()
				.WithConstructorArgument("partitionCount", CompositionJobsHelper.PartitionCount);

			kernel.Bind<ISerializer>().To<Serializer>().InThreadScope();

			kernel.Bind<IMailSender>()
				.To<DbMailSender>()
				.InSingletonScope()
				.WithConstructorArgument("partitionId", CompositionJobsHelper.PartitionIdForOtherMails);

			var binded = CompositionRootHelper.BindDecorators(kernel, CompositionRootHelper.Decorators);

			kernel.Bind(scanner => scanner.FromThisAssembly()
				.IncludingNonePublicTypes()
				.Select(IsServiceType)
				.Excluding(binded)
				.BindDefaultInterface()
				.Configure(binding => binding.InRequestScope()));
		}

		private static bool IsServiceType(Type type)
		{
			return type.IsClass && type.GetInterfaces().Any(intface => intface.Name == "I" + type.Name);
		}

		public static void BindDataAccess(IKernel kernel, string connectionString, string filesConnectionString,
			Func<IContext, object> scope)
		{
			kernel.Bind(x => x.FromAssembliesMatching(AlicargoDataAccessDll)
				.IncludingNonePublicTypes()
				.Select(IsServiceType)
				.Excluding<SqlProcedureExecutor>()
				.Excluding<ApplicationEditor>()
				.BindDefaultInterface()
				.Configure(y => y.InScope(scope)));

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", connectionString);

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.WhenInjectedInto<ClientFileRepository>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", filesConnectionString);

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.WhenInjectedInto<ApplicationFileRepository>()
				.InSingletonScope()
				.WithConstructorArgument("connectionString", filesConnectionString);
		}

		public static void BindConnection(IKernel kernel, string connectionString)
		{
			kernel.Bind<IDbConnection>()
				.ToMethod(x => new SqlConnection(connectionString))
				.InRequestScope()
				.OnDeactivation(x => x.Close());
		}
	}
}