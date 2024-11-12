using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;
using BookShelfter.Application.Features.Commands.AppUser.LoginUser;
using FluentValidation;
using MediatR;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
{
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var loginDto = new LoginDto()
        {
            UserName = request.UserName,
            Password = request.Password
        };

        var result = await _authService.LoginUserAsync(loginDto, 2);

        if (!result.Succes)
        {
            return new LoginUserCommandResponse
            {
                Success = false,
                Message = result.Message ?? "Giriş uğursuz oldu. xaiş edirik yeniden  cehd edin "
            };
        }

        var dataResult = result as IDataResult<Token>;
        if (dataResult?.Data == null)
        {
            return new LoginUserCommandResponse
            {
                Success = false,
                Message = "Token oluşturulamadı. Lütfen tekrar deneyin."
            };
        }

        return new LoginUserCommandResponse
        {
            Success = true,
            Message = "Login Successful",
            Token = dataResult.Data
        };
    }
}

