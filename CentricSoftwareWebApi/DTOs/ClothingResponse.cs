using System;

namespace CentricSoftwareWebApi.DTOs
{
  public class ClothingResponse
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Brand { get; set; }
  }
}
