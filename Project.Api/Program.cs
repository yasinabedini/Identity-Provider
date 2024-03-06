using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project.Api.Context;
using Zamin.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(t=>t.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConectionString")));

#region AA
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", j =>
{
j.Authority = "https://localhost:5001/";
j.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
{
ValidateAudience = false
};
});

builder.Services.AddAuthorization(c =>
{
    c.AddPolicy("myPolicy", c =>
    {
        c.RequireClaim("scope", "Project.Api");
    });
}); 
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("myPolicy");

app.UseHttpsRedirection();

app.Run();
