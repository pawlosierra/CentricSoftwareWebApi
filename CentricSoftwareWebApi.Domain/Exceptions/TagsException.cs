using System;

namespace CentricSoftwareWebApi.Domain.Exceptions
{
  public class TagsException : Exception
  {
    public TagsException(string errorCode, string message) : base(message)
    {
      ErrorCode = errorCode;
    }
    public string ErrorCode { get; set; }
  }
}
