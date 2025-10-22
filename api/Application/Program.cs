using Api.CrossCutting.Dependecies;
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
        Description = "API para gerenciamento de alunos, turmas e matrículas com documentação completa",
        Contact = new OpenApiContact
        {
            Name = "Equipe de Desenvolvimento",
            Email = "contato@escola.com",
            Url = new Uri("https://escola.com")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Add XML comments if available (optional - see below for setup)
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
            return new[] { api.GroupName };
        }

        if (api.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName))
        {
            return new[] { controllerName };
        }

        return new[] { "Default" };
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

app.UseAuthorization();

app.MapControllers();

app.Run();
