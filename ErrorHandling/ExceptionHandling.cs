using System;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class ExceptionHandling
	{
		public class DerivedCustomException: CustomException
		{}

		[Test]
		public void CatchAll()
		{
			try
			{
				CustomException.Throw();
			}
			catch
			{
			}
		}

		[Test]
		public void CatchParent()
		{
			try
			{
				CustomException.Throw();
			}
			catch(Exception e)
			{
				Assert.That(e, Is.InstanceOf<CustomException>());
			}
		}

		[Test]
		public void CatchExact()
		{
			try
			{
				CustomException.Throw();
			}
			catch(CustomException e)
			{
				Assert.That(e, Is.InstanceOf<CustomException>());
			}
		}

		[Test]
		[ExpectedException(typeof(CustomException))]
		public void CatchDerived()
		{
			try
			{
				CustomException.Throw();
			}
			catch(DerivedCustomException e)
			{
				Assert.That(e, Is.InstanceOf<CustomException>());
			}
		}

		[Test]
		public void MultiCatch()
		{
			var catchCount = 0;
			try
			{
				CustomException.Throw();
			}
			catch (CustomException)
			{
				catchCount += 1;
			}
			catch (Exception)
			{
				catchCount += 1;
			}
			Assert.That(catchCount, Is.EqualTo(1));
		}

		[Test]
		public void StackPreservation()
		{
			var exception = Assert.Catch<CustomException>(PreserveStack);
			Assert.That(exception.StackTrace, Is.StringContaining("CustomException.Throw()"));
		}

		[Test]
		public void StackLoss()
		{
			var exception = Assert.Catch<CustomException>(LoseStack);
			Assert.That(exception.StackTrace, Is.Not.StringContaining("CustomException.Throw()"));
		}

		[Test]
		public void StackWrapping()
		{
			var exception = Assert.Catch<CustomException>(WrapStack);
			Assert.That(exception.StackTrace, Is.Not.StringContaining("Throw()"));
			Assert.That(exception.InnerException.StackTrace, Is.StringContaining("Throw()"));
		}

		private static void WrapStack()
		{
			try
			{
				CustomException.Throw();
			}
			catch (CustomException e)
			{
				throw new CustomException(e);
			}
		}

		private static void LoseStack()
		{
			try
			{
				CustomException.Throw();
			}
			catch (CustomException e)
			{
				throw e;
			}
		}

		private static void PreserveStack()
		{
			try
			{
				CustomException.Throw();
			}
			catch (CustomException)
			{
				throw;
			}
		}
	}
}