using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicargo.DataAccess.Contracts.Contracts.Application;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Jobs.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class TextBulderTests
	{
		private TextBuilder _builder;
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
			_builder = new TextBuilder();
		}

		[TestMethod]
		public void Test_GetText()
		{
			var properties =
				typeof(ApplicationData).GetProperties().Where(x => x.PropertyType == typeof(string)).ToArray();
			var dictionary = new Dictionary<string, string>();
			var sb = new StringBuilder();
			foreach (var property in properties)
			{
				sb.AppendFormat("{{{0} [{0}: {{0}}]}}", property.Name).AppendLine();
				dictionary.Add(property.Name, _fixture.Create<string>());
			}
			sb.Append("{DisplayNumber[Number: \"{0}\"]}").AppendLine();
			sb.Append("{DisplayNumber}").AppendLine();
			dictionary.Add("DisplayNumber", _fixture.Create<string>());

			var text = _builder.GetText(sb.ToString(), TwoLetterISOLanguageName.Russian, dictionary);

			text.Should().NotContain("{").And.NotContain("}");
		}
	}
}