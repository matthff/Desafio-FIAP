using System.Text;
using Api.CrossCutting.Dependecies;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Swagger/OpenAPI with Swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // API Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Gestão Escolar",
        Description = "API para gerenciamento administrativo de alunos, turmas e matrículas",
        Contact = new OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "contato@fiap.com.br",
            Url = new Uri("https://fiapdesafio.com.br")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Entre com o Token JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });

    // Add XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    // Tag definitions for grouping
    options.TagActionsBy(api =>
    {
        if (api.GroupName != null)
        {
            return [api.GroupName];
        }

        if (api.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName))
        {
            return [controllerName];
        }

        return ["Default"];
    });

    options.DocInclusionPredicate((name, api) => true);

    // Enable annotations for better descriptions
    options.EnableAnnotations();
});

// Register your repositories (using scoped lifetime for DB contexts)
ConfigureRepository.ConfigureDependenciesRepository(builder.Services, builder.Configuration);

// Register your services
ConfigureService.ConfigureDependenciesServices(builder.Services);

// Register AutoMapper profiles
ConfigureAutoMapper.ConfigureAutoMapperProfiles(builder.Services);

// Setup Token configuration for JWT authentication
ConfigureAuthentication.AddTokenConfiguration(builder.Services, builder.Configuration);

// Setup JWT authentication
ConfigureAuthentication.AddJwtAuthentication(builder.Services);

// Setup authorization policies
ConfigureAuthentication.AddAuthorizationPolicies(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "api-docs/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/api-docs/v1/swagger.json", "API v1");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "API de Gestão Escolar - Documentação";
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        options.DisplayRequestDuration();
        options.EnableFilter();
        options.EnableDeepLinking();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
