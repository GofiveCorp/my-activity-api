
using Activity.Configuration;
using Activity.Data;
using Activity.Repositories.Implementation;
using Activity.Repositories.Interface;
using Activity.Services.Implementation;
using Activity.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Activity {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => options.OperationFilter<AddRequiredHeaderParameter>());
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
            builder.Services.AddScoped<IActivityService, ActivityService>();
            builder.Services.AddSingleton<ApiKeyFilter>();
            builder.Services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("ApiCorsPolicy");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
