using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.Core.Services.Abstract;
using Alicargo.Core.Services.Email;
using Alicargo.DataAccess.DbContext;
using Alicargo.DataAccess.Repositories;
using Alicargo.Services.Email;
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
			kernel.Bind<ISerializer>().To<Serializer>().InThreadScope();
			kernel.Bind<IMailSender>()
				.To<DbMailSender>()
				.InSingletonScope()
				.WithConstructorArgument("partitionId", CompositionJobsHelper.PartitionIdForOtherMails);
			kernel.Bind<ILocalizationService>().To<LocalizationService>().InRequestScope();

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

		public static void BindDataAccess(IKernel kernel, Func<IContext, object> scope)
		{
			kernel.Bind(x => x.FromAssembliesMatching(AlicargoDataAccessDll)
				.IncludingNonePublicTypes()
				.Select(IsServiceType)
				.Excluding<SqlProcedureExecutor>()
				.BindDefaultInterface()
				.Configure(y => y.InScope(scope)));
		}

		public static void BindConnection(IKernel kernel, string connectionString, string filesConnectionString)
		{
			kernel.Bind<IDbConnection>()
				.ToMethod(x => new SqlConnection(connectionString))
				.InRequestScope()
				.OnDeactivation(x => x.Close());

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.When(request => request.ParentRequest.Service == typeof (IAuthenticationRepository)
				                 || request.ParentRequest.Service == typeof (ISenderRepository)
				                 || request.ParentRequest.Service == typeof (IApplicationEventRepository)
								 || request.ParentRequest.Service == typeof(IEmailMessageRepository)
				)
				.InSingletonScope()
				.Named("MainDb")
				.WithConstructorArgument("connectionString", connectionString);

			kernel.Bind<ISqlProcedureExecutor>()
				.To<SqlProcedureExecutor>()
				.WhenInjectedInto<ClientFileRepository>()
				.InSingletonScope()
				.Named("FilesDb")
				.WithConstructorArgument("connectionString", filesConnectionString);
		}
	}
}