
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IFeedBackServices
{
Task<(FeedBack? feedback, string? error)> Create(FeedBackForm feedbackForm, string language);
Task<(List<FeedBackDto> feedbacks, int? totalCount, string? error)> GetAll(FeedBackFilter filter, string language);
Task<(FeedBack? feedback, string? error)> GetById(Guid id, string language);
Task<(FeedBack? feedback, string? error)> Update(Guid id , FeedBackUpdate feedbackUpdate, string language);
Task<(FeedBack? feedback, string? error)> Delete(Guid id, string language);
}

public class FeedBackServices : IFeedBackServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public FeedBackServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(FeedBack? feedback, string? error)> Create(FeedBackForm feedbackForm, string language)
{
    var feedback = _mapper.Map<FeedBack>(feedbackForm);
    feedback = await _repositoryWrapper.FeedBack.Add(feedback);

    if (feedback != null)
    {
        var emailService = new EmailService();
        await emailService.SendEmail(feedback.Email, "Feedback Received", "We have received your feedback. Thank you!");
    }

    return (feedback, null);
}

public async Task<(FeedBack? feedback, string? error)> GetById(Guid id, string language)
    {
        var feedback = await _repositoryWrapper.FeedBack.GetById(id);
        if (feedback == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Feedback not found", "لم يتم العثور على الرسالة", language));
        return (feedback, null);
    }
public async Task<(List<FeedBackDto> feedbacks, int? totalCount, string? error)> GetAll(FeedBackFilter filter, string language)
    {
        var (feedbacks, totalCount) = await _repositoryWrapper.FeedBack.GetAll<FeedBackDto>(
            x =>
                (filter.Fullname == null || x.Fullname.Contains(filter.Fullname)) &&
                (filter.PhoneNumber == null || x.PhoneNumber == filter.PhoneNumber) &&
                (filter.Email == null || x.Email.Contains(filter.Email))
            ,
            filter.PageNumber, filter.PageSize);
        return (feedbacks, totalCount, null);
    }

public async Task<(FeedBack? feedback, string? error)> Update(Guid id ,FeedBackUpdate feedbackUpdate, string language)
    {
        var feedback = await _repositoryWrapper.FeedBack.GetById(id);
        if (feedback == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Feedback not found", "لم يتم العثور على الرسالة", language));
        feedback = _mapper.Map(feedbackUpdate, feedback);
        feedback = await _repositoryWrapper.FeedBack.Update(feedback, id);
        return (feedback, null);
        
    }

public async Task<(FeedBack? feedback, string? error)> Delete(Guid id, string language)
    {
        var feedback = await _repositoryWrapper.FeedBack.GetById(id);
        if (feedback == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Feedback not found", "لم يتم العثور على الرسالة", language));
        await _repositoryWrapper.FeedBack.SoftDelete(id);
        return (feedback, null);
    }

}
