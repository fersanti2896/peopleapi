using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace PeopleAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(opc => opc.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "People API", Version = "v1", Description = "Web API of People", Contact = new OpenApiContact { Email = "fersanti2896@gmail.com" } });

                var fileXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var routeXML = Path.Combine(AppContext.BaseDirectory, fileXML);
                c.IncludeXmlComments(routeXML);
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options => {
                options.AddPolicy("FreeAPI",
                    builder => {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) {

            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "People API");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("FreeAPI");

            app.UseEndpoints(end => {
                end.MapControllers();
            });
        }
    }
}
