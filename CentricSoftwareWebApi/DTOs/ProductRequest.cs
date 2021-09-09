using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.DTOs
{
  public class ProductRequest
  {
    [Required(ErrorMessage = "The field name is required")]
    [StringLength(20, ErrorMessage = "The maximum number of characters is 20")]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field description is required")]
    [StringLength(50, ErrorMessage = "The maximum number of characters is 50")]
    public string Description { get; set; }

    [StringLength(50, ErrorMessage = "The maximum number of characters is 50")]
    public string Brand { get; set; }

    [Required(ErrorMessage = "The field category is required")]
    [StringLength(50, ErrorMessage = "The maximum number of characters is 50")]
    public string Category { get; set; }

    public List<string> Tags { get; set; }
  }
}
