using Google.Apis.Auth.AspNetCore3;
using IcantHumor.Data;
using IcantHumor.Services;
using IcantHumor.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
builder.Services.AddAuthentication(options =>
{
    // This forces challenge results to be handled by Google OpenID Handler, so there's no
    // need to add an AccountController that emits challenges for Login.
    options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // This forces forbid results to be handled by Google OpenID Handler, which checks if
    // extra scopes are required and does automatic incremental auth.
    options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    // Default scheme that will handle everything else.
    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie()
.AddGoogleOpenIdConnect(options =>
{
    options.ClientId = "112990426089-bd2nau1l3dlgdjnl87q1tc8q4g8035sp.apps.googleusercontent.com";
    options.ClientSecret = "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";
});
//builder.Services.AddAuthentication(config =>
//{
//    config.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    config.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    config.DefaultChallengeScheme = "Google";
//}).AddCookie()
//  .AddOAuth("Google", options =>
//  {
//      options.ClientId = "112990426089-bd2nau1l3dlgdjnl87q1tc8q4g8035sp.apps.googleusercontent.com";
//      options.ClientSecret = "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";
//      options.CallbackPath = new PathString("/signin-google");
//      options.AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint;
//      options.TokenEndpoint = GoogleDefaults.TokenEndpoint;
//      options.UserInformationEndpoint = GoogleDefaults.UserInformationEndpoint;
//      options.ClaimActions.Clear();
//      options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
//      options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
//      options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
//      options.SaveTokens = true;
//      options.Events = new OAuthEvents
//      {
//          OnCreatingTicket = async context =>
//          {
//              var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
//              request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//              request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
//              var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
//              response.EnsureSuccessStatusCode();
//              var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
//              context.RunClaimActions(user.RootElement);
//          }
//      };
//  });


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
