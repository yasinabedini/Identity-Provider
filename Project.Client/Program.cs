using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

#region Http Clients
builder.Services.AddHttpClient("UserService", client =>
{
client.BaseAddress = new Uri("https://localhost:7024/");
});

builder.Services.AddHttpClient("oAtuh", c =>
{
    c.BaseAddress = new Uri("https://localhost:5001");
}); 
#endregion


#region Authentication

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(c =>
{
c.DefaultScheme = "Cookies";
c.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")
.AddOpenIdConnect("oidc", c =>
{
c.Authority = "https://localhost:5001";
c.ClientId = "Project.Client";
c.ClientSecret = "Project.Client";
c.ResponseType = "code";
c.Scope.Clear();
c.Scope.Add("openid");
c.Scope.Add("profile");
c.Scope.Add("Project.Api");
c.Scope.Add("offline_access");
c.GetClaimsFromUserInfoEndpoint = true;
c.SaveTokens = true;
}); 
#endregion


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
