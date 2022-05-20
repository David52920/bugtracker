using bugtracker.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace bugtracker.Extensions{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor) {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user) {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FirstName", user.FirstName ?? ""));
            identity.AddClaim(new Claim("LastName", user.LastName ?? ""));
            identity.AddClaim(new Claim("UserName", user.UserName ?? ""));
            return identity;
        }
    } 
}