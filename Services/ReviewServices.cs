
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IReviewServices
{
Task<(ReviewDto? review, string? error)> Create(ReviewForm reviewForm, Guid userId);
Task<(List<ReviewDto> reviews, int? totalCount, string? error)> GetAll(ReviewFilter filter);
Task<(ReviewDto? review, string? error)> Update(Guid id , ReviewUpdate reviewUpdate, Guid userId);
Task<(ReviewDto? review, string? error)> Delete(Guid id);
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
   
   
public async Task<(ReviewDto? review, string? error)> Create(ReviewForm reviewForm , Guid userId)
{
    
    if (reviewForm.Rating < 0 || reviewForm.Rating > 5)
    {
        return (null, "Rating must be between 0 and 5");
    }
    var item = await _repositoryWrapper.Item.GetById(reviewForm.ItemId);
    if (item == null)
    {
        return (null, "Item not found");
    }
    var reviewExists = await _repositoryWrapper.Review.Get(x => x.UserId == userId && x.ItemId == reviewForm.ItemId);
    if (reviewExists != null)
    {
        return (null, "You have already reviewed this item");
    }
    
    var review = _mapper.Map<Review>(reviewForm);
    review.UserId = userId;
    var result = await _repositoryWrapper.Review.Add(review);
    if (result == null)
    {
        return (null, "Failed to create review");
    }
    return (_mapper.Map<ReviewDto>(result), null);
      
}

public async Task<(List<ReviewDto> reviews, int? totalCount, string? error)> GetAll(ReviewFilter filter)
    {
        var (reviews,totalCount) = await _repositoryWrapper.Review.GetAll<ReviewDto>(
            x=> (filter.ItemId == null || x.ItemId == filter.ItemId)&&
                (filter.Rating == null || x.Rating >= filter.Rating)&& 
                (filter.UserId == null || x.UserId == filter.UserId),
            filter.PageNumber,
            filter.PageSize);
            return (reviews, totalCount, null);
            
            
       
    }

public async Task<(ReviewDto? review, string? error)> Update(Guid id ,ReviewUpdate reviewUpdate, Guid userId)
    {
        var review = await _repositoryWrapper.Review.Get(x => x.Id == id);
        if (review == null) return (null, "Review not found");
       if (review.UserId != userId) return (null, "You are not authorized to update this review");
        if (reviewUpdate.Rating < 0 || reviewUpdate.Rating > 5)
        {
            return (null, "Rating must be between 0 and 5");
        }
        _mapper.Map(reviewUpdate, review);
        var result = await _repositoryWrapper.Review.Update(review);
        if (result == null) return (null, "Error in updating review");
        return (_mapper.Map<ReviewDto>(result), null);
      
    }

public async Task<(ReviewDto? review, string? error)> Delete(Guid id)
    {
        var review = await _repositoryWrapper.Review.Get(x => x.Id == id);
        if (review == null) return (null, "Review not found");
        var result = await _repositoryWrapper.Review.SoftDelete(id);
        if (result == null) return (null, "Error in deleting review");
        return (_mapper.Map<ReviewDto>(result), null);
   
    }

}
