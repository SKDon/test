using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.App_Start.Jobs;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Calculation;
using Alicargo.Core.Contracts;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.Core.Contracts.Event;
using Alicargo.Core.Email;
using Alicargo.Core.Event;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories.Application;
using Alicargo.DataAccess.Repositories.User;
using Alicargo.Utilities;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace Alicargo.App_Start
{
	internal static class CompositionRoot
	{
		private const string AlicargoDataAccessDll = "Alicargo.DataAccess.dll";

		public static void BindServices(IKernel kernel, ILog mainLog)
		{
			kernel.Bind<ILog>().ToMethod(context => mainLog).InSingletonScope();

			kernel.Bind<IPasswordConverter>().To<PasswordConverter>().InThreadScope();

			kernel.Bind<IExcelClientCalculation>().To<ExcelClientCalculation>().InThreadScope();

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