using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Repositories.Abstraction;
using Project.Infrastructure.Repositories.Implementation;
using Project.Service.Services.Abstraction;
using Project.Service.Services.Implementation;

namespace Project.Service.Utilities.DependencyResolvers
{
    public static class ProjectDependencies
    {
        public static void AddProjectDependencies(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped(typeof(ILoanRepository),typeof(LoanRepository));
            services.AddScoped(typeof(IInvoiceRepository),typeof(InvoiceRepository));
            services.AddScoped(typeof(IOperationRepository),typeof(OperationRepository));
            services.AddScoped(typeof(IOrderRepository),typeof(OrderRepository));
            services.AddScoped(typeof(IClientRepository),typeof(ClientRepository));


            // Services
            services.AddScoped(typeof(ILoanService), typeof(LoanService));
            services.AddScoped(typeof(IClientService), typeof(ClientService));
        }
    }
}
