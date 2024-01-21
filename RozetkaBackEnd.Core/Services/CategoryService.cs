using AutoMapper;
using RozetkaBackEnd.Core.Dtos.Category;
using RozetkaBackEnd.Core.Entites.Category;

using RozetkaBackEnd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<AppCategory> _categoryRepo;
        private readonly IMapper _mapper;
        public CategoryService(IRepository<AppCategory> categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> Create(CategoryDto category)
        {
            await _categoryRepo.Insert(_mapper.Map<AppCategory>(category));
            await _categoryRepo.Save();
            return new ServiceResponse
            {
                Success = true,
                Message = "category succes created"
            };
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            await _categoryRepo.Delete(id);
            await _categoryRepo.Save();
            return new ServiceResponse
            {
                Success = true,
                Message = "category succes deleted"
            };
        }

        public async Task<ServiceResponse> Get(int id)
        {
            AppCategory appCategory = await _categoryRepo.GetByID(id);
            CategoryDto category = _mapper.Map<CategoryDto>(appCategory);
            return new ServiceResponse
            {
                Success = true,
                Message = "category succes get",
                Payload = category
            };
        }

        public async Task<ServiceResponse> GetAll()
        {
            var appCategories = await _categoryRepo.GetAll();
            var categories = _mapper.Map<CategoryDto>(appCategories);
            return new ServiceResponse
            {
                Success = true,
                Message = "categories succes download",
                Payload = categories
            };
        }

        public async Task<ServiceResponse> Update(CategoryDto category)
        {
            await _categoryRepo.Update(_mapper.Map<AppCategory>(category));
            await _categoryRepo.Save();
            return new ServiceResponse
            {
                Success = true,
                Message = "categories succes updated",
            };
        }
    }
}
