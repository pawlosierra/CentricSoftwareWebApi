using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.DTOs
{
  public class ProductResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Created_at { get; set; }
    public string Brand { get; set; }
    public List<string> Tags { get; set; }
  }
}
