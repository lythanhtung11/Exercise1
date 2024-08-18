using Exercise1;
using Exercise1.Services.Users;
using Microsoft.Extensions.DependencyInjection;

var host = AppConfig.CreateHostBuilder(args).Build();

//Execute the job
var userService = host.Services.GetRequiredService<IUserService>();
await userService.UpdateUserStatusAsync();

Console.WriteLine("Press [Enter] to exit...");
Console.ReadLine();