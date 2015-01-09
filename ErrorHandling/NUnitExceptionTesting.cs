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
			Throw();
		}

		[Test]
		public void ThrowsMethod()
		{
			var exception = Assert.Throws<CustomException>(Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		public void CatchMethod()
		{
			var exception = Assert.Catch<CustomException>(Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		public void ThrowsConstraint()
		{
			Assert.That(Throw, Throws.TypeOf<CustomException>().With.InnerException.Null);
		}

		[Test]
		[ExpectedException]
		public void ThrowsMethodBaseException()
		{
			var exception = Assert.Throws<Exception>(Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		public void CatchMethodBaseException()
		{
			var exception = Assert.Catch<Exception>(Throw);
			Assert.That(exception.InnerException, Is.Null);
		}

		[Test]
		[ExpectedException(typeof (AssertionException))]
		public void NUnitUsesExceptions()
		{
			Assert.Fail();
		}

		private static void Throw()
		{
			throw new CustomException();
		}
	}
}