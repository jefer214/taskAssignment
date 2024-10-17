using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskingSystem.Server;
using TaskingSystem.Server.Models;
using TaskingSystem.Server.Repositories;
using TaskingSystem.Server.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(jwtOption =>
{
    string? key = config.GetValue<string>("JwtConfig:Key");
    byte[] keyBytes = Encoding.ASCII.GetBytes(key);
    jwtOption.SaveToken = true;
    jwtOption.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true
    };
});

string? connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskingSystem.Api", Version = "v1" });
    opt.AddSecurityDefinition(JwtAuthenticationDefaults.BearerPrefix, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = JwtAuthenticationDefaults.HeaderName,
        Type = SecuritySchemeType.Http,
        BearerFormat = JwtAuthenticationDefaults.AuthenticationScheme,
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtAuthenticationDefaults.BearerPrefix
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddDbContext<TaskAssignmentContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
