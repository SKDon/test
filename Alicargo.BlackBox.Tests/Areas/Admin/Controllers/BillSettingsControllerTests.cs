using System.Web.Mvc;
using Alicargo.Areas.Admin.Controllers;
using Alicargo.Areas.Admin.Models;
using Alicargo.BlackBox.Tests.Properties;
using Alicargo.Core.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.DataAccess.Contracts.Repositories;
using Alicargo.TestHelpers;
using Alicargo.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ploeh.AutoFixture;

namespace Alicargo.BlackBox.Tests.Areas.Admin.Controllers
{
	[TestClass]
	public class BillSettingsControllerTests
	{
		private CompositionHelper _composition;
		private BillSettingsController _controller;
		private Fixture _fixture;
		private ISerializer _serializer;
		private ISettingRepository _settings;

		[TestCleanup]
		public void TestCleanup()
		{
			_composition.Dispose();
		}

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_composition = new CompositionHelper(Settings.Default.MainConnectionString, Settings.Default.FilesConnectionString);
			_settings = _composition.Kernel.Get<ISettingRepository>();
			_serializer = _composition.Kernel.Get<ISerializer>();

			_controller = _composition.Kernel.Get<BillSettingsController>();
		}

		[TestMethod]
		public void Test_GetIndex()
		{
			var setting = _settings.Get(SettingType.Bill);
			var billSettings = _serializer.Deserialize<BillSettings>(setting.Data);

			var result = (ViewResult)_controller.Index();
			var model = (BillSettingsModel)result.Model;

			model.BankDetails.ShouldBeEquivalentTo(billSettings);
			model.ShouldBeEquivalentTo(billSettings, options => options.Excluding(x => x.VAT).ExcludingMissingProperties());
			model.VAT.ShouldBeEquivalentTo(billSettings.VAT * 100);
		}

		[TestMethod]
		public void Test_PostIndex()
		{
			var model = _fixture.Create<BillSettingsModel>();
			model.Version = _settings.Get(SettingType.Bill).RowVersion;

			var result = _controller.Index(model);
			var setting = _settings.Get(SettingType.Bill);
			var billSettings = _serializer.Deserialize<BillSettings>(setting.Data);

			result.Should().BeOfType<RedirectToRouteResult>();
			billSettings.ShouldBeEquivalentTo(model, options => options.Excluding(x => x.VAT).ExcludingMissingProperties());
			billSettings.VAT.ShouldBeEquivalentTo((decimal)model.VAT / 100);
			billSettings.ShouldBeEquivalentTo(model.BankDetails, options => options.ExcludingMissingProperties());
		}

		[TestMethod]
		public void Test_UpdateConflictException()
		{
			var original = _settings.Get(SettingType.Bill);
			var model = _fixture.Create<BillSettingsModel>();

			var result = (ViewResult)_controller.Index(model);
			var newBill = _settings.Get(SettingType.Bill);

			newBill.ShouldBeEquivalentTo(original);
			result.ViewData.ModelState.IsValid.Should().BeFalse();
		}
	}
}