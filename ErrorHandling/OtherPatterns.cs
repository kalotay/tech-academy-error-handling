using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class OtherPatterns
	{
		private Dictionary<string, object> _dict;

		[TestFixtureSetUp]
		public void SetUp()
		{
			_dict = new Dictionary<string, object> {{"a", "stuff"}};
		}

		[TestCase("a")]
		[TestCase("b")]
		public void ExceptionPattern(string key)
		{
			try
			{
				var item = _dict[key];
				OnFound(key, item);
			}
			catch (KeyNotFoundException)
			{
				OnNotFound(key);
			}
		}

		[TestCase("a")]
		[TestCase("b")]
		public void TestDoPattern(string key)
		{
			if (_dict.ContainsKey(key))
			{
				var item = _dict[key];
				OnFound(key, item);
				
			}
			else
			{
				OnNotFound(key);
			}
		}

		[TestCase("a")]
		[TestCase("b")]
		public void TryPattern(string key)
		{
			object item;
			if (_dict.TryGetValue(key, out item))
			{
				OnFound(key, item);
				
			}
			else
			{
				OnNotFound(key);
			}
		}

		private static void OnNotFound(string key)
		{
			Console.WriteLine("Missing key {0}", key);
		}

		private static void OnFound(string key, object item)
		{
			Console.WriteLine("Found key {0} with value {1}", key, item);
		}
	}
}