using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Contracts.Repositories;
using Alicargo.Core.Services;
using Alicargo.DataAccess.DbContext;
using Alicargo.Services;
using log4net;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using ILog = Alicargo.Core.Services.ILog;

namespace Alicargo.App_Start
{
	internal static class CompositionRoot
	{
		private const string AlicargoDataAccessDll = "Alicargo.DataAccess.dll";

		public static void BindServices(IKernel kernel)
		{
			kernel.Bind<ILog>().ToMethod(context => new Log4NetWrapper(LogManager.GetLogger("Logger"))).InSingletonScope();

			kernel.Bind<IPasswordConverter>().To<PasswordConverter>().InThreadScope();

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
				  .InSingletonScope()
				  .WithConstructorArgument("connectionString", filesConnectionString);
		}
	}
}