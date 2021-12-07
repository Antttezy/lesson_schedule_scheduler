using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Authorization;
using Scheduler.Context;
using Scheduler.Inetrfaces;
using Scheduler.Services;
using System;

namespace Scheduler.Data
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DataContext"), b => b.MigrationsAssembly("Scheduler")));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IWorkloadRepository, WorkloadRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();

            services.AddSingleton<JwtAccessTokenOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                JwtAccessTokenOptions options = new(configuration);

                o.TokenValidationParameters = new()
                {
                    IssuerSigningKey = options.GetAsymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = options.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = options.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers();

            services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
            builder.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
            ));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
