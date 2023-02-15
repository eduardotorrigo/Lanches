using LanchesMac.Areas.Admin.Services;
using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;
using LanchesMac.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

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

        services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<RelatorioVendasService>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", politica =>
            {
                politica.RequireRole("Admin");
            });
        });
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });

        services.AddMemoryCache();
        services.AddSession();

    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seed)
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
        seed.SeedRoles();
        seed.SeedUsers();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new { controller = "Lanche", action = "List" });

            endpoints.MapControllerRoute(
            name: "admin",
            pattern: "admin",
            defaults: new { controller = "Admin", action = "Index" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}