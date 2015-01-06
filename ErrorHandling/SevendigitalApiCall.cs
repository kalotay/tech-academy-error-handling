using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Releases;
using SevenDigital.Api.Wrapper;

namespace ErrorHandling
{
	[TestFixture]
	public class SevendigitalApiCall
	{
		[Test, Ignore]
		public async Task SearchesReleases()
		{
			var api = Api<ReleaseSearch>.Create.WithQuery("the");
			var searchResult = await api.Please();
			Assert.That(searchResult.Results.Count, Is.GreaterThan(0));
			Assert.That(searchResult.Results[0].Release.Title, Is.StringContaining("the"));
		}
	}
}