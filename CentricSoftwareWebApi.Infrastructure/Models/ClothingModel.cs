using System;
using System.Collections.Generic;

#nullable disable

namespace CentricSoftwareWebApi.Infrastructure.Models
{
    public partial class ClothingModel
    {
        public ClothingModel()
        {
            ProductModels = new HashSet<ProductModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Brand { get; set; }

        public virtual ICollection<ProductModel> ProductModels { get; set; }
    }
}
