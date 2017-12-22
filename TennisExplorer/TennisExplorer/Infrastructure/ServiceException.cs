using System;

namespace TennisExplorer.Infrastructure
{
	public class ServiceException : Exception
	{
		public ServiceException(string message) : base(message)
		{

		}
	}
}
