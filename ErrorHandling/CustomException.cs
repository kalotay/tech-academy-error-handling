using System;

namespace ErrorHandling
{
	public class CustomException: Exception
	{
		public CustomException(Exception exception):base("custom", exception)
		{}

		public CustomException()
		{}
	}
}