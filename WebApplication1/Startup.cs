using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Domain;
using WebApplication1.Domain.Repositories;
using WebApplication1.Domain.Repositories.EntityFramework;
using WebApplication1.Service;

namespace WebApplication1
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			//	Подключаем конфиг из appsetings.json:
			Configuration.Bind("Project", new Config());

			//	Подключаем нужный функционал приложения в качестве сервисов:
			services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
			services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
			services.AddTransient<DataManager>();

			//	Подключаем контекст БД:
			services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

			//	Настраиваем Identity-систему:
			services.AddIdentity<IdentityUser, IdentityRole>(opts =>
			{
				opts.User.RequireUniqueEmail = true;
				opts.Password.RequiredLength = 6;
				opts.Password.RequireNonAlphanumeric = false;
				opts.Password.RequireLowercase = false;
				opts.Password.RequireUppercase = false;
				opts.Password.RequireDigit = false;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

			//	Настраиваем authentication cookie:
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.Name = "MyCompanyAuth";
				options.Cookie.HttpOnly = true;
				options.LoginPath = "/account/login";
				options.AccessDeniedPath = "/account/accessdenied";
				options.SlidingExpiration = true;
			});

			//	Настраиваем политику авторизации для AdminArea:
			services.AddAuthorization(x =>
			{
				x.AddPolicy("AdminArea", policy => policy.RequireRole("admin"));
			});

			//	Добавляем поддержку контроллеров и представлений (MVC):
			services.AddControllersWithViews(x =>
			{
				x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
			})
			//	Выставляем совместимость с asp.net core 3.0:
			.SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
		}
		
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//	Порядок регистрации middleware (сервисов) очень важен!

			//	Для отслеживания ошибок на деве (debug):
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//	Подключаем поддержку статичных файлов (css, js, ...):
			app.UseStaticFiles();

			//	Система маршрутизации:
			app.UseRouting();

			//	Подключаем аутентификацию и авторизацию:
			app.UseCookiePolicy();
			app.UseAuthentication();
			app.UseAuthorization();

			//	Регистрация маршрутов (эндпоинтов):
			app.UseEndpoints(endpoints =>
			{
				//	Маршрут для области admin:
				endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				//	Стандартный (Home) адресс, маршрут:
				endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

				/*endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello World!");
				});*/
			});
		}
	}
}
