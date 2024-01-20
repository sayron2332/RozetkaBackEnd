﻿using AutoMapper;
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

            AppUser mappedUser = _autoMapper.Map<AppUser>(newUser);

            var result = await _userManager.CreateAsync(mappedUser);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "user successfully created",
                    
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "Error",
                Errors = result.Errors
            };

        }

    }
    
}
