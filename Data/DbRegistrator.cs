using ComputerCheker.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerCheker.Data
{
    static class DbRegistrator
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services, IConfiguration configuration) => services
            .AddDbContext<ComputerDB>(opt =>
            {
                var type = configuration["TYPE"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Не определен тип БД");
                    default: throw new InvalidOperationException("Не определено Бд");


                    case "MSSQL":
                        opt.UseSqlServer(configuration.GetConnectionString(type));
                        break;
                    case "InMemory":
                        opt.UseInMemoryDatabase("Computer.db");
                        break;
                }
            })
            .AddTransient<DbInitializer>();
    }
}
