
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface ICategoryServices
{
Task<(Category? category, string? error)> Create(CategoryForm categoryForm );
Task<(List<CategoryDto> categorys, int? totalCount, string? error)> GetAll(CategoryFilter filter);
Task<(Category? category, string? error)> Update(Guid id , CategoryUpdate categoryUpdate);
Task<(Category? category, string? error)> Delete(Guid id);
}

public class CategoryServices : ICategoryServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public CategoryServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Category? category, string? error)> Create(CategoryForm categoryForm )
{
    var category = _mapper.Map<Category>(categoryForm);
    var result = await _repositoryWrapper.Category.Add(category);
    if (result == null) return (null, "Error in creating category");
    return (result, null);
}

public async Task<(List<CategoryDto> categorys, int? totalCount, string? error)> GetAll(CategoryFilter filter)
    {
        var (category,totalCount) = await _repositoryWrapper.Category.GetAll<CategoryDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)),
            filter.PageNumber,
            filter.PageSize
        );
        return (category, totalCount, null);
    }

public async Task<(Category? category, string? error)> Update(Guid id ,CategoryUpdate categoryUpdate)
    {
        var category = await _repositoryWrapper.Category.Get(x => x.Id == id);
        if (category == null) return (null, "Category not found");
        _mapper.Map(categoryUpdate, category);
        var result = await _repositoryWrapper.Category.Update(category);
        if (result == null) return (null, "Error in updating category");
        return (result, null);
        
        
    }

public async Task<(Category? category, string? error)> Delete(Guid id)
    {
        var category = await _repositoryWrapper.Category.Get(x => x.Id == id);
        if (category == null) return (null, "Category not found");
        var result = await _repositoryWrapper.Category.SoftDelete(id);
        if (result == null) return (null, "Error in deleting category");
        return (result, null);
        
    }

}
