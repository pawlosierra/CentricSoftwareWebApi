using System;
using System.Collections.Generic;
using System.Text;

namespace CentricSoftwareWebApi.Domain.Models
{
  public class Clothing
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Brand { get; set; }

    public virtual ICollection<Product> ProductModels { get; set; }
  }
}
