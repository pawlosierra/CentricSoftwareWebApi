using System;

namespace CentricSoftwareWebApi.Domain.Exceptions
{
  public class ClothingException : Exception
  {
    public ClothingException(string errorCode, string message) : base(message)
    {
      ErrorCode = errorCode;
    }
    public string ErrorCode { get; set; }
  }
}
