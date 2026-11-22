using Impacta.GestaoFrota.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Impacta.GestaoFrota.UI.Security;

public static class IdentityDataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<FrotaContext>();
        await context.Database.EnsureCreatedAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await EnsureRoleAsync(roleManager, RoleNames.Administrador);
        await EnsureRoleAsync(roleManager, RoleNames.Funcionario);

        var adminEmail = configuration["IdentitySeed:AdminEmail"] ?? "admin@impacta.local";
        var adminPassword = configuration["IdentitySeed:AdminPassword"] ?? "Admin@123";
        var funcionarioEmail = configuration["IdentitySeed:FuncionarioEmail"] ?? "funcionario@impacta.local";
        var funcionarioPassword = configuration["IdentitySeed:FuncionarioPassword"] ?? "Funcionario@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Erro ao criar usuário administrador: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(adminUser, RoleNames.Administrador))
        {
            var result = await userManager.AddToRoleAsync(adminUser, RoleNames.Administrador);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Erro ao associar papel de administrador: {errors}");
            }
        }

        var funcionarioUser = await userManager.FindByEmailAsync(funcionarioEmail);
        if (funcionarioUser is null)
        {
            funcionarioUser = new ApplicationUser
            {
                UserName = funcionarioEmail,
                Email = funcionarioEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(funcionarioUser, funcionarioPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Erro ao criar usuário funcionário: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(funcionarioUser, RoleNames.Funcionario))
        {
            var result = await userManager.AddToRoleAsync(funcionarioUser, RoleNames.Funcionario);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Erro ao associar papel de funcionário: {errors}");
            }
        }
    }

    private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (await roleManager.RoleExistsAsync(roleName))
        {
            return;
        }

        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(error => error.Description));
            throw new InvalidOperationException($"Erro ao criar papel '{roleName}': {errors}");
        }
    }
}
