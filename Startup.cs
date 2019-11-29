using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace sandbox
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // services.AddScoped<IValueService, ValueService>();
            // services.AddSingleton<IValueProvider, RandomValueProvider>();
        }

        public void ConfigureContainer(ContainerBuilder builder) {
            builder.RegisterType<ValueService>().As<IValueService>();
            builder.RegisterType<RandomValueProvider>()
                // .As<IValueProvider>()
                .Keyed<IValueProvider>("Rnd");

            builder
                .RegisterDecorator<IValueProvider>((c, inner) => new ValueProviderCacheDecorator(inner), fromKey: "Rnd")
                .As<IValueProvider>();
            // builder.RegisterType<ValueProviderCacheDecorator>()
            //     .As<IValueProvider>()
            //     .WithParameter(new ResolvedParameter((pi, ctx) => ctx.ResolveNamed<IValueProvider>("Rnd")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
