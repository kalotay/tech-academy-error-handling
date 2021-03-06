﻿using System;
using NUnit.Framework;

namespace ErrorHandling
{
	[TestFixture]
	public class IterativeSquareRoot
	{
		public static double SquareRoot(double x, double guess = 1, int iterations = 10)
		{
			var estimate = guess;
			for (var i = 0; i < iterations; i++)
			{
				estimate = 0.5*(estimate + x/estimate);
			}
			return estimate;
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(4)]
		[TestCase(2)]
		[TestCase(0.5)]
		public void IterativeMethodMatchesExact(double x)
		{
			var expected = Math.Sqrt(x);
			var actual = SquareRoot(x);
			Assert.That(actual, Is.EqualTo(expected).Within(0.01));
		}

		[Test, Ignore]
		public void NegativeRoot()
		{
			var x = SquareRoot(-1);
			Assert.That(x, Is.InRange(0, double.MaxValue));
		}

		[Test, Ignore]
		public void InvalidGuess()
		{
			var x = SquareRoot(1, 0);
			Assert.That(x, Is.InRange(0, double.MaxValue));
		}

		[Test, Ignore]
		public void InvalidIterations()
		{
			var x = SquareRoot(1, -1, -1);
			Assert.That(x, Is.InRange(0, double.MaxValue));
		}
	}
}