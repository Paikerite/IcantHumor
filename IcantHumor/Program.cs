using IcantHumor.Data;
using IcantHumor.Services;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor;
using MudBlazor.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<IHDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IcantHumorDBConnection"),
    o=>o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.BackgroundBlurred = true;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
});

builder.Services.AddAuthorizationCore();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IcantHumorAPI", Version = "v1" });
});

builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IMediaFilesService, MediaFilesService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IReactedUserService, ReactedUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseForwardedHeaders();

app.UseStaticFiles();

app.MapControllers();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
