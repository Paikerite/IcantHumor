using IcantHumor.Data;
using IcantHumor.Models;
using IcantHumor.Services;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MudBlazor;
using MudBlazor.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<IHDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IcantHumorDBConnection"),
    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddOptions();
builder.Services.Configure<OAuthDiscordConfig>(builder.Configuration.GetSection("DiscordOAuth"));
builder.Services.Configure<OAuthGoogleConfig>(builder.Configuration.GetSection("GoogleOAuth"));

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.BackgroundBlurred = true;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
});
//builder.Services.AddAuthentication(options =>
//{
//    // This forces challenge results to be handled by Google OpenID Handler, so there's no
//    // need to add an AccountController that emits challenges for Login.
//    options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
//    // This forces forbid results to be handled by Google OpenID Handler, which checks if
//    // extra scopes are required and does automatic incremental auth.
//    options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
//    // Default scheme that will handle everything else.
//    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie()

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
builder.Services.AddScoped<IFavouritesService, FavouritesService>();
builder.Services.AddScoped<IMediaFilesService, MediaFilesService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IGoogleAPIService, GoogleAPIService>();
builder.Services.AddScoped<IDiscordAPIService, DiscordAPIService>();
builder.Services.AddScoped<IReactedUserService, ReactedUserService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
