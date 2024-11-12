using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Abstractions.Services.Authentications;
using BookShelfter.Application.Abstractions.Token;
using BookShelfter.Application.Common;
using BookShelfter.Application.DTOs;
using BookShelfter.Application.Repositories.Customer;
using BookShelfter.Domain.Entities;
using BookShelfter.Domain.Entities.Identity;
using BookShelfter.Persistence.Contexts;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly BookShelfterDbContext _context;
    private readonly IEmailService _emailService;
    public readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;
    private readonly ICustomerWriteRepository _customerWriteRepository;
    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, BookShelfterDbContext context, IEmailService emailService, IConfiguration configuration, ILogger<AuthService> logger, ICustomerWriteRepository customerWriteRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
        _emailService = emailService;
        _configuration = configuration;
        _logger = logger;
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<DataResult<Token>> RefreshTokenLoginAsync(string refreshToken)
    {
        {
            var utcNow = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            var user = await _userManager.Users.SingleOrDefaultAsync(u =>
                u.RefreshToken == refreshToken && u.RefreshTokenEndDate > utcNow);


            if (user == null)
            {
                return new DataResult<Token>(null, false, "Invalid refresh token or token expired.");
            }


            var roles = await _userManager.GetRolesAsync(user);
            var newAcessToken = _tokenService.CreateAccessToken(30, user, roles);


            var newRefreshToken = _tokenService.CreateRefreshToken();


            user.RefreshToken = newRefreshToken;
            user.RefreshTokenEndDate = DateTime.UtcNow.AddDays(7);


            await _userManager.UpdateAsync(user);


            var token = new Token
            {
                AccessToken = newAcessToken.AccessToken,
                Expiration = newAcessToken.Expiration,
                RefreshToken = newRefreshToken
            };


            return new DataResult<Token>(token, true, "Refresh token generated succeffuly");



        }
    }

    public async Task PasswordResetAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool> VerifyResetToken(string resetToken, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        return result;

    }

    public async Task<IResult> RegisterAsync(RegisterDto registerDto, int accessTokenLifeTime)
    {
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {

                var exisTingUser = await _userManager.FindByEmailAsync(registerDto.Email);

                if (exisTingUser != null)
                {
                    return new Result(false, "This email is already registered");
                }

                var user = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    NameSurName = registerDto.NameSurname,
                    Id = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    return new Result(false,
                        "User creation failed: " + string.Join(",", result.Errors.Select(e => e.Description)));
                }

                var customer = new Customer
                {
                    Name = registerDto.NameSurname,
                    Email = registerDto.Email,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber, // Lazım olsa başka məlumatlar da əlavə edəcəyəm  və altda default bir adress təyin edirəm 
                    Address = "Default Address" 
                };

                await _customerWriteRepository.AddAsync(customer);
                await _customerWriteRepository.SaveAsync();



                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var baseUrl = _configuration["Application:BaseUrl"];
                var confirmationLink = $"{baseUrl}/api/auth/confirmemail?token={Uri.EscapeDataString(emailConfirmationToken)}&email={Uri.EscapeDataString(user.Email)}";
                //var confirmationLink = $"{baseUrl}/confirmation-success?token={Uri.EscapeDataString(emailConfirmationToken)}&email={Uri.EscapeDataString(user.Email)}";

                //var confirmationLink = $"{baseUrl}/confirmation-success";


                await _emailService.SendEmailAsync(user.Email, "Email Confirmation", $"Please confirm your email by clicking this link: {confirmationLink}");

                scope.Complete();


                return new Result(true, "User registered successfully. Please check your email to confirm your email address.");

            }
            catch (Exception ex)
            {
                return new Result(false, "An error occurred: " + ex.Message);
            }
        }
    }


    public async Task<IResult> LoginUserAsync(LoginDto loginDto, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user == null)
            return new DataResult<Token>(null, false, "Invalid username or password ");

        if (!user.EmailConfirmed)
        {
            return new DataResult<Token>(null, false, "Email not confirmed. Please check you email you account.");
        }






        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);



        //var result = await _userManager.CheckPasswordAsync(user, loginDto.UserName, loginDto.Password, false, false);



        //var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        //if (!isPasswordValid)
        //{
        //    return new DataResult<Token>(null, false, "Invalid username or password");

        //}


        if (!result.Succeeded)
        {

            return new DataResult<Token>(null, false, "Invalid username or password.");
        }


        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenService.CreateAccessToken(accessTokenLifeTime, user, roles);

        var refreshToken = _tokenService.CreateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenEndDate = DateTime.UtcNow.AddMinutes(15);
        await _userManager.UpdateAsync(user);




        //return new DataResult<Token>(token, true, "Login successful.");

        return new DataResult<Token>(new Token
        {
            AccessToken = token.AccessToken,
            Expiration = token.Expiration,
            RefreshToken = refreshToken

        }, true, "Login Successful");

    }

    public async Task<IResult> ConfirmEmailAsyncAndCreateToken(string token, string email, int accessTokenLifeTime)
    {
        var user = await _userManager.FindByEmailAsync(email);


        if (user is null)
        {
            return new Result(false, "Invalid email adress");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (result.Succeeded)
        {
            user.EmailConfirmed = true;

            try
            {
                var updatedUser = await _userManager.UpdateAsync(user);

                if (!updatedUser.Succeeded)
                {
                    _logger.LogError("ConfirmEmailCommandHandler: Failed to update user {UserId} after confirming email", user.Id);

                    return new Result(false, "Failed to update user after email confirmation");
                }
                _logger.LogInformation("ConfirmEmailCommandHandler: User {UserId} email confirmed successfully", user.Id);

                var roles = await _userManager.GetRolesAsync(user);
                var jwtToken = _tokenService.CreateAccessToken(accessTokenLifeTime, user, roles);


                var redirectUrl = $"{_configuration["Application:AngularBaseUrl"]}/confirmation-success?token={jwtToken.AccessToken}";
                //return Result.Redirect(redirectUrl);

                return new Result(true, "Email confirmed successfully. Redirecting...", redirectUrl); 

                //return new DataResult<Token>(jwtToken, true, "Email confirmed and token generated succesfully.");

            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "ConfirmEmailCommandHandler: Concurrency issue when updating user {UserId}", user.Id);
                return new Result(false, "A concurrency issue occurred while updating your information. Please try again.");

            }
        }

        _logger.LogWarning("AuthService: Email confirmation failed for user {UserId} with token {Token}", user.Id, token);
        return new Result(false, "Email confirmation failed. Invalid token or token has expired.");




    }

    public async Task<IDataResult<Token>> LoginGoogleAsync(string idToken)
    {
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string> { _configuration["Google:ClientId"] }
                });

            var user = await _userManager.FindByEmailAsync(payload.Email);

            if (user == null)
            {
                var baseUserName = payload.Email.Substring(0, payload.Email.IndexOf("@"));
                var uniqueUserName = baseUserName;
                int counter = 1;

                while (await _userManager.FindByNameAsync(uniqueUserName) != null)
                {
                    uniqueUserName = $"{baseUserName}{counter}";
                    counter++;
                }

                user = new AppUser
                {
                    Email = payload.Email,
                    UserName = uniqueUserName,
                    NameSurName = payload.Name,
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString()
                };



                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return new ErrorDataResult<Token>(null, "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }

                var customer = new Customer
                {
                    Name = payload.Name,
                    Email = payload.Email,
                    AppUserId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                    Address = "Default Adress"
                };

                await _customerWriteRepository.AddAsync(customer);

                await _customerWriteRepository.SaveAsync();




            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateAccessToken(30, user, roles);

            return new SuccessDataResult<Token>(token, "Google login successful");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error occurred while saving user to database.");
            return new ErrorDataResult<Token>(null, "Error occurred while saving user to database: " + ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Google login failed.");
            return new ErrorDataResult<Token>(null, "Google login failed: " + ex.Message);
        }
    }



    //public async Task<IDataResult<Token>> LoginGoogleAsync(string idToken)
    //{
    //    try
    //    {
    //        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken,
    //            new GoogleJsonWebSignature.ValidationSettings
    //            {
    //                Audience = new List<string> { _configuration["Google:ClientId"] }

    //            });



    //        var user = await _userManager.FindByEmailAsync(payload.Email);


    //        if (user == null)
    //        {
    //            user = new AppUser
    //            {
    //                Email = payload.Email,
    //                UserName = payload.Email.Substring(0, payload.Email.IndexOf("@")),
    //                NameSurName = payload.Name,
    //                EmailConfirmed = true,
    //                Id = Guid.NewGuid().ToString()
    //            };


    //            var result = await _userManager.CreateAsync(user);
    //            if (!result.Succeeded)
    //            {
    //                return new ErrorDataResult<Token>(null, "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    //            }
    //        }

    //        var roles = await _userManager.GetRolesAsync(user);
    //        var token = _tokenService.CreateAccessToken(30, user, roles);

    //        return new SuccessDataResult<Token>(token, "Google login Succefful");




    //    }

    //    catch (DbUpdateException ex)
    //    {
    //        _logger.LogError(ex, "Error occurred while saving user to database.");
    //        return new ErrorDataResult<Token>(null, "Error occurred while saving user to database: " + ex.InnerException?.Message);
    //    }

    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, "Google login failed.");
    //        return new ErrorDataResult<Token>(null, "Google login failed: " + ex.Message);
    //    }
    //}
}
