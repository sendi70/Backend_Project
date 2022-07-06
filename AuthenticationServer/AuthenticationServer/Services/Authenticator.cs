using AuthenticationServer.Models;
using AuthenticationServer.Models.Responses;
using System.Threading.Tasks;

namespace AuthenticationServer.Services
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator _AccessTokenGenerator;
        private readonly RefreshTokenGenerator _RefreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public Authenticator(IRefreshTokenRepository refreshTokenRepository, RefreshTokenGenerator refreshTokenGenerator, AccessTokenGenerator accessTokenGenerator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _RefreshTokenGenerator = refreshTokenGenerator;
            _AccessTokenGenerator = accessTokenGenerator;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(User user)
        {
            string accessToken = _AccessTokenGenerator.GenerateToken(user);
            string refreshToken = _RefreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken()
            {
                Token = accessToken,
                UserId = user.Id,
            };
            await _refreshTokenRepository.Create(refreshTokenDTO);

            return new AuthenticatedUserResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

    }
}
