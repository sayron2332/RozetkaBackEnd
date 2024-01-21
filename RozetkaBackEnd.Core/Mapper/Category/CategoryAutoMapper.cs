using AutoMapper;
using RozetkaBackEnd.Core.Dtos.Category;
using RozetkaBackEnd.Core.Entites.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Mapper.Category
{
    public class CategoryAutoMapper : Profile
    {
        public CategoryAutoMapper()
        {
            CreateMap<CategoryDto, AppCategory>().ReverseMap();
        }
    }
}
