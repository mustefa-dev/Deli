
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface ITagServices
{
Task<(Tag? tag, string? error)> Create(TagForm tagForm );
Task<(List<TagDto> tags, int? totalCount, string? error)> GetAll(TagFilter filter);
Task<(Tag? tag, string? error)> Update(Guid id , TagUpdate tagUpdate);
Task<(Tag? tag, string? error)> Delete(Guid id);
}

public class TagServices : ITagServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public TagServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Tag? tag, string? error)> Create(TagForm tagForm )
{
    throw new NotImplementedException();
      
}

public async Task<(List<TagDto> tags, int? totalCount, string? error)> GetAll(TagFilter filter)
    {
        throw new NotImplementedException();
    }

public async Task<(Tag? tag, string? error)> Update(Guid id ,TagUpdate tagUpdate)
    {
        throw new NotImplementedException();
      
    }

public async Task<(Tag? tag, string? error)> Delete(Guid id)
    {
        throw new NotImplementedException();
   
    }

}
