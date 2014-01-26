using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Controllers.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.TestHelpers;
using Alicargo.ViewModels.Application;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.BlackBox.Tests.Controllers.Application
{
	[TestClass]
	public class ApplicationListControllerTests
	{
		[TestMethod]
		public void Test_List_ByAdmin()
		{
			using(var context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString))
			{
				var controller = context.Kernel.Get<ApplicationListController>();

				var result = controller.List(10, 0, null);

				var data = (ApplicationListCollection)result.Data;

				data.Total.ShouldBeEquivalentTo(20);

				data.Data.Should().HaveCount(10);
			}
		}

		[TestMethod]
		public void Test_List_ByForwarder()
		{
			using(var context = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString, RoleType.Forwarder))
			{
				var controller = context.Kernel.Get<ApplicationListController>();

				var result = controller.List(5, 0, null);

				var data = (ApplicationListCollection)result.Data;

				data.Total.ShouldBeEquivalentTo(6);

				data.Data.Should().HaveCount(5);
			}
		}
	}
}
