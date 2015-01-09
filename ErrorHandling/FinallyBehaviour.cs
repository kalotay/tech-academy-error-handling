using System;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class FinallyBehaviour: IDisposable
	{
		private bool _disposed;

		[SetUp]
		public void ResetDisposed()
		{
			_disposed = false;
		}

		public void Dispose()
		{
			_disposed = true;
		}

		[Test]
		public void FinallyWithoutException()
		{
			var _ = 0;
			try
			{
				_ += 1;
			}
			finally
			{
				Dispose();
			}
			Assert.That(_disposed);
		}

		[Test]
		public void FinallyWithUsing()
		{
			var _ = 0;
			using (this)
			{
				_ += 1;
			}
			Assert.That(_disposed);
		}

		[Test]
		public void FinallyWithCaughtException()
		{
			try
			{
				CustomException.Throw();
			}
			catch(CustomException)
			{}
			finally
			{
				Dispose();
			}
			Assert.That(_disposed);
		}

		[Test]
		[ExpectedException(typeof(CustomException))]
		public void FinallyWithException()
		{
			try
			{
				try
				{
					CustomException.Throw();
				}
				finally
				{
					Dispose();
				}
			}
			catch (Exception)
			{
				Assert.That(_disposed);
				throw;
			}
		}

		[Test]
		[ExpectedException(typeof(CustomException))]
		public void FinallyWithExceptionAndUsing()
		{
			try
			{
				using(this)
				{
					CustomException.Throw();
				}
			}
			catch (Exception)
			{
				Assert.That(_disposed);
				throw;
			}
		}
	}
}