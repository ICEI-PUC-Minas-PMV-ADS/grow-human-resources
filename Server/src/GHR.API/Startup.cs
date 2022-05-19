using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

using GHR.Application;
using GHR.Application.Serices.Contracts.Metas;
using GHR.Application.Services.Contracts.Cargos;
using GHR.Application.Services.Contracts.Contas;
using GHR.Application.Services.Contracts.Departamentos;
using GHR.Application.Services.Contracts.Funcionarios;
using GHR.Application.Services.Implements.Cargos;
using GHR.Application.Services.Implements.Contas;
using GHR.Application.Services.Implements.Departamentos;
using GHR.Application.Services.Implements.Funcionarios;
using GHR.Application.Services.Implements.Metas;
using GHR.Domain.DataBase.Contas;
using GHR.Persistence.Implements.Contracts.Metas;
using GHR.Persistence.Interfaces.Contexts;
using GHR.Persistence.Interfaces.Contracts.Cargos;
using GHR.Persistence.Interfaces.Contracts.Contas;
using GHR.Persistence.Interfaces.Contracts.Departamentos;
using GHR.Persistence.Interfaces.Contracts.Funcionarios;
using GHR.Persistence.Interfaces.Contracts.Global;
using GHR.Persistence.Interfaces.Implements.Cargos;
using GHR.Persistence.Interfaces.Implements.Contas;
using GHR.Persistence.Interfaces.Implements.Departamentos;
using GHR.Persistence.Interfaces.Implements.Funcionarios;
using GHR.Persistence.Interfaces.Implements.Global;
using GHR.Persistence.Interfaces.Implements.Metas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GHR.API
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
            services
                .AddDbContext<GHRContext>(
                    context => context.UseSqlite(Configuration.GetConnectionString("Default")));

            services
                .AddIdentityCore<Conta>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                    })
                .AddRoles<Funcao>()
                .AddRoleManager<RoleManager<Funcao>>()
                .AddSignInManager<SignInManager<Conta>>()
                .AddRoleValidator<RoleValidator<Funcao>>()
                .AddEntityFrameworkStores<GHRContext>()
                .AddDefaultTokenProviders();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            services
                .AddControllers()
                .AddJsonOptions(
                        options => options.JsonSerializerOptions.Converters
                            .Add(new JsonStringEnumConverter())
                    )
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling =
                                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<IDepartamentoService, DepartamentoService>();
            services.AddScoped<IFuncionarioMetaService, FuncionarioMetaService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<IMetaService, MetaService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IContaService, ContaService>();

            services.AddScoped<ICargoPersistence, CargoPersistence>();
            services.AddScoped<IDepartamentoPersistence, DepartamentoPersistence>();
            services.AddScoped<IFuncionarioMetaPersistence, FuncionarioMetaPersistence>();
            services.AddScoped<IFuncionarioPersistence, FuncionarioPersistence>();
            services.AddScoped<IGlobalPersistence, GlobalPersistence>();
            services.AddScoped<IMetaPersistence, MetaPersistence>();
            services.AddScoped<IContaPersistence, ContaPersistence>();


            services.AddCors();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "GHR.API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Beares. Entre com 'Bearer [espaço] em seguida coloque sei token.
                                    Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "GHR.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(cors => cors.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin());

            app.UseStaticFiles(new StaticFileOptions() {

                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
