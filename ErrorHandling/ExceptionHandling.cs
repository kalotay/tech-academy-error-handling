using System;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class ExceptionHandling
	{
		public class CustomException: Exception
		{
			public CustomException(Exception exception):base("custom", exception)
			{}

			public CustomException()
			{}
		}

		public class DerivedCustomException: CustomException
		{}

		[Test]
		public void CatchAll()
		{
			try
			{
				Throw();
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
				Throw();
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
				Throw();
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
				Throw();
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
				Throw();
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
			Assert.That(exception.StackTrace, Is.StringContaining("Throw()"));
		}

		[Test]
		public void StackLoss()
		{
			var exception = Assert.Catch<CustomException>(LoseStack);
			Assert.That(exception.StackTrace, Is.Not.StringContaining("Throw()"));
		}

		[Test]
		public void StackWrapping()
		{
			var exception = Assert.Catch<CustomException>(WrapStack);
			Assert.That(exception.StackTrace, Is.Not.StringContaining("Throw()"));
			Assert.That(exception.InnerException.StackTrace, Is.StringContaining("Throw()"));
		}

		private void WrapStack()
		{
			try
			{
				Throw();
			}
			catch (CustomException e)
			{
				throw new CustomException(e);
			}
		}

		private void LoseStack()
		{
			try
			{
				Throw();
			}
			catch (CustomException e)
			{
				throw e;
			}
		}

		private void PreserveStack()
		{
			try
			{
				Throw();
			}
			catch (CustomException)
			{
				throw;
			}
		}

		private static void Throw()
		{
			throw new CustomException();
		}
	}
}