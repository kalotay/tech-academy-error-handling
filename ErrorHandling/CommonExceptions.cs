using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace ErrorHandling
{
	public class CommonExceptions
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
	}
}