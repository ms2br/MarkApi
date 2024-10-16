using AutoMapper;
using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.CORE.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Profiles
{
    public class AppUserMapping:Profile
    {
        public AppUserMapping()
        {
            CreateMap<RegisterDto, AppUser>().ReverseMap();
            CreateMap<AppUser, UserDto>();
        }
    }
}
