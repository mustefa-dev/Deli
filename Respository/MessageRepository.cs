using AutoMapper;
using Deli.DATA;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Repository
{

    public class MessageRepository : GenericRepository<Message , Guid> , IMessageRepository
    {
        public MessageRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
