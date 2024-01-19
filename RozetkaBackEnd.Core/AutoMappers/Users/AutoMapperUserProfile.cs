using AutoMapper;
using RozetkaBackEnd.Core.Dtos.User;
using RozetkaBackEnd.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.AutoMappers.Users
{
    internal class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<LoginUserDto,AppUser> ();
        }
    }
}
