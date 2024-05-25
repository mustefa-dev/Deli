
using AutoMapper;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Interface;
using Deli.Repository;
using Deli.Services;

namespace Deli.Services;


public interface INewsServices
{
    Task<(News? news, string? error)> Create(NewsForm newsForm);
    Task<(List<NewsDto> newss, int? totalCount, string? error)> GetAll(NewsFilter filter);
    Task<(News? news, string? error)> GetById(Guid id);
    Task<(News? news, string? error)> Update(Guid id, NewsUpdate newsUpdate);
    Task<(News? news, string? error)> Delete(Guid id);
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


    public async Task<(News? news, string? error)> Create(NewsForm newsForm)
    {
        var news = _mapper.Map<News>(newsForm);
        var response = await _repositoryWrapper.News.Add(news);
        return response == null ? (null, "News Couldn't add") : (response, null);
    }

    public async Task<(News? news, string? error)> GetById(Guid id)
    {
        var news = await _repositoryWrapper.News.GetById(id);
        if (news == null) return (null, "News not found");
        return (news, null);
    }
    public async Task<(List<NewsDto> newss, int? totalCount, string? error)> GetAll(NewsFilter filter)
    {
        var (news, totalCount) = await _repositoryWrapper.News.GetAll<NewsDto>(
            x => (x.Title.Contains(filter.Title) || filter.Title==null) //&& (x.isMain == filter.isMain || filter.isMain == null)
            ,
            filter.PageNumber, filter.PageSize);        
        return (news, totalCount, null);
    }

    public async Task<(News? news, string? error)> Update(Guid id, NewsUpdate newsUpdate)
    {
        var     news = await _repositoryWrapper.News.GetById(id);
        if (news == null) return (null, "News not found");
        news = _mapper.Map(newsUpdate, news);
        news = await _repositoryWrapper.News.Update(news, id);
        return (news, null);
    }

    public async Task<(News? news, string? error)> Delete(Guid id)
    {
        var news = await _repositoryWrapper.News.GetById(id);
        if (news == null) return (null, "News not found");
        await _repositoryWrapper.News.SoftDelete(id);
        return (news, null);
    }
}
