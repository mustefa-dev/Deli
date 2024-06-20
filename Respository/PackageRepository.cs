using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class PackageRepository : GenericRepository<Package , Guid> , IPackageRepository
    {
        public PackageRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
    }
}
