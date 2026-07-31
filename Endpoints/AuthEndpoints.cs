using GamersDock.Entities;
using GamersDock.Dtos;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GamersDock.Endpoints
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth");

            // POST: /api/auth/register
            group.MapPost("/register", async (UserManager<Users> userManager, RegisterRequest dto) =>
            {
                // DTO validation
                var validationResults = new List<ValidationResult>();
                var ctx = new ValidationContext(dto);
                if (!Validator.TryValidateObject(dto, ctx, validationResults, validateAllProperties: true))
                {
                    var errors = validationResults
                        .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                        .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }

                var user = new Users
                {
                    UserName = dto.UserName
                };

                var result = await userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    return Results.Ok(new { message = "User registered successfully" });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToArray();
                    return Results.BadRequest(new { Errors = errors });
                }
            });

            // POST: /api/auth/login
            group.MapPost("/login", async (SignInManager<Users> signInManager, LoginRequest dto) =>
            {
                var validationResults = new List<ValidationResult>();
                var ctx = new ValidationContext(dto);
                if (!Validator.TryValidateObject(dto, ctx, validationResults, validateAllProperties: true))
                {
                    var errors = validationResults
                        .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "")
                        .ToDictionary(g => g.Key, g => g.Select(r => r.ErrorMessage).ToArray());
                    return Results.ValidationProblem(errors);
                }

                var result = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Results.Ok(new { message = "Login successful" });
                }
                else
                {
                    return Results.Unauthorized();
                }
            });

            // POST: /api/auth/logout
            group.MapPost("/logout", async (SignInManager<Users> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok(new { message = "Logout successful" });
            });

            return app;
        }
    }
}