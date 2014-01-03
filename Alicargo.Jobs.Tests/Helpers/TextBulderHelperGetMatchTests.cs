using Alicargo.Jobs.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Alicargo.Jobs.Tests.Helpers
{
	[TestClass]
	public class TextBulderHelperGetMatchTests
	{
		private Fixture _fixture;

		[TestInitialize]
		public void TestInitialize()
		{
			_fixture = new Fixture();
		}

		[TestMethod]
		public void Test_NoneToMatch()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch(_fixture.Create<string>(), _fixture.Create<string>(), out text, out format);

			match.Should().BeFalse();
			text.Should().BeNull();
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_NoMatch()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name1} asdfsa {name3}", "name2", out text, out format);

			match.Should().BeFalse();
			text.Should().BeNull();
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_BadMatch()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf { name} asdfsa", "name", out text, out format);

			match.Should().BeFalse();
			text.Should().BeNull();
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_BadMatch2()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name [asdfsa]", "name", out text, out format);

			match.Should().BeFalse();
			text.Should().BeNull();
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_OneMatch()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name} asdfsa", "name", out text, out format);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo("{name}");
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_OneMatchRussian()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {Имя} asdfsa", "Имя", out text, out format);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo("{Имя}");
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_TwoMatch()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name} {name [format]} asdfsa", "name", out text, out format);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo("{name}");
			format.Should().BeNull();
		}

		[TestMethod]
		public void Test_TwoMatchWithFromat()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name[f1]} {name [f2]} asdfsa", "name", out text, out format);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo("{name[f1]}");
			format.ShouldBeEquivalentTo("f1");
		}

		[TestMethod]
		public void Test_ComlexFromat()
		{
			string text;
			string formatOut;
			const string format = "\"f1/0.123\":]\t[ '#{0}'!!!";
			const string name = "{name [" + format + "]}";
			var match = TextBulderHelper.GetMatch("asdfsf " + name + " asfasdfagt5t!!", "name", out text, out formatOut);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo(name);
			formatOut.ShouldBeEquivalentTo(format);
		}

		[TestMethod]
		public void Test_RussianFromat()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name [формат]} asfasdfagt5t!!", "name", out text, out format);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo("{name [формат]}");
			format.ShouldBeEquivalentTo("формат");
		}

		[TestMethod]
		public void Test_SimpleFromat()
		{
			string text;
			string format;
			var match = TextBulderHelper.GetMatch("asdfsf {name [f1]} asfasdfagt5t!!", "name", out text, out format);

			match.Should().BeTrue();
			text.ShouldBeEquivalentTo("{name [f1]}");
			format.ShouldBeEquivalentTo("f1");
		}
	}
}