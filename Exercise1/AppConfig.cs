using Exercise1.Databases;
using Exercise1.Jobs;
using Exercise1.Services;
using Exercise1.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Exercise1
{
    public static class AppConfig
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
          .ConfigureAppConfiguration((context, config) =>
          {
              // Load the appsettings.json file
              config.SetBasePath(Directory.GetCurrentDirectory());
              config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
          })
          .ConfigureServices((context, services) =>
          {
              // Bind JobSettings from configuration
              var jobSettings = context.Configuration.GetSection("JobSettings").Get<JobSettings>();
              services.AddSingleton(jobSettings);

              //Add database context
              services.AddDbContext<DatabaseContext>(options =>
                  options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

              // Register the DI
              services.AddScoped<IUserRepository, UserRepository>();
              services.AddScoped<IUserService, UserService>();
              services.AddScoped<IEmailService, EmailService>();

          });
    }
}
