using Gatti.Repositorio;
using GestaoVeiculo.Data;
using GestaoVeiculo.Repositorio;
using GestaoVeiculo.Services.GestaoService;
using GestaoVeiculo.Services.LoginService;
using GestaoVeiculo.Services.SenhaService;
using GestaoVeiculo.Services.SessaoService;
using Microsoft.EntityFrameworkCore;

namespace GestaoVeiculo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ILoginInterface, LoginService>();
            builder.Services.AddScoped<ISenhaInterface, SenhaService>();
            builder.Services.AddScoped<ISessaoInterface, SessaoService>();
            builder.Services.AddScoped<IGestaoInterface, GestaoService>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;


            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
