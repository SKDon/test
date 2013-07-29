using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Alicargo.Core.Security;
using Alicargo.Services;
using Alicargo.Services.Abstract;
using Alicargo.Services.Email;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using log4net;

namespace Alicargo.App_Start
{
	public static class CompositionRoot
	{
		public static void BindServices(IKernel kernel)
		{
			kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(string.Empty)).InSingletonScope();

			kernel.Bind<IPasswordConverter>().To<PasswordConverter>().InThreadScope();

			// todo: auto binding for intersections
			kernel.Bind<IMailSender>().To<SilentMailSender>().InRequestScope();
			kernel.Bind<IMailSender>().To<MailSender>().WhenInjectedInto<SilentMailSender>().InRequestScope();

			kernel.Bind<IApplicationManager>().To<ApplicationManagerWithMailing>().InRequestScope();
			kernel.Bind<IApplicationManager>().To<ApplicationManager>().WhenInjectedInto<ApplicationManagerWithMailing>().InRequestScope();

			kernel.Bind<IAwbManager>().To<AwbManagerWithMailing>().InRequestScope();
			kernel.Bind<IAwbManager>().To<AwbManager>().WhenInjectedInto<AwbManagerWithMailing>().InRequestScope();

			kernel.Bind(scanner => scanner.FromThisAssembly()
										  .Select(IsServiceType)
										  .Excluding<MailSender>()
										  .Excluding<ApplicationManager>()
										  .Excluding<AwbManager>()
										  .BindDefaultInterface()
										  .Configure(binding => binding.InRequestScope()));
		}

		private static bool IsServiceType(Type type)
		{
			return type.IsClass
				&& !type.Name.EndsWith("WithMailing")
				&& type.GetInterfaces().Any(intface => intface.Name == "I" + type.Name);
		}

		public static void RegisterConfigs(IKernel kernel)
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, kernel);

			RouteConfig.RegisterRoutes(RouteTable.Routes);

			BinderConfig.RegisterBinders(System.Web.Mvc.ModelBinders.Binders);
		}

		public static void BindDataAccess(IKernel kernel, string connectionString)
		{
			kernel.Bind<IDbConnection>()
				  .ToMethod(x => new SqlConnection(connectionString))
				  .InRequestScope()
				  .OnDeactivation(x => x.Close());

			kernel.Bind(x => x.FromAssembliesMatching("Alicargo.DataAccess.dll")
							  .IncludingNonePublicTypes()
							  .Select(IsServiceType)
							  .BindDefaultInterface()
							  .Configure(y=>y.InRequestScope()));
		}
	}
}