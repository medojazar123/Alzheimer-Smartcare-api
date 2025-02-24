using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.models;

namespace api.Mappers
{
    public static class AccountMapper
    {
        public static RegisterResponseDto toRegisterResponseDto(this AppUser user,string? token)
        {
            return new RegisterResponseDto
            {
                Fullname = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                Token = token,
            };
        }
    }
}