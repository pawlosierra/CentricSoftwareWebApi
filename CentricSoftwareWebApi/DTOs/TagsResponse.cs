using System.Collections.Generic;

namespace CentricSoftwareWebApi.DTOs
{
  public class TagsResponse
  {
    public string Description { get; set; }

    public virtual ICollection<ProductResponse> ProductModels { get; set; }
  }
}
