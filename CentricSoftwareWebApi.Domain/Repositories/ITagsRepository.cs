using CentricSoftwareWebApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CentricSoftwareWebApi.Domain.Repositories
{
  public interface ITagsRepository
  {
    Task<IEnumerable<Tags>> GetTags();
    Task<Tags> AddTags(Tags tags);
  }
}
