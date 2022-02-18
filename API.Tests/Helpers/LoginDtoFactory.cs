using Application.Dtos;
using System;

namespace API.Tests.Helpers
{
    public class LoginDtoFactory
    {
        public LoginDto CreateLoginDto(LoginRequest requestType)
        {
            switch (requestType)
            {
                case LoginRequest.InvalidBody:
                    return new LoginDto(null, null);
                case LoginRequest.InvalidPassword:
                case LoginRequest.InvalidEmail:
                case LoginRequest.EverythingOk:
                    return new LoginDto("valid@email.com", "password");
                default:
                    throw new NotImplementedException("This case is not implemented");
            }
        }
    }
}
