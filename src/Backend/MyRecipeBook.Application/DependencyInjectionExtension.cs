using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services); // configura o AutoMapper
        AddUseCases(services); 
        AddPasswordEncripter(services); 
    }
    private static void AddAutoMapper(IServiceCollection services)
    {
        var autoMapper = new MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping()); 
        }).CreateMapper();
        
        services.AddScoped(options => autoMapper);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }    private static void AddPasswordEncripter(IServiceCollection services)
    {
        services.AddScoped(options => new PasswordEncripter());
    }
}