
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface ICategoryServices
{
Task<(Category? category, string? error)> Create(CategoryForm categoryForm, string language);
Task<(List<CategoryDto> categorys, int? totalCount, string? error)> GetAll(CategoryFilter filter, string language);
Task<(Category? category, string? error)> Update(Guid id , CategoryUpdate categoryUpdate, string language);
Task<(Category? category, string? error)> Delete(Guid id, string language);
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
   
   
public async Task<(Category? category, string? error)> Create(CategoryForm categoryForm, string language) 
{
    var category = _mapper.Map<Category>(categoryForm);
    var result = await _repositoryWrapper.Category.Add(category);
    if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in creating Category", "خطأ في إنشاء الفئة", language));
    return (result, null);
}

public async Task<(List<CategoryDto> categorys, int? totalCount, string? error)> GetAll(CategoryFilter filter, string language)
    {
        var (category,totalCount) = await _repositoryWrapper.Category.GetAll<CategoryDto>(
            x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name))&&
                 (string.IsNullOrEmpty(filter.ArName) || x.ArName.Contains(filter.ArName))&&
                    (filter.CategoryId == null || x.Id == filter.CategoryId),
            filter.PageNumber,
            filter.PageSize
        );
        if (category == null) return (null, null, ErrorResponseException.GenerateErrorResponse("Error in getting Categorys", "خطأ في الحصول على الفئات", language));
        return (category, totalCount, null);
    }

public async Task<(Category? category, string? error)> Update(Guid id ,CategoryUpdate categoryUpdate, string language)
    {
        var category = await _repositoryWrapper.Category.Get(x => x.Id == id);
        if (category == null) return (null, ErrorResponseException.GenerateErrorResponse("Category not found", "لم يتم العثور على الفئة", language));
        _mapper.Map(categoryUpdate, category);
        var result = await _repositoryWrapper.Category.Update(category);
        if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in updating category", "خطأ في تحديث الفئة", language));
        return (result, null);
        
        
    }

public async Task<(Category? category, string? error)> Delete(Guid id, string language)
    {
        var category = await _repositoryWrapper.Category.Get(x => x.Id == id);
        if (category == null) return (null, ErrorResponseException.GenerateErrorResponse("Category not found", "لم يتم العثور على الفئة", language));
        var result = await _repositoryWrapper.Category.SoftDelete(id);
        if (result == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in deleting category", "خطأ في حذف الفئة", language));
        return (result, null);
        
    }

}
