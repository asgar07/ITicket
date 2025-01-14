using DomainLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using ServiceLayer;
using ServiceLayer.Mapping;
using ServiceLayer.Services;
using ServiceLayer.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var configuration = new ConfigurationBuilder()
	.SetBasePath(AppContext.BaseDirectory)
	.AddJsonFile("appsettings.json")
	.Build();

builder.Services.AddControllers();
builder.Services.AddIdentity<AppUser, IdentityRole>()
 .AddEntityFrameworkStores<AppDbContext>()
 .AddDefaultTokenProviders();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowReactApp",
		builder => builder.WithOrigins("http://localhost:3000")
		.AllowAnyHeader()
		.AllowAnyMethod());
});

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireDigit = true;

	options.User.RequireUniqueEmail = true;

	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
	options.Lockout.AllowedForNewUsers = true;
});



builder.Services
	.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(cfg =>
	{
		cfg.RequireHttpsMetadata = false;
		cfg.SaveToken = true;
		cfg.TokenValidationParameters = new TokenValidationParameters
		{
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
			ClockSkew = TimeSpan.Zero // remove delay of token when expire
		};
	});
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddRepositoryLayer();
builder.Services.AddServiceLayer();
builder.Services.AddTransient<ITokenService, TokenService>();




//builder.Services.AddSwaggerGen(options =>
//{

//	options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//	{
//		Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
//		In = ParameterLocation.Header,
//		Name = "Authorization",
//		Type = SecuritySchemeType.ApiKey
//	});

//	options.OperationFilter<SecurityRequirementsOperationFilter>();
//});

var app = builder.Build();
if (false)
{
	app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT_ApiIdentity v1"));
}


app.UseHttpsRedirection();

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());
// Use CORS policy
app.UseCors("AllowReactApp");

app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapFallbackToFile("index.html");
	endpoints.MapControllers();
});

app.Run();