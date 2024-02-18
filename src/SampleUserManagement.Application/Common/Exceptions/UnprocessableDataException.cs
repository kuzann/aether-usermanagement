
namespace SampleUserManagement.Application.Common.Exceptions
{
	public class UnprocessableDataException : Exception
	{
		public UnprocessableDataException()
		{
		}

		public UnprocessableDataException(string message) : base(message)
		{
		}

		public UnprocessableDataException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
