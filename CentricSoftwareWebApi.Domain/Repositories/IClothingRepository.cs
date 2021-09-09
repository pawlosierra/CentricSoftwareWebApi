using CentricSoftwareWebApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Domain.Repositories
{
  public interface IClothingRepository
  {
    Task<Clothing> AddClothing(Clothing clothing);
  }
}
