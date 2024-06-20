using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class TagRepository : GenericRepository<Tag , Guid> , ITagRepository
    {
        public TagRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
