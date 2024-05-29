using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Deli.Interface;
using Deli.Services;

namespace Deli.Services
{
    public interface ILikedServices
    {
      
    }
}

public class LikedServices : ILikedServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public LikedServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }
    
}
