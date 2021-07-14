using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using POCJira.Shared.Configurations;
using POCJira.Shared.Extensions;

namespace POCJira.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddLogging();
            services.AddAutoMapper(new Assembly[]{
                Assembly.Load("POCJira.WebApi"),
                Assembly.Load("POCJira.Repository"),
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new OpenApiInfo { Title = "API teste de WebHooks do Jira", Version = "v1" });
               });
            services.AddOptions();
            services.ConfigureAppConfiguration<Application>(Configuration.GetSection("AppConfiguration"));
            services.AddJiraHttpClient(Configuration.GetSection("AppConfiguration").Get<Application>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API WebHooks"));
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddAutofacServiceProvider();
        }
    }
}
