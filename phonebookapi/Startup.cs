using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PhonebookApi.Models;
using PhonebookApi.Repository;

namespace PhonebookApi
{
    public class Startup
    {
        readonly string Origins = "_allowedorigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDBSettings>(
                Configuration.GetSection(nameof(MongoDBSettings)));

            services.AddSingleton<IMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddCors(options =>
            {
                options.AddPolicy(name: Origins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost",
                                                          "http://localhost:4200");
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();

                                  });
            });

            services.AddTransient<IMongoPhoneBookDBContext, MongoPhoneBookDBContext>();
            services.AddTransient<IPhoneBookRepository, PhoneBookRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(Origins);

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
 