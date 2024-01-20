using AutoMapper;
using RozetkaBackEnd.Core.Dtos.User;
using RozetkaBackEnd.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Mapper.user
{
    public class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile() 
        {
            CreateMap<RegisterUserDto, AppUser>().ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
        }
    }
}
