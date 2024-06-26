﻿using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using MCA.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MCA.Services.Identity.Services
{
    public class ProfileService : IProfileService
    {

        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimPrincipalFactory;
        private readonly UserManager<ApplicationUser> _userManager;        
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProfileService(
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimPrincipalFactory, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userClaimPrincipalFactory = userClaimPrincipalFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        async Task IProfileService.GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user= await _userManager.FindByIdAsync(sub);  
            ClaimsPrincipal userClaims= await _userClaimPrincipalFactory.CreateAsync(user);
            
            List<Claim> claims = userClaims.Claims.ToList();
            claims=claims.Where(claim=> context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
            
            if (_userManager.SupportsUserRole)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        IdentityRole role =await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        { 
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));   
                        }
                    }
                }
            }

            context.IssuedClaims= claims;            
        }

        async Task IProfileService.IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
