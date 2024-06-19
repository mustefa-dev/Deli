
using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;
using Deli.Repository;
using Deli.Services;

namespace Deli.Services;


public interface INewsServices
{
    Task<(News? news, string? error)> Create(NewsForm newsForm, string language);
    Task<(List<NewsDto> newss, int? totalCount, string? error)> GetAll(NewsFilter filter, string language);
    Task<(News? news, string? error)> GetById(Guid id, string language);
    Task<(News? news, string? error)> Update(Guid id, NewsUpdate newsUpdate, string language);
    Task<(News? news, string? error)> Delete(Guid id, string language);
}

public class NewsServices : INewsServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public NewsServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(News? news, string? error)> Create(NewsForm newsForm, string language)
    {
        var news = _mapper.Map<News>(newsForm);
        var response = await _repositoryWrapper.News.Add(news);
        return response == null ? (null, ErrorResponseException.GenerateLocalizedResponse("Error in Creating a News", "خطأ في انشاء الخبر", language)) : (response, null);
    }

    public async Task<(News? news, string? error)> GetById(Guid id, string language)
    {
        var news = await _repositoryWrapper.News.GetById(id);
        if (news == null) return (null, ErrorResponseException.GenerateLocalizedResponse("News not found", "لم يتم العثور على الخبر", language));
        return (news, null);
    }
    public async Task<(List<NewsDto> newss, int? totalCount, string? error)> GetAll(NewsFilter filter, string language)
    {
        var (news, totalCount) = await _repositoryWrapper.News.GetAll<NewsDto>(
            x => (x.Title.Contains(filter.Title) || filter.Title==null) &&
            (x.ArTitle.Contains(filter.ArTitle) || filter.ArTitle==null)
            //&& (x.isMain == filter.isMain || filter.isMain == null)
            ,
            filter.PageNumber, filter.PageSize);
        foreach (var ne in news)
        {
            var original = await _repositoryWrapper.News.GetById(ne.Id);
            ne.Title = ErrorResponseException.GenerateLocalizedResponse(original.Title, original.ArTitle, language);
            ne.Description = ErrorResponseException.GenerateLocalizedResponse(original.Description, original.ArDescription, language);
            
        }
        return (news, totalCount, null);
    }

    public async Task<(News? news, string? error)> Update(Guid id, NewsUpdate newsUpdate, string language)
    {
        var     news = await _repositoryWrapper.News.GetById(id);
        if (news == null) return (null, ErrorResponseException.GenerateLocalizedResponse("News not found", "لم يتم العثور على الخبر", language));
        news = _mapper.Map(newsUpdate, news);
        news = await _repositoryWrapper.News.Update(news, id);
        return (news, null);
    }

    public async Task<(News? news, string? error)> Delete(Guid id, string language)
    {
        var news = await _repositoryWrapper.News.GetById(id);
        if (news == null) return (null, ErrorResponseException.GenerateLocalizedResponse("News not found", "لم يتم العثور على الخبر", language));
        await _repositoryWrapper.News.SoftDelete(id);
        return (news, null);
    }
}
