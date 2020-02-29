using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using mvp.identity.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace mvp.identity.Services
{
    public class CustomProfileService : IProfileService
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //public CustomProfileService(UserManager<ApplicationUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //var user = _userManager.GetUserAsync(context.Subject).GetAwaiter().GetResult();
            //var claims = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult();
            var claims = context.Subject.Claims.Where(s => s.Type == JwtClaimTypes.Id);
            context.IssuedClaims.AddRange(claims);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            //var user = _userManager.GetUserAsync(context.Subject).GetAwaiter().GetResult();
            context.IsActive = true;// user != null && user.LockoutEnd == null;
            return Task.CompletedTask;
        }
    }
}
