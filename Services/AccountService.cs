using Backend.Model;
using JwtSharp;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly JwtIssuer jwtIssuer;
        private readonly UserManager<IdentityUser> userManager;

        public AccountService(JwtIssuer jwtIssuer, UserManager<IdentityUser> userManager)
        {
            this.jwtIssuer = jwtIssuer;
            this.userManager = userManager;
        }

        public string IssueJwtTokenAsync(string username)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
            };

            var token = this.jwtIssuer.IssueToken(claims);

            return token;
        }

        public async Task<bool> IsPasswordCorrectAsync(string username, string password)
        {
            if (username == null)
                return false;
            if (password == null)
                return false;

            var user = await this.userManager.FindByNameAsync(username);
            var isPasswordCorrect = await this.userManager.CheckPasswordAsync(user, password);

            return isPasswordCorrect;
        }

        public async Task<UserModel> GetUserAsync(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            if (user == null)
                return null;

            var result = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = (await this.userManager.GetRolesAsync(user)).ToArray()
            };

            return result;
        }

    }
}