
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuizWorkShop.IRepository;
using QuizWorkShop.Mapping;
using QuizWorkShop.Models;
using QuizWorkShop.Repository;
using System.Text;

namespace QuizWorkShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            //Add Connection String
            builder.Services.AddDbContext<QuizContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            //Inject Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<QuizContext>();

            // Add default Schema to validate on Token
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "mySchema"; // Note: Removed space in schema name
                options.DefaultChallengeScheme = "mySchema"; // Added DefaultChallengeScheme
            })
            .AddJwtBearer("mySchema", options =>
            {
                string key = "Welcom to my secrit key in Qiz0123456789abcde"; // Ensure this key is stored securely, e.g., in app settings
                var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, // Validate the token expiration
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey
                };
            });


            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IGetAllQuiz_SP, SP_GetAllQuiz>();

            

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
