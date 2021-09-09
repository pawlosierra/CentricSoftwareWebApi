using System;
using System.Collections.Generic;
using System.Text;

namespace CentricSoftwareWebApi.Domain.Models
{
  public class Product
  {
    public int Id { get; set; }
    public Guid IdClothing { get; set; }
    public int IdTags { get; set; }
    public List<string> Tags { get; set; }
    public virtual Clothing IdClothingNavigation { get; set; }
    public virtual Tags IdTagsNavigation { get; set; }
  }
}
