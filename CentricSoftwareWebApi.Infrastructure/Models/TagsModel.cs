using System;
using System.Collections.Generic;

#nullable disable

namespace CentricSoftwareWebApi.Infrastructure.Models
{
    public partial class TagsModel
    {
        public TagsModel()
        {
            ProductModels = new HashSet<ProductModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ProductModel> ProductModels { get; set; }
    }
}
