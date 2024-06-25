
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IReviewServices
{
Task<(ReviewDto? review, string? error)> Create(ReviewForm reviewForm, Guid userId, string language);
Task<(List<ReviewDto> reviews, int? totalCount, string? error)> GetAll(ReviewFilter filter, string language);
Task<(ReviewDto? review, string? error)> Update(Guid id , ReviewUpdate reviewUpdate, Guid userId, string language);
Task<(ReviewDto? review, string? error)> Delete(Guid id, string language);
}

public class ReviewServices : IReviewServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public ReviewServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(ReviewDto? review, string? error)> Create(ReviewForm reviewForm , Guid userId, string language)
{
    
    if (reviewForm.Rating < 0 || reviewForm.Rating > 5)
    {
        return (null, ErrorResponseException.GenerateLocalizedResponse("Rating must be between 0 and 5", "يجب أن يكون التقييم بين 0 و 5", language));
    }
    var item = await _repositoryWrapper.Item.GetById(reviewForm.ItemId);
    if (item == null)
    {
        return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
    }
    var reviewExists = await _repositoryWrapper.Review.Get(x => x.UserId == userId && x.ItemId == reviewForm.ItemId);
    if (reviewExists != null)
    {
        return (null, ErrorResponseException.GenerateLocalizedResponse("You have already reviewed this item", "لقد قمت بتقييم هذا العنصر بالفعل", language));
    }
    
    var review = _mapper.Map<Review>(reviewForm);
    review.UserId = userId;
    review.User = await _repositoryWrapper.User.Get(x => x.Id == userId);
    var result = await _repositoryWrapper.Review.Add(review);
    if (result == null)
    {
        return (null, ErrorResponseException.GenerateLocalizedResponse("Error in Creating a Review", "خطأ في انشاء التقييم", language));
    }
    return (_mapper.Map<ReviewDto>(result), null);
      
}

public async Task<(List<ReviewDto> reviews, int? totalCount, string? error)> GetAll(ReviewFilter filter, string language)
    {
        var (reviews,totalCount) = await _repositoryWrapper.Review.GetAll<ReviewDto>(
            x=> (filter.ItemId == null || x.ItemId == filter.ItemId)&&
                (filter.Rating == null || x.Rating >= filter.Rating)&& 
                (filter.UserId == null || x.UserId == filter.UserId),
            filter.PageNumber,
            filter.PageSize);
            return (reviews, totalCount, null);
            
            
       
    }

public async Task<(ReviewDto? review, string? error)> Update(Guid id ,ReviewUpdate reviewUpdate, Guid userId, string language)
    {
        var review = await _repositoryWrapper.Review.Get(x => x.Id == id);
        if (review == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Review not found", "لم يتم العثور على التقييم", language));
       if (review.UserId != userId) return (null, ErrorResponseException.GenerateLocalizedResponse("You are not authorized to update this review", "غير مصرح لك بتحديث هذا التقييم", language));
        if (reviewUpdate.Rating < 0 || reviewUpdate.Rating > 5)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Rating must be between 0 and 5", "يجب أن يكون التقييم بين 0 و 5", language));
        }
        _mapper.Map(reviewUpdate, review);
        var result = await _repositoryWrapper.Review.Update(review);
        if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in updating review", "خطأ في تحديث التقييم", language));
        return (_mapper.Map<ReviewDto>(result), null);
      
    }

public async Task<(ReviewDto? review, string? error)> Delete(Guid id, string language)
    {
        var review = await _repositoryWrapper.Review.Get(x => x.Id == id);
        if (review == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Review not found", "لم يتم العثور على التقييم", language));
        var result = await _repositoryWrapper.Review.SoftDelete(id);
        if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in deleting review", "خطأ في حذف التقييم", language));
        return (_mapper.Map<ReviewDto>(result), null);
   
    }

}
