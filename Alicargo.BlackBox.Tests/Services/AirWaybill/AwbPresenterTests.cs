using Alicargo.BlackBox.Tests.Properties;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.AirWaybill;
using Alicargo.TestHelpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace Alicargo.BlackBox.Tests.Services.AirWaybill
{
	[TestClass]
	public class AwbPresenterTests
	{
		private AwbPresenter _presenter;

		[TestInitialize]
		public void TestInitialize()
		{
			var compositionHelper = new CompositionHelper(
				Settings.Default.MainConnectionString,
				Settings.Default.FilesConnectionString);

			_presenter = compositionHelper.Kernel.Get<AwbPresenter>();
		}

		[TestMethod]
		public void Test_List()
		{
			var collection = _presenter.List(10, 0, null, null, TwoLetterISOLanguageName.English);

			collection.Data.Length.Should().BeInRange(1, 10);
		}
	}
}