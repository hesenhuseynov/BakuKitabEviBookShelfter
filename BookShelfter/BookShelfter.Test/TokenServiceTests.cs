using System;
using System.Collections.Generic;
using BookShelfter.Application.Abstractions.Token;
using BookShelfter.Domain.Entities.Identity;
using BookShelfter.Infrastructure.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BookShelfter.Test
{
    public class TokenServiceTests
    {
        private readonly ITokenService _tokenService;
        private readonly AppUser _testUser;
        private readonly IList<string> _roles;
        private readonly Mock<ILogger<TokenService>> _loggerMock;

        public TokenServiceTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:Key", "BuCokGizliVeGuvenliBirAnahtarOlmali12345" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _loggerMock = new Mock<ILogger<TokenService>>();
            _tokenService = new TokenService(configuration, _loggerMock.Object);
            _testUser = new AppUser { Id = "123", Email = "test@example.com", UserName = "testUser" };
            _roles = new List<string> { "Admin" };
        }

        [Fact]
        public void CreateAccesToken_ShouldSetCorrectExpirationTime()
        {
            int accessTokenLifeTime = 3;
            var token = _tokenService.CreateAccessToken(accessTokenLifeTime, _testUser, _roles);

            Assert.NotNull(token);
            Assert.True(token.Expiration > DateTime.UtcNow, "Token expiration should be in the future");
            Assert.True(token.Expiration <= DateTime.UtcNow.AddMinutes(accessTokenLifeTime), "Token expiration time should be within the expected lifetime");

            _loggerMock.Verify(x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Token created with expiration")),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.AtLeastOnce);

            Console.WriteLine($"Token Expires at: {token.Expiration}");
            Console.WriteLine($"Current Utc Time: {DateTime.UtcNow}");
            Console.WriteLine($"Expected Expiration Time: {DateTime.UtcNow.AddMinutes(accessTokenLifeTime)}");
        }
    }
}
