using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class LikedRepository : GenericRepository<Liked , Guid> , ILikedRepository
    {
        public LikedRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
