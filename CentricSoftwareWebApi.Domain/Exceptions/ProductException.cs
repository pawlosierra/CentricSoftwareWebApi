using System;

namespace CentricSoftwareWebApi.Domain.Exceptions
{
  public class ProductException : Exception
  {
    public ProductException(string errorCode, string message) : base(message)
    {
      ErrorCode = errorCode;
    }
    public string ErrorCode { get; set; }
  }
}
