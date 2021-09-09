using System;
using System.Collections.Generic;
using System.Text;

namespace CentricSoftwareWebApi.Domain.Models
{
  public class Tags
  {
    public int Id { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Product> ProductModels { get; set; }
  }
}
