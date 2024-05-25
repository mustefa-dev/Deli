using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class AppsettingsRepository : GenericRepository<Appsettings , Guid> , IAppsettingsRepository
    {
        public AppsettingsRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
