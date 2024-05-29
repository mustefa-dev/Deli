using Deli.Helpers;
using Deli.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Deli.DATA.DTOs;
using Deli.Entities;
using Deli.Services;

namespace Deli.Controllers
{
    public class LikedController : BaseController
    {
        private readonly ILikedServices _likedServices;

        public LikedController(ILikedServices likedServices)
        {
            _likedServices = likedServices;
        }
        
        
    }
}
