using System.Text.Json;
using Lab3.Data;
using Lab3.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace Lab3;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true;
                cancellationTokenSource.Cancel();
            };

            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly(nameof(Lab3))
                )
            );

            builder.Services.AddAutoMapper(typeof(StudentProfile).Assembly);

            builder.Services.AddScoped<ExceptionHandlingMiddleware>();

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            builder
                .Services.AddControllers(options =>
                    options.SuppressAsyncSuffixInActionNames = false
                )
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddResponseCaching();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                await scope
                    .ServiceProvider.GetRequiredService<ApplicationDbContext>()
                    .Database.MigrateAsync(cancellationToken);
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseResponseCaching();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            using var f = new StreamWriter("D:\\Temp\\log.txt");
            f.WriteLine(ex.ToString());
        }
    }
}
