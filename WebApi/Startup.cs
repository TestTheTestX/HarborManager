using HarborManager.Contracts;
using HarborManager.Logic;
using HarborManager.RepositoryInMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace HarborManager.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {         
            services.AddDbContext<HarborDbContext>(opt => opt.UseInMemoryDatabase("HarborDB"));
            services.AddMvc();
            services.AddScoped<IHarborDbContext, HarborDbContext>();
            services.AddScoped<IBookings, Bookings>();
            services.AddScoped<IBoats, Boats>();
            services.AddScoped<IDocks, Docks>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
