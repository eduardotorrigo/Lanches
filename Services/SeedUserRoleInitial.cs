using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedRoles()
    {
        if(!_roleManager.RoleExistsAsync("Member").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Member";
            role.NormalizedName = "MEMBER";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
        if(!_roleManager.RoleExistsAsync("Admin").Result)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            role.NormalizedName = "ADMIN";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUsers()
    {
        if(_userManager.FindByEmailAsync("usuario@local.com").Result == null)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = "usuario@local.com";
            user.NormalizedUserName = "USUARIO@LOCAL.COM";
            user.Email = "usuario@local.com";
            user.NormalizedEmail = "USUARIO@LOCAL.COM";
            user.EmailConfirmed = true;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult userResult = _userManager.CreateAsync(user, "Numsey#2022").Result;

            if(userResult.Succeeded)
            {
                var result = _userManager.AddToRoleAsync(user, "Member");
            }
        }
        if(_userManager.FindByEmailAsync("admin@local.com").Result == null)
        {
            IdentityUser user = new IdentityUser();
            user.UserName = "admin@local.com";
            user.NormalizedUserName = "ADMIN@LOCAL.COM";
            user.Email = "admin@local.com";
            user.NormalizedEmail = "ADMIN@LOCAL.COM";
            user.EmailConfirmed = true;
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult userResult = _userManager.CreateAsync(user, "Numsey#2022").Result;

            if(userResult.Succeeded)
            {
                _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
