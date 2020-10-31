using Bookinist.DAL.Entityes;
using Bookinist.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bookinist.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
           .AddTransient<IRepository<Book>, BooksRepository>()
           .AddTransient<IRepository<Category>, DbRepository<Category>>()
           .AddTransient<IRepository<Seller>, DbRepository<Seller>>()
           .AddTransient<IRepository<Buyer>, DbRepository<Buyer>>()
           .AddTransient<IRepository<Deal>, DealsRepository>()
        ;
    }
}
