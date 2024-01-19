using Microsoft.AspNetCore.Identity;
using RozetkaBackEnd.Core.Dtos.User;
using RozetkaBackEnd.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozetkaBackEnd.Core.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

                return new ServiceResponse
                {
                    Success = true,

                    Message = "You SignIn",
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

    }
    
}
