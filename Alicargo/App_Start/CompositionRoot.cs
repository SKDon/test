using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Alicargo.Contracts.Helpers;
using Alicargo.Core.Services;
using Alicargo.Services.Abstract;
using Alicargo.Services.Email;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using log4net;

namespace Alicargo.App_Start
{
    internal static class CompositionRoot
    {
        private const string WithMailingSufix = "WithMailing";
        private const string AlicargoDataaccessDll = "Alicargo.DataAccess.dll";

        public static void BindServices(IKernel kernel)
        {
            kernel.Bind<ILog>().ToMethod(context => LogManager.GetLogger(string.Empty)).InSingletonScope();

            kernel.Bind<IPasswordConverter>().To<PasswordConverter>().InThreadScope();

            // todo: 1.5. auto binding for intersections
            kernel.Bind<IMailSender>().To<SilentMailSender>().InRequestScope();
            kernel.Bind<IMailSender>().To<MailSender>().WhenInjectedInto<SilentMailSender>().InRequestScope();

            var binded = BindMailingIntersection(kernel);

            kernel.Bind(scanner => scanner.FromThisAssembly()
                                          .Select(IsServiceType)
                                          .Excluding<MailSender>()
                                          .Excluding(binded)
                                          .BindDefaultInterface()
                                          .Configure(binding => binding.InRequestScope()));
        }

        // todo: 2. Test in UI
        private static IEnumerable<Type> BindMailingIntersection(IKernel kernel)
        {
            var assembly = Assembly.GetCallingAssembly();
            var services = assembly.GetTypes().Where(IsServiceType).ToArray();
            var mailingTypes = assembly.GetTypes().Where(x => x.Name.EndsWith(WithMailingSufix)).ToArray();
            var interfaces = assembly.GetTypes().Where(x => x.IsInterface && x.IsPublic).ToArray();
            var binded = new List<Type>();
            foreach (var mailingType in mailingTypes)
            {
                var name = mailingType.Name.Replace(WithMailingSufix, "");
                var @interface = interfaces.First(x => x.Name.Equals("I" + name));
                var service = services.FirstOrDefault(x => x.Name.Equals(name));
                if (service != null)
                {
                    kernel.Bind(@interface).To(mailingType).InRequestScope();
                    kernel.Bind(@interface).To(service).WhenInjectedInto(mailingType).InRequestScope();
                    binded.Add(service);
                    binded.Add(mailingType);
                }
            }
            return binded;
        }

        private static bool IsServiceType(Type type)
        {
            return type.IsClass
                && !type.Name.EndsWith(WithMailingSufix)
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

            kernel.Bind(x => x.FromAssembliesMatching(AlicargoDataaccessDll)
                              .IncludingNonePublicTypes()
                              .Select(IsServiceType)
                              .BindDefaultInterface()
                              .Configure(y => y.InRequestScope()));
        }
    }
}