using RozetkaBackEnd.Core.Dtos.Category;
using RozetkaBackEnd.Core.Entites.Category;
using RozetkaBackEnd.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Interfaces
{
    internal interface ICategoryService
    {
        Task<ServiceResponse> GetAll();
        Task<ServiceResponse> Create(CategoryDto category);
        Task<ServiceResponse> Update(CategoryDto category);
        Task<ServiceResponse> Delete(int id);
        Task<ServiceResponse> Get(int id);


    }
}
