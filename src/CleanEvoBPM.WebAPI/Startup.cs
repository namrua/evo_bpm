using System;
using CleanEvoBPM.Application;
using CleanEvoBPM.Infrastructure;
using CleanEvoBPM.WebAPI.Filters;
using CleanEvoBPM.WebAPI.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using KeycloakIdentityModel;
using KeycloakIdentityModel.Models.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using CleanEvoBPM.WebAPI.Helper;

namespace CleanEvoBPM.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;
            services.AddCors();
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter()));
            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddJwtBearer(o =>
                        {
                            o.RequireHttpsMetadata = false;
                            o.Authority = Configuration["EvoBPM_Authen:Authority"];
                            o.Audience = Configuration["EvoBPM_Authen:Audience"];
                            o.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateAudience = false
                            };

                            o.Events = new JwtBearerEvents()
                            {
                                OnAuthenticationFailed = c =>
                                {
                                    c.NoResult();

                                    c.Response.StatusCode = 500;
                                    c.Response.ContentType = "text/plain";
                                    if (Environment.IsDevelopment())
                                    {
                                        return c.Response.WriteAsync(c.Exception.ToString());
                                    }
                                    return c.Response.WriteAsync("An error occured processing your authentication.");
                                }
                            };
                        });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("ROLE_ADMIN"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EVOBPM API", Version = "v1" });
                c.AddFluentValidationRules();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(Configuration["EvoBPM_Authen:AuthURL"]),
                            TokenUrl = new Uri(Configuration["EvoBPM_Authen:TokenURL"]),
                            Scopes = new Dictionary<string, string>
                            {
                                {"email", "evobpm" }
                            }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            services.AddHttpContextAccessor();
            services.AddTransient<IUserHelper, UserHelper>();
            services.AddElasticsearch(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition"));

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Instant POS API V1");
                c.OAuthClientId(Configuration["EvoBPM_Authen:ClientId"]);
                c.OAuthClientSecret(Configuration["EvoBPM_Authen:ClientSecret"]);
                c.OAuthAppName("Authorization with keycloak");
                c.OAuthUsePkce();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
