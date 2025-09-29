using Microsoft.AspNetCore.Identity;

namespace BookStoreApi.Repositories
{
        public interface ITokenRepository
        {
            string CreateJWTToken(IdentityUser user, List<string> roles);
        }
}
