using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class QualityToolsRepository : GenericRepository<QualityTools , Guid> , IQualityToolsRepository
    {
        public QualityToolsRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
