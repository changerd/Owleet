using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Owleet.Filters;
using Owleet.Models;

namespace Owleet
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(opts => {
                opts.Password.RequiredLength = 5;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры
            })
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Регистрация фильтров проверки существования записей
            services.AddScoped<ValidateEntityExistsAttribute<Test>>();
            services.AddScoped<ValidateEntityExistsAttribute<Question>>();
            services.AddScoped<ValidateEntityExistsAttribute<Answer>>();
            services.AddScoped<ValidateEntityExistsAttribute<Tournament>>();
            services.AddScoped<ValidateTestAuthorAttribute>();
            
            services.AddMvc(options => { 
                    // Регистрация фильтра валидации модели
                    options.Filters.Add(typeof(ValidateModelAttribute));
                    // Изменение сообщений об ошибках валидации
                    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(val => $"Значение '{val}' недопустимо.");
                    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(val => $"Значение для свойства '{val}' не было предоставлено.");
                    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Значение обязательное.");
                    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "Требуется непустое тело запроса.");
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(val => $"Значение '{val}' недопустимо.");
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((v,p) => $"Значение '{v}' недопустимо для {p}.");
                    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(val => $"Значение '{val}' недопустимо.");
                    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(val => $"Предоставленное значение недопустимо для {val}.");
                    options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "Предоставленное значение недействительно.");
                    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(val => $"Поле {val} должно быть числом.");
                    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "Поле должно быть числом.");
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
