using E_Commerce.api.APILayer.CustomExceptionMiddleware;
//using E_Commerce.api.APILayer.Extensions;
using E_Commerce.core.ApplicationLayer.DTOModel.Helpers;
using E_Commerce.core.ApplicationLayer.Interface;
using E_Commerce.infrastructure.RepositoryLayer;
using E_Commerce.infrastructure.RepositoryLayer.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(C =>
{
    C.EnableAnnotations();

    C.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Swagger API",
        Description = "FlexKart E-Commerce Project",

    });
    
    C.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme

    {

        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",

        In = ParameterLocation.Header,

        Name = "Authorization",

        Type = SecuritySchemeType.ApiKey

    });

    C.ExampleFilters();

    C.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

    C.OperationFilter<SecurityRequirementsOperationFilter>();

});

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.AddAutoMapper(typeof(GeneralProfile).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<AdminDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<ILogin, Login>();
builder.Services.AddScoped<ICategory, Category>();
builder.Services.AddScoped<IBrand, Brand>();

builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
////var logger = app.Services.GetRequiredService<ILoggerManager>();
//app.ConfigureExceptionHandler(logger);
//app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(MyAllowSpecificOrigins);
app.Run();
