using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RozetkaBackEnd.Core.Dtos.User;
using RozetkaBackEnd.Core.Entites.Token;
using RozetkaBackEnd.Core.Entites.User;
using RozetkaBackEnd.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly IMapper _autoMapper;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtService jwtService, IMapper autoMapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _autoMapper = autoMapper;
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginUserDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found or email or password incorrect",
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var tokens = await _jwtService.GenerateJwtTokensAsync(user);

                return new ServiceResponse
                {
                    AccessToken = tokens.Token,
                    RefreshToken = tokens.refreshToken.Token,
                    Message = "Logged in successfully",
                    Success = true,
                };
               
            }
            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }
            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is locked out. Connect with site administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }

        private async Task<string> CreateImageUser(IFormFile file)
        {
            string extensions = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            string fileName = DateTime.Now.Ticks.ToString() + extensions;

            string exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Images\\User", fileName);

            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }
        public async Task<ServiceResponse> RegisterUserAsync(RegisterUserDto newUser)
        {
            AppUser user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "this user already exist",
                };
            }

            string fileName = await CreateImageUser(newUser.ImageFile);
            
            AppUser mappedUser = _autoMapper.Map<AppUser>(newUser);

            mappedUser.Image = fileName;

            var result = await _userManager.CreateAsync(mappedUser);
            if (result.Succeeded)
            {
              
                return new ServiceResponse
                {
                    Success = true,
                    Message = "user successfully created",
                    
                };
            }

            File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "Images\\User", fileName));

            return new ServiceResponse
            {
                Success = false,
                Message = "Error",
                Errors = result.Errors
            };

        }

        public async Task<ServiceResponse> LogoutAsync()
        {
            IEnumerable<RefreshToken> tokens = await _jwtService.GetAll();
            foreach (RefreshToken token in tokens)
            {
                await _jwtService.Delete(token);
            }

            await _signInManager.SignOutAsync();
            return new ServiceResponse
            {
                Success= true,
                Message = "User succes logout"

            };
        }

        public async Task<ServiceResponse> DeleteAsync(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User deleted"

                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User not found"

            };

        }
        public async Task<ServiceResponse> UpdateAsync(EditUserDto updateUser)
        {
            AppUser user = await _userManager.FindByEmailAsync(updateUser.Email);

            return new ServiceResponse
            {
                Success = false,
                Message = "User not found"

            };

        }

    }
    
}
