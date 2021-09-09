using System;
using System.Collections.Generic;

#nullable disable

namespace CentricSoftwareWebApi.Infrastructure.Models
{
    public partial class ProductModel
    {
        public int Id { get; set; }
        public Guid IdClothing { get; set; }
        public int IdTags { get; set; }

        public virtual ClothingModel IdClothingNavigation { get; set; }
        public virtual TagsModel IdTagsNavigation { get; set; }
    }
}
