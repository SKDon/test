using System;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using Alicargo.Core.Repositories;
using Alicargo.DataAccess.DbContext;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;

namespace Alicargo.DataAccess
{
	public static class CompositionRoot
	{
		private static bool IsServiceType(Type type)
		{
			return type.IsClass && type.GetInterfaces().Any(intface => intface.Name == "I" + type.Name);
		}

		// todo: move registration to the global.asax
		public static IKernel ConfigureDataAccess(this IKernel kernel, string connectionString)
		{
			kernel.Bind<IDbConnection>()
				.ToMethod(x => new SqlConnection(connectionString))
				.InRequestScope()
				.OnDeactivation(x => x.Close());

			kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

			kernel.Bind<AlicargoDataContext>().ToSelf().InRequestScope();

			kernel.Bind<DataContext>().To<AlicargoDataContext>().InRequestScope();

			kernel.Bind(scanner => scanner.FromThisAssembly()
				.IncludingNonePublicTypes()
				.Select(IsServiceType)
				.Excluding<UnitOfWork>()
				.BindDefaultInterface());

			return kernel;
		}
	}
}
