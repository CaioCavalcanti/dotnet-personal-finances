using System;
using Accounts.API.Infrastructure.AutofacModules;
using Accounts.API.Infrastructure.Filters;
using Accounts.API.Infrastructure.Swagger;
using Accounts.Infrastructure.Database;
using Accounts.Infrastructure.EventBus;
using Accounts.Infrastructure.Messaging.Queue;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Accounts.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            var container = new ContainerBuilder();

            container.Populate(services);
            container.RegisterAutofacModules();

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Accounts API v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });

            services
                .AddMemoryCache()
                .AddCustomSwagger()
                .AddAccountsDbContext(Configuration)
                .AddMessageQueue()
                .AddEventBus();
        }
    }
}
