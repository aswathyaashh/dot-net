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

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
    C.OperationFilter<SecurityRequirementsOperationFilter>();
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //C.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(CategoryAutoMapping).Assembly);
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
builder.Services.AddScoped<ICategory , Category>();
builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
//                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);

app.Run();
