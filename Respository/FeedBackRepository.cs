using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class FeedBackRepository : GenericRepository<FeedBack , Guid> , IFeedBackRepository
    {
        public FeedBackRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
