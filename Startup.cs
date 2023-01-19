using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories;
using LanchesMac.Infra.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        var connectionsString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionsString, ServerVersion.AutoDetect(connectionsString));
        });

        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<ILancheRepository, LancheRepository>();
        
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}