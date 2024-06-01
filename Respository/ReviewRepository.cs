using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class ReviewRepository : GenericRepository<Review , Guid> , IReviewRepository
    {
        public ReviewRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
