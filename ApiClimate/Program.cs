using ApiClimate.Data;
using ApiClimate.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigurationServices(IServiceCollection services)
    {
        //AddCors
        services.AddCors(c =>
        {
            c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        #region DatabaseConnection

        services.AddDbContext<ClimateDbContext>(options =>
        {


            options.UseSqlServer(
                               Configuration.GetConnectionString("DbConnString"));
        });
        #endregion

        #region AddInicialize
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddScoped<AuthenticationServices, AuthenticationServices>();
        services.AddScoped<SearchServices, SearchServices>();

        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);


        });
        #region Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                         ClockSkew = TimeSpan.Zero
                     });
    }
    #endregion

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {

        app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

    }
    #endregion
}

