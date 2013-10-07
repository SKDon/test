using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Services;
using Alicargo.Services;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace Alicargo.App_Start
{
	internal static class CompositionRoot
	{
		private const string AlicargoDataAccessDll = "Alicargo.DataAccess.dll";

		public static void BindServices(IKernel kernel)
		{
			kernel.Bind<ILog>().ToMethod(context => new Log4NetWrapper(log4net.LogManager.GetLogger("Logger"))).InSingletonScope();

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

		public static void BindDataAccess(IKernel kernel, string connectionString, Func<IContext, object> scope)
		{
			kernel.Bind(x => x.FromAssembliesMatching(AlicargoDataAccessDll)
							  .IncludingNonePublicTypes()
							  .Select(IsServiceType)
							  .BindDefaultInterface()
							  .Configure(y => y.InScope(scope)));
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