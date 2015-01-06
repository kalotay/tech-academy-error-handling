using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class CommonExceptions: IEnumerable
	{
		[Test]
		[ExpectedException(typeof (InvalidOperationException))]
		public void InvalidOperation()
		{
			var list = new List<object> {"a"};
			foreach (var item in list)
			{
				list.Add(item);
			}
		}

		[Test]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void ArgumentOutOfRange()
		{
			var list = new List<object>();
			list.FindIndex(1, o => true);
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ArgumentNull()
		{
			new Hashtable {{null, "a"}};
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void NullReference()
		{
			object obj = null;
			obj.ToString();
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void IndexOutOfRange()
		{
			var list = new object[0];
			var _ = list[0];
		}

		[Test, Ignore("crashes test runner")]
		[ExpectedException(typeof (StackOverflowException))]
		public void StackOverflow()
		{
			StackOverflow();
		}

		[Test]
		[ExpectedException(typeof (OutOfMemoryException))]
		public void OutOfMemory()
		{
			var _ = new decimal[int.MaxValue];
		}

		[Test]
		[ExpectedException(typeof (DivideByZeroException))]
		public void DivideByZero()
		{
			var dividend = 1;
			var divisor = 0;
			var _ = dividend/divisor;
		}

		[Test]
		[ExpectedException(typeof (FileNotFoundException))]
		public void FileNotFound()
		{
			File.Open(Guid.NewGuid().ToString(), FileMode.Open);
		}

		[Test]
		[ExpectedException(typeof (FormatException))]
		public void Format()
		{
			DateTime.Parse("never");
		}

		[Test]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void KeyNotFound()
		{
			var dict = new Dictionary<object, object>();
			var _ = dict[0];
		}

		[Test]
		[ExpectedException(typeof (NotImplementedException))]
		public void NotImplemented()
		{
			GetEnumerator();
		}

		[Test]
		[ExpectedException(typeof(NotSupportedException))]
		public void NotSupported()
		{
			using (var stream = File.OpenWrite("writeonly"))
			{
				stream.ReadByte();
			}
		}

		[Test]
		[ExpectedException(typeof(ObjectDisposedException))]
		public void ObjectDisposed()
		{
			var stream = File.OpenWrite("writeonly");
			stream.Dispose();
			stream.Flush();
		}

		[Test]
		[ExpectedException(typeof(OverflowException))]
		public void Overflow()
		{
			checked
			{
				var maxValue = int.MaxValue;
				var _ = maxValue * maxValue;
			}
		}

		[Test]
		[ExpectedException(typeof (AggregateException))]
		public void Aggregate()
		{
			var task = Task.Run(() => { throw new Exception(); });
			task.Wait();
		}

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}