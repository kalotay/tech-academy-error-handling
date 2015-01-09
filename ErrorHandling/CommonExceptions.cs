using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
			var succesfulTask = Task.Run(() => { });
			var failingTask = Task.Run(() => { throw new CustomException(); });
			var tasks = Task.WhenAll(succesfulTask, failingTask);
			tasks.Wait();
		}

		[Test]
		[ExpectedException(typeof (WebException))]
		public void Web()
		{
			var request = WebRequest.Create("http://localhost:10000");
			using (request.GetResponse())
			{}
		}

		[Test]
		[ExpectedException(typeof (SocketException))]
		public void Socket()
		{
			using (var socket = new Socket(SocketType.Stream, ProtocolType.IP))
			{
				socket.Connect("localhost", 10000);
			}
		}

		[Test]
		[ExpectedException(typeof (SqlException))]
		public void Sql()
		{
			var builder = new SqlConnectionStringBuilder
				{
					DataSource = "(local)",
					UserID = "bob",
					Password = "hunter2",
					InitialCatalog = "Adventure Works"
				};
			var connectionString = builder.ToString();
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
			}
		}

		[Test]
		[ExpectedException(typeof (XmlException))]
		public void Xml()
		{
			XDocument.Parse("{}");
		}

		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}