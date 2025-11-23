using BusinessLogic.DTOs.Account;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IJwtService jwtService;

        public AccountsService(IJwtService jwtService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task Register(RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User registration failed: {errors}");
            }
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
                throw new Exception("User not found");

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
                throw new Exception("Invalid login attempt");

            return new LoginResponse
            {
                AccessToken = await jwtService.GenerateTokenAsync(await jwtService.GetClaimsAsync(user))
            };
        }


        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

    }
}
