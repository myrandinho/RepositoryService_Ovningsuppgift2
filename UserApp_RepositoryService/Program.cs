using DataAcess_SharedLibary.Repositories;
using DataAcess_SharedLibary.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserApp_RepositoryService.LocalServices;

var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\my-projects\RepositoryService_Ovningsuppgift2\DataAcess_SharedLibary\Data\database_repo_serv.mdf;Integrated Security=True";

var app = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton(new RoleRepository(connectionString));
    services.AddSingleton(new UserRepository(connectionString));
    services.AddSingleton<RoleService>();
    services.AddSingleton<UserService>();
    services.AddSingleton<MenuService>();
}).Build();

app.Start();

var menuService = app.Services.GetRequiredService<MenuService>();
menuService.MainMenu();