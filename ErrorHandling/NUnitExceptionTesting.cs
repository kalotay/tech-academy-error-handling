using System;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class NUnitExceptionTesting
	{
		[Test]
		[ExpectedException(typeof (CustomException))]
		public void ExpectedExceptionAttribute()
		{
			CustomException.Throw();
		}

		[Test]
		public void ThrowsMethod()
		{
			var exception = Assert.Throws<CustomException>(CustomException.Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		public void CatchMethod()
		{
			var exception = Assert.Catch<CustomException>(CustomException.Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		public void ThrowsConstraint()
		{
			Assert.That(CustomException.Throw, Throws.TypeOf<CustomException>().With.InnerException.Null);
		}

		[Test]
		[ExpectedException]
		public void ThrowsMethodBaseException()
		{
			var exception = Assert.Throws<Exception>(CustomException.Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		public void CatchMethodBaseException()
		{
			var exception = Assert.Catch<Exception>(CustomException.Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void NUnitUsesExceptions()
		{
			Assert.Fail();
		}
	}
}